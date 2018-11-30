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
    export default class Offre extends Vue {
        @Prop() private msg!: string;
        mounted() {
            this.msg = 'Offre de Prix';
        }
    }
</script>