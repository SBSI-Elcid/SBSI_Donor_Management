<template>
    <div>
        <v-form class="form-container" ref="form" lazy-validation>
            <v-row>
                <v-col cols="3" lg="3" md="3" sm="12" class="py-0">
                    <label class="label-container">Skin</label>
                    <v-text-field 
                                  v-model ="donorPhysicalExam.Skin"
                                  :rules="[rules.maxLength(10)]"
                                  :disabled="isDisabled"
                                  dense outlined />
                </v-col>

                <v-col cols="3" lg="3" md="3" sm="12" class="py-0">
                    <label class="label-container">HEENT</label>
                    <v-text-field type="number"
                                  v-model ="donorPhysicalExam.HEENT"
                                  :disabled="isDisabled"
                                  dense outlined />
                </v-col>

                <v-col cols="3" lg="3" md="3" sm="12" class="py-0">
                    <label class="label-container">HEART and Lungs</label>
                    <v-text-field type="number"
                                  v-model ="donorPhysicalExam.HeartAndLungs"
                                  :disabled="isDisabled"
                                  dense outlined />
                </v-col>
            </v-row>

            <v-row>
                <v-col cols="12" lg="12" md="10" sm="12" class="py-0">
                    <label class="label-container">General appearance</label>
                    <v-text-field :rules="[rules.maxLength(50)]"
                                  v-model ="donorPhysicalExam.GeneralStatus"
                                  :disabled="isDisabled"
                                  dense outlined />
                </v-col>
            </v-row>

            <div class="section-outer-container mt-3">
                <div class="text-right">
                    <!--<v-btn color="default" large tile class="mr-2" v-if="isEditingValue && !isDisabled" @click="submit"><v-icon color="success" size="25" left>mdi-content-save</v-icon> Save</v-btn>
                    <v-btn color="default" large tile class="mr-2" v-if="!isEditingValue && isPassed && !isDisabled" @click="approveOrMarkDeferred"><v-icon color="success" size="25" left>mdi-check</v-icon> Approve</v-btn>
                    <v-btn color="default" large tile class="mr-2" v-if="!isEditingValue && !isPassed && !isDisabled" @click="approveOrMarkDeferred"><v-icon color="warning" size="25" left>mdi-cancel</v-icon> Mark as Deferred</v-btn>-->
                    <!--<v-btn color="default" large tile class="mr-2" v-if="isEditingValue && !isDisabled" @click=""><v-icon color="success" size="25" left>mdi-content-save</v-icon> Save</v-btn>-->
                    <v-btn color="default" large tile class="mr-2" v-if="!isEditingValue && !isDisabled" @click="approveOrMarkDeferred"><v-icon color="success" size="25" left>mdi-check</v-icon> Approve</v-btn>
                    <v-btn color="default" large tile class="mr-2" v-if="!isEditingValue && !isDisabled" @click=""><v-icon color="warning" size="25" left>mdi-cancel</v-icon> Mark as Deferred</v-btn>
                </div>
            </div>
        </v-form>
    </div>
</template>

<script lang="ts">import VueBase from '@/components/VueBase';
import { Component, Vue } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import DonorModule from '@/store/DonorModule';
import LookupModule from '@/store/LookupModule';
import Common from '@/common/Common';
import DonorScreeningService from '@/services/DonorScreeningService';
import { DonorPhysicalExaminationDto, IDonorPhysicalExaminationDto } from '@/models/DonorScreening/DonorPhysicalExaminationDto';
import { LookupKeys } from '@/models/Enums/LookupKeys';
import { ILookupOptions } from '@/models/Lookups/LookupOptions';
import { DonorStatus } from '@/models/Enums/DonorStatus';
import { PhysicalExamResultStatus } from '@/models/Enums/PhysicalExamResultStatus';

@Component
export default class PhysicalExamForm extends VueBase {
  protected donorModule: DonorModule = getModule(DonorModule, this.$store);
  protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
  protected donorScreeningService: DonorScreeningService = new DonorScreeningService();

  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected errorMessage: string = '';
  protected remarks: string | null = null;
  protected canAddReason: boolean = false;
  protected donorPhysicalExam: IDonorPhysicalExaminationDto = new DonorPhysicalExaminationDto();
  protected isEditingValue: boolean = false;
  protected isDisabled: boolean = true;

