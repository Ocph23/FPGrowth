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
            .state("penjual-profilpenjual", {
                url: '/profilpenjual',
                parent: "penjual",
                templateUrl: 'apps/views/penjual/profilpenjual.html'
            })
    });