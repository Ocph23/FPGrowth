angular
	.module('app', [ 'swangular', 'app.router', 'app.conponent', 'app.controller', 'app.service' ])
	.controller('homeController', homeController);

function homeController($scope, AuthService) {
	$scope.logOff = function() {
		AuthService.logOff();
	};
}
