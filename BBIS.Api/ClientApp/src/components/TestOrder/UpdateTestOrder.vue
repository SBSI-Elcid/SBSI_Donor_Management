<template>
  <div>
    <v-dialog v-model="showDialog" persistent max-width="850px" v-if="testOrderInfo">     
      <v-card>
        <v-card-title>
          <h4 class="pl-1 pt-1">Update Test Order Result</h4>
        </v-card-title>
        <v-divider></v-divider>
        <v-card-text>
          <v-form ref="form" v-model="formValid" lazy-validation>
            <v-container class="mt-3">
              
              <v-row no-gutters>
                <v-col cols="4" class="pr-1">
                  <v-text-field label="Test Order Number" readonly dense outlined v-model="testOrderInfo.TestOrderNumber" class="font-weight-bold" />
                </v-col> 
                <v-col cols="8">
                  <v-text-field label="Patient Name" readonly dense outlined v-model="testOrderInfo.PatientName" class="font-weight-bold" />
                </v-col> 
              </v-row>

              <!-- Cross match test orders -->
              <v-row no-gutters v-if="groupCrossMatchTestOrders.length > 0">
                <v-col cols="12">
                  <!-- Component Header -->
                  <v-row no-gutters v-for="(item, grpIndex) in groupCrossMatchTestOrders" :key="grpIndex">
                    <v-col cols="12">
                        <v-row no-gutters class="mb-0">
                          <v-col cols="5">
                            <p class="subtitle-1 font-weight-bold mx-0 pa-0"> {{ item.BloodComponentName }} </p>
                          </v-col>
                        </v-row>

                        <v-row no-gutters v-for="(xmatch, index) in item.CrossMatchTestOrders" :key="index">
                          <v-col cols="3" class="pr-2">
                            <v-text-field label="Unit Serial #" readonly dense outlined v-model="xmatch.DonorUnitSerialNumber" />
                          </v-col>

                          <v-col cols="3" class="pr-2">
                            <v-autocomplete
                              label="Cross Match Type"
                              dense
                              outlined
                              :items="crosMatchTypes"
                              v-model="xmatch.CrossMatchType"
                              :rules="[rules.required]" 
                            ></v-autocomplete>
                          </v-col>

                          <v-col cols="3" class="pr-2">
                            <v-text-field placeholder="LISS/AGH" label="LISS/AGH" dense outlined v-model="xmatch.LISS_AGH" />
                          </v-col>

                          <v-col cols="3">
                            <v-autocomplete 
                              label="Result"
                              v-model="xmatch.Result"
                              placeholder="Result"
                              dense
                              outlined
                              :items="compatibilityOptions" 
                              :rules="[rules.required]" 
                              required>
                            </v-autocomplete>
                          </v-col>
                        </v-row>
                     
                    </v-col>
                  
                  </v-row>
                </v-col>
              </v-row>

              <!-- Blood Typing Test Order -->
              <div v-if="testOrderInfo.BloodTypingTestOrder !== null">
                <h3 class="pb-2">Blood Typing Result</h3>
                
                <v-row no-gutters>
                  <v-col cols="6">
                    <p class="subtitle-2 font-weight-bold">Forward Typing</p>
                    <v-row no-gutters >
                      <v-col cols="4" class="pr-1">
                        <v-autocomplete 
                          v-model="testOrderInfo.BloodTypingTestOrder.FTAntiA"
                          @change="onBloodTypingOptionChange"
                          label="Anti A"
                          dense
                          outlined
                          :items="bloodTypingOptions" 
                          :rules="[rules.required]" 
                          required>
                        </v-autocomplete>
                      </v-col>

                      <v-col cols="4" class="pr-1">
                        <v-autocomplete 
                          v-model="testOrderInfo.BloodTypingTestOrder.FTAntiB"
                          @change="onBloodTypingOptionChange"
                          label="Anti B"
                          dense
                          outlined
                          :items="bloodTypingOptions" 
                          :rules="[rules.required]" 
                          required>
                        </v-autocomplete>
                      </v-col>
                
                      <v-col cols="4" class="pr-1">
                        <v-autocomplete 
                          v-model="testOrderInfo.BloodTypingTestOrder.FTAntiD"
                          @change="onBloodTypingOptionChange"
                          label="Anti D"
                          dense
                          outlined
                          :items="bloodTypingOptions" 
                          :rules="[rules.required]" 
                          required>
                        </v-autocomplete>
                      </v-col>
                    </v-row>
                  </v-col>

                  <v-col cols="6" class="pl-3">
                    <p class="subtitle-2 font-weight-bold">Reverse Typing</p>
                    <v-row no-gutters class="mb-n4">
                      <v-col cols="4" class="pr-1">
                        <v-autocomplete 
                          v-model="testOrderInfo.BloodTypingTestOrder.RTACells"
                          @change="onBloodTypingOptionChange"
                          label="A Cells"
                          dense
                          outlined
                          :items="bloodTypingOptions" 
                          :rules="[rules.required]" 
                          required>
                        </v-autocomplete>
                      </v-col>

                      <v-col cols="4" class="pr-1">
                        <v-autocomplete 
                          v-model="testOrderInfo.BloodTypingTestOrder.RTBCells"
                          @change="onBloodTypingOptionChange"
                          label="B Cells"
                          dense
                          outlined
                          :items="bloodTypingOptions" 
                          :rules="[rules.required]" 
                          required>
                        </v-autocomplete>
                      </v-col>
                    </v-row>
                  </v-col>
                </v-row>
                              
                <v-row no-gutters>
                  <v-col cols="6">
                    <p class="subtitle-2 font-weight-bold">Results</p>
                    <v-row no-gutters >
                      <v-col cols="4" class="pr-1">
                        <v-autocomplete 
                          v-model="testOrderInfo.BloodTypingTestOrder.BloodType"
                          label="Blood Type"
                          dense
                          outlined
                          :items="bloodTypeOptions" 
                          :rules="[rules.required]" 
                          required>
                        </v-autocomplete>
                      </v-col>

                      <v-col cols="4" class="pr-1">
                        <v-autocomplete 
                          v-model="testOrderInfo.BloodTypingTestOrder.BloodRh"
                          class="pl-4"
                          label="RH Type"
                          dense
                          outlined
                          :items="posNegOptions" 
                          :rules="[rules.required]" 
                          required>
                        </v-autocomplete>
                      </v-col>
                    </v-row>
                  </v-col>
                </v-row>
              </div>

              <!-- Blood Screening Test Order -->
              <div v-if="testOrderInfo.BloodScreeningTestOrder !== null">
                <v-divider class="pb-3"></v-divider>
                
                <h3 class="pb-3">Blood Screening Result</h3>
                <v-row no-gutters >
                  <v-col cols="2">
                    <v-autocomplete 
                      label="Result"
                      v-model="testOrderInfo.BloodScreeningTestOrder.Result"
                      placeholder="Result"
                      style="max-width: 200px;"
                      dense
                      outlined
                      :items="posNegOptions" 
                      :rules="[rules.required]" 
                      required>
                    </v-autocomplete>
                  </v-col>
                </v-row>
              </div>
             
              <!-- Coombs Test Order -->
              <div v-if="testOrderInfo.CoombsTestOrder !== null">
                <v-divider class="pb-3"></v-divider>

                <h3 class="pb-3">Coombs Test Order Result</h3>
                <v-row no-gutters >
                  <v-col cols="4" class="pr-1">
                    <v-autocomplete 
                      v-model="testOrderInfo.CoombsTestOrder.DATResult"
                      label="Direct Anti Human Globulin (DAT)"
                      style="max-width: 250px;"
                      dense
                      outlined
                      :items="posNegOptions" 
                      :rules="[rules.required]" 
                      required>
                    </v-autocomplete>
                  </v-col>

                  <v-col cols="4">
                    <v-autocomplete 
                      v-model="testOrderInfo.CoombsTestOrder.IATResult"
                      label="Indirect Anti Human Globulin (IAT)"
                      style="max-width: 250px;"
                      dense
                      outlined
                      :items="posNegOptions" 
                      :rules="[rules.required]" 
                      required>
                    </v-autocomplete>
                  </v-col>
                </v-row>
              </div>

              <v-divider class="pb-3"></v-divider>

              <!-- Signatories -->
              <h3 class="pb-3">Signatories</h3>
              <v-row no-gutters>
                <v-col cols="3" class="pr-2">
                  <v-autocomplete 
                    v-model="testOrderInfo.PerformedBy"
                    label="Performed By"
                    item-text="Name"
                    item-value="SignatoryId"
                    dense
                    outlined
                    :items="signatoryOptions" 
                    :rules="[rules.required]" 
                    required>
                  </v-autocomplete>
                </v-col>

                <v-col cols="3" class="pr-2">
                  <v-autocomplete 
                    v-model="testOrderInfo.ReviewedBy"
                    label="Reviewed By"
                      item-text="Name"
                    item-value="SignatoryId"
                    dense
                    outlined
                    :items="signatoryOptions"
                    required>
                  </v-autocomplete>
                </v-col>

                <v-col cols="3" class="pr-2">
                  <v-autocomplete 
                    v-model="testOrderInfo.ValidatedBy"
                    label="Validated By"
                    item-text="Name"
                    item-value="SignatoryId"
                    dense
                    outlined
                    :items="signatoryOptions"
                    required>
                  </v-autocomplete>
                </v-col>

                <v-col cols="3">
                  <v-autocomplete 
                    v-model="testOrderInfo.PathologistId"
                    label="Pathologist"
                    item-text="Name"
                    item-value="SignatoryId"
                    dense
                    outlined
                    :items="pathalogistOptions" 
                    :rules="[rules.required]" 
                    required>
                  </v-autocomplete>
                </v-col>
              </v-row>

              <!-- Remarks -->
              <v-row no-gutters>
                <v-col cols="8">
                  <v-textarea
                    v-model="testOrderInfo.Remarks"
                    label="Additional Remarks"
                    placeholder="Remarks"
                    auto-grow
                    outlined
                    rows="2"
                    row-height="15"
                  ></v-textarea>
                </v-col>
              </v-row>

            </v-container>
          </v-form>     
        </v-card-text>
        <v-divider />
        <v-card-actions>
          <v-btn color="primary" text @click="onSave(true)">Save &amp; Mark as Completed</v-btn>
          <v-spacer></v-spacer>
          <v-btn color="default darken-1" text  @click="onClose(false)">Cancel</v-btn>
          <v-btn color="primary" text @click="onSave(false)">Save</v-btn>
        </v-card-actions> 
      </v-card>
    </v-dialog>
  </div>
