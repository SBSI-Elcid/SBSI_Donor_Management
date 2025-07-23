<template>
    <div>
        <v-stepper v-model="currentStep">
            <v-stepper-header>
                <v-stepper-step :complete="currentStep > 1" step="1" :editable="true" :edit-icon="icon">
                    Donor Personal Information
                    <small>(to be filled by the donor)</small>
                </v-stepper-step>
                <v-divider />
                <!--<v-stepper-step :complete="currentStep > 2" step="2" :editable="true">
                  Medical History
                </v-stepper-step>
                <v-divider />
                <v-stepper-step :complete="currentStep > 3" step="3" :editable="true">
                  Donor's Informed Consent
                </v-stepper-step>-->
            </v-stepper-header>

            <v-stepper-items>
                <v-stepper-content step="1">
                    <PersonalData :inReviewPage="inReviewPage" @goToStep="goToStep" @submit="onSubmit" :inProcess="true" />
                </v-stepper-content>

                <!--<v-stepper-content step="2">
                  <MedicalHistoryForm :inReviewPage="inReviewPage" @goToStep="goToStep" />
                </v-stepper-content>

                <v-stepper-content step="3">
                  <ConsentData :inReviewPage="inReviewPage" @goToStep="goToStep" @submit="onSubmit" :inProcess="true" />
                </v-stepper-content>-->
            </v-stepper-items>
        </v-stepper>
    </div>
</template>

<script lang="ts">
    import VueBase from '../../VueBase';
    import { Component } from 'vue-property-decorator';
    import { getModule } from 'vuex-module-decorators';
    import PersonalData from '@/components/DonorRegistration/RegistrationForms/PersonalDataForm.vue';
    import MedicalHistoryForm from '@/components/DonorRegistration/RegistrationForms/MedicalHistoryForm.vue'
    import ConsentData from '@/components/DonorRegistration/RegistrationForms/InformedConsentForm.vue';
    import DonorModule from '@/store/DonorModule';
    import DonorRegistrationService from '@/services/DonorRegistrationService';
    import moment from 'moment';
    import { IRegisteredDonorInfoDto } from '@/models/DonorRegistration/IRegisteredDonorInfoDto';
    import { IDonorDto } from '@/models/DonorRegistration/DonorDto';
    import { Roles } from '@/models/Enums/Roles';
    import Common from '@/common/Common';
    import { DonorStatus } from '@/models/Enums/DonorStatus';
    import moment from 'moment';


    @Component({
        components: { PersonalData, MedicalHistoryForm, ConsentData }
    })
    export default class DonorInformation extends VueBase {
        protected donorModule: DonorModule = getModule(DonorModule, this.$store);
        protected donorRegistrationService: DonorRegistrationService = new DonorRegistrationService();

        protected currentStep: number = 1;
        protected donorRegistrationId: Guid = '';


        protected get inReviewPage(): boolean {
            return this.donorModule.hasDonorTransaction;
        }

        protected get icon(): string {
            return this.inReviewPage ? "mdi-account" : "mdi-pencil";
        }

        protected async mounted(): Promise<void> {
            if (this.$route.params.reg_id && typeof (this.$route.params.reg_id) === 'string') {
                this.donorRegistrationId = this.$route.params.reg_id;

                let loader = this.showLoader();
                try {
                    let donorInfo: IRegisteredDonorInfoDto = await this.donorRegistrationService.getRegisteredDonorInfo(this.donorRegistrationId);
                    this.donorModule.setTransactionId(donorInfo.DonorTransactionId);
                    console.log("DonorInfo", donorInfo);
                    if (Common.hasValue(donorInfo.DonorStatus) && donorInfo.DonorStatus === DonorStatus.Deferred) {
                        this.donorModule.setDonorStatus(donorInfo.DonorStatus);
                    }

                    donorInfo.BirthDate = moment(donorInfo.BirthDate).toDate();
                    this.donorModule.setDonorInformation(donorInfo);
                }

                catch (error) {
                    console.log(error);
                }
                finally {

                    loader.hide();
                }
            }

        }

        protected goToStep(step: number): void {
            this.currentStep = step;

            window.scrollTo(0, 0);
        }

        public async created(): Promise<void> {
            await this.donorModule.loadMedicalQuestionnaire();
        }

        public onSubmit(): void {
            this.confirm('Please make sure all entered data is correct, click Cancel to edit or Submit to continue.', '', 'Submit', 'Cancel', this.onSubmitConfirmation)
        }

        public async onSubmitConfirmation(confirm: boolean): Promise<void> {
            if (confirm) {

                let loader = this.showLoader();
                try {
                    let dto: IDonorDto = this.donorModule.getDonorInformation;
                    let transactionId = await this.donorRegistrationService.updateCreateDonorTransaction(dto, this.donorRegistrationId);

                    this.notify_success('Donor is now ready For Initial Screening.');

                    if (this.hasAccess([Roles.InitialScreener, Roles.DonorAdmin])) {
                        this.$router.push({ path: `/donor/vitalsigns/${this.donorRegistrationId}` });
                    }
                    else {
                        this.$router.push({ path: `/donors` });
                    }
                }
                catch (error: any) {
                    if (error.StatusCode != 500) {
                        this.notify_error(error.Message);
                    }
                }
                finally {
                    loader.hide();
                }
            }
            else {
                this.goToStep(1);
            }
        }
    }
</script>