<template>
  <v-container fluid>
    <v-card>
     <v-card-actions>
        <h2 class="ml-2 mt-1 head-title ms-4 grey--text">Donor Screening </h2> <span v-if="donorStatus" class="pl-3 mt-2" ><strong class="red--text">{{ donorStatus }}</strong></span>
        <v-spacer></v-spacer>
      </v-card-actions>
      <v-divider></v-divider>
      <DonorScreeningProcess />
    </v-card>
  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import DonorScreeningProcess from '@/components/DonorScreening/DonorScreeningProcess.vue';
import LookupModule from '@/store/LookupModule';
import { Route } from 'vue-router';
import DonorModule from '@/store/DonorModule';
import Common from '@/common/Common';

@Component({
   components: { DonorScreeningProcess }
})
export default class DonorScreeningView extends VueBase {
  protected lookupModule: LookupModule = getModule(LookupModule, this.$store);
  protected donorModule: DonorModule = getModule(DonorModule, this.$store);
  protected apiRequestActive: boolean = false;

  protected get donorStatus(): string {
    return this.donorModule.getDonorStatus;
  }

  protected async created(): Promise<void> {
    await this.lookupModule.loadLookups();
  }

  protected beforeRouteLeave(to: Route, from: Route, next: Function): void {
    if (to.path !== from.path) {
      this.resetValues();
    }
    next();
  }

  protected beforeRouteUpdate(to: Route, from: Route, next: Function): void {
    if (Common.isNull(to.name)) {
      this.resetValues();
    }
    next();
  }

  protected resetValues(): void {
    this.donorModule.resetDonor();
  }
}
</script>