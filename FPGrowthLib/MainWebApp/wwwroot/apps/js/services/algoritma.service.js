angular.module('app.algoritma.service', []).factory('AlgoritmaService', AlgoritmaService);

function AlgoritmaService($http, $q, message, helperServices, AuthService, StorageService) {
	var service = {};
	`4567456789`;

	service.get = function(param) {
		var def = $q.defer();

		param.idpenjual = 1;

		$http({
			method: 'Post',
			url: helperServices.url + '/api/Rekomendasi/byalgoritma',
			headers: AuthService.getHeader(),
			data: param
		}).then(
			(response) => {
				service.instance = true;
				service.Items = response.data;
				def.resolve(service.Items);
			},
			(err) => {
				message.error(err);
				def.reject(err);
			}
		);

		return def.promise;
	};

	return service;
}
