<template>
    <v-dialog v-model="dialog" max-width="1000px" persistent >
        <v-container fluid>
            <v-card outlined>
                <v-card-title class="headline font-weight-bold justify-center red lighten-4">
                    CHECKLIST
                </v-card-title>

                <v-card-text>
                    <v-form>
                        <v-row>
                            <v-col cols="12">
                                <v-card outlined>
                                    <v-card-title class="red white--text py-2 px-4 font-weight-bold">
                                        CONFIRMATION CALL
                                    </v-card-title>
                                    <v-card-text>
                                        <v-textarea v-model ="checklist.ConfirmationCall" class="ma-2" label="Input details" outlined dense></v-textarea>
                                    </v-card-text>
                                </v-card>
                            </v-col>

                            <v-col cols="12">
                                <v-card outlined>
                                    <v-card-title class="red white--text py-2 px-4 font-weight-bold">
                                        VERIFICATION CALL
                                    </v-card-title>
                                    <v-card-text>
                                        <v-textarea v-model ="checklist.VerificationCall" class="ma-2" label="Input details" outlined dense></v-textarea>
                                    </v-card-text>
                                </v-card>
                            </v-col>

                            <v-col cols="12">
                                <v-card outlined>
                                    <v-card-title class="red white--text py-2 px-4 font-weight-bold">
                                        FINAL CALL
                                    </v-card-title>
                                    <v-card-text>
                                        <v-textarea v-model ="checklist.FinalCall" class="ma-2" label="Input details" outlined dense></v-textarea>
                                    </v-card-text>
                                </v-card>
                            </v-col>

                            <v-col cols="12" class="text-right">
                                <v-btn color="red" dark large @click="createChecklist"> SAVE </v-btn>
                                <v-btn color="red" dark large @click="$emit('close')" class="white--text; ma-2">CANCEL</v-btn>
                            </v-col>
                        </v-row>
                    </v-form>
                </v-card-text>
            </v-card>
        </v-container>
    </v-dialog>
</template>

<script lang="ts">
    import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
    import VueBase from '@/components/VueBase';
    import { ScheduleDto, ISchedule } from '@/models/Schedules/ScheduleDto';
    import { IChecklistDto,ChecklistDto } from '@/models/Schedules/ChecklistDto';
    import ScheduleService from '@/services/ScheduleService';
    import {} from '@models/Schedules/'
    import moment from 'moment';

    @Component 
    export default class CheckList extends VueBase {

        @Prop({ default: "" }) readonly schedule_id!: string;
        protected dialog: boolean = false;
       
        @Prop({ default: false }) readonly value!: boolean;
        protected schedule: ISchedule = new ScheduleDto();
        protected scheduleService = new ScheduleService();
        protected showChecklist: boolean = false;
        protected checklist: IChecklistDto = new ChecklistDto();
        protected dialog: boolean = false;


        mounted() {
            this.dialog = this.value;
            this.fetchCheckList();
            
        }

        @Watch('value')
        onValueChanged(val: boolean) {
            this.dialog = val;
        }

        protected get scheduleId(): string {
            return this.schedule_id;
        }

        protected async fetchCheckList(): Promise<void> {


           
            this.checklist = await this.scheduleService.getCheckList(this.scheduleId);


        }

        protected async createChecklist(): Promise<void> {
            try {
              
                this.checklist.ScheduleId = this.scheduleId;
                const id = await this.scheduleService.upsertCheckList(this.checklist);
                this.notify_success('Form successfully saved.');
                this.$emit('close'); // Close dialog
            } catch (err: any) {
                if (err.StatusCode != 500) {
                    let errorMessage = error.Message ?? "An error occured while processing your request.";
                    this.notify_error(errorMessage);
                }
            }
        }
        //@Watch('scheduleId')
        //onScheduleIdChanged(newId: string) {
        //    this.loadScheduleById();
        //}

        //@Watch('isEditing')
        //onEditingChanged(newVal: boolean) {
        //    if (newVal) {
        //        this.loadScheduleById();  // or any method you want to trigger when editing starts
        //    }
        //}

        //protected get labelUpdate(): string {
        //    return this.isEditing ? "UPDATE CHECKLIST" : "CREATE CHECKLIST";
        //}
        //protected async loadScheduleById(): Promise<void> {

        //    if (!this.isEditing || !this.scheduleId) {
        //        this.schedule = new ScheduleDto();
        //        return;
        //    }

        //    try {
        //        const result = await this.scheduleService.getSchedulesById(this.scheduleId);
        //        this.schedule = result;
        //        this.schedule.ScheduleDateTime = moment(result.ScheduleDateTime).format("YYYY-MM-DD")// populate your form-bound object

        //    } catch (err) {
        //        console.error('Failed to load schedule', err);
        //        this.$toast?.error('Failed to load schedule details');
        //    }
        //}

        //protected async createSchedule(): Promise<void> {
        //    try {

        //        moment(this.schedule.ScheduleDateTime).format("YYYY-MM-DD");
        //        console.log(this.schedule);
        //        const id = await this.scheduleService.upsertSchedule(this.schedule);
        //        this.$toast?.success('Schedule created successfully');
        //        this.$emit('close'); // Close dialog
        //    } catch (err) {
        //        console.error(err);
        //        this.$toast?.error('Failed to create schedule');
        //    }
        //}

    }
</script>