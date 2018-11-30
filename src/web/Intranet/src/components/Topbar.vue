<template>
    <v-toolbar color="green darken-4"
               dark
               app
               :clipped-left="$vuetify.breakpoint.mdAndUp"
               fixed>
        <v-toolbar-title style="width: 300px" class="ml-0 pl-3">
            <v-toolbar-side-icon @click.stop="drawerChange( !drawer)"></v-toolbar-side-icon>
            <span class="hidden-sm-and-down">{{getTitle()}}</span>
        </v-toolbar-title>
        <v-text-field flat
                      solo-inverted
                      prepend-icon="search"
                      label="Search"
                      class="hidden-sm-and-down"></v-text-field>
        <v-spacer></v-spacer>
        <v-btn icon>
            <v-icon>apps</v-icon>
        </v-btn>
        <v-btn icon>
            <v-icon>notifications</v-icon>
        </v-btn>
        <v-btn icon large>
            <v-avatar size="32px" tile>
                <img src="https://cdn.vuetifyjs.com/images/logos/logo.svg"
                     alt="Vuetify">
            </v-avatar>
        </v-btn>
    </v-toolbar>
</template>

<script lang="ts">
    import Vuex, { mapGetters, mapActions } from 'vuex';
    import { Component, Prop, Vue } from 'vue-property-decorator';

    @Component({

        computed: mapGetters(['drawer']),
        methods:  mapActions(['drawerChange'])
        
    })
    export default class Topbar extends Vue {
        @Prop() title!: string;
        getTitle(){ return this.title;}

        
    }
</script>

<style scoped>
</style>