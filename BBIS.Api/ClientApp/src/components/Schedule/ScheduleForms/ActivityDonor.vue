<template>
        <v-container fluid>
            <v-card>
                <v-card-actions>
                    <h2 class="ml-2 mt-1 head-title ms-4 grey--text">Activity Donors List</h2>
                    <v-spacer></v-spacer>
                    <v-btn color="default" @click ="addClick" class="mt-2 mr-2" depressed ><v-icon size="25" color="primary" left>mdi-account-plus</v-icon> Register Donor</v-btn>
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
                                            <v-text-field label="Name" dense outlined />
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

                    <v-data-table :headers="columnHeaders"
                                  :items="records"
                                  :options.sync="options"
                                  :server-items-length="dataLoaded ? pagedResult.TotalCount : 0"
                                  :loading="loading"
                                  class="elevation-2 pa-2 mt-2">

                        <template v-slot:[`progress`]>
                            <v-progress-linear color="#B92F2F" indeterminate></v-progress-linear>
                        </template>
                    </v-data-table>
                </v-card-text>

                <v-card-actions class="justify-end">
                    <v-btn color="red darken-2" @click="" class="white--text">Save</v-btn>
                    <v-btn color="red darken-2" @click="goSchedulesView" class="white--text">Back</v-btn>
                </v-card-actions>
            </v-card>

        </v-container>
</template>

<script lang="ts">
    import VueBase from '@/components/VueBase';
    import { Component, Watch, Prop, Vue } from 'vue-property-decorator';
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

    @Component()
    export default class ActivityDonor extends VueBase {
        @Prop({ default: "" }) readonly schedule_id!: string;

        @Prop({ default: false }) readonly value!: boolean;

        protected donorModule: DonorModule = getModule(DonorModule, this.$store);
        protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
        protected donorScreeningService: DonorScreeningService = new DonorScreeningService();
        protected syncDataService: SyncDataService = new SyncDataService();

        protected dataLoaded: boolean = false;
        protected loading: boolean = false;
        protected showError: boolean = false;
        protected errorMessage: string = 'Error while loading records';
        protected columnHeaders: any = [
            { text: 'Name', value: 'FullName', sortable: true },
        ];





        protected dialog: boolean = false;
        protected records: Array<IDonorListDto> = [];
        protected pagedSearchDto: DonorPagedSearchDto = new DonorPagedSearchDto();
        protected pagedResult!: PagedSearchResultDto<IDonorListDto>;
        protected options: any = {};
        protected selectedId: Guid | null = null;
        protected selectedDonorName: string = '';
        protected isSyncInProgress: boolean = false;

        @Watch('value')
        onValueChanged(val: boolean) {
            this.dialog = val;
        }



        @Watch('options')
        protected async optionChange(): Promise<void> {
            await this.loadrecords();
        }


        protected async mounted(): Promise<void> {
            this.dialog = this.value;
            await this.lookupModule.loadLookups();
        }

        protected goSchedulesView(): void {
            this.$router.push({ path: `/schedules` });

        }
        public get lookupOptions(): (key: string) => Array<ILookupOptions> {
            return (key) => this.lookupModule.getOptionsByKey(key);
        }

        protected addClick(): void {
            let route = this.$router.resolve({ path: "/activitydonorregister" });
            window.open(route.href);
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

        //protected onSyncClick(): void {
        //  this.confirm('This will Sync offline data and may take a while. Do you want to continue?', 'Sync confirm', 'Sync', 'Cancel', this.onSyncConfirm);
        //}

        //protected async onSyncConfirm(confirm: boolean): Promise<void> {
        //  if (confirm) {
        //    await this.syncDonors();
        //  }
        //}

        //protected async syncDonors(): Promise<void> {
        //  let loader = this.showLoader();
        //  try {
        //    this.isSyncInProgress = true;
        //    let message: string = await this.syncDataService.syncDonor();
        //    this.isSyncInProgress = false;

        //    if (message.includes('successfully')) {
        //      this.notify_success(message);

        //      this.loadrecords();
        //    }
        //    else {
        //      this.notify_warning(message);
        //    }
        //  }
        //  catch (error) {
        //    this.isSyncInProgress = false;
        //    console.error(error);
        //    this.notify_error('Something went wrong Synching data');
        //  }
        //  finally {
        //    loader.hide();
        //  }
        //}

        //protected async mounted(): Promise<void> {
        //    await this.lookupModule.loadLookups();
        //}
    }
</script>