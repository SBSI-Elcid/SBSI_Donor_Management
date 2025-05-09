<template>
    <v-form class="form-container" ref="form"  lazy-validation>
        <v-row>
            <!-- Start Time -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Start Time</label>
                <v-text-field dense outlined label="Start Time of Blood Collection" />
            </v-col>

            <!-- End Time -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">End Time</label>
                <v-text-field dense outlined label="End Time of Blood Collection" />
            </v-col>

            <!-- Donor Reaction -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Donor Reaction</label>
                <v-text-field dense outlined label="Donor Reaction" />
            </v-col>
        </v-row>

        <v-row align="center">
            <!-- Success Radio -->
            <v-col cols="12" md="2">
                <label class="font-weight-bold">Success?</label>
                <v-radio-group row>
                    <v-radio label="Yes" value="yes" />
                    <v-radio label="No" value="no" />
                </v-radio-group>
            </v-col>

            <!-- Blood Type -->
            <v-col cols="12" md="2">
                <label class="font-weight-bold">Blood Type</label>
                <v-select :items="['A', 'B', 'AB', 'O']"
                          label="Blood Type"
                          dense
                          outlined />
            </v-col>

            <!-- Collected Blood Amount -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Collected Blood Amount (mL)</label>
                <v-text-field dense outlined label="Collected Blood Amount" />
            </v-col>

            <!-- Patient Allocation -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Patient Allocation</label>
                <v-row>
                    <v-col cols="4">
                        <v-text-field dense outlined label="Last Name" />
                    </v-col>
                    <v-col cols="4">
                        <v-text-field dense outlined label="First Name" />
                    </v-col>
                    <v-col cols="4">
                        <v-text-field dense outlined label="Middle Name" />
                    </v-col>
                </v-row>
            </v-col>
        </v-row>
    </v-form>
</template>