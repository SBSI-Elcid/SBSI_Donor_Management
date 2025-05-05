<template>
  <div>
    <v-dialog v-model="showDialog" persistent max-width="800px">     
      <v-card>
        <v-card-title>
          <h4 class="pl-3 pt-3">Create Test Order</h4>
        </v-card-title>
        <v-card-text>
          <v-form ref="form" v-model="formValid" lazy-validation>
            <v-container class="mt-3">
              <v-row no-gutters class="mb-n3">
                <v-col cols="8">
                  <v-text-field label="Patient Name" readonly dense outlined v-model="patient.Name" class="font-weight-bold" />
                </v-col>
                <v-col cols="4" class="pl-2">
                  <v-text-field label="Blood Type" readonly dense outlined v-model="patient.BloodType" class="font-weight-bold" />
                </v-col>
              </v-row>

              <v-checkbox style="display: inline-block;" class="my-4 pb-2" label="Compatibility Test" v-model="addCrossMatchOrder" :hide-details="true"/>
              
              <v-row no-gutters v-if="groupItemsForCrossMatching.length > 0 && addCrossMatchOrder">
                <v-col cols="12">
                  <!-- Component Header -->
                  <v-row no-gutters v-for="(item, grpIndex) in groupItemsForCrossMatching" :key="grpIndex">
                    <v-col cols="12">
                        <v-row no-gutters class="mb-n5">
                          <v-col cols="3">
                            <p class="subtitle-1 font-weight-bold"> {{ item.BloodComponentName }} </p>
                          </v-col>

                          <v-col cols="5" class="pl-2 pr-2">
                            <v-autocomplete
                              label="Cross Match Type"
                              placeholder="Cross Match Type"
                              dense
                              outlined
                              :items="crosMatchTypes"
                              v-model="item.CrossMatchType"
                              :rules="[rules.required]" 
                            ></v-autocomplete>
                          </v-col>
                        </v-row>

                        <v-row no-gutters>
                          <v-col cols="12">
                            <!-- Cross match test order items -->

                            <table class="xmatch-table">
                              <tr>
                                <th class="xmatch-col">Unit Serial Number</th>
                                <th class="xmatch-col">Collection Date</th>
                                <th class="xmatch-col">Expiry Date</th>
                                <th class="xmatch-col">Volume</th>
                              </tr>

                              <tr v-for="(xmatch, index) in item.CrossMatchTestOrders" :key="index">
                                <td class="xmatch-col">{{ xmatch.DonorUnitSerialNumber }}</td>
                                <td class="xmatch-col">{{ formatDate(xmatch.CollectionDate) }}</td>
                                <td class="xmatch-col">{{ formatDate(xmatch.ExpiryDate) }}</td>
                                <td class="xmatch-col">{{ xmatch.Volume }}</td>
                              </tr>
                            </table>
                           
                          </v-col>
                        </v-row>
                      <v-divider class="mt-6 mb-5"></v-divider>
                    </v-col>
                  
                  </v-row>
                </v-col>
              </v-row>

              <p class="pb-1 subtitle-2">Select other type of test(s) to be done:</p>
              <v-row no-gutters v-for="(item, index) in testOrderTypes" :key="index">
                <v-col cols="4">
                  <v-checkbox class="pt-0 mt-0"
                    v-model="selectedOrders"
                    :label="item.text"
                    :value="item.value"
                    color="indigo"
                  ></v-checkbox>
                </v-col>

                <v-col cols="8">
                </v-col>
              </v-row>
            </v-container>
          </v-form>     
        </v-card-text>
        <v-divider />
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="default darken-1" text  @click="onClose(false)">Cancel</v-btn>
          <v-btn color="primary" text @click="onSave">Save</v-btn>
        </v-card-actions> 
      </v-card>
    </v-dialog>
  </div>
</template>

<script lang="ts">
import { Component, Emit, Prop, Watch } from 'vue-property-decorator';
import VueBase from '../../components/VueBase';
import Common from '@/common/Common';
import TestOrderService from '@/services/TestOrderService';
import { TestOrderTypeEnum } from '@/models/TestOrderTypeEnum';
import { CreateTestOrderDto } from '@/models/TestOrder/CreateTestOrderDto';
import { TypeAheadResultDto } from '@/models/TestOrder/TypeAheadResultDto';
import RequisitionsService from '@/services/RequisitionsService';
import LookupModule from '@/store/LookupModule';
import { getModule } from 'vuex-module-decorators';
import { LookupKeys } from '@/models/Enums/LookupKeys';
import { GroupCrossMatchOrderDto } from '@/models/TestOrder/GroupCrossMatchOrderDto';
import moment from 'moment';

