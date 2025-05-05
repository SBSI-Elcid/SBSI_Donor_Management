<template>
  <v-menu ref="menuDatePicker"
          v-model="menuDatePicker"
          :close-on-content-click="false"
          :close-on-click="true"
          :return-value.sync="dateModel"
          :disabled="disabled"
          offset-y>

    <template v-slot:activator="{ on }">
        <v-text-field v-on="on"
            :value="dateFormatted"
            :placeholder="placeHolder"
            :rules="rules"
            :disabled="disabled"
            @click:append="menuDatePicker = true"
            @click:clear="onTextClear"
            @keydown.delete="onTextClear"
            append-icon="mdi-calendar-month"
            autocomplete="off"
            outlined dense
            readonly />
    </template>

    <v-date-picker v-model="dateModel"
        :picker-date.sync="pickerDate"
        @input="onDateSelected(dateModel)"
        no-title scrollable>
      <v-spacer></v-spacer>
      <v-btn text color="primary" @click="onTextClear()">Clear</v-btn>
    </v-date-picker>

  </v-menu>
</template>

<script lang="ts">
import { Component, Prop, Vue, Watch, Emit } from 'vue-property-decorator';
import moment from 'moment';

@Component
export default class DatePicker extends Vue {

  @Prop({ required: false })
  public placeHolder!: string;

  @Prop({ default: '' })
  public defaultDate!: string;

  @Prop()
  public rules!: Function | undefined;

  @Prop()
  public value!: Date | null;

  @Prop({ default: false })
  public disabled!: boolean;

  @Prop({ default: 'DD-MMM-YYYY' })
  public dateFormat!: string;

  protected dateModel: string | null = null;
  protected pickerDate: string | null = null;
  protected menuDatePicker: boolean = false;

  protected created(): void {
    if(this.value) {
      this.dateModel = moment(this.value).format('YYYY-MM-DD');
    }
  }
 
  @Watch('value')
  protected updateDateModel(dateValue: Date | null): void {
    this.change(dateValue);
    if (!dateValue) {
      this.dateModel = '';
      return;
    }
    else {
      this.dateModel = moment(dateValue).format('YYYY-MM-DD');
    }
  }

  protected onTextClear(): void {
    this.dateModel = null;
    this.onDateSelected(null);
    this.$emit('cleared');
  }

  protected onDateSelected(date: string | null): void {
    let dialog: any = this.$refs.menuDatePicker;
    if (dialog != null) {
      dialog.save(date);
    }
    this.menuDatePicker = false;
    let selectedDate = this.input(date);
    this.change(selectedDate);
  }

  protected get dateFormatted(): string | null{    
    return this.dateModel ? moment(this.dateModel,'YYYY-MM-DD').format(this.dateFormat) : null;
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