// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
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
})

new Vue({
    el: '#app',
    data: () => ({
        drawer: null
    }),

    props: {
        source: String
    }
})