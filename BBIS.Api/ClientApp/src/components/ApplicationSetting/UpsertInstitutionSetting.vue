<template>
  <v-row justify="center">
    <v-dialog v-model="showDialog" persistent max-width="400px">     
      <v-card>
        <v-card-title>{{ title }}</v-card-title>
        <v-card-text>
          <v-form ref="form" v-model="formValid" lazy-validation>
            <v-container class="mt-3">
              <v-row no-gutters>
                <v-col cols="12">
                  <label>Institution Name</label>
                  <v-text-field outlined dense  v-model="model.Name" :rules="[rules.required, rules.maxLength(100)]" />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Address</label>
                  <v-text-field outlined dense v-model="model.Address" :rules="[rules.required, rules.maxLength(200)]" />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Contact No.</label>
                  <v-text-field outlined dense v-model="model.ContactNo" :rules="[rules.required, rules.maxLength(20)]" />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Email Address</label>
                  <v-text-field outlined dense v-model="model.EmailAddress" :rules="[rules.required, rules.emailFormat, rules.maxLength(80)]" />
                </v-col>
              </v-row>

               <v-row no-gutters>
                <v-col cols="12">
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
import VueBase from '../VueBase';
import { Component, Emit, Prop } from 'vue-property-decorator';
import Common from '@/common/Common';
import ApplicationSettingService from '@/services/ApplicationSettingService';
import { InstitutionDto, IInstitutionDto } from '@/models/ApplicationSetting/InstitutionDto';

@Component({ components: { } })
export default class UpsertInstitutionSetting extends VueBase {
  @Prop({ required: true })
  public toggle!: boolean;

  @Prop()
  public isEdit!: Guid;

  @Prop()
  public id!: Guid;

  protected applicationSettingService: ApplicationSettingService = new ApplicationSettingService();

  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected errorMessage: string = '';

  protected model: IInstitutionDto = new InstitutionDto();

  protected get showDialog(): boolean {
    return this.toggle;
  }

  protected get switchLabel(): string {
    return this.model.IsActive ? 'Active' : 'Inactive';
  }

  protected get title(): string {
    return this.isEdit ? 'Update Institution' : 'Create Institution';
  }

  public async mounted() : Promise<void> {
    if (this.isEdit) {
      await this.loadInstitution(this.id);
    }
  }

  protected async save(): Promise<void> {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    let loader = this.showLoader();
    try {
      await this.applicationSettingService.upsertInstitutionSetting(this.model);
      let message: string = this.isEdit ? 'Institution successfully updated.' : 'Institution successfully created.';
      this.notify_success(message);
      this.onClose(true);
    } catch (error: any) {
      if (error.StatusCode != 500) {
        this.errorMessage = error.Message;
        this.notify_error(this.errorMessage);
      }
    }
    finally {
      loader.hide();
    }
  }

  protected async loadInstitution(id: Guid): Promise<void> {
    let loader = this.showLoader();
    try {
      let institution = await this.applicationSettingService.getInstitutionById(id);
      this.model = {
        InstitutionId: institution.InstitutionId,
        Name: institution.Name,
        Address: institution.Address,
        ContactNo: institution.ContactNo,
        EmailAddress: institution.EmailAddress,
        IsActive: institution.IsActive
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
  private onClose(refresh: boolean): boolean {
    let form: any = this.$refs.form as any;
    if (!Common.isNull(form)) {
      form.resetValidation();
    }
    
    this.model = new InstitutionDto();
    return refresh;
  }
}
</script>