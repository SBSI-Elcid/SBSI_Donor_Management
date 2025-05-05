<template>
  <v-container fluid>
    <v-card max-width="600" max-height="600" class="mx-auto mt-8">
     
         <h2 class="pa-7 ms-4 text-center forbidden">403</h2> 

        <p class="pa-6 text-center forbidden-text">NOT THIS TIME, ACCESS NOT GRANTED</p>
    </v-card>
 
  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component } from 'vue-property-decorator';

@Component
export default class ForbiddenView extends VueBase {

}

</script>

<style scoped>
  .forbidden {
    font-family: 'Courier New', Courier, monospace;
    font-size: 10ch;
    color:crimson;
  }
  
  .forbidden-text {
    font-family: 'Courier New', Courier, monospace;
    font-size: 3ch;
    color:chocolate;
    font-weight: 500;
  }

</style>