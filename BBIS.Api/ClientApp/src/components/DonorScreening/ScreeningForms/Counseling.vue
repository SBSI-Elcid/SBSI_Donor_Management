<template>
  <div>
      <v-form class="form-container" ref="form" v-model="formValid" lazy-validation>
          <v-row>
              <v-col cols="12" md="12" class="d-flex justify-end align-center">
                  <v-radio-group v-model="selectedLanguage" row class="justify-end">
                      <v-radio label="Tagalog" value="tagalog"></v-radio>
                      <v-radio label="English" value="english"></v-radio>
                      <v-radio label="Other Dialect" value="otherDialect"></v-radio>
                  </v-radio-group>
              </v-col>
          </v-row>
                  <table style="width:100%">
                      <tr v-for="question in donorMedicalHistories" :key="question.donorMedHistory.MedicalQuestionnaireId">
                          <td style="width: 65%;">
                              <div v-if="question.header != null" class="section-outer-container medical-header-label pa-2">{{question.header}}</div>
                              <label class="subtitle-1 pa-2">{{question.question}}</label>
                          </td>

                          <td style="width: 15%;">
                              <v-radio-group v-if="!question.hasHistory"
                                             v-model="question.donorMedHistory.Answer"
                                             :rules="[rules.required]"
                                             :disabled="isDisabled"
                                             hide-details="true"
                                             row>
                                  <v-radio label="Yes" value="Yes" />
                                  <v-radio label="No" value="No" />
                              </v-radio-group>

                              <div v-else class="pr-2 pl-2 text-center">
                                  <span>{{ question.donorMedHistory.Answer }}</span>
                                  <v-icon v-if="question.donorMedHistory.Answer === 'Yes'" color="success">mdi-check</v-icon>
                                  <v-icon v-else color="error">mdi-close</v-icon>
                              </div>
                          </td>

                          <td style="width: 20%;">
                              <v-text-field v-if=""
                                            v-model="question.donorMedHistory.Remarks"
                                            :disabled="isDisabled"
                                            placeholder="Remarks"
                                            hide-details="true"
                                            dense flat solo />

                              <p class="pa-2 text-caption">
                                  <!--{{ question.donorMedHistory.Remarks }}-->
                              </p>
                          </td>
                      </tr>
                  </table>
                  <v-divider />

                  <!-- BUTTONS -->
                  <div class="section-outer-container text-right pt-3 pb-2" v-if="!isDisabled">
                      <!--<v-btn color="default" large tile class="mr-2" v-if="" @click=""><v-icon color="success" size="25" left>mdi-content-save</v-icon> Save</v-btn>-->
                      <v-btn color="default" large tile class="mr-2" v-if="" @click="onApprove"><v-icon color="success" size="25" left>mdi-check</v-icon> Approve</v-btn>
                      <v-btn color="default" large tile class="mr-2" v-if="" @click="onDeferred"><v-icon color="warning" size="25" left>mdi-cancel</v-icon> Mark as Deferred</v-btn>
                  </div>
</v-form>
  </div>
