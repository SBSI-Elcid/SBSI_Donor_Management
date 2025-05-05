<template>
  <div>
    
    <UpdateDonorTestOrder :toggle="showUpdateTestOrderDialog" :transactionId="selectedId" @onClose="onUpdateDialogClose" v-if="showUpdateTestOrderDialog" :donorName="selectedDonorName" />
    
    <v-card>      
      <v-card-text>
        <div>
          <v-expansion-panels class="elevation-2">
            <v-expansion-panel>
              <v-expansion-panel-header class="grey--text text-subtitle-2">Search Filters</v-expansion-panel-header>
              <v-expansion-panel-content>
                <v-row no-gutters>
                  <v-col cols="3">
                    <v-text-field type="text" label="Donor Name/Registration Number" v-model.trim="pagedSearchDto.SearchText" dense outlined />
                  </v-col>

                  <v-col cols="2">
                    <v-text-field class="pl-2" type="date" label="Created Date From" dense outlined v-model="pagedSearchDto.DateFrom" />
                  </v-col>

                  <v-col cols="2">
                    <v-text-field class="pl-2" type="date" label="Created Date To" dense outlined v-model="pagedSearchDto.DateTo" />
                  </v-col>
                  <v-spacer></v-spacer>
                  <v-col cols="1">
                    <v-btn color="default" @click="loadrecords"><v-icon left color="primary">mdi-filter</v-icon> Filter</v-btn>
                  </v-col>
                </v-row>
              
              </v-expansion-panel-content>
            </v-expansion-panel>
          </v-expansion-panels>
        </div>

        <v-data-table
          :headers="columnHeaders"
          :items="records"
          :options.sync="options"
          :server-items-length="dataLoaded ? pagedResult.TotalCount : 0"
          :loading="loading"
          class="elevation-2 pa-2 mt-2">

          <template v-slot:[`progress`]>
            <v-progress-linear color="#B92F2F" indeterminate></v-progress-linear>
          </template>

          <template v-slot:[`item.TestTypes`]="{ item }">
            <v-chip class="pa-2" v-for="type in item.TestTypes" :key="type.TestTypeName" small :color="statusColor(type.IsReactive, item.TestCompleted)" outlined> {{ type.TestTypeName }} </v-chip>
          </template>

          <template v-slot:[`item.IsReactive`]="{ item }">
            {{ item.IsReactive ? 'Yes' : 'No'}}
          </template>

          <template v-slot:[`item.TestCompleted`]="{ item }">
            {{ item.TestCompleted ? 'Yes' : 'No'}}
          </template>

          <template v-slot:[`item.DateCreated`]="{ item }">
            {{ formatDate(item.DateCreated) }}
          </template>

          <template v-slot:[`item.Actions`]="{ item }">
            <v-btn  title="Update Test Order"  v-if="!item.TestCompleted" icon color="primary" @click="onUpdateTestOrder(item)">
              <v-icon>mdi-test-tube-empty</v-icon>
            </v-btn>
          </template>
        </v-data-table>
      </v-card-text>
    </v-card>
 
  </div>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Watch } from 'vue-property-decorator';
import UpdateDonorTestOrder from '@/components/TestOrder/UpdateDonorTestOrder.vue';
import { PagedSearchResultDto } from '@/models/PagedSearchDto';
import { IDonorTestOrderListDto } from '@/models/TestOrder/DonorTestOrderListDto';
import TestOrderService from '@/services/TestOrderService';
import ReportsService from '@/services/ReportsService';
import Common from '@/common/Common';
import { DonorTestOrderPagedSearchDto } from '@/models/TestOrder/DonorTestOrderPagedSearchDto';

@Component({components: { UpdateDonorTestOrder }})
export default class DonorTestOrders extends VueBase {
  protected testOrderService: TestOrderService = new TestOrderService();
  protected reportsService: ReportsService = new ReportsService();

  protected dataLoaded: boolean = false;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'Donor No.',        value: 'RegistrationNumber',  sortable: true,   width: '10%' },
    { text: 'Name',             value: 'FullName',            sortable: true,   width: '20%' },
    { text: 'Blood Type',       value: 'FinalBloodType',      sortable: true,   width: '10%' },
    { text: 'Tests Requested',  value: 'TestTypes',           sortable: true,   width: '30%' },
    { text: 'Reactive',         value: 'IsReactive',          sortable: true,   width: '10%' },
    { text: 'Completed',        value: 'TestCompleted',       sortable: true,   width: '10%' },
    { text: 'Created Date',     value: 'DateCreated',         sortable: true,   width: '10%'},
    { text: '',                 value: 'Actions',             sortable: false,  width: '5%'  }
  ];

  protected records: Array<IDonorTestOrderListDto> = [];
  protected pagedSearchDto: DonorTestOrderPagedSearchDto = new DonorTestOrderPagedSearchDto();
  protected pagedResult!: PagedSearchResultDto<IDonorTestOrderListDto>;
  protected options: any = {}; 

  protected showUpdateTestOrderDialog: boolean = false;
  protected selectedId: Guid | null = null;
  protected selectedDonorName: string= '';
  
  @Watch('options')
  protected async optionChange(): Promise<void> {
    await this.loadrecords(); 
  }

  protected statusColor(isReactive: boolean, isCompleted: boolean): string {
    if (!isCompleted) {
      return 'info';
    }

    return isReactive ? 'error' : 'success darken-2';
  }

  protected onUpdateTestOrder(donorItem: IDonorTestOrderListDto): void {
    this.selectedId = donorItem.DonorTransactionId;
    this.selectedDonorName = donorItem.FullName;
    this.showUpdateTestOrderDialog = true;
  }

  protected async onUpdateDialogClose(refreshRecord: boolean): Promise<void> {
    this.showUpdateTestOrderDialog = false;
    this.selectedId = null;
    this.selectedDonorName = '';

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
      this.pagedResult = await this.testOrderService.getDonorTestOrderList(this.pagedSearchDto);
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

  protected exportReport(): void {

    if(this.selectedId == null) {
      return;
    }
    
    this.notify_info("Downloading report...");
    
    let loader = this.showLoader();
    this.reportsService.exportTestOrderReport(this.selectedId)
      .then((res: Response) => {
        return res.blob();
      })
      .then((blob: Blob) => {
        let blobUrl: string = window.URL.createObjectURL(blob);
        Common.startDownload(blobUrl, `TestOrderReport_${this.formatDate(new Date())}`);
      })
      .catch((err: any) => {
        console.log(err);
      })
      .finally(() => {
        loader.hide();
      });
  }
  
}
</script>