<template>
  <v-card min-width="250">
    <v-card-text >
        <div class="mx-auto text-center">
        <v-icon medium :color="iconColor" class="pb-2">{{ vicon }}</v-icon>
        <h1 class="font-weight-medium pt-3 pb-3">{{ count }}</h1>
        <p class="grey--text text-caption font-italic">{{ text }}</p>
        </div>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Prop, } from 'vue-property-decorator';

@Component
export default class DashboardTile extends VueBase { 

  @Prop( {default: 'mdi-account-group-outline'} )
  public vicon!: string;

  @Prop( {default: 'default'} )
  public iconColor!: string;

  @Prop( {default: 0 } )
  public count!: number;

  @Prop( {default: ''} )
  public text!: string;
 
}
</script>