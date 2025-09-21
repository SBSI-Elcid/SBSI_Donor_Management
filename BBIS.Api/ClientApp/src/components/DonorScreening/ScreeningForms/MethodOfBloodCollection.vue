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
                <v-radio-group row
                               :disabled="isEditingValue"
                               v-model="donorInitialScreening.MethodOfBloodCollection">
                    <v-radio label="Whole Blood Donor" value="whole" />
                    <v-radio label="Apheresis Donor" value="apheresis" />
                </v-radio-group>
            </v-col>
            <!-- Blood Type -->
            <!--<v-col cols="12" md="4">
                <label class="font-weight-bold">Blood Type</label>
                <v-select :items="bloodTypesOptions"
                          v-model ="donorInitialScreening.MethodOfBloodCollection"
                          label="Blood Type"
                          dense
                          outlined />
            </v-col>-->
        </v-row>


        <v-row>
            <v-col cols="12" sm="6" md="2">
                <label class="font-weight-bold">Complete Blood Count</label>
            </v-col>
        </v-row>

        <v-row>
            <!-- HGB -->
            <v-col cols="12" sm="6" md="2">
                <!--<label class="font-weight-bold">HGB</label>-->
                <v-text-field dense
                              outlined
                              :disabled="isEditingValue"
                              :rules="[rules.numberRequired, rules.positiveNumber]"
                              label="Hemoglobin"
                              v-model="donorInitialScreening.HGB" />
            </v-col>

            <!-- HCT -->
            <v-col v-if="!disableOptions" cols="12" sm="6" md="2">
                <!--<label class="font-weight-bold">HCT</label>-->
                <v-text-field dense
                              outlined
                              :disabled="isEditingValue"
                              :rules="disableOptions ? [] : [rules.numberRequired, rules.positiveNumber]"
                              label="Hematocrit"
                              v-model="donorInitialScreening.HCT" />
            </v-col>

            <!-- RBC -->
            <v-col v-if="!disableOptions" cols="12" sm="6" md="2">
                <!--<label class="font-weight-bold">RBC</label>-->
                <v-text-field dense
                              outlined
                              :disabled="isEditingValue"
                              :rules="disableOptions ? [] : [rules.numberRequired, rules.positiveNumber]"
                              label="Red Blood Cells"
                              v-model="donorInitialScreening.RBC" />
            </v-col>

            <!-- WBC -->
            <v-col v-if="!disableOptions" cols="12" sm="6" md="2">
                <!--<label class="font-weight-bold">WBC</label>-->
                <v-text-field dense
                              outlined
                              :disabled="isEditingValue"
                              :rules="disableOptions ? [] : [rules.numberRequired, rules.positiveNumber]"
                              label="White Blood Cells"
                              v-model="donorInitialScreening.WBC" />
            </v-col>

            <!-- PLT -->
            <v-col v-if="!disableOptions" cols="12" sm="6" md="2">
                <!--<label class="font-weight-bold">PLT</label>-->
                <v-text-field dense
                              outlined
                              :disabled="isEditingValue"
                              :rules="disableOptions ? [] : [rules.numberRequired, rules.positiveNumber]"
                              label="Platelet Count"
                              v-model="donorInitialScreening.PLTCount" />
            </v-col>
        </v-row>

        <div class="section-outer-container mt-3">
            <div class="text-right" v-if="!isEditingValue">
                <v-btn color="default" large tile class="mr-2" @click="onApprove"><v-icon color="success" size="25" left>mdi-check</v-icon> Approve</v-btn>
                <v-btn color="default" large tile class="mr-2" @click="onDeferred"><v-icon color="warning" size="25" left>mdi-cancel</v-icon> Mark as Deferred</v-btn>
            </div>
        </div>
    </v-form>
