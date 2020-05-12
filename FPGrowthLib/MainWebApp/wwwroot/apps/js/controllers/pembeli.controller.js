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
	.controller('pembelidiskusiController', pembelidiskusiController)
	.controller('pembeliProfileController', pembeliProfileController)
	.controller('pembeliProfilePenjualController', pembeliProfilePenjualController)
	.controller('pembelihomemenuController', pembelihomemenuController);

function pembeliController($scope, helperServices, kodefikasiService, AuthService, ChatService, PembeliCartService) {
	$scope.helper = helperServices;
	$scope.kodefikasi = kodefikasiService;

	if (AuthService.userIsLogin()) {
		if (!ChatService.signalR) {
			ChatService.startSignalR();
		}
	}

	$scope.chartStatus = () => PembeliCartService.chartStatus();
}

function pembeliProfileController($scope, helperServices, AuthService, $http, helperServices, StorageService, message) {
	$scope.helper = helperServices;
	AuthService.profile().then((profile) => {
		$scope.profile = profile;
		$scope.profile.tgl_lahir = new Date(profile.tgl_lahir);
	});

	$scope.update = (data) => {
		$scope.helper.IsBusy = true;
		$http({
			url: helperServices.url + '/api/pembeli',
			method: 'Put',
			headers: AuthService.getHeader(),
			data: data
		}).then(
			(result) => {
				StorageService.addObject('profile', result.data);
				result.data.tgl_lahir = new Date(result.data.tgl_lahir);
				$scope.profile = result.data;
				message.info('Profile berhasil Diubah');
				$scope.helper.IsBusy = false;
			},
			(err) => {
				message.error(err);
				$scope.helper.IsBusy = false;
			}
		);
	};
}

function pembeliHomeController($scope, helperServices, BarangService, KategoriService, $rootScope) {
	$scope.helper = helperServices;
	$scope.Source = null;
	$scope.selectedKategori = null;
	BarangService.get().then((barangs) => {
		$scope.Source = barangs;
		$scope.source = angular.copy($scope.Source);
	});

	$scope.getSource = () => {
		if ($scope.Source && $scope.selectedKategori) {
			return $scope.Source.filter((x) => x.idkategori == $scope.selectedKategori.idkategori);
		}

		return $scope.Source;
	};

	$rootScope.$on('selectedKategori', function(evt, data) {
		$scope.$apply((x) => {
			$scope.selectedKategori = data;
			$scope.source = $scope.Source.filter((x) => x.idkategori == data.idkategori);
			$scope.searchText = data.nama_kategori;
		});
	});

	$rootScope.$on('cariEvent', function(evt, data) {
		$scope.searchText = data;
		if (!data) {
			$scope.selectedKategori = null;
		} else {
			$scope.source = angular.copy($scope.Source);
		}
	});
}

function pembeliDetailProdukController(
	$scope,
	$stateParams,
	AuthService,
	BarangService,
	PembeliCartService,
	CommentService,
	message,
	kodefikasiService,
	ChatService
) {
	$scope.kodefikasi = kodefikasiService;
	$scope.message = '';
	BarangService.getById($stateParams.id).then((result) => {
		$scope.model = result;
		BarangService.getRecomendation($stateParams.id).then((recomendations) => {
			$scope.recomendations = recomendations;
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

	$scope.chat = (userid) => {
		ChatService.chatWith(userid);
	};
}

function pembeliprofilpenjualController() {}

function pembelikeranjangController() {}

function pembeliorderController(
	$scope,
	$state,
	$stateParams,
	AuthService,
	ManagemenTransaksiService,
	message,
	PembeliCartService,
	OrderService,
	PembeliService
) {
	$scope.model = {};
	if (!$stateParams.data) {
		$state.go('pembeli-keranjang');
	} else {
		AuthService.profile().then((profile) => {
			$scope.profile = profile;
			ManagemenTransaksiService.getActive().then((man) => {
				var model = {
					idpembeli: profile.idpembeli,
					idmanajemen: man.idmanajemen,
					alamatpengiriman: profile.alamat,
					no_tlp: profile.no_tlp,
					tgl_order: new Date(),
					management: man,
					data: $stateParams.data
				};

				model.total = OrderService.total(model.data);
				model.jumlah = OrderService.jumlah(model);
				model.diantar = model.jumlah >= man.bts_jumlah_pengiriman ? 'Diantar' : 'Tidak Diantar';
				$scope.model = model;
			});
		});
	}

	$scope.order = function(model) {
		PembeliService.createOrder(model).then((result) => {
			message.info('Order Berhasil Dibuat');
			PembeliCartService.clear();
			$state.go('pembeli-daftartagihan');
		});
	};
}

function pembelitagihanController() {}

function pembelidaftartagihanController(
	$scope,
	helperServices,
	PembeliService,
	AuthService,
	BarangService,
	kodefikasiService,
	OrderService
) {
	$scope.orderService = OrderService;
	$scope.helper = helperServices;
	$scope.kodefikasi = kodefikasiService;
	AuthService.profile().then((profile) => {
		PembeliService.getOrders(profile.idpembeli).then((data) => {
			data.forEach((element) => {
				OrderService.diantar(element);
			});
			$scope.Source = data;
		});
	});

	$scope.setBarang = (items) => {
		BarangService.get().then((data) => {
			items.data.forEach((element) => {
				if (!element.barang) {
					element.barang = data.find((x) => x.idbarang == element.idbarang);
				}
			});
		});
	};

	$scope.FilterPembayaran = function(item) {
		return item.pembayaran ? true : false;
	};
	$scope.FilterNotPembayaran = function(item) {
		return item.pembayaran ? false : true;
	};
}

function pembelikonfirbayarController(
	$scope,
	$stateParams,
	$state,
	AuthService,
	PembeliService,
	ManagemenTransaksiService,
	helperServices,
	kodefikasiService,
	OrderService
) {
	$scope.orderService = OrderService;
	$scope.kodefikasi = kodefikasiService;
	$scope.helper = helperServices;

	if (!$stateParams.data) {
		$state.go('pembeli-daftartagihan');
	} else {
		AuthService.profile().then((x) => {
			$scope.profile = x;
			$scope.order = $stateParams.data;
		});
	}

	$scope.save = (model) => {
		$scope.helper.IsBusy = true;
		var pembayaran = {
			tgl_pembayaran: new Date(),
			potongan: $scope.order.manajemen.potongan,
			bank: model.bank,
			idorder: $scope.order.idorder,
			status_pembayaran: 'Menunggu Verifikasi',
			data_bukti: model.data_bukti
		};

		if (model.bank == 'Bank Lain') {
			pembayaran = model.nama_bank_lain;
		}

		PembeliService.createPembayaran(pembayaran).then((result) => {
			$scope.order.pembayaran = result;
			$scope.order.status = 'Menunggu Verifikasi Pembayaran';
			$scope.helper.IsBusy = false;
			$state.go('pembeli-daftartagihan');
		});
	};
}

function pembeliProfilePenjualController(
	$scope,
	PenjualOrderService,
	DataPenjualService,
	$stateParams,
	BarangService,
	kodefikasiService
) {
	$scope.kodefikasi = kodefikasiService;

	DataPenjualService.getById($stateParams.id).then((x) => {
		$scope.model = x;
		BarangService.getByPenjualId(x.idpenjual).then((source) => {
			$scope.source = source;
		});
	});
}

function pembelihomemenuController() {}

function pembelipesanController() {}

function pembelidiskusiController() {}
