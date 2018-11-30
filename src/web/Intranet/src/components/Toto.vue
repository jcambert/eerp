<template>
    <v-layout justify-center align-center>
        <v-tooltip right>
            <v-btn icon
                   large
                   :href="msg"
                   target="_blank"
                   slot="activator">
                <v-icon large>code</v-icon>
            </v-btn>
            <span>Source</span>
        </v-tooltip>
        {{ msg }}

    </v-layout>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';

    @Component
    export default class Toto extends Vue {
        @Prop() private msg!: string;
        mounted() {
            this.msg = 'HELLO WORLD';
        }
    }
</script>

<style scoped>
</style>