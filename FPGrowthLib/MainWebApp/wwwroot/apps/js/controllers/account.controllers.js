angular
	.module('account.controller', [])
	.controller('RegisterPembeliController', RegisterPembeliController)
	.controller('RegisterPenjualController', RegisterPenjualController)
	.controller('LoginController', LoginController);

function LoginController($scope, $state, AuthService) {
	$scope.login = function(user) {
		AuthService.login(user).then((x) => {
			$state.go(x.role + '-home');
		});
	};
}

function RegisterPembeliController($scope, $state, AuthService) {
	$scope.register = function(user) {
		AuthService.registerPembeli(user).then((x) => {});
	};
}

function RegisterPenjualController($scope, $state, AuthService) {
	$scope.register = function(user) {
		AuthService.registerPenjual(user).then((x) => {});
	};
}
