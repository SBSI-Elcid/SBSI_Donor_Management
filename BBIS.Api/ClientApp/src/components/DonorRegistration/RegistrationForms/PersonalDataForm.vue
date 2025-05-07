<template>
  <div>
    <v-form class="form-container" ref="form" v-model="formValid" lazy-validation>
      <!-- FIRST SECTION: NAME -->
       <v-row class="ml-2 mt-2"><h4>Personal Information</h4><v-divider></v-divider></v-row>
      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12">
          <label class="caption font-weight-medium">* Last Name</label>
          <v-text-field v-model="newDonor.Lastname"
                        :rules="[rules.required, rules.maxLength(90)]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12">
          <label class="caption font-weight-medium">* First Name</label>
          <v-text-field v-model="newDonor.Firstname"
                        :rules="[rules.required, rules.maxLength(90)]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12">
          <label class="caption font-weight-medium">* Middle Name</label>
          <v-text-field v-model="newDonor.Middlename"
                        :rules="[rules.maxLength(90)]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>
      </v-row>

      <!-- SECOND SECTION: BIRTHDATE, AGE, CIVIL STATUS, GENDER -->
      <v-row>
        <v-col cols="12" lg="2" md="2" sm="12" class="py-0">
          <label class="caption font-weight-medium">* Birthdate</label>
          <BirthdatePicker v-model="newDonor.BirthDate" :rules="[rules.required]" :disabled="inReviewPage" />
        </v-col>

        <v-col cols="12" lg="1" md="1" sm="12" class="py-0">
            <label class="caption font-weight-medium">Age</label>
            <v-text-field v-model="newDonor.Age"
                          :disabled="inReviewPage"
                          dense outlined 
                          readonly />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">* Civil Status</label>
          <v-autocomplete :items="civilStatusOptions"
                          v-model="newDonor.CivilStatus"
                          :rules="[rules.required]"
                          :disabled="inReviewPage"
                          dense outlined />
        </v-col>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">* Gender</label>
          <v-radio-group class="row-radio-group-container" 
                         v-model="newDonor.Gender" 
                         :rules="[rules.required]"
                         :disabled="inReviewPage"
                         row>
            <v-radio v-for="(option, index) in genderOptions" :key="index" :label="option.text" :value="option.value" />
          </v-radio-group>
        </v-col>
      </v-row>

      <!-- THIRD SECTION: ADDRESS -->
      <v-row class="ml-2 mt-2"><h4>Current Home Address</h4><v-divider></v-divider></v-row>
      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">* House Number</label>
          <v-text-field v-model="newDonor.AddressNo"
                          :rules="[rules.maxLength(20)]"
                          :disabled="inReviewPage"
                          dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">* Street</label>
          <v-text-field v-model="newDonor.AddressStreet"
                        :rules="[rules.maxLength(50), rules.required]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">* Barangay</label>
          <v-text-field v-model="newDonor.AddressBarangay"
                        :rules="[rules.maxLength(60), rules.required]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>
      </v-row>

      <!-- THIRD SECTION: ADDRESS -->
      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">* Town/Municipality</label>
          <v-text-field v-model="newDonor.AddressMunicipality"
                        :rules="[rules.maxLength(50), rules.required]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">* Province/City</label>
          <v-text-field v-model="newDonor.AddressProvinceOrCity"
                        :rules="[rules.maxLength(70), rules.required]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">* Zip Code</label>
          <v-text-field v-model="newDonor.AddressZipcode"
                        :rules="[rules.maxLength(10), rules.required]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>
      </v-row>
      
      <!-- FOURTH SECTION: OFFICE ADDRESS, OCCUPATION -->
      <v-row class="ml-2 mt-2"><h4>Other Information</h4><v-divider></v-divider></v-row>
      <v-row>
        <v-col cols="12" lg="6" md="6" sm="12" class="py-0">
          <label class="caption font-weight-medium">Office Address</label>
          <v-text-field v-model="newDonor.OfficeAddress"
                        :rules="[rules.maxLength(250)]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">Profession</label>
          <v-text-field v-model="newDonor.WorkName"
                        :rules="[rules.maxLength(90)]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>
      </v-row>

      <!-- FIFTH SECTION: RELIGION, BATIONALITY, EDUCATION ATTAINMENT -->
      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">Religion</label>
          <v-text-field v-model="newDonor.Religion"
                        :rules="[rules.maxLength(60)]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">* Nationality</label>
            <v-autocomplete v-model="newDonor.Nationality" auto-select-first
                :items="nationalityOptions"
                :disabled="inReviewPage"
                :rules="[rules.required]"
                :item-value="newDonor.Nationality[0]"
                dense outlined/>
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">Education</label>
          <v-text-field v-model="newDonor.Education"
                        :rules="[rules.maxLength(70)]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>
      </v-row>

      <!-- SIXTH SECTION: CONTACT INFORMATION -->
      <v-row class="ml-2 mt-2"><h4>Contact Information</h4><v-divider></v-divider></v-row>
      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">Telephone No.</label>
          <v-text-field v-model="newDonor.TelNo"
                        :rules="[rules.maxLength(30)]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">Mobile No.</label>
          <v-text-field v-model="newDonor.MobileNo"
                        :rules="[rules.maxLength(20), rules.required]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">Email Address</label>
          <v-text-field v-model="newDonor.EmailAddress"
                        :rules="[rules.emailFormat, rules.maxLength(60)]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>
      </v-row>

      <!-- SEVENTH SECTION: IDENTIFICATION NO -->
      <v-row><h4>* Provide Atleast One (1) Valid ID</h4><v-divider></v-divider></v-row>
      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">School ID No.</label>
          <v-text-field v-model="newDonor.SchoolIdNo"
                        :rules="[rules.maxLength(20)]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">Company ID No.</label>
          <v-text-field v-model="newDonor.CompanyIdNo"
                        :rules="[rules.maxLength(20)]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">PRC ID No.</label>
          <v-text-field v-model="newDonor.PRCNo"
                        :rules="[rules.maxLength(30)]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>
      </v-row>

      <!-- EIGTH SECTION: IDENTIFICATION NO CONTINUATION -->
      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">Driver's License No.</label>
          <v-text-field v-model="newDonor.DriverLicenseNo"
                        :rules="[rules.maxLength(30)]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">SSS/GSIS/BIR No.</label>
          <v-text-field v-model="newDonor.SssGsisBirNo"
                        :rules="[rules.maxLength(40)]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="caption font-weight-medium">Others</label>
          <v-text-field v-model="newDonor.OtherNo"
                        :rules="[rules.maxLength(20)]"
                        :disabled="inReviewPage"
                        dense outlined />
        </v-col>
      </v-row>
      <v-divider></v-divider>
      <!-- BUTTONS -->
      <!--<div class="section-outer-container text-right mt-2 pb-2"> 
        <v-btn color="default" large class="mt-2 mr-2" @click="checkForNext">Next <v-icon size="25" color="primary" right>mdi-chevron-right</v-icon></v-btn>
      </div>-->
      <div class="section-outer-container text-right pt-3 pb-2">
          <v-btn v-if="!inReviewPage" color="primary" large class="mr-2 pa-2" @click="checkForSubmit"><v-icon size="25" left>mdi-content-save</v-icon>{{submitLabel}}</v-btn>
      </div>

    </v-form>
  </div>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Emit, Watch, Prop } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import moment from 'moment';
