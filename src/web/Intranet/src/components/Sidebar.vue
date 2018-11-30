<template>
    <v-navigation-drawer fixed
                         :clipped="$vuetify.breakpoint.mdAndUp"
                         app
                         :value="drawer">
        <v-list dense>
            <template v-for="item in menuItems">
                <v-layout row
                          v-if="item.heading"
                          align-center
                          :key="item.heading">
                    <v-flex xs6>
                        <v-subheader v-if="item.heading">
                            {{ item.heading }}
                        </v-subheader>
                    </v-flex>
                    <v-flex xs6 class="text-xs-center">
                        <a href="#!" class="body-2 black--text">EDIT</a>
                    </v-flex>
                </v-layout>
                <v-list-group v-else-if="item.children"
                              v-model="item.model"
                              :key="item.text"
                              :prepend-icon="item.model ? item.icon : item['icon-alt']"
                              append-icon="">
                    <v-list-tile slot="activator">
                        <v-list-tile-content>
                            <v-list-tile-title>
                                {{ item.text }}
                            </v-list-tile-title>
                        </v-list-tile-content>
                    </v-list-tile>
                    <v-list-tile v-for="(child, i) in item.children"
                                 :key="i"
                                 @click="routerLink(child.route_name)">
                        <v-list-tile-action v-if="child.icon">
                            <v-icon>{{ child.icon }}</v-icon>
                        </v-list-tile-action>
                        <v-list-tile-content>
                            <v-list-tile-title>
                                {{ child.text }}
                            </v-list-tile-title>
                        </v-list-tile-content>
                    </v-list-tile>
                </v-list-group>
                <v-list-tile v-else @click="routerLink(item.route_name)" :key="item.text">
                    <v-list-tile-action>
                        <v-icon>{{ item.icon }}</v-icon>
                    </v-list-tile-action>
                    <v-list-tile-content>
                        <v-list-tile-title>
                            {{ item.text }}
                        </v-list-tile-title>
                    </v-list-tile-content>
                </v-list-tile>
            </template>
        </v-list>
    </v-navigation-drawer>
    
    
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { Action } from 'vuex-class';
    import Vuex, { mapGetters, mapActions } from 'vuex';
    @Component({

        computed: mapGetters(['drawer','menuItems']),
        methods: mapActions(['fetchMenu'])
        
    })
    export default class Sidebar extends Vue {
        @Action('fetchMenu', {  }) fetchMenu: any;
        mounted(){
            this.fetchMenu();
        };
    }
</script>

<style scoped>
</style>