</template>
<script lang="ts">
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

    @Component({ components: { RecentDonationTable } })
    export default class InitialScreeningForm extends VueBase {
        protected donorModule: DonorModule = getModule(DonorModule, this.$store);
        protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
        protected donorScreeningService: DonorScreeningService = new DonorScreeningService();

        protected formValid: boolean = true;
        protected rules: any = {
            ...Common.ValidationRules,

            numberRequired: (v: any) => (!!v || v === 1) || 'This field is required.',
            positiveNumber: (v: number) => v >= 1 || 'Must be a positive number',
        }
        protected errorMessage: string = '';
        protected showInHouseOptions: boolean = false;
        protected showRecentDonations: boolean = false;
        protected showPatientDirectedFields: boolean = false;
        protected showMobileBloodDonationFields: boolean = false;
        protected donorInitialScreening: IDonorInitialScreeningDto = new DonorInitialScreeningDto();
        protected recentDonations: Array<IDonorRecentDonationDto> = new Array<IDonorRecentDonationDto>();
        protected isEditingValue: boolean = false;
        protected isDisabled: boolean = true;

        protected disableOptions: boolean = false;
        protected selectedMethod: string = '';


        protected get selectedDonorName(): string {
            return this.donorInitialScreening.DonorName;
        }

        public get options(): (key: string) => Array<ILookupOptions> {
            return (key) => this.lookupModule.getOptionsByKey(key);
        }

        protected get bloodTypesOptions(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.BloodTypes).map(x => { return { text: x.Text, value: x.Value } });
        }

        protected get donationTypesOptions(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.DonationTypes).map(x => { return { text: x.Text, value: x.Value } });
        }

        protected get inHouseTypesOptions(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.InHouseTypes).map(x => { return { text: x.Text, value: x.Value } });
        }

        protected get agencyOptions(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.AgencyTypes).map(x => { return { text: x.Text, value: x.Value } });
        }

        protected get hospitalOptions(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.HospitalTypes).map(x => { return { text: x.Text, value: x.Value } });
        }

        protected get BloodDonator(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.BloodDonator).map(x => { return { text: x.Text, value: x.Value } });
        }

        @Watch("showInHouseOptions")
        public onInHouseSelected(): void {
            if (this.showInHouseOptions === false) {
                this.donorInitialScreening.InHouseTypeValue = "";
            }
        }

        @Watch('donorInitialScreening.MethodOfBloodCollection')
        onMethodChange(newValue: string): void {
            this.disableOptions = newValue === 'whole';
        }

        @Watch("showPatientDirectedFields")
        public onPatentDirectedSelected(): void {
            if (this.showPatientDirectedFields === false) {
                this.donorInitialScreening.NameOfPatient = "";
                this.donorInitialScreening.PatientHospital = "";
                this.donorInitialScreening.PatientBloodType = "";
                this.donorInitialScreening.PatientWBOrComponent = "";
                this.donorInitialScreening.PatientNoOfUnits = 0;
            }
        }

        @Watch("showMobileBloodDonationFields")
        public onMobileDonationSelected(): void {
            if (this.showMobileBloodDonationFields === false) {
                this.donorInitialScreening.MobileBloodDonationPlace = "";
                this.donorInitialScreening.MobileBloodDonationOrganizer = "";
            }
        }

        public async created(): Promise<void> {
            let loader = this.showLoader();
            this.selectedMethod = this.donorInitialScreening.MethodOfBloodCollection;

            try {
                await this.getInitialScreeningInfo();
                this.donorModule.fetchDonorActivityType(this.$route.params.reg_id);
            }
            catch (error) {
                console.log(error);
            }
            finally {
                loader.hide();
            }

            this.donorInitialScreening.MethodOfBloodCollection = "whole";
        }

        protected async getInitialScreeningInfo(): Promise<void> {
            if (this.$route.params.reg_id && typeof (this.$route.params.reg_id) === 'string') {
                let regId = this.$route.params.reg_id;
                this.donorInitialScreening = await this.donorScreeningService.getInitialScreeningInfo(regId);

                this.donorModule.setTransactionId(this.donorInitialScreening.DonorTransactionId);

                // Enable the fields when Donor Status is not deferred.
                if (Common.hasValue(this.donorInitialScreening.DonorStatus) && this.donorInitialScreening.DonorStatus !== DonorStatus.Deferred && this.donorInitialScreening.DonorStatus !== DonorStatus.ForMethodBloodCollection) {
                    this.isDisabled = false;
                    if (this.donorInitialScreening.DonorStatus !== DonorStatus.ForMethodBloodCollection) {
                        this.isEditingValue = true;
                    }
                }
                this.onDonationTypeChange();
                this.onInHouseTypeChange();
                this.onBloodDonatorChange();
            }
        }

        public onDonationTypeChange(): void {
            if (this.donorInitialScreening.DonationType === "InHouse") {
                this.showInHouseOptions = true;

                this.showMobileBloodDonationFields = false;
                this.showPatientDirectedFields = false;
            }
            else if (this.donorInitialScreening.DonationType === "Mobile") {
                this.showMobileBloodDonationFields = true;

                this.showInHouseOptions = false;
                this.showPatientDirectedFields = false;
            }
        }

        public onInHouseTypeChange(): void {
            if (this.donorInitialScreening.InHouseTypeValue === "PatientDirected") {
                this.showPatientDirectedFields = true;
            }
            else {
                this.showPatientDirectedFields = false;
            }
        }

        public onBloodDonatorChange(): void {
            if (this.donorInitialScreening.BloodDonator === "Repeat") {
                this.showRecentDonations = true;
            }
            else if (this.donorInitialScreening.BloodDonator === "Lapsed") {
                this.showRecentDonations = true;
            }
            else {
                this.showRecentDonations = false;
            }
        }

        public onChangeLog(records: Array<IDonorRecentDonationDto>): void {
            this.recentDonations = records.map(x => { return { DonorRecentDonationId: null, Agency: x.Agency, NumberOfDonation: x.NumberOfDonation, DateOfRecentDonation: x.DateOfRecentDonation, PlaceOfRecentDonation: x.PlaceOfRecentDonation }; });
        }

        protected onDeferred(): void {
            //this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
            //if (this.formValid === false) {
            //    return;
            //}

            this.mark_deferred(`Are you sure you want to tag this donor as deffered?`, 'Mark Donor as Deferred', 'Mark as Deferred', 'Cancel', this.onDeferralConfirmation);
        }

        public async onDeferralConfirmation(confirm: boolean, result: any): Promise<void> {
            if (confirm) {
                this.donorInitialScreening.DonorStatus = DonorStatus.Deferred;
                this.donorInitialScreening.DeferralStatus = result[0].DeferralStatus;
                this.donorInitialScreening.Remarks = result[0].Remarks;
                await this.onSubmit();
            }
        }

        protected onApprove(): void {
            this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
            if (this.formValid === false) {
                return;
            }

            this.confirm(`Are you sure you want to proceed with approving this donor?`, 'Approve Donor', 'Approve', 'Cancel', this.onApprovalConfirmation);
        }

        public async onApprovalConfirmation(confirm: boolean): Promise<void> {
            if (confirm) {
                this.donorInitialScreening.DonorStatus = DonorStatus.ForBloodIssuance;
                await this.onSubmit();
            }
        }

        public async onSubmit(): Promise<void> {
            let loader = this.showLoader();
            try {
                this.donorInitialScreening.RecentDonations = this.recentDonations;
                await this.donorScreeningService.upsertInitialScreening(this.donorInitialScreening);
                this.notify_success('Form successfully submitted.');

                this.$router.push({ path: '/donors' });
            }
            catch (error: any) {
                if (error.StatusCode != 500) {
                    let errorMessage = error.Message ?? "An error occured while processing your request.";
                    this.notify_error(errorMessage);
                }
            }
            finally {
                loader.hide();
            }
        }

    }</script>

<style lang="scss" scoped>
    .radio-container-row {
        margin-top: 4px;
    }

    table, th, td {
        border: 1px solid rgb(139, 138, 138);
        border-collapse: collapse;
    }
</style>

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
