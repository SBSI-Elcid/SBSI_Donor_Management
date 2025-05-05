<template>
 <v-dialog v-model="showModal" :width="400" persistent>
    <v-card>
      <v-card-title>{{ title }}</v-card-title>
      <v-card-text>{{ messageBody }}</v-card-text>
      <v-card-actions class="px-4">
        <v-spacer />
        <v-btn text color="secondary" @click="okButtonClicked">Ok</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import { StorageKeysEnum } from '@/common/StorageKeysEnum';
import VueBase from '@/components/VueBase';
import BrowserStorageService from '@/services/BrowserStorageService';
import AuthModule from '@/store/AuthModule';
import { Component, Emit, Prop, Watch } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import jwt_decode from "jwt-decode";

@Component({ components: { } })
export default class SessionExpired extends VueBase {
  private authState: AuthModule = getModule(AuthModule, this.$store);
  protected storage: BrowserStorageService = new BrowserStorageService();
  protected showModal: boolean = false;
  protected title: string = "Session expired";
  protected messageBody: string = "Your session has expired. Click OK to login again.";

  protected get currentPath(): string {
    let path: string = this.storage.getItem("currentPath");
    return path;
  }

  protected get authToken(): string {
    let token: string = this.storage.getItem(StorageKeysEnum.Token);
    return token;
  }

  @Watch("isTokenExpired")
  protected onTokenExpires(): void {
    if(this.isTokenExpired == true && this.currentPath !== 'login' && this.currentPath !== 'portal' && this.currentPath !== 'register' && this.currentPath !== 'requisition') {
      this.showModal = true;
    }
  }


  protected get isTokenExpired(): boolean {
    return !this.authToken;
  }

  protected async okButtonClicked(): Promise<void> {
    this.showModal = false;
    this.storage.clear();
    await this.$router.push({ name: 'login' });
  }

  protected mounted(): void {
    this.onTokenExpires();
  }
}
</script>

