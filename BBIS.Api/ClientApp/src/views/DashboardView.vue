<template>
  <v-container fluid>
     <v-row class="pa-3">
      
        <v-col cols="3">
          <DashboardTile :count="donorCount" text="Number of Donors" vicon="mdi-account-group-outline" iconColor="primary" />
        </v-col>

        <v-col cols="3">
          <DashboardTile :count="inventoryCount.TotalItems" text="Inventory Items" vicon="mdi-checkbox-marked-circle" iconColor="success" />
        </v-col>

        <v-col cols="3">
          <DashboardTile :count="inventoryCount.TotalNearlyExpiredItems" text="Nearly Expiring Items" vicon="mdi-alert-circle" iconColor="warning" />
        </v-col>

        <v-col cols="3">
          <DashboardTile :count="inventoryCount.TotalExpiredItems" text="Expired Items" vicon="mdi-alert-decagram" iconColor="error" />
        </v-col>
       
      </v-row>

      <v-row class="mx-2">
         <v-col cols="4" class="pa-4">
          <p class="text-center text-caption">Average No. per Blood Type</p>
          <PieChart
            :chart-options="chartOptions"
            :chart-data="pieChartData"
            :width="width"
            :height="height" />
        </v-col>

        <v-col cols="8" class="pa-4">
          <BarChart
            :chart-options="chartOptions"
            :chart-data="chartData"
            :width="width"
            :height="height" />
        </v-col>
        
      </v-row>
  </v-container>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import DashboardTile from '@/components/Common/DashboardTile.vue'
import BarChart from '@/components/Common/BarChart.vue';
import PieChart from '@/components/Common/PieChart.vue';
import DashboardService from '@/services/DashboardService';
import { InventoryCountDto, IInventoryCountDto } from '@/models/Dashboard/InventoryCountDto';
import VueBase from '@/components/VueBase';

@Component({
  components: { DashboardTile, BarChart, PieChart }
})
export default class DashboardView extends VueBase { 
  protected dashboardService: DashboardService = new DashboardService();

  protected donorCount: number = 0;
  protected inventoryCount: IInventoryCountDto = new InventoryCountDto();
  protected bloodTypeLabels: Array<string> = [];
  protected bloodTypeData: Array<number> = [];
  protected bloodTypeColor: Array<string> = ['#9F2233', '#AB5C67', '#D2979F', '#8B6268'];
  protected donorLabels: Array<string> = [];
  protected donorData: Array<number> = [];
  protected deferredData: Array<number> = [];

  protected get chartData():  any {
    return {
      labels: this.donorLabels,
      datasets: [
        {
          label: 'Average No. of Donors',
          backgroundColor: '#BA5959',
          data: this.donorData
        },
        {
          label: 'Average No. of Deferred Donors',
          backgroundColor: '#2E1801',
          data: this.deferredData
        }
      ]
    }
  }

  protected get pieChartData() : any {
    return {
      labels: this.bloodTypeLabels,
      datasets: [
        {
          backgroundColor: this.bloodTypeColor,
          data: this.bloodTypeData
        }
      ]
    }
  }

  protected chartOptions: any = {
    responsive: true,
    maintainAspectRatio: true
  }

  protected width: number = 400;
  protected height: number = 400;

  protected async mounted(): Promise<void> {
    let loader = this.showLoader();
    try {
      this.donorCount = await this.dashboardService.getDonorCount();
      this.inventoryCount = await this.dashboardService.getInventoryCount();

      let bloodTypeCount = await this.dashboardService.getBloodTypeCount();
      this.bloodTypeLabels = bloodTypeCount.map(x => x.BloodType);
      this.bloodTypeData = bloodTypeCount.map(x => x.Count);

      let donorCount = await this.dashboardService.getMonthlyDonorCount()
      this.donorLabels = donorCount.Donors.map(x => x.Month);
      this.donorData = donorCount.Donors.map(x => x.Count);
      this.deferredData = donorCount.DeferredDonors.map(x => x.Count);
    }
    catch (error) {
      console.log(error);
    }
    finally {
      loader.hide();
    }
  }
}
</script>

<style lang="scss" scoped>

</style>