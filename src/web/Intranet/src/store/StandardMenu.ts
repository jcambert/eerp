export default [
        //{ icon: 'contacts', text: 'Contacts' },
       // { icon: 'history', text: 'Frequently contacted' },
       // { icon: 'content_copy', text: 'Duplicates' },
        {
            icon: 'keyboard_arrow_up',
            'icon-alt': 'keyboard_arrow_down',
            text: 'Commercial',
            model: true,
            children: [
                { icon: 'phone_forwarded', text: 'Offres de Prix',route_name:'commercial.offre' },
                { icon: 'euro_symbol', text: 'Cotations',route_name:'commercial.cotation' }
            ]
        }
       
    ]
