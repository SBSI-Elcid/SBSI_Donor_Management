<template>
    <v-container fluid>
        <v-form>
            <v-row>
                <v-col cols="6">
                    <v-text-field label="Type of Reaction" />
                </v-col>
                <v-col cols="6">
                    <v-select label="Severity of Reaction"
                              :items="['Mild', 'Moderate', 'Severe']"
                              solo
                              append-icon="mdi-menu-down" />
                </v-col>
            </v-row>

            <v-row>
                <v-col cols="6">
                    <v-text-field label="Reaction Manifestation" />
                </v-col>
                <v-col cols="6">
                    <v-text-field label="Action / Interventions" />
                </v-col>
            </v-row>

            <v-row>
                <v-col>
                    <v-card outlined>
                        <v-card-title>Vital Signs Monitoring</v-card-title>
                        <v-simple-table>
                            <thead>
                                <tr>
                                    <th>Time</th>
                                    <th>BP</th>
                                    <th>PR</th>
                                    <th>Others</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="i in 3" :key="i">
                                    <td><v-text-field dense hide-details /></td>
                                    <td><v-text-field dense hide-details /></td>
                                    <td><v-text-field dense hide-details /></td>
                                    <td><v-text-field dense hide-details /></td>
                                </tr>
                            </tbody>
                        </v-simple-table>
                    </v-card>
                </v-col>

                <v-col>
                    <v-textarea label="Doctor's Notes" rows="4" outlined />
                    <v-text-field label="Discharge Status" />
                    <v-row>
                        <v-col>
                            <v-text-field label="Monitored By" />
                        </v-col>
                        <v-col>
                            <v-text-field label="Doctor's Name" />
                        </v-col>
                    </v-row>
                </v-col>
            </v-row>

            <v-row>
                <v-col>
                    <v-textarea label="Day 1 Post Donation" rows="2" outlined />
                </v-col>
                <v-col>
                    <v-textarea label="Day 2 Post Donation" rows="2" outlined />
                </v-col>
                <v-col>
                    <v-textarea label="Day 3 Post Donation" rows="2" outlined />
                </v-col>
                <v-col>
                    <v-textarea label="Supervisor's Note" rows="2" outlined />
                </v-col>
            </v-row>
        </v-form>
    </v-container>
</template>


