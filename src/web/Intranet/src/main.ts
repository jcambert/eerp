import Vue from 'vue';
import App from './App.vue';
import Vuetify from 'vuetify'
import Vuex from 'vuex'; 
import { sync } from 'vuex-router-sync';

import router from '@/router';
import store from '@/store';

import 'vuetify/dist/vuetify.min.css' 
import 'material-design-icons-iconfont/dist/material-design-icons.css'

import CotationDialogModule from '@/store/modules/CotationDialogModule';
console.dir(CotationDialogModule); 

Vue.config.productionTip = true;

Vue.use(Vuetify);

Vue.mixin({
    methods: {
        routerLink(name) {
            this.$router.push({ name: name });
        },
        goBack() {
            this.$router.back();
        }

    }
})

sync(store, router); 
new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app');

//global.store = store;
