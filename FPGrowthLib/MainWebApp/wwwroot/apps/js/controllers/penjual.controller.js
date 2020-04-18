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

function penjualprofilpController($scope, $http, helperServices, AuthService, message, StorageService) {
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

function penjualdaftarbarangController($scope, BarangService, kodefikasiService) {
	$scope.kodefikasi = kodefikasiService;
	BarangService.get().then((data) => {
		$scope.Items = data;
	});

	$scope.padLeft = (number, length) => {
		return number.padLeft(length);
	};
}

function penjualdetailbarangController(
	$scope,
	$stateParams,
	AuthService,
	BarangService,
	PembeliCartService,
	CommentService,
	message
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
function penjualeditbarangController($scope, AuthService, message, KategoriService, BarangService, $stateParams) {
	$scope.title = 'Edit Barang';
	$scope.model = {};
	var id = $stateParams.id;
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
			$scope.orders = [];
			entries.forEach((values, key) => {
				var item = values[0];
				if (item) {
					var model = {
						idorder: item.idorder,
						kodeorder: kodefikasiService.order(item.idorder, item.tgl_order),
						idpembayaran: kodefikasiService.pembayaran(item.idpembayaran, item.tgl_pembayaran),
						status_pembayaran: item.status_pembayaran,
						bukti_pembayaran: item.bukti_pembayaran,
						idpengiriman: item.idpengiriman,
						kodepengiriman: kodefikasiService.pengiriman(item.idpengiriman, item.tgl_pengiriman),
						nama_pembeli: item.nama_pembeli,
						no_tlp: item.no_tlp,
						alamat: item.alamat,
						bukti_pengiriman: item.bukti_pengiriman,
						jumlah_barang: item.jumlah_barang,
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
						model.total >= item.bts_jumlah_pengiriman ? (item.idpengiriman ? 'Sudah ' : 'Belum') : 'Tidak';
					$scope.orders.push(model);
				}
			});
		});
	});

	$scope.selectModel = (item) => {
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
			model.kodepengiriman = kodefikasiService.pengiriman(x.idpengiriman, x.tgl_pengiriman);
		});
		$scope.model = null;
	};
}

function penjualdaftarpesananController() {}
