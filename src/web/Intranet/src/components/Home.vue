<template>
    <v-content>
        <v-container fluid fill-height>
            <router-view></router-view>
            
        </v-container>
    </v-content>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    
    @Component
    export default class Home extends Vue {
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