</template>

<script lang="ts">
import { Component, Emit, Prop, Watch } from 'vue-property-decorator';
import VueBase from '../VueBase';
import Common from '@/common/Common';
import TestOrderService from '@/services/TestOrderService';
import CommonOptions from '@/common/CommonOptions';
import { TestOrderTypeEnum } from '@/models/TestOrderTypeEnum';
import { TestOrderDto } from '@/models/TestOrder/TestOrderDto';
import { UpdateCrossMatchTestOrderDto } from '@/models/TestOrder/UpdateCrossMatchTestOrderDto';
import { GroupCrossMatchTestOrderViewDto } from '@/models/TestOrder/GroupCrossMatchTestOrderViewDto';
import LookupModule from '@/store/LookupModule';
import { getModule } from 'vuex-module-decorators';
import { LookupKeys } from '@/models/Enums/LookupKeys';
import { UpdateTestOrderDto } from '@/models/TestOrder/UpdateTestOrderDto';
import { SignatoryOptionDto } from '@/models/Signatories/SignatoryOptions';
import SignatoryService from '@/services/SignatoryService';
import { BloodTypingTestOrderDto } from '@/models/TestOrder/BloodTypingTestOrderDto';

@Component({ components: { } })
export default class UpdateTestOrder extends VueBase {
  @Prop({ required: true }) public toggle!: boolean;
  @Prop({ required: true }) public testOrderId!: Guid;

