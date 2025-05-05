<template>
  <div>
    <v-form class="form-container" ref="form" v-model="formValid" lazy-validation>
      <v-row>
        <v-col cols="2" class="py-0">
          <label class="label-container">Body Weight (kgs)</label>
          <v-text-field type="number"
                        v-model="donorInitialScreening.Weight"
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>

        <v-col cols="2" class="py-0">
          <label class="label-container">Specific Gravity</label>
          <v-text-field type="number"
                        v-model="donorInitialScreening.SPGR"
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>

        <v-col cols="2" class="py-0">
          <label class="label-container">Blood Type</label>
          <v-autocomplete :items="bloodTypesOptions"
                          v-model="donorInitialScreening.BloodType"
                          :disabled="isDisabled"
                          dense outlined />
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">FOR APHERESIS DONOR</label>
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="12" lg="1" md="1" sm="12" class="py-0">
          <label class="label-container">HGB</label>
          <v-text-field type="number"
                        v-model="donorInitialScreening.HGB"
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="1" md="1" sm="12" class="py-0">
          <label class="label-container">HCT</label>
          <v-text-field type="number"
                        v-model="donorInitialScreening.HCT"
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="1" md="1" sm="12" class="py-0">
          <label class="label-container">RBC</label>
          <v-text-field type="number"
                        v-model="donorInitialScreening.RBC"
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="1" md="1" sm="12" class="py-0">
          <label class="label-container">WBC</label>
          <v-text-field type="number"
                        v-model="donorInitialScreening.WBC"
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>
        <v-col cols="12" lg="1" md="1" sm="12" class="py-0">
          <label class="label-container">PLT Count</label>
          <v-text-field type="number"
                        v-model="donorInitialScreening.PLTCount"
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">Type of Donation:</label>
          <v-radio-group class="radio-container-row" v-model="donorInitialScreening.DonationType" 
                         @change="onDonationTypeChange" :disabled="isDisabled" row>
            <v-radio v-for="(type, index) in donationTypesOptions" :key="index" :label="type.text" :value="type.value" />
          </v-radio-group>
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <v-radio-group v-if="showInHouseOptions" class="radio-container-row" v-model="donorInitialScreening.InHouseTypeValue" 
                         @change="onInHouseTypeChange" :disabled="isDisabled">
            <v-radio v-for="(type, index) in inHouseTypesOptions" :key="index" :label="type.text" :value="type.value" />
          </v-radio-group>
        </v-col>
      </v-row>

      <v-row v-if="showPatientDirectedFields">
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">Patient's Name</label>
          <v-text-field v-model="donorInitialScreening.NameOfPatient"
                        :rules="[rules.maxLength(100)]"
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">Hospital</label>
          <v-text-field v-model="donorInitialScreening.PatientHospital"
                        :rules="[rules.maxLength(100)]"
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">Blood Type</label>
          <v-autocomplete :items="bloodTypesOptions"
                          v-model="donorInitialScreening.PatientBloodType"
                          :disabled="isDisabled"
                          dense outlined />
        </v-col>
      </v-row>

      <v-row v-if="showPatientDirectedFields">
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">WB/Component</label>
          <v-text-field v-model="donorInitialScreening.PatientWBOrComponent"
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">No. of units</label>
          <v-text-field v-model="donorInitialScreening.PatientNoOfUnits"
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>
      </v-row>

      <v-row v-if="showMobileBloodDonationFields">
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">Mobile Blood Donation Place</label>
          <v-text-field v-model="donorInitialScreening.MobileBloodDonationPlace"
                        :rules="[rules.maxLength(100)]"
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">Organizer</label>
          <v-text-field v-model="donorInitialScreening.MobileBloodDonationOrganizer"
                        :rules="[rules.maxLength(50)]"
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">Already a Blood Donor?</label>
          <v-radio-group class="radio-container-row" v-model="donorInitialScreening.BloodDonator" 
                         @change="onBloodDonatorChange" :disabled="isDisabled" row>
            <v-radio v-for="(type, index) in BloodDonator" :key="index" :label="type.text" :value="type.value" />
          </v-radio-group>
        </v-col>
      </v-row>

      <v-row v-if="showRecentDonations">
        <v-col cols="12">
          <RecentDonationTable @onChangeLog="onChangeLog" :agencyOptions="agencyOptions" :donorRegistrationId="donorInitialScreening.DonorRegistrationId" :canOnlyView="isEditingValue || isDisabled" />
        </v-col>
      </v-row>

      <div class="section-outer-container mt-3">
        <div class="text-right">
          <v-btn color="default" large tile class="mr-2" v-if="isEditingValue && !isDisabled" @click="onSubmit"><v-icon color="success" size="25" left>mdi-content-save</v-icon> Save</v-btn>
          <v-btn color="default" large tile class="mr-2" v-if="!isEditingValue && !isDisabled" @click="onApprove"><v-icon color="success" size="25" left>mdi-check</v-icon> Approve</v-btn>
          <v-btn color="default" large tile class="mr-2" v-if="!isEditingValue && !isDisabled" @click="onDeferred"><v-icon color="warning" size="25" left>mdi-cancel</v-icon> Mark as Deferred</v-btn>
        </div>
      </div>
    </v-form>
  </div>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Watch } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import DonorModule from '@/store/DonorModule';
