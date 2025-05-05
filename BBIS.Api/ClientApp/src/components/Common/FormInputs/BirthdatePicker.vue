<template>
  <div>
    <v-menu
      ref="menuDatePicker"
      v-model="menu"
      :close-on-content-click="false"
      transition="scale-transition"
      offset-y
      min-width="auto"
      :return-value.sync="date"
      :disabled="disabled"
      @input="setDates">
      <template v-slot:activator="{ on, attrs }">
        <v-text-field
          :label="labelText"
          v-model="dateFormatted"
          readonly
          dense
          outlined
          :placeholder="placeHolder"
          @click:append="menu = true"
          @click:clear="onTextClear"
          @keydown.delete="onTextClear"
          :rules="rules"
          :disabled="disabled"
          autocomplete="off"
          v-bind="attrs"
          v-on="on"
        ></v-text-field>
      </template>
      <v-date-picker
        v-model="date"
        :active-picker.sync="activePicker"
        :max="maxDateAllowed"
        min="1920-01-01"
        @change="change"
        @input="onDateSelected(date)"
      ></v-date-picker>
    </v-menu>
  </div>
</template>

<script lang="ts">
import moment from 'moment';
import { Component, Prop, Vue, Watch, Emit } from 'vue-property-decorator';

@Component
export default class BirthdatePicker extends Vue {
  @Prop()
  public value!: Date | null;

  @Prop()
  public rules!: Function | undefined;

  @Prop({ default: false })
  public disabled!: boolean;

  @Prop({ required: false })
  public placeHolder!: string;

  @Prop({ required: false })
  public labelText!: string;

  protected activePicker: any = null;
  protected date: string | null = null;
  protected menu: boolean = false;

  protected get maxDateAllowed(): string {
    return moment().format('YYYY-MM-DD');
  }
  
  protected get dateFormatted(): string | null {    
    return this.date ? moment(this.date,'YYYY-MM-DD').format('MMM DD, YYYY') : null;
  }

  protected set dateFormatted(value) {
    this.date = value;
    this.onDateSelected(value)
  }

  @Watch('menu') 
  protected onMenuChange(val: boolean): void {
    val && setTimeout(() => (this.activePicker = 'YEAR'))
  }

  @Watch('value')
  protected updateDateModel(dateValue: Date | null): void {
    this.change(dateValue);
    if (!dateValue) {
      this.date = '';
      return;
    }
    this.date = moment(dateValue || new Date()).format('YYYY-MM-DD');
  }

  protected setDates(): void {
    if (this.value) {
      this.date = moment(this.value || new Date()).format('YYYY-MM-DD');
    }
  }

  protected onTextClear(): void {
    this.date = null;
    this.onDateSelected(null);
    this.$emit('cleared');
  }

  protected onDateSelected(date: string | null): void {
    let dialog: any = this.$refs.menuDatePicker;
    if (dialog) {
      dialog.save(date);
    }
    this.menu = false;
    this.input(date);
  }

  protected save(date: any): void {
    this.date = date;
    this.input(date);
  }

  @Emit()
  protected input(date: string | null) : Date | null {    
    return date === null ? null : moment(date).toDate();
  }

  @Emit()
  protected change(date: Date | null) : Date | null {    
    return date;
  }

}
</script>