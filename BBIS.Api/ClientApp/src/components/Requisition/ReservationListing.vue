<template>
<div>
    <CreateTestOrder v-if="showCreateTestOrderDialog" :toggle="showCreateTestOrderDialog" :patient="patientTypeAheadResultDto" :reservationId="selectedReservationId" @onClose="onDialogClose" />
    <UpdateTestOrder v-if="showUpdateOrderDialog" :toggle="showUpdateOrderDialog" :testOrderId="selectedTestOrderId" @onClose="onDialogClose" />
    <UpdateReservationModal v-if="showUpdateReservationDialog" :toggle="showUpdateReservationDialog" :reservationId="selectedReservationId" :currentStatus="currentReservationStatus" @onClose="onDialogClose" />
    <TransfusionModal v-if="showPostTransfusionDialog" :toggle="showPostTransfusionDialog" :reservationId="selectedReservationId" @onClose="onDialogClose"/>

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

                  <v-col cols="3">
                    <v-autocomplete class="pl-2" label="Status" placeholder="Status" 
                                    :items="statusOptions" v-model="pagedSearchDto.Statuses" 
                                    clearable deletable-chips dense multiple outlined small-chips />
                  </v-col>
                  <v-col cols="2">
                    <v-text-field class="pl-2" type="date" label="Requested Date From" dense outlined v-model="pagedSearchDto.DateFrom" />
                  </v-col>

                  <v-col cols="2">
                    <v-text-field class="pl-2" type="date" label="Requested Date To" dense outlined v-model="pagedSearchDto.DateTo" />
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
          item-key="ReservationId"
          :single-expand="true"     
          show-expand      
          class="elevation-2 pa-2 mt-2">

          <template v-slot:[`progress`]>
            <v-progress-linear color="#B92F2F" indeterminate></v-progress-linear>
          </template>

          <template v-slot:[`item.BloodType`]="{ item }">
            <span class="font-weight-bold">{{ item.BloodType }}</span>
          </template>

          <template v-slot:[`item.Components`]="{ item }">
            <v-chip class="mx-1" v-for="(comp, index) in item.Components" :key="index" small outlined color="info"> {{ comp }} </v-chip>
          </template>
          
          <template v-slot:[`item.RequestedDate`]="{ item }">
            {{ formatDate(item.RequestedDate) }}
          </template>

          <template v-slot:[`item.Status`]="{ item }">
            <v-chip class="mx-1" small outlined color="indigo"> {{ item.Status }} </v-chip>
          </template>

          <template v-slot:[`expanded-item`]="{ headers, item }">
            <td v-if="item.TestOrderSummary.length > 0" :colspan="headers.length">
              <v-row v-for="(summary, index) in item.TestOrderSummary" :key="index" class="ma-1 pl-5" no-gutters>
                <v-col cols="4">              
                  <v-chip class="ma-1" v-for="(orders, tIndex) in summary.TestOrders.filter(x => x != '')" :key="`to_${tIndex}`" small outlined :color="statusColor(summary.TestCompleted)"> {{ orders }} </v-chip>
                  <v-chip class="ma-1" v-for="(orders, cIndex) in summary.CrossMatchTestOrders" :key="`xm_${cIndex}`" small outlined :color="statusColor(summary.TestCompleted)"> {{ orders }} </v-chip>
                </v-col>
                <v-col cols="2">              
                  Completed: <span class="font-weight-bold">{{ summary.TestCompleted ? 'Yes' : 'No'}}</span>
                </v-col>
                <v-col cols="2" class="ma-n2">              
                  <v-btn title="Update Test Order" v-if="!summary.TestCompleted && canUpsertTestOrders(item)" icon color="primary" @click="onUpdateTestOrder(summary.TestOrderId)">
                    <v-icon>mdi-pencil-outline</v-icon>
                  </v-btn>
                </v-col>
              </v-row>
            </td>
            <td v-else :colspan="headers.length">
              <v-row class="ma-0 pl-5">
                <v-col cols="12" class="pa-0">              
                  <span class="pl-5 font-weight-bold">No results available.</span>
                </v-col>
              </v-row>
            </td>
          </template>
          
          <template v-slot:[`item.Actions`]="{ item }">
            <v-btn icon title="Add Test Order" v-if="canUpsertTestOrders(item)" @click="onCreateTestOrder(item)"><v-icon color="primary">mdi-test-tube</v-icon></v-btn> 
            <v-btn icon title="Update Reservation Status" v-if="canUpdateStatus(item)" @click="onUpdateReservation(item)"><v-icon color="primary">mdi-pencil-outline</v-icon></v-btn>
            <v-btn icon title="Post Transfusion Details" v-if="showTransfusionDetails(item)" @click="onClickTranfusion(item)"><v-icon color="primary">mdi-account-eye-outline</v-icon></v-btn>
          </template>

        </v-data-table>
      </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Watch } from 'vue-property-decorator';