import LookupModule from '@/store/LookupModule';
import DonorScreeningService from '@/services/DonorScreeningService';
import Common from '@/common/Common';
import { DonorInitialScreeningDto, IDonorInitialScreeningDto } from '@/models/DonorScreening/DonorInitialScreeningDto';
import { IDonorRecentDonationDto } from '@/models/DonorScreening/DonorRecentDonationDto';
import { ILookupOptions } from '@/models/Lookups/LookupOptions';
import { LookupKeys } from '@/models/Enums/LookupKeys';
import { DonorStatus } from '@/models/Enums/DonorStatus';
import RecentDonationTable from '@/components/DonorScreening/ScreeningForms/RecentDonationTable.vue';

@Component({ components: { RecentDonationTable } })
export default class InitialScreeningForm extends VueBase { 
  protected donorModule: DonorModule = getModule(DonorModule, this.$store);
  protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
  protected donorScreeningService: DonorScreeningService = new DonorScreeningService();

  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected errorMessage: string = '';
  protected showInHouseOptions: boolean = false;
  protected showRecentDonations: boolean = false;
  protected showPatientDirectedFields: boolean = false;
  protected showMobileBloodDonationFields: boolean = false;
  protected donorInitialScreening: IDonorInitialScreeningDto = new DonorInitialScreeningDto();
  protected recentDonations: Array<IDonorRecentDonationDto> = new Array<IDonorRecentDonationDto>();
  protected isEditingValue: boolean = false;
  protected isDisabled: boolean = true;
 
  protected get selectedDonorName(): string {
    return this.donorInitialScreening.DonorName;
  }

  public get options(): (key: string) => Array<ILookupOptions> {
    return (key) => this.lookupModule.getOptionsByKey(key);
  }

