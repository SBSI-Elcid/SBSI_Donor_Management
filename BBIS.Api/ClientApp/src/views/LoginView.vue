<template>
    <v-container fluid fill-height>
      <v-layout align-center justify-center>
        <v-flex xs12 sm8 md4>
          <v-card elevation="4" light max-width="550px">
            <v-toolbar dark color="#B92F2F">
              <v-img src="https://cdn.bitrix24.com/b14890059/landing/656/656f65e51eb5be34a62faef7ef28d6e7/SBSI-Logo-1_1x.png" max-height="50" max-width="80" />
              <v-toolbar-title class="text-subtitle-1 pl-3"><span class="text-subtitle-2">Sign in to your account</span></v-toolbar-title>
            </v-toolbar>
            <v-divider></v-divider>
            <v-card-text>
              <p class="pt-2 pl-5 pb-2 text-subtitle-1">Sign in with your username and password:</p>

              <v-form ref="form" v-model="formValid" lazy-validation>
                  <v-text-field 
                    dense outlined
                    :disabled="apiRequestActive" 
                    label="Login" 
                    name="login" 
                    prepend-icon="mdi-account" 
                    type="text" 
                    v-model="loginDto.Username" 
                    :rules="userNameRequired"
                    auto-complete="off">
                  </v-text-field>
                  <v-text-field 
                    dense outlined
                    :disabled="apiRequestActive" 
                    id="password" 
                    label="Password" 
                    name="password" 
                    prepend-icon="mdi-lock" 
                    type="password" 
                    v-model="loginDto.Password"  
                    @keydown="keydown" 
                    :rules="passwordRequired"
                    auto-complete="off">
                  </v-text-field>
              </v-form>
              
              <v-progress-linear v-if="apiRequestActive" indeterminate color="error"></v-progress-linear>
              
              <p v-if="showError" class="text-body-2 text-center" style="color: red;">{{ errorMessage }}</p>
            
            </v-card-text>
          <v-divider></v-divider>
          <v-card-actions :class="{ 'pa-3': $vuetify.breakpoint.smAndUp }">
            <v-spacer></v-spacer>
            <v-btn v-if="!apiRequestActive" color="default" :large="$vuetify.breakpoint.smAndUp" @click="authenticate">
              <v-icon left>mdi-login-variant</v-icon> Login
            </v-btn>
          </v-card-actions>
        </v-card>
        </v-flex>
         
      </v-layout>
     
    </v-container>
</template>


<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import AuthModule from '../store/AuthModule';
import AuthService from '../services/AuthService';
import { LoginDto } from '../models/LoginDto';
import { AuthenticationResult } from '../models/AuthenticationResult';
import { RequestResult } from '@/common/RequestResult';
import BrowserStorageService from '@/services/BrowserStorageService';
import VueBase from '@/components/VueBase';
import UserAccountsService from '@/services/UserAccountsService';

@Component({})
export default class Login extends VueBase { 
  private authState: AuthModule = getModule(AuthModule, this.$store);
  private authService: AuthService = new AuthService();
  private userAccountService: UserAccountsService = new UserAccountsService();
  protected storage: BrowserStorageService = new BrowserStorageService();

  private loginDto: LoginDto = { Username: '', Password: '' };
  private showError: boolean = false;
  private errorMessage: string = '';
  private formValid: boolean = true;
  private userNameRequired: any = [(v: any) => !!v || 'Please enter your username'];
  private passwordRequired: any = [(v: any) => !!v || 'Please enter your password'];
  private apiRequestActive: boolean = false;
  private platformName: string =  'Blood Bank Inventory System';
 
  protected async authenticate(): Promise<void> {

    this.formValid = (this.$refs.form as Vue & { validate: () => boolean }).validate();
    if(!this.formValid) return;

    try {
      this.apiRequestActive = true;
      this.showError = false;
      this.errorMessage = '';
     
      const authResultDto: RequestResult<AuthenticationResult> =  await this.authService.authenticate(this.loginDto);
      if (authResultDto.Success && authResultDto.Data?.Success) {
        this.storage.setItem("currentPath", "");
        await this.authState.setAuthenticationResult(authResultDto.Data);
     
        this.$router.push({ path: '/home' });
      }
      else {
        //display error message
        this.showError = true;
        this.errorMessage = 'Login failed, invalid username or password.';
      }     
      this.apiRequestActive = false;
    } 
    catch (error) {
      this.apiRequestActive = false;
      console.log(error);
    }   
  }

  protected async keydown(e: any) {
    if(e.keyCode == 13) {
     await this.authenticate(); 
    }    
  }

  protected async mounted(): Promise<void> {
    await this.getOfflineStatus();
  }

}

</script>