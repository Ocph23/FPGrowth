angular.module('app.penjual.service', []).factory('PenjualOrderService', PenjualOrderService);

function PenjualOrderService($http, helperServices, AuthService, message, $q) {
	var url = helperServices.url + '/api/penjual';
	var service = {};

	service.getLastOrder = function() {
		var def = $q.defer();
		if (service.lastOrder) {
			def.resolve(service.lastOrder);
		} else {
			$http({
				method: 'Get',
				url: url + '/getlastorder',
				headers: AuthService.getHeader()
			}).then(
				(response) => {
					service.lastOrder = response.data;
					def.resolve(service.lastOrder);
				},
				(err) => {
					message.error(err.data);
					def.reject(err);
				}
			);
		}

		return def.promise;
	};

	return service;
}
