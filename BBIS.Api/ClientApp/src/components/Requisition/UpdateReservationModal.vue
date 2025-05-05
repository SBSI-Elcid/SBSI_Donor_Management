<template>
  <v-row justify="center">
    <v-dialog v-model="showDialog" persistent max-width="20%">     
      <v-card>
        <v-card-title>
         	<h4 class="pl-1 pt-1">Update Reservation Status</h4>
        </v-card-title>
        <v-divider></v-divider>
        <v-card-text>
          <v-form ref="form" v-model="formValid" lazy-validation>
            <v-container>
              <v-row no-gutters>
                <v-col cols="12">
                  <label class="label-container">Reservation Status</label>
                  <v-autocomplete :items="reservationStatusOptions" v-model="model.Status" :rules="[rules.required]" dense outlined />
                </v-col>
              </v-row>
            </v-container>
          </v-form>     
        </v-card-text>
        <v-divider />
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="default darken-1" text @click="onClose(false)">Cancel</v-btn>
          <v-btn color="primary" text @click="onSave">Save</v-btn>
        </v-card-actions> 
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Emit, Prop } from 'vue-property-decorator';
import Common from '@/common/Common';
import RequisitionsService from '@/services/RequisitionsService';
import { UpdateReservationDto } from '@/models/Requisitions/UpdateReservationDto';
import CommonOptions from '@/common/CommonOptions';
import TestOrderService from '@/services/TestOrderService';

@Component({ components: { } })
export default class UpdateReservationModal extends VueBase {
  @Prop({ required: true }) public toggle!: boolean;
  @Prop({ required: true }) public reservationId!: Guid;
  @Prop({ required: true }) public currentStatus!: string;

	protected get showDialog(): boolean {
    return this.toggle;
  }

  protected requisitionService: RequisitionsService = new RequisitionsService();
  protected testOrderService: TestOrderService = new TestOrderService();
  protected errorMessage: string = '';
  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected reservationStatusOptions: any = CommonOptions.RequisitionStatusOptions;

  protected get model(): UpdateReservationDto {
    return  {
      ReservationId: this.reservationId,
      Status: this.currentStatus
    };
  }

  protected async mounted() : Promise<void> {
    let loader = this.showLoader();

    try {
      let isCompleted: boolean = await this.testOrderService.getTestOrderStatus(this.reservationId);
      if (!isCompleted) {
        this.reservationStatusOptions = [{ text: this.currentStatus, value: this.currentStatus}, { text: 'Cancelled', value: 'Cancelled'}];
      }
    } catch (error) {
      console.log(error);
    } finally {
      loader.hide();
    }
  }

	protected async onSave(): Promise<void> {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    let loader = this.showLoader();
    try {
      await this.requisitionService.updateReservation(this.model);
      this.notify_success('Reservation successfully updated.');
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
  protected onClose(refresh: boolean): boolean {
    (this.$refs.form as Vue & { reset: () => void }).reset();
    return refresh;
  }

}
</script>

<style lang="scss" scoped>
  :deep(.row-radio-group-container) {
    margin-top: 4px;
  }


</style>