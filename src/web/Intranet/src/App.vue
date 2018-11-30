<template>

        <v-app id="inspire">
            <router-view></router-view>
            <router-view name="topbar" title="Intranet"></router-view>
            <router-view name="sidebar" ></router-view>
        </v-app>
        <!--<Home msg="Hello world!" />-->

</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    /*import Home from './components/Home.vue';*/

    @Component({
        components: {
            
        }
    })
    export default class App extends Vue {
        
    }
</script>

<style>
</style>
