<template>
  <div>
    <v-stepper v-model="currentStep">
      <v-stepper-header>
        <v-stepper-step :complete="currentStep > 1" step="1" :editable="donorCanProceed" complete-icon="mdi-pencil">
          Donor Personal Information
          <small>(to be filled by the donor)</small>
        </v-stepper-step>
        <v-divider />
      </v-stepper-header>

      <v-stepper-items> 
        <v-stepper-content step="1">
          <PersonalData :inReviewPage="false" @goToStep="goToStep" @submit="onSubmit" :scheduleId ="scheduleId" />
        </v-stepper-content>

        <!--<v-stepper-content step="2">
          <MedicalHistoryForm :inReviewPage="false" @goToStep="goToStep" />
        </v-stepper-content>
        <v-stepper-content step="3">
          <ConsentData :inReviewPage="false" @goToStep="goToStep" @submit="onSubmit" />
        </v-stepper-content>-->
      </v-stepper-items>
    </v-stepper>
  </div>
</template>

<script lang="ts">
import VueBase from '../VueBase';
import { Component, Emit,Prop } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import PersonalData from '@/components/DonorRegistration/RegistrationForms/PersonalDataForm.vue';
import MedicalHistoryForm from '@/components/DonorRegistration/RegistrationForms/MedicalHistoryForm.vue';
import ConsentData from '@/components/DonorRegistration/RegistrationForms/InformedConsentForm.vue';
import DonorModule from '@/store/DonorModule';
import AuthModule from '@/store/AuthModule';
import DonorRegistrationService from '@/services/DonorRegistrationService';
import { IDonorDto } from '@/models/DonorRegistration/DonorDto';
import LookupModule from '@/store/LookupModule';
import moment from 'moment';

@Component({
   components: { PersonalData, MedicalHistoryForm, ConsentData }
})
export default class DonorRegistration extends VueBase { 

    @Prop({ default: "" }) readonly scheduleId!: string;
  protected donorModule: DonorModule = getModule(DonorModule, this.$store);
  protected authModule: AuthModule = getModule(AuthModule, this.$store);
  protected donorRegistrationService: DonorRegistrationService = new DonorRegistrationService();
  protected lookupModule: LookupModule = getModule(LookupModule, this.$store);

  protected currentStep: number = 1;
  protected donorCanProceed: boolean = false;
  protected errorMessage: string = '';
  

  protected get newDonor(): IDonorDto {
    return this.donorModule.getDonorInformation;
  }

  protected get hasRole(): (roles: Array<string>) => boolean {
    return (roles) => this.authModule.userHasAnyRole(roles);
  }
 
  protected async mounted(): Promise<void> {
    let loader = this.showLoader();
    try {
      await this.donorModule.loadMedicalQuestionnaire();
      await this.lookupModule.loadLookups();
      
      this.newDonor.BirthDate = moment(this.newDonor.BirthDate).toDate();
      this.donorModule.setDonorInformation(this.newDonor);
    } 
    catch (error) {
      console.log(error);
    }
    finally {
      loader.hide();
    }
  }

  protected beforeDestroy(): void {
    this.donorModule.resetDonor();
  }

  protected goToStep(step: number): void {
    this.currentStep = step;

    if (step === 1) {
      this.donorCanProceed = true;
    }
    
    window.scrollTo(0,0);
  }

  public onSubmit(): void {

      console.log(this.scheduleId);
    this.confirm('Please make sure all entered data is correct, click Cancel to edit or Submit to continue.', '', 'Submit', 'Cancel', this.onSubmitConfirmation)
  }

  public async onSubmitConfirmation(confirm: boolean): Promise<void> {
    if (confirm) {
      let loader = this.showLoader();
      try {
        let createDto: IDonorDto = this.donorModule.getDonorInformation;

        createDto.ScheduleId = this.scheduleId;
        let registrationNumber = await this.donorRegistrationService.register(createDto);
        this.donorModule.resetDonor();
        this.notify_success('Donor successfully registered.');

        this.onSuccessfulRegistration(registrationNumber);
      }
      catch(error: any) {
        if (error.StatusCode != 500) {
          this.errorMessage = error.Message;
          this.notify_error(this.errorMessage);
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

  @Emit('onSuccess')
  public onSuccessfulRegistration(registrationNumber: string): Guid {
    return registrationNumber;
  }
}
</script>

<style lang="scss" scoped>

</style>