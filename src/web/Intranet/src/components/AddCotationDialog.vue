<template>
    <v-dialog :value="addCotationDialog"
              fullscreen
              hide-overlay
              transition="dialog-bottom-transition"
              scrollable>
        <v-card tile>
            <v-toolbar card dark color="primary">
                <v-btn icon dark @click="closeAddCotationDialog()">
                    <v-icon>close</v-icon>
                </v-btn>
                <v-toolbar-title>Settings</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-toolbar-items>
                    <v-btn dark flat @click="closeAddCotationDialog()">Save</v-btn>
                </v-toolbar-items>
                <v-menu bottom right offset-y>
                    <v-btn slot="activator" dark icon>
                        <v-icon>more_vert</v-icon>
                    </v-btn>
                    <v-list>
                        <!--<v-list-tile v-for="(item, i) in items" :key="i" @click="">
        <v-list-tile-title>{{ item.title }}</v-list-tile-title>
    </v-list-tile>-->
                    </v-list>
                </v-menu>
            </v-toolbar>
            <v-card-text>
                <v-btn color="primary" dark @click="dialog2 = !dialog2">Open Dialog 2</v-btn>
                <v-tooltip right>
                    <v-btn slot="activator">Tool Tip Activator</v-btn>
                    Tool Tip
                </v-tooltip>
                <v-list three-line subheader>
                    <v-subheader>User Controls</v-subheader>
                    <v-list-tile avatar>
                        <v-list-tile-content>
                            <v-list-tile-title>Content filtering</v-list-tile-title>
                            <v-list-tile-sub-title>Set the content filtering level to restrict apps that can be downloaded</v-list-tile-sub-title>
                        </v-list-tile-content>
                    </v-list-tile>
                    <v-list-tile avatar>
                        <v-list-tile-content>
                            <v-list-tile-title>Password</v-list-tile-title>
                            <v-list-tile-sub-title>Require password for purchase or use password to restrict purchase</v-list-tile-sub-title>
                        </v-list-tile-content>
                    </v-list-tile>
                </v-list>
               
            </v-card-text>

            <div style="flex: 1 1 auto;"></div>
        </v-card>
    </v-dialog>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { Action } from 'vuex-class';
    import Vuex, { mapGetters, mapActions } from 'vuex';
    @Component({
        computed: mapGetters([ 'addCotationDialog']),
        methods: mapActions([ 'closeAddCotationDialog','addNewCotation'])
    })
    export default class AddCotationDialog extends Vue {
        
        constructor() {
            super();
            
        }

        @Action('closeAddCotationDialog', {}) closeAddCotationDialog: any;

    }
</script>

<style scoped>
</style>