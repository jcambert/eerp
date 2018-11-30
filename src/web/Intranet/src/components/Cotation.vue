<template>
    <v-layout justify-center>
        <v-flex xs12 sm12>
            <v-toolbar color="green darken-3" dark>
                <v-toolbar-side-icon></v-toolbar-side-icon>
                <v-toolbar-title>Cotations</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-text-field flat
                              solo-inverted
                              prepend-icon="search"
                              label="Rechercher"
                              class="hidden-sm-and-down"></v-text-field>
            </v-toolbar>

            <v-card>
                <v-container fluid
                             grid-list-md>
                    <v-layout row wrap>
                        <v-flex v-for="cotation in cotations" class="xs3"
                                :key="cotation.title">
                            <CotationCardView v-bind:cotation="cotation"></CotationCardView>
                            
                        </v-flex>
                    </v-layout>
                </v-container>
            </v-card>
        </v-flex>
    </v-layout>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { Action } from 'vuex-class';
    import Vuex, { mapGetters, mapActions } from 'vuex';
    import CotationCardView from './CotationCardView.vue';
    @Component({
        components: { CotationCardView },
        computed: mapGetters([ 'cotations']),
        methods: mapActions(['fetchCotation'])
    })
    export default class Cotation extends Vue {
        cards: any[];
        constructor() {
            super();
            //this.cards = [];
            //this.cards.push({ dp: '12345', reference: 'Pre-fab homes', designation: 'vxcvxcv', indice: '0',version:'0' });
            //this.cards.push({ dp: '4566', reference: 'Pre-fab homes', designation: 'sfsdff', indice: '2' ,version:'1'});
            
        }
        @Action('fetchCotation', {}) fetchCotation: any;
        mounted() {
            this.fetchCotation();
        };
    }
</script>