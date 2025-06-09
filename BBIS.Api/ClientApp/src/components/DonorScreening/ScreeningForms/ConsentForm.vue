<template>
    <div>
        <v-form class="form-container" ref="form" v-model="formValid" lazy-validation>
            <table style="width:100%">
                <tr>
                    <td style="width: 65%;">
                        <label class="subtitle-1 pa-2">
                            <div class="pr-2 pl-2 text-center">
                                <h3>DONOR'S INFORMED CONSENT</h3>
                            </div>
                        </label>

                        <label class="subtitle-1 pa-2">
                            <div class="pr-2 pl-2 text-center">
                                <p>
                                    I certify that I am the person referred to in all entries which wre read and well understood by me. I am made aware of the advantages of voluntary blood donation and that it has to be done out of my free will,
                                    aware of risks during and after extraction. The same have been explained to me in an undestandable language and dialect that I speak.
                                </p>
                                <p>
                                    I am informed that the donated blood will be given through <b>VSMMC-BLOOD SERVICE UNIT</b>. Furthermore, the following have been explained to and understood by me:
                                </p>
                                <p>
                                    The donated blood will be tested for the blood type, hemoglobin the (NVSP required/approved tests for trasfusion transmissible infections including HIV)
                                    and other tests for purpose of research and continuing medical practice and no official result/s will be released to me.
                                </p>
                                <p>
                                    If found reactive, my blood/blood sample will be submitted to the National Reference Laboratory for confirmatory testing.
                                </p>
                                <p>
                                    When Confirmed to have a disease, I AGREE/NOT AGREE that the result will be relayed to the Municipal Health Officer (MHO)/Company Physician/Agency-identified Blood Program Coordinator
                                    /my Physician of choice, and will be referred to the appropriate agency/facility/specialty medicine for counseling ang other management.
                                </p>
                                <p>
                                    All information provided by and solicited from me in the processes involved in the donation will be delt with strict confidentiality and will only be limited to <b>VSMMC-BLOOD SERVICE UNIT</b>
                                    and the person/s I allow to have access on them.
                                </p>
                                <p>
                                    I certify that I have, to the best of my knowledge, truthfully answered the questions in the questionaire and the same is true with the questions araised during the counseling and I voluntarily
                                    submit myself to the donatioon process and to the facts stated above donating as Voluntary/Directed Donation.
                                </p>
                            </div>
                        </label>
                        <v-divider></v-divider>
                        <label class="subtitle-1 pa-2">
                            <div class="pr-2 pl-2 text-center">
                                <p>
                                    I certify that I am the person referred to in all entries which wre read and well understood by me. I am made aware of the advantages of voluntary blood donation and that it has to be done out of my free will,
                                    aware of risks during and after extraction. The same have been explained to me in an undestandable language and dialect that I speak.
                                </p>
                                <p>
                                    I am informed that the donated blood will be given through <b>VSMMC-BLOOD SERVICE UNIT</b>. Furthermore, the following have been explained to and understood by me:
                                </p>
                                <p>
                                    The donated blood will be tested for the blood type, hemoglobin the (NVSP required/approved tests for trasfusion transmissible infections including HIV)
                                    and other tests for purpose of research and continuing medical practice and no official result/s will be released to me.
                                </p>
                                <p>
                                    If found reactive, my blood/blood sample will be submitted to the National Reference Laboratory for confirmatory testing.
                                </p>
                                <p>
                                    When Confirmed to have a disease, I AGREE/NOT AGREE that the result will be relayed to the Municipal Health Officer (MHO)/Company Physician/Agency-identified Blood Program Coordinator
                                    /my Physician of choice, and will be referred to the appropriate agency/facility/specialty medicine for counseling ang other management.
                                </p>
                                <p>
                                    All information provided by and solicited from me in the processes involved in the donation will be delt with strict confidentiality and will only be limited to <b>VSMMC-BLOOD SERVICE UNIT</b>
                                    and the person/s I allow to have access on them.
                                </p>
                                <p>
                                    I certify that I have, to the best of my knowledge, truthfully answered the questions in the questionaire and the same is true with the questions araised during the counseling and I voluntarily
                                    submit myself to the donatioon process and to the facts stated above donating as Voluntary/Directed Donation.
                                </p>
                            </div>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td v-if="DonorInformation.Age >= 18">
                    </td>
                    <td v-else>
                        <label class="subtitle-1 pa-2">
                            <div class="pr-2 pl-2 text-center">
                                <h3>PARENT'S CONSENT FOR MINOR BLOOD DONORS</h3>
                            </div>
                        </label>
                        <label class="subtitle-1 pa-2">
                            <div class="pr-2 pl-2 text-center">
                                <v-row>
                                    <v-col class="pt-0 pb-0">
                                        <p>I am allowing my (child):</p>
                                    </v-col>
                                </v-row>
                                <v-row>
                                    <v-col cols="12" lg="4" md="4" sm="12" class="pt-0 pb-0">
                                        <v-text-field v-model="DonorInformation.Firstname"
                                                      :rules="[rules.required, rules.maxLength(90)]"
                                                      :disabled="true"
                                                      dense outlined />
                                    </v-col>
                                    <v-col cols="12" lg="4" md="4" sm="12" class="pt-0 pb-0">
                                        <v-text-field v-model="DonorInformation.Middlename"
                                                      :rules="[rules.maxLength(90)]"
                                                      :disabled="true"
                                                      dense outlined />
                                    </v-col>
                                    <v-col cols="12" lg="4" md="4" sm="12" class="pt-0 pb-0">
                                        <v-text-field v-model="DonorInformation.Lastname"
                                                      :rules="[rules.required, rules.maxLength(90)]"
                                                      :disabled="true"
                                                      dense outlined />
                                    </v-col>
                                </v-row>
                                <v-row>
                                    <v-col cols="12" lg="12" md="12" sm="12" class="pt-0 pb-0">
                                        <p>to voluntary donate blood on:</p>
                                    </v-col>
                                    <v-col cols="12" lg="12" md="12" sm="12" class="pt-0 pb-0">
                                        <p><b>VICENTE SOTTO MEMORIAL MEDICAL CENTER - BLOOD SERVICE UNIT</b></p>
                                    </v-col>
                                </v-row>
                                <v-row>
                                    <v-col cols="12" lg="12" md="12" sm="12" class="pt-0 pb-0">
                                        <p>Date:</p>
                                    </v-col>
                                    <v-col cols="12" lg="12" md="12" sm="12" class="pt-0 pb-0">
                                        <p><b>{{new Date().toLocaleDateString()}}</b></p>
                                    </v-col>
                                </v-row>
                                <v-row>
                                    <v-col cols="12" lg="12" md="12" sm="12" class="pt-0 pb-0">
                                        <p>Venue:</p>
                                    </v-col>
                                    <v-col cols="12" lg="12" md="12" sm="12" class="pt-0 pb-0">
                                        <p><b>VICENTE SOTTO MEMORIAL MEDICAL CENTER - BLOOD SERVICE UNIT</b></p>
                                    </v-col>
                                </v-row>
                                <v-row>
                                    <v-col cols="12" lg="12" md="12" sm="12" class="pt-0 pb-0">
                                        <p>I am aware that the blood collected will be used to save life of a patient who needs it most.</p>
                                    </v-col>
                                    <v-col cols="12" lg="4" md="4" sm="12" class="pt-0 pb-0">

                                    </v-col>
                                    <v-col cols="12" lg="4" md="4" sm="12" class="pt-0 pb-0">
                                        <p><b>RELATION TO THE BLOOD DONOR</b></p>
                                        <v-autocomplete :items="relationsOptions"
                                                        :rules="[rules.required]"
                                                        dense outlined />
                                    </v-col>
                                </v-row>
                            </div>
                        </label>
                    </td>
                </tr>
            </table>
            <v-divider />

            <!-- BUTTONS -->
            <div class="section-outer-container text-right pt-3 pb-2" v-if ="!isDisabled"> 
                <v-btn v-if="" color="primary" large class="mr-2" @click="onApprove"><v-icon size="25" left>mdi-content-save</v-icon> {{ submitLabel }}</v-btn>
            </div>
        </v-form>
    </div>
