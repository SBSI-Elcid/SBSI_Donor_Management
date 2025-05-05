<template>
  <div>
    <v-form class="form-container" ref="form" v-model="formValid" lazy-validation>
      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">Blood Bag Used</label>
          <v-autocomplete :items="bloodBagCollectionOptions"
                          v-model="donorBloodCollection.CollectionType"
                          :disabled="isDisabled"
                          dense outlined />
        </v-col>

        <v-col cols="12" lg="9" md="9" sm="12" class="py-3">
          <v-radio-group v-model="donorBloodCollection.CollectionSubType" :disabled="isDisabled" row>
            <v-radio v-for="(type, index) in bloodBagCollectionSubOptions" :key="index" :label="type.text" :value="type.value" />
          </v-radio-group>
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">Start Time</label>
          <v-text-field type="time"
                        v-model="startTime" 
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">End Time</label>
          <v-text-field type="time"
                        v-model="endTime" 
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">Collected Blood Amount ({{ donorBloodCollection.UnitOfMeasurement}} )</label>
          <v-text-field type="number" v-model="donorBloodCollection.CollectedBloodAmount"
                        :disabled="isDisabled"
                        dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">Success?</label>
          <v-radio-group class="row-radio-group-container" v-model="success" :disabled="isDisabled || isEditingValue" row>
            <v-radio label="Yes" :value="true" />
            <v-radio label="No" :value="false" />
          </v-radio-group>
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">Unwell?</label>
          <v-textarea v-model="donorBloodCollection.UnwellReason" 
                      :rules="[rules.maxLength(100)]"
                      :disabled="isDisabled"
                      rows="2" no-resize
                      dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">Medication Given</label>
          <v-textarea v-model="donorBloodCollection.MedicationGiven" 
                      :rules="[rules.maxLength(100)]"
                      :disabled="isDisabled"
                      rows="2" no-resize
                      dense outlined />
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
          <label class="label-container">Unit Serial Number</label>
          <v-text-field v-model="donorBloodCollection.UnitSerialNumber" :disabled="isDisabled" readonly dense outlined />
        </v-col>

        <v-col cols="12" lg="3" md="3" sm="12" class="mt-1">
          <v-btn color="default" class="mt-2 mr-2" @click="onPrint" :disabled="isDisabled"><v-icon left>mdi-barcode</v-icon> Print Barcode</v-btn>
        </v-col>
      </v-row>

      <div class="section-outer-container mt-3">
        <div class="text-right">
          <v-btn color="default" large tile class="mr-2" v-if="!isDisabled" :disabled="success === null" @click="onSubmit"><v-icon color="success" size="25" left>mdi-content-save</v-icon> {{ isEditingValue ? 'Save' : 'Submit'}}</v-btn>
        </div>
      </div>
    </v-form>

     <div id="printBarcode" style="display: none;" >
      <div class="text-center mx-auto" style="max-width: 300px;">
        <img id="barcode" class="pa-1"/>
        <img id="barcode" class="pa-1"/>
        <img id="barcode" class="pa-1"/>
        <img id="barcode" class="pa-1"/>
        <img id="barcode" class="pa-1"/>
        <img id="barcode" class="pa-1"/>
      </div>
    </div>

  </div>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Watch } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import DonorModule from '@/store/DonorModule';
import LookupModule from '@/store/LookupModule';
import DonorScreeningService from '@/services/DonorScreeningService';
import ApplicationSettingService from '@/services/ApplicationSettingService';
import Common from '@/common/Common';
import { DonorBloodCollectionDto, IDonorBloodCollectionDto } from '@/models/DonorScreening/DonorBloodCollectionDto';
import { BloodBagCollections } from '@/models/Enums/BloodBagCollections';
import { ILookupOptions } from '@/models/Lookups/LookupOptions';
import { LookupKeys } from '@/models/Enums/LookupKeys';
import { ApplicationSettingKeys } from '@/models/ApplicationSettingKeys';
import { DonorStatus } from '@/models/Enums/DonorStatus';
import moment from 'moment';
import JsBarcode from 'jsbarcode'

@Component
export default class BloodCollectionForm extends VueBase { 
  protected donorModule: DonorModule = getModule(DonorModule, this.$store);
  protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
  protected donorScreeningService: DonorScreeningService = new DonorScreeningService();
  protected appSettingService: ApplicationSettingService = new ApplicationSettingService()

  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected errorMessage: string = '';

  protected success: boolean | null = null;
  protected generatedSerialNumber: string = '';
  protected startTime: string = '';
  protected endTime: string = '';
  protected donorBloodCollection: IDonorBloodCollectionDto = new DonorBloodCollectionDto();
  protected isEditingValue: boolean = false;
  protected isDisabled: boolean = true;

  public get options(): (key: string) => Array<ILookupOptions> {
    return (key) => this.lookupModule.getOptionsByKey(key);
  }

  protected get bloodBagCollectionOptions(): Array<{text: string, value: string}> {
    return this.options(LookupKeys.BloodBagCollectionTypes).map(x => { return { text: x.Text, value: x.Value} });
  }

