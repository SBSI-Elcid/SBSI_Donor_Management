import Vue from 'vue';
import Vuex from 'vuex';
import AuthModule from './AuthModule';
import DialogModule from './DialogModule';
import DonorModule from './DonorModule';
import LookupModule from './LookupModule';

Vue.use(Vuex)

const store = new Vuex.Store({
  //strict: process.env.NODE_ENV !== 'production',
  modules: {  
    AuthModule,
    DialogModule,
    DonorModule,
    LookupModule
  },
  state: {
    environment: process.env.NODE_ENV
  }
});

export default store;
