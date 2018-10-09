
var mv=new Vue({
    el: '#app',
    data: () => (Object.assign({},data,{
        api: {},
        
    })),
    props: {
        
    },
    methods:{
        
        goto(url) {
            window.location.href = url;
        }
    },
    computed: {
        
    },
    mounted:function() {
        console.log("mounted");
       
        
    }
})

