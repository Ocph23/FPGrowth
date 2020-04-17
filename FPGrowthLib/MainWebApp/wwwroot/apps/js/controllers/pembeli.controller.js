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
	.controller('pembelihomemenuController', pembelihomemenuController);

function pembeliController($scope, kodefikasiService, AuthService, ChatService) {
	$scope.kodefikasi = kodefikasiService;

	if (AuthService.userIsLogin()) {
		if (!ChatService.signalR) {
			ChatService.startSignalR();
		}
	}
}

function pembeliProfileController($scope, AuthService, $http, helperServices, StorageService, message) {
	AuthService.profile().then((profile) => {
		$scope.profile = profile;
		$scope.profile.tgl_lahir = new Date(profile.tgl_lahir);
	});

	$scope.update = (data) => {
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
			},
			(err) => {
				message.error(err);
			}
		);
	};
}

function pembeliHomeController($scope, BarangService, KategoriService, $rootScope) {
	$scope.Source = null;
	$scope.selectedKategori = null;
	BarangService.get().then((barangs) => {
		$scope.Source = barangs;
	});

	$rootScope.$on('selectedKategori', function(evt, data) {
		$scope.selectedKategori = data;
		$scope.searchText = data.kode_kategori;
	});
	$rootScope.$on('cariEvent', function(evt, data) {
		$scope.searchText = data;
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

	$scope.chat = (userid) => {
		ChatService.chatWith(userid);
	};
}

function pembeliprofilpenjualController() {}

function pembelikeranjangController() {}

function pembeliorderController($scope, $stateParams, AuthService, ManagemenTransaksiService, message, PembeliService) {
	$scope.model = {};
	AuthService.profile().then((profile) => {
		$scope.profile = profile;

		ManagemenTransaksiService.getActive().then((man) => {
			$scope.model = {
				idpembeli: profile.idpembeli,
				idmanajemen: man.idmanajemen,
				alamatpengiriman: profile.alamat,
				no_tlp: profile.no_tlp,
				tgl_order: new Date(),
				data: $stateParams.data
			};
		});
	});

	$scope.order = function(model) {
		PembeliService.createOrder(model).then((result) => {
			message.info('Order Berhasil Dibuat');
			PembeliCartService.clear();
		});
	};
}

function pembelitagihanController() {}

function pembelidaftartagihanController($scope, PembeliService, AuthService, BarangService, kodefikasiService) {
	$scope.kodefikasi = kodefikasiService;
	AuthService.profile().then((profile) => {
		PembeliService.getOrders(profile.idpembeli).then((data) => {
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

	$scope.total = (source) => {
		return source.data.reduce((total, item) => {
			return total + item.jumlah * item.harga;
		}, 0);
	};

	$scope.padLeft = (number, length) => {
		return number.padLeft(length);
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
	ManagemenTransaksiService
) {
	if (!$stateParams.data) {
		$state.go('pembeli-daftartagihan');
	} else {
		AuthService.profile().then((x) => {
			$scope.profile = x;
			ManagemenTransaksiService.getActive().then((x) => {
				$scope.manajemen = x;
				$scope.order = $stateParams.data;
			});
		});
	}

	$scope.padLeft = (number, length) => {
		return number.padLeft(length);
	};

	$scope.total = (source) => {
		return source.data.reduce((total, item) => {
			return total + item.jumlah * item.harga;
		}, 0);
	};

	$scope.save = (model) => {
		var pembayaran = {
			tgl_pembayaran: new Date(),
			potongan: $scope.manajemen.potongan,
			bank: model.bank,
			idorder: $scope.order.idorder,
			status_pembayaran: 'Menunggu Verifikasi',
			data_bukti: model.data_bukti
		};

		if (model.bank == 'Bank Lain') {
			pembayaran = model.nama_bank_lain;
		}

		PembeliService.createPembayaran(pembayaran).then((result) => {
			order.pembayaran = result;
			order.status = 'Menunggu Verifikasi Pembayaran';
		});
	};
}

function pembelihomemenuController() {}

function pembelipesanController() {}

function pembelidiskusiController() {}
