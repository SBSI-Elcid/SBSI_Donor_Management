<template>
  <v-row justify="center">
    <PatientListModal :toggle="showPatientListDialog" :bloodType="bloodType" :bloodRH="bloodRH" v-if="showPatientListDialog" @onClose="showPatientListDialog =false" @onSelect="onSelectPatient" />

    <v-dialog v-model="showDialog" persistent max-width="950">     
      <v-card>
        <v-card-title>
         	<h3 class="pl-1 pt-1">Reserve</h3>
          <v-spacer></v-spacer>
          <h3> {{ bloodType }}{{ RhTypeSign }}</h3>
          <v-icon size="20" color="red">mdi-water</v-icon>
        </v-card-title>
        <v-divider></v-divider>
        <v-card-text>
          <v-form ref="form" v-model="formValid" lazy-validation>
            <v-container>
              <v-row no-gutters>
                <v-col cols="12">
                  <v-checkbox style="display: inline-block;" class="pr-2 my-2" label="Adult" v-model="reservation.ForAdult" :hide-details="true"/>
                  <v-checkbox style="display: inline-block;" class="pl-2 my-2" label="Emergency" v-model="reservation.IsEmergency" :hide-details="true"/>
                  <v-checkbox style="display: inline-block;" class="pl-2 my-2" label="Previously Transfused" v-model="hasPreviousTransfusion" :hide-details="true"/>
                </v-col>
              </v-row>

              <v-row no-gutters>
								<v-col cols="5" class="pr-2">
									<label class="label-container">Patient Name</label>
									<v-text-field v-model="patient.PatientName" :rules="[rules.required]" readonly dense outlined />
								</v-col>

                <v-col cols="1" class="pr-2">
									<v-btn class="mt-6" @click="addPatient" icon> <v-icon color="primary">mdi-filter-outline</v-icon> </v-btn>
								</v-col>
                
								<v-col cols="3" class="pr-2 pl-2">
									<label class="label-container">Age</label>
									<v-text-field v-model="patient.Age" :rules="[rules.required]" readonly dense outlined />
								</v-col>

								<v-col cols="3">
									<label class="label-container">Sex</label>
									<v-text-field v-model="patient.Sex" :rules="[rules.required]" readonly dense outlined />
								</v-col>
							</v-row>

              <v-row no-gutters>
								<v-col cols="3" class="pr-2">
									<label class="label-container">Patient Type</label>
									<v-autocomplete :items="patientTypeOptions" v-model="reservation.PatientType" :rules="[rules.required]" dense outlined />
								</v-col>

                <v-col cols="3">
                  <label class="label-container">Priority Level</label>
									<v-autocomplete :items="priorityLevelOptions" v-model="reservation.PriorityLevel" :rules="[rules.required]" dense outlined />
								</v-col>
                
								<v-col cols="3" class="pr-2 pl-2">
									<label class="label-container">Room No</label>
									<v-text-field v-model="reservation.RoomNo" :rules="[rules.required, rules.maxLength(10)]" dense outlined />
								</v-col>

								<v-col cols="3">
									<label class="label-container">Membership</label>
									<v-autocomplete :items="membershipOptions" v-model="reservation.Membership" :rules="[rules.required]" dense outlined />
								</v-col>
							</v-row>

              <v-row no-gutters>
								<v-col cols="3" class="pr-2">
									<label class="label-container">Requested By</label>
									<v-text-field v-model="reservation.RequestedBy" :rules="[rules.required, rules.maxLength(55)]" dense outlined />
								</v-col>

                <v-col cols="3">
                  <label class="label-container">Requested Date</label>
									<DatePicker v-model="reservation.RequestedDateTime" :rules="[rules.required]" dense outlined />
								</v-col>

								<v-col cols="3" class="pr-2 pl-2" v-if="hasPreviousTransfusion">
									<label class="label-container">Previous Transfusion Date</label>
									<DatePicker v-model="reservation.PreviousTransfusionDate" :rules="hasPreviousTransfusion ? [rules.required] : []" dense outlined />
								</v-col>

								<v-col cols="3" v-if="hasPreviousTransfusion">
									<label class="label-container">Previous No of Units Transfused</label>
									<v-text-field v-model="reservation.PreviousNoOfUnitsTransfused" :rules="hasPreviousTransfusion ? [rules.required] : []" dense outlined />
								</v-col>
							</v-row>
              
              <v-row no-gutters v-if="hasBloodType">
                <v-col cols="12" class="pr-2 pl-2" v-for="(checklist, i) in checklists" :key="i">
                  <BloodComponentWidget :key="i" 
                    :bloodComponent="checklist" 
                    :bloodType="patient.BloodType" 
                    :options="getAvailableUnitOptions(checklist.BloodComponentId)" 
                    @onSelected="onReservationItemSelected"
                    @onDeSelected="onReservationItemUnSelected" />
                </v-col>
              </v-row>
              
            </v-container>
          </v-form>     
        </v-card-text>
        <v-divider />
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn text color="default darken-1" @click="onClose(false)"> Cancel</v-btn>
          <v-btn text color="primary" :disabled="disableSaveButton" @click="onSave"> Save</v-btn>
        </v-card-actions> 
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Emit, Prop } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import LookupModule from '@/store/LookupModule';
import Common from '@/common/Common';
import { LookupKeys } from '@/models/Enums/LookupKeys';
import DatePicker from '@/components/Common/FormInputs/DatePicker.vue';
import PatientListModal from '@/components/Patient/PatientListModal.vue';
import BloodComponentWidget from '@/components/Requisition/BloodComponentWidget.vue';
import { ChecklistOptionDto } from '@/models/Requisitions/ChecklistOptionDto';
import RequisitionsService from '@/services/RequisitionsService';
import { ILookupOptions } from '@/models/Lookups/LookupOptions';
import { SelectedPatientDto } from '@/models/Patient/SelectedPatientDto';
import InventoryService from '@/services/InventoryService';
import { AvailableInventoryItemDto } from '@/models/Inventory/AvailableInventoryItemDto';
import { ReservationDto } from '@/models/Requisitions/ReservationDto';
import { ReservationItemDto } from '@/models/Requisitions/ReservationItemDto';