  protected testOrderService: TestOrderService = new TestOrderService();
  protected signatoryService: SignatoryService = new SignatoryService();
  protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected errorMessage: string = '';
  protected posNegOptions: any = CommonOptions.PosNegativeOptions;
  protected compatibilityOptions: any = CommonOptions.CompatibilityOptions;
  protected bloodTypeOptions: any = CommonOptions.BloodTypeOptions;
  protected TestOrderTypeEnum = TestOrderTypeEnum;
  protected testOrderInfo: TestOrderDto | null = null;
  protected crossMatchTestOrders: Array<UpdateCrossMatchTestOrderDto> = [];
  protected groupCrossMatchTestOrders: Array<GroupCrossMatchTestOrderViewDto> = [];
  protected markAsCompleted: boolean = false;
  protected signatoryOptions: Array<SignatoryOptionDto> = [];
  protected pathalogistOptions: Array<SignatoryOptionDto> = [];
  protected bloodTypingOptions: any = CommonOptions.BloodTypingOptions;

  //@Watch('testOrderInfo.BloodTypingTestOrder', {deep: true}) 
  protected onBloodTypingOptionChange(): void {
    if (this.testOrderInfo?.BloodTypingTestOrder === null) {
      return;
    }

    const _zero: string = '0';
    let bloodTyping = this.testOrderInfo?.BloodTypingTestOrder as BloodTypingTestOrderDto;

    if (!bloodTyping.FTAntiA || !bloodTyping.FTAntiB || !bloodTyping.FTAntiD || !bloodTyping.RTACells || !bloodTyping.RTBCells) {
      return;
    }

    bloodTyping.BloodRh = "";
    bloodTyping.BloodType = "";

    // A Positive
    if (bloodTyping.FTAntiA !== _zero && bloodTyping.FTAntiB === _zero && bloodTyping.FTAntiD !== _zero && bloodTyping.RTACells === _zero && bloodTyping.RTBCells !== _zero) {
      bloodTyping.BloodRh = "Positive";
      bloodTyping.BloodType = "A";
    }
    // B Positive
    else if (bloodTyping.FTAntiA === _zero && bloodTyping.FTAntiB !== _zero && bloodTyping.FTAntiD !== _zero && bloodTyping.RTACells !== _zero && bloodTyping.RTBCells === _zero) {
      bloodTyping.BloodRh = "Positive";
      bloodTyping.BloodType = "B";
    }
    // O Positive
    else if (bloodTyping.FTAntiA === _zero && bloodTyping.FTAntiB === _zero && bloodTyping.FTAntiD !== _zero && bloodTyping.RTACells !== _zero && bloodTyping.RTBCells !== _zero) {
      bloodTyping.BloodRh = "Positive";
      bloodTyping.BloodType = "O";
    }
    // AB Positive
    else if (bloodTyping.FTAntiA !== _zero && bloodTyping.FTAntiB !== _zero && bloodTyping.FTAntiD !== _zero && bloodTyping.RTACells === _zero && bloodTyping.RTBCells === _zero) {
      bloodTyping.BloodRh = "Positive";
      bloodTyping.BloodType = "AB";
    }
    // A Neg
    else if (bloodTyping.FTAntiA !== _zero && bloodTyping.FTAntiB === _zero && bloodTyping.FTAntiD === _zero && bloodTyping.RTACells === _zero && bloodTyping.RTBCells !== _zero) {
      bloodTyping.BloodRh = "Negative";
      bloodTyping.BloodType = "A";
    }
    // B Neg
    else if (bloodTyping.FTAntiA === _zero && bloodTyping.FTAntiB !== _zero && bloodTyping.FTAntiD === _zero && bloodTyping.RTACells !== _zero && bloodTyping.RTBCells === _zero) {
      bloodTyping.BloodRh = "Negative";
      bloodTyping.BloodType = "B";
    }
    // O Neg
    else if (bloodTyping.FTAntiA === _zero && bloodTyping.FTAntiB === _zero && bloodTyping.FTAntiD === _zero && bloodTyping.RTACells !== _zero && bloodTyping.RTBCells !== _zero) {
      bloodTyping.BloodRh = "Negative";
      bloodTyping.BloodType = "O";
    }
    // AB Neg
    else if (bloodTyping.FTAntiA !== _zero && bloodTyping.FTAntiB !== _zero && bloodTyping.FTAntiD === _zero && bloodTyping.RTACells === _zero && bloodTyping.RTBCells === _zero) {
      bloodTyping.BloodRh = "Negative";
      bloodTyping.BloodType = "AB";
    }
  }
 
