<template>
    <div>

        <v-dialog v-model="dialog" max-width="900px" persistent>
            <v-card>
                <v-card-title class="white--text justify-center" style="background-color: rgb(185, 47, 47);">
                    <strong>CREATE / EDIT / VIEW SCHEDULE</strong>
                </v-card-title>

                <v-card-actions v-if="isEditing" class="text-left; ml-5">
                    <v-btn style="background-color: rgb(185, 47, 47);" class="white--text" @click ="openActivityDonor">VIEW DONORS</v-btn>
                    <v-btn style="background-color: rgb(185, 47, 47);" class="white--text" @click="openChecklist">
                        VIEW CHECKLIST
                    </v-btn>
                </v-card-actions>

                <v-card-text>
                    <v-container>
                        <v-row>
                            <v-col cols="12">
                                <v-text-field :disabled ="isDisabled" v-model="schedule.ActivityName" label="Name of Activity" dense outlined></v-text-field>
                            </v-col>
                        </v-row>

                        <v-row>
                            <v-col cols="12">
                                <label class="font-weight-bold mb-2">Type of Voluntary Non-Remunerated Blood Donation</label>
                                <v-radio-group :disabled ="isDisabled" v-model="schedule.ActivityType" row>
                                    <v-radio label="Walk-In" value="Walk-In"></v-radio>
                                    <v-radio label="In-House" value="In-House"></v-radio>
                                    <v-radio label="Mobile Blood Donation" value="Mobile"></v-radio>
                                    <v-radio label="Advocacy" value="Advocacy"></v-radio>
                                </v-radio-group>
                            </v-col>
                        </v-row>

                        <v-row>
                            <v-col cols="12" md="6">
                                <v-text-field :disabled ="isDisabled" v-model="schedule.ScheduleDateTime" type="datetime-local"
                                              label="Date and Time"
                                              dense
                                              outlined></v-text-field>
                            </v-col>
                            <v-col cols="12" md="6">
                                <v-text-field :disabled ="isDisabled" v-model="schedule.ActivityVenue" label="Venue" dense outlined></v-text-field>
                            </v-col>
                        </v-row>

                        <v-row>
                            <v-col cols="12" md="6">
                                <v-text-field :disabled ="isDisabled" v-model="schedule.PartnerInstitutionName" label="Name of Partner Institution" dense outlined></v-text-field>
                            </v-col>
                            <v-col cols="12" md="6">
                                <v-text-field :disabled ="isDisabled" v-model="schedule.PointPersonName" label="Name of Point Person" dense outlined></v-text-field>
                            </v-col>
                        </v-row>

                        <v-row>
                            <v-col cols="12">
                                <v-text-field :disabled ="isDisabled" label="Expected Number of Donors / Audience"
                                              dense
                                              v-model="schedule.ExpectedDonorNumber"
                                              outlined></v-text-field>
                            </v-col>
                        </v-row>
                    </v-container>
                </v-card-text>

                <v-card-actions class="justify-end">
                    <v-btn v-if ="!isDisabled" color="red darken-2" @click="createSchedule" class="white--text">{{labelUpdate}}</v-btn>
                    <v-btn color="red darken-2" @click="$emit('close')" class="white--text">CANCEL</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <CheckList v-if="showChecklist"
                   :value="showChecklist"
                   :isDisabled ="isDisabled"
                   :schedule_id="schedule.ScheduleId"
                   @close="handleClose" />

        <!--<ActivityDonor v-if="showActivityDonor"
                   :value="showActivityDonor"
                   :schedule_id="schedule.ScheduleId"
                   @close="handleClose" />-->

    </div>
</template>

<script lang="ts">
    import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
    import CheckList from '@/components/Schedule/ScheduleForms/CheckList.vue';
    import ActivityDonor from '@/components/Schedule/ScheduleForms/ActivityDonor.vue';
    import VueBase from '@/components/VueBase';
    import { ScheduleDto, ISchedule } from '../../models/Schedules/ScheduleDto';
    import ScheduleService from '@/services/ScheduleService';
    import moment from 'moment';

    @Component({ components: { CheckList } })
    export default class CreateSchedule extends VueBase {
        @Prop({ default: false }) readonly value!: boolean;

        @Prop({ default: false }) readonly isEditing!: boolean;

        @Prop({ default: false }) readonly isDisabled!: boolean;

        @Prop({ default: "" }) readonly scheduleId!: string;
        protected dialog: boolean = false;
        protected schedule: ISchedule = new ScheduleDto();
        protected scheduleService = new ScheduleService();
        protected showChecklist: boolean = false;
        protected showActivityDonor: boolean = false;

        mounted() {
            this.loadScheduleById();
            this.dialog = this.value;
        }

        @Watch('value')
        onValueChanged(val: boolean) {
            this.dialog = val;
        }
        @Watch('scheduleId')
        onScheduleIdChanged(newId: string) {
            this.loadScheduleById();
        }

        @Watch('isEditing')
        onEditingChanged(newVal: boolean) {
            if (newVal) {
                this.loadScheduleById();  // or any method you want to trigger when editing starts
            }
        }

        protected openActivityDonor(): void {
            this.$router.push({ path: `/schedules/activitydonor/${this.scheduleId}` });
        }

        protected openChecklist(): void {
            this.showChecklist = true;
        }
        protected handleClose(): void {
            this.showChecklist = false;
            this.showActivityDonor = false;
        }


        protected get labelUpdate(): string {
            return this.isEditing ? "UPDATE SCHEDULE" : "CREATE SCHEDULE";
        }
        protected async loadScheduleById(): Promise<void> {

            if (!this.isEditing || !this.scheduleId) {
                this.schedule = new ScheduleDto();
                return;
            }

            try {
                const result = await this.scheduleService.getSchedulesById(this.scheduleId);
                this.schedule = result;
                this.schedule.ScheduleDateTime = moment(result.ScheduleDateTime).format("YYYY-MM-DDTHH:mm");// populate your form-bound object
    
            } catch (err) {
                console.error('Failed to load schedule', err);
                this.$toast?.error('Failed to load schedule details');
            }
        }

        protected async createSchedule(): Promise<void> {
            try {
                
                moment(this.schedule.ScheduleDateTime).format("YYYY-MM-DD");
                const id = await this.scheduleService.upsertSchedule(this.schedule);
                this.notify_success('Form successfully submitted.');
                this.$emit('close'); // Close dialog
            } catch (err: any) {
                if (err.StatusCode != 500) {
                    let errorMessage = error.Message ?? "An error occured while processing your request.";
                    this.notify_error(errorMessage);
                }
            }
        }

    }
</script>