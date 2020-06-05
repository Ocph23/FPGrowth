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
	.controller('adminsuperLaporanController', adminsuperLaporanController)
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

function adminsuperManagemenTransaksiController($scope, ManagemenTransaksiService, message) {
	$scope.model = {};
	$scope.tambahTitle = 'Tambah Data';
	ManagemenTransaksiService.get().then((data) => {
		$scope.Items = data;
	});

	$scope.new = () => {
		$scope.tambahTitle = 'Tambah Data';
		$scope.model = { potongan: 0, bts_jumlah_pengiriman: 0, status: true };
	};

	$scope.selectedItem = (param) => {
		$scope.tambahTitle = 'Edit';
		$scope.model = angular.copy(param);
	};

	$scope.simpan = (param) => {
		if (param.idmanajemen == undefined) {
			ManagemenTransaksiService.post(param).then((res) => {
				$scope.model = {};
				$('#modelId').modal('hide');
				message.info('Data Berhasil Ditambah');
			});
		} else {
			ManagemenTransaksiService.put(param).then((res) => {
				$scope.model = {};
				$('#modelId').modal('hide');
				message.info('Data Berhasil Diubah');
			});
		}
	};

	$scope.delete = (param) => {
		message.dialog().then((x) => {
			ManagemenTransaksiService.delete(param).then((data) => {});
		});
	};
}
function adminsuperDataPenjualController(
	$scope,
	helperServices,
	DataPenjualService,
	message,
	$http,
	kodefikasiService,
	AuthService
) {
	$scope.kodefikasi = kodefikasiService;
	$scope.helper = helperServices;
	DataPenjualService.get().then((result) => {
		$scope.source = result;
	});

	$scope.changeStatus = (data) => {
		var text = 'Mengaktifkan Penjual';
		if (data.status) {
			var text = 'Menonaktifkan Penjual';
		}

		message.dialog(text).then((res) => {
			$http({
				method: 'Get',
				url: helperServices.url + '/user/changeStatusPenjual?userId=' + data.iduser,
				headers: AuthService.getHeader()
			}).then(
				(response) => {
					data.status = !data.status;
					message.info('Berhasil...!');
				},
				(err) => {
					message.error(err.data);
				}
			);
		});
	};
	$scope.register = function(user) {
		AuthService.registerPenjual(user).then((x) => {
			$scope.source.push(x);
		});
	};
}

function adminsuperDataPembeliController(
	$scope,
	DataPembeliService,
	message,
	helperServices,
	AuthService,
	$http,
	kodefikasiService
) {
	$scope.kodefikasi = kodefikasiService;
	DataPembeliService.get().then((result) => {
		$scope.source = result;
	});

	$scope.changeStatus = (data) => {
		var text = 'Mengaktifkan Pembeli';
		if (data.status) {
			var text = 'Menonaktifkan Pembeli';
		}

		message.dialog(text).then((res) => {
			$http({
				method: 'Get',
				url: helperServices.url + '/user/changeStatusPembeli?userId=' + data.iduser,
				headers: AuthService.getHeader()
			}).then(
				(response) => {
					data.status = !data.status;
					message.info('Berhasil...!');
				},
				(err) => {
					message.error(err.data);
				}
			);
		});
	};
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
			if (element.diantar == 'Ya') {
				element.diantar = element.pengiriman ? 'Sudah' : 'Belum';
			} else {
				element.diantar = 'Tidak Diantar';
			}
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

function adminsuperLaporanController($scope, kodefikasiService, OrderService) {
	$scope.kodefikasi = kodefikasiService;
	$scope.orderService = OrderService;
	$scope.source = [];
	OrderService.laporan().then((result) => {
		$scope.data = result;
	});

	$scope.filter = (from, to) => {
		$scope.source = $scope.data.filter((x) => new Date(x.tgl_order) >= from && new Date(x.tgl_order) <= to);
	};

	$scope.print = () => {
		window.print();
	};

	$scope.totalBayar = (source) => {
		return source.reduce((total, item) => {
			return (total += item.harga * item.jumlah);
		}, 0);
	};

	$scope.adminFee = (source) => {
		return source.reduce((total, item) => {
			return (total += item.harga * item.jumlah * (item.potongan / 100));
		}, 0);
	};
}

function adminsuperMenuUtamaController() {}
function adminsuperDataParameterController() {}
function adminsuperTambahKategoriController() {}
function adminsuperDataTransaksiController() {}
function adminsuperAnalisaController() {}
