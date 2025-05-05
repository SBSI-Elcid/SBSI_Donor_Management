<template>
  <v-row justify="center">
    <v-dialog v-model="showDialog" persistent max-width="600px">     
      <v-card>
        <v-card-title>User Profile</v-card-title>
        <v-card-text>
          <v-form ref="form" v-model="formValid" lazy-validation>
            <v-container class="mt-3">
               <v-row no-gutters>
                <v-col cols="4">
                  <div>First name</div>                  
                  <div class="font-weight-bold"> {{ userProfileDetails.FirstName }} </div>
                </v-col>

                <v-col cols="4">
                  <div>Last name</div>
                  <div class="font-weight-bold"> {{ userProfileDetails.LastName }} </div>
                </v-col>

                <v-col cols="4">
                  <div>Email Address</div>
                  <div class="font-weight-bold">{{ userProfileDetails.EmailAddress }} </div>
                </v-col>
              </v-row>

              <v-divider class="my-3"/>
 
              <v-row no-gutters>
                <v-col cols="12">
                  <v-switch class="mt-0" outlined dense v-model="changePassword" label="Change Password" color="primary" />
                </v-col>
                <v-col cols="12">
                  <label>User name</label>
                  <v-text-field outlined dense v-model="userProfileDetails.Username" :error-messages="usernameUniqueError" :rules="[rules.required, rules.maxLength(45)]" required></v-text-field>
                </v-col>
                <v-col cols="12" v-if="changePassword">
                  <label>Current Password</label>
                  <v-text-field outlined dense v-model="model.CurrentPassword" :error-messages="invalidCurrentPassword" :rules="[rules.required, rules.maxLength(250)]" required></v-text-field>
                </v-col>
                <v-col cols="12" v-if="changePassword">
                  <label>New Password</label>
                  <v-text-field outlined dense v-model="model.NewPassword" :rules="[rules.required, rules.maxLength(250)]" required></v-text-field>
                </v-col>
                <v-col cols="12" v-if="changePassword">
                  <label>Retype New Password</label>
                  <v-text-field outlined dense v-model="retypePassword" :error-messages="notMatchError" :rules="[rules.required, rules.maxLength(250)]" required>
                  </v-text-field>
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
import UserProfileViewDto from '@/models/UserAccounts/UserProfileViewDto';
import UserProfileUpdateDto, { IUserProfileUpdateDto } from '@/models/UserAccounts/UserProfileUpdateDto';
import UserAccountsService from '@/services/UserAccountsService';
import { Component, Emit, Prop } from 'vue-property-decorator';
import VueBase from '../../components/VueBase';
import Common from '@/common/Common';

@Component({ components: { } })
export default class UserProfile extends VueBase {
  @Prop({ required: true })
  public toggle!: boolean;

  @Prop({ required: true })
  public userId!: Guid;

  protected userAccountService: UserAccountsService = new UserAccountsService();

  protected formValid: boolean = true;
  protected rules: any = {...Common.ValidationRules }
  protected notMatchError: string | null = null;
  protected usernameUniqueError: string = '';
  protected invalidCurrentPassword: string = '';

  protected userProfileDetails: UserProfileViewDto = new UserProfileViewDto();
  protected model: IUserProfileUpdateDto = new UserProfileUpdateDto();
  protected retypePassword: string = "";
  protected changePassword: boolean = false;
  
  protected get showDialog(): boolean {
    return this.toggle;
  }

  protected async loadUserAccount(): Promise<void> {
    try {
      let user = await this.userAccountService.getUser(this.userId);
      this.userProfileDetails = {
        UserId: user.UserAccountId,
        FirstName: user.Firstname,
        LastName: user.Lastname,
        EmailAddress: user.EmailAddress,
        Username: user.Username
      };
    } 
    catch (error) {
      console.log(error);
    }
  }

  protected async save(): Promise<void> {
    if(this.changePassword && this.retypePassword !== this.model.NewPassword) {
      this.notMatchError = "Password did not match.";
      return;
    }

    this.model.UserId = this.userProfileDetails.UserId;
    this.model.Username = this.userProfileDetails.Username;

    this.userAccountService.updateProfile(this.model)
      .then((res: any) => {
        let error: any = res.Data; 
        
        //Item1 is the field name; Item2 is error message
        if(!Common.isNull(error.Item1) || !Common.isNull(error.Item2)) {
          let errorMessage = 'Error updating profile. ';

          if (error.Item1 === 'Username'){
            this.usernameUniqueError = error.Item2;
          }
          else if (error.Item1 === 'CurrentPassword') {
            this.invalidCurrentPassword = error.Item2;
          }
          else {
            errorMessage = errorMessage.concat(error.Item2);
          }

          this.notify_warning(errorMessage);
        }
        else {
          this.notify_success('Profile was successfully updated');
          this.onClose(true);
        }        
      })
      .catch((err: any) => {
        if (err.StatusCode != 500) {
          this.notify_error(err.Message);
        }
      });  
  }

  protected async mounted(): Promise<void> {
    await this.loadUserAccount();
  }

  @Emit("onClose") 
  private onClose(close: boolean): boolean {
    return close;
  }
}
</script>