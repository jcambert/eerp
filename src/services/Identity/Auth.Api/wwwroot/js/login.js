Vue.use(Vuetify);
new Vue({
    el: '#app',
    data: () => ({
        drawer: null,
        //  message:"HELLO VUE"
        licence: '',
        valid:true,
        licenceRule: [(v) => !!v || 'Le Numero de license est requis',
            (v) => /^\d+$/.test(v) || 'Numero de licence non valide'
        ]
    }),

    props: {
        source: String
    },
    computed: {
        isValid() {
            return Boolean(this.licence.length > 0);
        }
    },
    methods: {
        login() {
            //alert('submitting');
            document.getElementById("loginForm").submit();
        }
    }
})