</template>
<script lang="ts">
    import VueBase from '@/components/VueBase';
    import { Component, Emit, Prop, Vue, Watch } from 'vue-property-decorator';
    import { getModule } from 'vuex-module-decorators';
    import DonorModule from '@/store/DonorModule';
    import { IMedicalQuestionnaireDto } from '@/models/DonorRegistration/MedicalQuestionnaireDto';
    import { DonorMedicalHistoryDto, IDonorMedicalHistoryDto } from '@/models/DonorRegistration/DonorMedicalHistoryDto';
    import { IDonorDto } from '@/models/DonorRegistration/DonorDto';
    import Common from '@/common/Common';
    import { ILookupOptions } from '@/models/Lookups/LookupOptions';
    import { LookupKeys } from '@/models/Enums/LookupKeys';
    import LookupModule from '@/store/LookupModule';
    import DonorRegistrationService from '@/services/DonorRegistrationService';
    import { IRegisteredDonorInfoDto } from '@/models/DonorRegistration/IRegisteredDonorInfoDto';
    import { DonorStatus } from '@/models/Enums/DonorStatus';
    import moment from 'moment';
    import DonorScreeningService from '../../../services/DonorScreeningService';
    @Component
    export default class MedicalHistoryForm extends VueBase {
        //@Prop({ required: true, default: false })
        //public inReviewPage!: boolean;
        protected formValid: boolean = true;
        protected rules: any = { ...Common.ValidationRules }
        protected donorRegistrationService: DonorRegistrationService = new DonorRegistrationService();
        protected donorScreeningService: DonorScreeningService = new DonorScreeningService();
        //@Prop({ required: false, default: false })
        //public inProcess!: boolean;

        protected donorModule: DonorModule = getModule(DonorModule, this.$store);
        protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
        protected isDisabled: boolean = true;
        /*protected donorInfo?: IDonorDto;*/

        protected async created() {

            try {
                const RegisteredDonorInfo: IRegisteredDonorInfoDto = await this.donorRegistrationService.getRegisteredDonorInfo(this.$route.params.reg_id);
                console.log(RegisteredDonorInfo);

                this.donorModule.setTransactionId(RegisteredDonorInfo.DonorTransactionId);

                if (Common.hasValue(RegisteredDonorInfo.DonorStatus) && RegisteredDonorInfo.DonorStatus === DonorStatus.Deferred && this.RegisteredDonorInfo.DonorStatus !== DonorStatus.ForCounseling) {
                    this.isDisabled = false;
                    this.donorModule.setDonorStatus(RegisteredDonorInfo.DonorStatus);
                }

                
                this.donorModule.setDonorInformation(RegisteredDonorInfo);
                
            } catch (error) {
                console.error(error);
            }
       
            
            //await this.loadDonorInfo(); // Load donor info before mounting
            // Fetch medical questionnaires
        }
      
        protected get submitLabel(): string {
            return "I Agree and Submit";
        }

        @Watch('DonorInformation.BirthDate')
        protected onChangeModelBirthDate(): void {
            this.calculateAge();
        }

        protected calculateAge(): void {
            let years = moment().diff(moment(this.DonorInformation.BirthDate, 'YYYY-MM-DD'), 'years');
            this.DonorInformation.Age = years;
            console.log("DonorAge",this.DonorInformation.Age)
        }

        protected get DonorInformation(): IDonorDto {
            return this.donorModule.getDonorInformation;

        }
        public get options(): (key: string) => Array<ILookupOptions> {
            return (key) => this.lookupModule.getOptionsByKey(key);
        }


        protected get relationsOptions(): Array<{ text: string, value: string }> {
            /*let options = this.options(LookupKeys.RelationsToDonor  arguments);*/
            let options = this.options(LookupKeys.RelationsToDonor);
            return options.map(x => { return { text: x.Text, value: x.Value } });
        }

        protected async onApprove(): void {
            console.log(this.DonorInformation.Age)

            let dto: IRegisteredDonorInfoDto = await this.donorRegistrationService.getRegisteredDonorInfo(this.$route.params.reg_id);
            let transactionId = await this.donorScreeningService.uMethodBloodCollection(dto);

            this.notify_success('Donor is now ready For Blood Collection.');
            this.$router.push({ path: '/donors' });
            //this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
            //if (this.formValid === false) {
            //    return;
            //}

            //this.confirm(`Are you sure you want to proceed with approving this donor?`, 'Approve Donor', 'Approve', 'Cancel', this.onSubmit);
        }


        //@Emit('goToStep')
        //public onBack(): number {
        //  return 2;
        //}

        //  @Emit('submit')
        //  public onSubmit(): void {
        //    let donorMedicalHistory: Array<IDonorMedicalHistoryDto> = this.donorMedicalHistories.map(x => x.donorMedHistory);
        //    this.newDonor.MedicalHistories = donorMedicalHistory;
        //    this.donorModule.setDonorInformation(this.newDonor);
        //  }
    }
</script>

<style lang="scss" scoped>
  table, th, td {
    border: 1px solid rgb(139, 138, 138);
    border-collapse: collapse;
  }

  .medical-header-label {
    font-weight: bold;
  }

  .medical-question-row {
    padding-bottom: 10px !important;
    border-top: 1px solid rgb(139, 138, 138);
  }

  .medical-question-radio-buttons {
    margin-top: 0px !important;
  }

</style>