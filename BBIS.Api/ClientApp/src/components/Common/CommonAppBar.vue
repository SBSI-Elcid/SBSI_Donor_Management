<template>
  <v-app-bar app color="#B92F2F" height="70" dark>
      <v-avatar class="mr-3" color="grey lighten-5" size="50">
        <v-img contain max-height="65%" src="https://cdn.bitrix24.com/b14890059/landing/656/656f65e51eb5be34a62faef7ef28d6e7/SBSI-Logo-1_1x.png" ></v-img>
      </v-avatar>
    </v-app-bar>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component({
  components: { }
})
export default class CommonAppBar extends Vue { 
  
}
</script>
