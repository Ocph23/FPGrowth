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

function adminsuperTambahKategoriController() {}
function adminsuperDataParameterController() {}
function adminsuperDataTransaksiController() {}
function adminsuperAnalisaController() {}
function adminsuperManagemenTransaksiController() {}
function adminsuperDataPenjualController() {}
function adminsuperDataPembeliController() {}
function adminsuperDataOrderController() {}
function adminsuperKonfirPembayaranController() {}
function adminsuperKonfirPengirimanController() {}
