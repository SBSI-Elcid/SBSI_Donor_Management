<template>
    <div>
        <v-form class="form-container" ref="form" v-model="formValid" lazy-validation>
            <v-row>
                <v-col cols="2" class="py-0">
                    <label class="label-container">Body Weight (kgs)</label>
                    <v-text-field type="number"
                                  :disabled="isDisabled"
                                  v-model ="donorVitalSigns.BodyWeight"
                                  :rules="[rules.numberRequired, rules.positiveNumber]"
                                  dense outlined />
                </v-col>

                <v-col cols="2" class="py-0">
                    <label class="label-container">Temperature (&deg;C)</label>
                    <v-text-field type="number"
                                  :disabled="isDisabled"
                                  v-model ="donorVitalSigns.Temperature"
                                  :rules="[rules.numberRequired, rules.temperatureRange]"
                                  dense outlined />
                </v-col>

            </v-row>

            <v-row>
                <v-col cols="2" class="py-0">
                    <label class="label-container">Blood Pressure</label>
                    <v-text-field
                                  :disabled="isDisabled"
                                  v-model ="donorVitalSigns.BloodPressure"
                                  :rules="[rules.required, rules.bloodPressurePattern]"
                                  dense outlined />
                </v-col>

                <v-col cols="2" class="py-0">
                    <label class="label-container">Pulse Rate</label>
                    <v-text-field type="number"
                                  :disabled="isDisabled"
                                  v-model ="donorVitalSigns.PulseRate"
                                  :rules="[rules.numberRequired, rules.positiveNumber]"
                                  dense outlined />
                </v-col>

            </v-row>

            <v-row>
                <v-col cols="2" class="py-0">
                    <label class="label-container">Respiratory Rate</label>
                    <v-text-field type="number"
                                  :disabled="isDisabled"
                                  v-model ="donorVitalSigns.RespiratoryRate"
                                  :rules="[rules.numberRequired, rules.positiveNumber]"
                                  dense outlined />
                </v-col>

                <v-col cols="2" class="py-0">
                    <label class="label-container">Oxygen Saturation</label>
                    <v-text-field type="number"
                                  :disabled="isDisabled"
                                  v-model ="donorVitalSigns.OxygenSaturation"
                                  :rules="[rules.numberRequired, rules.oxygenRange]"
                                  dense outlined />
                </v-col>

            </v-row>

            <div class="section-outer-container mt-3" v-if ="isEditable">
                <div class="text-right">
                    <v-btn color="default" large tile class="mr-2" @click="onApprove"><v-icon color="success" size="25" left>mdi-check</v-icon> Approve</v-btn>
                    <v-btn color="default" large tile class="mr-2" @click="onDeferred"><v-icon color="warning" size="25" left>mdi-cancel</v-icon> Mark as Deferred</v-btn>
                </div>
            </div>
        </v-form>
    </div>
