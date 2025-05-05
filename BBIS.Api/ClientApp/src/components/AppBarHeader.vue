<template>
  <div>

    <UserProfile v-if="showUserProfile" :toggle="showUserProfile" :userId="userId" @onClose="userProfileClose" />

    <v-app-bar app flat outlined height="65px">
      <v-app-bar-nav-icon class="hidden-lg-and-up" @click.stop="$emit('toggleDrawer')" />
      <v-toolbar-title style="width: 300px"  class="ml-0 pl-4" >
        <span class="hidden-sm-and-down text-subtitle-2 grey--text">{{ title }}</span>
      </v-toolbar-title>    

      <v-spacer />

      <span v-if="isAuthenticated" style="font-size:14px;" class="pr-2 grey--text"><strong>{{ authData.Name }}</strong></span>
      <v-btn icon @click="onShowUserProfile">
        <v-icon>mdi-account</v-icon>
      </v-btn> 
      <v-btn icon @click="logout">
        <v-icon>mdi-logout</v-icon>
      </v-btn>   
    </v-app-bar>
  </div>
</template>

<script lang="ts">
import { JwtData } from '@/models/JwtData';
import BrowserStorageService from '@/services/BrowserStorageService';
import jwtDecode from 'jwt-decode';
import { Component, Vue } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import AuthModule from '../store/AuthModule';
import UserProfile from '@/components/UserAccounts/UserProfile.vue';
import { Store } from 'vuex';

@Component( {components: { UserProfile }})
export default class AppBarHeader extends Vue {
  protected authState: AuthModule = getModule(AuthModule, this.$store);
  protected drawer: boolean = true;
  protected title: string = 'Blood Bank Information System';
  protected storage: BrowserStorageService = new BrowserStorageService();
  protected showUserProfile: boolean = false;
  protected userId: Guid = '';
  
  protected async logout() {  
    this.storage.clear();
    await this.authState.signOut();
    await this.$router.push({ name: 'login' });
  }

  protected onShowUserProfile(): void {
    this.showUserProfile = true;
    this.userId = this.authData.UserId;
  }

  protected userProfileClose(state: any): void {
    this.showUserProfile = false;
  }

  protected get isAuthenticated(): boolean {
    return this.token !== null;
  }

  private get token(): string | null {
    return this.authState.authToken;
  }
  
  protected get authData(): any {
    if (!this.token) {
      return {};
    }
    let data = jwtDecode(this.token) as JwtData;

    return data;
  }
  
}
</script>
