<template>
    <v-form class="form-container" ref="form" v-model="formValid" lazy-validation>
        <v-row class="mb-4" align="center">
            <!-- Blood Bag to be Used -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Blood Bag to be Used</label>
                <v-select :items="['Type A', 'Type B', 'Type C']"
                          label="Blood Bag to be Used"
                          dense
                          outlined />
            </v-col>

            <!-- Bag Type Options -->
            <v-col cols="12" md="8">
                <v-radio-group row>
                    <v-radio label="Single" value="single" />
                    <v-radio label="Double" value="double" />
                    <v-radio label="Triple" value="triple" />
                    <v-radio label="Quadruple" value="quadruple" />
                </v-radio-group>
            </v-col>
        </v-row>

        <v-row align="center">
            <!-- Segment Serial Number -->
            <v-col cols="12" sm="6" md="4">
                <label class="font-weight-bold">Segment Serial Number</label>
                <v-text-field dense outlined label="Segment Serial Number" />
            </v-col>

            <!-- Unit Serial Number -->
            <v-col cols="12" sm="6" md="4">
                <label class="font-weight-bold">Unit Serial Number</label>
                <v-text-field dense outlined label="Unit Serial Number" />
            </v-col>

            <!-- Print Button -->
            <v-col cols="12" md="4" class="text-right">
                <v-btn color="red darken-2" class="white--text mt-6" rounded>
                    PRINT BARCODE
                </v-btn>
            </v-col>
        </v-row>
    </v-form>
</template>