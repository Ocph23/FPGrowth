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
		AuthService.getUsers().then((users) => {
			if (profile.role == 'pembeli') {
				users.filter((p) => p.iduser != profile.iduser && p.role != 'pembeli').forEach((user) => {
					var channel = {
						channelId: user.iduser,
						role: user.role,
						userName: user.nama,
						email: user.email,
						avatar: user.photo
					};
					$scope.channels.push(channel);
				});
			} else
				users.filter((p) => p.iduser != profile.iduser).forEach((user) => {
					var channel = {
						channelId: user.iduser,
						role: user.role,
						userName: user.nama,
						email: user.email,
						avatar: user.photo
					};
					$scope.channels.push(channel);
				});

			ChatService.get().then((result) => {
				$scope.channels.forEach((item) => {
					item.chats = result.filter((x) => x.idpenerima == item.channelId || x.idpengirim == item.channelId);
				});

				$scope.users = [];

				var datas = helperServices.groupBy(
					$scope.channels.sort(function(x, y) {
						return x.channelId - y.channelId;
					}),
					(x) => x.role
				);
				datas.forEach((values, key) => {
					$scope.users.push({ key: key, values: values });
				});
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

function confirmEmailController(
	$scope,
	$stateParams,
	$state,
	$http,
	AuthService,
	helperServices,
	StorageService,
	message
) {
	$scope.model = {};
	var user = $stateParams.user;
	$scope.isBusy = false;
	$scope.isSended = false;

	if (!user) {
		$state.go('login');
	}

	$scope.resendCode = () => {
		$scope.model = {};
		$scope.isSended = true;

		$http({
			method: 'get',
			url: helperServices.url + '/user/resendverify?userid=' + user.iduser,
			headers: AuthService.getHeader()
		}).then(
			(result) => {
				$scope.isSended = false;
				message.info('Periksa Handphone Anda');
			},
			(err) => {
				$scope.isSended = false;
				message.error(err.data);
			}
		);
	};

	$scope.verifikasi = (model) => {
		$scope.isBusy = true;
		$http({
			method: 'get',
			url: helperServices.url + '/user/verifyemail?userid=' + user.iduser + '&token=' + model.kode,
			headers: AuthService.getHeader()
		}).then(
			(res) => {
				$scope.isBusy = false;
				user.status = true;
				StorageService.addObject('user', user);
				$state.go(user.role + '-home');
			},
			(err) => {
				$scope.isBusy = false;
				message.error(err.data);
			}
		);
	};
}
