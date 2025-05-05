<template>
  <v-container fluid>

    <UpsertSignatory v-if="showDialog" :toggle="showDialog" :id="selectedId" :isEdit="isEdit" @onClose="onDialogClose" />

    <v-card>
      <v-card-actions>
        <h2 class="ml-2 mt-1 head-title ms-4 grey--text">Signatories</h2> 
        <v-spacer></v-spacer>
        <v-btn color="default" class="mt-2 mr-2" depressed @click="addClick"><v-icon size="25" color="primary" left>mdi-plus</v-icon> Add New Signatory </v-btn>
      </v-card-actions>
      <v-divider></v-divider>

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
          class="elevation-2 pl-2 pr-2">

          <template v-slot:[`progress`]>
            <v-progress-linear color="#B92F2F" indeterminate></v-progress-linear>
          </template>

          <template v-slot:[`item.IsActive`]="{ item }">
            <v-chip small outlined :color="item.IsActive ? 'primary' : 'default' " label >{{ item.IsActive ? 'Active' : 'Inactive' }}</v-chip>     
          </template>

          <template v-slot:[`item.actions`]="{ item }">
            <v-btn icon @click="editClick(item.SignatoryId)">
              <v-icon medium>mdi-pencil-outline</v-icon>
            </v-btn> 
            <v-btn icon @click="onDelete(item.SignatoryId)">
              <v-icon medium>mdi-delete-outline</v-icon>
            </v-btn> 
          </template>
        </v-data-table>
      
    </v-card>
 
  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { PagedSearchDto, PagedSearchResultDto } from '@/models/PagedSearchDto';
import { Component, Watch } from 'vue-property-decorator';
import UpsertSignatory from '@/components/Signatories/UpsertSignatory.vue';
import SignatoryService from '@/services/SignatoryService';
import SignatoryViewDto from '@/models/Signatories/SignatoryViewDto';

@Component({ components: { UpsertSignatory } })
export default class SignatoriesView extends VueBase {
  protected signatoryService: SignatoryService = new SignatoryService();

  protected dataLoaded: boolean = false;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'Name',             value: 'FullName',        sortable: true,   width: "20%" },
    { text: 'Position',         value: 'Position',        sortable: true,   width: "20%" },
    { text: 'Position Prefix',  value: 'PositionPrefix',  sortable: true,   width: "20%" },
    { text: 'License No.',      value: 'LicenseNo',       sortable: true,   width: "20%" },
    { text: 'Status',           value: 'IsActive',        sortable: false,  width: "10%" },
    { text: '',                 value: 'actions',         sortable: false,  width: "10%" }
  ];
  protected records: Array<SignatoryViewDto> = [];
  protected pagedSearchDto: PagedSearchDto = new PagedSearchDto();
  protected pagedResult!: PagedSearchResultDto<SignatoryViewDto>;
  protected options: any = {}; 
  protected showDialog: boolean = false;
  protected selectedId: Guid | null = null;
  protected isEdit: boolean = false;
 
  @Watch('options')
  protected async optionChange(): Promise<void> {
    await this.loadrecords(); 
  }

  protected addClick(): void {
    this.showDialog = true;
  }

  protected editClick(id: Guid): void {
    this.showDialog = true;
    this.isEdit = true;
    this.selectedId = id;
  }

  protected async onDialogClose(refreshRecord: boolean): Promise<void> {
    this.showDialog = false;
    this.selectedId = '';
    this.isEdit = false;

    if (refreshRecord) {
      await this.loadrecords();
    }
  }

  protected onDelete(signatoryId: Guid): void {
    this.selectedId = signatoryId;
    this.confirm('Are you sure you want to delete this item?', 'Delete', 'Delete', 'Cancel', this.deleteConfirm);
  }

  protected async deleteConfirm(confirm: boolean): Promise<void> {
    if (confirm && this.selectedId) {
      let loader = this.showLoader();
      try {
        await this.signatoryService.deleteSignatory(this.selectedId as Guid);
        await this.loadrecords();
        this.notify_info('Signatory successfully deleted.')
      } 
      catch (error) {
        console.log(error);
        this.notify_error('Something went wrong deleting signatory.');
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
      this.pagedResult = await this.signatoryService.getSignatories(this.pagedSearchDto);
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

  protected async mounted() : Promise<void> {
    //await this.loadrecords();
  }

}

</script>