<template>
  <v-row justify="center">
    <v-dialog v-model="showDialog" persistent max-width="600px">     
      <v-card>
        <v-card-title>
         <h3 class="pl-3 pt-3">Update Test Order  <span class="subtitle-1">{{ donorName }}</span></h3>
        </v-card-title>
        <v-card-text>
          <v-form ref="form" v-model="formValid" lazy-validation>
            <v-container class="mt-3" v-if="model != null">
              <v-row>
                <v-col cols="5" lg="3" md="3" sm="12" class="py-0">
                  <label class="label-container">Final Blood Type</label>
                  <v-autocomplete :items="bloodTypesOptions"
                                  v-model="model.FinalBloodType"
                                  dense outlined />
                </v-col>
                <v-col cols="5" lg="3" md="3" sm="12" class="py-0">
                  <label class="label-container">Rh</label>
                  <v-autocomplete :items="posNegativeOptions"
                                  v-model="model.BloodRh"
                                  :rules="[rules.required]"
                                  dense outlined />
                </v-col>
              </v-row>
              <v-divider class="mt-1" />
              <br>
              <v-simple-table>
                <template v-slot:default>
                  <tbody>
                    <tr v-for="(item, index) in model.TestTypes" :key="index">
                      <td>{{ item.TestTypeName }}</td>
                      <td> 
                        <v-checkbox :id="`${item.DonorTestOrderTestTypeId}`" v-model="item.IsReactive" label="Is Reactive"></v-checkbox>
                      </td>
                      <td>
                        <v-text-field class="mt-4" placeholder="Remarks" outlined dense v-model="item.Remarks" :rules="[rules.maxLength(70)]" required></v-text-field>
                      </td>
                    </tr>
                  </tbody>
                </template>
              </v-simple-table>
            </v-container>
          </v-form>     
        </v-card-text>
        <v-divider />
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="default darken-1" small @click="onClose(false)">Cancel</v-btn>
          <v-btn color="primary" small @click="onSave">Update</v-btn>
        </v-card-actions> 
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script lang="ts">
import { Component, Emit, Prop } from 'vue-property-decorator';
import VueBase from '../VueBase';
import Common from '@/common/Common';
import TestOrderService from '@/services/TestOrderService';
import { BloodTestTypeDto } from '@/models/TestOrder/BloodTestTypeDto';
import { DonorTestOrderDto } from '@/models/TestOrder/DonorTestOrderDto';
import LookupModule from '@/store/LookupModule';
import { getModule } from 'vuex-module-decorators';
import { LookupKeys } from '@/models/Enums/LookupKeys';
import CommonOptions from '@/common/CommonOptions';

@Component({ components: { } })
export default class UpdateDonorTestOrder extends VueBase {
  @Prop({ required: true })  public toggle!: boolean;
  @Prop({ required: true }) public transactionId!: Guid;
  @Prop({ required: true }) public donorName!: string;

  protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
  protected testOrder: TestOrderService = new TestOrderService();
  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected errorMessage: string = '';
  protected testTypes: Array<BloodTestTypeDto> = [];
  protected bloodTypesOptions: Array<{text: string, value: string}>  = [];
  protected posNegativeOptions: any = CommonOptions.PosNegativeOptions;
  
  protected model: DonorTestOrderDto | null = null;

  protected async getBloodTypes(): Promise<void> {
    var lookups = await this.lookupModule.loadLookups([LookupKeys.BloodTypes]);
    this.bloodTypesOptions = lookups[0].LookupOptions.map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get showDialog(): boolean {
    return this.toggle;
  }

  protected onSave(): void {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    this.confirm(`This will update and mark test order as Completed, do you want to continue?`, 'Confirm update', 'Update', 'Cancel', this.onSaveConfirm);
  }

  protected async onSaveConfirm(confirm: boolean): Promise<void> {
    if(confirm) {
      await this.save(true);
    }
  }
  
  protected async save(markAsCompleted: boolean): Promise<void> {
    let loader = this.showLoader();
    try {
      (this.model as DonorTestOrderDto).TestCompleted = markAsCompleted;
      let testOk: boolean = await this.testOrder.updateDonorTestOrder(this.model as DonorTestOrderDto);
      if (testOk) {
        this.notify_success('Test order is Non-Reactive.');
      }
      else {
        this.notify_warning('Test order is Reactive, Donor will be Deferred.');
      }
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
  protected onClose(refresh: boolean): boolean {
      (this.$refs.form as Vue & { reset: () => void }).reset();
      return refresh;
  }

  protected async mounted(): Promise<void> {
    let loader = this.showLoader();
    try {
      await this.getBloodTypes();
      this.model = await this.testOrder.getDonorTestOrder(this.transactionId);
    } catch (error) {
      console.log(error);
    } finally {
      loader.hide();
    }
  }
}
</script>