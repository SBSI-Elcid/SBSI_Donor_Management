<template>
  <v-row justify="center">
    <v-dialog v-model="showDialog" persistent max-width="70%">     
      <v-card>

        <v-row no-gutters class="pa-4">
          <v-col cols="8">
            <h3 class="pt-4">Add to inventory   
              <span class="ml-2 subtitle-2">{{ donorName }}</span>
            </h3>
          </v-col>
          <v-col cols="4" class="text-right pr-2">
            <span class="display-2 bloodtype" v-if="itemsToSave.length > 0">
              <strong>{{ itemsToSave[0].BloodType }}</strong>
              <strong class="display-1">{{ bloodRhSign(itemsToSave[0].BloodRh) }}</strong>
            </span>
          </v-col>
        </v-row>
        
        <v-divider></v-divider>
        <v-card-text>
          <v-form ref="form" v-model="formValid" lazy-validation>
            <v-container>
              <br>
              <v-simple-table>
                <template v-slot:default>
                  <thead>
                    <tr>
                      <th class="text-left"> Component </th>
                      <th class="text-left"> Collection Date </th>
                      <th class="text-left"> Volume </th>
                       <th class="text-left"> UM </th>
                      <th class="text-left"> Expiry Date </th>
                      <th class="text-left"> Notify Before Expiry </th>
                      <th class="text-left"> Actions </th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="(item, index) in itemsToSave" :key="index">
                      <td>{{ item.BloodComponentName }}</td>
                      <td>{{ formatDate(item.CollectionDate) }}</td>
                      <td> 
                        <v-text-field class="mt-4" type="number" outlined dense v-model="item.Volume" :rules="[rules.minValue(1)]" required style="width: 80px;"></v-text-field>
                      </td>
                       <td>{{ item.UnitMeasure }}</td>
                      <td class="pt-4">
                        <DatePicker :rules="[rules.required]" v-model="item.ExpiryDate"   />
                      </td>
                      <td class="pt-4">
                        <DatePicker :rules="[rules.required]" :value="item.NotifyBeforeExpireOn"  />
                      </td>
                      <td>
                        <v-btn icon @click="onItemDelete(item)"><v-icon>mdi-delete-outline</v-icon></v-btn>
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
          <v-btn icon color="primary" @click="resetList" v-if="inventoryItems.length !== itemsToSave.length"><v-icon>mdi-refresh</v-icon></v-btn>
          <v-spacer></v-spacer>
          <v-btn color="default darken-1" @click="onClose(false)"><v-icon left>mdi-cancel</v-icon>Cancel</v-btn>
          <v-btn color="primary" :disabled="disableSaveButton" @click="onSave"><v-icon left>mdi-database-plus</v-icon>Save</v-btn>
        </v-card-actions> 
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script lang="ts">
import { Component, Emit, Prop } from 'vue-property-decorator';
import VueBase from '../VueBase';
import Common from '@/common/Common';
import { AddInventoryDto } from '@/models/Inventory/AddInventoryDto';
import { InventoryItemDto } from '@/models/Inventory/InventoryItemDto';
import InventoryService from '@/services/InventoryService';
import DatePicker from '@/components/Common/FormInputs/DatePicker.vue';
import moment from 'moment';

@Component({ components: { DatePicker } })
export default class AddToInventory extends VueBase {
  @Prop({ required: true }) public toggle!: boolean;
  @Prop({ required: true }) public transactionId!: Guid;
  @Prop({ required: true }) public donorName!: string;
 
  protected inventoryService: InventoryService = new InventoryService();
  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected errorMessage: string = '';
  protected model: AddInventoryDto | null = null;
  protected inventoryItems: Array<InventoryItemDto> = [];
  protected itemsToSave: Array<InventoryItemDto> = [];
  protected dateModel = moment(new Date()).format('DD-MM-YYYY');


  protected bloodRhSign(rh: string): string {
    if (!rh) {
      return '';
    }

    return rh === 'Positive' ? '+' : '-';
  }

  protected get showDialog(): boolean {
    return this.toggle;
  }

  protected get disableSaveButton(): boolean {
    return this.itemsToSave.length === 0;
  }

  protected onSave(): void {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    this.confirm(`This will be added to the inventory and cannot be undone. Do you want to continue?`, 'Confirm', 'Save', 'Cancel', this.onSaveConfirm);
  }

  protected async onSaveConfirm(confirm: boolean): Promise<void> {
    if(confirm) {
      await this.save(true);
    }
  }

  protected resetList(): void {
    this.itemsToSave = this.inventoryItems;
  }

  protected onItemDelete(item: InventoryItemDto) : void {
    this.itemsToSave = this.itemsToSave.filter(x => x.BloodComponentId != item.BloodComponentId);
  }
  
  protected async save(markAsCompleted: boolean): Promise<void> {
    let loader = this.showLoader();
    try {
      this.model =  {
        DonorTranctionId: this.transactionId,
        InstitutionId: null,
        InventoryItems: this.itemsToSave,
      }

      await this.inventoryService.addToInventory(this.model);

      this.notify_success('Inventory items was successfully added.');
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
      this.inventoryItems = await this.inventoryService.prepareInventoryItems(this.transactionId);
      this.itemsToSave = this.inventoryItems;
    }
    catch (error) {
      console.log(error) 
    }
    finally {
      loader.hide();
    }
  }
}
</script>

<style scoped>
  .bloodtype {
    color: red;
  }
</style>