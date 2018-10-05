


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
        apisettings: {},
        items: [],
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
                    this.apisettings = result.body; console.dir(result); 

                   // Vue.http.get(this.apisettings.EndPoint+this.apisettings.Club)
                },
                error => { console.error(error); });
        }
        
    },
    computed: {
        
    },
    mounted:function() {
        console.log("mounted");
       this.getSettings();
        
    }
})

