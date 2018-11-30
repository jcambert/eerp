import Vue from 'vue';
import Vuex, { StoreOptions, MutationTree, GetterTree, ActionTree } from 'vuex';
//import { RootState } from './types';
import standardMenu from './StandardMenu';
import fakeCotation from './FakeCotations';
Vue.use(Vuex);
 interface RootState {
    drawer: boolean;
     menuItems: Array<MenuItem>;
     cotations: Array<Cotation>;
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
    cotations: Array<Cotation>()
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
    }
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
    }
}
const store: StoreOptions<RootState> = {
    state: state,
    mutations: mutations,
    getters: getters,
    actions:actions
    
}

export default new Vuex.Store<RootState>(store);