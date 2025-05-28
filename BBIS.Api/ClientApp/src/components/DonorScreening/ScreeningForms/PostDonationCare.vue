<template>
    <v-container fluid>
        <v-form class="form-container" ref="form" v-model="formValid" lazy-validation>
            <v-row>
                <v-col cols="6">
                    <v-combobox label="Type of Reaction"
                                :items="['Local', 'Vasovagal', 'Apheresis related', 'Allergic']"
                                solo
                                v-model="donorPostDonationCare.TypeOfReaction"
                                append-icon="mdi-menu-down" />
                </v-col>
                <v-col cols="6">
                    <v-combobox label="Severity of Reaction"
                                :items="['Grade 1', 'Grade 2', 'Grade 3', 'Grade 4', 'Grade 5']"
                                solo
                                v-model="donorPostDonationCare.SeverityOfReaction"
                                append-icon="mdi-menu-down" />
                </v-col>
            </v-row>

            <v-row>
                <v-col cols="6">
                    <v-combobox label="Reaction Manifestation"
                                :items="['Dizziness','Fainting','Convulsion']"
                                solo
                                v-model="donorPostDonationCare.ReactionManifestation"
                                append-icon="mdi-menu-down" />
                </v-col>
                <v-col cols="6">
                    <v-combobox label="Action / Interventions"
                                :items="['Positioned Supine','Leg Elevated','Muscle Contraction','Oxygen Inhalation','Iv Fluids']"
                                solo
                                v-model="donorPostDonationCare.ActionInterventions"
                                append-icon="mdi-menu-down" />
                </v-col>
            </v-row>

            <v-row>
                <v-col>
                    <v-card outlined>
                        <v-card-title>Vital Signs Monitoring</v-card-title>
                        <v-simple-table>
                            <thead>
                                <tr>
                                    <th>Time</th>
                                    <th>BP</th>
                                    <th>PR</th>
                                    <th>Others</th>
                                </tr>
                            </thead>
                            <!--<tbody>
                        <tr v-for="i in 4" :key="i">
                            <td><v-text-field type ="time" dense hide-details /></td>
                            <td><v-text-field dense hide-details /></td>
                            <td><v-text-field dense hide-details /></td>
                            <td><v-text-field dense hide-details /></td>
                        </tr>
                    </tbody>-->
                            <tbody>
                                <tr v-for="(item, index) in vitalSignsMonitoringDetails" :key="index">
                                    <td><v-text-field v-model="item.Time" type="time" dense hide-details /></td>
                                    <td><v-text-field v-model="item.BP" dense hide-details /></td>
                                    <td><v-text-field v-model="item.PR" dense hide-details /></td>
                                    <td><v-text-field v-model="item.Others" dense hide-details /></td>
                                </tr>
                                <!--<tr v-for="(detail, i) in vitalSignsMonitoringDetails" :key="i">
                            <td><v-text-field v-model="vitalSignsMonitoringDetails.Time" type="time" dense hide-details /></td>
                            <td><v-text-field v-model="vitalSignsMonitoringDetails.BP" dense hide-details /></td>
                            <td><v-text-field v-model="vitalSignsMonitoringDetails.PR" dense hide-details /></td>
                            <td><v-text-field v-model="vitalSignsMonitoringDetails.Others" dense hide-details /></td>
                        </tr>-->
                            </tbody>
                        </v-simple-table>
                    </v-card>
                </v-col>

                <v-col>
                    <v-textarea v-model = "donorPostDonationCare.DoctorsNote" label="Doctor's Notes" rows="4" outlined />
                    <v-text-field v-model ="donorPostDonationCare.DischargeStatus" label="Discharge Status" />
                    <v-row>
                        <!--<v-col>
                            <v-text-field v-model = "donorPostDonationCare.DoctorsNote" label="Monitored By" />
                        </v-col>-->
                        <v-col>
                            <v-text-field v-model = "donorPostDonationCare.DoctorName" label="Doctor's Name" />
                        </v-col>
                    </v-row>
                </v-col>
            </v-row>

            <v-row v-if="postDonationDetail.length >= 3">
                <v-col>
                    <v-textarea v-model="postDonationDetail[0].Details" label="Day 1 Post Donation" rows="2" outlined />
                </v-col>
                <v-col>
                    <v-textarea v-model="postDonationDetail[1].Details" label="Day 2 Post Donation" rows="2" outlined />
                </v-col>
                <v-col>
                    <v-textarea v-model="postDonationDetail[2].Details" label="Day 3 Post Donation" rows="2" outlined />
                </v-col>
                <v-col>
                    <v-textarea label="Supervisor's Note" rows="2" outlined />
                </v-col>
            </v-row>

            <div class="section-outer-container text-right pt-3 pb-2">
                <!--<v-btn color="default" large tile class="mr-2" v-if="" @click=""><v-icon color="success" size="25" left>mdi-content-save</v-icon> Save</v-btn>-->
                   <v-btn color="default" large tile class="mr-2" @click="onApprove"><v-icon color="success" size="25" left>mdi-check</v-icon> Save</v-btn>
            </div>
        </v-form>
    </v-container>
</template>

