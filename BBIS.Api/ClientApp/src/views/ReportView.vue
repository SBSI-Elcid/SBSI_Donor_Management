<template>
  <v-container fluid>
    <v-card>
      <v-card-actions>
        <h2 class="ml-2 mt-1 head-title ms-4 grey--text">Reports</h2> 
      </v-card-actions>
      <v-divider></v-divider>

      <div>
        <!-- TABS -->
        <v-tabs class="block-tab" v-model="currentTab">
          <v-tab :key="0">Donor Report</v-tab>
        </v-tabs>

        <v-tabs-items v-model="currentTab">
          <v-tab-item :key="0">
            <DonorReport v-if="currentTab === 0" />
          </v-tab-item>
        </v-tabs-items>
      </div>
    </v-card>
 
  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component } from 'vue-property-decorator';
import BarcodeFormat from '@/components/Reports/BarcodeFormat.vue';
import DonorReport from '@/components/Reports/DonorReport.vue';

@Component( { components: {BarcodeFormat, DonorReport} })
export default class ReportView extends VueBase {
  protected currentTab: number = 0;

}

</script>