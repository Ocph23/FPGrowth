angular.module('algoritma.router', [ 'ui.router' ]).config(function($stateProvider, $urlRouterProvider) {
	$stateProvider.state('algoritma', {
		url: '/algoritma',
		controller: 'AlgoritmaController',
		templateUrl: '../apps/views/algoritma/index.html'
	});
});
