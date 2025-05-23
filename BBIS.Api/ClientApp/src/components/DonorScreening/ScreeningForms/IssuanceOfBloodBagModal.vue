<template>
    <v-form class="form-container" ref="form" v-model="formValid" lazy-validation>
        <v-row class="mb-4" align="center">
            <!-- Blood Bag to be Used -->
            <v-col cols="12" md="4">
                <label class="font-weight-bold">Blood Bag to be Used</label>
                <v-select :items="bloodBagCollectionOptions"
                          label="Blood Bag to be Used"
                          v-model="newDonorBloodBagIssuance.BloodBagToBeUsed"
                          dense
                          outlined />
            </v-col>
            <v-col cols="12" md="8">
                <v-radio-group row v-model="newDonorBloodBagIssuance.BloodBagType">

                    <v-radio v-for="(type, index) in bloodBagCollectionSubOptions"
                             :key="index"
                             :label="type.text"
                             :value="type.value" />
                </v-radio-group>
            </v-col>
        </v-row>

        <v-row align="center">
            <v-col cols="12" sm="6" md="4">
                <label class="font-weight-bold">Segment Serial Number</label>
                <v-text-field dense
                              outlined
                              label="Segment Serial Number"
                              v-model="newDonorBloodBagIssuance.SegmentSerialNumber" />
            </v-col>
            <v-col cols="12" sm="6" md="4">
                <label class="font-weight-bold">Unit Serial Number</label>
                <v-text-field dense
                              outlined
                              label="Unit Serial Number"
                              v-model="newDonorBloodBagIssuance.UnitSerialNumber" />
            </v-col>

            <v-col cols="12" md="4" class="text-left; mb-7">
                <v-btn color="red darken-2" class="white--text mt-6" rounded @click="onPrint">
                    PRINT BARCODE
                </v-btn>

            </v-col>
            <div class="section-outer-container mt-3 pb-5">
                <div class="text-right">
                    <v-btn color="default" large tile class="mr-2" @click="onApprove"><v-icon color="success" size="25" left>mdi-check</v-icon> Issue Blood Bag</v-btn>
                    <v-btn color="red" @click="$emit('close')">Close</v-btn>
                </div>
            </div>
        </v-row>
        <div id="printBarcode" style="display: none;">
            <div class="text-center mx-auto" style="max-width: 300px;">
                <img id="barcode" class="pa-1" />
                <img id="barcode" class="pa-1" />
                <img id="barcode" class="pa-1" />
                <img id="barcode" class="pa-1" />
                <img id="barcode" class="pa-1" />
                <img id="barcode" class="pa-1" />
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
    import { DonorBloodBagIssuanceDto, IDonorBloodBagIssuance } from '@/models/DonorScreening/DonorBloodBagIssuance';
    import { ILookupOptions } from '@/models/Lookups/LookupOptions';
    import { LookupKeys } from '@/models/Enums/LookupKeys';
    import { DonorStatus } from '@/models/Enums/DonorStatus';
    import { BloodBagCollections } from '@/models/Enums/BloodBagCollections';
    import JsBarcode from 'jsbarcode'
    //import RecentDonationTable from '@/components/DonorScreening/ScreeningForms/RecentDonationTable.vue';
    @Component
    export default class IssuanceOfBloodBagModal extends VueBase {
        protected donorModule: DonorModule = getModule(DonorModule, this.$store);
        protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
        protected donorScreeningService: DonorScreeningService = new DonorScreeningService();

        protected formValid: boolean = true;
        protected rules: any = { ...Common.ValidationRules }

        protected generatedSerialNumber: string = '';
        protected errorMessage: string = '';
        protected showInHouseOptions: boolean = false;
        protected showRecentDonations: boolean = false;
        protected showPatientDirectedFields: boolean = false;
        protected showMobileBloodDonationFields: boolean = false;
        protected donorBloodBagIssuance: IDonorBloodBagIssuance = new DonorBloodBagIssuanceDto();

        protected newDonorBloodBagIssuance: IDonorBloodBagIssuance = new DonorBloodBagIssuanceDto();
        protected isEditingValue: boolean = false;
        protected isDisabled: boolean = true;

        public async created(): Promise<void> {

            let loader = this.showLoader();

            try {

                await this.getDonorBloodBagIssuance();
            }
            catch (error) {
                console.log(error);
            }
            finally {
                loader.hide();
            }
        }

     
        public get options(): (key: string) => Array<ILookupOptions> {
            return (key) => this.lookupModule.getOptionsByKey(key);
        }
        protected get bloodBagCollectionOptions(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.BloodBagCollectionTypes).map(x => { return { text: x.Text, value: x.Value } });
        }

        public get bloodBagCollectionSubOptions(): Array<{ text: string, value: string }> {
            if (this.newDonorBloodBagIssuance.BloodBagToBeUsed === BloodBagCollections.KARMI || this.newDonorBloodBagIssuance.BloodBagToBeUsed === BloodBagCollections.TERUMO) {
                return this.bloodBagsSizeOptions;
            }
            else if (this.newDonorBloodBagIssuance.BloodBagToBeUsed === BloodBagCollections.SpecialBag) {
                return this.specialBagOptions;
            }
            else if (this.newDonorBloodBagIssuance.BloodBagToBeUsed === BloodBagCollections.Apheresis) {
                return this.apheresisBagOptions;
            }
            else {
                return [];
            }
        }

        protected get bloodBagsSizeOptions(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.BloodBagSizeTypes).map(x => { return { text: x.Text, value: x.Value } });
        }

        protected get specialBagOptions(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.SpecialBagTypes).map(x => { return { text: x.Text, value: x.Value } });
        }

        protected get apheresisBagOptions(): Array<{ text: string, value: string }> {
            return this.options(LookupKeys.ApheresisBagTypes).map(x => { return { text: x.Text, value: x.Value } });
        }

        protected onApprove = (): void => {

            const form = this.$refs.form as any;

            if (!form) {
                this.notify_error('Form is not ready yet.');
                return;
            }

            this.formValid = form.validate();
            if (this.formValid === false) {
                return;
            }

            this.confirm(`Are you sure you want to proceed with approving this donor?`, 'Approve Donor', 'Approve', 'Cancel', this.onApprovalConfirmation);
        };

        protected onPrint(): void {

            this.generatedSerialNumber = this.newDonorBloodBagIssuance.SegmentSerialNumber;
            if (Common.hasValue(this.generatedSerialNumber)) {
                JsBarcode("#barcode", `${this.generatedSerialNumber}`, {
                    width: 2,
                    height: 60,
                    textAlign: 'left'
                });
            }
            this.$htmlToPaper('printBarcode');


        }

        protected async getDonorBloodBagIssuance(): Promise<void> {
            console.log(this.$route.params.reg_id);
            if (this.$route.params.reg_id && typeof (this.$route.params.reg_id) === 'string') {
                let regId = this.$route.params.reg_id;
                this.donorBloodBagIssuance = await this.donorScreeningService.getBloodBagIssuance(regId);
                console.log(this.donorBloodBagIssuance);

                this.newDonorBloodBagIssuance.DonorTransactionId = this.donorBloodBagIssuance.DonorTransactionId

                this.donorModule.setTransactionId(this.donorBloodBagIssuance.DonorTransactionId);
            }
        }


        public async onApprovalConfirmation(confirm: boolean): Promise<void> {
            if (confirm) {
                this.donorBloodBagIssuance.DonorStatus = DonorStatus.ForBloodCollection;
                this.$emit('Issued', this.donorBloodBagIssuance.SegmentSerialNumber);
                await this.onSubmit();
            }
        }

        public async onSubmit(): Promise<void> {
            console.log(this.newDonorBloodBagIssuance);
            let loader = this.showLoader();
            try {
                //this.donorVitalSigns.RecentDonations = this.recentDonations;
                await this.donorScreeningService.upsertBloodBagIssuance(this.newDonorBloodBagIssuance);
                this.notify_success('Form successfully submitted.');

                this.$emit('close');
            }
            catch (error: any) {
                if (error.StatusCode != 500) {
                    let errorMessage = error.Message ?? "An error occured while processing your request.";
                    this.notify_error(errorMessage);
                    console.log(error);
                }
            }
            finally {
                loader.hide();
            }
        }
    }
</script>