  protected get bloodTypesOptions(): Array<{text: string, value: string}> {
    return this.options(LookupKeys.BloodTypes).map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get donationTypesOptions(): Array<{text: string, value: string}> {
    return this.options(LookupKeys.DonationTypes).map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get inHouseTypesOptions(): Array<{text: string, value: string}> {
    return this.options(LookupKeys.InHouseTypes).map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get agencyOptions(): Array<{text: string, value: string}> {
    return this.options(LookupKeys.AgencyTypes).map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get hospitalOptions(): Array<{text: string, value: string}> {
    return this.options(LookupKeys.HospitalTypes).map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get BloodDonator(): Array<{text: string, value: string}> {
    return this.options(LookupKeys.BloodDonator).map(x => { return { text: x.Text, value: x.Value} });
  }

  @Watch("showInHouseOptions")
  public onInHouseSelected(): void {
    if (this.showInHouseOptions === false) {
      this.donorInitialScreening.InHouseTypeValue = "";
    }
  }

  @Watch("showPatientDirectedFields")
  public onPatentDirectedSelected(): void {
    if (this.showPatientDirectedFields === false) {
      this.donorInitialScreening.NameOfPatient = "";
      this.donorInitialScreening.PatientHospital = "";
      this.donorInitialScreening.PatientBloodType = "";
      this.donorInitialScreening.PatientWBOrComponent = "";
      this.donorInitialScreening.PatientNoOfUnits = 0;
    }
  }

  @Watch("showMobileBloodDonationFields")
  public onMobileDonationSelected(): void {
    if (this.showMobileBloodDonationFields === false) {
      this.donorInitialScreening.MobileBloodDonationPlace = "";
      this.donorInitialScreening.MobileBloodDonationOrganizer = "";
    }
  }

  public async created(): Promise<void> {
    let loader = this.showLoader();
    
    try {
      await this.getInitialScreeningInfo();
    }
    catch (error) {
      console.log(error);
    }
    finally {
      loader.hide();
    }
  }

  protected async getInitialScreeningInfo(): Promise<void>  {
    if (this.$route.params.reg_id && typeof (this.$route.params.reg_id) === 'string') {
      let regId = this.$route.params.reg_id;
      this.donorInitialScreening = await this.donorScreeningService.getInitialScreeningInfo(regId);

      this.donorModule.setTransactionId(this.donorInitialScreening.DonorTransactionId);

      // Enable the fields when Donor Status is not deferred.
      if (Common.hasValue(this.donorInitialScreening.DonorStatus) && this.donorInitialScreening.DonorStatus !== DonorStatus.Deferred) {
        this.isDisabled = false;
        if (this.donorInitialScreening.DonorStatus !== DonorStatus.ForInitialScreening) {
          this.isEditingValue = true;
        }
      }
      this.onDonationTypeChange();
      this.onInHouseTypeChange();
      this.onBloodDonatorChange();
    }
  }

  public onDonationTypeChange() : void {
    if (this.donorInitialScreening.DonationType === "InHouse") {
      this.showInHouseOptions = true;

      this.showMobileBloodDonationFields = false;
      this.showPatientDirectedFields = false;
    }
    else if (this.donorInitialScreening.DonationType === "Mobile") {
      this.showMobileBloodDonationFields = true;

      this.showInHouseOptions = false;
      this.showPatientDirectedFields = false;
    }
  }

  public onInHouseTypeChange() : void {
    if (this.donorInitialScreening.InHouseTypeValue === "PatientDirected") {
      this.showPatientDirectedFields = true;
    }
    else {
      this.showPatientDirectedFields = false;
    }
  }

  public onBloodDonatorChange() : void {
    if (this.donorInitialScreening.BloodDonator === "Repeat") {
      this.showRecentDonations = true;
    }
    else if (this.donorInitialScreening.BloodDonator === "Lapsed") {
      this.showRecentDonations = true;
    }
    else {
      this.showRecentDonations = false;
    }
  }

  public onChangeLog(records: Array<IDonorRecentDonationDto>): void {
    this.recentDonations = records.map(x => { return { DonorRecentDonationId: null, Agency: x.Agency, NumberOfDonation: x.NumberOfDonation, DateOfRecentDonation: x.DateOfRecentDonation, PlaceOfRecentDonation: x.PlaceOfRecentDonation }; });
  }

  protected onDeferred(): void {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(this.formValid === false) {
      return;
    }

    this.mark_deferred(`Are you sure you want to tag this donor as deffered?`, 'Mark Donor as Deferred', 'Mark as Deferred', 'Cancel', this.onDeferralConfirmation);
  }

  public async onDeferralConfirmation(confirm: boolean, result: any): Promise<void> {
    if (confirm) {
      this.donorInitialScreening.DonorStatus = DonorStatus.Deferred;
      this.donorInitialScreening.DeferralStatus = result[0].DeferralStatus;
      this.donorInitialScreening.Remarks = result[0].Remarks;
      await this.onSubmit();
    }
  }

  protected onApprove(): void {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(this.formValid === false) {
      return;
    }

    this.confirm(`Are you sure you want to proceed with approving this donor?`, 'Approve Donor', 'Approve', 'Cancel', this.onApprovalConfirmation);
  }

  public async onApprovalConfirmation(confirm: boolean): Promise<void> {
    if (confirm) {
      this.donorInitialScreening.DonorStatus = DonorStatus.ForPhysicalExamination;
      await this.onSubmit();
    }
  }

  public async onSubmit(): Promise<void> {
    let loader = this.showLoader();
    try {
      this.donorInitialScreening.RecentDonations = this.recentDonations;
      await this.donorScreeningService.upsertInitialScreening(this.donorInitialScreening);
      this.notify_success('Form successfully submitted.');

      this.$router.push({path: '/donors'});
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
  
}
</script>

<style lang="scss" scoped>
  .radio-container-row {
    margin-top: 4px;
  }

  table, th, td {
    border: 1px solid rgb(139, 138, 138);
    border-collapse: collapse;
  }
</style>