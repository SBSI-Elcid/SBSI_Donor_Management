<template>
    <v-dialog v-model="dialog" max-width="900px" persistent>
        <v-card>
            <v-card-title class="red lighten-2 white--text justify-center">
                <strong>CREATE / EDIT / VIEW SCHEDULE</strong>
            </v-card-title>

            <v-card-actions class="justify-center">
                <v-btn color="red darken-2" class="white--text">CREATE SCHEDULE</v-btn>
                <v-btn color="red darken-2" class="white--text">CANCEL</v-btn>
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
                            <v-text-field label="Date and Time" dense outlined></v-text-field>
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
                <v-btn text color="grey darken-1">Close</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>