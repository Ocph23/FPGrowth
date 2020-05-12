angular
	.module('account.controller', [])
	.controller('RegisterPembeliController', RegisterPembeliController)
	.controller('RegisterPenjualController', RegisterPenjualController)
	.controller('inboxController', inboxController)
	.controller('confirmEmailController', confirmEmailController)
	.controller('LoginController', LoginController);

function LoginController($scope, $state, AuthService, helperServices) {
	$scope.helper = helperServices;
	$scope.login = function(user) {
		$scope.helper.IsBusy = true;
		AuthService.login(user).then((x) => {
			$scope.helper.IsBusy = false;
			$state.go(x.role + '-home');
		});
	};
}

function RegisterPembeliController($scope, helperServices, AuthService) {
	$scope.helper = helperServices;
	$scope.register = function(user) {
		$scope.helper.IsBusy = true;
		AuthService.registerPembeli(user).then((x) => {
			$scope.helper.IsBusy = false;
		});
	};
}

function RegisterPenjualController($scope, $state, AuthService) {
	$scope.register = function(user) {
		AuthService.registerPenjual(user).then((x) => {});
	};
}

function inboxController($scope, AuthService, ChatService, helperServices) {
	AuthService.profile().then((profile) => {
		$scope.profile = profile;
		$scope.channels = [];

		ChatService.get().then((result) => {
			var users = helperServices.groupBy(
				result.filter((p) => p.idpengirim != profile.iduser),
				(x) => x.idpengirim
			);
			users.forEach((values, key) => {
				var item = values[0];
				var channel = { channelId: key, role: item.role, userName: item.pengirim, avatar: item.avatar };
				$scope.channels.push(channel);
			});

			$scope.channels.forEach((item) => {
				item.chats = result.filter((x) => x.idpenerima == item.channelId || x.idpengirim == item.channelId);
			});
		});
	});

	$scope.selectChannel = (channel) => {
		$scope.channel = channel;
	};

	$scope.send = (channel, message) => {
		if (message) {
			var data = {
				tgl_pesan: new Date(),
				avatar: $scope.profile.photo,
				idpengirim: $scope.profile.iduser,
				pengirim: $scope.profile.nama,
				idpenerima: channel.channelId,
				isi_pesan: message
			};

			ChatService.post(data).then((result) => {
				channel.chats.push(data);
				if (ChatService.signalR) {
					ChatService.sendChat(channel.channelId.toString(), result);
				}
				setTimeout((x) => {
					$scope.$apply((x) => {
						$scope.message = ' ';
					});
				}, 100);
			});
		}
	};
}

function confirmEmailController($scope, $stateParams, $http, AuthService, helperServices) {
	var code = $stateParams.code;
	var id = $stateParams.id;

	$scope.isSuccess = false;

	$http({
		method: 'get',
		url: helperServices.url + '/user/verifyemail?userid=' + id + '&token=' + code,
		headers: AuthService.getHeader()
	}).then(
		(result) => {
			$scope.isSuccess = true;
		},
		(err) => {
			$scope.isError = true;
		}
	);
}
