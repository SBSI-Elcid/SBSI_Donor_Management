<template>
  <div>
    <v-form class="form-container" ref="form" v-model="formValid" lazy-validation>
      <table style="width:100%">
        <tr v-for="question in donorMedicalHistories" :key="question.donorMedHistory.MedicalQuestionnaireId">
          <td style="width: 65%;">
            <div v-if="question.header != null" class="section-outer-container medical-header-label pa-2">{{question.header}}</div>
            <label class="subtitle-1 pa-2">{{question.question}}</label>
          </td>

          <td style="width: 15%;">
            <v-radio-group 
              v-if ="!question.hasHistory"
              v-model="question.donorMedHistory.Answer" 
              :rules="[rules.required]" 
              hide-details="true" 
              row
            >
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
            <v-text-field 
              v-if="" 
              v-model="question.donorMedHistory.Remarks"
              placeholder="Remarks"
              hide-details="true"
              dense flat solo 
            />

            <p class="pa-2 text-caption">
              {{ question.donorMedHistory.Remarks }}
            </p>
          </td>
        </tr>
      </table>
      <v-divider />

      <!-- BUTTONS -->
      <div class="section-outer-container text-right pt-3 pb-2">
        <!--<v-btn color="default" large tile class="mr-2" v-if="" @click=""><v-icon color="success" size="25" left>mdi-content-save</v-icon> Save</v-btn>-->
        <v-btn color="default" large tile class="mr-2" v-if="" @click=""><v-icon color="success" size="25" left>mdi-check</v-icon> Approve</v-btn>
        <v-btn color="default" large tile class="mr-2" v-if="" @click=""><v-icon color="warning" size="25" left>mdi-cancel</v-icon> Mark as Deferred</v-btn>
      </div>
    </v-form>
  </div>
</template>
<script lang="ts">
import { Component, Emit, Prop, Vue } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import DonorModule from '@/store/DonorModule';
import { IMedicalQuestionnaireDto } from '@/models/DonorRegistration/MedicalQuestionnaireDto';
import { DonorMedicalHistoryDto, IDonorMedicalHistoryDto } from '@/models/DonorRegistration/DonorMedicalHistoryDto';
import { IDonorDto } from '@/models/DonorRegistration/DonorDto';
import Common from '@/common/Common';
import DonorRegistrationService from '@/services/DonorRegistrationService';
import moment from 'moment';
import { DonorStatus } from '@/models/Enums/DonorStatus';

@Component
export default class MedicalHistoryForm extends Vue {
  //@Prop({ required: true, default: false })
  //public inReviewPage!: boolean;
  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected donorRegistrationId: Guid = '';
  protected donorRegistrationService: DonorRegistrationService = new DonorRegistrationService();
  //@Prop({ required: false, default: false })
    //public inProcess!: boolean;

  protected donorInfo?: IDonorDto;

  protected donorModule: DonorModule = getModule(DonorModule, this.$store);



  protected get medicalQuestionnaires(): Array<IMedicalQuestionnaireDto> {
    let questionnaires = this.donorModule.getMedicalQuestionnaire;
    
    if (this.newDonor.Gender === "Male") {
      questionnaires = questionnaires.filter(x => x.GenderOption != "Female");
    }
    return questionnaires;
  }

//  protected async newDonor(): Promise<IDonorDto | undefined> {
//  if (this.$route.params.reg_id && typeof this.$route.params.reg_id === 'string') {
//    this.donorRegistrationId = this.$route.params.reg_id;

//    try {
//      const donorInfo: IRegisteredDonorInfoDto = await this.donorRegistrationService.getRegisteredDonorInfo(this.donorRegistrationId);
//      this.donorModule.setTransactionId(donorInfo.DonorTransactionId);

//      if (Common.hasValue(donorInfo.DonorStatus) && donorInfo.DonorStatus === DonorStatus.Deferred) {
//        this.donorModule.setDonorStatus(donorInfo.DonorStatus);
//      }

//      donorInfo.BirthDate = moment(donorInfo.BirthDate).toDate();
//      this.donorModule.setDonorInformation(donorInfo);

//      return this.donorModule.getDonorInformation;
//    } catch (error) {
//      console.error(error);
//    }
//  }

//  return undefined;
    //}
     protected async created() {
    await this.loadDonorInfo(); // Load donor info before mounting
    await this.donorModule.loadMedicalQuestionnaire(); // Fetch medical questionnaires
  }

    //protected async mounted() {
    //    try {
    //        await this.loadDonorInfo();  // Wait until donor info is fully loaded
    //        await this.donorModule.loadMedicalQuestionnaire(); // Wait for medical questionnaires
    //    } catch (error) {
    //        console.error("Error loading data: ", error);
    //    }
    //}
    protected async loadDonorInfo(): Promise<void> {
        if (this.$route.params.reg_id && typeof this.$route.params.reg_id === 'string') {
            this.donorRegistrationId = this.$route.params.reg_id;

            try {
                const donorInfo: IRegisteredDonorInfoDto = await this.donorRegistrationService.getRegisteredDonorInfo(this.donorRegistrationId);

                this.donorModule.setTransactionId(donorInfo.DonorTransactionId);

                if (Common.hasValue(donorInfo.DonorStatus) && donorInfo.DonorStatus === DonorStatus.Deferred) {
                    this.donorModule.setDonorStatus(donorInfo.DonorStatus);
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
        let medicalQuestion: { header: string, question: string, donorMedHistory: IDonorMedicalHistoryDto } = {
            header: question.HeaderText,
            question: question.QuestionTagalogText,
            donorMedHistory: {
                MedicalQuestionnaireId: question.MedicalQuestionnaireId,
                Answer: donorMedicalHistory.Answer,
                Remarks: donorMedicalHistory.Remarks,
                HeaderText: question.HeaderText,
                QuestionText: question.QuestionTagalogText
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

  protected onApprove(): void {
  this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
  if(this.formValid === false) {
    return;
  }

  this.confirm(`Are you sure you want to proceed with approving this donor?`, 'Approve Donor', 'Approve', 'Cancel', this.onSubmit);
  }

    public onSubmit(): void {
        let donorMedicalHistory: Array<IDonorMedicalHistoryDto> = this.donorMedicalHistories.map(x => x.donorMedHistory);
        this.newDonor.MedicalHistories = donorMedicalHistory;
        this.donorModule.setDonorInformation(this.newDonor);
    }

    //@Emit('goToStep')
  //public onBack(): number {
  //  return 1;
  //}

  //@Emit('goToStep')
  //public onNext(): number | void {

  //  let donorMedicalHistory: Array<IDonorMedicalHistoryDto> = this.donorMedicalHistories.map(x => x.donorMedHistory);
  //  this.newDonor.MedicalHistories = donorMedicalHistory;
  //  this.donorModule.setDonorInformation(this.newDonor);
    
  //  return 3;
  //}
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