angular
	.module('pembeli.controller', [])
	.controller('pembeliController', pembeliController)
	.controller('pembeliHomeController', pembeliHomeController)
	.controller('pembeliDetailProdukController', pembeliDetailProdukController)
	.controller('pembeliprofilpenjualController', pembeliprofilpenjualController)
	.controller('pembelikeranjangController', pembelikeranjangController)
	.controller('pembeliorderController', pembeliorderController)
	.controller('pembelitagihanController', pembelitagihanController)
	.controller('pembelidaftartagihanController', pembelidaftartagihanController)
	.controller('pembelikonfirbayarController', pembelikonfirbayarController)
	.controller('pembelipesanController', pembelipesanController)
	.controller('pembelidiskusiController', pembelidiskusiController);

function pembeliController() {}

function pembeliHomeController($scope, BarangService) {
	$scope.Source = [];
	BarangService.get().then((barangs) => {
		$scope.Source = barangs;
	});
}

function pembeliDetailProdukController($scope, $stateParams, BarangService, PembeliCartService) {
	BarangService.getById($stateParams.id).then((result) => {
		$scope.model = result;
	});

	$scope.addToCart = (item) => {
		PembeliCartService.add(item);
	};
}

function pembeliprofilpenjualController() {}

function pembelikeranjangController() {}

function pembeliorderController($scope, $stateParams) {
	$scope.datas = $stateParams.data;
}

function pembelitagihanController() {}

function pembelidaftartagihanController() {}

function pembelikonfirbayarController() {}

function pembelipesanController() {}

function pembelidiskusiController() {}
