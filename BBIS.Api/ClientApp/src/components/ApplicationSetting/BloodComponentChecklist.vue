<template>
  <v-container fluid>
    <UpsertBloodComponentChecklist v-if="showUpsertChecklistDialog" :toggle="showUpsertChecklistDialog" :id="selectedId" :isEdit="isEdit" @onClose="onCreateDialogClose" />
    
    <v-card>
      <v-row class="mx-3 pt-3">
        <v-col cols="6">
          <v-text-field class="pr-2" type="text" label="Search" v-model.trim="pagedSearchDto.SearchText" dense outlined />
        </v-col>
        <v-col cols="2">
          <v-btn class="pr-2" color="default" @click="loadrecords" depressed><v-icon size="25" color="primary" left>mdi-magnify</v-icon>Search</v-btn>
        </v-col>
        <v-spacer></v-spacer>
        <v-col cols="3" class="text-right">
          <v-btn class="pr-2 " color="default" @click="onAdd" depressed><v-icon size="25" color="primary" left>mdi-playlist-plus</v-icon>Add New Checklist</v-btn>
        </v-col>
      </v-row>

      <v-data-table :headers="columnHeaders" 
                    :items="records" 
                    :options.sync="options"
                    :server-items-length="dataLoaded ? pagedResult.TotalCount : 0"
                    :loading="loading"            
                    class="elevation-2 pl-2 pr-2">

        <template v-slot:[`item.IsActive`]="{ item }">
          <v-chip small outlined :color="item.IsActive ? 'primary' : 'default' " label >{{ item.IsActive ? 'Active' : 'Inactive' }}</v-chip>  
        </template>

        <template v-slot:[`item.IsAdult`]="{ item }">
          {{ item.IsAdult ? 'Yes' : 'No'}}
        </template>

        <template v-slot:[`item.Actions`]="{ item }">
          <v-icon @click="onEdit(item.BloodComponentChecklistId)" class="mr-3">mdi-pencil</v-icon>
        </template>
      </v-data-table>
    </v-card>
  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Watch } from 'vue-property-decorator';
import UpsertBloodComponentChecklist from '@/components/ApplicationSetting/UpsertBloodComponentChecklist.vue';
import { PagedSearchDto, PagedSearchResultDto } from '@/models/PagedSearchDto';
import ApplicationSettingService from '@/services/ApplicationSettingService';
import { IBloodComponentChecklistListDto } from '@/models/ApplicationSetting/IBloodComponentChecklistListDto';

@Component({components: { UpsertBloodComponentChecklist }})
export default class BloodComponentChecklist extends VueBase { 

  protected applicationSettingService: ApplicationSettingService = new ApplicationSettingService();
  protected dataLoaded: boolean = false;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'Blood Component',          value: 'BloodComponentName',    sortable: true,     width: '20%' },
    { text: 'Checklist Description',    value: 'ChecklistDescription',  sortable: false,    width: '50%' },
    { text: 'For Adult',                value: 'IsAdult',               sortable: false,    width: '10%' },
    { text: 'Status',                   value: 'IsActive',              sortable: false,    width: '10%' },
    { text: '',                         value: 'Actions',               sortable: false,    width: '10%' }
  ];

  protected records: Array<IBloodComponentChecklistListDto> = [];
  protected pagedSearchDto: PagedSearchDto = new PagedSearchDto();
  protected pagedResult!: PagedSearchResultDto<IBloodComponentChecklistListDto>;
  protected options: any = {}; 
  protected showUpsertChecklistDialog: boolean = false;
  protected selectedId: Guid = '';
  protected isEdit: boolean = false;

  @Watch('options')
  protected async optionChange(): Promise<void> {
    await this.loadrecords(); 
  }

  protected onAdd(): void {
    this.showUpsertChecklistDialog = true;
  }

  protected onEdit(id: Guid): void {
    this.showUpsertChecklistDialog = true;
    this.isEdit = true;
    this.selectedId = id;
  }

  protected async onCreateDialogClose(refreshRecord: boolean): Promise<void> {
    this.showUpsertChecklistDialog = false;
    this.selectedId = '';
    this.isEdit = false;

    if (refreshRecord) {
      await this.loadrecords();
    }
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
      this.pagedResult = await this.applicationSettingService.getBloodComponentChecklistSettings(this.pagedSearchDto);
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

  protected async mounted(): Promise<void> {
    //await this.loadrecords();
  }
  
}
</script>