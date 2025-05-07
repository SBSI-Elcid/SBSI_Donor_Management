<template>
    <div>
        <v-form class="form-container" ref="form"  lazy-validation>
            <v-row>
                <v-col cols="2" class="py-0">
                    <label class="label-container">Body Weight (kgs)</label>
                    <v-text-field type="number"
                                  dense outlined />
                </v-col>

                <v-col cols="2" class="py-0">
                    <label class="label-container">Temperature</label>
                    <v-text-field type="number"
                                  dense outlined />
                </v-col>

            </v-row>

            <v-row>
                <v-col cols="2" class="py-0">
                    <label class="label-container">Blood Pressure</label>
                    <v-text-field type="number"
                                  dense outlined />
                </v-col>

                <v-col cols="2" class="py-0">
                    <label class="label-container">Pulse Rate</label>
                    <v-text-field type="number"
                                  dense outlined />
                </v-col>

            </v-row>

            <v-row>
                <v-col cols="2" class="py-0">
                    <label class="label-container">Respiratory Rate</label>
                    <v-text-field type="number"
                                  dense outlined />
                </v-col>

                <v-col cols="2" class="py-0">
                    <label class="label-container">Oxygen Saturation</label>
                    <v-text-field type="number"
                                  dense outlined />
                </v-col>

            </v-row>

            <!--<v-row>
                <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
                    <label class="label-container">Type of Donation:</label>
                    <v-radio-group class="radio-container-row" v-model="donorInitialScreening.DonationType"
                                   @change="onDonationTypeChange" :disabled="isDisabled" row>
                        <v-radio v-for="(type, index) in donationTypesOptions" :key="index" :label="type.text" :value="type.value" />
                    </v-radio-group>
                </v-col>
            </v-row>

            <v-row>
                <v-col cols="12" lg="3" md="3" sm="12" class="py-0">
                    <v-radio-group v-if="showInHouseOptions" class="radio-container-row" v-model="donorInitialScreening.InHouseTypeValue"
                                   @change="onInHouseTypeChange" :disabled="isDisabled">
                        <v-radio v-for="(type, index) in inHouseTypesOptions" :key="index" :label="type.text" :value="type.value" />
                    </v-radio-group>
                </v-col>
            </v-row>-->


            <div class="section-outer-container mt-3">
                <div class="text-right">
                    <v-btn color="default" large tile class="mr-2" v-if="" @click=""><v-icon color="success" size="25" left>mdi-content-save</v-icon> Save</v-btn>
                    <v-btn color="default" large tile class="mr-2" v-if="" @click=""><v-icon color="success" size="25" left>mdi-check</v-icon> Approve</v-btn>
                    <v-btn color="default" large tile class="mr-2" v-if="" @click=""><v-icon color="warning" size="25" left>mdi-cancel</v-icon> Mark as Deferred</v-btn>
                </div>
            </div>
        </v-form>
    </div>
</template>
<<!--script>
    import VueBase from '@/components/VueBase';
    import { Component, Watch } from 'vue-property-decorator';
    import { getModule } from 'vuex-module-decorators';
    import DonorModule from '@/store/DonorModule';
    import LookupModule from '@/store/LookupModule';
    import DonorScreeningService from '@/services/DonorScreeningService';
    import Common from '@/common/Common';
    import { DonorInitialScreeningDto, IDonorInitialScreeningDto } from '@/models/DonorScreening/DonorInitialScreeningDto';
    import { IDonorRecentDonationDto } from '@/models/DonorScreening/DonorRecentDonationDto';
    import { ILookupOptions } from '@/models/Lookups/LookupOptions';
    import { LookupKeys } from '@/models/Enums/LookupKeys';
    import { DonorStatus } from '@/models/Enums/DonorStatus';
    import RecentDonationTable from '@/components/DonorScreening/ScreeningForms/RecentDonationTable.vue';

</script>-->
