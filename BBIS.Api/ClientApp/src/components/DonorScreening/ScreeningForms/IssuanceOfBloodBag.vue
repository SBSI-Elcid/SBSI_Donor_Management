<template>
    <v-form class="form-container" ref="form" v-model="formValid" lazy-validation>
        <v-row class="mb-4" align="center">
            <!-- Blood Bag to be Used -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Blood Bag to be Used</label>
                <v-select :items="bloodBagCollectionOptions"
                          label="Blood Bag to be Used"
                          v-model = "donorBloodBagIssuance.BloodBagToBeUsed"
                          dense
                          outlined />
                <!--<v-text-field dense outlined label="Blood Bag to be used" v-model ="donorBloodBagIssuance.BloodBagToBeUsed"></v-text-field>-->
                
            </v-col>

            <!-- Bag Type Options -->
            <v-col cols="12" md="8">
                <v-radio-group row v-model ="donorBloodBagIssuance.BloodBagType">
                    <!--<v-radio label="Single" value="single" />
    <v-radio label="Double" value="double" />
    <v-radio label="Triple" value="triple" />
    <v-radio label="Quadruple" value="quadruple" />-->
                    <v-radio v-for="(type, index) in bloodBagCollectionSubOptions" 
                             :key="index" 
                             :label="type.text" 
                             :value="type.value"
                             
                             />
                </v-radio-group>
            </v-col>
        </v-row>

        <v-row align="center">
            <!-- Segment Serial Number -->
            <v-col cols="12" sm="6" md="4">
                <label class="font-weight-bold">Segment Serial Number</label>
                <v-text-field dense
                              outlined
                              label="Segment Serial Number"
                              v-model="donorBloodBagIssuance.SegmentSerialNumber" />
            </v-col>

            <!-- Unit Serial Number -->
            <v-col cols="12" sm="6" md="4">
                <label class="font-weight-bold">Unit Serial Number</label>
                <v-text-field dense
                              outlined
                              label="Unit Serial Number"
                              v-model="donorBloodBagIssuance.UnitSerialNumber" />
            </v-col>

            <!-- Print Button -->
            <v-col cols="12" md="4" class="text-left; mb-7">
                <v-btn color="red darken-2" class="white--text mt-6" rounded>
                    PRINT BARCODE
                </v-btn>

            </v-col>
            <div class="section-outer-container mt-3">
                <div class="text-right">
                    <v-btn color="default" large tile class="mr-2" @click="onApprove"><v-icon color="success" size="25" left>mdi-check</v-icon> Approve</v-btn>
                    <!--v-btn color="default" large tile class="mr-2" @click=""><v-icon color="warning" size="25" left>mdi-cancel</v-icon> Mark as Deferred</v-btn>-->
                </div>
            </div>
        </v-row>
    </v-form>
</template>

