<template>
  <v-footer
      class="justify-center"
      color="#292929"
      height="40"
			fixed
    >
      <div class="title font-weight-light grey--text text--lighten-1 text-center">
        &copy; {{ (new Date()).getFullYear() }} — SBSI
      </div>
    </v-footer>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component({
  components: { }
})
export default class CommonFooter extends Vue { 
  
}
</script>
