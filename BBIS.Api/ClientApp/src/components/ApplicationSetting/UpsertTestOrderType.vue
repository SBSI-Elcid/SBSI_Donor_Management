<template>
  <v-row justify="center">
    <v-dialog v-model="showDialog" persistent max-width="600px">     
      <v-card>
        <v-card-title>{{ title }}</v-card-title>
        <v-card-text>
          <v-form ref="form" v-model="formValid" lazy-validation>
            <v-container class="mt-3">
              <v-row no-gutters>
                <v-col cols="12">
                  <label>Code</label>
                  <v-text-field v-model="model.Code" :rules="[rules.required, rules.maxLength(10)]" dense outlined />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Name</label>
                  <v-text-field v-model="model.Name" :rules="[rules.required, rules.maxLength(100)]" dense outlined />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Description</label>
                    <v-text-field v-model="model.Description" :rules="[rules.required, rules.maxLength(200)]" dense outlined />
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
import VueBase from '@/components/VueBase';
import { Component, Emit, Prop } from 'vue-property-decorator';
import Common from '@/common/Common';
import ApplicationSettingService from '@/services/ApplicationSettingService';
import { ITestOrderTypeSettingDto, TestOrderTypeSettingDto } from '@/models/ApplicationSetting/ITestOrderTypeSettingDto';

@Component({ components: { } })
export default class UpsertTestOrderType extends VueBase {
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

  protected componentOptions: Array<{text: string, value: string}> = []
  protected model: ITestOrderTypeSettingDto = new TestOrderTypeSettingDto();

  protected get showDialog(): boolean {
    return this.toggle;
  }

  protected get switchLabel(): string {
    return this.model.IsActive ? 'Active' : 'Inactive';
  }

  protected get title(): string {
    return this.isEdit ? 'Update Test Order Type' : 'Create Test Order Type';
  }

  protected async mounted(): Promise<void> {
    if (this.isEdit) {
      await this.loadTestOrderType(this.id);
    }
  }

  protected async save(): Promise<void> {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    let loader = this.showLoader();
    try {
      await this.applicationSettingService.upsertTestOrderType(this.model);
      let message: string = this.isEdit ? 'Test Order Type successfully updated.' : 'Test Order Type successfully created.';
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

  protected async loadTestOrderType(id: Guid): Promise<void> {
    let loader = this.showLoader();
    try {
      let type = await this.applicationSettingService.getTestOrderTypeById(id);
      this.model = {
        TestOrderTypeId: type.TestOrderTypeId,
        Code: type.Code,
        Name: type.Name,
        Description: type.Description,
        IsActive: type.IsActive
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
    
    this.model = new TestOrderTypeSettingDto();
    return refresh;
  }
}
</script>