var mv = new Vue({
    el: '#app',
    data: () => (Object.assign({}, data, {})),
    methods: Object.assign({}, methods, {}),
    mounted: function () {
        this.getSettings()
            .then(reponse =>
            { 
                console.log('Index mounted'); 
                this.hideLoader();
            });
        
    }



});