@Component({ components: { } })
export default class CreateTestOrder extends VueBase {
  @Prop({ required: true }) public toggle!: boolean;
  @Prop({ required: false, default: null }) public reservationId!: Guid;
  @Prop({ required: false, default: null }) public patient!: TypeAheadResultDto;

  protected testOrder: TestOrderService = new TestOrderService();
  protected requisitionService: RequisitionsService = new RequisitionsService();
  protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected errorMessage: string = '';
  protected testOrderTypes: Array<any> = [];
  protected TestOrderTypeEnum = TestOrderTypeEnum;
  protected selectedOrders: Array<Guid> = [];
  protected testOrderDto: CreateTestOrderDto = new CreateTestOrderDto();
  protected selectedPatient: TypeAheadResultDto | null = null;
  protected groupItemsForCrossMatching: Array<GroupCrossMatchOrderDto> = [];
  protected addCrossMatchOrder: boolean = true;

  protected get showDialog(): boolean {
    return this.toggle;
  }

  protected formatDate(date: Date): string {
    return moment(date).format('MMM DD, yyyy');
  }

  protected onSave(): void {    
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    this.confirm(`Save this test order(s)?`, 'Confirm update', 'Save', 'Cancel', this.onSaveConfirm);
  }

  protected async onSaveConfirm(confirm: boolean): Promise<void> {
    if(confirm) {
      await this.save();
    }
  }
  
  protected async save(): Promise<void> {
    let loader = this.showLoader();
    try {

      if (this.selectedPatient !== null) {
        this.testOrderDto.PatientId = this.selectedPatient.Id;
      }

      this.testOrderDto.ReservationId = this.reservationId;
      
      if (this.selectedOrders.length == 0 && !this.addCrossMatchOrder) {
        this.notify_error('Please select at least 1 Test Order');
        return;
      }
      
      this.testOrderDto.TestOrders = this.selectedOrders;
      this.testOrderDto.CrossMatchTestOrders = [];

      if (this.addCrossMatchOrder && this.groupItemsForCrossMatching.length > 0) {
        for (let i = 0; i < this.groupItemsForCrossMatching.length; i++) {
          for (let j = 0; j < this.groupItemsForCrossMatching[i].CrossMatchTestOrders.length; j++) {
            this.groupItemsForCrossMatching[i].CrossMatchTestOrders[j].CrossMatchType = this.groupItemsForCrossMatching[i].CrossMatchType;
          }      
          
          this.testOrderDto.CrossMatchTestOrders.push(...this.groupItemsForCrossMatching[i].CrossMatchTestOrders);
        }
      }
      
      await this.testOrder.createTestOrder(this.testOrderDto);

      this.notify_success('Test order successfully created.');
      this.onClose(true);
    } 
    catch (error: any) {
      if (error.StatusCode != 500) {
        this.errorMessage = error.Message;
        this.notify_error(this.errorMessage);
      }
    }
    finally {
      loader.hide();
    }
  }

  protected async loadTestOrderTypes(): Promise<Array<{text: string, value: string}>> {
    let types = await this.testOrder.getTestOrderTypes();
    let results: Array<{text: string, value: string}>  = types.map(x => { return { text: x.Name, value: x.TestOrderTypeId } });
    return results;
  }

  protected async loadItemsForCrossMatching(): Promise<void> {
    if(this.reservationId) {
      this.groupItemsForCrossMatching = await this.requisitionService.getItemsForCrossMatching(this.reservationId);
    }
  }

  protected async loadLookups(): Promise<void> {
    await this.lookupModule.loadLookups([LookupKeys.CrossMatchTypes]);
  }
  
  protected get crosMatchTypes(): Array<{text: string, value: string}> {
    var lookups = this.lookupModule.getOptionsByKey(LookupKeys.CrossMatchTypes);
    return lookups.map(x => { return { text: x.Text, value: x.Value} });
  }

  @Emit("onClose") 
  protected onClose(refresh: boolean): boolean {
    (this.$refs.form as Vue & { reset: () => void }).reset();
    return refresh;
  }

  protected async mounted(): Promise<void> {
    let loader = this.showLoader();
    
    try {

      await this.loadLookups();
      if (this.patient) {
        this.selectedPatient = this.patient;
      }

      await this.loadItemsForCrossMatching();
      this.testOrderTypes = await this.loadTestOrderTypes();
    }
    catch (error) {
      console.log(error);
    }
    finally {
      loader.hide();
    }
  }
}
</script>
