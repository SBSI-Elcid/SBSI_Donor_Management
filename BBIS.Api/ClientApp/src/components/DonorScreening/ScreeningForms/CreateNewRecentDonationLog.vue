<template>
  <v-row justify="center">
    <v-dialog v-model="showDialog" persistent max-width="400px">     
      <v-card>
        <v-card-title>Create Donation Log</v-card-title>
        <v-card-text>
          <v-form ref="form" v-model="formValid" lazy-validation>
            <v-container class="mt-3">
              <v-row no-gutters>
                <v-col cols="12">
                  <label>Agency</label>
                  <v-autocomplete :items="agencyOptions" v-model="model.Agency" dense outlined />
                </v-col>
              </v-row>
              
              <v-row no-gutters>
                <v-col cols="12">
                  <label>Place of the Recent Donation</label>
                  <v-text-field outlined dense v-model="model.PlaceOfRecentDonation" :rules="[rules.required, rules.maxLength(90)]" />
                  <v-autocomplete :items="hospitalOptions" v-model="model.PlaceOfRecentDonation" :rules="[rules.required]" dense outlined />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Number of Donation</label>
                  <v-text-field type="number" outlined dense v-model="model.NumberOfDonation" :rules="[rules.required]" />
                </v-col>
              </v-row>

              <v-row no-gutters>
                <v-col cols="12">
                  <label>Date of Recent Donation</label>
                  <v-text-field type="date" outlined dense v-model="model.DateOfRecentDonation" :rules="[rules.required]" />
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
import { Component, Prop } from 'vue-property-decorator';
import Common from '@/common/Common';
import VueBase from '@/components/VueBase';
import { DonorRecentDonationDto, IDonorRecentDonationDto } from '@/models/DonorScreening/DonorRecentDonationDto';

@Component({ components: { } })
export default class CreateNewRecentDonationLog extends VueBase {
  @Prop({ required: true })
  public toggle!: boolean;

  @Prop({ required: true })
  public agencyOptions!: boolean;
  public hospitalOptions!: boolean;

  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected errorMessage: string = '';

  protected model: IDonorRecentDonationDto = new DonorRecentDonationDto();

  protected get showDialog(): boolean {
    return this.toggle;
  }

  protected async save(): Promise<void> {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    let loader = this.showLoader();
    try {
      this.onClose(true, this.model);
    } catch (error: any) {
      if (error.StatusCode != 500) {
        this.errorMessage = error.Message;
        this.notify_error(this.errorMessage);
      }
    } finally {
      loader.hide();
    }
  }

  protected onClose(refresh: boolean, newLog: IDonorRecentDonationDto) {
    this.$emit('onClose', refresh, newLog);
    (this.$refs.form as Vue & { reset: () => void }).reset();
  }
}
</script>