  public get bloodBagCollectionSubOptions(): Array<{text: string, value: string}> { 
    if (this.donorBloodCollection.CollectionType === BloodBagCollections.KARMI || this.donorBloodCollection.CollectionType === BloodBagCollections.TERUMO) {
      return this.bloodBagsSizeOptions;
    }
    else if (this.donorBloodCollection.CollectionType === BloodBagCollections.SpecialBag) {
      return this.specialBagOptions;
    }
    else if (this.donorBloodCollection.CollectionType === BloodBagCollections.Apheresis) {
      return this.apheresisBagOptions;
    }
    else {
      return [];
    }
  }

  protected get bloodBagsSizeOptions(): Array<{text: string, value: string}> {
    return this.options(LookupKeys.BloodBagSizeTypes).map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get specialBagOptions(): Array<{text: string, value: string}> {
    return this.options(LookupKeys.SpecialBagTypes).map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get apheresisBagOptions(): Array<{text: string, value: string}> {
    return this.options(LookupKeys.ApheresisBagTypes).map(x => { return { text: x.Text, value: x.Value} });
  }

  @Watch('success')
  protected onSuccessRadioSelectionChange(value: boolean): void {
    this.donorBloodCollection.Success = value;
  }

  protected async created(): Promise<void> {
    let loader = this.showLoader();
    
    try {
      await this.getBloodCollectionInfo();    
    }
    catch (error) {
      console.log(error);
    }
    finally {
      loader.hide();
    }
  }

  protected async getBloodCollectionInfo(): Promise<void>  {
    if (this.$route.params.reg_id && typeof (this.$route.params.reg_id) === 'string') {
      let transactionId = this.$route.params.reg_id;
     
      this.donorBloodCollection = await this.donorScreeningService.getBloodCollectionInfo(transactionId);
      await this.loadUnitMeasureAppSetting();
      this.donorModule.setTransactionId(this.donorBloodCollection.DonorTransactionId);
      
      // Enable the fields when Donor Status is not deferred and not for initial screening and not for physical exam.
      if (Common.hasValue(this.donorBloodCollection.DonorStatus) && this.donorBloodCollection.DonorStatus !== DonorStatus.Deferred 
          && this.donorBloodCollection.DonorStatus !== DonorStatus.ForInitialScreening && this.donorBloodCollection.DonorStatus !== DonorStatus.ForPhysicalExamination) {
        this.isDisabled = false;
        if (this.donorBloodCollection.DonorStatus !== DonorStatus.ForBloodCollection) {
          this.isEditingValue = true;
        }
      }

      let hasBloodCollectionId = Common.hasValue(this.donorBloodCollection.DonorBloodCollectionId);

      // Show the selected value if there is an existing data or the current date and time if donor status is "For Blood Collection" 
      this.startTime = hasBloodCollectionId || this.isDisabled === false ? moment(this.donorBloodCollection.StartTime).format('HH:mm') : '';
      this.endTime = hasBloodCollectionId || this.isDisabled === false ? moment(this.donorBloodCollection.EndTime).format('HH:mm') : '';

      // Show the selected value if there is an existing data else set it to null.
      this.success = hasBloodCollectionId ? this.donorBloodCollection.Success : null;

      this.generatedSerialNumber = this.donorBloodCollection.UnitSerialNumber;
      if(Common.hasValue(this.generatedSerialNumber)) {
        JsBarcode("#barcode", `${this.generatedSerialNumber}`, {
            width: 2,
            height: 60,
            textAlign: 'left'
          });
      }
    }
  }

  protected async loadUnitMeasureAppSetting(): Promise<void> {
    let settings = await this.appSettingService.getApplicationSettingsByKey([ApplicationSettingKeys.BloodCollectionUnitOfMeasure]);
    if (settings.length > 0) {
      this.donorBloodCollection.UnitOfMeasurement = settings[0].SettingValue;
    }
  }

  protected onPrint(): void {
    this.$htmlToPaper('printBarcode');
  }

  protected async onSubmit(): Promise<void> {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(this.formValid === false) {
      return;
    }

    if (this.donorBloodCollection.Success === false) {
      this.donorBloodCollection.DonorStatus = DonorStatus.Deferred;
      this.donorBloodCollection.DeferralStatus = 'Temporary';
    }
    else {
      this.donorBloodCollection.DonorStatus = DonorStatus.ForLaboratoryTest;
    }

    let startTime = this.startTime.split(':');
    let endTime = this.endTime.split(':');

    this.donorBloodCollection.StartTime =  moment({hour: parseInt(startTime[0]), minute: parseInt(startTime[1])}).toDate();
    this.donorBloodCollection.EndTime = moment({hour: parseInt(endTime[0]), minute: parseInt(endTime[1])}).toDate();
    
    let loader = this.showLoader();
    try {
      await this.donorScreeningService.upsertBloodColllection(this.donorBloodCollection);
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
}
</script>

<style lang="scss" scoped>
  .radio-container-row {
    margin-top: 0px;
  }

  .section-container-w365 {
    width: 350px;
    display: inline-block;
    vertical-align: middle;
    text-align: left;
    padding: 0;
    margin: 0;
  }

</style>