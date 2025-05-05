<template>
  <v-row justify="center">
    <v-dialog v-model="showDialog" persistent max-width="400px">     
      <v-card>
        <v-card-title>Create Blood Component</v-card-title>
        <v-card-text>
          <v-form ref="form" v-model="formValid" lazy-validation>
            <v-container class="mt-3">
              <v-row no-gutters>
                <v-col cols="12">
                  <label>Component name</label>
                  <v-text-field outlined dense  v-model="model.ComponentName" :rules="[rules.required, rules.maxLength(60)]" />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Expiry (in days)</label>
                  <v-text-field type="number" outlined dense v-model="model.ExpiryInDays" :rules="[rules.required]" />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Notify On Days Before</label>
                  <v-text-field type="number" outlined dense v-model="model.NotifyOnDaysBefore" :rules="[rules.required]" />
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
import VueBase from '../../components/VueBase';
import { Component, Emit, Prop } from 'vue-property-decorator';
import Common from '@/common/Common';
import ApplicationSettingService from '@/services/ApplicationSettingService';
import { BloodComponentSettingDto, IBloodComponentSettingDto } from '@/models/ApplicationSetting/BloodComponentSettingDto';

@Component({ components: { } })
export default class CreateBloodComponentSetting extends VueBase {
  @Prop({ required: true })
  public toggle!: boolean;

  protected applicationSettingService: ApplicationSettingService = new ApplicationSettingService();

  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected errorMessage: string = '';

  protected model: IBloodComponentSettingDto = new BloodComponentSettingDto();

  protected get showDialog(): boolean {
    return this.toggle;
  }

  protected get switchLabel(): string {
    return this.model.IsActive ? 'Active' : 'Inactive';
  }

  protected async save(): Promise<void> {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    let loader = this.showLoader();
    try {
      await this.applicationSettingService.upsertBloodComponentSetting(this.model);
      this.notify_success('Blood component successfully created.');
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

  @Emit("onClose") 
  private onClose(refresh: boolean): boolean {
    (this.$refs.form as Vue & { reset: () => void }).reset();
    return refresh;
  }
}
</script>