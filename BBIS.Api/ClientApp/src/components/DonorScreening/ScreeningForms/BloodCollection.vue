<template>
    <v-form class="form-container" ref="form" lazy-validation>
        <v-row>
            <!-- Start Time -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Start Time</label>
                <v-text-field dense
                              outlined
                              type="time"
                              label="Start Time of Blood Collection" />
            </v-col>

            <!-- End Time -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">End Time</label>
                <v-text-field dense outlined type="time" label="End Time of Blood Collection" />
            </v-col>

            <!-- Donor Reaction -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Donor Reaction</label>
                <v-text-field dense outlined label="Donor Reaction" />
            </v-col>
        </v-row>

        <v-row align="center">
            <!-- Success Radio -->
            <v-col cols="12" md="2" class="mb-7">
                <label class="font-weight-bold">Success?</label>
                <v-radio-group row>
                    <v-radio label="Yes" value="yes" />
                    <v-radio label="No" value="no" />
                </v-radio-group>
            </v-col>

            <!-- Blood Type -->
            <v-col cols="12" md="2">
                <label class="font-weight-bold">Blood Type</label>
                <v-select :items="bloodTypesOptions"
                          label="Blood Type"
                          dense
                          outlined />
            </v-col>

            <!-- Collected Blood Amount -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Collected Blood Amount (mL)</label>
                <v-text-field dense outlined label="Collected Blood Amount" />
            </v-col>

            <!-- Patient Allocation -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Patient Allocation</label>
                <v-row>
                    <v-col cols="4">
                        <v-text-field dense outlined label="Last Name" />
                    </v-col>
                    <v-col cols="4">
                        <v-text-field dense outlined label="First Name" />
                    </v-col>
                    <v-col cols="4">
                        <v-text-field dense outlined label="Middle Name" />
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
            <v-btn color="default" large tile class="mr-2" @click=""><v-icon color="success" size="25" left>mdi-check</v-icon> Approve</v-btn>
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

        protected formValid: boolean = true;
        protected rules: any = { ...Common.ValidationRules }
        protected errorMessage: string = '';
        protected showInHouseOptions: boolean = false;
        protected showRecentDonations: boolean = false;
        protected showPatientDirectedFields: boolean = false;
        protected showMobileBloodDonationFields: boolean = false;
       // protected donorInitialScreening: IDonorInitialScreeningDto = new DonorInitialScreeningDto();
        protected recentDonations: Array<IDonorRecentDonationDto> = new Array<IDonorRecentDonationDto>();
        protected isEditingValue: boolean = false;
        protected isDisabled: boolean = true;

        protected approvedSegmentSerialNumber: string = "";

        public get options(): (key: string) => Array<ILookupOptions> {
            return (key) => this.lookupModule.getOptionsByKey(key);
        }

        protected get bloodTypesOptions(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.BloodTypes).map(x => { return { text: x.Text, value: x.Value } });
        }

        protected showIssuanceDialog: boolean = false;


       protected handleIssued(segmentSerialNumber: string): void {
          this.approvedSegmentSerialNumber = segmentSerialNumber;
       }

    }
    

</script>