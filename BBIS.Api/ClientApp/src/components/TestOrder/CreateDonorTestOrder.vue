<template>
  <v-row justify="center">
    <v-dialog v-model="showDialog" persistent max-width="500px">     
      <v-card>
        <v-card-title>
          <h4 class="pl-3 pt-3">Create Test Order  <span class="subtitle-1">{{ donorName }}</span></h4>
        </v-card-title>
        <v-card-text>
          <v-form ref="form" v-model="formValid" lazy-validation>
            <v-container class="mt-3">
              <v-row no-gutters>
                <v-col cols="12">
                  <label>Select type of test(s) to be done</label>
                  <v-autocomplete 
                    outlined dense multiple chips small-chips 
                    v-model="model.TestTypes" 
                    item-text="TypeName"
                    item-value="BloodTestTypeId"
                    :items="testTypes" 
                    :rules="[rules.arrayRequired]" 
                    required>
                  </v-autocomplete>
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
import VueBase from '../../components/VueBase';
import Common from '@/common/Common';
import TestOrderService from '@/services/TestOrderService';
import { CreateDonorTestOrderDto } from '@/models/TestOrder/CreateDonorTestOrderDto';
import { BloodTestTypeDto } from '@/models/TestOrder/BloodTestTypeDto';

@Component({ components: { } })
export default class CreateDonorTestOrder extends VueBase {
  @Prop({ required: true }) public toggle!: boolean;
  @Prop({ required: true }) public transactionId!: Guid;
  @Prop({ required: true }) public donorName!: string;

  private testOrder: TestOrderService = new TestOrderService();
  private formValid: boolean = true;
  private rules: any = {...Common.ValidationRules }
  private errorMessage: string = '';
  private testTypes: Array<BloodTestTypeDto> = [];
  
  private model: CreateDonorTestOrderDto = new CreateDonorTestOrderDto();

  private get showDialog(): boolean {
    return this.toggle;
  }
  
  private async save(): Promise<void> {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    let loader = this.showLoader();
    try {
      this.model.DonorTransactionId = this.transactionId;
      await this.testOrder.createDonorTestOrder(this.model);
      this.notify_success('Test order successfully created.');
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

  @Emit("onClose") 
  private onClose(refresh: boolean): boolean {
    (this.$refs.form as Vue & { reset: () => void }).reset();
    return refresh;
  }

  private async mounted(): Promise<void> {
    this.testTypes = await this.testOrder.getBloodTestTypes();
    this.model.TestTypes = this.testTypes.map(x => x.BloodTestTypeId);
  }
}
</script>