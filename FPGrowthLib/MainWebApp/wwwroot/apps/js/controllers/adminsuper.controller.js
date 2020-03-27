angular
	.module('adminsuper.controller', [])
	.controller('adminsuperController', adminsuperController)
	.controller('adminsuperHomeController', adminsuperHomeController)
	.controller('adminsuperMenuUtamaController', adminsuperMenuUtamaController)
	.controller('adminsuperDaftarKategoriController', adminsuperDaftarKategoriController)
	.controller('adminsuperTambahKategoriController', adminsuperTambahKategoriController)
	.controller('adminsuperDataParameterController', adminsuperDataParameterController)
	.controller('adminsuperDataTransaksiController', adminsuperDataTransaksiController)
	.controller('adminsuperAnalisaController', adminsuperAnalisaController)
	.controller('adminsuperManagemenTransaksiController', adminsuperManagemenTransaksiController)
	.controller('adminsuperDataPenjualController', adminsuperDataPenjualController)
	.controller('adminsuperDataPembeliController', adminsuperDataPembeliController)
	.controller('adminsuperDataOrderController', adminsuperDataOrderController)
	.controller('adminsuperKonfirPembayaranController', adminsuperKonfirPembayaranController)
	.controller('adminsuperKonfirPengirimanController', adminsuperKonfirPengirimanController);

function adminsuperController() {}
function adminsuperHomeController() {}
function adminsuperMenuUtamaController() {}
function adminsuperDaftarKategoriController($scope, KategoriService) {
	$scope.model = {};
	KategoriService.get().then((data) => {
		$scope.Items = data;
	});

	$scope.selectedItem = (param) => {
		$scope.model = angular.copy(param);
	};

	$scope.simpan = (param) => {
		if (param.idkategori == undefined) {
			KategoriService.post(param).then((res) => {
				$scope.model = {};
			});
		} else {
			KategoriService.put(param).then((res) => {
				$scope.model = {};
			});
		}
	};

	$scope.delete = (param) => {
		KategoriService.delete(param).then((data) => {});
	};
}

function adminsuperDataParameterController() {}
function adminsuperTambahKategoriController() {}
function adminsuperDataTransaksiController() {}
function adminsuperAnalisaController() {}
function adminsuperManagemenTransaksiController($scope, ManagemenTransaksiService) {
	$scope.model = {};
	$scope.tambahTitle = 'Tambah Data Parameter';
	ManagemenTransaksiService.get().then((data) => {
		$scope.Items = data;
	});

	$scope.new = () => {
		$scope.tambahTitle = 'Tambah Data Parameter';
		$scope.model = {};
	};

	$scope.selectedItem = (param) => {
		$scope.tambahTitle = 'Edit';
		$scope.model = angular.copy(param);
	};

	$scope.simpan = (param) => {
		if (param.idkategori == undefined) {
			ManagemenTransaksiService.post(param).then((res) => {
				$scope.model = {};
			});
		} else {
			ManagemenTransaksiService.put(param).then((res) => {
				$scope.model = {};
			});
		}
	};

	$scope.delete = (param) => {
		ManagemenTransaksiService.delete(param).then((data) => {});
	};
}
function adminsuperDataPenjualController() {}
function adminsuperDataPembeliController() {}
function adminsuperDataOrderController() {}
function adminsuperKonfirPembayaranController() {}
function adminsuperKonfirPengirimanController() {}
