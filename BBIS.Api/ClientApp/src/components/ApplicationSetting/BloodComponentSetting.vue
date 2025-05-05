<template>
  <v-container fluid>
    <CreateBloodComponentSetting :toggle="showCreateBloodComponentDialog" @onClose="onCreateDialogClose" />
    
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
          <v-btn class="pr-2" color="default" @click="onAdd" depressed><v-icon size="25" color="primary" left>mdi-water-plus</v-icon>Add New Blood Component</v-btn>
        </v-col>
      </v-row>

      <v-data-table :headers="columnHeaders" 
                    :items="records" 
                    :options.sync="options"
                    :server-items-length="dataLoaded ? pagedResult.TotalCount : 0"
                    :loading="loading"            
                    class="elevation-2 pl-2 pr-2">

        <template v-slot:[`item.ComponentName`]="{ item }">
          {{item.ComponentName}}
        </template>

        <template v-slot:[`item.ExpiryInDays`]="{ item }">
          <v-text-field v-if="item.BloodComponentId === editedItem.BloodComponentId" type="number" v-model="editedItem.ExpiryInDays" :hide-details="true" dense single-line :autofocus="true" outlined />
          <span v-else>{{item.ExpiryInDays}}</span>
        </template>

        <template v-slot:[`item.NotifyOnDaysBefore`]="{ item }">
          <v-text-field v-if="item.BloodComponentId === editedItem.BloodComponentId" type="number" v-model="editedItem.NotifyOnDaysBefore" :hide-details="true" dense single-line :autofocus="true" outlined />
          <span v-else>{{item.NotifyOnDaysBefore}}</span>
        </template>

        <template v-slot:[`item.IsActive`]="{ item }">
          <v-switch flat v-if="item.BloodComponentId === editedItem.BloodComponentId" v-model="editedItem.IsActive"/>
          <v-chip v-else small outlined :color="item.IsActive ? 'primary' : 'default' " label >{{ item.IsActive ? 'Active' : 'Inactive' }}</v-chip>  
        </template>

        <template v-slot:[`item.Actions`]="{ item }">
          <div v-if="item.BloodComponentId === editedItem.BloodComponentId">
            <v-icon @click="save" class="mr-3">mdi-content-save</v-icon>
            <v-icon @click="close">mdi-window-close</v-icon>
          </div>
          <div v-else>
            <v-icon @click="onEdit(item)" class="mr-3">mdi-pencil</v-icon>
          </div>
        </template>
      </v-data-table>
    </v-card>
  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Watch } from 'vue-property-decorator';
import CreateBloodComponentSetting from '@/components/ApplicationSetting/CreateBloodComponentSetting.vue';
import { BloodComponentSettingDto, IBloodComponentSettingDto } from '@/models/ApplicationSetting/BloodComponentSettingDto';
import { PagedSearchDto, PagedSearchResultDto } from '@/models/PagedSearchDto';
import ApplicationSettingService from '@/services/ApplicationSettingService';

@Component({components: {CreateBloodComponentSetting}})
export default class BloodComponentSetting extends VueBase { 
  protected applicationSettingService: ApplicationSettingService = new ApplicationSettingService();
  protected dataLoaded: boolean = false;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'Component Name', value: 'ComponentName', sortable: true, width: '30%' },
    { text: 'Expiry (in days)', value: 'ExpiryInDays', sortable: false, width: '25%' },
    { text: 'Notify On Days Before', value: 'NotifyOnDaysBefore', sortable: false, width: '25%' },
    { text: 'Status', value: 'IsActive', sortable: false, width: '10%' },
    { text: '', value: 'Actions', sortable: false, width: '10%' }
  ];
  protected records: Array<IBloodComponentSettingDto> = [];
  protected pagedSearchDto: PagedSearchDto = new PagedSearchDto();
  protected pagedResult!: PagedSearchResultDto<IBloodComponentSettingDto>;
  protected options: any = {}; 
  protected selectedId: string = '';
  protected editedIndex: number = -1;
  protected editedItem: IBloodComponentSettingDto = new BloodComponentSettingDto();
  protected showCreateBloodComponentDialog: boolean = false;

  @Watch('options')
  protected async optionChange(): Promise<void> {
    await this.loadrecords(); 
  }

  protected onAdd(): void {
    this.showCreateBloodComponentDialog = true;
  }

  protected async onCreateDialogClose(refreshRecord: boolean): Promise<void> {
    this.showCreateBloodComponentDialog = false;

    if (refreshRecord) {
      await this.loadrecords();
    }
  }

  protected onEdit(item: IBloodComponentSettingDto): void {
    this.editedIndex = this.records.indexOf(item);
    this.editedItem = Object.assign({}, item);
  }

  protected close(): void {
    this.editedItem = Object.assign({}, new BloodComponentSettingDto());
    this.editedIndex = -1;
  }
  
  protected async save(): Promise<void> {
    if (this.editedIndex > -1) {

      let loader = this.showLoader();
      try {
        await this.applicationSettingService.upsertBloodComponentSetting(this.editedItem);
        Object.assign(this.records[this.editedIndex], this.editedItem)
        this.notify_success('Blood component successfully updated.');
        this.close()
      }
      catch(error: any) {
        if (error.StatusCode != 500) {
          this.errorMessage = error.Message;
          this.notify_error(this.errorMessage);
        }
      }
      finally {
        loader.hide();
      }
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
      this.pagedResult = await this.applicationSettingService.getBloodComponentSettings(this.pagedSearchDto);
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