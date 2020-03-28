angular
	.module('penjual.controller', [])
	.controller('penjualController', penjualController)
	.controller('penjualpHomeController', penjualpHomeController)
	.controller('penjualprofilpController', penjualprofilpController)
	.controller('penjualeditprofilController', penjualeditprofilController)
	.controller('penjualdaftarbarangController', penjualdaftarbarangController)
	.controller('penjualdetailbarangController', penjualdetailbarangController)
	.controller('penjualtambahbarangController', penjualtambahbarangController)
	.controller('penjualdaftarorderController', penjualdaftarorderController)
	.controller('penjualdaftarpesananController', penjualdaftarpesananController);

function penjualController($scope, AuthService) {
	//AuthService.Init([ 'penjual' ]);
	AuthService.profile().then((x) => {
		var x = x;
	});
}

function penjualpHomeController() {}

function penjualprofilpController() {}

function penjualeditprofilController() {}

function penjualdaftarbarangController() {}

function penjualdetailbarangController() {}

function penjualtambahbarangController() {}

function penjualdaftarorderController() {}

function penjualdaftarpesananController() {}
