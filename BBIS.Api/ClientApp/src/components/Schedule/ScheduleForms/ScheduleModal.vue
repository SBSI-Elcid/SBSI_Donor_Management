<template>
    <v-container fluid>
        <v-card>
            <v-card-title class="white--text" style="background-color: rgb(185, 47, 47);">
                PARTNER ACQUISITION AND RETENTION SERVICES - SCHEDULES
            </v-card-title>
            <v-data-table :headers="[  { text: 'Activity Name', value: 'ActivityName' },
                                       { text: 'Activity Type', value: 'ActivityType' },
                                       { text: 'Schedule Date', value: 'ScheduleDateTime' },
                                       { text: 'Activity Venue', value: 'ActivityVenue' },
                                       { text: 'Partner Institution', value: 'PartnerInstitutionName' },
                                       { text: 'Point Person', value: 'PointPersonName' }
                                    ]"
                          :items="sevenDayRecords"
                          class="elevation-1"
                          :loading="loading"
                          dense>

                <template v-slot:[`item.ActivityName`]="{ item }">
                    <v-btn text small color="primary" @click="openSchedule(item.ScheduleId)">
                        {{ item.ActivityName }}
                    </v-btn>
                </template>

                <template v-slot:[`item.ScheduleDateTime`]="{ item }">
                    {{ formatDate(item.ScheduleDateTime) }}
                </template>
            </v-data-table>
        </v-card>

        <CreateSchedule v-model="showCreateDialog" :isEditing="isEditing" :isDisabled ="isDisabled" :scheduleId="scheduleId" @close="handleClose" />
    </v-container>
</template>

<script lang="ts">
    import CreateSchedule from '@/components/Schedule/CreateSchedule.vue';
    import { Component, Vue, Watch, Prop } from 'vue-property-decorator';
    import { getModule } from 'vuex-module-decorators';
    import AuthModule from '../store/AuthModule';
    import VueBase from '@/components/VueBase';
    import ScheduleService from '@/services/ScheduleService';
    import { ISchedule,ScheduleDto } from '@/models/Schedules/ScheduleDto';
    import DashboardService from '@/services/DashboardService';
    import { SchedulePagedSearchDto } from '@/models/SchedulePagedSearchDto';
    import { PagedSearchResultDto } from '@/models/PagedSearchDto';
    import moment from 'moment';

    @Component({ components: { CreateSchedule } })
    export default class ScheduleModal extends VueBase {
        @Prop({ default: false }) readonly value!: boolean;

        @Prop({ default: 0 }) readonly dateFilter!: number;

        protected isDisabled: boolean = true;
        protected dashboardService: DashboardService = new DashboardService();
        protected scheduleService = new ScheduleService();
        protected showCreateDialog: boolean = false;
        protected loading: boolean = false;
        protected records: ISchedule[] = [];
        protected scheduleId: Guid | null = null;
        protected scheduleList: Array<ISchedule> = [];
        protected isEditing: boolean = false;
        protected isFromModal: boolean = false;

        protected showSevenDayDialog: boolean = false;

        protected formatDate(date: string): string {
            return moment(date).format('YYYY-MM-DD');
        }
        mounted() {
            this.fetchSchedules();
        }

        protected get sevenDayRecords(): ISchedule[] {

            const today = new Date();
            today.setHours(0, 0, 0, 0);


            const filterDays = new Date(today);
            filterDays.setDate(today.getDate() + this.dateFilter);

           
            switch (this.dateFilter) {
                case 7:
                    return this.scheduleList.filter(s => {
                        const scheduleDate = new Date(s.ScheduleDateTime);
                        scheduleDate.setHours(0, 0, 0, 0);
                        return scheduleDate >= today && scheduleDate <= filterDays ;

                    });
                    break;
                case 1:
                  
                    return this.scheduleList.filter(s => {
                    const scheduleDate = new Date(s.ScheduleDateTime);
                        scheduleDate.setHours(0, 0, 0, 0);
                      
                        return scheduleDate.getTime() === filterDays.getTime();

                });
                    break;
                default:
                    return this.scheduleList = [];
                    break;


            }
            //return this.scheduleList.filter(s => {
            //    const scheduleDate = new Date(s.ScheduleDateTime);
            //    scheduleDate.setHours(0, 0, 0, 0);
            //    return scheduleDate >= today && scheduleDate <= filterDays;

            //});
            
        }

        protected async openSchedule(scheduleId: Guid): Promise<void> {
            this.isEditing = true;
            this.scheduleId = scheduleId;
            this.isFromModal = true;
            this.showCreateDialog = true;


        }

        protected handleClose(): void {
            this.isEditing = false;
            this.scheduleId = null;
            this.showCreateDialog = false;
        }


        private async fetchSchedules(): Promise<void> {
            this.scheduleList = await this.dashboardService.getScheduleList();
        }

    }

</script>