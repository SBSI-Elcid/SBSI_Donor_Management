<template>
  <v-container fluid>

      <v-card class="">
          <v-card-actions>
              <h2 class="ml-2 mt-1 head-title ms-4 grey--text">Donor Registrations</h2>
              <v-spacer></v-spacer>
              <v-btn color="default" class="mt-2 mr-2" depressed @click="addClick"><v-icon size="25" color="primary" left>mdi-account-plus</v-icon> Register Donor</v-btn>
          </v-card-actions>

          <!--<v-row no-gutters class="mx-3 pt-3">
        <v-col md="3" sm="9">
            <v-text-field type="text" label="Search" v-model.trim="pagedSearchDto.SearchText" dense outlined />
        </v-col>

        <v-col md="2" sm="2" class="pl-2">
            <v-btn color="default" @click="loadrecords" depressed><v-icon size="25" color="primary" left>mdi-magnify</v-icon> Search</v-btn>
        </v-col>

        <v-col md="2" sm="2" class="pl-2">
            <v-btn color="default" @click="loadrecords" depressed><v-icon size="25" color="primary" left>mdi-refresh</v-icon> Refresh</v-btn>
        </v-col>-->
          <!--<v-col md="2" cla">
        <v-btn color="default" class="" @click="loadrecords">
            <v-icon color="primary" size="25">mdi-refresh</v-icon> Refresh
        </v-btn>
    </v-col>-->
          <!--</v-row>-->

          <v-row no-gutters class="mx-3 pt-3">
              <v-col md="3" sm="9">
                  <v-text-field type="text"
                                label="Search"
                                v-model.trim="pagedSearchDto.SearchText"
                                dense
                                outlined />
              </v-col>

              <v-col md="auto" sm="auto" class="pl-1">
                  <v-btn color="default" @click="loadrecords" depressed>
                      <v-icon size="25" color="primary" left>mdi-magnify</v-icon>
                      Search
                  </v-btn>
              </v-col>

              <v-col md="auto" sm="auto" class="pl-1">
                  <v-btn color="default" @click="loadrecords" depressed>
                      <v-icon size="25" color="primary" left>mdi-refresh</v-icon>
                      Refresh
                  </v-btn>
              </v-col>
          </v-row>







          <v-data-table :headers="columnHeaders"
                        :items="records"
                        :options.sync="options"
                        :server-items-length="dataLoaded ? pagedResult.TotalCount : 0"
                        :loading="loading"
                        class="elevation-2 pl-2 pr-2">

              <template v-slot:[`progress`]>
                  <v-progress-linear color="#B92F2F" indeterminate></v-progress-linear>
              </template>

              <template v-slot:[`item.RegistrationNumber`]="{ item }">
                  <v-btn class="pa-0 mx-0" text dense color="primary" @click="onRegNumberClick(item.DonorRegistrationId)"> {{ item.RegistrationNumber }}</v-btn>
              </template>

              <template v-slot:[`item.RegistrationDate`]="{ item }">
                  <span>{{ formatDate(item.RegistrationDate) }}</span>
              </template>

          </v-data-table>

      </v-card>
 
  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { PagedSearchDto, PagedSearchResultDto } from '@/models/PagedSearchDto';
import { Component, Watch } from 'vue-property-decorator';
import DonorRegistrationService from '@/services/DonorRegistrationService';
import { IRegisteredDonorDto } from '@/models/DonorRegistration/IRegisteredDonorDto';

@Component({ components: {} })
export default class RegistrationsView extends VueBase {
  protected donorRegistrationService: DonorRegistrationService = new DonorRegistrationService();

  protected dataLoaded: boolean = false;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'Registration No.', value: 'RegistrationNumber', sortable: true },
    { text: 'Name', value: 'FullName', sortable: true },
    { text: 'Civil Status', value: 'CivilStatus', sortable: true },
    { text: 'Gender', value: 'Gender', sortable: true },
    { text: 'Date Register', value: 'RegistrationDate', sortable: true }
  ];
  protected records: Array<IRegisteredDonorDto> = [];
  protected pagedSearchDto: PagedSearchDto = new PagedSearchDto();
  protected pagedResult!: PagedSearchResultDto<IRegisteredDonorDto>;
  protected options: any = {}; 
  protected selectedId: Guid | null = null;
 
  @Watch('options')
  protected async optionChange(): Promise<void> {
    await this.loadrecords(); 
  }

  protected addClick(): void {
    let route = this.$router.resolve({ path: "/register" });
    window.open(route.href);
  }

  protected onRegNumberClick(regId: Guid): void {
    this.$router.push({ path: `/donor/info/${regId}` });
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
      this.pagedResult = await this.donorRegistrationService.getRegisteredDonors(this.pagedSearchDto);
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