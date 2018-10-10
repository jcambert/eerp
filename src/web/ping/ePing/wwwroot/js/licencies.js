﻿
var mv=new Vue({
    el: '#app',
    data: () => (Object.assign({}, data, {
        showDetail: false,
        api: {},
        club: {},
        joueurs: [],
        parties: [],
        partiesFiltered: [],
        historiques: {},
        historiquesLoaded:false,
        currentJoueur: {},
        pointJoueur:0,
        items: [],
        filter: "",
        orderJoueurChoices: {
            'byName': { 'fields': ['nom', 'prenom'], 'orders': ['asc', 'asc'] },
            'byPoints': { 'fields': ['point', 'nom', 'prenom'], 'orders': ['desc', 'asc', 'asc'] },
            'byCategorie': { 'fields': ['categorie', 'nom', 'prenom'], 'orders': ['desc', 'asc','asc']}
        },
        orderJoueur: 'byName',
        filtered: [],
        oldclasse: 5,
        
        showJoueurInfo: false,
        showingHistorique : false,
        showingPartie : false,
        categorieAge: {
            'N': {value:'Non determiné'},
            'P': { value: 'Poussin', desc: 'jeunes ayant 8 ans au plus' },
            'B1': { value: 'Benjamin 1', desc: 'jeunes agés de 8 à 9 ans' },
            'B2': { value: 'Benjamin 2', desc: 'jeunes agés de 9 à 10 ans' },
            'M1': { value: 'Minime 1', desc: 'jeunes agés de 10 à 11 ans' },
            'M2': { value: 'Minime 1', desc: 'jeunes agés de 11 à 12 ans' },
            'C1': { value: 'Cadet 1', desc: 'jeunes agés de 12 à 13 ans' },
            'C2': { value: 'Cadet 2', desc: 'jeune agés de 13 à 14 ans' },
            'J1': { value: 'Junior 1', desc: 'jeunes agés de 14 à 15 ans' },
            'J2': { value: 'Junior 2', desc: 'jeunes agés de 15 à 16 ans' },
            'J3': { value: 'Junior 3', desc: 'jeunes agés de 16 à 17 ans' },
            'S': { value: 'Senior', desc: 'adultes agés de 18 à 39 ans' },
            'V1': { value: 'Veteran 1', desc: 'adultes agés de 40 à 49 ans' },
            'V2': { value: 'Veteran 2', desc: 'adultes agés de 50 à 59 ans' },
            'V3': { value: 'Veteran 3', desc: 'adultes agés de 60 à 69 ans' },
            'V4': { value: 'Veteran 4', desc: 'adultes agés de 70 à 79 ans' },
            'V5': { value: 'Veteran 1', desc: 'adultes agés de plus de 80 ans' }
        },
        lorem: `Lorem ipsum dolor sit amet, mel at clita quando. Te sit oratio vituperatoribus, nam ad ipsum posidonium mediocritatem, explicari dissentiunt cu mea. Repudiare disputationi vim in, mollis iriure nec cu, alienum argumentum ius ad. Pri eu justo aeque torquatos.`
       
    })),
    props: {
        source: String
    },
    methods:{
        getSettings() {
            var self = this;
            this.loader = true;
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
                                        this.loader = false;
                                    },
                                    error2 => {
                                        console.error(error2);
                                        this.loader = false;
                                    })
                            },
                                error => {
                                    console.error(error);
                                    this.loader = false;
                                });
                        
                },
                error => { console.error(error); this.loader = false;});
        },
        reloadJoueursDuClub() {
            this.loader = true;
            
            uri2 = this.api.ApiSettings.EndPoint + this.api.ApiSettings.ReloadJoueursDuClub.replace('{numero}', this.api.User.numeroClub);
            console.log(uri2);
            Vue.http.get(uri2).then(
                r2 => {
                    console.dir(r2);
                    this.joueurs = this.filtered = r2.data;
                    this.loader = false;
                },
                error2 => {
                    console.error(error2);
                    this.loader = false;
                })
        },
        showJoueurDivider(joueur,index) {
            if (index > 0) {
                if (this.orderJoueur == 'byPoints') return joueur.classement != this.filtered[index - 1].classement;
                if (this.orderJoueur == 'byName') return this.filtered[index - 1].nom != joueur.nom;
                if (this.orderJoueur == 'byCategory') return this.filtered[index - 1].categorie != joueur.categorie;
            }
            return false;
        },
        loadPartiesDujoueur(joueur) {
            
            this.parties.splice(0,this.parties.length);
            this.loader = true;
            this.historiquesLoaded = false;
            uri2 = this.api.ApiSettings.EndPoint + this.api.ApiSettings.PartiesDuJoueur.replace('{licence}', joueur.licence);
            console.log(uri2);
            Vue.http.get(uri2).then(
                r2 => {
                    //console.dir(r2);
                    _.forEach(r2.data, data => this.parties.push(data));
                    
                    //this.parties = r2.data;
                    this.loader = false;
                    this.showDetail = true;
                    this.showDetailParties();
                    //en arriere plan
                    this.loadHistoriquesDujoueur(joueur);
                },
                error2 => {
                    console.error(error2);
                    this.loader = false;
                }),

                this.currentJoueur = joueur;
        },
        loadHistoriquesDujoueur(joueur) {
            
            //this.historiques.splice(0, this.historiques.length);
            this.loader = true;
            uri2 = this.api.ApiSettings.EndPoint + this.api.ApiSettings.HistoriquesDuJoueur.replace('{licence}', joueur.licence);
            console.log(uri2);
            Vue.http.get(uri2).then(
                r2 => {
                    console.dir(r2);
                    //this.historiques = r2.data;
                    this.historiques = Object.assign({}, r2.data);
                    // _.forEach(r2.data, data =>  this.historiques.push(data));
                    this.historiquesLoaded = true;
                    this.loader = false;
                },
                error2 => {
                    console.error(error2);
                    this.loader = false;
                });
            this.currentJoueur = joueur;
        },
        showPartieDivider(partie, index) {
            if (index > 0) {
                return partie.date != this.partiesFiltered[index - 1].date;
                //if (this.orderJoueur == 'byName') return this.filtered[index - 1].nom != joueur.nom;
            }
            return false;
        },
        showDetailHistoriques() {
            this.showingHistorique = true;
            this.showingPartie = false;
        },
        showDetailParties() {
            this.showingHistorique = false;
            this.showingPartie = true;
        },
        infoJoueur(joueur) {
            this.showJoueurInfo = true;
        },
        goto(url) {
            window.location.href = url;
        },
        saveExtra() {
            this.loader = true;
            uri2 = this.api.ApiSettings.EndPoint + this.api.ApiSettings.JoueurExtra.replace('{licence}', this.currentJoueur.licence);
            body = {};
            body.Email = this.currentJoueur.Email;
            body.Telephone = this.currentJoueur.Telephone;
            console.log(uri2);
            Vue.http.put(uri2, JSON.stringify(body)).then(
                response => {
                    this.showJoueurInfo = false
                    this.loader = false;
                },
                error => {
                    console.dir(error);
                    this.showJoueurInfo = false
                    this.loader = false;
                    this.loadPartiesDujoueur(this.currentJoueur);
                }
            );
        },
        undoExtra() {
            this.showJoueurInfo = false;
            this.loadPartiesDujoueur(this.currentJoueur);
        }
    },
    computed: {
        joueursFilter() {
            if (this.filter == null) this.filter = "";
            console.dir(this.filter);
            if (this.filter == "") {
                console.log("restore filter");
                this.filtered = this.joueurs;
            }else
                if (_.isFinite(parseInt(this.filter))) {
                    console.log("classe filter");
                this.filtered = this.joueurs.filter(joueur => {  return joueur.classement == parseInt(this.filter); });
            } else {
                console.log("name filter");
                this.filtered = this.joueurs.filter(joueur => { return joueur.nom.toLowerCase().match(this.filter.toLowerCase()); });
                console.dir(this.filtered);
                }
            this.filtered = _.orderBy(this.filtered, this.orderJoueurChoices[this.orderJoueur].fields, this.orderJoueurChoices[this.orderJoueur].orders);
            return this.filtered;
        },
        partieOrdered() {
            
            this.pointJoueur = 0;
            this.partiesFiltered = _.orderBy(this.parties, ['date', 'idPartie'], ['asc', 'desc']);
            _.forEach(this.partiesFiltered,(value, key) => { console.dir(value); this.pointJoueur += value.pointsGagnesPerdus});

            return this.partiesFiltered;
        }
        
    },
    mounted:function() {
        console.log("mounted");
       this.getSettings();
        
    }
})

