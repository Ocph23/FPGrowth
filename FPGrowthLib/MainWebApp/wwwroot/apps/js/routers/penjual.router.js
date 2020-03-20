angular.module("penjual.router", [])
    .config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/account/login');
        $stateProvider
            .state("penjual", {
                url: '/penjual',
                templateUrl: ''
            })
            .state("penjual-home", {
                url: '/home',
                parent: "penjual",
                templateUrl: ''
            })
    });