</template>
<script lang="ts">
    import VueBase from '@/components/VueBase';
    import { Component, Watch } from 'vue-property-decorator';
    import { getModule } from 'vuex-module-decorators';
    import DonorModule from '@/store/DonorModule';
    import LookupModule from '@/store/LookupModule';
    import DonorScreeningService from '@/services/DonorScreeningService';
    import Common from '@/common/Common';
    import { DonorVitalSignsDto, IDonorVitalSigns } from '@/models/DonorScreening/DonorVitalSignsDto';
    import { ILookupOptions } from '@/models/Lookups/LookupOptions';
    import { LookupKeys } from '@/models/Enums/LookupKeys';
    import { DonorStatus } from '@/models/Enums/DonorStatus';
    import DonorRegistrationService from '@/services/DonorRegistrationService';
    import { IRegisteredDonorInfoDto,RegisteredDonorInfoDto } from '@/models/DonorRegistration/IRegisteredDonorInfoDto';
    //import RecentDonationTable from '@/components/DonorScreening/ScreeningForms/RecentDonationTable.vue';
    @Component
    export default class VitalSigns extends VueBase {
        protected donorModule: DonorModule = getModule(DonorModule, this.$store);
        protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
        protected donorScreeningService: DonorScreeningService = new DonorScreeningService();
        protected donorRegistrationService: DonorRegistrationService = new DonorRegistrationService();

        protected formValid: boolean = true;
        protected rules: any = {
            ...Common.ValidationRules,

            numberRequired: (v: any) => (!!v || v === 0) || 'This field is required.',

            positiveNumber: (v: number) => v >= 0 || 'Must be a positive number',

            temperatureRange: (v: number) =>
                (v >= 30 && v <= 50) || 'Temperature should be between 30\u00B0C and 50\u00B0C ',

            oxygenRange: (v: number) =>
                (v >= 80 && v <= 100) || 'Oxygen saturation should be 80% - 100%',

            bloodPressurePattern: (v: string) => {
                const pattern = /^\d{2,3}\/\d{2,3}$/;
                return pattern.test(v) || 'Invalid format. Use format like 120/80';
            }
        }
        protected errorMessage: string = '';
        protected showInHouseOptions: boolean = false;
        protected showRecentDonations: boolean = false;
        protected showPatientDirectedFields: boolean = false;
        protected showMobileBloodDonationFields: boolean = false;
        protected donorVitalSigns: IDonorVitalSigns = new DonorVitalSignsDto();
        protected isEditingValue: boolean = false;
        protected isDisabled: boolean = false;

        protected donorInfo: IRegisteredDonorInfoDto = new RegisteredDonorInfoDto();
       
        protected get isEditable(): boolean {
            return this.donorVitalSigns.DonorStatus === DonorStatus.ForVitalSigns;
        }

        public async created(): Promise<void> {

            let loader = this.showLoader();

            try {
                
                await this.getDonorVitalSignsInfo();

                this.donorModule.fetchDonorActivityType(this.$route.params.reg_id);
                //await this.getLatestDonorStatusWithSameDonor();
            }
            catch (error) {
                console.log(error);
            }
            finally {
                loader.hide();
            }
        }

        //protected async getLatestDonorStatusWithSameDonor(): Promise<void> {
        //    if (this.$route.params.reg_id && typeof (this.$route.params.reg_id) === 'string') {
        //        let regId = this.$route.params.reg_id;
        //        let donorInfo: IRegisteredDonorInfoDto = await this.donorRegistrationService.getRegisteredDonorInfo(regId);
        //        console.log("donorInfo",donorInfo);
        //        this.donorModule.setDonorStatus(donorInfo.DonorStatus);
               
        //    }
        //}

        protected async getDonorVitalSignsInfo(): Promise<void> {
            if (this.$route.params.reg_id && typeof (this.$route.params.reg_id) === 'string') {
                let regId = this.$route.params.reg_id;
                this.donorVitalSigns = await this.donorScreeningService.getDonorVitalSignsInfo(regId);
                console.log(this.donorVitalSigns);
                
                this.donorModule.setTransactionId(this.donorVitalSigns.DonorTransactionId);

                if (Common.hasValue(this.donorVitalSigns.DonorStatus) && this.donorVitalSigns.DonorStatus !== DonorStatus.Deferred && this.donorVitalSigns.DonorStatus !== DonorStatus.ForVitalSigns) {
                    this.isDisabled = true;
                }
            }
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

        protected onDeferred(): void {
            this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
            if (this.formValid === false) {
                return;
            }

            this.mark_deferred(`Are you sure you want to tag this donor as deffered?`, 'Mark Donor as Deferred', 'Mark as Deferred', 'Cancel', this.onDeferralConfirmation);
        }

        public async onDeferralConfirmation(confirm: boolean, result: any): Promise<void> {
            if (confirm) {
                this.donorVitalSigns.DonorStatus = DonorStatus.Deferred;
                this.donorVitalSigns.DeferralStatus = result[0].DeferralStatus;
                this.donorVitalSigns.Remarks = result[0].Remarks;
                await this.onSubmit();
            }
        }
        public async onApprovalConfirmation(confirm: boolean): Promise<void> {
            if (confirm) {
                this.donorVitalSigns.DonorStatus = DonorStatus.ForPhysicalExamination;
                await this.onSubmit();
            }
        }
        
        public async onSubmit(): Promise<void> {
            console.log(this.donorVitalSigns);
            let loader = this.showLoader();
            try {
                //this.donorVitalSigns.RecentDonations = this.recentDonations;
                await this.donorScreeningService.upsertVitalSignsScreening(this.donorVitalSigns);
                this.notify_success('Form successfully submitted.');

                this.$router.push({ path: '/donors' });
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
