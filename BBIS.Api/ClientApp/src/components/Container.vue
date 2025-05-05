<template>
  <div>
    <AppBarHeader @toggleDrawer="toggleNavBar" />
    <SidebarNav ref="navBar" />

    <v-main>
      <div class="mx-4">
        <router-view/>
      </div>
    </v-main>

  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import SidebarNav from './SidebarNav.vue';
import AppBarHeader from './AppBarHeader.vue';

@Component({
   components: { SidebarNav, AppBarHeader }
})
export default class Container extends Vue {
  protected toggleNavBar() {
    let navBarRefs = this.$refs.navBar as SidebarNav;

    // This would make the nav bar responsive by being able to open a mini variant of the navigation bar when in smaller screens
    if (this.$vuetify.breakpoint.mdAndDown) {
      navBarRefs.drawer = !navBarRefs.drawer;
    }
  }
}
</script>
