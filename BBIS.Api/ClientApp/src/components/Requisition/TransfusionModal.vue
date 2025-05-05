<template>
  <v-row justify="center">
    <v-dialog v-model="showDialog" persistent max-width="60%">     
      <v-card>
        <v-card-title>
         	<h3 class="pl-1 pt-1">Post Transfusion</h3>
        </v-card-title>
        <v-divider></v-divider>
        <v-card-text>
          <v-form ref="form" v-model="formValid" lazy-validation>
            <v-container>
               <v-card class="my-2" max-width="100%">
                <v-card-text>
                  <div>Patient Details</div>
                  <v-row>
                    <v-col cols="10">
                      <span class="text-h6 text--primary">
                        {{transfusionDetail.PatientDetails.FullName}}
                      </span>
                    </v-col>
                    <v-col cols="2" class="text-right">
                      <span class="text-h4 red--text">
                        {{transfusionDetail.PatientDetails.BloodType}}{{transfusionDetail.PatientDetails.BloodRh === 'Positive' ? '+' : '-'}}
                      </span>
                    </v-col>
                  </v-row>

                  <v-row no-gutters>
                    <v-col cols="4">
                      <label class="label-container">Age:</label>
                      <span class="font-weight-bold ma-1">{{transfusionDetail.PatientDetails.Age}} </span> 
                    </v-col>
                    <v-col cols="4">
                      <label class="label-container">Gender:</label>
                      <span class="font-weight-bold ma-1">{{transfusionDetail.PatientDetails.Gender}} </span> 
                    </v-col>
                    <v-col cols="4">
                      <label class="label-container">Patient No:</label>
                      <span class="font-weight-bold ma-1">{{transfusionDetail.PatientDetails.PatientNo}} </span> 
                    </v-col>
                  </v-row>

                  <v-row no-gutters>
                    <v-col cols="4">
                      <label class="label-container">Requesting Physician:</label>
                      <span class="font-weight-bold ma-1">{{transfusionDetail.PatientDetails.RequestingPhysician}} </span> 
                    </v-col>
                    <v-col cols="4">
                      <label class="label-container">Ward/Room No:</label>
                      <span class="font-weight-bold ma-1">{{transfusionDetail.PatientDetails.RoomNo}} </span> 
                    </v-col>
                    <v-col cols="4">
                      <label class="label-container">Membership:</label>
                      <span class="font-weight-bold ma-1">{{transfusionDetail.PatientDetails.Membership}} </span> 
                    </v-col>
                  </v-row>

                  <v-row no-gutters>
                    <v-col cols="4">
                      <label class="label-container">Previous Transfusion Date:</label>
                      <span class="font-weight-bold ma-1">{{formatDate(transfusionDetail.PatientDetails.PreviousTransfusionDate)}} </span> 
                    </v-col>
                    <v-col cols="4">
                      <label class="label-container">Previous No of Units Transfused:</label>
                      <span class="font-weight-bold ma-1">{{transfusionDetail.PatientDetails.PreviousNoOfUnitsTransfused}} </span> 
                    </v-col>
                  </v-row>
                </v-card-text>
              </v-card>

              <v-expansion-panels class="mt-5">
                <v-expansion-panel v-for="(component, index) in transfusionDetail.BloodComponentDetails" :key="index">
                  <v-expansion-panel-header>
                    <v-row no-gutters>
                      <v-col cols="4">
                        {{component.ComponentName}}
                      </v-col>
                      <v-col cols="3">
                        <label class="label-container">Volume:</label>
                        <span class="font-weight-bold ma-1">{{component.Volume}} {{component.UnitMeasure}}</span>
                      </v-col>
                      <v-col cols="3">
                        <label class="label-container">Expiration Date:</label>
                        <span class="font-weight-bold ma-1">{{formatDate(component.ExpirationDate)}}</span>
                      </v-col>
                      <v-col cols="2" class="text-right">
                        <span class="font-weight-bold red--text font-20p">{{component.ComponentBloodType}}{{component.ComponentBloodRh === 'Positive' ? '+' : '-'}}</span>
                      </v-col>
                    </v-row>
                  </v-expansion-panel-header>
                  <v-expansion-panel-content>
                    <v-row no-gutters>
                      <v-col cols="3">
                        <span class="font-weight-bold">Unit Serial #: {{component.UnitSerialNumber}}</span>
                      </v-col>
                    </v-row>
                    <v-row no-gutters>
                      <v-col cols="4" class="pr-2">
                        <label class="label-container">Transfusion Started:</label>
                        <v-text-field type="datetime-local" v-model="component.Tranfusion.TransfusionStarted" :placeHolder="'Transfusion Started'" :rules="[rules.required]" @change="onChangeTransfusionDates(component.Tranfusion)" :disabled="isView" dense outlined />
                      </v-col>
                      <v-col cols="4" class="pr-2">
                        <label class="label-container">Transfusion Ended:</label>
                        <v-text-field type="datetime-local" v-model="component.Tranfusion.TransfusionEnded" :placeHolder="'Component Consumed'" :rules="[rules.required]" @change="onChangeTransfusionDates(component.Tranfusion)" :disabled="isView" dense outlined />
                      </v-col>
                      <v-col cols="4" class="pr-2">
                        <label class="label-container">Total Time unit was consumed:</label>
                        <v-text-field v-model="component.Tranfusion.TotalTimeConsumed" placeholder="Total Time unit was Consumed" :disabled="isView" readonly dense outlined></v-text-field>
                      </v-col>
                    </v-row>
                    <v-row no-gutters>
                      <v-col cols="4" class="pr-2">
                        <v-text-field v-model="component.Tranfusion.MedicationGiven" label="Medication given during transfusion" :rules="[rules.maxLength(100)]" :disabled="isView" dense outlined></v-text-field>
                      </v-col>
                      <v-col cols="4" class="pr-2">
                        <v-text-field v-model="component.Tranfusion.HookedBy" label="Blood Unit Hooked by" :rules="[rules.required, rules.maxLength(100)]" :disabled="isView" dense outlined></v-text-field>
                      </v-col>
                      <v-col cols="4" class="pr-2">
                        <v-text-field v-model="component.Tranfusion.RemovedBy" label="Blood Unit Removed by" :rules="[rules.required, rules.maxLength(100)]" :disabled="isView" dense outlined></v-text-field>
                      </v-col>
                    </v-row>

                    <table>
                      <tr>
                        <th></th>
                        <th>Temperature</th>
                        <th>Blood Pressure</th>
                        <th>Respiratory Rate</th>
                        <th>Pulse Rate</th>
                        <th>Remarks</th>
                      </tr>
                      <tr>
                        <td class="pa-1">Prior to transfusion</td>
                        <td><v-text-field type="number" v-model="component.Tranfusion.VitalSigns[0].Temperature" hide-details="true" :disabled="isView" dense flat solo /></td>
                        <td><v-text-field v-model="component.Tranfusion.VitalSigns[0].BloodPressure" counter="10" hide-details="true" :disabled="isView" dense flat solo /></td>
                        <td><v-text-field type="number" v-model="component.Tranfusion.VitalSigns[0].RespiratoryRate" hide-details="true" :disabled="isView" dense flat solo /></td>
                        <td><v-text-field type="number" v-model="component.Tranfusion.VitalSigns[0].PulseRate" hide-details="true" :disabled="isView" dense flat solo /></td>
                        <td><v-text-field v-model="component.Tranfusion.VitalSigns[0].Remarks" counter="250" hide-details="true" :disabled="isView" dense flat solo /></td>
                      </tr>
                      <tr>
                        <td class="pa-1">During height of transfusion</td>                        
                        <td><v-text-field type="number" v-model="component.Tranfusion.VitalSigns[1].Temperature" hide-details="true" :disabled="isView" dense flat solo /></td>
                        <td><v-text-field v-model="component.Tranfusion.VitalSigns[1].BloodPressure" counter="10" hide-details="true" :disabled="isView" dense flat solo /></td>
                        <td><v-text-field type="number" v-model="component.Tranfusion.VitalSigns[1].RespiratoryRate" hide-details="true" :disabled="isView" dense flat solo /></td>
                        <td><v-text-field type="number" v-model="component.Tranfusion.VitalSigns[1].PulseRate" hide-details="true" :disabled="isView" dense flat solo /></td>
                        <td><v-text-field v-model="component.Tranfusion.VitalSigns[1].Remarks" counter="250" hide-details="true" :disabled="isView" dense flat solo /></td>
                      </tr>
                      <tr>
                        <td class="pa-1">30 mins post-transfusion</td>
                        <td><v-text-field type="number" v-model="component.Tranfusion.VitalSigns[2].Temperature" hide-details="true" :disabled="isView" dense flat solo /></td>
                        <td><v-text-field v-model="component.Tranfusion.VitalSigns[2].BloodPressure" counter="10" hide-details="true" :disabled="isView" dense flat solo /></td>
                        <td><v-text-field type="number" v-model="component.Tranfusion.VitalSigns[2].RespiratoryRate" hide-details="true" :disabled="isView" dense flat solo /></td>
                        <td><v-text-field type="number" v-model="component.Tranfusion.VitalSigns[2].PulseRate" hide-details="true" :disabled="isView" dense flat solo /></td>
                        <td><v-text-field v-model="component.Tranfusion.VitalSigns[2].Remarks" counter="250" hide-details="true" :disabled="isView" dense flat solo /></td>
                      </tr>
                    </table>

                    <v-row class="my-0">
                      <v-col cols="3">                        
                        <label class="label-container">Transfusion Status</label>
                        <v-radio-group class="radio-container-row" v-model="component.Tranfusion.TransfusionStatus" :disabled="isView">
                          <v-radio v-for="(type, index) in transfusionStatusOptions" :key="index" :label="type.text" :value="type.value" />
                        </v-radio-group>
                      </v-col>
                      <v-col cols="9">
                        <v-textarea v-model="component.Tranfusion.TransfusionNotes" label="Transfusion Notes" :rules="[rules.required, rules.maxLength(250)]" :disabled="isView" no-resize dense outlined />
                      </v-col>
                    </v-row>
                  </v-expansion-panel-content>
                </v-expansion-panel>
              </v-expansion-panels>
            </v-container>
          </v-form>     
        </v-card-text>
        <v-divider />
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn v-if="isView" text color="default darken-1" @click="onClose(false)"> Back</v-btn>
          <v-btn v-if="!isView" text color="default darken-1" @click="onClose(false)"> Cancel</v-btn>
          <v-btn v-if="!isView" text color="primary" @click="onSave"> Save</v-btn>
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
import CommonOptions from '@/common/CommonOptions';
import { TransfusionViewDetailsDto, TransfusionViewDto } from '@/models/Requisitions/TransfusionViewDetailsDto';
import moment from 'moment';
import { RequisitionStatus } from '@/models/Enums/RequisitionStatus';
import { TransfusionDto } from '@/models/Requisitions/TransfusionDto';

