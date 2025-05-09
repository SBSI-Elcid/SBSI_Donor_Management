<!--<template>
    <div>
        <v-form class="form-container" ref="form" v-model="formValid" lazy-validation>
            <v-row>
                <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
                    <label class="label-container">Blood Bag Used</label>
                    <v-autocomplete :items="['Single', 'Double', 'Triple', 'Quad']"
                                    dense
                                    outlined />
                </v-col>

                <v-col cols="12" lg="9" md="9" sm="12" class="py-3">
                    <label class="label-container">Options</label>
                    <v-radio-group>
                        <v-radio label="Option A" value="A" />
                        <v-radio label="Option B" value="B" />
                        <v-radio label="Option C" value="C" />
                    </v-radio-group>
                </v-col>
            </v-row>

            <v-row>
                <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
                    <label class="label-container">Start Time</label>
                    <v-text-field type="time" dense outlined />
                </v-col>

                <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
                    <label class="label-container">End Time</label>
                    <v-text-field type="time" dense outlined />
                </v-col>
            </v-row>

            <v-row>
                <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
                    <label class="label-container">Collected Blood Amount</label>
                    <v-text-field type="number" dense outlined />
                </v-col>

                <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
                    <label class="label-container">Success?</label>
                    <v-radio-group class="row-radio-group-container">
                        <v-radio label="Yes" value="true" />
                        <v-radio label="No" value="false" />
                    </v-radio-group>
                </v-col>
            </v-row>

            <v-row>
                <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
                    <label class="label-container">Unwell?</label>
                    <v-textarea rows="2" no-resize dense outlined />
                </v-col>

                <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
                    <label class="label-container">Medication Given</label>
                    <v-textarea rows="2" no-resize dense outlined />
                </v-col>
            </v-row>

            <v-row>
                <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
                    <label class="label-container">Unit Serial Number</label>
                    <v-text-field readonly dense outlined />
                </v-col>

                <v-col cols="12" lg="3" md="3" sm="12" class="mt-1">
                    <v-btn color="default" class="mt-2 mr-2">
                        <v-icon left>mdi-barcode</v-icon> Print Barcode
                    </v-btn>
                </v-col>
            </v-row>

            <div class="section-outer-container mt-3">
                <div class="text-right">
                    <v-btn color="default" large tile class="mr-2">
                        <v-icon color="success" size="25" left>mdi-content-save</v-icon> Submit
                    </v-btn>
                </div>
            </div>
        </v-form>
    </div>
</template>-->
<template>
    <v-form class="form-container" ref="form" v-model="formValid" lazy-validation>
        <v-row class="mb-4">
            <!-- Method of Blood Collection -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Method of Blood Collection</label>
                <v-radio-group row>
                    <v-radio label="Whole Blood Donor" value="whole" />
                    <v-radio label="Apheresis Donor" value="apheresis" />
                </v-radio-group>
            </v-col>

            <!-- Blood Type -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Blood Type</label>
                <v-select :items="['A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-']"
                          label="Blood Type"
                          dense
                          outlined />
            </v-col>
        </v-row>

        <v-row>
            <!-- HGB -->
            <v-col cols="12" sm="6" md="2">
                <label class="font-weight-bold">HGB</label>
                <v-text-field dense outlined label="Hemoglobin" />
            </v-col>

            <!-- HCT -->
            <v-col cols="12" sm="6" md="2">
                <label class="font-weight-bold">HCT</label>
                <v-text-field dense outlined label="Hematocrit" />
            </v-col>

            <!-- RBC -->
            <v-col cols="12" sm="6" md="2">
                <label class="font-weight-bold">RBC</label>
                <v-text-field dense outlined label="Red Blood Cells" />
            </v-col>

            <!-- WBC -->
            <v-col cols="12" sm="6" md="2">
                <label class="font-weight-bold">WBC</label>
                <v-text-field dense outlined label="White Blood Cells" />
            </v-col>

            <!-- PLT -->
            <v-col cols="12" sm="6" md="2">
                <label class="font-weight-bold">PLT</label>
                <v-text-field dense outlined label="Platelet Count" />
            </v-col>
        </v-row>
    </v-form>
</template>


<!--<script lang="ts">
    import VueBase from '@/components/VueBase';
    import { Component, Watch } from 'vue-property-decorator';
    import { getModule } from 'vuex-module-decorators';
    import DonorModule from '@/store/DonorModule';
    import LookupModule from '@/store/LookupModule';
    import DonorScreeningService from '@/services/DonorScreeningService';
    import ApplicationSettingService from '@/services/ApplicationSettingService';
    import Common from '@/common/Common';
    import { DonorBloodCollectionDto, IDonorBloodCollectionDto } from '@/models/DonorScreening/DonorBloodCollectionDto';
    import { BloodBagCollections } from '@/models/Enums/BloodBagCollections';
    import { ILookupOptions } from '@/models/Lookups/LookupOptions';
    import { LookupKeys } from '@/models/Enums/LookupKeys';
    import { ApplicationSettingKeys } from '@/models/ApplicationSettingKeys';
    import { DonorStatus } from '@/models/Enums/DonorStatus';
    import moment from 'moment';
    import JsBarcode from 'jsbarcode'

    @Component
    export default class BloodCollectionForm extends VueBase {
        protected donorModule: DonorModule = getModule(DonorModule, this.$store);
        protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
        protected donorScreeningService: DonorScreeningService = new DonorScreeningService();
        protected appSettingService: ApplicationSettingService = new ApplicationSettingService()

        protected formValid: boolean = true;
        protected rules: any = { ...Common.ValidationRules }
        protected errorMessage: string = '';

        protected success: boolean | null = null;
        protected generatedSerialNumber: string = '';
        protected startTime: string = '';
        protected endTime: string = '';
        protected donorBloodCollection: IDonorBloodCollectionDto = new DonorBloodCollectionDto();
        protected isEditingValue: boolean = false;
        protected isDisabled: boolean = true;
</script>-->
