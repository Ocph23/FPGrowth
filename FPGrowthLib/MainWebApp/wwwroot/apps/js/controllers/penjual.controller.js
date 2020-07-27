angular
	.module('penjual.controller', [])
	.controller('penjualController', penjualController)
	.controller('penjualpHomeController', penjualpHomeController)
	.controller('penjualprofilpController', penjualprofilpController)
	.controller('penjualeditprofilController', penjualeditprofilController)
	.controller('penjualdaftarbarangController', penjualdaftarbarangController)
	.controller('penjualdetailbarangController', penjualdetailbarangController)
	.controller('penjualtambahbarangController', penjualtambahbarangController)
	.controller('penjualeditbarangController', penjualeditbarangController)
	.controller('penjualdaftarorderController', penjualdaftarorderController)
	.controller('penjualLaporanController', penjualLaporanController)
	.controller('penjualdaftarpesananController', penjualdaftarpesananController);

function penjualController($scope, AuthService, $state, ChatService) {
	AuthService.Init([ 'penjual' ]);
	if (AuthService.userIsLogin()) {
		if (!ChatService.signalR) {
			ChatService.startSignalR();
		}
	} else {
		$state.go('login');
	}
}

function penjualpHomeController($scope, AuthService, BarangService, PenjualOrderService, kodefikasiService) {
	$scope.kodefikasi = kodefikasiService;
	AuthService.profile().then((profile) => {
		$scope.profile = profile;
		BarangService.getByPenjualId(profile.idpenjual).then((source) => {
			$scope.source = source;
			PenjualOrderService.getLastOrder().then((lastOrder) => {
				$scope.lastOrder = lastOrder;
			});
		});
	});
}

function penjualprofilpController($scope, $http, helperServices, AuthService, message, $state, StorageService) {
	$scope.helper = helperServices;
	AuthService.profile().then((x) => {
		$scope.profile = x;
	});

	$scope.update = (data) => {
		$http({
			url: helperServices.url + '/api/penjual',
			method: 'Put',
			headers: AuthService.getHeader(),
			data: data
		}).then(
			(result) => {
				StorageService.addObject('profile', result.data);
				$scope.profile = result.data;
				message.info('Profile berhasil Diubah');
				$state.go('penjual-home');
			},
			(err) => {
				message.error(err);
			}
		);
	};
}

function penjualeditprofilController($scope) {
	$scope.model = {};
}

function penjualdaftarbarangController($scope, BarangService, KategoriService, kodefikasiService, message) {
	$scope.kodefikasi = kodefikasiService;
	$scope.selectedKategori = { idkategori: 0 };
	BarangService.get().then((data) => {
		$scope.Items = data;
		KategoriService.get().then((kategories) => {
			$scope.kategories = kategories;
		});
	});

	$scope.padLeft = (number, length) => {
		return number.padLeft(length);
	};

	$scope.filterKategori = (param) => {
		BarangService.get().then((data) => {
			$scope.Items = data.filter((res) => res.idkategori == param.idkategori);
		});
	};

	$scope.delete = (id) => {
		message.dialog('Hapus Barang').then((x) => {
			BarangService.delete(id).then((result) => {
				message.info('Barang Berhasil Dihapus');
			});
		});
	};
}

function penjualdetailbarangController(
	$scope,
	$stateParams,
	AuthService,
	BarangService,
	PembeliCartService,
	CommentService,
	$state,
	$rootScope
) {
	$scope.message = '';
	BarangService.getById($stateParams.id).then((result) => {
		$scope.model = result;
		if (!result.comments)
			CommentService.get($stateParams.id).then((comments) => {
				$scope.model.comments = comments;
			});

		if (AuthService.userIsLogin()) {
			AuthService.profile().then((profile) => {
				$scope.profile = profile;
			});
		}
	});

	$scope.addToCart = (item) => {
		PembeliCartService.add(item);
	};

	$scope.addComment = (message) => {
		CommentService.post({
			idbarang: $stateParams.id,
			tgl_komentar: new Date(),
			iduser: $scope.profile.iduser,
			nama: $scope.profile.nama,
			isi_komentar: message
		}).then(
			(result) => {
				if (!$scope.model.comments) $scope.model.comments = [];
				$scope.model.comments.push(result);
				$scope.message = '';
			},
			(err) => {
				message.error(err);
			}
		);
	};

	$scope.padLeft = (number, length) => {
		return number.padLeft(length);
	};
}

