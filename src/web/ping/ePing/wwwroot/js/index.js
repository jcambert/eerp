var mv = new Vue({
    el: '#app',
    data: () => (Object.assign({}, data, {})),
    methods: Object.assign({}, methods, {}),
    mounted: function () {
        console.log("mounted");
        this.getSettings();
    }
});