<template>
  <v-row justify="center">
    <v-dialog v-model="showDialog" persistent max-width="400px">     
      <v-card>
        <v-card-title> {{ title }} </v-card-title>
        <v-card-text>
          <v-form ref="form" v-model="formValid" lazy-validation>
            <v-container class="mt-3">
              <v-row no-gutters>
                <v-col cols="12">
                  <label>First Name</label>
                  <v-text-field outlined dense  v-model="model.FirstName" :rules="[rules.required, rules.maxLength(50)]" required />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Middle Name</label>
                  <v-text-field outlined dense v-model="model.MiddleName" :rules="[rules.required, rules.maxLength(50)]" required />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Last Name</label>
                  <v-text-field outlined dense v-model="model.LastName" :rules="[rules.required, rules.maxLength(50)]" required />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Position</label>
                  <v-text-field outlined dense v-model="model.Position" :rules="[rules.required, rules.maxLength(100)]" required />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Position Prefix</label>
                  <v-text-field outlined dense v-model="model.PositionPrefix" :rules="[rules.required, rules.maxLength(50)]" required />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>License Number</label>
                  <v-text-field outlined dense v-model="model.LicenseNo" :rules="[rules.required, rules.maxLength(30)]" required />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Is Pathologist?</label>
                  <v-switch outlined dense v-model="model.IsPathologist" :label="switchLabelPathologist" color="primary" hide-details="true"/>
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12" class="pt-3">
                  <label>Status</label>
                  <v-switch outlined dense v-model="model.IsActive" :label="switchLabel" color="primary" hide-details="true"/>
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
import { Component, Emit, Prop } from 'vue-property-decorator';
import VueBase from '../VueBase';
import Common from '@/common/Common';
import SignatoryService from '@/services/SignatoryService';
import SignatoryDto, { ISignatoryDto } from '@/models/Signatories/ISignatoryDto';

@Component({ components: { } })
export default class UpsertSignatory extends VueBase {
  @Prop({ required: true })
  public toggle!: boolean;

  @Prop()
  public isEdit!: Guid;

  @Prop()
  public id!: Guid;

  protected signatoryService: SignatoryService = new SignatoryService();
  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected errorMessage: string = '';
  protected model: ISignatoryDto = new SignatoryDto();

  protected get showDialog(): boolean {    
    return this.toggle;
  }

  protected get switchLabelPathologist(): string {
    return this.model.IsPathologist ? 'Yes' : 'No';
  }

  protected get switchLabel(): string {
    return this.model.IsActive ? 'Active' : 'Inactive';
  }

  protected get title(): string {
    return this.isEdit ? 'Update Signatory' : 'Create Signatory';
  }

  protected async mounted() : Promise<void> {
    if (this.isEdit) {
      await this.loadSignatory();
    }
  }

  protected async save(): Promise<void> {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    let loader = this.showLoader();
    try {
      await this.signatoryService.upsertSignatory(this.model);      
      let message: string = this.isEdit ? 'Signatory successfully updated.' : 'Signatory successfully created.';
      this.notify_success(message);
      this.onClose(true);
    } catch (error: any) {
      if (error.StatusCode != 500) {
        this.errorMessage = error.Message;
        this.notify_error(this.errorMessage);
      }
    } finally {
      loader.hide()
    }
  }

  protected async loadSignatory(): Promise<void> {
    let loader = this.showLoader();
    try {
      let signatory = await this.signatoryService.getSignatory(this.id);
      this.model = {
        SignatoryId: signatory.SignatoryId,
        FirstName: signatory.FirstName,
        MiddleName: signatory.MiddleName,
        LastName: signatory.LastName,
        Position: signatory.Position,
        PositionPrefix: signatory.PositionPrefix,
        LicenseNo: signatory.LicenseNo,
        IsActive: signatory.IsActive,
        IsPathologist: signatory.IsPathologist
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
    let form: any = this.$refs.form as any;
    if (!Common.isNull(form)) {
      form.resetValidation();
    }
    
    this.model = new SignatoryDto();
    return refresh;
  }
}
</script>