function penjualtambahbarangController($scope, AuthService, message, KategoriService, BarangService) {
	$scope.title = 'Tambah Barang';
	$scope.model = {};
	AuthService.profile().then((x) => {
		$scope.profile = x;
		KategoriService.get().then((kategories) => {
			$scope.Kategories = kategories;
		});
	});

	$scope.simpan = function(model) {
		model.idpenjual = $scope.profile.idpenjual;
		model.idkategori = model.kategori.idkategori;
		model.tgl_publish = new Date();
		BarangService.post(model).then((data) => {
			message.info('Data Berhasil Disimpan');
		});

		model = {};
	};
}
function penjualeditbarangController(
	$scope,
	AuthService,
	message,
	KategoriService,
	BarangService,
	$state,
	$rootScope,
	PreviousState,
	$stateParams
) {
	$scope.title = 'Edit Barang';
	$scope.model = {};
	var id = $stateParams.id;
	var a = $state.$current.$previousState;
	AuthService.profile().then((x) => {
		$scope.profile = x;
		KategoriService.get().then((kategories) => {
			$scope.Kategories = kategories;
			BarangService.getById(id).then((data) => {
				$scope.model = data;
			});
		});
	});

	$scope.simpan = function(model) {
		model.idpenjual = $scope.profile.idpenjual;
		model.idkategori = model.kategori.idkategori;
		BarangService.put(model).then((data) => {
			message.info('Data Berhasil Disimpan');
			if (PreviousState.Name) {
				$state.go(PreviousState.Name, PreviousState.Params);
			}
		});
		model = {};
	};
}

function penjualdaftarorderController($scope, AuthService, kodefikasiService, OrderService, helperServices) {
	$scope.kodefikasi = kodefikasiService;
	$scope.orderService = OrderService;
	AuthService.profile().then((x) => {
		$scope.profile = x;
		OrderService.getOrderByIdPenjual($scope.profile.idpenjual).then((orders) => {
			var entries = helperServices.groupBy(orders, (x) => x.idorder);
			var orders = [];
			entries.forEach((values, key) => {
				var item = values[0];
				if (item) {
					var model = {
						idorder: item.idorder,
						kodeorder: kodefikasiService.order(item.idorder, item.tgl_order),
						idpembayaran: kodefikasiService.pembayaran(item.idpembayaran, item.tgl_pembayaran),
						tgl_pembayaran: item.tgl_pembayaran,
						tgl_pengiriman: item.tgl_pengiriman,
						status_pembayaran: item.status_pembayaran,
						bukti_pembayaran: item.bukti_pembayaran,
						idpengiriman: item.idpengiriman,
						kodepengiriman: kodefikasiService.pengiriman(item.idpengiriman, item.tgl_pengiriman),
						nama_pembeli: item.nama_pembeli,
						no_tlp: item.no_tlp,
						alamat: item.alamat,
						bukti_pengiriman: item.bukti_pengiriman,
						jumlah_barang: jumlahBarang(values),
						status_pengantaran: item.status_pengantaran,
						alamatpengiriman: item.alamatpengiriman,
						bts_jumlah_pengiriman: item.bts_jumlah_pengiriman
					};

					model.data = [];
					values.forEach((da) => {
						var data = {
							idbarang: kodefikasiService.barang(da.idbarang, da.tgl_publish),
							harga: da.harga,
							jumlah: da.jumlah,
							nama_barang: da.nama_barang,
							idpenjual: da.idpenjual,
							tgl_publish: da.tgl_publish,
							potongan: da.potongan
						};
						model.data.push(data);
					});

					model.total = OrderService.total(model.data);
					model.diantar =
						model.jumlah_barang >= item.bts_jumlah_pengiriman
							? item.idpengiriman ? 'Sudah' : 'Belum'
							: 'Tidak Diantar';

					if (model.idpembayaran) orders.push(model);
				}
			});

			$scope.orders = orders.sort((x, y) => {
				return y.idorder - x.idorder;
			});
		});

		function jumlahBarang(data) {
			return data.reduce((total, item) => {
				return total + item.jumlah;
			}, 0);
		}
	});

	$scope.selectModel = (item) => {
		if (!item.tgl_pengiriman) {
			item.tgl_pengiriman = new Date();
		} else {
			item.tgl_pengiriman = new Date(item.tgl_pengiriman);
		}
		$scope.model = item;
	};

	$scope.emptyModel = () => {
		$scope.model = null;
	};

	$scope.save = (model) => {
		var data = {
			idorder: model.idorder,
			idpengiriman: model.idpengiriman,
			tgl_pengiriman: model.tgl_pengiriman,
			data_bukti_pengiriman: model.data_bukti_pengiriman,
			jumlah_barang: model.jumlah_barang,
			keterangan: model.keterangan
		};

		if (!data.idpengiriman) {
			data.idpengiriman = 0;
		}

		OrderService.createPengiriman(data).then((x) => {
			model.idpengiriman = x.idpengiriman;
			model.bukti_pengiriman = x.bukti_pengiriman;
			model.kodepengiriman = kodefikasiService.pengiriman(x.idpengiriman, x.tgl_pengiriman);
			model.diantar = 'Sudah';
			$('#pengirimanModalCenter').modal('hide');
			$('body').removeClass('modal-open');
			$('.modal-backdrop').remove();
		});
		$scope.model = null;
	};
}

function penjualdaftarpesananController() {}

function penjualLaporanController($scope, AuthService, kodefikasiService, OrderService) {
	$scope.kodefikasi = kodefikasiService;
	$scope.orderService = OrderService;
	$scope.source = [];

	AuthService.profile().then((x) => {
		$scope.profile = x;
		OrderService.laporan().then((result) => {
			$scope.data = result.filter((z) => (z.idpenjual = $scope.profile.idpenjual));
		});
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
