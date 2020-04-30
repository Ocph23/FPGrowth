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

function adminsuperController($scope, AuthService) {
	AuthService.Init([ 'adminsuper' ]);
}
function adminsuperHomeController($scope, kodefikasiService, DataPembeliService, OrderService) {
	$scope.kodefikasi = kodefikasiService;
	$scope.orderService = OrderService;
	DataPembeliService.get().then((result) => {
		$scope.pembeli = result;
		OrderService.get().then((result) => {
			result.forEach((element) => {
				OrderService.setBarang(element);
				element.total = OrderService.total(element.data);
				OrderService.diantar(element);
			});
			$scope.orders = result;
			$scope.konfirm = result.filter((x) => x.pembayaran);
		});
	});
}

function adminsuperDaftarKategoriController($scope, KategoriService, kodefikasiService) {
	$scope.kodefikasi = kodefikasiService;
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
function adminsuperDataPenjualController($scope, DataPenjualService, kodefikasiService) {
	$scope.kodefikasi = kodefikasiService;
	DataPenjualService.get().then((result) => {
		$scope.source = result;
	});
}

function adminsuperDataPembeliController($scope, DataPembeliService, kodefikasiService) {
	$scope.kodefikasi = kodefikasiService;
	DataPembeliService.get().then((result) => {
		$scope.source = result;
	});
}

function adminsuperDataOrderController($scope, OrderService, kodefikasiService) {
	$scope.kodefikasi = kodefikasiService;
	$scope.orderService = OrderService;

	OrderService.get().then((result) => {
		$scope.source = result.filter((x) => !x.pembayaran);
		$scope.source.forEach((element) => {
			OrderService.setBarang(element);
			OrderService.diantar(element);
		});
	});
}
function adminsuperKonfirPembayaranController($scope, OrderService, kodefikasiService) {
	$scope.kodefikasi = kodefikasiService;
	$scope.orderService = OrderService;
	OrderService.get().then((result) => {
		$scope.source = result.filter((x) => x.pembayaran);
		$scope.source.forEach((element) => {
			OrderService.setBarang(element);
			OrderService.diantar(element);
		});
	});

	$scope.showBukti = (item) => {
		$scope.model = item;
	};

	$scope.verifikasiPembayaran = (item) => {
		OrderService.verifikasiPembayaran(item.pembayaran.idpembayaran).then((x) => {
			item.pembayaran.status_pembayaran = 'Lunas';
		});
	};
}
function adminsuperKonfirPengirimanController($scope, OrderService, kodefikasiService) {
	$scope.kodefikasi = kodefikasiService;
	$scope.orderService = OrderService;

	OrderService.get().then((result) => {
		$scope.source = result.filter((x) => x.pembayaran);
	});
}

function adminsuperMenuUtamaController() {}
function adminsuperDataParameterController() {}
function adminsuperTambahKategoriController() {}
function adminsuperDataTransaksiController() {}
function adminsuperAnalisaController() {}
