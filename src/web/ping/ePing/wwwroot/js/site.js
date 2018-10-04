// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*
new Vue({
    el: '#app',
    data: () => ({
        drawer: null,
        message:'BONJOUR !!!',
        items: [
            { icon: 'lightbulb_outline', text: 'Notes' },
            { icon: 'touch_app', text: 'Reminders' },
            { divider: true },
            { heading: 'Labels' },
            { icon: 'add', text: 'Create new label' },
            { divider: true },
            { icon: 'archive', text: 'Archive' },
            { icon: 'delete', text: 'Trash' },
            { divider: true },
            { icon: 'settings', text: 'Settings' },
            { icon: 'chat_bubble', text: 'Trash' },
            { icon: 'help', text: 'Help' },
            { icon: 'phonelink', text: 'App downloads' },
            { icon: 'keyboard', text: 'Keyboard shortcuts' }
        ]
    }),
    props: {
        source: String
    }
})*/
String.prototype.format = function () {
    var str = this;
    for (i = 0; i < arguments.length; i++) {
        //if (arguments[i] == null || arguments[i] == undefined) continue;
        str = str.replace('{' + (i) + '}', (arguments[i] == null || arguments[i] == undefined)?"":arguments[i]);
       // console.log(arguments[i]);//console.log(str)
    }
    str = str.replace(/\{\d+\}/, "");
    //console.dir(str)
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
        console.log('mounted');
    }
})

