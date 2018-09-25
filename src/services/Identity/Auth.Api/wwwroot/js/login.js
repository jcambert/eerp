Vue.use(Vuetify);
new Vue({
    el: '#app',
    data: () => ({
        drawer: null,
        message:"HELLO VUE"
    }),

    props: {
        source: String
    }
})