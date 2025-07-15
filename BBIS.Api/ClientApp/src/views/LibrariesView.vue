<template>
    <v-container fluid>
        <v-card>
            <v-card-actions>
                <h2 class="ml-2 mt-1 head-title ms-4 grey--text">Libraries</h2>
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
<script lang="ts">import VueBase from '@/components/VueBase';
import { Component } from 'vue-property-decorator';

@Component
export default class LibrariesView extends VueBase {
  public selectedTab: string = '';
  public tabs: Array<{name: string, icon: string, route: string}> = [
    {
      name: 'Roles',
      icon: 'mdi-account-card',
      route: '/libraries/roles',
    },
    {
      name: 'Questionnare',
      icon: 'mdi-cloud-question',
      route: '',
    }
  ];

}</script>

<style lang="scss" scoped>
    .tabs-name {
        text-align: left;
        width: 200px;
    }

    .header-wrapper {
        padding: 12px 12px 12px 17px;
    }
</style>