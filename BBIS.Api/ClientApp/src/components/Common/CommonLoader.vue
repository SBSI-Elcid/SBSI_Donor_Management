<template>
  <v-progress-circular
      :size="70"
      :width="7"
      color="#B92F2F"
      indeterminate
    ></v-progress-circular>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';

@Component
export default class CommonLoader extends Vue {}

</script>