angular.module('app.algoritma.service', []).factory('AlgoritmaService', AlgoritmaService);

function AlgoritmaService($http, $q, message, helperServices, AuthService, StorageService) {
	var service = {};`4567456789`

	service.get = function(id) {
		var def = $q.defer();
		$http({
			method: 'Get',
			url: helperServices.url + '/api/Rekomendasi/1',
			headers: AuthService.getHeader()
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
