<template>
  <v-card class="my-2" width="100%">
    <v-row class="mx-auto my-0">
      <v-col cols="4" class="py-2">
        <v-checkbox v-model="selected" class="my-2 font-weight-medium" color="info" :label="bloodComponent.ComponentName" @change="onSelectionChange" :hide-details="true"/>
      </v-col>
      <v-col cols="6" class="py-3 pa-0">
        <v-autocomplete v-if="selected" 
          :items="options" 
          multiple
          chips
          v-model="selectedItems" 
          label="Items" 
          placeholder="Please select" 
          :hide-details="true" 
          dense 
          outlined />
      </v-col>
      <v-col cols="2" class="text-right">
        <v-btn icon left @click="show = !show">
          <v-icon>{{ show ? 'mdi-chevron-up' : 'mdi-chevron-down' }}</v-icon>
        </v-btn>
      </v-col>
    </v-row>

    <v-row class="mx-auto my-0" v-for="(checklist, index) in bloodComponent.CheckLists" :key="index" v-show="show">
      <v-col cols="12" class="py-0 px-7">
        <v-checkbox class="my-1" v-model="selectedReservationCheckLists" :label="checklist.Description" :value="checklist.ChecklistId" :hide-details="true"/>
      </v-col>
    </v-row>

    <v-row class="mx-auto my-0 pb-2" v-show="show">
      <v-col cols="12" class="py-2 px-7">
        <v-textarea v-model="otherNotes" label="Others please specify" rows="2" no-resize :hide-details="true" dense outlined />
      </v-col>
    </v-row>
  </v-card>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { ChecklistOptionDto } from '@/models/Requisitions/ChecklistOptionDto';
import { ReservationItemDto } from '@/models/Requisitions/ReservationItemDto';
import InventoryService from '@/services/InventoryService';
import { Component, Prop, Watch } from 'vue-property-decorator';

@Component
export default class BloodComponentWidget extends VueBase {
  @Prop()
  public value!: ReservationItemDto;
  @Prop()
  public bloodComponent!: ChecklistOptionDto;
  @Prop()
  public options!: Array<{text: string, value: string}>;

  protected selected: boolean = false;
  protected inventoryService: InventoryService = new InventoryService();
  protected availableUnits: Array<{text: string, value: string}> = [];
  protected show: boolean = false;
  protected selectedItems: Array<Guid> = [];
  protected selectedReservationItems: Array<ReservationItemDto> = [];
  protected selectedReservationCheckLists: Array<Guid> = [];
  protected otherNotes: string = '';

  protected onSelectionChange(isSelected: boolean): void {
    if (!isSelected) {
      this.selectedItems = [];
      this.$emit('onDeSelected', this.selectedReservationItems);
    }
  }

  @Watch('selectedItems', { deep: true }) 
  protected onReservationItemChange(): void {
    this.selectedReservationItems = [];

    if (this.selectedItems.length > 0) {
      for (let index = 0; index < this.selectedItems.length; index++) {
        let itemId = this.selectedItems[index];
        let dto: ReservationItemDto = { 
          BloodComponentId: this.bloodComponent.BloodComponentId,
          InventoryItemId: itemId,
          Volume: 0,
          OtherNotes: this.otherNotes,
          ReservationCheckLists: this.selectedReservationCheckLists
        };

        this.selectedReservationItems.push(dto);
      }
    }

    this.$emit('onSelected', this.selectedReservationItems);
  }
 
}
</script>

<style lang="scss" scoped>

</style>