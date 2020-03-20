angular.module('account.router', [ 'ui.router' ]).config(function($stateProvider, $urlRouterProvider) {
	$urlRouterProvider.otherwise('/account/login');
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
		.state('register', {
			url: '/register',
			parent: 'account',
			templateUrl: '../apps/views/account/register.html'
		})
		.state('home', {
			url: '/home',
			controller: 'homeController',
			templateUrl: '../apps/views/home.html'
		})
		.state(
			'about',
			{
				// we'll get to this in a bit
			}
		);
});
