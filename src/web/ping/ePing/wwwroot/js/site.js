


Vue.directive('init', {
    bind: function (el, binding, vnode) {
        vnode.context[binding.arg] = binding.value;
        console.dir(vnode.context);
    }
});
Vue.http.headers.common['Access-Control-Allow-Origin'] = '*';
var mv=new Vue({
    el: '#app',
    data: () => ({
        drawer: null,
        //apiendpoint: "",
        api: {},
        club: {},
        joueurs: [],
        currentJoueur: {},
        items: [],
        filter: "",
        orderChoices: { 'byName': { 'fields': ['nom', 'prenom'], 'orders': ['asc', 'asc'] }, 'byPoints': { 'fields': ['point', 'nom', 'prenom'], 'orders': ['desc', 'asc', 'asc'] }},
        order: 'byName',
        filtered: [],
        oldclasse:6,
        lorem: `Lorem ipsum dolor sit amet, mel at clita quando. Te sit oratio vituperatoribus, nam ad ipsum posidonium mediocritatem, explicari dissentiunt cu mea. Repudiare disputationi vim in, mollis iriure nec cu, alienum argumentum ius ad. Pri eu justo aeque torquatos.`
       
    }),
    props: {
        source: String
    },
    methods:{
        getSettings() {
            var self = this;
            Vue.http.get('/api/dashboard/settings')
                .then(
                    result => {
                        this.api = result.data; console.dir(result);
                        uri = this.api.ApiSettings.EndPoint + this.api.ApiSettings.Club.replace('{numero}', this.api.User.numeroClub);
                        console.dir(uri);
                        Vue.http.get(uri)
                            .then(r => {
                                console.dir(r);
                                this.club = r.data;

                                uri2 = this.api.ApiSettings.EndPoint + this.api.ApiSettings.JoueursDuClub.replace('{numero}', this.api.User.numeroClub);
                                console.log(uri2);
                                Vue.http.get(uri2).then(
                                    r2 => {
                                        console.dir(r2);
                                        this.joueurs = this.filtered = r2.data;
                                    },
                                    error2 => {
                                        console.error(error2);
                                    })
                            },
                                error => {
                                    console.error(error);
                                });
                        
                },
                error => { console.error(error); });
        },
        showDivider(joueur,index) {
            if (index > 0) {
                //console.dir(this.filtered[index - 1].nom != joueur.nom);
                //joueur.nom != this.joueursFilter[index - 1].nom
                if (this.order == 'byPoints') return joueur.classe != this.filtered[index - 1].classe;
                if (this.order == 'byName') return this.filtered[index - 1].nom != joueur.nom;
            }
            return false;
        }
    },
    computed: {
        joueursFilter() {
            console.dir(this.filter);
            if (this.filter == "") {
                console.log("restore filter");
                this.filtered = this.joueurs;
            }else
                if (_.isFinite(parseInt(this.filter))) {
                    console.log("classe filter");
                this.filtered = this.joueurs.filter(joueur => {  return joueur.classe == parseInt(this.filter); });
            } else {
                console.log("name filter");
                this.filtered = this.joueurs.filter(joueur => { return joueur.nom.toLowerCase().match(this.filter.toLowerCase()); });
                console.dir(this.filtered);
                }
            this.filtered = _.orderBy(this.filtered, this.orderChoices[this.order].fields, this.orderChoices[this.order].orders);
            return this.filtered;
        }
        
    },
    mounted:function() {
        console.log("mounted");
       this.getSettings();
        
    }
})

