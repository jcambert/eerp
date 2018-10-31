var mv = new Vue({
    el: '#app',
    data: () => (Object.assign({}, data, {})),
    methods: Object.assign({}, methods, {}),
    mounted: function () {
        this.getSettings();
        console.log('Index mounted');
    }

       
    
});