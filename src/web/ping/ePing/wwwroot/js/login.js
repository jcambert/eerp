
String.prototype.format = function () {
    var str = this;
    for (i = 0; i < arguments.length; i++) {
        str = str.replace('{' + (i) + '}', (arguments[i] == null || arguments[i] == undefined)?"":arguments[i]);
    }
    str = str.replace(/\{\d+\}/, "");
    return str;
}

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
        licence: "905821",
        prenom: "",
        tokenendpoint: "",
        valid: false,
        licenceRules: [v => !!v || 'Votre numero de licence ou votre nom sont requis'],
        prenomRules: [v => !!v || 'Votre prenom est requis'],
    }),

    props: {
        source: String
    },
    methods:{
        submitForm() {
            if (this.$refs.form.validate()) {
                url = this.tokenendpoint.format(this.licence, this.prenom)

                url = "/login/?licenceOrName={0}&prenom={1}".format(this.licence, this.prenom);
                
                Vue.http.post(url).then(
                    function (response) { 
                        console.dir(response);
                        window.location.href = "/Home";
                    },
                    function (error) {
                        console.dir(error);
                    });
                    
            }
        },
        clearForm() {
            this.$refs.form.reset();
            console.dir(this.licence);
        },
        isLicence() {
            return /^\d+$/.test(this.licence) || this.licence=="" || this.licence == undefined;
        },
        
    },
    computed: {
        formValid:function() {
            return this.valid;
        }
    },
    mounted:function() {
       // this.setTokenEndPoint(Vue.tokenEndPoint);
        //console.log('mounted');
    }
})

