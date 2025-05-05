<template>
  <v-row justify="center">
    <v-dialog v-model="showDialog" persistent max-width="620px">     
      <v-card>
        <v-card-title>Update Patient</v-card-title>
        <v-divider></v-divider>
        <v-card-text>
          <v-form ref="form" v-model="formValid" lazy-validation>
            <v-container class="mt-3">

              <v-row no-gutters>
                <v-col cols="6" class="px-1">
                  <label>Blood Type</label>
                  <v-autocomplete :items="bloodTypesOptions" v-model="model.BloodType" :rules="[rules.required]" dense outlined />
                </v-col>

                <v-col cols="6" class="px-1">
                  <label>Blood Rh</label>
                  <v-autocomplete :items="rhOptions" v-model="model.Rh" :rules="[rules.required]" dense outlined />
                </v-col>
              </v-row>

              <v-row no-gutters>
                 <v-col cols="6" class="px-1">
                  <label>Patient Record No.</label>
                  <v-text-field outlined dense  v-model="model.PatientIdNo" :rules="[rules.required, rules.maxLength(15)]" required></v-text-field>
                </v-col>

                <v-col cols="6" class="px-1">
                  <label>First name</label>
                  <v-text-field outlined dense v-model="model.FirstName" :rules="[rules.required, rules.maxLength(50)]" required></v-text-field>
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="6" class="px-1">
                  <label>Middle name</label>
                  <v-text-field outlined dense v-model="model.MiddleName" :rules="[rules.required, rules.maxLength(50)]" required></v-text-field>
                </v-col>

                <v-col cols="6" class="px-1">
                  <label>Last name</label>
                  <v-text-field outlined dense v-model="model.LastName" :rules="[rules.required, rules.maxLength(50)]" required></v-text-field>
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="6" class="px-1">
                  <label>Date of birth</label>
                  <BirthdatePicker v-model="model.DateOfBirth" :rules="[rules.required]" />
                </v-col>

                <v-col cols="6" class="px-1">
                  <label>Gender</label>
                    <v-autocomplete :items="genderOptions"
                          v-model="model.Gender"
                          :rules="[rules.required]"
                          dense outlined />
                </v-col>
              </v-row>
                         
              <v-row no-gutters>
                <v-col cols="6" class="px-1">
                  <label>Civil Status</label>
                  <v-autocomplete :items="civilStatusOptions"
                          v-model="model.CivilStatus"
                          :rules="[rules.required]"
                          dense outlined />
                </v-col>

                <v-col cols="6" class="px-1">
                  <label>Contact No.</label>
                  <v-text-field outlined dense v-model="model.ContactNumber" :rules="[rules.required, rules.maxLength(25)]" required></v-text-field>
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12" class="px-1">
                  <label>Address</label>
                  <v-textarea outlined rows="4" no-resize dense v-model="model.Address" :rules="[rules.required, rules.maxLength(255)]" required></v-textarea>
                </v-col>
              </v-row>

            </v-container>
          </v-form>     
        </v-card-text>
        <v-divider />
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="default darken-1" text @click="onClose(false)">Cancel</v-btn>
          <v-btn color="primary" text @click="save">Save</v-btn>
        </v-card-actions> 
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script lang="ts">
import PatientService from '@/services/PatientService';
import { Component, Emit, Prop } from 'vue-property-decorator';
import VueBase from '../VueBase';
import CommonOptions from '@/common/CommonOptions';
import Common from '@/common/Common';
import LookupModule from '@/store/LookupModule';
import { getModule } from 'vuex-module-decorators';
import { PatientDto } from '@/models/Patient/PatientDto';
import { LookupKeys } from '@/models/Enums/LookupKeys';
import BirthdatePicker from '@/components/Common/FormInputs/BirthdatePicker.vue';

@Component({ components: { BirthdatePicker } })
export default class UpdatePatient extends VueBase {
  @Prop({ required: true })
  public toggle!: boolean;

  @Prop({ required: true }) 
  public id!: Guid;

  protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
  protected patientService: PatientService = new PatientService();

  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected errorMessage: string = '';
  protected genderOptions: any = CommonOptions.GenderOptions;
  protected model: PatientDto = new PatientDto();
  protected rhOptions: any = CommonOptions.RhOptions;

  protected get showDialog(): boolean {
    return this.toggle;
  }

  protected async loadLookups(): Promise<void> {
    await this.lookupModule.loadLookups([LookupKeys.BloodTypes, LookupKeys.CivilStatus]);
  }
  
  protected get bloodTypesOptions(): Array<{text: string, value: string}> {
    var lookups = this.lookupModule.getOptionsByKey(LookupKeys.BloodTypes);
    return lookups.map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get civilStatusOptions(): Array<{text: string, value: string}> {
    let options = this.lookupModule.getOptionsByKey(LookupKeys.CivilStatus);
    return options.map(x => { return { text: x.Text, value: x.Value} });
  }

  protected async save(): Promise<void> {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    let loader = this.showLoader();
    try {
      let reult = await this.patientService.update(this.model);
      this.notify_success('Patient successfully updated.');
      this.onClose(true);
    } catch (error: any) {
      if (error.StatusCode != 500) {
        this.errorMessage = error.Message;
        this.notify_error(this.errorMessage);
      }
    } finally {
      loader.hide();
    }
  }

  protected async loadPatient(): Promise<void> {
    let loader = this.showLoader();
    try {
      let patient = await this.patientService.getPatientById(this.id);
      this.model = {
        PatientId: patient.PatientId,
        BloodType: patient.BloodType,
        Rh: patient.Rh,
        PatientIdNo: patient.PatientIdNo,
        LastName: patient.LastName,
        FirstName: patient.FirstName,
        MiddleName: patient.MiddleName,
        Gender: patient.Gender,
        CivilStatus: patient.CivilStatus,
        DateOfBirth: patient.DateOfBirth,
        ContactNumber: patient.ContactNumber,
        Address: patient.Address
      };
    } 
    catch (error) {
      console.log(error);
    } 
    finally {
      loader.hide();
    }
  }

  @Emit("onClose") 
  protected onClose(refresh: boolean): boolean {
    (this.$refs.form as Vue & { reset: () => void }).reset();
    return refresh;
  }

  protected async mounted(): Promise<void> {
    await this.loadLookups();
    
    if (this.toggle) {
      this.loadPatient();
    }
  }
}
</script>