<template>
  <div>
     <template v-for="(item, index) in barcodes">
        <div style="padding: 4px;" :key="index">
          <p v-if="item.TopLabel !== ''" style="padding-left: 8px; margin-bottom: -4px;" class="font-weight-bold text-uppercase caption">{{ item.TopLabel }}</p>
          <span v-html="barcodeElement(item.BarcodeText)"></span>
          <p v-if="item.BottomLabel !== ''" style="padding-left: 8px;" class="font-weight-bold pt-0 mt-n3 caption caption">{{ item.BottomLabel }}</p>
        </div>
      </template>
  </div>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Prop } from 'vue-property-decorator';
import JsBarcode from 'jsbarcode'
import { BarcodeReportDto } from '@/models/Reports/BarcodeReportDto';

@Component
export default class TestOrderBarcodeReport extends VueBase {
	@Prop({ required: true }) public barcodes!: Array<BarcodeReportDto>;
 	
   protected mounted(): void {
    for(var i = 0; i < this.barcodes.length; i++) {
      let id = `#${this.barcodes[i].BarcodeText}`;
       JsBarcode(id, this.barcodes[i].BarcodeText, {
        width: 2,
        height: 70,
        textAlign: "left"
      });
    }
  }

  protected barcodeElement(id: string): string {
    return `<img class="mx-0" id="${id}" />`;
  }

  protected beforeDestroy(): void {
    this.$emit('resetDataSource');
  }
 
}

</script>