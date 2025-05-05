<template>
  <v-container fluid>
    
    <v-card>
      <v-card-actions>
        <h2 class="mt-1 head-title ms-4 grey--text">Inventory</h2> 
      </v-card-actions>

      <v-card-text>
        <div class="mt-2">
					<div class="d-flex flex-row mb-0">
						<p class="caption mt-3">Filter by</p>
						<v-autocomplete 
							label="Blood Type"
							class="pl-2" 
							:items="bloodTypeOptions" 
							v-model="filterDto.BloodTypes"
							clearable deletable-chips dense multiple outlined small-chips
							placeholder="Blood Type" style="max-width: 370px;"
							/>
						<v-autocomplete 
							label="Status"
							class="pl-2 pr-2" 
							placeholder="Status" 
							:items="statusOptions"
							clearable deletable-chips dense multiple outlined small-chips
							v-model="filterDto.Statuses" style="max-width: 500px;"
						/>
						<v-text-field v-if="filterByDate" class="pl-2" type="date" label="Collection Date From" dense outlined v-model="filterDto.DateFrom" style="max-width: 200px;" />
						<v-text-field v-if="filterByDate" class="pl-2 pr-2" type="date" label="Collection Date To" dense outlined v-model="filterDto.DateTo" style="max-width: 200px;" />
						<v-checkbox v-model="filterByDate" label="Date Filter" class="mt-1 pr-4"/>
						<v-btn color="default" @click="loadRecords">
              <v-icon left color="primary">mdi-filter</v-icon> Filter
            </v-btn>
            <v-btn color="default" @click="exportData" class="ml-2">
              <v-icon size="25" color="green" left>mdi-file-excel-box-outline</v-icon> 
              Export
            </v-btn>
					</div>
        </div>
				<v-card-actions class="mb-3">
					<v-spacer></v-spacer>
          <span class="pr-2 grey--text text-caption">
            <v-icon medium color="primary">mdi-registered-trademark</v-icon> Reserved
          </span> 
          <span class="pr-2 grey--text text-caption">
            <v-icon medium color="success">mdi-checkbox-marked-circle</v-icon> Good
          </span> 
          <span class="pr-2 grey--text text-caption">
            <v-icon medium color="warning">mdi-alert-circle</v-icon> Nearly Expiration
          </span> 
          <span class="grey--text text-caption">
            <v-icon medium color="error">mdi-alert-decagram</v-icon> Expired
          </span> 
        </v-card-actions>
        
        <v-data-table
          :headers="columnHeaders"
          :items="records"
          :loading="loading"            
          class="elevation-2 pa-2 mt-2">

          <template v-slot:[`progress`]>
            <v-progress-linear color="#B92F2F" indeterminate></v-progress-linear>
          </template>

					<template v-slot:[`item.BloodType`]="{ item }">
						<span class="font-weight-bold">{{ item.BloodType }}</span>
            <span class="font-weight-bold">{{ item.BloodRh == 'Positive' ? '+' : '-' }}</span>
					</template>

					<template v-slot:[`item.CollectionDate`]="{ item }">
						{{ formatDate(item.CollectionDate) }}
					</template>

					<template v-slot:[`item.ExpiryDate`]="{ item }">
						{{ formatDate(item.ExpiryDate) }}
					</template>

          <template v-slot:[`item.Status`]="{ item }">
            <v-icon medium v-if="item.Status=='Reserved'" color="primary">mdi-registered-trademark</v-icon>
            <v-icon medium v-if="item.Status=='NearExpiry'" color="warning">mdi-alert-circle</v-icon>
            <v-icon medium v-if="item.Status=='InStock'" color="success">mdi-checkbox-marked-circle</v-icon>
            <v-icon medium v-if="item.Status=='Expired'" color="error">mdi-alert-decagram</v-icon>
          </template>
        </v-data-table>
      </v-card-text>

			
    </v-card>
 
  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { InventoryFilterDto } from '@/models/Inventory/InventoryFilterDto';
import { InventoryListDto } from '@/models/Inventory/InventoryListDto';
import InventoryService from '@/services/InventoryService';
import { Component } from 'vue-property-decorator';
import { InventoryStatus } from '@/models/Enums/InventoryStatus';
import DatePicker from '@/components/Common/FormInputs/DatePicker.vue';
import Common from '@/common/Common';

@Component({components: { DatePicker} })
export default class InventoryList extends VueBase {
  protected inventoryService: InventoryService = new InventoryService();
  protected dataLoaded: boolean = true;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected filterByDate: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'Blood Type',      value: 'BloodType',        sortable: false },
    // { text: 'Blood Rh',        value: 'BloodRh',          sortable: false },
    { text: 'Component',       value: 'ComponentName',    sortable: false },
    { text: 'Unit Serial No.', value: 'UnitSerialNumber', sortable: false },
    { text: 'No. Of Unit',     value: 'NoOfUnit',         sortable: false },
    { text: 'Collected By',    value: 'CollectedBy',      sortable: false },
    { text: 'Collection Date', value: 'CollectionDate',   sortable: false },
    { text: 'Expiration Date', value: 'ExpiryDate',       sortable: false },
    { text: 'Status',          value: 'Status',           sortable: false }
  ];

  protected statusOptions: Array<string> = [InventoryStatus.InStock, InventoryStatus.Reserved, InventoryStatus.NearExpiry, InventoryStatus.Expired];
  protected bloodTypeOptions: Array<string> = ['A+', 'A-', 'AB+', 'AB-', 'B+', 'B-', 'O+', 'O-'];

  protected records: Array<InventoryListDto> = [];
  protected filterDto: InventoryFilterDto = {
    BloodTypes: [],
    Statuses:  [],
    DateFilterType: '',
    DateFrom: null,
    DateTo: null
  };

  protected async loadRecords(): Promise<void> {
    this.loading = true;

    try {
      this.filterDto.DateFilterType = this.filterByDate ? 'collection' : '';
      this.records = await this.inventoryService.getInventoryItems(this.filterDto);
      this.loading = false;
    } catch (error) {
      console.log(error);
      this.loading = false;
    }
  }

  protected exportData(): void {
    if (this.filterDto == null || this.records.length == 0){
      return;
    }

    this.notify_info("Downloading excel file...");
    
    let loader = this.showLoader();
    this.inventoryService.exportInventory(this.filterDto)
      .then((response) => {
        let blobUrl: string = window.URL.createObjectURL(response);
        Common.startDownload(blobUrl, `Inventory_${this.formatDate(new Date())}`);
      })
      .catch((err: any) => {
        console.log(err);
      })
      .finally(() => {
        loader.hide();
      });
  }
 
  protected async mounted() : Promise<void> {
    await this.loadRecords();
  }
}
</script>