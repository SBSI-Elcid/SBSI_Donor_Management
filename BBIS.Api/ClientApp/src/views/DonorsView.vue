<template>
  <v-container fluid>

    <CreateDonorTestOrder :toggle="showCreateTestOrderDialog" :transactionId="selectedId" @onClose="onCreateDialogClose" v-if="showCreateTestOrderDialog" :donorName="selectedDonorName" />
    <UpdateDonorTestOrder :toggle="showUpdateTestOrderDialog" :transactionId="selectedId" @onClose="onUpdateDialogClose" v-if="showUpdateTestOrderDialog" :donorName="selectedDonorName" />
    <AddToInventory :toggle="showAddToInventoryDialog" :transactionId="selectedId" @onClose="onAddToInventoryClose" v-if="showAddToInventoryDialog" :donorName="selectedDonorName" />

    <v-card>
      <v-card-actions>
        <h2 class="ml-2 mt-1 head-title ms-4 grey--text">Donors</h2> 
        <v-spacer></v-spacer>
        <v-btn color="default" @click="onSyncClick" class="mr-5" v-if="!isSyncInProgress && isOffline">
          <v-icon>mdi-database-sync</v-icon> Sync Data
        </v-btn>
        <v-progress-circular v-if="isSyncInProgress" :size="55" color="primary" indeterminate class="mr-5"></v-progress-circular>
      </v-card-actions>

      <v-card-text>
        <div> 
          <v-expansion-panels class="elevation-2">
            <v-expansion-panel>
              <v-expansion-panel-header class="grey--text text-subtitle-2">Search Filters</v-expansion-panel-header>
              <v-expansion-panel-content>
                <v-row no-gutters>
                  <v-col cols="3">
                    <v-text-field label="Name" v-model.trim="pagedSearchDto.Name" dense outlined />
                  </v-col>

                  <v-col cols="2">
                    <v-text-field class="pl-2" label="Registration No." v-model.trim="pagedSearchDto.RegistrationNumber"  dense outlined />
                  </v-col>

                  <v-col cols="2">
                    <v-autocomplete class="pl-2" 
                                    label="Blood Type" 
                                    v-model="pagedSearchDto.BloodType"
                                    :items="bloodTypesOptions"
                                    clearable
                                    dense outlined />
                  </v-col>

                  <v-col cols="2">
                    <v-autocomplete class="pl-2" 
                                    label="Status" 
                                    :items="statusFilteredByRoles"
                                    v-model="pagedSearchDto.DonorStatus" 
                                    clearable
                                    dense outlined />
                  </v-col>

                  <v-spacer></v-spacer>
                  <v-col cols="1">
                    <v-btn color="default" @click="loadrecords"><v-icon color="primary" left>mdi-filter</v-icon> Filter</v-btn>
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

          <template v-slot:[`item.RegistrationNumber`]="{ item }">
            <v-btn class="pa-0 mx-0" text dense color="primary" @click="onRegNumberClick(item.DonorRegistrationId, item.DonorStatus)"> {{ item.RegistrationNumber }}</v-btn>
          </template>

          <template v-slot:[`item.DonorStatus`]="{ item }">
            <v-chip small :color="statusColor(item.DonorStatus)" outlined>{{ hasAccess(['DonorAdmin']) || hasManyRoles() ? addSpaceBetweenUpperCaseLetters(item.DonorStatus) : 'In Progress'}}</v-chip>
          </template>

          <template v-slot:[`item.BirthDate`]="{ item }">
            {{ formatDate(item.BirthDate) }}
          </template>

          <template v-slot:[`item.Actions`]="{ item }">
            <v-btn 
              title="Create Test Order"
              v-if="canCreateTestOrder(item.DonorStatus) && !item.HasTestOrder" 
              icon 
              color="primary" 
              @click="onCreateTestOrder(item)">
                <v-icon>mdi-test-tube</v-icon>
            </v-btn>

            <v-btn 
              title="Update Test Order"
              v-if="item.HasTestOrder && !item.TestOrderCompleted" 
              icon
              color="primary" 
              @click="onUpdateTestOrder(item)">
                <v-icon>mdi-test-tube-empty</v-icon>
            </v-btn>

            <v-icon v-if="item.IsOffline && !item.IsSync && isOffline" color="info" title="Offline data not sync">mdi-database-off</v-icon>
            <v-btn 
              title="Add to inventory"
              v-if="!isOffline && item.TestOrderCompleted && item.DonorStatus == 'Success'" 
              icon 
              color="primary darken-1" 
              @click="onAddToInventory(item)">
              <v-icon>mdi-database-plus</v-icon>
            </v-btn>
          </template>
        </v-data-table>
      </v-card-text>
    </v-card>
 
  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Watch } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import DonorModule from '@/store/DonorModule';