<script lang="ts">
    import VueBase from '@/components/VueBase';
    import { Component, Watch } from 'vue-property-decorator';
    import { getModule } from 'vuex-module-decorators';
    import DonorModule from '@/store/DonorModule';
    import LookupModule from '@/store/LookupModule';
    import DonorScreeningService from '@/services/DonorScreeningService';
    import Common from '@/common/Common';
    import { DonorPostDonationCare, DonorPostDonationCareDto, IDonorPostDonationCare } from '@/models/DonorScreening/DonorPostDonationCareDto';
    import { VitalSignsMonitoringDetailsDto, IVitalSignsMonitoringDetailsDto } from '@/models/DonorScreening/VitalSignsMonitoringDetailsDto';
    import { IPostDonationDetail,PostDonationDetailsDto } from '@/models/DonorScreening/PostDonationDetailsDto';
    import { ILookupOptions } from '@/models/Lookups/LookupOptions';
    import { LookupKeys } from '@/models/Enums/LookupKeys';
    import { DonorStatus } from '@/models/Enums/DonorStatus';
    import moment from 'moment';


    @Component
    export default class PostDonationCare extends VueBase {

        protected donorModule: DonorModule = getModule(DonorModule, this.$store);
        protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
        protected donorScreeningService: DonorScreeningService = new DonorScreeningService();

        protected formValid: boolean = true;
        protected rules: any = { ...Common.ValidationRules }
        protected vTime: string = '';
    
        protected errorMessage: string = '';
       
        protected donorPostDonationCare: IDonorPostDonationCare = new DonorPostDonationCareDto();
        /* protected vitalSignsMonitoringDetails: IVitalSignsMonitoringDetailsDto = new VitalSignsMonitoringDetailsDto();*/
        protected vitalSignsMonitoringDetails: IVitalSignsMonitoringDetailsDto[] = [];
        protected postDonationDetail: IPostDonationDetail[] = [];
        protected isEditingValue: boolean = false;
        protected isDisabled: boolean = true;


        public async created(): Promise<void> {

            let loader = this.showLoader();

            try {

                await this.getDonorPostDonationCare();
            }
            catch (error) {
                console.log(error);
            }
            finally {
                loader.hide();
            }
        }

        protected async getDonorPostDonationCare(): Promise<void> {
            if (this.$route.params.reg_id && typeof this.$route.params.reg_id === 'string') {
                const regId = this.$route.params.reg_id;
                this.donorPostDonationCare = await this.donorScreeningService.getDonorPostDonationCare(regId);

               
                const rawDetails = this.donorPostDonationCare.VitalSignsMonitoringDetails;

                const rawPostDonationDetails = this.donorPostDonationCare.PostDonationListDetails;

                this.postDonationDetail = Array.isArray(rawPostDonationDetails) ? [...rawPostDonationDetails] : [];

                this.vitalSignsMonitoringDetails = Array.isArray(rawDetails) ? [...rawDetails] : [];

                console.log("postDonationDetail", this.postDonationDetail);
                while (this.postDonationDetail.length < 3) {
                    this.postDonationDetail.push(new PostDonationDetailsDto());
                }

                // Fill the array to always have 4 items
                while (this.vitalSignsMonitoringDetails.length < 4) {
                    this.vitalSignsMonitoringDetails.push(new VitalSignsMonitoringDetailsDto());
                }
               /* console.log("PostDonationCare:", this.vitalSignsMonitoringDetails);*/

                //this.vitalSignsMonitoringDetails.forEach(detail => {
                //    if (detail.Time) {
                //        detail.Time = moment(detail.Time).format('HH:mm');
                //    } else {
                //        detail.Time = '';
                //    }
                //});

                this.vitalSignsMonitoringDetails.forEach(detail => {
                    const isEmpty = !detail.Time || detail.Time.startsWith('0001-01-01');
                    detail.Time = isEmpty ? '' : moment(detail.Time).format('HH:mm');
                });

                this.donorModule.setTransactionId(this.donorPostDonationCare.DonorTransactionId);
            }
        }

        //protected async getDonorPostDonationCare(): Promise<void> {
        //    if (this.$route.params.reg_id && typeof (this.$route.params.reg_id) === 'string') {
        //        let regId = this.$route.params.reg_id;
        //        this.donorPostDonationCare = await this.donorScreeningService.getDonorPostDonationCare(regId);
        //        /*console.log(this.donorBloodBagIssuance);*/
        //        this.vitalSignsMonitoringDetails = [this.donorPostDonationCare.VitalSignsMonitoringDetails] || new VitalSignsMonitoringDetailsDto();
        //        console.log("PostDonationCare:", this.vitalSignsMonitoringDetails);
               
        //        this.donorModule.setTransactionId(this.donorPostDonationCare.DonorTransactionId);
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

        public async onApprovalConfirmation(confirm: boolean): Promise<void> {
            if (confirm) {
                this.donorPostDonationCare.VitalSignsMonitoringDetails = this.vitalSignsMonitoringDetails;
                this.donorPostDonationCare.PostDonationListDetails = this.postDonationDetail;
                await this.onSubmit();
            }
        }

        public async onSubmit(): Promise<void> {
            console.log(this.donorPostDonationCare);
            let loader = this.showLoader();
            try {
                //this.donorVitalSigns.RecentDonations = this.recentDonations;
                await this.donorScreeningService.upsertDonorPostDonationCare(this.donorPostDonationCare);
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


