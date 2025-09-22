<template>
  <div>
    <v-navigation-drawer color="#B92F2F" app dark v-model="drawer">
      <div class="pa-2">
        <div class="logo">
          <v-img src="@/assets/logo.png" max-height="50" max-width="80" contain />
        </div>
        <div class="company-name">
          <span class="white--text">
            Scientific Biotech Specialties, Inc.
          </span>
        </div>
      </div>

      <v-divider></v-divider>

      <v-list dense>
        <template v-for="(item, index) in userModules">            
          <v-list-group v-if="item.IsParentMenu" :key="index" v-model="item.Toggle" >
            <template v-slot:activator>
              <v-list-item-action v-if="item.Icon">
                <v-icon>{{ item.Icon }}</v-icon> 
              </v-list-item-action>
              <v-list-item-content> 
                <v-list-item-title>{{ item.Menu }}</v-list-item-title>
              </v-list-item-content>
            </template>
             <!-- For Submenu items -->
             <template v-for="(child, i) in item.SubMenuItems">   
              <v-list-group v-if="child.IsParentMenu" :key="i"  >
                <template v-slot:activator>

                    <v-list-item-action v-if="child.Icon"> <v-icon>{{ child.Icon }}</v-icon> </v-list-item-action>
                    <v-list-item-content>
                      <v-list-item-title>{{ child.Menu }}</v-list-item-title>
                    </v-list-item-content>

                </template>
              </v-list-group>

                <v-list-item v-else :key="i" link :to="child.Link" class="ml-4"> 
                  <v-list-item-action v-if="child.Icon"> <v-icon>{{ child.Icon }}</v-icon> </v-list-item-action>
                  <v-list-item-content>
                    <v-list-item-title>{{ child.Menu }}</v-list-item-title>
                  </v-list-item-content>
                </v-list-item>
              </template>
          </v-list-group>

          <v-list-item v-else :key="index" link :to="item.Link">
            <v-list-item-action>
              <v-icon>{{ item.Icon }}</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>{{ item.Menu }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </template>  
      </v-list>

    </v-navigation-drawer>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import AuthModule from '../store/AuthModule';
import VueBase from './VueBase';
import { ModuleDto } from '@/models/UserAccounts/ModuleDto';
import UserAccountsService from '@/services/UserAccountsService';

@Component
export default class Sidebar extends VueBase {
  protected authState: AuthModule = getModule(AuthModule, this.$store);
  private userAccountService: UserAccountsService = new UserAccountsService();
  protected tempModel: boolean = false;
  public drawer: boolean = true;
  protected userModules: Array<ModuleDto> = [];

  protected get isAuthenticated(): boolean {
    return this.authState.isAuthenticated;
  }

  protected get authData(): any {
    return this.authState.userJwtData;
  }
 
    protected async mounted(): Promise<void> {
        console.log(this.isOffline);
    //if (!this.isOffline) {
        if (this.authState.userModules.length == 0) {
            console.log("FirstIF");
          const userModules = await this.userAccountService.getUserModules(this.authState.userId);
          
          this.userModules = [{ Menu: 'Dashboard', Icon: 'mdi-table', Link: '/home', IsParentMenu: false, ModuleId: '', OrderNo: 0, SubMenuItems: [], Toggle: false, ToggleChild: false, ParentModuleId: null }, ...userModules]
         
        await this.authState.setUserModules(this.userModules);
      }
        else {
            console.log("SecondIF");
        this.userModules = this.authState.userModules;
      }
    
   // else {
      //this.userModules =  [
      //  { Menu: 'Dashboard', Icon: 'mdi-table', Link: '/home', IsParentMenu: false, ModuleId: '', OrderNo: 1, SubMenuItems: [], Toggle: false, ToggleChild: false, ParentModuleId: null },
      //  { Menu: 'Registrations', Icon: 'mdi-account-details-outline', Link: '/registrations', IsParentMenu: false, ModuleId: '', OrderNo: 1, SubMenuItems: [], Toggle: false, ToggleChild: false, ParentModuleId: null },
      //  { Menu: 'Donors', Icon: 'mdi-account-group-outline', Link: '/donors', IsParentMenu: false, ModuleId: '', OrderNo: 1, SubMenuItems: [], Toggle: false, ToggleChild: false, ParentModuleId: null },
      //];
    //}
  }
}
</script>

<style lang="scss" scoped>
 .mt-2 {
    margin-top: 6px !important;
  }

  .logo {
    display: inline-block;
  }

  .company-name {
    display: inline-block;
    width: 144px;
    padding-left: 20px;
    font-size: 14px;
    font-weight: 400;
    font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
  }

  .v-application .primary--text {
    color: #ffffff !important;
    caret-color: #ffffff !important;
  }
</style>
