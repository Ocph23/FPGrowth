angular.module('account.router', [ 'ui.router' ]).config(function($stateProvider, $urlRouterProvider) {
	$urlRouterProvider.otherwise('/pembeli/home');
	$stateProvider
		.state('account', {
			url: '/account',
			templateUrl: '../apps/views/account/index.html'
		})
		.state('login', {
			url: '/login',
			parent: 'account',
			controller: 'LoginController',
			templateUrl: '../apps/views/account/sign-in.html'
		})
		.state('reset-password', {
			url: '/resetpassword',
			parent: 'account',
			templateUrl: '../apps/views/account/resetpassword.html'
		})
		.state('recover-password', {
			url: '/recoverpassword',
			parent: 'account',
			templateUrl: '../apps/views/account/recoverpassword.html'
		})
		.state('register', {
			url: '/register',
			parent: 'account',
			controller: 'RegisterPembeliController',
			templateUrl: '../apps/views/account/sign-up.html'
		})
		.state('register-penjual', {
			url: '/registerpenjual',
			parent: 'account',
			controller: 'RegisterPenjualController',
			templateUrl: '../apps/views/account/sign-up-penjual.html'
		})
		.state('confirmemail', {
			url: '/confirmemail',
			parent: 'account',
			params: {
				user: null
			},
			controller: 'confirmEmailController',
			templateUrl: '../apps/views/account/confirmemail.html'
		});
});
