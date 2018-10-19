

var mv=new Vue({
    el: '#app',
    data: () => (Object.assign({}, data, {
        showDetail: false,
        
        club: {},
        joueurs: [],
        parties: [],
        partiesFiltered: [],
        historiques: {},
        historiquesLoaded: false,
        currentJoueur: {},
        pointJoueur: 0,
        items: [],
        filter: "",
        orderJoueurChoices: {
            'byName': { 'fields': ['nom', 'prenom'], 'orders': ['asc', 'asc'] },
            'byPoints': { 'fields': ['point', 'nom', 'prenom'], 'orders': ['desc', 'asc', 'asc'] },
            'byCategorie': { 'fields': ['categorie', 'nom', 'prenom'], 'orders': ['desc', 'asc', 'asc'] }
        },
        orderJoueur: 'byName',
        filtered: [],
        oldclasse: 5,

        showJoueurInfo: false,
        showingHistorique: false,
        showingPartie: false,
        categorieAge: {
            'N': { value: 'Non determiné' },
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
        activeChart: "tabs-",
        chartDialog: false,
        charts: [{ id: 0, title: 'Classement', text: 'Classement' }, { id: 1, title: 'Points', text: 'Points' }, { id: 2, title: 'Moyenne', text: 'Moyenne' }, { id: 3, title: 'Victoire', text: 'Victoire' }, { id: 4, title: 'Défaite', text: 'Défaite' }],
        chartData: {},
        
        

       
    })),
    props: {
        source: String
    },
    methods: Object.assign({}, methods, {
        /*showSnackbar(options) {
            _.merge(this.snackbar, options);
            this.snackbar.visible = true;
        },*/
        
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
                });
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
                    this.currentJoueur = joueur;
                },
                error => {
                    console.error(error);
                    this.showSnackbar({ color: 'error', message: error });
                    this.loader = false;
                });

                
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
                    //this.loadPartiesDujoueur(this.currentJoueur);
                }
            );
        },
        undoExtra() {
            this.showJoueurInfo = false;
           // this.loadPartiesDujoueur(this.currentJoueur);
        },
        onActiveChartChanged(index) {
            console.log('Chart tab change to:'+index)
            if (!_.has(this.currentJoueur, 'licence')) return;
            console.log(index);
            this.showChart(this.currentJoueur, index);
        },
        showChart(joueur,index) {
            this.loader = true;
            this.currentJoueur = joueur;
            this.labels.splice(0,this.labels.length);
            this.datasets.splice(0,this.datasets.length) ;
            this.buildChart(joueur, index == undefined ? 0 : index).then(
                result => { 
                    console.dir(result);
                    this.labels = result.labels;
                    this.datasets = result.datasets;
                    this.loader = false;
                    this.chartDialog = true; 
                },
                error => {
                    console.error(error);
                    this.showSnackbar({ color: 'error', message: error });
                    this.loader = false;
                });
           
        },
        buildChart(joueur, index) {
            
            
            return new Promise((resolve, reject) => {
                if (index == undefined) return reject();
                console.log('BuildChart:' + index);
                _datasets = [];
                _labels = [];
                //console.dir(joueur); return;
                if (index === 0) {
                   
                    uri2 = this.api.ApiSettings.EndPoint + this.api.ApiSettings.HistoriqueClassementDuJoueur.replace('{licence}', joueur.licence);
                    Vue.http.get(uri2).then(
                        response => {
                            // window.data = response.data;
                            data = [];
                            _.forEach(response.data, cl => {
                                _labels.push(cl.date);
                                data.push(cl.point);
                            })
                           _datasets.push(
                                {
                                    label: 'Historique du classement de ' + joueur.prenom,
                                    backgroundColor: window.randomColor(),
                                    borderColor: window.randomColor(),
                                    data: data,
                                    fill: false,
                                });
                            console.dir(_labels);
                            console.dir(_datasets);
                           return resolve({labels:_labels,datasets:_datasets});
                        },
                        error => {
                            //console.dir(error);
                            return reject(error);
                            //this.loader = false;
                        }
                    );
                }
                else if (index === 1) {
                    uri2 = this.api.ApiSettings.EndPoint + this.api.ApiSettings.HistoriquePointDuJoueur.replace('{licence}', joueur.licence);

                    Vue.http.get(uri2).then(
                        response => {
                            // window.data = response.data;
                            data = [];
                            _.forEach(response.data, cl => {
                                _labels.push(cl.date);
                                data.push(cl.pointsGagnesPerdus);
                            })
                            _datasets.push(
                                {
                                    label: 'Historique des points de ' + joueur.prenom,
                                    backgroundColor: window.randomColor(),
                                    borderColor: window.randomColor(),
                                    data: data,
                                    fill: false,
                                });
                            console.dir(_labels);
                            console.dir(_datasets);
                            return resolve({ labels: _labels, datasets: _datasets });
                        },
                        error => {
                            return reject(error);
                           // console.dir(error);

                           // this.loader = false;
                        }
                    );
                }
                else if (index === 3) {
                    uri2 = this.api.ApiSettings.EndPoint + this.api.ApiSettings.HistoriqueVictoireDuJoueur.replace('{licence}', joueur.licence);

                    Vue.http.get(uri2).then(
                        response => {
                            // window.data = response.data;
                            data = [];
                            _.forEach(response.data, cl => {
                                _labels.push(cl.date);
                                data.push(cl.victoire);
                            })
                            _datasets.push(
                                {
                                    label: 'Historique des victoires de ' + joueur.prenom,
                                    backgroundColor: window.randomColor(),
                                    borderColor: window.randomColor(),
                                    data: data,
                                    fill: false,
                                });
                            console.dir(_labels);
                            console.dir(_datasets);
                            return resolve({ labels: _labels, datasets: _datasets });
                        },
                        error => {
                            return reject(error);
                            // console.dir(error);

                            // this.loader = false;
                        }
                    );
                }
                else if (index === 4) {
                    uri2 = this.api.ApiSettings.EndPoint + this.api.ApiSettings.HistoriqueDefaiteDuJoueur.replace('{licence}', joueur.licence);

                    Vue.http.get(uri2).then(
                        response => {
                            // window.data = response.data;
                            data = [];
                            _.forEach(response.data, cl => {
                                _labels.push(cl.date);
                                data.push(cl.defaite);
                            })
                            _datasets.push(
                                {
                                    label: 'Historique des defaites de ' + joueur.prenom,
                                    backgroundColor: window.randomColor(),
                                    borderColor: window.randomColor(),
                                    data: data,
                                    fill: false,
                                });
                            console.dir(_labels);
                            console.dir(_datasets);
                            return resolve({ labels: _labels, datasets: _datasets });
                        },
                        error => {
                            return reject(error);
                            // console.dir(error);

                            // this.loader = false;
                        }
                    );
                }
                else {

                    _.forEach(['January', 'February', 'March', 'April', 'May', 'June', 'July'], month => { _labels.push(month); });



                    for (var i = 0; i < 2; i++) {
                        _datasets.push(
                            {
                                label: 'My ' + joueur.prenom + ' dataset',
                                backgroundColor: window.randomColor(),
                                borderColor: window.randomColor(),
                                data: [
                                    randomScalingFactor(),
                                    randomScalingFactor(),
                                    randomScalingFactor(),
                                    randomScalingFactor(),
                                    randomScalingFactor(),
                                    randomScalingFactor(),
                                    randomScalingFactor()
                                ],
                                fill: false,
                            });
                    }
                    console.dir(_labels);
                    console.dir(_datasets);
                    return resolve({ labels: _labels, datasets: _datasets });

                }
               
            });
        }
    }),
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

