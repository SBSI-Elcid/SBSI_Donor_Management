<template>
  <v-card max-width="290" max-height="64">
     <v-tooltip right>
        <template v-slot:activator="{ on, attrs }">
          <v-card-text class="text-h6 grey--text" v-bind="attrs" v-on="on">
            <v-icon left large v-if="statusText=='Qualified'" :color="colorCard">mdi-check-circle</v-icon>
            <v-icon left large v-else :color="colorCard">mdi-account-cancel</v-icon>
            {{ addSpaceBetweenUpperCaseLetters(statusText) }} 
          </v-card-text>
        </template>
        <span>{{donorStatus.Remarks}}</span>
      </v-tooltip>
  </v-card>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Prop, } from 'vue-property-decorator';
import Common from '@/common/Common';
import { IVerifyDonorResultDto } from '@/models/DonorRegistration/VerifyDonorResultDto';

@Component
export default class StatusCard extends VueBase { 

  @Prop()
  public donorStatus!: IVerifyDonorResultDto

  protected showDetails: boolean = false;

  protected get colorCard(): string {
    return this.donorStatus.IsValid ? 'success' : 'red' 
  }

  protected get statusText(): string {
    return Common.isNull(this.donorStatus.DeferralStatus) ? 'Qualified' : this.donorStatus.DeferralStatus;
  }

  protected get hasDetails(): boolean {
    return Common.hasValue(this.donorStatus.Remarks) ? true : false;
  }
}
</script>