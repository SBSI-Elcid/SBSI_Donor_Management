<template>
  <v-container fluid>
    <UpsertTestOrderType v-if="showUpsertTestOrderDialog" :toggle="showUpsertTestOrderDialog" :id="selectedId" :isEdit="isEdit" @onClose="onCreateDialogClose" />
    
    <v-card>
      <v-row class="mx-3 pt-3">
        <v-col cols="6">
          <v-text-field class="pr-2" type="text" label="Search" v-model.trim="pagedSearchDto.SearchText" dense outlined />
        </v-col>
        <v-col cols="2">
          <v-btn class="pr-2" color="default" @click="loadrecords" depressed><v-icon size="25" color="primary" left>mdi-magnify</v-icon>Search</v-btn>
        </v-col>
        <!--<v-spacer></v-spacer>
        <v-col cols="3" class="text-right">
          <v-btn class="pr-2" color="default" @click="onAdd" depressed><v-icon left>mdi-test-tube</v-icon>Add New Test Order Type</v-btn>
        </v-col>-->
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

        <template v-slot:[`item.Actions`]="{ item }">
          <v-icon @click="onEdit(item.TestOrderTypeId)" class="mr-3">mdi-pencil</v-icon>
        </template>
      </v-data-table>
    </v-card>
  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Watch } from 'vue-property-decorator';
import UpsertTestOrderType from '@/components/ApplicationSetting/UpsertTestOrderType.vue';
import { PagedSearchDto, PagedSearchResultDto } from '@/models/PagedSearchDto';
import ApplicationSettingService from '@/services/ApplicationSettingService';
import { ITestOrderTypeSettingDto } from '@/models/ApplicationSetting/ITestOrderTypeSettingDto';

@Component({components: { UpsertTestOrderType }})
export default class TestOrderType extends VueBase {
protected applicationSettingService: ApplicationSettingService = new ApplicationSettingService();

  protected dataLoaded: boolean = false;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'Code',           value: 'Code',             sortable: true,   width: '10%' },
    { text: 'Name',           value: 'Name',             sortable: true,   width: '15%' },
    { text: 'Description',    value: 'Description',      sortable: true,   width: '30%' },
    { text: 'Status',         value: 'IsActive',         sortable: true,   width: '10%' },
    { text: '',               value: 'Actions',          sortable: false,  width: '5%'  }
  ];

  protected records: Array<ITestOrderTypeSettingDto> = [];
  protected pagedSearchDto: PagedSearchDto = new PagedSearchDto();
  protected pagedResult!: PagedSearchResultDto<ITestOrderTypeSettingDto>;
  protected options: any = {}; 

  protected showUpsertTestOrderDialog: boolean = false;
  protected selectedId: Guid | null = null;  
  protected isEdit: boolean = false;
  
  @Watch('options')
  protected async optionChange(): Promise<void> {
    await this.loadrecords(); 
  }

  protected onAdd(): void {
    this.showUpsertTestOrderDialog = true;
  }

  protected onEdit(id: Guid): void {
    this.showUpsertTestOrderDialog = true;
    this.isEdit = true;
    this.selectedId = id;
  }

  protected async onCreateDialogClose(refreshRecord: boolean): Promise<void> {
    this.showUpsertTestOrderDialog = false;
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
      this.pagedResult = await this.applicationSettingService.getTestOrderTypeSettings(this.pagedSearchDto);
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