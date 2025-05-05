<template>
  <v-container fluid>
    <v-card>
      <v-card-actions>
        <h2 class="ml-2 mt-1 head-title ms-4 grey--text">Application Settings</h2> 
      </v-card-actions>
      <v-divider></v-divider>
      
      <v-tabs optional vertical class="mt-3">
        <v-tab v-for="tab in tabs" :key="tab.name" v-model="selectedTab" :to="tab.route">
          <v-icon left>{{ tab.icon }}</v-icon>
          <span class="tabs-name">{{ tab.name }}</span>
        </v-tab>
        <v-tabs-items>
          <router-view />
        </v-tabs-items>
      </v-tabs>

    </v-card>
  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component } from 'vue-property-decorator';

@Component
export default class ApplicationSettingsView extends VueBase { 
  public selectedTab: string = '';
  public tabs: Array<{name: string, icon: string, route: string}> = [
    {
      name: 'Unit of Measurement',
      icon: 'mdi-ruler',
      route: '/app-settings/unit-of-measurement',
    },
    {
      name: 'Blood Components',
      icon: 'mdi-water',
      route: '/app-settings/blood-component',
    },
    /*{
      name: 'Institutions',
      icon: 'mdi-hospital-building',
      route: '/app-settings/institutions',
    },*/
    {
      name: 'Blood Component Checklist',
      icon: 'mdi-list-box-outline',
      route: '/app-settings/blood-component-checklist'
    },
    {
      name: 'Test Order Type',
      icon: 'mdi-test-tube',
      route: '/app-settings/test-order-type'
    }
  ];
  
}
</script>

<style lang="scss" scoped>
  .tabs-name{
    text-align: left;
    width: 200px;
  }

  .header-wrapper{
    padding: 12px 12px 12px 17px;
  }
</style>