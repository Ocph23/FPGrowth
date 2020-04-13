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

function penjualController($scope, AuthService) {
	//AuthService.Init([ 'penjual' ]);
}

function penjualpHomeController($scope, AuthService) {
	AuthService.profile().then((profile) => {
		$scope.profile = profile;
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

function penjualdaftarbarangController($scope, BarangService) {
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

function penjualdaftarorderController() {}

function penjualdaftarpesananController() {}
