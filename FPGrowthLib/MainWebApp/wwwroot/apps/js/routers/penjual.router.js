angular.module("penjual.router", [])
    .config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/account/login');
        $stateProvider
            .state("penjual", {
                url: '/penjual',
                templateUrl: 'apps/views/penjual/index.html'
            })
            .state("penjual-home", {
                url: '/home',
                parent: "penjual",
                templateUrl: 'apps/views/pembeli/home.html'
            })
    });