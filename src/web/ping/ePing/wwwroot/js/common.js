
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

})
data = {
    drawer: null,
    miniDrawer: false,
    loader: false,
    api: {},
    datasets: [],
    labels: [],
    snackbar: { visible: false, timeout: 4000, message: '', color: 'info' },
}

methods = {
    
    showSnackbar(options) {
        _.merge(this.snackbar, options);
        this.snackbar.visible = true;
    },
    goto(url) {
        window.location.href = url;
    },
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
                error => {
                    console.error(error); this.loader = false;
                });
    }
}

