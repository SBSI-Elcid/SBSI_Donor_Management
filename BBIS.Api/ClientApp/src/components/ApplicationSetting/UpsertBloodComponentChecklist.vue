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
                  <v-checkbox v-model="model.IsAdult" label="For Adult" :value="true" dense outlined />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Blood Component</label>
                  <v-autocomplete :items="componentOptions" v-model="model.BloodComponentId" :rules="[rules.required]" dense outlined />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Description</label>
                    <v-textarea v-model="model.ChecklistDescription" :rules="[rules.required, rules.maxLength(500)]" rows="5" counter="500" no-resize dense outlined />
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
import { Component, Emit, Prop, Watch } from 'vue-property-decorator';
import Common from '@/common/Common';
import ApplicationSettingService from '@/services/ApplicationSettingService';
import { BloodComponentChecklistDto, IBloodComponentChecklistDto } from '@/models/ApplicationSetting/BloodComponentChecklistDto';
import { IBloodComponentDto } from '@/models/ApplicationSetting/IBloodComponentDto';

@Component({ components: { } })
export default class UpsertBloodComponentChecklist extends VueBase {
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
  protected model: IBloodComponentChecklistDto = new BloodComponentChecklistDto();

  protected async mounted(): Promise<void> {
    if (this.toggle) {
      let loader = this.showLoader();
      
      try {
        await this.loadBloodComponentsOptions();
        
        if (this.isEdit) {
          await this.loadChecklist(this.id);
        }
      } 
      catch (error) {
        console.log(error);
      }
      finally {
        loader.hide();
      }
    }
  }

  protected get showDialog(): boolean {
    return this.toggle;
  }

  protected get switchLabel(): string {
    return this.model.IsActive ? 'Active' : 'Inactive';
  }

  protected get title(): string {
    return this.isEdit ? 'Update Blood Component Checklist' : 'Create Blood Component Checklist';
  }

  protected async save(): Promise<void> {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    let loader = this.showLoader();
    try {
      await this.applicationSettingService.upsertBloodComponentChecklist(this.model);
      let message: string = this.isEdit ? 'Checklist successfully updated.' : 'Checklist successfully created.';
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

  protected async loadChecklist(id: Guid): Promise<void> {
    let checklist = await this.applicationSettingService.getBloodComponentChecklistById(id);
    this.model = {
      BloodComponentChecklistId: checklist.BloodComponentChecklistId,
      BloodComponentId: checklist.BloodComponentId,
      ChecklistDescription: checklist.ChecklistDescription,
      IsActive: checklist.IsActive,
      IsAdult: checklist.IsAdult
    };
  }

  protected async loadBloodComponentsOptions(): Promise<void> {
    let components: Array<IBloodComponentDto> = await this.applicationSettingService.getBloodComponents();
    if (Common.hasValue(components) && components.length > 0) {
      this.componentOptions = components.map(x => { return { text: x.ComponentName, value: x.BloodComponentId }});
    }
  }

  @Emit("onClose") 
  private onClose(refresh: boolean): boolean {
    let form: any = this.$refs.form as any;
    if (!Common.isNull(form)) {
      form.resetValidation();
    }
    
    this.model = new BloodComponentChecklistDto();
    return refresh;
  }
}
</script>