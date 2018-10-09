
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

data = {
    drawer: null,
    miniDrawer: false,
    loader: false,
}

