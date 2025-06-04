<template>
    <div>
        <CommonAppBar />

        <v-main>
            <section class="grey lighten-5">
                <v-container class="mt-1">

                    <div class="text-center">
                        <h2 class="display-1 mb-3 grey--text">
                            <span><v-icon large left color="error">mdi-blood-bag</v-icon></span>
                            Activity Donor Registration
                        </h2>
                    </div>


                    <v-responsive class="mx-auto mb-12" width="56">
                        <v-divider class="mb-1"></v-divider>
                        <v-divider></v-divider>
                    </v-responsive>


                    <p class="subtitle-1 text-center pt-2">Please enter your information to verify.</p>

                    <v-card class="mx-auto" width="450px">
                        <v-form class="form-container; pa-4" ref="form" v-model="formValid" lazy-validation>

                            <v-row no-gutters class="">
                                <v-col cols="12">
                                    <v-text-field label="Last Name"
                                                  :rules="[rules.required, rules.maxLength(90)]"
                                                  dense outlined />
                                </v-col>
                            </v-row>

                            <v-row no-gutters>
                                <v-col cols="12" class="py-0">
                                    <v-text-field label="First Name"
                                                  :rules="[rules.required, rules.maxLength(90)]"
                                                  dense outlined />
                                </v-col>
                            </v-row>

                            <v-row no-gutters>
                                <v-col cols="12" class="py-0">
                                    <v-text-field label="Middle Name"
                                                  :rules="[rules.required, rules.maxLength(90)]"
                                                  dense outlined />
                                </v-col>
                            </v-row>

                            <v-row no-gutters>
                                <v-col cols="12" class="py-0">
                                    <BirthdatePicker :labelText="'Birthdate'" />
                                </v-col>
                            </v-row>

                            <v-row no-gutters>
                                <v-col align-self="center" cols="12" class="py-0 text-center">
                                    <v-btn large
                                           color="primary"
                                           class="mr-2">
                                        <v-icon left>mdi-account-search</v-icon> Verify
                                    </v-btn>
                                </v-col>
                            </v-row>

                        </v-form>
                    </v-card>
                </v-container>
            </section>
        </v-main>
        <common-footer />
        <!--<v-progress-linear indeterminate color="red" class="mt-3"></v-progress-linear>-->
    </div>
</template>
<script lang="ts">
    import VueBase from '@/components/VueBase';
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
    import { DonorStatus } from '@/models/Enums/DonorStatus';

    import CommonAppBar from '@/components/Common/CommonAppBar.vue';
    import CommonFooter from '@/components/Common/CommonFooter.vue';

    @Component({
        components: { CommonAppBar, CommonFooter, BirthdatePicker }
    })
    export default class ActivityDonorRegister extends VueBase {
        protected donorModule: DonorModule = getModule(DonorModule, this.$store);
        protected donorRegistrationService: DonorRegistrationService = new DonorRegistrationService();

        protected formValid: boolean = true;
        protected rules: any = { ...Common.ValidationRules }

        protected apiRequestActive: boolean = false;
        protected errorMessage: string = '';


        //        protected verifyDonorResult: IVerifyDonorResultDto = new VerifyDonorResultDto();
        //        protected newDonor: IDonorDto = new DonorDto();

        //        protected async onVerify(): Promise<void> {
        //            this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
        //            if (!this.formValid) return;

        //            try {
        //                this.apiRequestActive = true;
        //                this.verifyDonorResult = await this.donorRegistrationService.verifyDonor({ FirstName: this.newDonor.Firstname, MiddleName: this.newDonor.Middlename, LastName: this.newDonor.Lastname, BirthDate: this.newDonor.BirthDate })
        //                if (!Common.isNull(this.verifyDonorResult.Donor)) {
        //                    this.newDonor = this.verifyDonorResult.Donor;
        //                    this.newDonor.MedicalHistories = new Array<DonorMedicalHistoryDto>();
        //                }


        ///*                const allStatuses = Object.values(DonorStatus);*/

        //                //if (!allStatuses.includes(this.verifyDonorResult.DeferralStatus)) {
        //                //    this.$router.push({ path: `/donor/info/${this.verifyDonorResult.donorRegistrationId}`});
        //                //}
        //                //console.log(this.verifyDonorResult);
        //                ////this.$router.push({ path: `/donor/info/${regId}` });
        //                //console.log("DonorStatus", this.verifyDonorResult.DeferralStatus);
        //                //console.log("DonorRegistrationId", this.verifyDonorResult.donorRegistrationId);
        //                if (this.verifyDonorResult.IsValid) {
        //                    this.onNext();
        //                }

        //                // Hide the verify button once a status was retrieved and show the donor status cards.
        //                this.showVerifyButton = false;
        //                this.showDonorStatus = true;

        //                this.showNextButton = this.verifyDonorResult.IsValid;
        //                this.showRefreshButton = !this.showNextButton;
        //            }
        //            catch (error: any) {
        //                if (error.StatusCode != 500) {
        //                    this.errorMessage = error.Message;
        //                    this.notify_error(this.errorMessage);
        //                }

        //                this.showRefreshButton = true;
        //            }
        //            finally {
        //                this.apiRequestActive = false;
        //            }
        //        }

        //        @Watch('newDonor.BirthDate')
        //        protected onChangeModelBirthDate(): void {
        //            this.calculateAge();
        //        }

        //        protected calculateAge(): void {
        //            let years = moment().diff(moment(this.newDonor.BirthDate, 'YYYY-MM-DD'), 'years');
        //            console.log(years);
        //            if (years === 65 || years <= 15) {
        //                this.isBloodDonator = false;
        //                this.notify_warning("Age is 15 and below or 65 and above, are restricted from blood donation");
        //            } else {
        //                this.isBloodDonator = true;
        //            }
        //        }

        //        public onReset(): void {
        //            (this.$refs.form as any).reset();
        //            this.showVerifyButton = true;
        //            this.showDonorStatus = false;
        //            this.showRefreshButton = false;
        //        }

        //        @Emit('goToStep')
        //        public onNext(): number {
        //            this.donorModule.setDonorInformation(this.newDonor);
        //            return 2;
        //        }
    }
</script>

