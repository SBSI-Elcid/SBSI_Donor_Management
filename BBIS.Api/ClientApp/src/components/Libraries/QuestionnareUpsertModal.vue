<template>
    <v-row justify="center">
        <v-dialog v-model="showDialog" persistent max-width="600px">
            <v-card>
                <v-card-title> Questionnare Management </v-card-title>
                <v-card-text>
                    <v-form ref="form" v-model="formValid" lazy-validation>
                        <v-container class="mt-3">
                            <v-row no-gutters>
                                <v-col cols="12">
                                    <label>Header Text</label>
                                    <v-textarea v-model="librariesQuestionnare.HeaderText"  rows="2" no-resize dense outlined />
                                </v-col>
                            </v-row>
                            <v-row no-gutters>
                                <v-col cols="12">
                                    <label>Tagalog Text</label>
                                    <v-textarea v-model="librariesQuestionnare.QuestionTagalogText"  rows="2" no-resize dense outlined />
                                </v-col>
                            </v-row>
                            <v-row no-gutters>
                                <v-col cols="12">
                                    <label>English Text</label>
                                    <v-textarea v-model="librariesQuestionnare.QuestionEnglishText"  rows="2" no-resize dense outlined />
                                </v-col>
                            </v-row>
                            <v-row no-gutters>
                                <v-col cols="12">
                                    <label>Other Dialect Text</label>
                                    <v-textarea v-model="librariesQuestionnare.QuestionOtherDialectText"  rows="2" no-resize dense outlined />
                                </v-col>
                            </v-row>
                            <v-row no-gutters>
                                <v-col cols="12">
                                    <label>Gender Option</label>
                                    <v-select v-model="librariesQuestionnare.GenderOption"
                                              :items="['Male', 'Female']"
                                              dense
                                              outlined
                                              ></v-select>
                                </v-col>
                            </v-row>
                            <v-row no-gutters>
                                <v-col cols="12">
                                    <label>Order No</label>
                                    <v-textarea type="number" v-model="librariesQuestionnare.OrderNo"  rows ="1"  no-resize dense outlined />
                                </v-col>
                            </v-row>
                        </v-container>
                    </v-form>
                </v-card-text>
                <v-divider />
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="default darken-1" text @click="onClose(false)">Cancel</v-btn>
                    <v-btn color="primary" text @click="onApprove">Save</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-row>
</template>

<script lang="ts">
 import VueBase from '@/components/VueBase';
import { Component, Emit, Prop, Watch } from 'vue-property-decorator';
import Common from '@/common/Common';
import ApplicationSettingService from '@/services/ApplicationSettingService';
import { BloodComponentChecklistDto, IBloodComponentChecklistDto } from '@/models/ApplicationSetting/BloodComponentChecklistDto';
import { IBloodComponentDto } from '@/models/ApplicationSetting/IBloodComponentDto';
import { IRoleDto, RoleDto } from '../../models/ApplicationSetting/RoleDto';
import { IMedicalQuestionnaireDto, MedicalQuestionnaireDto } from '../../models/DonorRegistration/MedicalQuestionnaireDto';


@Component({ components: { } })
export default class QuestionnareUpsertModal extends VueBase {
    @Prop({ required: true })
    public toggle!: boolean;


    @Prop({ default: new MedicalQuestionnaireDto() })
    public questionnareDto!: IMedicalQuestionnaireDto;



    protected librariesService: ApplicationSettingService = new ApplicationSettingService();

    protected librariesQuestionnare: IMedicalQuestionnaireDto = new MedicalQuestionnaireDto();


    protected rules: any = {
        ...Common.ValidationRules, camelCase: (v: string) => {
            return /^[A-Z][a-zA-Z0-9]*$/.test(v) || 'Must start with uppercase and have no spaces';
        } }
    protected formValid: boolean = true;


    protected get showDialog(): boolean {
        return this.toggle;
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

        this.confirm(`Are you sure you want to proceed with saving this Role?`, 'Approve Role', 'Approve', 'Cancel', this.onApprovalConfirmation);
    };

    public async onApprovalConfirmation(confirm: boolean): Promise<void> {
        if (confirm) {
            await this.onSubmit();
        }
    }

    created() {
        this.librariesQuestionnare = this.questionnareDto;
        //this.librariesRole.RoleId = this.id;
        //this.librariesRole.RoleName = this.roleName;
    }

    public async onSubmit(): Promise<void> {
        /*console.log(this.librariesRole);*/
        let loader = this.showLoader();
        try {
            //this.donorVitalSigns.RecentDonations = this.recentDonations;
            await this.librariesService.upsertLibrariesQuestionnare(/*this.librariesRole*/);
            this.notify_success('Form successfully submitted.');



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




  @Emit("onClose")
  private onClose(refresh: boolean): boolean {
    let form: any = this.$refs.form as any;
    if (!Common.isNull(form)) {
      form.resetValidation();
    }

     /* this.librariesRole = new RoleDto();*/

    return refresh;
  }
}
</script>