<template>
  <v-container fluid>
    <div> 
      <v-expansion-panels class="elevation-2">
        <v-expansion-panel>
          <v-expansion-panel-header class="grey--text text-subtitle-2">Search Filters</v-expansion-panel-header>
          <v-expansion-panel-content>
            <v-row no-gutters>
              <v-col cols="2">
                <v-autocomplete 
                  label="Blood Type"
                  class="pl-2" 
                  :items="bloodTypeOptions" 
                  v-model="filterDto.BloodTypes"
                  clearable deletable-chips dense multiple outlined small-chips
                  placeholder="Blood Type" />
              </v-col>

              <v-col cols="3">
                <v-autocomplete 
                  label="Status"
                  class="pl-2" 
                  placeholder="Status" 
                  :items="statusOptions"
                  clearable deletable-chips dense multiple outlined small-chips
                  v-model="filterDto.Statuses" />
              </v-col>
              <v-col cols="2">
                <v-text-field class="pl-2" type="date" label="Donation Date From" dense outlined v-model="filterDto.DonationDateFrom" />
              </v-col>

              <v-col cols="2">
                <v-text-field class="pl-2" type="date" label="Donation Date To" dense outlined v-model="filterDto.DonationDateTo" />
              </v-col>
              <v-spacer></v-spacer>
              <v-col cols="1">
                <v-btn color="default" @click="loadRecords"><v-icon left color="primary">mdi-filter</v-icon> Filter</v-btn>
              </v-col>
              <v-col cols="1">
               <v-btn color="default" @click="exportReport"><v-icon size="25" color="green" left>mdi-file-excel-box-outline</v-icon> Export</v-btn>
              </v-col>
            </v-row>
          
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-expansion-panels>
    </div>

    <v-data-table
      :headers="columnHeaders"
      :items="records"
      :loading="loading"            
      class="elevation-2 pa-2 mt-2">

      <template v-slot:[`progress`]>
        <v-progress-linear color="blue" indeterminate></v-progress-linear>
      </template>

      <template v-slot:[`item.Status`]="{ item }">
        <v-chip small :color="statusColor(item.Status)" outlined>{{ item.Status }}</v-chip>
      </template>

      <template v-slot:[`item.BloodType`]="{ item }">
        <span class="font-weight-bold">{{ item.BloodType }}</span>
        <span class="font-weight-bold">{{ item.BloodRh == 'Positive' ? '+' : '-' }}</span>
      </template>

      <template v-slot:[`item.DateOfDonation`]="{ item }">
        {{ formatDate(item.DateOfDonation) }}
      </template>
    </v-data-table>
 
  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component } from 'vue-property-decorator';
import ReportsService from '@/services/ReportsService';
import { ExcelReportDto } from '@/models/Reports/ExcelReportDto';
import ApplicationSettingService from '@/services/ApplicationSettingService';
import Common from '@/common/Common';
import { DonorStatus } from '@/models/Enums/DonorStatus';
import { DonorReportFiltersDto } from '@/models/Reports/DonorReportFiltersDto';
import { DonorReportDto } from '@/models/Reports/DonorReportDto';

@Component({components: { }})
export default class DonorReport extends VueBase {
  protected reportsService: ReportsService = new ReportsService();
  protected appSettingService: ApplicationSettingService = new ApplicationSettingService();

  protected dataLoaded: boolean = false;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'Unit Serial No.',  value: 'UnitSerialNumber',  sortable: true  },
    { text: 'Gender',           value: 'Gender',            sortable: true  },
    { text: 'Age',              value: 'Age',               sortable: true  },
    { text: 'Blood Type',       value: 'BloodType',         sortable: true  },
    // { text: 'Blood Rh',         value: 'BloodRh',           sortable: true  },
    { text: 'Collected Amount', value: 'CollectedAmount',   sortable: true  },
    { text: 'Date Donated',     value: 'DateOfDonation',    sortable: true  },
    { text: 'Status',           value: 'Status',            sortable: true  },
  ];

  protected statusOptions: Array<string> = [DonorStatus.Success, DonorStatus.Deferred];
  protected bloodTypeOptions: Array<string> = ['A', 'AB', 'B', 'O'];

  protected records: Array<DonorReportDto> = [];
  protected filterDto: DonorReportFiltersDto = {
    BloodTypes: [],
    Statuses:  [],
    DonationDateFrom: null,
    DonationDateTo: null
  };

  protected excelReportDto: ExcelReportDto<DonorReportDto> = new ExcelReportDto<DonorReportDto>();

  protected exportReport(): void {
    if (this.filterDto == null || this.records.length == 0){
      return;
    }

    this.notify_info("Downloading excel file...");
    
    let loader = this.showLoader();
    this.reportsService.exportDonorReport(this.filterDto)
      .then((response) => {
        let blobUrl: string = window.URL.createObjectURL(response);
        Common.startDownload(blobUrl, `Donor_Report_${this.formatDate(new Date())}`);
      })
      .catch((err: any) => {
        console.log(err);
      })
      .finally(() => {
        loader.hide();
      });
  }

  protected async loadRecords(): Promise<void> {  
    this.loading = true;
    try {
      this.records = await this.reportsService.getDonorReport(this.filterDto);
    } 
    catch (error) {
      console.log(error);
    }
    finally {
      this.loading = false;
    }
  }

  protected statusColor(donorStatus: string): string {
    let color: string = 'primary';
    switch(donorStatus) {
      case DonorStatus.ForLaboratoryTest:
        color = 'warning';
        break;
      case DonorStatus.Success:
        color = 'success';
         break;
      case DonorStatus.Deferred:
        color = 'error';
        break;
      case DonorStatus.Inventory:
        color = 'grey';
        break;
    }
    return color;
  }
  
  protected async mounted() : Promise<void> {
    await this.loadRecords();
  }
  
}
</script>

<style lang="scss" scoped>
  :deep(.v-data-table > .v-data-table__wrapper tbody tr.v-data-table__expanded__content) {
    box-shadow: none;
  }
</style>