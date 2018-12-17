<template>
    <div id="app">
        <v-app id="inspire">
            <router-view name="topbar"></router-view>
            <router-view name="sidebar"></router-view>
            <router-view></router-view>
            <Topbar></Topbar>
            <Sidebar></Sidebar>
            <Content></Content>

           
        </v-app>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import Home from './components/Home.vue';

    @Component({
        components: {
            Home
        }
    })
    export default class App extends Vue { }
</script>

<style>
</style>
