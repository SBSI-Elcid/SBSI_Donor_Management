<template>
  <v-container fluid>
    <PatientListModal :toggle="showPatientListDialog" v-if="showPatientListDialog" @onClose="showPatientListDialog =false" />
    <ReservationModal :toggle="showReservationModal" v-if="showReservationModal" @onClose="showReservationModal=false" />
    <v-card>
      <v-card-actions>
        <h2 class="ml-2 mt-1 head-title grey--text">Requisitions</h2> 
        <v-spacer></v-spacer>
      
      </v-card-actions>
      <h3 class="pl-4 subtitle-1 grey--text">Inventory availability group by Blood type</h3> 
      <v-card-text>
        <v-row>
          <v-col v-for="(group, i) in inventoryGroupList" :key="i" cols="12" md="6" class="text-center">
            <v-card class="pa-4" color="grey lighten-4" elevation="2" tile>
              <v-row no-gutters>
                <v-col cols="2" class="text-left">
                    <h1 class="display-2 mb-2">
                      <span>{{ group.BloodType }}
                        <v-icon size="30" color="red">mdi-water</v-icon>
                      </span> 
                  </h1>
                </v-col>
                <v-col cols="10" class="text-right">
                  <v-btn small color="primary" depressed title="Reserve" @click="reserve" v-if="group.InventoryItems.length > 0">
                    <v-icon left>mdi-flask-plus</v-icon> Reserve
                  </v-btn> 
                </v-col>
              </v-row>
              <v-row no-gutters>
                <v-col cols="12" class="text-left">
                  <div class="d-inline-flex">
                    <p class="caption">Total: <span class="font-weight-bold">{{ group.TotalNumberOfUnits }}</span></p>
                    <p class="pl-3 caption">Expired: <span class="font-weight-bold">{{ group.TotalExpiredCount }}</span></p>
                  </div>
                  <v-divider></v-divider>
                    <v-simple-table dense light>
                      <template v-slot:default>
                        <tbody>
                          <tr v-for="(item, index) in group.InventoryItems" :key="index">
                            <td class="text-left caption">{{ item.Component }}</td>
                            <td class="text-left caption font-weight-bold">{{ item.NoOfUnit }}</td>
                            <td class="text-left caption">{{ formatDate(item.CollectionDate) }}</td>
                            <td class="text-left caption">{{ formatDate(item.ExpiryDate) }}</td>
                            <!-- <td class="text-left"> 
                              <v-btn small color="primary" depressed title="Reserve" @click="reserve">
                                <v-icon left>mdi-flask-plus</v-icon> Reserve
                              </v-btn> 
                            </td> -->
                          </tr>
                        </tbody>
                      </template>
                    </v-simple-table>
                </v-col>
              </v-row>

            </v-card>
          </v-col>
        </v-row>
      </v-card-text>
    </v-card>
 
  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Watch } from 'vue-property-decorator';
import { InventoryGroupDto } from '@/models/Inventory/InventoryGroupDto';
import InventoryService from '@/services/InventoryService';
import PatientListModal from '@/components/Patient/PatientListModal.vue';
import ReservationModal from '@/components/Requisition/ReservationModal.vue';

@Component({ components: { PatientListModal, ReservationModal } })
export default class InventoryView extends VueBase {
  
  protected inventoryService: InventoryService = new InventoryService();
  protected dataLoaded: boolean = true;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected inventoryGroupList: Array<InventoryGroupDto> = []
  protected showPatientListDialog: boolean = false;
  protected showReservationModal: boolean = false;
   //blood-bag

  protected async loadInventorList(): Promise<void> {
    this.inventoryGroupList = await this.inventoryService.getInventoryStatus();
  }

  protected addPatient(): void {
    this.showPatientListDialog = true;
  }

  protected reserve(): void {
    this.showReservationModal = true;
  }

  protected async mounted() : Promise<void> {
    await this.loadInventorList();
  }

}

</script>