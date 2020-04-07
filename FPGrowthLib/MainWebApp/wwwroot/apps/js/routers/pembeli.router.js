angular.module('pembeli.router', []).config(function($stateProvider, $urlRouterProvider) {
	$stateProvider
		.state('pembeli', {
			url: '/pembeli',
			templateUrl: 'apps/views/pembeli/index.html'
		})
		.state('pembeli-home', {
			url: '/home',
			parent: 'pembeli',
			controller: 'pembeliHomeController',
			templateUrl: 'apps/views/pembeli/home.html'
		})
		.state('pembeli-detailproduk', {
			url: '/detailproduk/:id',
			parent: 'pembeli',
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
			templateUrl: 'apps/views/pembeli/daftartagihan.html'
		})
		.state('pembeli-konfirbayar', {
			url: '/konfirbayar',
			parent: 'pembeli',
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
