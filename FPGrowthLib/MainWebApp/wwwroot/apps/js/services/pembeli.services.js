angular
	.module('app.pembeli.service', [])
	.factory('PembeliService', PembeliService)
	.factory('PembeliCartService', PembeliCartService);

function PembeliCartService($http, $q, message, helperServices, AuthService, StorageService) {
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
		clear: clear
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

	function saveCart() {
		StorageService.addObject('cart', service.data);
	}
	function clear() {
		StorageService.remove('cart');
		service.data = [];
	}
}

function PembeliService($http, $state, $q, message, AuthService, helperServices) {
	var controller = helperServices.url + 'api/barang';
	var service = {};
	service.orders = [];
	service.data = [];

	return {
		getBarang: getBarang,
		createOrder: createOrder,
		getOrder: getOrder,
		getOrders: getOrders,
		createPembayaran: createPembayaran
	};

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

	function createOrder(data) {
		var def = $q.defer();
		$http({
			method: 'post',
			url: helperServices.url + '/api/Pembelian/createorder',
			headers: AuthService.getHeader(),
			data: data
		}).then(
			(response) => {
				service.orders.push(response.data);
				def.resolve(response.data);
			},
			(err) => {
				message.error(err);
				def.reject(err);
			}
		);
		return def.promise;
	}

	function createPembayaran(data) {
		var def = $q.defer();
		$http({
			method: 'post',
			url: helperServices.url + '/api/Pembelian/createPembayaran',
			headers: AuthService.getHeader(),
			data: data
		}).then(
			(response) => {
				def.resolve(response.data);
			},
			(err) => {
				message.error(err);
				def.reject(err);
			}
		);
		return def.promise;
	}

	function getOrders(idpembeli) {
		var def = $q.defer();
		$http({
			method: 'get',
			url: helperServices.url + '/api/pembelian/GetOrderByIdPembeli/' + idpembeli,
			headers: AuthService.getHeader()
		}).then(
			(response) => {
				service.orders = response.data;
				def.resolve(service.orders);
			},
			(err) => {
				message.error(err);
				def.reject(err);
			}
		);
		return def.promise;
	}

	function getOrder(idpembeli, idorder) {
		var def = $q.defer();
		getOrders(idpembeli).then(orders).then((orders) => {
			var order = orders.find((x) => x.idorder == idorder);
			def.resolve(order);
		});
		return def.promise;
	}
}