import DonorModule from '@/store/DonorModule';
import LookupModule from '@/store/LookupModule';
import { IDonorDto } from '@/models/DonorRegistration/DonorDto';
import Common from '@/common/Common';
import CommonOptions from '@/common/CommonOptions';
import { LookupKeys } from '@/models/Enums/LookupKeys';
import BirthdatePicker from '@/components/Common/FormInputs/BirthdatePicker.vue';
import { ILookupOptions } from '@/models/Lookups/LookupOptions';

@Component({ components: { BirthdatePicker } })
export default class PersonalData extends VueBase {
  @Prop({ required: true, default: false })
  public inReviewPage!: boolean;

  @Prop({ required: false, default: false })
  public inProcess!: boolean;

    protected get submitLabel(): string {
        return this.inProcess ? "Update and Set for Screening" : "I Agree and Submit";
    }

  protected donorModule: DonorModule = getModule(DonorModule, this.$store);
  protected lookupModule: LookupModule = getModule(LookupModule, this.$store);

  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected genderOptions: any = CommonOptions.GenderOptions;

  protected get newDonor(): IDonorDto {
    return this.donorModule.getDonorInformation;
  }
  
  protected get civilStatusOptions(): Array<{text: string, value: string}> {
    let options = this.lookupModule.getOptionsByKey(LookupKeys.CivilStatus);
    return options.map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get nationalityOptions(): Array<{text: string, value: string}> {
    let options = this.lookupModule.getOptionsByKey(LookupKeys.Nationalities);
    return options.map(x => { return { text: x.Text, value: x.Value} });
  } 

  protected checkForNext(): void {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(this.checkForIdentification() === false) {
      this.notify_error("Please enter at least 1 ID number.");
    }
    else if (this.formValid) {
      this.onNext();
    }

    return;
  }

  protected checkForIdentification(): boolean {
    if (Common.hasValue(this.newDonor.SchoolIdNo) || Common.hasValue(this.newDonor.CompanyIdNo) || Common.hasValue(this.newDonor.PRCNo) || Common.hasValue(this.newDonor.DriverLicenseNo) 
          || Common.hasValue(this.newDonor.SssGsisBirNo) || Common.hasValue(this.newDonor.OtherNo)) {
      return true;
    }

    return false;
  }

  @Watch('newDonor.BirthDate')
  protected onChangeModelBirthDate(): void {
    this.calculateAge();
  }
 
  protected calculateAge(): void {
    let years = moment().diff(moment(this.newDonor.BirthDate, 'YYYY-MM-DD'), 'years');
    this.newDonor.Age = years;
  }

  //@Emit('goToStep')
  //public onNext(): number | void {
  //  this.donorModule.setDonorInformation(this.newDonor);
  //  return 2;
    //}

    protected checkForSubmit(): void {
        this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
        if (this.formValid) {
            this.donorModule.setDonorInformation(this.newDonor);
            this.onSubmit();
        }

        return;
    }

    @Emit('submit')
    public onSubmit(): void {
        //let donorMedicalHistory: Array<IDonorMedicalHistoryDto> = this.donorMedicalHistories.map(x => x.donorMedHistory);
        //this.newDonor.MedicalHistories = donorMedicalHistory;
        this.donorModule.setDonorInformation(this.newDonor);
    }

  protected mounted(): void {
    this.calculateAge();
  }

}
</script>

<style lang="scss" scoped>
  :deep(.section-container-w225) {
    width: 225px;
    display: inline-block;
    vertical-align: middle;
    text-align: left;
    padding: 0;
    margin: 0;
  }

  :deep(.section-container-w125) {
    width: 125px;
    display: inline-block;
    vertical-align: middle;
    text-align: left;
    padding: 0;
    margin: 0;
  }

  :deep(.row-radio-group-container) {
    margin-top: 2px;
  }

</style>