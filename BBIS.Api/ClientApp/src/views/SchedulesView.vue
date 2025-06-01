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
                        <v-btn color="red darken-2" class="white--text" @click="">Edit Schedule</v-btn>
                        <v-btn color="red darken-2" class="white--text" @click="">Cancel Schedule</v-btn>
                    </v-card-actions>

                    <v-data-table :headers="[
                                              { text: 'Activity Name', value: 'ActivityName' },
                                              { text: 'Activity Type', value: 'ActivityType' },
                                              { text: 'Schedule Date & Time', value: 'ScheduleDateTime' },
                                              { text: 'Activity Venue', value: 'ActivityVenue' },
                                              { text: 'Partner Institution', value: 'PartnerInstitutionName' },
                                              { text: 'Point Person', value: 'PointPersonName' }
                                            ]"
                                  :items="[]"
                                  class="elevation-1"
                                  dense>
                    </v-data-table>
                </v-card>
            </v-col>
        </v-row>

        <CreateSchedule v-model = "showCreateDialog" @close="showCreateDialog = false" />
    </v-container>
</template>

<script lang="ts">
    import CreateSchedule from '../components/Schedule/CreateSchedule.vue';
    import { Component, Vue } from 'vue-property-decorator';
    import { getModule } from 'vuex-module-decorators';
    import AuthModule from '../store/AuthModule';
    import VueBase from '@/components/VueBase';

    @Component({ components: { CreateSchedule } })
    export default class SchedulesView extends VueBase {

        protected showCreateDialog: boolean = false;
        protected createSchedule(): void {
            this.showCreateDialog = true;
        }
    }


</script>