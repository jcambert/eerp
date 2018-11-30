<template>
    <v-card color="cyan darken-2" class="white--text"  >

                <v-layout  height="200px">
                    <v-flex xs12 align-end flexbox>
                        <v-card-title primary-title>
                            <div>
                                <div class="headline">
                                    <v-badge color="green">
                                        <span slot="badge" dark small>{{cotation.version}}</span>
                                        <span>{{cotation.dp}}</span>
                                    </v-badge>
                                </div>
                                <div>Référence  :{{cotation.reference}}</div>
                                <div>Désignation:{{cotation.designation }}</div>
                                <div>Indice     :{{cotation.indice }}</div>

                            </div>
                        </v-card-title>
                    </v-flex>
                </v-layout>


        <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn icon>
                <v-icon>edit</v-icon>
            </v-btn>
            <v-btn icon>
                <v-icon>delete</v-icon>
            </v-btn>
        </v-card-actions>
    </v-card>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
import Cotation from '@/components/Cotation.vue';
    @Component
    export default class CotationCardView extends Vue {
        @Prop() cotation: any;
    }
</script>

<style scoped>
</style>