</template>
<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Emit, Prop, Vue,Watch } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import DonorModule from '@/store/DonorModule';
import { IMedicalQuestionnaireDto } from '@/models/DonorRegistration/MedicalQuestionnaireDto';
import { DonorMedicalHistoryDto, IDonorMedicalHistoryDto } from '@/models/DonorRegistration/DonorMedicalHistoryDto';
import { IDonorDto } from '@/models/DonorRegistration/DonorDto';
import { IRegisteredDonorInfoDto } from '@/models/DonorRegistration/IRegisteredDonorInfoDto';
import Common from '@/common/Common';
import DonorRegistrationService from '@/services/DonorRegistrationService';
import DonorScreeningService from '@/services/DonorScreeningService';
import moment from 'moment';
import { DonorStatus } from '@/models/Enums/DonorStatus';
import { DonorCounselingDto, IDonorCounseling } from '../../../models/DonorScreening/DonorCounselingDto';

    @Component
    export default class MedicalHistoryForm extends VueBase {

        protected formValid: boolean = true;
        protected rules: any = { ...Common.ValidationRules }
        protected donorRegistrationId: Guid = '';
        protected donorRegistrationService: DonorRegistrationService = new DonorRegistrationService();
        protected donorScreeningService: DonorScreeningService = new DonorScreeningService();
        protected selectedLanguage: string = '';

        protected donorCounseling: IDonorCounseling = new DonorCounselingDto();

        protected donorInfo?: IDonorDto;

        protected donorModule: DonorModule = getModule(DonorModule, this.$store);
        protected isDisabled: boolean = false;

        protected get medicalQuestionnaires(): Array<IMedicalQuestionnaireDto> {
            let questionnaires = this.donorModule.getMedicalQuestionnaire;

            if (this.newDonor.Gender === "Male") {
                questionnaires = questionnaires.filter(x => x.GenderOption != "Female");
            }
            return questionnaires;
        }

       
        protected async created() {
            this.selectedLanguage = "english";
            await this.loadDonorInfo(); // Load donor info before mounting
            await this.donorModule.loadMedicalQuestionnaire(); // Fetch medical questionnaires
            this.donorModule.fetchDonorActivityType(this.$route.params.reg_id);
        }

        
         mounted() {
            window.addEventListener('keydown', this.handleF11Key);
        }
         beforeDestroy() {
            window.removeEventListener('keydown', this.handleF11Key);
        }

        protected handleF11Key(event: KeyboardEvent): void {
            if (event.key === 'F11') {
                event.preventDefault(); // prevent browser fullscreen
                this.setAllAnswersToYes();
            }
        }
        protected setAllAnswersToYes(): void {
            this.donorMedicalHistories.forEach((question) => {
                if (!question.hasHistory && question.donorMedHistory) {
                    this.$set(question.donorMedHistory, 'Answer', 'Yes'); // ensure reactivity
                }
            });
        }

        @Watch('selectedLanguage')
        onSelectedLanguageChanged() {
            
            // If needed, you can force recomputation or trigger other logic here
        }

        protected async loadDonorInfo(): Promise<void> {
            if (this.$route.params.reg_id && typeof this.$route.params.reg_id === 'string') {
                this.donorRegistrationId = this.$route.params.reg_id;

                try {
                    const donorInfo: IRegisteredDonorInfoDto = await this.donorRegistrationService.getRegisteredDonorInfo(this.donorRegistrationId);

                    this.donorModule.setTransactionId(donorInfo.DonorTransactionId);
                    if (Common.hasValue(donorInfo.DonorStatus) && donorInfo.DonorStatus !== DonorStatus.Deferred && donorInfo.DonorStatus !== DonorStatus.ForCounseling) {
                        this.isDisabled = true;
                    }
                   
                    donorInfo.BirthDate = moment(donorInfo.BirthDate).toDate();
                    this.donorModule.setDonorInformation(donorInfo);

                } catch (error) {
                    console.error(error);
                }

             
            }
          
        }

        protected get newDonor(): IDonorDto {
            return this.donorModule.getDonorInformation;
        }

        protected get donorMedicalHistories(): Array<{ header: string, question: string, donorMedHistory: IDonorMedicalHistoryDto, hasHistory: boolean }> {
            let medicalQuestions: Array<{ header: string, question: string, donorMedHistory: IDonorMedicalHistoryDto, hasHistory: boolean }> = [];
            this.medicalQuestionnaires.forEach(question => {
                let donorMedicalHistory: DonorMedicalHistoryDto = this.newDonor.MedicalHistories.find(x => x.MedicalQuestionnaireId === question.MedicalQuestionnaireId) ?? new DonorMedicalHistoryDto();
                let hasHistory = this.newDonor.MedicalHistories.some(
                    x => x.MedicalQuestionnaireId === question.MedicalQuestionnaireId
                );
                let questionText = '';
 
                switch (this.selectedLanguage) {
                    case 'english':
                        questionText = question.QuestionEnglishText;
                        break;
                    case 'tagalog':
                        questionText = question.QuestionTagalogText;
                        break;
                    case 'otherDialect':
                        questionText = question.QuestionOtherDialectText;
                        break;
                    default:
                        questionText = question.QuestionEnglishText;
                        break;
                }
               
                let medicalQuestion: { header: string, question: string, donorMedHistory: IDonorMedicalHistoryDto } = {
                    header: question.HeaderText,
                    question: questionText,
                    donorMedHistory: {
                        MedicalQuestionnaireId: question.MedicalQuestionnaireId,
                        Answer: donorMedicalHistory.Answer,
                        Remarks: donorMedicalHistory.Remarks,
                        HeaderText: question.HeaderText,
                        QuestionText: questionText,
                    },
                    hasHistory: hasHistory
                };

                medicalQuestions.push(medicalQuestion);
            });

            return medicalQuestions;
        }

        protected checkForNext(): void {
            this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
            if (this.formValid) {
                this.onNext();
            }

            return;
        }


        protected onDeferred(): void {
            this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
            if (this.formValid === false) {
                return;
            }

            this.mark_deferred(`Are you sure you want to tag this donor as deffered?`, 'Mark Donor as Deferred', 'Mark as Deferred', 'Cancel', this.onDeferralConfirmation);
        }

        public async onDeferralConfirmation(confirm: boolean, result: any): Promise<void> {
            if (confirm) {
                this.donorCounseling.DonorStatus = DonorStatus.Deferred;
                this.donorCounseling.DeferralStatus = result[0].DeferralStatus;
                this.donorCounseling.Remarks = result[0].Remarks;
                await this.onSubmit();
            }
        }



        protected onApprove(): void {
            this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
            if (this.formValid === false) {
                return;
            }
            this.donorCounseling.DonorStatus = DonorStatus.ForConsent;
            this.confirm(`Are you sure you want to proceed with approving this donor?`, 'Approve Donor', 'Approve', 'Cancel', this.onSubmit);
        }

        public async onSubmit(confirm: boolean): Promise<void> {
            if (confirm) {
                let donorMedicalHistory: Array<IDonorMedicalHistoryDto> = this.donorMedicalHistories.map(x => x.donorMedHistory);
                this.newDonor.MedicalHistories = donorMedicalHistory;
                this.donorModule.setDonorInformation(this.newDonor);


                this.donorCounseling.medicalHistories = this.newDonor.MedicalHistories;
                this.donorCounseling.DonorRegistrationId = this.$route.params.reg_id;
                await this.insertData();
            }
            
        }

        public async insertData(): Promise<void> {

            
           try {
               await this.donorScreeningService.upsertCounselingScreening(this.donorCounseling);
               this.notify_success('Donor counseling saved successfully.');

               this.$router.push({ path: '/donors' });
                //this.$notify({ type: "success", text: "Donor counseling saved successfully." });
            } catch (error) {
                console.error("Insert failed", error);
                //this.$notify({ type: "error", text: "Failed to save donor counseling." });
            }
            
            //for (const history of this.newDonor.MedicalHistories) {
            //    /*console.log(this.newDonor.MedicalHistories);*/
            //    await this.donorScreeningService.upsertCounselingScreening(history);
            //}
        }
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