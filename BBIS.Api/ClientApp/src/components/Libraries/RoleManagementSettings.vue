<template>
    <v-container fluid>
        <v-card>
            <v-row class="mx-3 pt-3">
                <v-col cols="6">
                    <v-text-field class="pr-2" type="text" label="Search" v-model.trim="pagedSearchDto.SearchText" dense outlined />
                </v-col>
                <v-col cols="2">
                    <v-btn class="pr-2" color="default" @click="loadrecords" depressed><v-icon left>mdi-magnify</v-icon> Search</v-btn>
                </v-col>
            </v-row>

            <v-data-table :headers="columnHeaders"
                          :items="records"
                          :options.sync="options"
                          :server-items-length="dataLoaded ? pagedResult.TotalCount : 0"
                          :loading="loading"
                          class="elevation-2 pl-2 pr-2">

                <template v-slot:[`item.SettingKey`]="{ item }">
                    {{addSpaceBetweenUpperCaseLetters(item.SettingKey)}}
                </template>

                <template v-slot:[`item.SettingValue`]="{ item }">
                    <v-text-field v-if="item.ApplicationSettingId === editedItem.ApplicationSettingId"
                                  v-model="editedItem.SettingValue"
                                  hide-details="auto"
                                  :rules="[rules.required, rules.maxLength(100)]"
                                  outlined dense />
                    <span v-else>{{item.SettingValue}}</span>
                </template>

                <template v-slot:[`item.IsActive`]="{ item }">
                    <v-switch flat v-if="item.ApplicationSettingId === editedItem.ApplicationSettingId" v-model="editedItem.IsActive" />
                    <v-chip v-else small outlined :color="item.IsActive ? 'primary' : 'default' " label>{{ item.IsActive ? 'Active' : 'Inactive' }}</v-chip>
                </template>

                <template v-slot:[`item.Actions`]="{ item }">
                    <div v-if="item.ApplicationSettingId === editedItem.ApplicationSettingId">
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

<script lang="ts">import Common from '@/common/Common';
import VueBase from '@/components/VueBase';
import { ApplicationSettingDto, IApplicationSettingDto } from '@/models/ApplicationSetting/ApplicationSettingDto';
import { PagedSearchDto, PagedSearchResultDto } from '@/models/PagedSearchDto';
import ApplicationSettingService from '@/services/ApplicationSettingService';
import { Component, Watch } from 'vue-property-decorator';

@Component
export default class RoleManagementSettings extends VueBase {
  protected applicationSettingService: ApplicationSettingService = new ApplicationSettingService();
  protected dataLoaded: boolean = false;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'Setting Key', value: 'SettingKey', sortable: true, width: '40%' },
    { text: 'Setting Value', value: 'SettingValue', sortable: false, width: '40%' },
    { text: 'Status', value: 'IsActive', sortable: false, width: '10%' },
    { text: '', value: 'Actions', sortable: false, width: '10%' }
  ];
  protected records: Array<IApplicationSettingDto> = [];
  protected pagedSearchDto: PagedSearchDto = new PagedSearchDto();
  protected pagedResult!: PagedSearchResultDto<IApplicationSettingDto>;
  protected options: any = {};
  protected selectedId: Guid | null = null;
  protected editedIndex: number = -1;
  protected editedItem: IApplicationSettingDto = new ApplicationSettingDto();
  protected rules: any = {...Common.ValidationRules }

  @Watch('options')
  protected async optionChange(): Promise<void> {
    await this.loadrecords();
  }

  protected onEdit(item: IApplicationSettingDto): void {
    this.editedIndex = this.records.indexOf(item);
    this.editedItem = Object.assign({}, item);
  }

  protected close(): void {
    this.editedItem = Object.assign({}, new ApplicationSettingDto());
    this.editedIndex = -1;
  }

  protected async save(): Promise<void> {
    if (this.editedIndex > -1) {

      let loader = this.showLoader();
      try {
        await this.applicationSettingService.updateApplicationSetting(this.editedItem);
        Object.assign(this.records[this.editedIndex], this.editedItem)
        this.notify_success('Application Setting successfully updated.');
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
      this.pagedResult = await this.applicationSettingService.getApplicationSettings(this.pagedSearchDto);
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

}</script>