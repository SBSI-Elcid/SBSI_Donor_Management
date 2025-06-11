<template>
    <v-container fluid>
        <v-row class="pa-3">

            <v-col cols="4">
                <div @click="openScheduleModal(7)" style="cursor: pointer;">
                    <DashboardTile :count="returnNumbersWithinSevenDays" text="Number of Scheduled blood donation 7 days before the date of scheduled" vicon="mdi-account-group-outline" iconColor="primary" />
                </div>
            </v-col>

            <v-col cols="4">
                <div @click="openScheduleModal(1)" style="cursor: pointer;">
                    <DashboardTile :count="returnNumbersForTomorrow" text="Number of Scheduled blood donation 1 day before the date of scheduled" vicon="mdi-checkbox-marked-circle" iconColor="success" />
                </div>
            </v-col>

            <v-col cols="4">
                <DashboardTile :count="returnAdvocacyOrMobileCount" text="Number of scheduled mobile blood donation / advocacy" vicon="mdi-alert-circle" iconColor="warning" />
            </v-col>

        </v-row>

        <!--<v-row class="mx-2">
        <v-col cols="4" class="pa-4">
            <p class="text-center text-caption">Average No. per Blood Type</p>
            <PieChart :chart-options="chartOptions"
                      :chart-data="pieChartData"
                      :width="width"
                      :height="height" />
        </v-col>

        <v-col cols="8" class="pa-4">
            <BarChart :chart-options="chartOptions"
                      :chart-data="chartData"
                      :width="width"
                      :height="height" />
        </v-col>

    </v-row>-->
        <ScheduleModal :dateFilter="dateFilter"/>
    </v-container>
</template>

<script lang="ts">
    import { Component } from 'vue-property-decorator';
    import DashboardTile from '@/components/Common/DashboardTile.vue'
    import BarChart from '@/components/Common/BarChart.vue';
    import PieChart from '@/components/Common/PieChart.vue';
    import ScheduleModal from '@/components/Schedule/ScheduleForms/ScheduleModal.vue';
    import DashboardService from '@/services/DashboardService';
    import { InventoryCountDto, IInventoryCountDto } from '@/models/Dashboard/InventoryCountDto';
    import VueBase from '@/components/VueBase';
    import {ISchedule,ScheduleDto} from '@/models/Schedules/ScheduleDto'

    @Component({
        components: { DashboardTile, BarChart, PieChart, ScheduleModal }
    })
    export default class DashboardView extends VueBase {
        protected dashboardService: DashboardService = new DashboardService();

        protected donorCount: number = 0;
        protected schedulesCount: number = 0;
        protected schedulesList: Array<ISchedule> = [];
        protected inventoryCount: IInventoryCountDto = new InventoryCountDto();
        protected bloodTypeLabels: Array<string> = [];
        protected bloodTypeData: Array<number> = [];
        protected bloodTypeColor: Array<string> = ['#9F2233', '#AB5C67', '#D2979F', '#8B6268'];
        protected donorLabels: Array<string> = [];
        protected donorData: Array<number> = [];
        protected deferredData: Array<number> = [];
        protected dateFilter: number = 0;
        protected showSevenDayDialog: boolean = false;

        protected openScheduleModal(dateFilter: number): void {
            this.dateFilter = dateFilter;
            this.showSevenDayDialog = true;
        }

        protected chartOptions: any = {
            responsive: true,
            maintainAspectRatio: true
        }

        protected width: number = 400;
        protected height: number = 400;

        protected get returnAdvocacyOrMobileCount(): number {
            const filtered = this.schedulesList.filter(s =>
                s.ActivityType?.toLowerCase() === 'advocacy' ||
                s.ActivityType?.toLowerCase() === 'mobile'
            );

            return filtered.length;
        }
        protected get returnNumbersWithinSevenDays(): number {
            const today = new Date();
            today.setHours(0, 0, 0, 0); // normalize to 00:00:00

            const next7Days = new Date(today);
            next7Days.setDate(today.getDate() + 7);

            const upcomingSchedules = this.schedulesList.filter(s => {
                const scheduleDate = new Date(s.ScheduleDateTime);
                scheduleDate.setHours(0, 0, 0, 0); // normalize to 00:00:00
                return scheduleDate >= today && scheduleDate <= next7Days;
            });

           
            return upcomingSchedules.length;
        }

        protected get returnNumbersForTomorrow(): number {
            const today = new Date();
            today.setHours(0, 0, 0, 0); // Normalize to midnight

            const tomorrow = new Date(today);
            tomorrow.setDate(today.getDate() + 1);

            const schedulesTomorrow = this.schedulesList.filter(s => {
                const scheduleDate = new Date(s.ScheduleDateTime);
                scheduleDate.setHours(0, 0, 0, 0); // Normalize to midnight
                return scheduleDate.getTime() === tomorrow.getTime();
            });

            
            return schedulesTomorrow.length;
        }


        protected async mounted(): Promise < void> {
        let loader = this.showLoader();
        try {

            this.schedulesList = await this.dashboardService.getScheduleList();

            

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
            catch(error) {
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