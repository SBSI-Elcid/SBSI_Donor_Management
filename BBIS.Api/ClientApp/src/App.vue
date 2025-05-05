<template>
  <v-app style="background-color: #F4F4F4;">    
    <session-expired></session-expired>
    <router-view :key="$route.fullPath" />
  </v-app>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import SessionExpired from '@/components/SessionExpired.vue'
import './component-hooks';
import VueBase from './components/VueBase';

@Component({ components: { SessionExpired } })
export default class App extends VueBase {  
 
  protected async mounted(): Promise<void> {
    await this.getOfflineStatus();
  }
}
</script>

<style lang="scss">
html,
body {
  font-family: "Roboto", "RobotoFallback", "Helvetica", "Arial", sans-serif;
}

.app-bg {
background-color: #FFFFFF;
background-image: linear-gradient(204deg, #FFFFFF 0%, #f1f2f5 66%, #FF0000 100%);
}

 *{ text-transform: none !important; }

.headline {
  font-size: 1rem !important;
  font-weight: 400 !important;
  letter-spacing: normal !important;
}

.no-uppercase {
  text-transform: none !important;
}

.v-input {
  font-size: 13px !important;
}

.v-label {
  font-size: 16px !important;
  color: grey;
}

.check-label {
  font-size: 13px !important;
  color: grey;
}

.v-input--checkbox .v-input__control label {
  font-size: 14px !important;
}

.custom-loader {
  animation: loader 1s infinite;
  display: flex;
}

tbody tr:nth-of-type(odd) {
  background-color: rgba(0, 0, 0, .05);
}

@-moz-keyframes loader {
  from {
    transform: rotate(0);
  }
  to {
    transform: rotate(360deg);
  }
}
@-webkit-keyframes loader {
  from {
    transform: rotate(0);
  }
  to {
    transform: rotate(360deg);
  }
}
@-o-keyframes loader {
  from {
    transform: rotate(0);
  }
  to {
    transform: rotate(360deg);
  }
}
@keyframes loader {
  from {
    transform: rotate(0);
  }
  to {
    transform: rotate(360deg);
  }
}

.font-20p {
  font-size: 20px;
}

.hide-me {
  display: none;
}

 .xmatch-col {
    border: 0.1rem solid #dddddd; text-align: left; padding: 3px;
  }

  .colx {
    border: 0.1rem solid #fff; text-align: left; padding: 3px;
  }

  .xmatch-table {
    border-collapse: collapse; padding: 10px 100px; width: 100%; margin-top: 10px
  }

</style>
