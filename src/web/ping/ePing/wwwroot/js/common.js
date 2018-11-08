
function extendFunction(oldOne, newOne) {
    return (function () {
        oldOne();
        newOne();
    })();
}

Vue.filter("formatNumber", function (value) {
    return numeral(value).format("0.0"); // displaying other groupings/separators is possible, look at the docs
});
Vue.filter("formatPoints", function (value) {
    return value > 0 ? "+" + value : value; // displaying other groupings/separators is possible, look at the docs
});

Vue.directive('init', {
    bind: function (el, binding, vnode) {
        vnode.context[binding.arg] = binding.value;
        console.dir(vnode.context);
    }
});
Vue.http.headers.common['Access-Control-Allow-Origin'] = '*';


Vue.component('line-chart', {
    extends: VueChartJs.Line,
    render: function (createElement) {
        return createElement(
            'div', {
                style: this.styles,
                class: this.cssClasses
            },
            [
                createElement(
                    'canvas', {
                        attrs: {
                            id: this.id,
                            type: this.chartId,
                            width: this.width,
                            height: this.height
                        },
                        ref: 'canvas'
                    }
                )
            ]
        )
    },
    //mixins: [mixins.reactiveProp],
    watch: {
        datasets: {
            handler: function (val, oldVal) {
                if (oldVal == val) return;
                console.log('datasets change');
                this.renderChart({ labels: this.labels, datasets: val }, this.options);
            },
            deep: true
        }
    },
    props: {
        id: {
            required: true,
            type: String
        },
        datasets: {
            default: [],
            type: Array
        },
        labels: {
            default: [],
            type: Array
        },
        options: {
            default: function () {
                return {
                    responsive: true,
                    maintainAspectRatio: false,
                    showTooltips: false,
                    onAnimationComplete: function () {

                        var ctx = this.chart.ctx;
                        ctx.font = this.scale.font;
                        ctx.fillStyle = this.scale.textColor
                        ctx.textAlign = "center";
                        ctx.textBaseline = "bottom";

                        this.datasets.forEach(function (dataset) {
                            dataset.points.forEach(function (points) {
                                ctx.fillText(points.value, points.x, points.y - 10);
                            });
                        })
                    }
                }
            },
            type: Object
        }
    },
    mounted() {
        console.log('chart line mounted');
        console.dir(this.labels);
        console.dir(this.datasets);
        this.renderChart({ labels: this.labels, datasets: this.datasets }, this.options);
    }

});

var get = function (uri) {
    console.log("Make get request:", uri);
    return Vue.http.get(uri);
}

data = {
    drawer: null,
    miniDrawer: false,
    loader: false,
    loaderMessage: null,
    api: {},
    datasets: [],
    labels: [],
    snackbar: { visible: false, timeout: 3000, message: '', color: 'info' },
    organismes: [],
    club:null

}

methods = {
    showLoader(message) {
        this.loaderMessage = message;
        this.loader = true;
    },
    hideLoader() {
        this.loader = false;
    },
    showSnackbar(options) {
        _.merge(this.snackbar, options);
        console.dir(this.snackbar);
        this.snackbar.visible = true;
    },
    goto(url) {
        window.location.href = url;
    },
    createApiUri(name, queries) {
        //console.dir(this.api.ApiSettings.EndPoint); 
        uri = this.api.ApiSettings.EndPoint + this.api.ApiSettings[name];
        //console.dir(uri);
        _.forEach(queries, query => {
            uri = uri.replace("{"+query.key+"}", query.value);
        });
        return  uri;
    },
    getSettings() {
        
            this.showLoader("Connexion à la base de données en cours");
            return get('/api/dashboard/settings')
                .then(
                result => {
                    this.showLoader("Recherche du club");
                        this.api = result.data; console.dir(result);
                        //uri = this.api.ApiSettings.EndPoint + this.api.ApiSettings.Club.replace('{numero}', this.api.User.numeroClub);
                        this.createApiUri("Club", [{ key: "numero", value: this.api.User.numeroClub }]);
                        //console.dir(uri);

                        return uri;
                       

                })
                .then(get)
                .then(response => {
                    //this.showLoader("Recherche des joueurs du clubs");
                    this.club = response.data;
                    //uri = this.api.ApiSettings.EndPoint + this.api.ApiSettings.JoueursDuClub.replace('{numero}', this.api.User.numeroClub);
                    //return uri
                })
                /*.then(get)
                .then(response => {
                    this.joueurs = this.filtered = response.data;
                    return this.api;
                })*/
                .catch(error => {
                    console.error(error);
                    this.hideLoader();
                    this.showSnackbar({ color: 'error', message: error });
                })
           
            
        
    },

    getOrganismes() {
        
        return new Promise((resolve, reject) => {
            this.showLoader("Recherche des organismes");
            uri = this.api.ApiSettings.EndPoint + this.api.ApiSettings.Organismes;
            get(uri).then(
                response => {
                    this.organismes.splice(0, this.organismes.length);
                    _.forEach(response.data, organisme => { this.organismes.push(organisme); });
                    return resolve({ organismes: this.organismes });
                },
                error => {
                    return reject(error);
                });

        });
    }
};

mounted = function () {
    console.log("start mounted");


    //this.getSettings();
};
