<template>
  <div>
    <UpdateTestOrder :toggle="showUpdateOrderDialog" :testOrderId="selectedId" v-if="showUpdateOrderDialog" @onClose="onDialogClose" />

    <v-card>      
      <v-card-text>
        <div>
          <v-expansion-panels class="elevation-2">
            <v-expansion-panel>
              <v-expansion-panel-header class="grey--text text-subtitle-2">Search Filters</v-expansion-panel-header>
              <v-expansion-panel-content>
                <v-row no-gutters>
                  <v-col cols="3">
                    <v-text-field type="text" label="Patient Name" v-model.trim="pagedSearchDto.SearchText" dense outlined />
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
          :expanded.sync="expanded"
          item-key="TestOrderNumber"
          :single-expand="true"     
          show-expand
          class="elevation-2 pa-2 mt-2">

          <template v-slot:[`progress`]>
            <v-progress-linear color="#B92F2F" indeterminate></v-progress-linear>
          </template>

          <template v-slot:[`item.TestOrdersResult`]="{ item }">
            <v-chip class="mx-1" v-for="type in item.TestOrdersResult" :key="type.TestOrderName" small outlined :color="statusColor(item.TestCompleted)"> {{ type.TestOrderName }} </v-chip>
          </template>

          <template v-slot:[`item.TestCompleted`]="{ item }">
            {{ item.TestCompleted ? 'Yes' : 'No'}}
          </template>

          <template v-slot:[`item.DateCreated`]="{ item }">
            {{ formatDate(item.DateCreated) }}
          </template>

          <template v-slot:[`item.Actions`]="{ item }">
            <v-btn title="Update Test Order" v-if="!item.TestCompleted" icon color="primary" @click="onUpdateTestOrder(item)">
              <v-icon>mdi-pencil-outline</v-icon>
            </v-btn>
            <v-btn title="Print barcode"  v-if="item.CrossMatchTestOrdersResult.length > 0" icon color="default darken-1" @click="onBarcodePrintClick(item)">
              <v-icon>mdi-barcode</v-icon>
            </v-btn>
            <v-btn title="Print report" icon color="default darken-1" @click="onDownloadClick(item)">
              <v-icon>mdi-printer-outline</v-icon>
            </v-btn>
          </template>

          <template v-slot:[`expanded-item`]="{ headers, item }">
            <td :colspan="headers.length">
              <v-row v-for="result in filterTestOrders(item.TestOrdersResult)" :key="result.TestOrderTypeId" class="ma-1 pl-5" no-gutters>
                <v-col cols="3"> 
                  <v-chip class="mx-1" small outlined :color="statusColor(item.TestCompleted)"> {{result.TestOrderName}} </v-chip>
                </v-col>
                <v-col cols="9" style="white-space: pre">{{showResult(result.Result)}}</v-col>
              </v-row>

              <v-row v-for="(result, index) in item.CrossMatchTestOrdersResult" :key="index" class="ma-1 pl-5" no-gutters>
                <v-col cols="3"> 
                  <v-chip class="mx-1" small outlined :color="statusColor(item.TestCompleted)"> 
                    {{ `Cross Match -` + result.CrossMatchType + ' (' + result.DonorUnitSerialNumber + ')' }} 
                  </v-chip>
                </v-col>
                <v-col cols="9">{{showResult(result.Result)}}</v-col>
              </v-row>
            </td>
          </template>

        </v-data-table>
      </v-card-text>
    </v-card>
    
    <div id="barcodePrint" class="hide-me">
      <div v-if="barcodeResults.length > 0" class="text-center">
        <div style="padding: 4px;" v-for="(item, index) in barcodeResults" :key="index">
          <span v-html="barcodeElement(item.Barcode, item.BarcodeText)"></span>
        </div>
      </div>
    </div>

    <div id="testOrderReport" class="hide-me text-center">
      <TestOrderReport :testOrder="reportData" v-if="reportData" />
    </div>
 
  </div>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Watch } from 'vue-property-decorator';
import UpdateDonorTestOrder from '@/components/TestOrder/UpdateDonorTestOrder.vue';
import { PagedSearchResultDto } from '@/models/PagedSearchDto';
import TestOrderService from '@/services/TestOrderService';
import { IRequestedTestOrderListDto } from '@/models/TestOrder/IRequestedTestOrderListDto';
import UpdateTestOrder from '@/components/TestOrder/UpdateTestOrder.vue';
import { TestOrderTypeEnum } from '@/models/TestOrderTypeEnum';
import { IRequestedTestOrderDto } from '@/models/TestOrder/IRequestedTestOrderDto';
import Common from '@/common/Common';
import TestOrderReport from '@/components/Reports/TestOrderReport.vue'
import ReportsService from '@/services/ReportsService';
import { TestOrderReportDto } from '@/models/Reports/TestOrderReportDto';
import { BarcodeInfoDto } from '@/models/Reports/BarcodeInfoDto';
import { BarcodeResultDto } from '@/models/Reports/BarcodeResultDto';
import { RequestedTestOrderPagedSearchDto } from '@/models/TestOrder/RequestedTestOrderPagedSearchDto';

