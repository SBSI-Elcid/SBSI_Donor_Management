<template>
    <v-form class="form-container" ref="form" lazy-validation>
        <v-row>
            <!-- Start Time -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Start Time</label>
                <v-text-field dense
                              outlined
                              type="time"
                              label="Start Time of Blood Collection"
                              v-model ="startTime"/>
            </v-col>

            <!-- End Time -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">End Time</label>
                <v-text-field 
                              dense 
                              outlined 
                              type="time" 
                              label="End Time of Blood Collection"
                              v-model = "endTime"
                              />
            </v-col>

            <!-- Donor Reaction -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Donor Reaction</label>
                <v-text-field 
                              dense 
                              outlined 
                              label="Donor Reaction" 
                              v-model ="donorBloodCollection.UnwellReason"
                              />
            </v-col>
        </v-row>

        <v-row align="center">
            <!-- Success Radio -->
            <v-col cols="12" md="2" class="mb-7">
                <label class="font-weight-bold">Success?</label>
                <v-radio-group row v-model ="donorBloodCollection.Success"> 
                    <v-radio label="Yes" value="true" />
                    <v-radio label="No" value="false" />
                </v-radio-group>
            </v-col>

            <!-- Blood Type -->
            <!--<v-col cols="12" md="2">
                <label class="font-weight-bold">Blood Type</label>
                <v-select :items="bloodTypesOptions"
                          label="Blood Type"
                          dense
                          outlined />
            </v-col>-->

            <!-- Collected Blood Amount -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Collected Blood Amount (mL)</label>
                <v-text-field 
                              dense 
                              outlined 
                              label="Collected Blood Amount" 
                              v-model ="donorBloodCollection.CollectedBloodAmount"
                              />
            </v-col>

            <!-- Patient Allocation -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Patient Allocation</label>
                <v-row>
                    <v-col cols="4">
                        <v-text-field 
                                      dense 
                                      outlined 
                                      label="Last Name" 
                                      v-model="donorBloodCollection.PatientLastName"
                                      />
                    </v-col>
                    <v-col cols="4">
                        <v-text-field 
                                      dense 
                                      outlined 
                                      label="First Name" 
                                      v-model="donorBloodCollection.PatientFirstName"
                                      />
                    </v-col>
                    <v-col cols="4">
                        <v-text-field 
                                      dense 
                                      outlined 
                                      label="Middle Name" 
                                       v-model="donorBloodCollection.PatientMiddleName"
                                      />
                    </v-col>
                </v-row>
            </v-col>
        </v-row>

        <v-dialog v-model="showIssuanceDialog" max-width="800px" persistent>
            <v-card>
                <v-card-title>
                    Add Blood Bag
                    <v-spacer></v-spacer>
                    <!--<v-btn icon @click="showIssuanceDialog = false">
                    <v-icon>mdi-close</v-icon>
                </v-btn>-->
                </v-card-title>

                <v-card-text style="overflow-y: auto; max-height: 540px;">
                    <IssuanceOfBloodBagModal @close="showIssuanceDialog = false" @Issued="handleIssued" />
                </v-card-text>
            </v-card>
        </v-dialog>

        <!--<v-dialog v-model="showIssuanceDialog" max-width="1000px " persistent>
        <v-card class="">
            <IssuanceOfBloodBagModal @close="showIssuanceDialog = false" />
        </v-card>
    </v-dialog>-->
        <div  v-if="approvedSegmentSerialNumber" class="section-outer-container text-left pt-3 pb-2">
            <strong>Segment Serial Number Approved:</strong> {{ approvedSegmentSerialNumber }}
        </div>


        <div class="section-outer-container text-right pt-3 pb-2">
            <!--<v-btn color="default" large tile class="mr-2" v-if="" @click=""><v-icon color="success" size="25" left>mdi-content-save</v-icon> Save</v-btn>-->
            <v-btn color="default" large tile class="mr-2" @click="showIssuanceDialog = true"><v-icon color="success" size="25" left>mdi-blood-bag</v-icon> Add Bloog Bag</v-btn>
            <v-btn color="default" large tile class="mr-2" @click="onSubmit"><v-icon color="success" size="25" left>mdi-check</v-icon> Approve</v-btn>
            <v-btn color="default" large tile class="mr-2" @click=""><v-icon color="warning" size="25" left>mdi-cancel</v-icon> Mark as Deferred</v-btn>
        </div>
    </v-form>
</template>