import { PagedSearchResultDto } from '@/models/PagedSearchDto';
import CreateDonorTestOrder from '@/components/TestOrder/CreateDonorTestOrder.vue';
import UpdateDonorTestOrder from '@/components/TestOrder/UpdateDonorTestOrder.vue';
import AddToInventory from '@/components/Inventory/AddToInventory.vue';
import DonorScreeningService from '@/services/DonorScreeningService';
import { IDonorListDto } from '@/models/DonorScreening/IDonorListDto';
import { DonorStatus } from '@/models/Enums/DonorStatus';
import { DonorPagedSearchDto } from '@/models/DonorScreening/DonorPagedSearchDto';
import { ILookupOptions } from '@/models/Lookups/LookupOptions';
import LookupModule from '@/store/LookupModule';
import { LookupKeys } from '@/models/Enums/LookupKeys';
import SyncDataService from '@/services/SyncDataService';

@Component({components: { CreateDonorTestOrder, UpdateDonorTestOrder, AddToInventory }})
export default class DonorsView extends VueBase {
  protected donorModule: DonorModule = getModule(DonorModule, this.$store);
  protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
  protected donorScreeningService: DonorScreeningService = new DonorScreeningService();
  protected syncDataService: SyncDataService = new SyncDataService();

  protected showCreateTestOrderDialog: boolean = false;
  protected showUpdateTestOrderDialog: boolean = false;
  protected showAddToInventoryDialog: boolean = false;
  protected dataLoaded: boolean = false;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'Donor No.', value: 'RegistrationNumber', sortable: true },
    { text: 'Name', value: 'FullName', sortable: true },
    { text: 'Birthdate', value: 'BirthDate', sortable: true },
    { text: 'Gender', value: 'Gender', sortable: true },
    { text: 'Civil Status', value: 'CivilStatus', sortable: true },
    { text: 'Contact No.', value: 'ContactNo', sortable: true },
    { text: 'Blood Type', value: 'BloodType', sortable: true },
    { text: 'Status', value: 'DonorStatus', sortable: true },
    { text: '', value: 'Actions', sortable: false }
  ];

  protected records: Array<IDonorListDto> = [];
  protected pagedSearchDto: DonorPagedSearchDto = new DonorPagedSearchDto();
  protected pagedResult!: PagedSearchResultDto<IDonorListDto>;
  protected options: any = {}; 
  protected selectedId: Guid | null = null;
  protected selectedDonorName: string= '';
  protected isSyncInProgress: boolean = false;

  @Watch('options')
  protected async optionChange(): Promise<void> {
    await this.loadrecords(); 
  }
  
  public get lookupOptions(): (key: string) => Array<ILookupOptions> {
    return (key) => this.lookupModule.getOptionsByKey(key);
  }

  protected get bloodTypesOptions(): Array<{text: string, value: string}> {
    return this.lookupOptions(LookupKeys.BloodTypes).map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get donorStatusOptions(): Array<{text: string, value: string}> {
    return this.lookupOptions(LookupKeys.DonorStatus).map(x => { return { text: x.Text, value: x.Value} });
  }

  protected get statusFilteredByRoles(): Array<{text: string, value: string}> {
    let lookUpOptions: Array<ILookupOptions> = this.lookupOptions(LookupKeys.DonorStatus);

    let statuses = this.getDonorStatusByRoles();
    return lookUpOptions.filter(x => statuses.includes(x.Value)).map(x => { return { text: x.Text, value: x.Value} });
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
      this.pagedResult = await this.donorScreeningService.getDonors(this.pagedSearchDto);
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

  protected onRegNumberClick(regId: Guid, donorStatus: string): void {
    switch(donorStatus) {
      case DonorStatus.ForInitialScreening:
        this.$router.push({ path: `/donor/initialscreening/${regId}` });
        break;
      case DonorStatus.ForPhysicalExamination:
        this.$router.push({ path: `/donor/physicalexamination/${regId}` });
        break;
      case DonorStatus.ForBloodCollection:
        this.$router.push({ path: `/donor/bloodcollection/${regId}` });
        break;
      default:
        this.$router.push({ path: `/donor/info/${regId}` });
        break;
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
  
  protected canCreateTestOrder(donorStatus: string): boolean {
    return donorStatus !== DonorStatus.ForLaboratoryTest ? false: true;
  }

  protected onCreateTestOrder(donorItem: IDonorListDto): void {
    this.selectedId = donorItem.DonorTransactionId;
    this.selectedDonorName = donorItem.FullName;
    this.showCreateTestOrderDialog = true;
  }

  protected onUpdateTestOrder(donorItem: IDonorListDto): void {
    this.selectedId = donorItem.DonorTransactionId;
    this.selectedDonorName = donorItem.FullName;
    this.showUpdateTestOrderDialog = true;
  }

  protected async onCreateDialogClose(refreshRecord: boolean): Promise<void> {
    this.showCreateTestOrderDialog = false;
    this.selectedId = null;
    this.selectedDonorName = '';

    if (refreshRecord) {
      await this.loadrecords();
    }
  }
  
  protected onAddToInventory(donorItem: IDonorListDto): void {
    this.selectedId = donorItem.DonorTransactionId;
    this.selectedDonorName = donorItem.FullName;
    this.showAddToInventoryDialog = true;
  }

  protected async onAddToInventoryClose(refreshRecord: boolean): Promise<void> {
    this.showAddToInventoryDialog = false;
    this.selectedId = null;
    this.selectedDonorName = '';

    if (refreshRecord) {
      await this.loadrecords();
    }
  }

  protected async onUpdateDialogClose(refreshRecord: boolean): Promise<void> {
    this.showUpdateTestOrderDialog = false;
    this.selectedId = null;
    this.selectedDonorName = '';

    if (refreshRecord) {
      await this.loadrecords();
    }
  }

  protected onSyncClick(): void {
    this.confirm('This will Sync offline data and may take a while. Do you want to continue?', 'Sync confirm', 'Sync', 'Cancel', this.onSyncConfirm);
  }

  protected async onSyncConfirm(confirm: boolean): Promise<void> {
    if (confirm) {
      await this.syncDonors();
    }
  }

  protected async syncDonors(): Promise<void> {
    let loader = this.showLoader();
    try {
      this.isSyncInProgress = true;
      let message: string = await this.syncDataService.syncDonor();
      this.isSyncInProgress = false;

      if (message.includes('successfully')) {
        this.notify_success(message);

        this.loadrecords();
      }
      else {
        this.notify_warning(message);
      }
    } 
    catch (error) {
      this.isSyncInProgress = false;
      console.error(error);
      this.notify_error('Something went wrong Synching data');
    }
    finally {
      loader.hide();
    }
  }

  protected async mounted() : Promise<void> {
    await this.lookupModule.loadLookups();
  }
}
</script>