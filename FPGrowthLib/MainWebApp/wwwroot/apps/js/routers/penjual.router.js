angular.module('penjual.router', []).config(function ($stateProvider, $urlRouterProvider) {
	$stateProvider
		.state('penjual', {
			url: '/penjual',
			templateUrl: 'apps/views/penjual/index.html'
		})
		.state('penjual-home', {
			url: '/home',
			parent: 'penjual',
			templateUrl: 'apps/views/penjual/phome.html'
		})
		.state('penjual-profile', {
			url: '/profile',
			parent: 'penjual',
			templateUrl: 'apps/views/penjual/profilp.html'
		})
		.state('penjual-editprofile', {
			url: '/editprofile',
			parent: 'penjual',
			templateUrl: 'apps/views/penjual/editprofil.html'
		})
		.state('penjual-daftarbarang', {
			url: '/daftarbarang',
			parent: 'penjual',
			templateUrl: 'apps/views/penjual/daftarbarang.html'
		})
		.state('penjual-detailbarang', {
			url: '/detailbarang',
			parent: 'penjual',
			templateUrl: 'apps/views/penjual/detailbarang.html'
		});
});