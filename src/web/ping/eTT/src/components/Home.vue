<template>
    <v-content>
        <v-container fluid fill-height>
            <v-layout justify-center align-center>
                <v-tooltip right>
                    <v-btn icon
                           large
                           :href="source"
                           target="_blank"
                           slot="activator">
                        <v-icon large>code</v-icon>
                    </v-btn>
                    <span>Source</span>
                </v-tooltip>
            </v-layout>
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
