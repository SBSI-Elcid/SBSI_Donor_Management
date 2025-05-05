<template>
  <v-container fluid>
    <v-card>
     <v-card-actions>
        <h2 class="ml-2 mt-1 head-title ms-4 grey--text">Requisitions</h2> 
        <v-spacer></v-spacer>
      </v-card-actions>
      <v-divider></v-divider>

      <v-tabs v-model="selectedTab">
        <v-tab v-for="tab in tabs" :key="tab.name" v-model="selectedTab" :to="tab.route">
          <span>{{ tab.name }}</span>
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

@Component({
  components: {  }
})
export default class RequisitionView extends VueBase { 
  protected selectedTab: any = '';
  protected tabs: Array<{name: string, route: string}> = [
      {
        name: 'Availability',
        route: '/requisitions/availability'
      },
      {
        name: 'Reservations',
        route: '/requisitions/reservations'
      }
    ];
}
</script>