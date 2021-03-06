angular.module('penjual.router', []).config(function($stateProvider, $urlRouterProvider) {
	$stateProvider
		.state('penjual', {
			url: '/penjual',
			controller: 'penjualController',
			templateUrl: 'apps/views/penjual/index.html'
		})
		.state('penjual-home', {
			url: '/home',
			controller: 'penjualpHomeController',
			parent: 'penjual',
			templateUrl: 'apps/views/penjual/phome.html'
		})
		.state('penjual-profile', {
			url: '/profile',
			parent: 'penjual',
			controller: 'penjualprofilpController',
			templateUrl: 'apps/views/penjual/profilp.html'
		})
		.state('penjual-editprofile', {
			url: '/editprofile',
			parent: 'penjual',
			controller: 'penjualeditprofilController',
			templateUrl: 'apps/views/penjual/editprofil.html'
		})
		.state('penjual-daftarbarang', {
			url: '/daftarbarang',
			parent: 'penjual',
			controller: 'penjualdaftarbarangController',
			templateUrl: 'apps/views/penjual/daftarbarang.html'
		})
		.state('penjual-detailbarang', {
			url: '/detailbarang/:id',
			params: {
				id: null
			},
			parent: 'penjual',
			controller: 'penjualdetailbarangController',
			templateUrl: 'apps/views/penjual/detailbarang.html'
		})
		.state('penjual-tambahbarang', {
			url: '/tambahbarang',
			parent: 'penjual',
			controller: 'penjualtambahbarangController',
			templateUrl: 'apps/views/penjual/tambahbarang.html'
		})
		.state('penjual-editbarang', {
			url: '/editbarang/:id',
			parent: 'penjual',
			controller: 'penjualeditbarangController',
			templateUrl: 'apps/views/penjual/tambahbarang.html',
			resolve: {
				PreviousState: [
					'$state',
					function($state) {
						var currentStateData = {
							Name: $state.current.name,
							Params: $state.params,
							URL: $state.href($state.current.name, $state.params)
						};
						return currentStateData;
					}
				]
			}
		})
		.state('penjual-daftarorder', {
			url: '/daftarorder',
			parent: 'penjual',
			controller: 'penjualdaftarorderController',
			templateUrl: 'apps/views/penjual/daftarorder.html'
		})
		.state('penjual-daftarpesanan', {
			url: '/daftarpesanan',
			parent: 'penjual',
			controller: 'penjualdaftarpesananController',
			templateUrl: 'apps/views/penjual/daftarpesanan.html'
		})
		.state('penjual-inbox', {
			url: '/inbox',
			parent: 'penjual',
			controller: 'inboxController',
			templateUrl: 'apps/views/inbox.html'
		})
		.state('penjual-laporan', {
			url: '/laporan',
			parent: 'penjual',
			controller: 'penjualLaporanController',
			templateUrl: 'apps/views/penjual/laporan.html'
		});
});
