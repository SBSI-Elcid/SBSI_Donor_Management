<template>
    <v-container fluid>
        <v-row justify="center">
            <v-col cols="12" md="12">
                <v-card>
                    <v-card-title class="red white--text">
                        PARTNER ACQUISITION AND RETENTION SERVICES - SCHEDULES
                    </v-card-title>

                    <v-card-actions>
                        <v-btn color="red darken-2" class="white--text" @click="createSchedule">Create Schedule</v-btn>
                        <!--<v-btn color="red darken-2" class="white--text" @click="">Edit Schedule</v-btn>
                    <v-btn color="red darken-2" class="white--text" @click="">Cancel Schedule</v-btn>-->
                    </v-card-actions>

                    <v-data-table :headers="[
                                              { text: 'Activity Name', value: 'ActivityName' },
                                              { text: 'Activity Type', value: 'ActivityType' },
                                              { text: 'Schedule Date', value: 'ScheduleDateTime' },
                                              { text: 'Activity Venue', value: 'ActivityVenue' },
                                              { text: 'Partner Institution', value: 'PartnerInstitutionName' },
                                              { text: 'Point Person', value: 'PointPersonName' }
                                            ]"
                                  :items="records"
                                  class="elevation-1"
                                  :loading="loading"
                                  dense>

                        <template v-slot:[`item.ActivityName`]="{ item }">
                            <v-btn text small color="primary" @click="openSchedule(item.ScheduleId)">
                                {{ item.ActivityName }}
                            </v-btn>
                        </template>
                    </v-data-table>
                </v-card>
            </v-col>
        </v-row>

        <CreateSchedule v-model="showCreateDialog" :isEditing="isEditing" :scheduleId="scheduleId" @close="handleClose" />
        <router-view />
    </v-container>
</template>

<script lang="ts">
    import CreateSchedule from '../components/Schedule/CreateSchedule.vue';
    import { Component, Vue, Watch } from 'vue-property-decorator';
    import { getModule } from 'vuex-module-decorators';
    import AuthModule from '../store/AuthModule';
    import VueBase from '@/components/VueBase';
    import ScheduleService from '@/services/ScheduleService';
    import { ISchedule } from '@/models/ScheduleDto';
    import { SchedulePagedSearchDto } from '@/models/SchedulePagedSearchDto';
    import { PagedSearchResultDto } from '@/models/PagedSearchDto';
    import moment from 'moment';

    @Component({ components: { CreateSchedule } })
    export default class SchedulesView extends VueBase {
        protected scheduleService = new ScheduleService();
        protected showCreateDialog: boolean = false;
        protected loading: boolean = false;
        protected records: ISchedule[] = [];
        protected scheduleId: Guid | null = null;
        protected pagedResult!: PagedSearchResultDto<ISchedule>;
        protected isEditing: boolean = false;
        private options: any = {
            page: 1,
            itemsPerPage: 10,
            sortBy: [],
            sortDesc: []
        };

        mounted() {
            this.fetchSchedules();
        }

        protected createSchedule(): void {
            this.isEditing = false;
            this.scheduleId = null;
            this.showCreateDialog = true;
           
        }

        protected async openSchedule(scheduleId: Guid): Promise<void> {
            this.isEditing = true;
            this.scheduleId = scheduleId;
            this.showCreateDialog = true;


        }

        protected handleClose(): void {
            this.isEditing = false;
            this.scheduleId = null;
            this.showCreateDialog = false;
        }

        private async fetchSchedules(): Promise<void> {
            const { page, itemsPerPage, sortBy, sortDesc } = this.options;

            const dto: SchedulePagedSearchDto = {
                PageNumber: page,
                PageSize: itemsPerPage,
                SortBy: sortBy[0] || '',
                SortDesc: sortDesc[0] || false
                // Add optional filters here (ActivityName, DateFrom, etc.)
            };

            this.loading = true;
            try {
                this.pagedResult = await this.scheduleService.getSchedules(dto);
                this.records = this.pagedResult.Results.map(record => ({
                    ...record,
                    ScheduleDateTime: moment(record.ScheduleDateTime).format("YYYY-MM-DD")
                }));
                
            } catch (error: any) {
                if (error.StatusCode != 500) {
                    let errorMessage = error.Message ?? "An error occured while processing your request.";
                    this.notify_error(errorMessage);
                }
             } finally {
                this.loading = false;
            }
        }


    }


</script>