<template>
	<div>
		<CommonAppBar />

		<v-main>
			  <section
        id="features"
        class="grey lighten-3"
      >
        <div class="py-12"></div>

        <v-container class="text-center">
          <h2 class="display-2 font-weight-bold mb-3">SELF SERVICE PORTAL</h2>

          <v-responsive class="mx-auto mb-12" width="56">
            <v-divider class="mb-1"></v-divider>
            <v-divider></v-divider>
          </v-responsive>

          <v-row>
            <v-col v-for="({ icon, title, text, buttonColor, urlpath }, i) in features" :key="i" cols="12"  class="text-center">
              <v-card class="py-12 px-6 mx-auto" color="grey lighten-5" flat max-width="700">
                <v-theme-provider dark>
                  <div>
                    <v-avatar :color="buttonColor" size="88">
                      <v-icon large v-text="icon"></v-icon>
                    </v-avatar>
                  </div>
                </v-theme-provider>

                <v-card-title class="justify-center font-weight-black text-decoration-none">
									<v-btn large color="default" @click="buttonClick(urlpath)">
										<v-icon left>{{ icon }}</v-icon>
										{{ title }}
									</v-btn>
								</v-card-title>

                <v-card-text>
									<p class="font-weight-large subtitle-1 display-2">{{ text }}</p>
								</v-card-text>
              </v-card>
            </v-col>
          </v-row>
        </v-container>

        <div class="py-12"></div>
      </section>
		</v-main>

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
	</div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import CommonAppBar from '@/components/Common/CommonAppBar.vue';

@Component({
  components: { CommonAppBar }
})
export default class LandingPageView extends Vue { 
  private features: any = [
		{
			icon: 'mdi-blood-bag',
			title: 'DONOR REGISTRATION',
			text: 'You can become a superhero too. Just donate a bag of blood.',
			buttonColor: 'error',
      urlpath: 'register'
		},
		// {
		// 	icon: 'mdi-water-check',
		// 	title: 'BLOOD REQUISITION',
		// 	text: 'Request a blood',
		// 	buttonColor: 'primary',
    //   urlpath: 'requisition'
		// }		
	]

  private buttonClick(urlpath: string): void {
    this.$router.push({ name: urlpath })    
  }
}
</script>
