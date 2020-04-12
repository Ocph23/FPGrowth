angular.module('pembeli.router', []).config(function($stateProvider, $urlRouterProvider) {
	$stateProvider
		.state('pembeli', {
			url: '/pembeli',
			controller: 'pembeliController',
			templateUrl: 'apps/views/pembeli/index.html'
		})
		.state('pembeli-home', {
			url: '/home',
			parent: 'pembeli',
			controller: 'pembeliHomeController',
			templateUrl: 'apps/views/pembeli/home.html'
		})
		.state('pembeli-homemenu', {
			url: '/homemenu',
			parent: 'pembeli',
			templateUrl: 'apps/views/pembeli/homemenu.html'
		})
		.state('pembeli-detailproduk', {
			url: '/detailproduk/:id',
			parent: 'pembeli',
			params: {
				id: null
			},
			controller: 'pembeliDetailProdukController',
			templateUrl: 'apps/views/pembeli/detailproduk.html'
		})
		.state('pembeli-profilpenjual', {
			url: '/profilpenjual',
			controller: 'pembeliHomeController',
			parent: 'pembeli',
			templateUrl: 'apps/views/pembeli/profilpenjual.html'
		})
		.state('pembeli-keranjang', {
			url: '/keranjang',
			parent: 'pembeli',
			templateUrl: 'apps/views/pembeli/keranjang.html'
		})
		.state('pembeli-order', {
			url: '/order',
			parent: 'pembeli',
			params: {
				data: null
			},
			controller: 'pembeliorderController',
			templateUrl: 'apps/views/pembeli/order.html'
		})
		.state('pembeli-tagihan', {
			url: '/tagihan',
			parent: 'pembeli',
			templateUrl: 'apps/views/pembeli/tagihan.html'
		})
		.state('pembeli-daftartagihan', {
			url: '/daftartagihan',
			parent: 'pembeli',
			controller: 'pembelidaftartagihanController',
			templateUrl: 'apps/views/pembeli/daftartagihan.html'
		})
		.state('pembeli-konfirbayar', {
			url: '/konfirbayar',
			parent: 'pembeli',
			params: {
				data: null
			},
			controller: 'pembelikonfirbayarController',
			templateUrl: 'apps/views/pembeli/konfirbayar.html'
		})
		.state('pembeli-pesan', {
			url: '/pesan',
			parent: 'pembeli',
			templateUrl: 'apps/views/pembeli/pesan.html'
		})
		.state('pembeli-diskusi', {
			url: '/diskusi',
			parent: 'pembeli',
			templateUrl: 'apps/views/pembeli/diskusi.html'
		});
});
