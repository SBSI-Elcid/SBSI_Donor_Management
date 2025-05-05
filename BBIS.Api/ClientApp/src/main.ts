
// If you don't need the styles, do not connect
import 'sweetalert2/dist/sweetalert2.min.css';
import 'vue-loading-overlay/dist/vue-loading.css';

import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import './registerServiceWorker'
import VueSweetalert2 from 'vue-sweetalert2';
import Loading from 'vue-loading-overlay';

/*
This is a temporary fix for the issue on the main tab closing after printing/cancelling. 
The fix has been created and a PR was added but it is not yet merged (Check the package here: https://github.com/mycurelabs/vue-html-to-paper)
Once the fixed has been merged, remove the temporary fix by removing the vuehtmltopaper folder and its contents
then install the package by doing "npm i vue-html-to-paper" and updating the line below to "import VueHtmlToPaper from 'vue-html-to-paper'".
*/
import VueHtmlToPaper from './vuehtmltopaper';

const options = {
  name: '_blank',
  specs: [
    'fullscreen=yes',
    'titlebar=yes',
    'scrollbars=yes'
  ],
  styles: [
    'https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css',
    'https://unpkg.com/kidlat-css/css/kidlat.css',
  ],
  timeout: 1000, // default timeout before the print window appears
  autoClose: true, // if false, the window will not close after printing
  windowTitle: window.document.title, // override the window title
}

Vue.use(require('vue-moment'));
Vue.use(VueHtmlToPaper, options);
Vue.use(VueSweetalert2);
Vue.use(Loading, { color: '#1976d2'});
Vue.config.productionTip = false

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
