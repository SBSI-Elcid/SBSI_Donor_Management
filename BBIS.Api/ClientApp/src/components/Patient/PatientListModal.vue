<template>
  <v-container fluid>

    <CreatePatient :toggle="showCreateDialog" v-if="showCreateDialog" @onClose="onCreateDialogClose" />
    <UpdatePatient :toggle="showUpdateDialog" :id="selectedPatientId" v-if="showUpdateDialog" @onClose="onUpdateDialogClose" />

    <v-dialog v-model="showDialog" persistent max-width="850px" >     
      <v-card>
        <v-card-title>Select Patient</v-card-title>
        <v-divider></v-divider>
        <v-card-text>
					<v-row no-gutters class="mx-1 pt-3">
						<v-col cols="6">
							<v-text-field type="text" dense label="Search" v-model.trim="pagedSearchDto.SearchText" clearable outlined />
						</v-col>
						<v-col cols="2" class="pl-2" >
							<v-btn color="default" @click="loadrecords" depressed><v-icon size="25" color="primary" left>mdi-magnify</v-icon> Search</v-btn>
						</v-col>
						<v-col cols="4" class="pl-2 text-right" >
							<v-btn color="primary" small @click="addClick" depressed><v-icon left>mdi-plus</v-icon> Create</v-btn>
						</v-col>
					</v-row>
                    
					<v-data-table
						v-model="selected"
						show-select
						single-select
						item-key="PatientId"
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
              <v-icon @click="onEdit(item.PatientId)" class="mr-3">mdi-pencil</v-icon>
            </template> 
					</v-data-table>
        </v-card-text>
				<v-divider></v-divider>
        <v-card-actions>
          <v-btn depressed :disabled="selected.length == 0" color="success" @click="onSelect" style="width: 100px;">Select</v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="default darken-1" @click="onClose"> Cancel</v-btn>
        </v-card-actions> 
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { PagedSearchResultDto } from '@/models/PagedSearchDto';
import { Component, Emit, Prop, Watch } from 'vue-property-decorator';
import PatientService from '@/services/PatientService';
import { PatientViewDto } from '@/models/Patient/PatientViewDto';
import CreatePatient from '@/components/Patient/CreatePatient.vue';
import UpdatePatient from '@/components/Patient/UpdatePatient.vue';
import { SelectedPatientDto } from '@/models/Patient/SelectedPatientDto';
import moment from 'moment';
import { PatientPagedSearchDto } from '@/models/Patient/PatientPagedSearchDto';

@Component({ components: { CreatePatient, UpdatePatient } })
export default class PatientListModal extends VueBase {
	@Prop({ required: true }) public toggle!: boolean;
  @Prop({ required: false, default: '' }) public bloodType!: string;
  @Prop({ required: false, default: '' }) public bloodRH!: string;

	protected get showDialog(): boolean {
    return this.toggle;
  }

  protected patientService: PatientService = new PatientService();
  protected dataLoaded: boolean = false;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'Blood Type',         value: 'BloodType',   sortable: true },
    { text: 'Patient Name',       value: 'FullName',    sortable: true },
    { text: 'Patient Record No.', value: 'PatientIdNo', sortable: false },
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

  @Watch('options')
  protected async optionChange(): Promise<void> {
    await this.loadrecords(); 
  }

  protected addClick(): void {
    this.showCreateDialog = true;
  }

  protected onEdit(id: Guid): void {
    this.showUpdateDialog = true;
    this.selectedPatientId = id;
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

  @Emit('onSelect')
  protected async onSelect(): Promise<SelectedPatientDto> {
    let selectedPatient: SelectedPatientDto = {
      PatientId: this.selected[0].PatientId,
      PatientName: this.selected[0].FullName,
      BloodType: this.selected[0].BloodType,
      Age: this.calculateAge(this.selected[0].DateOfBirth,),
      Sex: this.selected[0].Gender,
    }
    return selectedPatient;
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
    this.pagedSearchDto.BloodType = this.bloodType;
    this.pagedSearchDto.BloodRh = this.bloodRH;
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

	@Emit("onClose") 
  protected onClose(refresh: boolean): boolean {
    return refresh;
  }


  protected async mounted() : Promise<void> {
    //await this.loadrecords();
  }

}

</script>