@Component({ components: { } })
export default class TransfusionModal extends VueBase {
  @Prop({ required: true }) public toggle!: boolean;
  @Prop({ required: true }) public reservationId!: Guid;

	protected get showDialog(): boolean {
    return this.toggle;
  }

  protected requisitionService: RequisitionsService = new RequisitionsService();
  protected errorMessage: string = '';
  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected transfusionStatusOptions: any = CommonOptions.TranfusionStatusOptions;
  protected transfusionDetail: TransfusionViewDetailsDto = new TransfusionViewDetailsDto();
  protected isView: boolean = false;

  protected onChangeTransfusionDates(component: TransfusionViewDto): void {
    if (Common.hasValue(component.TransfusionStarted) && Common.hasValue(component.TransfusionEnded)) {
      var diff = moment(component.TransfusionEnded).diff(moment(component.TransfusionStarted));
      var duration = moment.duration(diff);
      component.TotalTimeConsumed = duration.asHours();
    }
  }

	protected async onSave(): Promise<void> {
    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    let loader = this.showLoader();
    try {
      let transfusions: Array<TransfusionDto> = new Array<TransfusionDto>();
      
      this.transfusionDetail.BloodComponentDetails.forEach(x => {
        let transfusion: TransfusionDto = 
        {
          TransfusionId: x.Tranfusion.TransfusionId,
          ReservationId: this.transfusionDetail.ReservationId,
          ReservationItemId: x.ReservationItemId,
          TransfusionStarted: x.Tranfusion.TransfusionStarted,
          TransfusionEnded: x.Tranfusion.TransfusionEnded,
          TotalTimeConsumed: x.Tranfusion.TotalTimeConsumed,
          MedicationGiven: x.Tranfusion.MedicationGiven,
          HookedBy: x.Tranfusion.HookedBy,
          RemovedBy: x.Tranfusion.RemovedBy,
          VitalSigns: x.Tranfusion.VitalSigns,
          TransfusionStatus: x.Tranfusion.TransfusionStatus,
          TransfusionNotes: x.Tranfusion.TransfusionNotes
        }

        transfusions.push(transfusion);
      });

      await this.requisitionService.upsertTransfusionDetails(transfusions);
      this.notify_success('Transfusion details successfully updated.');
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

  protected async created(): Promise<void> {
    let loader = this.showLoader();

    try {
      this.transfusionDetail = await this.requisitionService.getTransfusionDetails(this.reservationId);
      this.isView = this.transfusionDetail.ReservationStatus === RequisitionStatus.Done;

      // Set total time consumed
      this.transfusionDetail.BloodComponentDetails.forEach(component => this.onChangeTransfusionDates(component.Tranfusion));
    }
    catch (error: any) {
      console.log(error)
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
  .radio-container-row {
    margin-top: 4px;
  }

  table, th, td {
    border: 1px solid rgb(139, 138, 138);
    border-collapse: collapse;
  }
</style>