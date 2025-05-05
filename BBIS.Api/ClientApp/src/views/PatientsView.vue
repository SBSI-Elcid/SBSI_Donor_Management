<template>
  <v-container fluid>

    <CreatePatient :toggle="showCreateDialog" v-if="showCreateDialog" @onClose="onCreateDialogClose" />
    <UpdatePatient :toggle="showUpdateDialog" :id="selectedPatientId" v-if="showUpdateDialog" @onClose="onUpdateDialogClose" />
    <CreateTestOrder :toggle="showCreateTestOrderDialog" :patient="patientTypeAheadResultDto" v-if="showCreateTestOrderDialog" @onClose="onDialogClose" />

    <v-card>
      <v-card-actions>
        <h2 class="ml-2 mt-1 head-title ms-4 grey--text">Patients</h2> 
        <v-spacer></v-spacer>
        <v-btn color="default" class="mt-2 mr-2" depressed @click="addClick"><v-icon size="25" color="primary" left>mdi-account-plus</v-icon> Add New Patient</v-btn>
      </v-card-actions>

      <v-row no-gutters class="mx-3 pt-3">
        <v-col md="3" sm="9">
          <v-text-field type="text" label="Search" v-model.trim="pagedSearchDto.SearchText" dense outlined />
        </v-col>
        <v-col md="2" sm="2" class="pl-2" >
          <v-btn color="default" @click="loadrecords" depressed><v-icon size="25" color="primary" left>mdi-magnify</v-icon> Search</v-btn>
        </v-col>
      </v-row>
               
      <v-data-table
        :headers="columnHeaders"
        :items="records"
        :options.sync="options"
        :server-items-length="dataLoaded ? pagedResult.TotalCount : 0"
        :loading="loading"            
        class="elevation-2 px-1">

        <template v-slot:[`progress`]>
          <v-progress-linear color="#B92F2F" indeterminate></v-progress-linear>
        </template>

        <template v-slot:[`item.BloodType`]="{ item }">
          <span class="font-weight-bold">{{ item.BloodType }}</span>
          <span class="font-weight-bold">{{ item.Rh == 'Positive' ? '+' : '-' }}</span>
        </template>

        <template v-slot:[`item.Actions`]="{ item }">
          <v-btn icon title="Edit patient" @click="onEdit(item.PatientId)"><v-icon>mdi-pencil</v-icon></v-btn> 
        </template> 
      </v-data-table>
      
    </v-card>


  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { PagedSearchResultDto } from '@/models/PagedSearchDto';
import { Component, Watch } from 'vue-property-decorator';
import PatientService from '@/services/PatientService';
import { PatientViewDto } from '@/models/Patient/PatientViewDto';
import CreatePatient from '@/components/Patient/CreatePatient.vue';
import UpdatePatient from '@/components/Patient/UpdatePatient.vue';
import CreateTestOrder from '@/components/TestOrder/CreateTestOrder.vue';
import moment from 'moment';
import { TypeAheadResultDto } from '@/models/TestOrder/TypeAheadResultDto';
import { PatientPagedSearchDto } from '@/models/Patient/PatientPagedSearchDto';

@Component({ components: { CreatePatient, UpdatePatient, CreateTestOrder } })
export default class PatientsView extends VueBase {
  protected patientService: PatientService = new PatientService();
  protected dataLoaded: boolean = false;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'Blood Type',         value: 'BloodType',   sortable: true },
    { text: 'Patient Record No.', value: 'PatientIdNo', sortable: false },
    { text: 'Patient Name',       value: 'FullName',    sortable: true },
    { text: 'Gender',             value: 'Gender',      sortable: true },
    { text: '',                   value: 'Actions',     sortable: false }
  ];
  protected records: Array<PatientViewDto> = [];
  protected pagedSearchDto: PatientPagedSearchDto = new PatientPagedSearchDto();
  protected pagedResult!: PagedSearchResultDto<PatientViewDto>;
  protected options: any = {}; 
  protected showCreateDialog: boolean = false;
  protected showUpdateDialog: boolean = false;
  protected selectedPatientId: Guid | null = null;
	protected selected: Array<PatientViewDto> = [];
  protected patientTypeAheadResultDto: TypeAheadResultDto | null = null;
  protected showCreateTestOrderDialog: boolean = false;

  @Watch('options')
  protected async optionChange(): Promise<void> {
    await this.loadrecords(); 
  }

  protected async onDialogClose(refresh: boolean): Promise<void> {
    this.showCreateTestOrderDialog = false;
  }

  protected addClick(): void {
    this.showCreateDialog = true;
  }

  protected onEdit(id: Guid): void {
    this.showUpdateDialog = true;
    this.selectedPatientId = id;
  }

  protected onCreateTestOrder(item: PatientViewDto): void {
    this.patientTypeAheadResultDto = { Id: item.PatientId, Name: item.FullName, BloodType: item.BloodType };
    this.showCreateTestOrderDialog = true;
  }

  protected async onCreateDialogClose(refreshRecord: boolean): Promise<void> {
    this.showCreateDialog = false;

    if (refreshRecord) {
      await this.loadrecords();
    }
  }

  protected async onUpdateDialogClose(refreshRecord: boolean): Promise<void> {
    this.showUpdateDialog = false;

    if (refreshRecord) {
      await this.loadrecords();
    }
  }

  protected calculateAge(birthDate: Date | null): number {
    let years = moment().diff(moment(birthDate, 'YYYY-MM-DD'), 'years');
    return years;
  }

  protected async loadrecords(): Promise<void> {  
    const { page, itemsPerPage, sortBy, sortDesc } = this.options;
    this.pagedSearchDto.PageNumber = page;
    this.pagedSearchDto.PageSize = itemsPerPage;
    this.pagedSearchDto.SortBy = sortBy[0] || '';
    this.pagedSearchDto.SortDesc = sortDesc[0];
    this.dataLoaded = false;
    this.loading = true;
    this.showError = false;
    
    try {
      this.pagedResult = await this.patientService.getList(this.pagedSearchDto);
      this.loading = false;
      this.records = this.pagedResult.Results;
      this.dataLoaded = true;
    } 
    catch (error) {
      this.showError = true;
      this.loading = false;
      this.dataLoaded = false;
    }
  }
}

</script>