import { PagedSearchResultDto } from '@/models/PagedSearchDto';
import { RequisitionPagedSearchDto } from '@/models/Requisitions/RequisitionPagedSearchDto';
import RequisitionsService from '@/services/RequisitionsService';
import { ReservationListingDto } from '@/models/Requisitions/ReservationListingDto';
import { TypeAheadResultDto } from '@/models/TestOrder/TypeAheadResultDto';
import CreateTestOrder from '@/components/TestOrder/CreateTestOrder.vue';
import UpdateTestOrder from '@/components/TestOrder/UpdateTestOrder.vue';
import UpdateReservationModal from '@/components/Requisition/UpdateReservationModal.vue';
import TransfusionModal from '@/components/Requisition/TransfusionModal.vue';
import { RequisitionStatus } from '@/models/Enums/RequisitionStatus';
import { Roles } from '@/models/Enums/Roles';

@Component({components: { CreateTestOrder, UpdateReservationModal, UpdateTestOrder, TransfusionModal }})
export default class ReservationListing extends VueBase {
  protected requisitionService: RequisitionsService = new RequisitionsService();

  protected dataLoaded: boolean = false;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'Patient Name',   value: 'PatientName',   sortable: true  },
    { text: 'Age',            value: 'Age',           sortable: true  },
    { text: 'Blood Type',     value: 'BloodType',     sortable: true  },
    { text: 'Components',     value: 'Components',    sortable: true  },
    { text: 'Requested Date', value: 'RequestedDate', sortable: true  },
    { text: 'Status',         value: 'Status',        sortable: true  },
    { text: '',               value: 'Actions',       sortable: false }
  ];

  protected records: Array<ReservationListingDto> = [];
  protected pagedSearchDto: RequisitionPagedSearchDto = new RequisitionPagedSearchDto();
  protected pagedResult!: PagedSearchResultDto<ReservationListingDto>;
  protected options: any = {}; 
  protected patientTypeAheadResultDto: TypeAheadResultDto | null = null;

  protected showCreateTestOrderDialog: boolean = false;
  protected showUpdateOrderDialog: boolean = false;
  protected showUpdateReservationDialog: boolean = false;
  protected showLinkOrderDialog: boolean = false;
  protected showPostTransfusionDialog: boolean = false;

  protected selectedTestOrderId: Guid = '';
  protected selectedReservationId: Guid = '';
  protected currentReservationStatus: string = '';
  protected linkPatientId: Guid = '';
  protected expanded: Array<any> = [];
  protected statusOptions: Array<string> = [RequisitionStatus.Received, RequisitionStatus.InProgress, RequisitionStatus.ForTransfusion, RequisitionStatus.Done, RequisitionStatus.Cancelled];
  
  @Watch('options')
  protected async optionChange(): Promise<void> {
    await this.loadrecords(); 
  }

  protected canUpsertTestOrders(item: ReservationListingDto): boolean {
    const hasAccess = this.hasAccess([Roles.MedTech]);
    return (item.Status === RequisitionStatus.Received || item.Status === RequisitionStatus.InProgress) && hasAccess;
  } 

  protected canUpdateStatus(item: ReservationListingDto): boolean {
    return item.Status === RequisitionStatus.Received || item.Status === RequisitionStatus.InProgress || item.Status === RequisitionStatus.ForTransfusion;
  } 

  protected showTransfusionDetails(item: ReservationListingDto): boolean {
    return item.Status === RequisitionStatus.ForTransfusion || item.Status == RequisitionStatus.Done;
  } 

  protected statusColor(isCompleted: boolean): string {
    return isCompleted ? 'success darken-2' : 'info';
  }

  protected onUpdateTestOrder(testOrderId: Guid): void {
    this.selectedTestOrderId = testOrderId;
    if (testOrderId) {
      this.showUpdateOrderDialog = true;
    }
  }

  protected onCreateTestOrder(item: ReservationListingDto): void {
    this.patientTypeAheadResultDto = { Id: item.PatientId, Name: item.PatientName, BloodType: item.BloodType };
    this.selectedReservationId = item.ReservationId;
    this.showCreateTestOrderDialog = true;
  }

  protected onUpdateReservation(item: ReservationListingDto): void {
    this.selectedReservationId = item.ReservationId;
    this.currentReservationStatus = item.Status
    this.showUpdateReservationDialog = true;
  }

  protected onLinkReservation(item: ReservationListingDto): void {
    this.selectedReservationId = item.ReservationId;
    this.linkPatientId = item.PatientId;
    this.showLinkOrderDialog = true;
  }

  protected onLinkReservationClose(refresh: boolean): void {
    this.showLinkOrderDialog = false;
    this.selectedReservationId = '';
    this.linkPatientId = '';

    if(refresh) {
      this.loadrecords();
    }
  }

  protected onClickTranfusion(item: ReservationListingDto): void {
    this.selectedReservationId = item.ReservationId;
    this.showPostTransfusionDialog = true;
  }

  protected onDialogClose(refresh: boolean): void {
    this.patientTypeAheadResultDto = null;
    this.selectedReservationId = '';
    this.selectedTestOrderId = '';
    this.currentReservationStatus = '';

    this.showCreateTestOrderDialog = false;
    this.showUpdateOrderDialog = false;
    this.showUpdateReservationDialog = false;
    this.showPostTransfusionDialog = false;

    if(refresh) {
      this.loadrecords();
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
      this.pagedResult = await this.requisitionService.getReservations(this.pagedSearchDto);
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

<style lang="scss" scoped>
  :deep(.v-data-table > .v-data-table__wrapper tbody tr.v-data-table__expanded__content) {
    box-shadow: none;
  }
</style>