@Component({ components: { DatePicker, PatientListModal, BloodComponentWidget } })
export default class ReservationModal extends VueBase {
  @Prop({ required: true }) public toggle!: boolean;
  @Prop({ required: true, default: '' }) public bloodType!: string;
  @Prop({ required: false, default: '' }) public bloodRH!: string;

	protected get showDialog(): boolean {
    return this.toggle;
  }

  protected inventoryService: InventoryService = new InventoryService();
  protected requisitionService: RequisitionsService = new RequisitionsService();
  protected lookupModule: LookupModule = getModule(LookupModule, this.$store);

  protected errorMessage: string = '';
  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected showPatientListDialog: boolean = false;
  protected hasPreviousTransfusion: boolean = false;
  protected checklists: Array<ChecklistOptionDto> = new Array<ChecklistOptionDto>();
  protected patient: SelectedPatientDto = new SelectedPatientDto();
  protected availableInventoryItems: Array<AvailableInventoryItemDto> = [];
  protected reservation: ReservationDto = new ReservationDto();
  protected reservationItems: Array<ReservationItemDto> = [];

  protected get RhTypeSign(): string {
    if (!this.bloodRH) {
      return '';
    }

    return this.bloodRH.toLowerCase() == 'positive' ? '+' : '-';
  }
 
  public get options(): (key: string) => Array<ILookupOptions> {
    return (key) => this.lookupModule.getOptionsByKey(key);
  }

  protected get priorityLevelOptions(): Array<{text: string, value: string}> {
    return this.options(LookupKeys.PriorityLevel).map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get patientTypeOptions(): Array<{text: string, value: string}> {
    return this.options(LookupKeys.PatientType).map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get membershipOptions(): Array<{text: string, value: string}> {
    return this.options(LookupKeys.Membership).map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get hasBloodType(): boolean {
    return Common.hasValue(this.patient.BloodType);
  }

  protected get disableSaveButton(): boolean {
    return this.reservationItems.length === 0;
  }

  protected onReservationItemSelected(selectedItems: Array<ReservationItemDto>): void {
    this.reservationItems = this.reservationItems.filter(x => !selectedItems.some(s => s.InventoryItemId === x.InventoryItemId));

    this.reservationItems.push(...selectedItems);
  }

  protected onReservationItemUnSelected(selectedItems: Array<ReservationItemDto>): void {
    this.reservationItems = this.reservationItems.filter(x => !selectedItems.some(s => s.InventoryItemId === x.InventoryItemId));
  }

  protected addPatient(): void {
    this.showPatientListDialog = true;
  }

  protected async onSelectPatient(patient: SelectedPatientDto): Promise<void> {
    this.patient = {
      PatientId: patient.PatientId,
      PatientName: patient.PatientName,
      BloodType: patient.BloodType,
      Age: patient.Age,
      Sex: patient.Sex
    };

    this.reservation.PatientId = patient.PatientId;
    this.showPatientListDialog = false;

    this.checklists = this.checklists.map(x => {
      return {
        BloodComponentId: x.BloodComponentId, 
        ComponentName: x.ComponentName, 
        CheckLists: x.CheckLists.filter(x => x.IsAdult === this.reservation.ForAdult)}
      });

    this.availableInventoryItems = await this.inventoryService.getAvailableInventoryUnits(this.patient.BloodType, this.bloodRH);
  }

  protected getAvailableUnitOptions(bloodComponentId: Guid): Array<{text: string, value: string}> {
    let inventoryItems: AvailableInventoryItemDto = this.availableInventoryItems.filter(x => x.BloodComponentId === bloodComponentId)[0];
    return Common.hasValue(inventoryItems) ? inventoryItems.AvailableOptions.map(x => {return {text: x.ItemText, value: x.InventoryItemId}}) : [];
  }

  protected async onSave(): Promise<void> {
    this.reservation.ReservationItems = this.reservationItems;

    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    let loader = this.showLoader();
    try {
      await this.requisitionService.createReservation(this.reservation);
      this.notify_success('Reservation successfully created.');
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

  protected async mounted(): Promise<void> {
    if(this.reservation.RequestedDateTime == null) {
      this.reservation.RequestedDateTime = new Date();
    }

    let loader = this.showLoader();
    try {
      await this.lookupModule.loadLookups();
      this.checklists = await this.requisitionService.getChecklists();
    }
    catch (error) {
      console.log(error);
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