angular
	.module('app.pembeli.service', [])
	.factory('PembeliService', PembeliService)
	.factory('PembeliCartService', PembeliCartService);

function PembeliCartService($http, $state, $q, message, AuthService, StorageService) {
	var service = {};
	service.data = StorageService.getObject('cart');
	if (!service.data) {
		service.data = [];
	}

	return {
		get: get,
		add: add,
		delete: remove,
		saveCart: saveCart,
		order: order
	};

	function get() {
		return service.data;
	}

	function add(item) {
		var data = service.data.find((x) => x.idbarang == item.idbarang);
		if (data) {
			data.jumlah++;
		} else {
			item.jumlah = 1;
			service.data.push(item);
		}
		StorageService.addObject('cart', service.data);
	}

	function remove(item) {
		var data = service.data.find((x) => x.idbarang == item.idbarang);
		if (data) {
			var index = service.data.indexOf(data);
			service.data.splice(index, 1);
			StorageService.addObject('cart', service.data);
		}
	}

	function order(data) {}

	function saveCart() {
		StorageService.addObject('cart', service.data);
	}
}

function PembeliService($http, $state, $q, message, AuthService) {
	var controller = helperService.url + 'api/barang';
	var service = {};

	service.data = [];

	return { getBarang: getBarang };

	function getBarang() {
		var defer = $q.defer();

		if (service.instance) {
			defer.resolve(service.data);
		} else {
			$http({
				method: 'Get',
				url: controller,
				headers: AuthService.getHeader()
			}).then(
				(result) => {
					service.instance = true;
					service.data = result.data;
					defer.resolve(service.data);
				},
				(err) => {
					message.error(err);
				}
			);
		}

		return defer.promise;
	}
}
