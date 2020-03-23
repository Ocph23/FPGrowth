angular.module('data.service', []).factory('KategoriService', KategoriService);

function KategoriService($http, $, message, helperService, AuthService) {
	var url = helperService.url + '/api/kategori';
	var service = { Items: [] };

	service.get = () => {
		var def = $.defer();
		if (service.instance) {
			def.resolve(service.Items);
		} else {
			$http({
				methode: 'Get',
				url: url,
				headers: AuthService.getheader()
			}).then(
				(response) => {
					service.instance = true;
					service.Items = response.data;
					def.resolve(service.Items);
				},
				(err) => {
					message.error(err.data);
					def.reject(err);
				}
			);
		}

		return def.promise;
	};

	service.getById = (id) => {
		var def = $.defer();
		if (service.instance) {
			var data = service.Items.find((x) => x.idkategori == id);
			def.resolve(data);
		} else {
			$http({
				methode: 'Get',
				url: url + '/' + id,
				headers: AuthService.getheader()
			}).then(
				(response) => {
					service.Items.push(response.data);
					def.resolve(response.data);
				},
				(err) => {
					message.error(err.data);
					def.reject(err);
				}
			);
		}

		return def.promise;
	};

	service.post = (param) => {
		var def = $.defer();
		$http({
			methode: 'Post',
			url: url,
			headers: AuthService.getheader(),
			data: param
		}).then(
			(response) => {
				service.Items.push(response.data);
				def.resolve(response.data);
			},
			(err) => {
				message.error(err.data);
				def.reject(err);
			}
		);

		return def.promise;
	};

	service.put = (param) => {
		var def = $.defer();
		$http({
			methode: 'Put',
			url: url,
			headers: AuthService.getheader(),
			data: param
		}).then(
			(response) => {
				def.resolve(response.data);
			},
			(err) => {
				message.error(err.data);
				def.reject(err);
			}
		);
		return def.promise;
	};

	service.delete = (param) => {
		var def = $.defer();
		$http({
			methode: 'Delete',
			url: url + '/' + param.idkategori,
			headers: AuthService.getheader(),
			data: param
		}).then(
			(response) => {
				var index = service.Items.indexOf(param);
				service.Items.splice(index, 1);
				def.resolve(response.data);
			},
			(err) => {
				message.error(err.data);
				def.reject(err);
			}
		);
		return def.promise;
	};

	return service;
}
