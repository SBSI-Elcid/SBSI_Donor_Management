<template>
    <v-dialog v-model="dialog" max-width="900px" persistent>
        <v-card>
            <v-card-title class="red lighten-2 white--text justify-center">
                <strong>CREATE / EDIT / VIEW SCHEDULE</strong>
            </v-card-title>

            <v-card-actions class="text-left; ml-5">
                <v-btn color="red darken-2" class="white--text">VIEW DONORS</v-btn>
                <v-btn color="red darken-2" class="white--text">VIEW CHECKLIST</v-btn>
            </v-card-actions>

            <v-card-text>
                <v-container>
                    <v-row>
                        <v-col cols="12">
                            <v-text-field label="Name of Activity" dense outlined></v-text-field>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12">
                            <label class="font-weight-bold mb-2">Type of Voluntary Non-Remunerated Blood Donation</label>
                            <v-radio-group row>
                                <v-radio label="Walk-In" value="Walk-In"></v-radio>
                                <v-radio label="In-House" value="In-House"></v-radio>
                                <v-radio label="Mobile Blood Donation" value="Mobile"></v-radio>
                                <v-radio label="Advocacy" value="Advocacy"></v-radio>
                            </v-radio-group>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12" md="6">
                            <v-text-field type="date"
                                          label="Date and Time"
                                          dense
                                          outlined></v-text-field>
                        </v-col>
                        <v-col cols="12" md="6">
                            <v-text-field label="Venue" dense outlined></v-text-field>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12" md="6">
                            <v-text-field label="Name of Partner Institution" dense outlined></v-text-field>
                        </v-col>
                        <v-col cols="12" md="6">
                            <v-text-field label="Name of Point Person" dense outlined></v-text-field>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12">
                            <v-text-field label="Expected Number of Donors / Audience"
                                          dense
                                          outlined></v-text-field>
                        </v-col>
                    </v-row>
                </v-container>
            </v-card-text>

            <v-card-actions class="justify-end">
                <v-btn color="red darken-2" class="white--text">CREATE SCHEDULE</v-btn>
                <v-btn color="red darken-2" @click="$emit('close')" class="white--text">CANCEL</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script lang="ts">
    import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
    import VueBase from '@/components/VueBase';

    @Component
    export default class CreateSchedule extends VueBase {
        @Prop({ default: false }) readonly value!: boolean;

        protected dialog: boolean = false;

        mounted() {
            this.dialog = this.value;
        }

        @Watch('value')
        onValueChanged(val: boolean) {
            this.dialog = val;
        }

    }
</script>