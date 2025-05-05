<template>
  <v-container fluid>
    <ReservationModal :toggle="showReservationModal" :bloodType="selectedBloodType" :bloodRH="selectedBloodRh" v-if="showReservationModal" @onClose="onDialogClose" />
    <h3 v-if="dataLoaded" class="pb-2 subtitle-1 grey--text">Inventory availability group by Blood type</h3> 
    <v-row>
      <v-col v-for="(group, i) in inventoryGroupList" :key="i" cols="12" md="6" class="text-center">
        <v-card class="pa-4" color="grey lighten-4" elevation="2" tile>
          <v-row no-gutters>
            <v-col cols="6" class="text-left">
                <h1 class="display-2 mb-2">
                  <span>{{ group.BloodType }} 
                    <v-icon class="ml-n3" v-if="group.BloodRh === 'Positive'" size="25" color="black">mdi-plus</v-icon>
                    <v-icon class="ml-n3" v-else size="25" color="black">mdi-minus</v-icon>
                    <v-icon size="30" color="red">mdi-water</v-icon>
                  </span> 
              </h1>
            </v-col>
            <v-col cols="6" class="text-right">
              <v-btn small color="primary" depressed title="Reserve" @click="reserve(group.BloodType, group.BloodRh)" v-if="group.InventoryItems.length > 0">
                <v-icon left>mdi-flask-plus</v-icon> Reserve
              </v-btn> 
            </v-col>
          </v-row>
          <v-row no-gutters>
            <v-col cols="12" class="text-left">
              <div class="d-inline-flex">
                <p class="caption">Total Unit: <span class="font-weight-bold">{{ group.TotalNumberOfUnits }}</span></p>
              </div>
              <v-divider></v-divider>
                <v-simple-table dense light>
                  <template v-slot:default>
                    <tr>
                      <th class="pl-4 text-left caption font-weight-bold">Component</th>
                      <th class="pl-4 text-left caption font-weight-bold">Unit</th>
                      <th class="pl-4 text-left caption font-weight-bold">Collection Date</th>
                      <th class="pl-4 text-left caption font-weight-bold">Expiry Date</th>
                    </tr>
                    <tbody>
                      <tr v-for="(item, index) in group.InventoryItems" :key="index">
                        <td class="text-left caption">{{ item.Component }}</td>
                        <td class="text-left caption">{{ item.NoOfUnit }}</td>
                        <td class="text-left caption">{{ formatDate(item.CollectionDate) }}</td>
                        <td class="text-left caption">{{ formatDate(item.ExpiryDate) }}</td>
                      </tr>
                    </tbody>
                  </template>
                </v-simple-table>
            </v-col>
          </v-row>

        </v-card>
      </v-col>
    </v-row>

  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Watch } from 'vue-property-decorator';
import { InventoryGroupDto } from '@/models/Inventory/InventoryGroupDto';
import InventoryService from '@/services/InventoryService';
import ReservationModal from '@/components/Requisition/ReservationModal.vue';

@Component({ components: { ReservationModal } })
export default class AvailabilityListing extends VueBase {
  
  protected inventoryService: InventoryService = new InventoryService();
  protected inventoryGroupList: Array<InventoryGroupDto> = []
  protected showReservationModal: boolean = false;
  protected selectedBloodType: string = '';
  protected selectedBloodRh: string = '';
  protected dataLoaded: boolean = false;

  protected async loadInventorList(): Promise<void> {
    let loader = this.showLoader();
    try {
      this.inventoryGroupList = await this.inventoryService.getInventoryStatus();
      this.dataLoaded = true;
    }
    catch (error) {
      console.log(error);
      this.notify_error('Something went wrong loading the available items.');
    }
    finally {
      loader.hide();
    }
  }
 
  protected reserve(bloodType: string, rh: string): void {
    this.selectedBloodType = bloodType;
    this.selectedBloodRh = rh;
    this.showReservationModal = true;
  }

  protected async onDialogClose(refresh: boolean): Promise<void> {
    this.showReservationModal = false;
    if(refresh) {
      await this.loadInventorList();
    }
  }

  protected async mounted() : Promise<void> {
    await this.loadInventorList();
  }

}

</script>