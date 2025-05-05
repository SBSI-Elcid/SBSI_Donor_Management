<template>
	<div style="padding: 4px;" v-if="barcode">
		<p v-if="topLabel !== ''" style="padding-left: 8px; margin-bottom: -3px;" class="font-weight-bold text-uppercase caption">{{ topLabel }}</p>
	  <span v-html="barcodeElement(barcode)"></span>
		<p v-if="bottomLabel !== ''" style="padding-left: 8px;" class="font-weight-bold pt-0 mt-n3 caption caption">{{ bottomLabel }}</p>
	</div>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { Component, Prop, Watch } from 'vue-property-decorator';
import JsBarcode from 'jsbarcode'

@Component
export default class BarcodeFormat extends VueBase {
	@Prop({ required: true }) public barcode!: string;
	@Prop({ required: false, default: '' }) public topLabel!: string;
	@Prop({ required: false, default: '' }) public bottomLabel!: string;
  
  protected mounted(): void {
    JsBarcode(`#${this.barcode}`, this.barcode, {
      width: 2,
      height: 70,
      textAlign: "left"
    });
  }

  protected barcodeElement(id: string): string {
    return `<img class="mx-0" id="${id}" />`;
  }
}

</script>