angular.module("penjual.router", [])
    .config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/account/login');
        $stateProvider
            .state("penjual", {
                url: '/penjual',
                templateUrl: 'apps/views/penjual/index.html'
            })
            .state("penjual-home", {
                url: '/phome',
                parent: "penjual",
                templateUrl: 'apps/views/penjual/phome.html'
            })
            .state("penjual-profilp", {
                url: '/profilp',
                parent: "penjual",
                templateUrl: 'apps/views/penjual/profilp.html'
            })
            .state("penjual-editprofil", {
                url: '/editprofil',
                parent: "penjual",
                templateUrl: 'apps/views/penjual/editprofil.html'
            })
    });