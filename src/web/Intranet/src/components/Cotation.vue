<template>
    <v-layout justify-center>
        <AddCotationDialog></AddCotationDialog>
        <v-flex xs12 sm12>
            <v-toolbar color="green darken-3" dark>
                <v-toolbar-side-icon></v-toolbar-side-icon>
                <v-toolbar-title>Cotations</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-btn fab dark small color="cyan" v-on:click="openAddCotationDialog()">
                    <v-icon >add</v-icon>
                </v-btn>
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
    import AddCotationDialog from './AddCotationDialog.vue';
    @Component({
        components: { CotationCardView, AddCotationDialog },
        computed: mapGetters(['cotations','addCotationDialog']),
        methods: mapActions(['fetchCotation', 'openAddCotationDialog'])
    })
    export default class CotationForm extends Vue {

        constructor() {
            super();

            
        }
        @Action('fetchCotation', {}) fetchCotation: any;
        mounted() {
            this.fetchCotation();
        };

        @Action('openAddCotationDialog', {}) openAddCotationDialog: any;

    }
</script>