  protected get showDialog(): boolean {
    return this.toggle;
  }

  protected onSave(completed: boolean): void {
    this.markAsCompleted = completed;
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    this.confirm(`This will update test order(s) result, do you want to continue?`, 'Confirm update', 'Update', 'Cancel', this.onSaveConfirm);
  }

  protected async onSaveConfirm(confirm: boolean): Promise<void> {
    if(confirm) {
      await this.save();
    }
  }
  
  protected async save(): Promise<void> {
    let loader = this.showLoader();

    try {
      let to = this.testOrderInfo;
      if (to != null) {
       
        let updateDto: UpdateTestOrderDto = {
          TestOrderId: to.TestOrderId,
          PatientId: to.PatientId,
          ReservationId: to.ReservationId,
          TestCompleted: this.markAsCompleted,
          Remarks: to.Remarks,
          PerformedBy: to.PerformedBy,
          ReviewedBy: to.ReviewedBy,
          ValidatedBy: to.ValidatedBy,
          CoombsTestOrder: to.CoombsTestOrder,
          PathologistId: to.PathologistId,
          BloodScreeningTestOrder: to.BloodScreeningTestOrder,
          BloodTypingTestOrder: to.BloodTypingTestOrder,
          CrossMatchTestOrders: []
        };

        if (this.groupCrossMatchTestOrders.length > 0) {
          for (let i = 0; i < this.groupCrossMatchTestOrders.length; i++) {
            const items = this.groupCrossMatchTestOrders[i].CrossMatchTestOrders;
            const mapItems: Array<UpdateCrossMatchTestOrderDto> = items.map(x => { 
              return { 
                CrossMathTestOrderId: x.CrossMatchTestOrderId,
                DonorUnitSerialNumber: x.DonorUnitSerialNumber,
                CrossMatchType: x.CrossMatchType,
                Result: x.Result,
                LISS_AGH: x.LISS_AGH  
                } 
              });
            updateDto.CrossMatchTestOrders.push(...mapItems);
          }
        } 
        
        await this.testOrderService.updateTestOrder(updateDto);
        this.notify_success('Test order successfully updated.');
        this.onClose(true);
      }
    } catch (error: any) {
      if (error.StatusCode != 500) {
        this.errorMessage = error.Message;
        this.notify_error(this.errorMessage);
      }
    } finally {
      loader.hide();
    }
  }

  protected async loadTestOrder(): Promise<void> {
    try {
      this.testOrderInfo = await this.testOrderService.getTestOrder(this.testOrderId);
      this.groupCrossMatchTestOrders = this.testOrderInfo.GroupCrossMatchTestOrders;
    } 
    catch (error) {
      this.notify_error('Something went wrong retrieving test order.');
    }
  }

  protected async loadSignatoryOptions(): Promise<void> {
    try {
      let signatories = await this.signatoryService.getSignatoryOptions();

      this.signatoryOptions = signatories.filter(x => x.IsPathologist == false);
      this.pathalogistOptions = signatories.filter(x => x.IsPathologist == true);
    } 
    catch (error) {
      this.notify_error('Something went wrong retrieving signatories');
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
      await this.loadSignatoryOptions();
      await this.loadLookups();
      await this.loadTestOrder();
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