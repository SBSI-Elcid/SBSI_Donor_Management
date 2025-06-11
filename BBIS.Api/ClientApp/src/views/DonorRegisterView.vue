<template>
    <div>
        <CommonAppBar />

        <v-main>
            <section id="features" class="grey lighten-5">
                <v-container class="mt-1">

                    <div class="text-center">
                        <h2 class="display-1 mb-3 grey--text">
                            <span><v-icon large left color="error">mdi-blood-bag</v-icon></span>
                            Donor Registration
                        </h2>
                    </div>


                    <v-responsive class="mx-auto mb-12" width="56">
                        <v-divider class="mb-1"></v-divider>
                        <v-divider></v-divider>
                    </v-responsive>

                    <div v-if="!success">
                        <p v-if="!showRegistrationForm" class="subtitle-1 text-center pt-2">Please enter your information to verify.</p>
                        <v-card class="mx-auto" width="450px" v-if="!showRegistrationForm" elevation="4">
                            <div class="pt-3"></div>
                            <DonorVerification @goToStep="onVerifySuccess" />
                        </v-card>

                        <v-card class="mx-auto" max-width="100%" v-if="showRegistrationForm">
                            <DonorRegistration @onSuccess=onRegistrationSuccess :scheduleId ="scheduleId" />
                        </v-card>
                    </div>

                    <div class="text-center mx-auto" v-if="success">

                        <v-card class="pa-3 mx-auto" color="default" flat max-width="790px" elevation="4">
                            <v-card-text>
                                <p class="subtitle-1 pb-3"><strong>Thank you for using the Self Service Portal!</strong></p>
                                <p class="font-weight-large display-1 mt-2">Your Reference No.is <span><strong>{{ registrationNumber }}</strong></span></p>
                                <p class="font-weight-large subtitle-1 display-2 mt-3">Click "Print" to print your reference number.</p>
                                <p class="font-weight-large subtitle-1 display-2">Please proceed to Assessment Window 1.</p>
                            </v-card-text>

                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn color="default" class="mr-2" @click="print"><v-icon size="25" color="primary" left>mdi-printer</v-icon>Print</v-btn>
                                <v-btn color="default" class="mr-2" @click="resetForm"><v-icon size="25" color="success" left>mdi-check</v-icon>Done</v-btn>
                                <v-spacer></v-spacer>
                            </v-card-actions>
                        </v-card>
                    </div>

                </v-container>

                <div id="printRef" style="display: none;">
                    <div class="text-center mx-auto">
                        <p class="subtitle-1 pb-3 pt-4"><strong>Thank you for using the Self Service Portal!</strong></p>
                        <p class="font-weight-large mt-2">Your Reference No.is <span><h2>{{ registrationNumber }}</h2></span></p>
                        <p class="font-weight-large subtitle-1">Please proceed to Assessment Window 1.</p>
                    </div>
                </div>

                <div class="py-12"></div>
            </section>
        </v-main>

        <common-footer />
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import CommonAppBar from '@/components/Common/CommonAppBar.vue';
    import CommonFooter from '@/components/Common/CommonFooter.vue';
    import DonorRegistration from '@/components/DonorRegistration/DonorRegistration.vue';
    import DonorVerification from '@/components/DonorRegistration/DonorVerification.vue';
    import DonorModule from '@/store/DonorModule';
    import { getModule } from 'vuex-module-decorators';

    @Component({
        components: { CommonAppBar, CommonFooter, DonorRegistration, DonorVerification }
    })
    export default class DonorRegisterView extends Vue {
        protected donorModule: DonorModule = getModule(DonorModule, this.$store);

        protected showRegistrationForm: boolean = false;
        protected success: boolean = false;
        protected scheduleId: string = '';
        protected registrationNumber: string = '';

        protected onVerifySuccess(step: number): void {
            if (step == 2) {
                this.showRegistrationForm = true;
            }
        }

        protected onRegistrationSuccess(registrationNumber: string): void {
            this.success = true;
            this.registrationNumber = registrationNumber;
        }

        protected print(): void {
            this.$htmlToPaper('printRef');
        }

        protected mounted(): void {
            const show = this.$route.query.showRegistrationForm;
            const scheduleId = this.$route.query.scheduleId;
            this.showRegistrationForm = show === 'true';
            this.scheduleId = scheduleId;
            //await this.loadrecords();
        }

        protected resetForm(): void {
            this.success = false;
            this.showRegistrationForm = false;
            this.donorModule.resetDonor();
        }
    }
</script>
