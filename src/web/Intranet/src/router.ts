import Vue from 'vue';
import Router from 'vue-router';
import Home from './components/Home.vue';
import Sidebar from './components/Sidebar.vue';
import Topbar from './components/Topbar.vue';
import Offre from './components/Offre.vue';
import Cotation from './components/Cotation.vue';
Vue.use(Router);

export default new Router({
    mode: 'history',
    routes: [
        {
            path: '/',
            name:'home',
            components: {
                default: Home,
                sidebar: Sidebar,
                topbar:Topbar
            },
            children: [
                {
                    path: '',
                    name: 'commercial.offre',
                    component:Offre 
                },
                {
                    path: 'cotation',
                    name: 'commercial.cotation',
                    component: Cotation
                }
            ]
        },
        {
            path: '*',
            redirect: '/'
        }
    ]
});