@Component({components: { UpdateDonorTestOrder, UpdateTestOrder, TestOrderReport }})
export default class RequestedTestOrders extends VueBase {

  protected testOrderService: TestOrderService = new TestOrderService();
  protected reportsService: ReportsService = new ReportsService();

  protected dataLoaded: boolean = false;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'Test Order No.', value: 'TestOrderNumber',   sortable: true},
    { text: 'Patient Name',   value: 'PatientName',       sortable: true},
    { text: 'Test Orders',    value: 'TestOrdersResult',  sortable: true},
    { text: 'Completed',      value: 'TestCompleted',     sortable: true},
    { text: 'Created Date',   value: 'DateCreated',       sortable: true},
    { text: '',               value: 'Actions',           sortable: false}
  ];

  protected records: Array<IRequestedTestOrderListDto> = [];
  protected pagedSearchDto: RequestedTestOrderPagedSearchDto = new RequestedTestOrderPagedSearchDto();
  protected pagedResult!: PagedSearchResultDto<IRequestedTestOrderListDto>;
  protected options: any = {}; 
  protected selectedId: Guid | null = null;
  protected selectedDonorName: string= '';
  protected showCreateTestOrderDialog: boolean = false;
  protected showUpdateOrderDialog: boolean = false;
  protected TestOrderTypeEnum = TestOrderTypeEnum;
  protected donorUnitSerialNumber: string = '';
  protected barcodeTopLabel: string = '';
  protected barcodeBottomLabel: string = '';
  protected expanded: Array<any> = [];
  protected toPrintTestOrder: IRequestedTestOrderListDto | null = null;
  protected reportData: TestOrderReportDto | null = null;
  protected barcodeResults: Array<BarcodeResultDto> = [];
  
  @Watch('options')
  protected async optionChange(): Promise<void> {
    await this.loadrecords(); 
  }

  protected statusColor(isCompleted: boolean): string {
    return isCompleted ? 'success darken-2' : 'info';
  }

  protected filterTestOrders(testOrders: Array<IRequestedTestOrderDto>) : Array<IRequestedTestOrderDto> {
    return testOrders.filter(x => !x.TestOrderName.includes("Compatibility Test"));
  }

  protected showResult(result: string): string {
    return Common.hasValue(result) ? result : 'No results available.';
  }

  protected createTestOrder(): void {
    this.showCreateTestOrderDialog = true;
  }

  protected updateTestOrder(): void {
    this.showUpdateOrderDialog = true;
  }

  protected onUpdateTestOrder(tesorder: IRequestedTestOrderListDto): void {
    this.selectedId = tesorder.TestOrderId;
    this.showUpdateOrderDialog = true;
  }

  protected async onBarcodePrintClick(item: IRequestedTestOrderListDto): Promise<void> {
  
    let barcodeInfo: Array<BarcodeInfoDto> = [{ BarcodeText: item.TestOrderNumber, TopText: item.PatientName, BottomText: '' }];

    if(item.CrossMatchTestOrdersResult.length > 0) {
      for(var i = 0; i < item.CrossMatchTestOrdersResult.length; i++) {
        barcodeInfo.push({ 
          BarcodeText: item.CrossMatchTestOrdersResult[i].DonorUnitSerialNumber, 
          TopText: `XMatch - ${item.CrossMatchTestOrdersResult[i].CrossMatchType}`, 
          BottomText: '' })
      }
    }

    this.barcodeResults = await this.reportsService.generateBarcodes({ Items: barcodeInfo });
    
    setTimeout(() => {
      this.$htmlToPaper('barcodePrint');
    }, 1000);
  }

  protected barcodeElement(barcode: string, text: string): string {
    return `<img class="mx-0" id="${text}" src="${barcode}" alt="${text}" />`;
  }

  protected async onDownloadClick(item: IRequestedTestOrderListDto): Promise<void> {
    this.notify_info("Downloading report...");
    this.selectedId = item.TestOrderId;
    this.reportData = null;
    await this.$nextTick();

    this.reportData = await this.reportsService.getTestOrderReport(this.selectedId);
    await this.$nextTick();
    this.$htmlToPaper('testOrderReport');
  }

  protected async onDialogClose(refresh: boolean): Promise<void> {
    this.showCreateTestOrderDialog = false;
    this.showUpdateOrderDialog = false;
    if(refresh) {
      await this.loadrecords();
    }
  }

  protected async onUpdateDialogClose(refreshRecord: boolean): Promise<void> {
    this.showUpdateOrderDialog = false;
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
      this.pagedResult = await this.testOrderService.getRequestedTestOrders(this.pagedSearchDto);
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

<style lang="scss" scoped>
  :deep(.v-data-table > .v-data-table__wrapper tbody tr.v-data-table__expanded__content) {
    box-shadow: none;
  }
</style>