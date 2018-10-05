


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
        apiendpoint: "",
        apisettings: {},
        items: [],
        lorem: `Lorem ipsum dolor sit amet, mel at clita quando. Te sit oratio vituperatoribus, nam ad ipsum posidonium mediocritatem, explicari dissentiunt cu mea. Repudiare disputationi vim in, mollis iriure nec cu, alienum argumentum ius ad. Pri eu justo aeque torquatos.`
       
    }),
    props: {
        source: String
    },
    methods:{
        
        
    },
    computed: {
        
    },
    mounted:function() {
        console.log("mounted");
        console.log(this.apiendpoint);
        Vue.http.get('/api/settings').then(function (result) { console.dir(result); }, function (error) { console.error(error); });
        
    }
})

