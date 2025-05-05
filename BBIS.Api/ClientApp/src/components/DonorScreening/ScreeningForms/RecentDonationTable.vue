<template>
  <v-card>
    <CreateBloodComponentSetting :toggle="showDonationLogDialog" :agencyOptions="agencyOptions" :hospitalOptions="hospitalOptions" @onClose="onCreateDialogClose" />

    <v-row class="mx-3 py-3">
      <v-card-actions>
        <h2 class="mt-1 head-title grey--text">Recent Donations</h2> 
      </v-card-actions>

      <v-spacer></v-spacer>
      <v-col cols="2" class="text-right">
        <v-btn class="pr-2 " color="default" v-if="!canOnlyView" @click="onAdd" depressed><v-icon left>mdi-plus</v-icon>Add New Donation Log</v-btn>
      </v-col>
    </v-row>

    <v-data-table :headers="columnHeaders" 
                  :items="records" 
                  :loading="loading"            
                  class="elevation-2 pl-2 pr-2">

      <template v-slot:[`item.Agency`]="{ item }">
        <v-autocomplete v-if="item.DonorRecentDonationId === editedItem.DonorRecentDonationId" :items="agencyOptions" v-model="editedItem.Agency" :hide-details="true" dense single-line outlined />
        <span v-else>{{addSpaceBetweenUpperCaseLetters(item.Agency)}}</span>
      </template>

      <template v-slot:[`item.NumberOfDonation`]="{ item }">
        <v-text-field v-if="item.DonorRecentDonationId === editedItem.DonorRecentDonationId" type="number" v-model="editedItem.NumberOfDonation" :hide-details="true" dense single-line outlined />
        <span v-else>{{item.NumberOfDonation}}</span>
      </template>

      <template v-slot:[`item.DateOfRecentDonation`]="{ item }">
        <v-text-field v-if="item.DonorRecentDonationId === editedItem.DonorRecentDonationId" type="date" v-model="editedItem.DateOfRecentDonation" :hide-details="true" dense single-line outlined />
        <span v-else>{{formatDate(item.DateOfRecentDonation)}}</span>
      </template>

      <template v-slot:[`item.PlaceOfRecentDonation`]="{ item }">
        <v-text-field v-if="item.DonorRecentDonationId === editedItem.DonorRecentDonationId" v-model="editedItem.PlaceOfRecentDonation" :hide-details="true" dense single-line outlined />
        <v-autocomplete v-if="item.DonorRecentDonationId === editedItem.DonorRecentDonationId" :items="hospitalOptions" v-model="editedItem.PlaceOfRecentDonation" :hide-details="true" dense single-line outlined />
        <span v-else>{{item.PlaceOfRecentDonation}}</span>
      </template>

      <template v-if="!canOnlyView" v-slot:[`item.Actions`]="{ item }">
        <div v-if="item.DonorRecentDonationId === editedItem.DonorRecentDonationId">
          <v-icon @click="save" class="mr-3">mdi-content-save</v-icon>
          <v-icon @click="close">mdi-window-close</v-icon>
        </div>
        <div v-else>
          <v-icon @click="onEdit(item)" class="mr-3">mdi-pencil</v-icon>
          <v-icon @click="onDelete(item)" class="mr-3">mdi-delete</v-icon>
        </div>
      </template>
    </v-data-table>
  </v-card>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Prop, Emit } from 'vue-property-decorator';
import { DonorRecentDonationDto, IDonorRecentDonationDto } from '@/models/DonorScreening/DonorRecentDonationDto';
import DonorScreeningService from '@/services/DonorScreeningService';
import CreateBloodComponentSetting from '@/components/DonorScreening/ScreeningForms/CreateNewRecentDonationLog.vue';

@Component({components: {CreateBloodComponentSetting}})
export default class RecentDonationTable extends VueBase { 
  @Prop()
  public donorRegistrationId!: Guid;

  @Prop()
  public agencyOptions!: Array<{text: string, value: string}>;
  public hospitalOptions!: Array<{text: string, value: string}>;

  @Prop()
  public canOnlyView!: boolean;

  protected donorScreeningService: DonorScreeningService = new DonorScreeningService();
  protected dataLoaded: boolean = false;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'Agency',                 value: 'Agency',                sortable: true,   width: '20%' },
    { text: 'How many times',         value: 'NumberOfDonation',      sortable: false,  width: '20%' },
    { text: 'Date of last donation',  value: 'DateOfRecentDonation',  sortable: false,  width: '25%' },
    { text: 'Place of last Donation', value: 'PlaceOfRecentDonation', sortable: false,  width: '25%' },
    { text: '',                       value: 'Actions',               sortable: false,  width: '10%' }
  ];

  protected records: Array<IDonorRecentDonationDto> = [];
  protected selectedId: string = '';
  protected editedIndex: number = -1;
  protected editedItem: IDonorRecentDonationDto = new DonorRecentDonationDto();
  protected showDonationLogDialog: boolean = false;

  protected onEdit(item: IDonorRecentDonationDto): void {
    this.editedIndex = this.records.indexOf(item);
    this.editedItem = Object.assign({}, item);
  }

  protected onDelete(item: IDonorRecentDonationDto): void {
    let index = this.records.indexOf(item);
    this.records.splice(index, 1);
  }

  protected close(): void {
    this.editedItem = Object.assign({}, new DonorRecentDonationDto());
    this.editedIndex = -1;
  }
  
  protected async save(): Promise<void> {
    if (this.editedIndex > -1) {
      try {
        Object.assign(this.records[this.editedIndex], this.editedItem)
        this.onChangeLog(this.records);
        this.close()
      }
      catch(error: any) {
        if (error.StatusCode != 500) {
          this.errorMessage = error.Message;
          this.notify_error(this.errorMessage);
        }
      }
    }
  }

  protected onAdd() : void {
    this.showDonationLogDialog = true;
  }

  protected async onCreateDialogClose(refreshRecord: boolean, newItem: IDonorRecentDonationDto): Promise<void> {
    this.showDonationLogDialog = false;

    if (refreshRecord) {
      newItem.DonorRecentDonationId = `temporaryid_${this.records.length+1}`;
      this.records.push({ DonorRecentDonationId: newItem.DonorRecentDonationId, 
                          Agency: newItem.Agency, 
                          NumberOfDonation: newItem.NumberOfDonation,
                          DateOfRecentDonation: newItem.DateOfRecentDonation,
                          PlaceOfRecentDonation: newItem.PlaceOfRecentDonation});
      this.onChangeLog(this.records);
                          
    }
  }

  protected async mounted(): Promise<void> {
    await this.loadrecords();
  }

  protected async loadrecords(): Promise<void> {  
    this.dataLoaded = false;
    this.loading = true;
    this.showError = false;
    
    try {
      this.records = await this.donorScreeningService.getRecentDonations(this.donorRegistrationId);
      this.loading = false;
      this.dataLoaded = true;
    } 
    catch (error) {
      this.showError = true;
      this.loading = false;
      this.dataLoaded = false;
    }
  }

  @Emit('onChangeLog')
  protected onChangeLog(records: Array<IDonorRecentDonationDto>): Array<IDonorRecentDonationDto> {
    return records;
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