<script lang="ts">
    import VueBase from '@/components/VueBase';
    import { Component, Watch } from 'vue-property-decorator';
    import { getModule } from 'vuex-module-decorators';
    import DonorModule from '@/store/DonorModule';
    import LookupModule from '@/store/LookupModule';
    import DonorScreeningService from '@/services/DonorScreeningService';
    import Common from '@/common/Common';
    import { DonorBloodBagIssuanceDto, IDonorBloodBagIssuance } from '@/models/DonorScreening/DonorBloodBagIssuance';
    import { ILookupOptions } from '@/models/Lookups/LookupOptions';
    import { LookupKeys } from '@/models/Enums/LookupKeys';
    import { DonorStatus } from '@/models/Enums/DonorStatus';
    import { BloodBagCollections } from '@/models/Enums/BloodBagCollections';
    //import RecentDonationTable from '@/components/DonorScreening/ScreeningForms/RecentDonationTable.vue';
    @Component
    export default class IssuanceOfBloodBag extends VueBase {
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
        protected donorBloodBagIssuance: IDonorBloodBagIssuance = new DonorBloodBagIssuanceDto();
        protected isEditingValue: boolean = false;
        protected isDisabled: boolean = true;

        public async created(): Promise<void> {

            let loader = this.showLoader();

            try {

                await this.getDonorBloodBagIssuance();
            }
            catch (error) {
                console.log(error);
            }
            finally {
                loader.hide();
            }
        }
        public get options(): (key: string) => Array<ILookupOptions> {
            return (key) => this.lookupModule.getOptionsByKey(key);
        }
        protected get bloodBagCollectionOptions(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.BloodBagCollectionTypes).map(x => { return { text: x.Text, value: x.Value } });
        }

        protected async getDonorBloodBagIssuance(): Promise<void> {
            if (this.$route.params.reg_id && typeof (this.$route.params.reg_id) === 'string') {
                let regId = this.$route.params.reg_id;
                this.donorBloodBagIssuance = await this.donorScreeningService.getBloodBagIssuance(regId);
                console.log(this.donorBloodBagIssuance);

                this.donorModule.setTransactionId(this.donorBloodBagIssuance.DonorTransactionId);
            }
        }

        public get bloodBagCollectionSubOptions(): Array<{ text: string, value: string }> {
            if (this.donorBloodBagIssuance.BloodBagToBeUsed === BloodBagCollections.KARMI || this.donorBloodBagIssuance.BloodBagToBeUsed === BloodBagCollections.TERUMO) {
                return this.bloodBagsSizeOptions;
            }
            else if (this.donorBloodBagIssuance.BloodBagToBeUsed === BloodBagCollections.SpecialBag) {
                return this.specialBagOptions;
            }
            else if (this.donorBloodBagIssuance.BloodBagToBeUsed === BloodBagCollections.Apheresis) {
                return this.apheresisBagOptions;
            }
            else {
                return [];
            }
        }

        protected get bloodBagsSizeOptions(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.BloodBagSizeTypes).map(x => { return { text: x.Text, value: x.Value } });
        }

        protected get specialBagOptions(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.SpecialBagTypes).map(x => { return { text: x.Text, value: x.Value } });
        }

        protected get apheresisBagOptions(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.ApheresisBagTypes).map(x => { return { text: x.Text, value: x.Value } });
        }


        //public onChangeLog(records: Array<IDonorRecentDonationDto>): void {
        //    this.recentDonations = records.map(x => { return { DonorRecentDonationId: null, Agency: x.Agency, NumberOfDonation: x.NumberOfDonation, DateOfRecentDonation: x.DateOfRecentDonation, PlaceOfRecentDonation: x.PlaceOfRecentDonation }; });
        //}

        //protected onDeferred(): void {
        //    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
        //    if (this.formValid === false) {
        //        return;
        //    }

        //    this.mark_deferred(`Are you sure you want to tag this donor as deffered?`, 'Mark Donor as Deferred', 'Mark as Deferred', 'Cancel', this.onDeferralConfirmation);
        //}

        //public async onDeferralConfirmation(confirm: boolean, result: any): Promise<void> {
        //    if (confirm) {
        //        this.donorInitialScreening.DonorStatus = DonorStatus.Deferred;
        //        this.donorInitialScreening.DeferralStatus = result[0].DeferralStatus;
        //        this.donorInitialScreening.Remarks = result[0].Remarks;
        //        await this.onSubmit();
        //    }
        //}

        protected onApprove = (): void => {

            const form = this.$refs.form as any;

            if (!form) {
                this.notify_error('Form is not ready yet.');
                return;
            }

            this.formValid = form.validate();
            if (this.formValid === false) {
                return;
            }

            this.confirm(`Are you sure you want to proceed with approving this donor?`, 'Approve Donor', 'Approve', 'Cancel', this.onApprovalConfirmation);
        };

        //protected onApprove(): void {
        //    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
        //    if (this.formValid === false) {
        //        return;
        //    }

        //    this.confirm(`Are you sure you want to proceed with approving this donor?`, 'Approve Donor', 'Approve', 'Cancel', this.onApprovalConfirmation);
        //}

        public async onApprovalConfirmation(confirm: boolean): Promise<void> {
            if (confirm) {
                this.donorBloodBagIssuance.DonorStatus = DonorStatus.ForMethodBloodCollection;
                await this.onSubmit();
            }
        }

        public async onSubmit(): Promise<void> {
            console.log(this.donorBloodBagIssuance);
            let loader = this.showLoader();
            try {
                //this.donorVitalSigns.RecentDonations = this.recentDonations;
                await this.donorScreeningService.upsertBloodBagIssuance(this.donorBloodBagIssuance);
                this.notify_success('Form successfully submitted.');

                this.$router.push({ path: '/donors' });
            }
            catch (error: any) {
                if (error.StatusCode != 500) {
                    let errorMessage = error.Message ?? "An error occured while processing your request.";
                    this.notify_error(errorMessage);
                    console.log(error);
                }
            }
            finally {
                loader.hide();
            }
        }
    }
</script>