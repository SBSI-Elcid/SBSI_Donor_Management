<template>
    <v-row justify="center">
        <v-dialog v-model="showDialog" persistent max-width="750px">
            <v-card>
                <v-card-title>Update User Account</v-card-title>
                <v-divider></v-divider>
                <v-card-text>
                    <v-form ref="form" v-model="formValid" lazy-validation>
                        <v-container class="mt-3">
                            <v-row>
                                <v-col cols="6">
                                    <v-row no-gutters>
                                        <v-col cols="12">
                                            <label>User name</label>
                                            <v-text-field outlined dense v-model="model.Username" :rules="[rules.required, rules.maxLength(45)]" required></v-text-field>
                                        </v-col>
                                    </v-row>

                                    <v-row no-gutters>
                                        <v-col cols="12">
                                            <label>Email Address</label>
                                            <v-text-field outlined dense v-model="model.EmailAddress" :rules="[rules.required, rules.emailFormat, rules.maxLength(150)]" required></v-text-field>
                                        </v-col>
                                    </v-row>

                                    <v-row no-gutters>
                                        <v-col cols="12">
                                            <label>First name</label>
                                            <v-text-field outlined dense v-model="model.Firstname" :rules="[rules.required, rules.maxLength(50)]" required></v-text-field>
                                        </v-col>
                                    </v-row>

                                    <v-row no-gutters>
                                        <v-col cols="12">
                                            <label>Last name</label>
                                            <v-text-field outlined dense v-model="model.Lastname" :rules="[rules.required, rules.maxLength(50)]" required></v-text-field>
                                        </v-col>
                                    </v-row>

                                    <v-row no-gutters>
                                        <v-col cols="12">
                                            <label>Roles</label>
                                            <v-autocomplete outlined 
                                                            dense 
                                                            multiple chips 
                                                            small-chips 
                                                            v-model="model.RoleIds" 
                                                            :items="roleOptions" 
                                                            item-value="RoleId"
                                                            item-text="RoleName"
                                                            :rules="[rules.arrayRequired]"
                                                            required>
                                            </v-autocomplete>
                                        </v-col>
                                    </v-row>

                                    <v-row no-gutters>
                                        <v-col cols="6">
                                            <v-checkbox v-model="model.IsActive"
                                                        label="Is Active"></v-checkbox>
                                        </v-col>

                                        <v-col cols="6">
                                            <v-checkbox v-model="model.PasswordHasChange"
                                                        label="Change password"></v-checkbox>
                                        </v-col>
                                    </v-row>

                                    <v-row no-gutters v-if="model.PasswordHasChange">
                                        <v-col cols="12">
                                            <label>New Password</label>
                                            <v-text-field :disabled="!model.PasswordHasChange" outlined dense v-model="model.UpdatedPassword" :rules="[rules.required, rules.maxLength(250)]" required></v-text-field>
                                        </v-col>

                                        <v-col cols="12">
                                            <v-btn :disabled="!model.PasswordHasChange" depressed small color="default" @click="generatePassword"><v-icon left>mdi-form-textbox-password</v-icon>Generate new password</v-btn>
                                        </v-col>
                                    </v-row>
                                </v-col>

                                <v-col cols="6">
                                    <SelectUserModule :modulesProp="modules" :selectedModulesProp="selectedModules" @onChange="onModuleSelectionChange"></SelectUserModule>
                                </v-col>
                            </v-row>

                        </v-container>
                    </v-form>
                </v-card-text>
                <v-divider />
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="default darken-1" text @click="onClose(false)">Cancel</v-btn>
                    <v-btn color="primary" text @click="save">Save</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-row>
</template>

<script lang="ts">
    import UserAccountsService from '@/services/UserAccountsService';
    import { Component, Emit, Prop, Watch } from 'vue-property-decorator';
    import VueBase from '../../components/VueBase';
    import CommonOptions from '@/common/CommonOptions';
    import { IUpdateUserAccountDto, UpdateUserAccountDto } from '@/models/UserAccounts/UpdateUserAccountDto';
    import Common from '@/common/Common';
    import { ModuleDto } from '@/models/UserAccounts/ModuleDto';
    import SelectUserModule from './SelectUserModule.vue';
    import  ApplicationSettingService from '@/services/ApplicationSettingService';

    @Component({ components: { SelectUserModule } })
    export default class UpdateUserAccount extends VueBase {
        @Prop({ required: true }) public toggle!: boolean;
        @Prop({ required: true }) public id!: Guid;

        private roleService: ApplicationSettingService = new ApplicationSettingService();
        private userAccountService: UserAccountsService = new UserAccountsService();
        private formValid: boolean = true;
        private rules: any = { ...Common.ValidationRules }

        private errorMessage: string = '';
        private roleOptions: any = [];
        private model: IUpdateUserAccountDto = new UpdateUserAccountDto();
        private modules: Array<ModuleDto> = [];
        private roleSettings: IRoleDto[] = [];
        private selectedModules: Array<ModuleDto> = [];

        private get showDialog(): boolean {
            return this.toggle;
        }

        private generatePassword(): void {
            const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
            let result = ' ';
            const length = 8;
            const charactersLength = characters.length;
            for (let i = 0; i < length; i++) {
                result += characters.charAt(Math.floor(Math.random() * charactersLength));
            }

            this.model.UpdatedPassword = result;
        }

        private async save(): Promise<void> {
            this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
            if (!this.formValid) return;

            let loader = this.showLoader();
            try {
                this.model.UserModuleIds = this.selectedModules.map(x => x.ModuleId);
               /* Console.log(this.model.UserModuleIds);*/
                await this.userAccountService.updateUser(this.model);
                this.notify_success('User account successfully updated.');
                console.log(this.model.UserModuleIds);
                this.onClose(true);
            }
            catch (error: any) {
                if (error.StatusCode != 500) {
                    this.errorMessage = error.Message;
                    this.notify_error(this.errorMessage);
                }

            }
            finally {
                loader.hide();
            }
        }

        private async loadUserAccount(): Promise<void> {
            let loader = this.showLoader();

            try {
                let user = await this.userAccountService.getUser(this.id);
                this.model = {
                    UserAccountId: user.UserAccountId,
                    Username: user.Username,
                    Firstname: user.Firstname,
                    Lastname: user.Lastname,
                    EmailAddress: user.EmailAddress,
                    IsActive: user.IsActive,
                    RoleIds: user.RoleIds,
                    PasswordHasChange: false,
                    UpdatedPassword: null,
                    UserModuleIds: user.UserModules.map(x => x.ModuleId)
                };

                this.selectedModules = user.UserModules;
            }
            catch (error) {
                console.log(error);
            }
            finally {
                loader.hide();
            }
        }

        protected onModuleSelectionChange(selectedModules: Array<ModuleDto>): void {
            this.selectedModules = selectedModules;
        }

        @Emit("onClose")
        private onClose(refresh: boolean): boolean {
            (this.$refs.form as Vue & { reset: () => void }).reset();
            return refresh;
        }

        protected async mounted(): Promise<void> {
            if (this.toggle) {
                this.roleSettings = await this.roleService.getAllRoleSettings();
                this.roleOptions = this.roleSettings.map(x => ({
                    RoleId: x.RoleId,
                    RoleName: x.RoleName
                }));
                this.modules = await this.userAccountService.getAllModules();
                await this.loadUserAccount();
            }
        }
    }
</script>