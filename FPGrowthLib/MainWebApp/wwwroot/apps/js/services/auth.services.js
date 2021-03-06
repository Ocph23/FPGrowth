angular.module('auth.service', []).factory('AuthService', AuthService);

function AuthService($http, $q, StorageService, $state, helperServices, message) {
	var controller = '/user';
	var service = {};

	return {
		Init: InitLoad,
		login: login,
		logOff: logoff,
		userIsLogin: userIsLogin,
		getUserName: getUserName,
		userIsLogin: userIsLogin,
		userInRole: userInRole,
		getHeader: getHeader,
		url: service.url,
		profile: profile,
		registerPembeli: registerPembeli,
		registerPenjual: registerPenjual,
		getToken: getToken,
		updatePhotoProfile: updatePhotoProfile,
		getUsers: getUsers
	};

	function getUsers() {
		var def = $q.defer();
		$http({
			method: 'get',
			url: helperServices.url + controller + '/getusers',
			headers: getHeader()
		}).then(
			(res) => {
				var result = res.data.map((x) => {
					x.nama =
						!x.nama_pembeli && !x.nama_penjual
							? (x.nama = 'admin')
							: x.nama_pembeli ? x.nama_pembeli : x.nama_penjual;
					return x;
				});
				def.resolve(result);
			},
			(err) => {
				helperServices.IsBusy = false;
				message.error(err);
				def.reject();
			}
		);

		return def.promise;
	}

	function updatePhotoProfile(userid, data) {
		var def = $q.defer();
		$http({
			method: 'post',
			url: helperServices.url + controller + '/photoprofile/' + userid,
			headers: getHeader(),
			data: data
		}).then(
			(res) => {
				StorageService.addObject('user', res.data);
				def.resolve(res.data);
			},
			(err) => {
				helperServices.IsBusy = false;
				message.error(err);
				def.reject();
			}
		);
		return def.promise;
	}

	function InitLoad(params) {
		var isFound = false;
		params.forEach((x) => {
			if (userInRole(x)) isFound = true;
		});

		if (!isFound) $state.go('login');
		else return isFound;
	}

	function profile() {
		var def = $q.defer();
		var result = StorageService.getObject('profile');
		if (result) {
			def.resolve(result);
		} else {
			$http({
				method: 'get',
				url: helperServices.url + controller + '/profile',
				headers: getHeader()
			}).then(
				(res) => {
					StorageService.addObject('profile', res.data);
					def.resolve(res.data);
				},
				(err) => {
					helperServices.IsBusy = false;
					message.error(err);
					def.reject();
				}
			);
		}

		return def.promise;
	}

	function login(user) {
		var def = $q.defer();
		$http({
			method: 'post',
			url: helperServices.url + controller + '/login',
			headers: getHeader(),
			data: user
		}).then(
			(res) => {
				if (!res.data.status) {
					$state.go('confirmemail', { user: res.data });
				} else {
					StorageService.addObject('user', res.data);
					def.resolve(res.data);
				}
			},
			(err) => {
				helperServices.IsBusy = false;
				if ((err.status = 401 && err.data)) {
					$state.go('confirmemail');
				} else {
					message.error(err);
					def.reject();
				}
			}
		);
		return def.promise;
	}

	function registerPenjual(data) {
		var def = $q.defer();
		$http({
			method: 'post',
			url: helperServices.url + controller + '/RegisterPenjual',
			headers: getHeader(),
			data: data
		}).then(
			(res) => {
				message.info('Registrasi Berhasil, Periksa Handphone Anda Untuk Verifikasi Akun');
				def.resolve(res.data);
			},
			(err) => {
				helperServices.IsBusy = false;
				message.error(err);
				def.reject();
			}
		);
		return def.promise;
	}

	function registerPembeli(data) {
		var def = $q.defer();
		$http({
			method: 'post',
			url: helperServices.url + controller + '/RegisterPembeli',
			headers: getHeader(),
			data: data
		}).then(
			(res) => {
				message.info('Registrasi Berhasil, Periksa Handphone Anda Untuk Verifikasi Akun');
				def.resolve(res.data);
			},
			(err) => {
				helperServices.IsBusy = false;
				message.error(err);
				def.reject();
			}
		);
		return def.promise;
	}

	function getHeader() {
		try {
			if (userIsLogin()) {
				var token = getToken();
				return {
					'Content-Type': 'application/json',
					Authorization: 'Bearer ' + token
				};
			}
			throw new Error('Not Found Token');
		} catch (err) {
			return {
				'Content-Type': 'application/json'
			};
		}
	}

	function logoff() {
		StorageService.clear();
		$state.go('login');
	}

	function getUserName() {
		if (userIsLogin) {
			var result = StorageService.getObject('user');
			return result.Username;
		}
	}

	function getToken() {
		var result = StorageService.getObject('user');
		return result.token;
	}

	function userIsLogin() {
		var result = StorageService.getObject('user');
		if (result) {
			return true;
		}
		return false;
	}

	function userInRole(role) {
		var result = StorageService.getObject('user');
		if (result && result.role == role) {
			return true;
		}
		return false;
	}
}