<script lang="ts">
    import VueBase from '@/components/VueBase';
    import { Component, Watch } from 'vue-property-decorator';
    import { getModule } from 'vuex-module-decorators';
    import DonorModule from '@/store/DonorModule';
    import LookupModule from '@/store/LookupModule';
    import DonorScreeningService from '@/services/DonorScreeningService';
    import ApplicationSettingService from '@/services/ApplicationSettingService';
    import Common from '@/common/Common';
    import { DonorBloodCollectionDto, IDonorBloodCollectionDto } from '@/models/DonorScreening/DonorBloodCollectionDto';
    import { BloodBagCollections } from '@/models/Enums/BloodBagCollections';
    import { ILookupOptions } from '@/models/Lookups/LookupOptions';
    import { LookupKeys } from '@/models/Enums/LookupKeys';
    import { ApplicationSettingKeys } from '@/models/ApplicationSettingKeys';
    import { DonorStatus } from '@/models/Enums/DonorStatus';
    import IssuanceOfBloodBagModal from '@/components/DonorScreening/ScreeningForms/IssuanceOfBloodBagModal.vue';
    import moment from 'moment';
    import JsBarcode from 'jsbarcode';

   @Component({
      components: {
        IssuanceOfBloodBagModal,
      },
    })

    export default class BloodCollection extends VueBase {
        protected donorModule: DonorModule = getModule(DonorModule, this.$store);
        protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
        protected donorScreeningService: DonorScreeningService = new DonorScreeningService();
       protected appSettingService: ApplicationSettingService = new ApplicationSettingService()
        protected formValid: boolean = true;
        protected rules: any = { ...Common.ValidationRules }
        protected errorMessage: string = '';
        protected showInHouseOptions: boolean = false;
        protected showRecentDonations: boolean = false;
        protected showPatientDirectedFields: boolean = false;
        protected showMobileBloodDonationFields: boolean = false;
       // protected donorInitialScreening: IDonorInitialScreeningDto = new DonorInitialScreeningDto();
        protected donorBloodCollection: IDonorBloodCollectionDto = new DonorBloodCollectionDto();
        protected recentDonations: Array<IDonorRecentDonationDto> = new Array<IDonorRecentDonationDto>();
        protected isEditingValue: boolean = false;
       protected isDisabled: boolean = true;
       protected startTime: string = '';
       protected endTime: string = '';

       protected approvedSegmentSerialNumber: string = "";
       protected showIssuanceDialog: boolean = false;

        public get options(): (key: string) => Array<ILookupOptions> {
            return (key) => this.lookupModule.getOptionsByKey(key);
        }

        protected get bloodTypesOptions(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.BloodTypes).map(x => { return { text: x.Text, value: x.Value } });
        }

       protected handleIssued(segmentSerialNumber: string): void {
           this.approvedSegmentSerialNumber = segmentSerialNumber;
       }
      

       protected async created(): Promise<void> {
           let loader = this.showLoader();

           try {
               await this.getBloodCollectionInfo();
           }
           catch (error) {
               console.log(error);
           }
           finally {
               loader.hide();
           }
       }


      
       protected async getBloodCollectionInfo(): Promise<void> {
           if (this.$route.params.reg_id && typeof (this.$route.params.reg_id) === 'string') {
               let transactionId = this.$route.params.reg_id;

               this.donorBloodCollection = await this.donorScreeningService.getBloodCollectionInfo(transactionId);
               console.log(this.donorBloodCollection);
               await this.loadUnitMeasureAppSetting();
               this.donorModule.setTransactionId(this.donorBloodCollection.DonorTransactionId);

               // Enable the fields when Donor Status is not deferred and not for initial screening and not for physical exam.
               if (Common.hasValue(this.donorBloodCollection.DonorStatus) && this.donorBloodCollection.DonorStatus !== DonorStatus.Deferred
                   && this.donorBloodCollection.DonorStatus !== DonorStatus.ForInitialScreening && this.donorBloodCollection.DonorStatus !== DonorStatus.ForPhysicalExamination) {
                   this.isDisabled = false;
                   if (this.donorBloodCollection.DonorStatus !== DonorStatus.ForBloodCollection) {
                       this.isEditingValue = true;
                   }
               }

               let hasBloodCollectionId = Common.hasValue(this.donorBloodCollection.DonorBloodCollectionId);

               // Show the selected value if there is an existing data or the current date and time if donor status is "For Blood Collection" 
               this.startTime = hasBloodCollectionId || this.isDisabled === false ? moment(this.donorBloodCollection.StartTime).format('HH:mm') : '';
               this.endTime = hasBloodCollectionId || this.isDisabled === false ? moment(this.donorBloodCollection.EndTime).format('HH:mm') : '';

               // Show the selected value if there is an existing data else set it to null.
               this.success = hasBloodCollectionId ? this.donorBloodCollection.Success : null;

               this.generatedSerialNumber = this.donorBloodCollection.UnitSerialNumber;
               if (Common.hasValue(this.generatedSerialNumber)) {
                   JsBarcode("#barcode", `${this.generatedSerialNumber}`, {
                       width: 2,
                       height: 60,
                       textAlign: 'left'
                   });
               }
           }
       }
       protected async loadUnitMeasureAppSetting(): Promise<void> {
           let settings = await this.appSettingService.getApplicationSettingsByKey([ApplicationSettingKeys.BloodCollectionUnitOfMeasure]);
           if (settings.length > 0) {
               this.donorBloodCollection.UnitOfMeasurement = settings[0].SettingValue;
           }
       }


       protected async onSubmit(): Promise<void> {
           this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
           if (this.formValid === false) {
               return;
           }

           if (this.donorBloodCollection.Success === false) {
               this.donorBloodCollection.DonorStatus = DonorStatus.Deferred;
               this.donorBloodCollection.DeferralStatus = 'Temporary';
           }
           else {
               this.donorBloodCollection.DonorStatus = DonorStatus.ForPostDonationCare;
           }

           let startTime = this.startTime.split(':');
           let endTime = this.endTime.split(':');

           this.donorBloodCollection.StartTime = moment({ hour: parseInt(startTime[0]), minute: parseInt(startTime[1]) }).toDate();
           this.donorBloodCollection.EndTime = moment({ hour: parseInt(endTime[0]), minute: parseInt(endTime[1]) }).toDate();

           let loader = this.showLoader();
           try {
               console.log(this.donorBloodCollection);
               await this.donorScreeningService.upsertBloodColllection(this.donorBloodCollection);
               this.notify_success('Form successfully submitted.');

               this.$router.push(`/donors`);
           }
           catch (error: any) {
               if (error.StatusCode != 500) {
                   let errorMessage = error.Message ?? "An error occured while processing your request.";
                   this.notify_error(errorMessage);
               }
           }
           finally {
               loader.hide();
           }
       }

    }
    

</script>