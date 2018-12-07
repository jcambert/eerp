import Vue from 'vue';
import Vuex, { StoreOptions, MutationTree, GetterTree, ActionTree } from 'vuex';
Vue.use(Vuex);
/*
import standardMenu from './StandardMenu';
import fakeCotation from './FakeCotations';
import AddCotationDialog from '@/components/AddCotationDialog.vue';
import CotationForm from '@/components/Cotation.vue' ;



 interface RootState {
    drawer: boolean;
     menuItems: Array<MenuItem>;
     cotations: Array<Cotation>;
     addCotationDialog: boolean;
     newCotation: Cotation;
}

interface MenuItem {
    icon: string;
    text: string;
    route_name: string;
    children: MenuItem[];
    [key: number]: string;
}

interface Cotation {
    dp: number;
    reference: string;
    designation: string;
    indice: string;
    version: number
}


let state : RootState= {
    drawer: true,
    menuItems: Array<MenuItem>(),
    cotations: Array<Cotation>(),
    addCotationDialog: false,
    newCotation: {}
}
const getters: GetterTree<RootState,RootState> = {
    drawer(state): boolean {
        return state.drawer;
    },
    menuItems(state): Array<MenuItem> {
        return state.menuItems;
    },
    cotations(state):Array<Cotation> {
        return state.cotations;
    },
    addCotationDialog(state) {
        return state.addCotationDialog;
    },
    newCotation(state) {
        return state.newCotation;
    }
}

const mutations: MutationTree<RootState> = {
    drawerChange(state, value: boolean) {
        state.drawer = value;
    },
    setMenuItems(state, menuItem: Array<MenuItem>) {
        state.menuItems = menuItem;
    },
    addMenuItem(state, menuItem:MenuItem,parent?:MenuItem) {
        if (parent == undefined)
            state.menuItems.push(menuItem);
        else
            parent.children.push(menuItem);
    },
    SetCotations(state, cotations: Array<Cotation>) {
        state.cotations = cotations;
    },
    openCloseAddCotationDialog(state, value: boolean) {
        state.addCotationDialog = value;
        if (value)
            state.newCotation = new Cotation();
    },
    addCotation(state, cotation: Cotation) {
        state.cotations.push(cotation);
    },
    
}

const actions: ActionTree<RootState, RootState> = {
    drawerChange({ commit },value): any {
        commit('drawerChange', value);
    },
    addMenuItem({ commit }, menuItem: MenuItem, parent?: MenuItem) {
        commit('addMenuItem', { menuItem: menuItem, parent: parent });
    },
    fetchMenu({ commit }) {
        commit('setMenuItems', standardMenu);
    },
    fetchCotation({ commit }) {
        commit('SetCotations', fakeCotation);
    },
    openAddCotationDialog({ commit }) {
        commit('openCloseAddCotationDialog', true);
    },
    closeAddCotationDialog({ commit }) {
        commit('openCloseAddCotationDialog', false);
    },
    addNewCotation({ commit },cotation:Cotation) {
        commit('addNewCotation', cotation);
    }

}

const store: StoreOptions<RootState> = {
    state: state,
    mutations: mutations,
    getters: getters,
    actions:actions
    
}

export default new Vuex.Store<RootState>(store);*/

import CotationDialog,{ICotationDialogState } from './modules/CotationDialogModule';
/*export interface IAppState {
   cotationDialog: ICotationDialogState;
}
const store = new Vuex.Store<IAppState>({
    modules: {
        CotationDialogModule
    }
});*/

const store = new Vuex.Store({
    modules: {
        dialog:CotationDialog
    }
})
console.dir(store);
export default store;
