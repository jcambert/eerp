var mv = new Vue({
    el: '#app',
    data: () => (Object.assign({}, data, {
        equipes: [],
        currentEquipe:null
    })),
    methods: Object.assign({}, methods, {
        getResultats(equipe) {
            this.showLoader("Recherche des resultats")
            uri = this.createApiUri("ResultatEquipe", [{ key: "division", value: equipe.idDivision }, { key: "poule", value: equipe.idPoule }]);
            get(uri)
                .then(response => {
                    console.dir(response);
                    this.resultats.splice(0, this.resultats.length);
                    _.forEach(response.data, resultat => this.resultats.push(resultat));
                    this.hideLoader();
                });
        },
        getClassements(equipe) {
            this.showLoader("Recherche des classements");
            uri = this.createApiUri("ClassementEquipe", [{ key: "division", value: equipe.idDivision }, { key: "poule", value: equipe.idPoule }]);
            get(uri)
                .then(response => {
                    console.dir(response);
                    this.classements.splice(0, this.classements.length);
                    _.forEach(response.data, classement => this.classements.push(classement));
                    this.hideLoader();
                });
        },
        getResultatsAndClassement(equipe) {
            return new Promise((resolve, reject) => {
                this.showLoader("Recherche des resultats")
                uri = this.createApiUri("ResultatEquipe", [{ key: "division", value: equipe.idDivision }, { key: "poule", value: equipe.idPoule }]);
                return resolve(uri);
                })
                .then(get)
                .then(response => {
                    this.resultats.splice(0, this.resultats.length);
                    _.forEach(response.data, resultat => this.resultats.push(resultat));

                    this.showLoader("Recherche des classements");
                    uri = this.createApiUri("ClassementEquipe", [{ key: "division", value: equipe.idDivision }, { key: "poule", value: equipe.idPoule }]);
                    return uri;
                })
                .then(get)
                .then(response => {
                    this.classements.splice(0, this.classements.length);
                    _.forEach(response.data, classement => this.classements.push(classement));
                    this.hideLoader();
                })
        },
        getColorA(resultat) {
            if (resultat.scoreA > resultat.scoreB) return 'success';
            if (resultat.scoreA < resultat.scoreB) return 'error';
            return 'primary';
        },
        getColorB(resultat) {
            if (resultat.scoreB > resultat.scoreA) return 'success';
            if (resultat.scoreB < resultat.scoreA) return 'error';
            return 'primary';
        },
        viewClub(numero) {
            alert('set viewclub:' + numero);
            window.viewingClub = numero;
            this.goto('');
        }
    }),
    mounted: function () {
        
        this.getSettings()
            .then(response => {
                this.showLoader("Recherche des equipes du clubs");
                this.equipes.splice(0,this.equipes.length)
                uri = this.createApiUri("EquipesDuclub", [{ key: "numero", value: this.club.numero }, { key: "type", value: "M" }]);//this.api.ApiSettings.EndPoint + this.api.ApiSettings.EquipesDuclub.replace('{numero}', this.api.User.numeroClub).replace('{type}', 'M');
                return uri;    
            })
            .then(get) 
       
            .then(response => {
       //         console.dir(response.data);
                _.forEach(response.data, equipe => { this.equipes.push(equipe); });
                if(this.equipes.length>0)
                    this.currentEquipe = this.equipes[0];
            })
         //   .then(this.getOrganismes)
            .then(organismes => {
                this.hideLoader();
                console.log('Equipes mounted');
            })
            ;
    }



});