<template>
  <v-row justify="center">
    <v-dialog v-model="showDialog" persistent max-width="600px">     
        <v-card>
            <v-card-title> Role Management </v-card-title>
            <v-card-text>
                <v-form ref="form" v-model="formValid" lazy-validation>
                    <v-container class="mt-3">
                        <v-row>
                            <v-col cols="6">
                                <label class="caption font-weight-medium">Role Name</label>
                                <v-text-field v-model="librariesRole.RoleName"
                                              :rules="[rules.required, rules.camelCase]"
                                              dense
                                              outlined />
                            </v-col>

                            <v-col cols="6">
                                <v-card outlined class="pa-2" style="max-height: 300px; overflow-y: auto;">
                                    <label class="caption font-weight-medium">Assign Tabs</label>

                                    <v-row dense>
                                        <v-col cols="12" v-for="(tab, index) in tabList" :key="index">
                                            <v-checkbox :label="tab"
                                                        :value="tab"
                                                        v-model="selectedTabs"
                                                        dense
                                                        hide-details />
                                        </v-col>
                                    </v-row>
                                </v-card>

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
    import { TabNames } from '@/models/Enums/TabNames';


@Component({ components: { } })
export default class RoleManagementUpsertModal extends VueBase {
    @Prop({ required: true })
    public toggle!: boolean;


    @Prop({ default: '' })
    public id!: string;

    @Prop({ default: '' })
    public roleName!: string;

  
    protected selectedTabs: Array<TabNames> = [];

    protected librariesService: ApplicationSettingService = new ApplicationSettingService();

    protected librariesRole: IRoleDto = new RoleDto();


    protected rules: any = {
        ...Common.ValidationRules, camelCase: (v: string) => {
            return /^[A-Z][a-zA-Z0-9]*$/.test(v) || 'Must start with uppercase and have no spaces';
        }
    };
    protected formValid: boolean = true;

    protected get tabList(): TabNames[] {
        return Object.values(TabNames);
    };

    protected get showDialog(): boolean {
        return this.toggle;
    };

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
        this.librariesRole.RoleId = this.id;
        this.librariesRole.RoleName = this.roleName;
    }

    public async onSubmit(): Promise<void> {
        console.log(this.librariesRole);
        let loader = this.showLoader();
        try {
            //this.donorVitalSigns.RecentDonations = this.recentDonations;
            await this.librariesService.upsertLibrariesRole(this.librariesRole);
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

      this.librariesRole = new RoleDto();
 
    return refresh;
  }
}
</script>