  public get options(): (key: string) => Array<ILookupOptions> {
    return (key) => this.lookupModule.getOptionsByKey(key);
  }

  protected get bloodBagTypesOptions(): Array<{text: string, value: string}> {
    let options = this.options(LookupKeys.BloodBagTypes);
    return options.map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get physicalExamResultOptions(): Array<{text: string, value: string}> {
    let options = this.options(LookupKeys.PhysicalExamResult);
    return options.map(x => { return { text: x.Text, value: x.Value} });
  }

  //protected get isPassed(): boolean {
  // return this.donorPhysicalExam.ResultStatus == PhysicalExamResultStatus.Passed;
  //}

  protected async created(): Promise<void> {
    let loader = this.showLoader();

    try {
      await this.getPhysicalExamInfo();
    }
    catch (error) {
      console.log(error);
    }
    finally {
      loader.hide();
    }
  }

  protected async getPhysicalExamInfo(): Promise<void>  {
    if (this.$route.params.reg_id && typeof (this.$route.params.reg_id) === 'string') {
      let id = this.$route.params.reg_id;
      this.donorPhysicalExam = await this.donorScreeningService.getPhysicalExamInfo(id);

      this.donorModule.setTransactionId(this.donorPhysicalExam.DonorTransactionId);

      // Enable the fields when Donor Status is not deferred and not for initial screening.
      if (Common.hasValue(this.donorPhysicalExam.DonorStatus) && this.donorPhysicalExam.DonorStatus !== DonorStatus.Deferred && this.donorPhysicalExam.DonorStatus !== DonorStatus.ForInitialScreening) {
        this.isDisabled = false;
        if (this.donorPhysicalExam.DonorStatus !== DonorStatus.ForPhysicalExamination) {
          this.isEditingValue = true;
        }
      }

      /*this.onRemarksChanged();*/
    }
  }

  //protected onRemarksChanged(): void {
  /*  if (this.donorPhysicalExam.ResultStatus === PhysicalExamResultStatus.TemporaryDeferral || this.donorPhysicalExam.ResultStatus === PhysicalExamResultStatus.PermanentDeferral || this.donorPhysicalExam.ResultStatus === PhysicalExamResultStatus.SelfDeferral || this.donorPhysicalExam.ResultStatus === PhysicalExamResultStatus.IndefiniteDeferral) {*/
  //    this.canAddReason = true;
  //    this.donorPhysicalExam.FailedRemarks = null;
  //  }
  //  else {
  //    this.canAddReason = false;
  //    this.donorPhysicalExam.FailedRemarks = null;
  //  }
  //}

  protected async approveOrMarkDeferred(): Promise<void> {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(this.formValid === false) {
      return;
    }
      this.donorPhysicalExam.DonorStatus = DonorStatus.ForCounseling;
    //if (this.isPassed) {
    //  this.donorPhysicalExam.DonorStatus = DonorStatus.ForBloodCollection;
    //}
    //else {
    //  this.donorPhysicalExam.DonorStatus = DonorStatus.Deferred;
    //  this.donorPhysicalExam.DeferralStatus = this.donorPhysicalExam.ResultStatus === PhysicalExamResultStatus.PermanentDeferral ? 'Permanent' :  'Temporary';
    //  this.donorPhysicalExam.Remarks = this.donorPhysicalExam.FailedRemarks;
    //}

    await this.submit();
  }

  protected async submit(): Promise<void> {
    let loader = this.showLoader();
    try {
      await this.donorScreeningService.upsertPhysicalExamination(this.donorPhysicalExam);
      this.notify_success('Form successfully submitted.');

      this.$router.push(`/donors`);
    }
    catch(error: any) {
      if (error.StatusCode != 500) {
        let errorMessage = error.Message ?? "An error occured while processing your request.";
        this.notify_error(errorMessage);
      }
    }
    finally {
      loader.hide();
    }
  }
}</script>

<style lang="scss" scoped>
    .radio-container-row {
        margin-top: 4px;
    }

    .line-height-40 {
        line-height: 40px !important;
    }

    .section-container-w280 {
        width: 280px;
        display: inline-block;
        vertical-align: middle;
        text-align: left;
        padding: 0;
        margin: 0;
    }

    .section-container-w421 {
        width: 421px;
        display: inline-block;
        vertical-align: middle;
        text-align: left;
        padding: 0;
        margin: 0;
    }
</style>