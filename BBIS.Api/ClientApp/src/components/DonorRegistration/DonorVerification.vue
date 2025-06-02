<template>
    <div class="pa-4">
        <v-form class="form-container" ref="form" v-model="formValid" lazy-validation>

            <v-row no-gutters>
                <v-col cols="12">
                    <v-text-field v-model="newDonor.Lastname" label="Last Name"
                                  :rules="[rules.required, rules.maxLength(90)]"
                                  dense outlined />
                </v-col>
            </v-row>

            <v-row no-gutters>
                <v-col cols="12" class="py-0">
                    <v-text-field v-model="newDonor.Firstname" label="First Name"
                                  :rules="[rules.required, rules.maxLength(90)]"
                                  dense outlined />
                </v-col>
            </v-row>

            <v-row no-gutters>
                <v-col cols="12" class="py-0">
                    <v-text-field v-model="newDonor.Middlename" label="Middle Name"
                                  :rules="[rules.required, rules.maxLength(90)]"
                                  dense outlined />
                </v-col>
            </v-row>

            <v-row no-gutters>
                <v-col cols="12" class="py-0">
                    <BirthdatePicker v-model="newDonor.BirthDate" :rules="[rules.required]" :labelText="'Birthdate'" />
                </v-col>
            </v-row>

            <v-row no-gutters v-if ="isBloodDonator">
                <v-col align-self="center" cols="12" class="py-0 text-center">
                    <v-btn large
                           :disabled="apiRequestActive"
                           v-if="showVerifyButton"
                           color="primary"
                           class="mr-2"
                           @click="onVerify">
                        <v-icon left>mdi-account-search</v-icon> Verify
                    </v-btn>
                </v-col>
            </v-row>

            <!-- STATUSES   -->
            <v-row v-if="showDonorStatus && !verifyDonorResult.IsValid">
                <v-col cols="12" class="py-0">
                    <StatusCard class="status-container"
                                :donorStatus="verifyDonorResult" />
                </v-col>
            </v-row>

            <!-- BUTTON -->
            <div v-if="showRefreshButton" class="section-outer-container mt-8">
                <v-btn color="default" class="mr-2" depressed v-if="showNextButton" @click="onNext">Next <v-icon size="25" color="primary" right>mdi-chevron-right</v-icon></v-btn> 
                <v-btn color="default" class="mr-2" depressed v-if="showRefreshButton" @click="onReset"><v-icon size="25" color="primary" left>mdi-refresh</v-icon> Reset Form</v-btn>
            </div>

        </v-form>
        <v-progress-linear v-if="apiRequestActive" indeterminate color="red" class="mt-3"></v-progress-linear>
    </div>
</template>

<script lang="ts">
    import VueBase from '../VueBase';
    import { Component, Emit, Vue, Watch } from 'vue-property-decorator';
    import { getModule } from 'vuex-module-decorators';
    import StatusCard from '@/components/Common/StatusCard.vue';
    import DonorModule from '@/store/DonorModule';
    import DonorRegistrationService from '@/services/DonorRegistrationService';
    import { DonorDto, IDonorDto } from '@/models/DonorRegistration/DonorDto';
    import { VerifyDonorResultDto, IVerifyDonorResultDto } from '@/models/DonorRegistration/VerifyDonorResultDto';
    import { DonorMedicalHistoryDto } from '@/models/DonorRegistration/DonorMedicalHistoryDto';
    import Common from '@/common/Common';
    import BirthdatePicker from '@/components/Common/FormInputs/BirthdatePicker.vue';
    import moment from 'moment';
    @Component({
        components: { StatusCard, BirthdatePicker }
    })
    export default class VerificationForm extends VueBase {
        protected donorModule: DonorModule = getModule(DonorModule, this.$store);
        protected donorRegistrationService: DonorRegistrationService = new DonorRegistrationService();

        protected formValid: boolean = true;
        protected rules: any = { ...Common.ValidationRules }
        protected showVerifyButton: boolean = true;
        protected showDonorStatus: boolean = false;
        protected showNextButton: boolean = false;
        protected showRefreshButton: boolean = false;
        protected apiRequestActive: boolean = false;
        protected errorMessage: string = '';
        protected isBloodDonator: boolean = true;

        protected verifyDonorResult: IVerifyDonorResultDto = new VerifyDonorResultDto();
        protected newDonor: IDonorDto = new DonorDto();

        protected async onVerify(): Promise<void> {
            this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
            if (!this.formValid) return;

            try {
                this.apiRequestActive = true;
                this.verifyDonorResult = await this.donorRegistrationService.verifyDonor({ FirstName: this.newDonor.Firstname, MiddleName: this.newDonor.Middlename, LastName: this.newDonor.Lastname, BirthDate: this.newDonor.BirthDate })
                if (!Common.isNull(this.verifyDonorResult.Donor)) {
                    this.newDonor = this.verifyDonorResult.Donor;
                    this.newDonor.MedicalHistories = new Array<DonorMedicalHistoryDto>();
                }

                console.log("DonorStatus", this.verifyDonorResult.DeferralStatus);
                console.log("DonorRegistrationId", this.verifyDonorResult.donorRegistrationId);
                if (this.verifyDonorResult.IsValid) {
                    this.onNext();
                }

                // Hide the verify button once a status was retrieved and show the donor status cards.
                this.showVerifyButton = false;
                this.showDonorStatus = true;

                this.showNextButton = this.verifyDonorResult.IsValid;
                this.showRefreshButton = !this.showNextButton;
            }
            catch (error: any) {
                if (error.StatusCode != 500) {
                    this.errorMessage = error.Message;
                    this.notify_error(this.errorMessage);
                }

                this.showRefreshButton = true;
            }
            finally {
                this.apiRequestActive = false;
            }
        }

        @Watch('newDonor.BirthDate')
        protected onChangeModelBirthDate(): void {
            this.calculateAge();
        }

        protected calculateAge(): void {
            let years = moment().diff(moment(this.newDonor.BirthDate, 'YYYY-MM-DD'), 'years');
            console.log(years);
            if (years === 65 || years <= 15) {
                this.isBloodDonator = false;
                this.notify_warning("Age is 15 and below or 65 and above, are restricted from blood donation");
            } else {
                this.isBloodDonator = true;
            }
        }

        public onReset(): void {
            (this.$refs.form as any).reset();
            this.showVerifyButton = true;
            this.showDonorStatus = false;
            this.showRefreshButton = false;
        }

        @Emit('goToStep')
        public onNext(): number {
            this.donorModule.setDonorInformation(this.newDonor);
            return 2;
        }
    }
</script>