angular
	.module('data.service', [])
	.factory('BarangService', BarangServices)
	.factory('ParameterService', ParameterService)
	.factory('CommentService', CommentServices)
	.factory('KategoriService', KategoriServices)
	.factory('OrderService', OrderService)
	.factory('DataPembeliService', DataPembeliService)
	.factory('DataPenjualService', DataPenjualService)
	.factory('ManagemenTransaksiService', ManagemenTransaksiService);

function KategoriServices($http, $q, message, helperServices, AuthService) {
	var url = helperServices.url + '/api/kategori';
	var service = { Items: [] };

	service.get = function() {
		var def = $q.defer();
		if (service.instance) {
			def.resolve(service.Items);
		} else {
			$http({
				method: 'Get',
				url: url,
				headers: AuthService.getHeader()
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

	service.getById = function(id) {
		var def = $q.defer();
		if (service.instance) {
			var data = service.Items.find((x) => x.idkategori == id);
			def.resolve(data);
		} else {
			$http({
				method: 'Get',
				url: url + '/' + id,
				headers: AuthService.getHeader()
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

	service.post = function(param) {
		var def = $q.defer();
		$http({
			method: 'Post',
			url: url,
			headers: AuthService.getHeader(),
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

	service.put = function(param) {
		var def = $q.defer();
		$http({
			method: 'Put',
			url: url,
			headers: AuthService.getHeader(),
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

	service.delete = function(param) {
		var def = $q.defer();
		$http({
			method: 'Delete',
			url: url + '/' + param.idkategori,
			headers: AuthService.getHeader()
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

function ManagemenTransaksiService($http, $q, message, helperServices, AuthService) {
	var url = helperServices.url + '/api/ManagementTransaksi';
	var service = { Items: [] };

	service.get = function() {
		var def = $q.defer();
		if (service.instance) {
			def.resolve(service.Items);
		} else {
			$http({
				method: 'Get',
				url: url,
				headers: AuthService.getHeader()
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

	service.getActive = function() {
		var def = $q.defer();
		service.get().then(
			(resultData) => {
				var result = resultData.find((x) => x.status == 'true');
				def.resolve(result);
			},
			(err) => {
				def.resolve(null);
			}
		);
		return def.promise;
	};

	service.getById = function(id) {
		var def = $q.defer();
		if (service.instance) {
			var data = service.Items.find((x) => x.idmanajemen == id);
			def.resolve(data);
		} else {
			service.get().then(
				(resultData) => {
					var result = resultData.find((x) => x.idmanajemen == id);
					def.resolve(result);
				},
				(err) => {
					def.resolve(null);
				}
			);
		}

		return def.promise;
	};

	service.post = function(param) {
		var def = $q.defer();
		$http({
			method: 'Post',
			url: url,
			headers: AuthService.getHeader(),
			data: param
		}).then(
			(response) => {
				service.Items.forEach((item) => {
					item.status = false;
				});
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

	service.put = function(param) {
		var def = $q.defer();
		$http({
			method: 'Put',
			url: url,
			headers: AuthService.getHeader(),
			data: param
		}).then(
			(response) => {
				service.Items.forEach((item) => {
					if (item.idmanajemen == param.idmanajemen) {
						item.nama_bank_pembayaran = param.nama_bank_pembayaran;
						item.no_rek_pembayaran = param.no_rek_pembayaran;
						item.potongan = param.potongan;
						item.status = param.status;
					} else {
						item.status = false;
					}
				});
				def.resolve(response.data);
			},
			(err) => {
				message.error(err.data);
				def.reject(err);
			}
		);
		return def.promise;
	};

	service.delete = function(param) {
		var def = $q.defer();
		$http({
			method: 'Delete',
			url: url + '/' + param.idmanajemen,
			headers: AuthService.getHeader()
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

	service.get();

	return service;
}

function BarangServices($http, $q, message, helperServices, AuthService) {
	var url = helperServices.url + '/api/barang';
	var service = { Items: [] };

	service.get = function() {
		var def = $q.defer();
		if (service.instance) {
			def.resolve(service.Items);
		} else {
			$http({
				method: 'Get',
				url: url,
				headers: AuthService.getHeader()
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

	service.getById = function(id) {
		var def = $q.defer();
		if (service.instance) {
			var data = service.Items.find((x) => x.idbarang == id);
			def.resolve(data);
		} else {
			$http({
				method: 'Get',
				url: url + '/' + id,
				headers: AuthService.getHeader()
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

	service.post = function(param) {
		var def = $q.defer();
		$http({
			method: 'Post',
			url: url,
			headers: AuthService.getHeader(),
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

	service.put = function(param) {
		var def = $q.defer();
		$http({
			method: 'Put',
			url: url,
			headers: AuthService.getHeader(),
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

	service.delete = function(param) {
		var def = $q.defer();
		$http({
			method: 'Delete',
			url: url + '/' + param.idbarang,
			headers: AuthService.getHeader()
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

	service.getByPenjualId = function(id) {
		var def = $q.defer();
		if (service.instance) {
			var data = service.Items.filter((x) => x.idpenjual == id);
			def.resolve(data);
		} else {
			service.get().then((result) => {
				var data = service.Items.filter((x) => x.idpenjual == id);
				def.resolve(data);
			});
		}

		return def.promise;
	};

	service.getRecomendation = (id) => {
		//localhost:5001/api/rekomendasi/byidbarang/1
		var def = $q.defer();
		https: $http({
			method: 'Get',
			url: helperServices.url + '/api/rekomendasi/byidbarang/' + id,
			headers: AuthService.getHeader()
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

	return service;
}

function OrderService($http, $q, message, helperServices, AuthService, BarangService, ManagemenTransaksiService) {
	var url = helperServices.url + '/api/pembelian';
	var service = {};
	service.data = [];

	service.get = () => {
		var def = $q.defer();
		if (service.instance) {
			def.resolve(service.data);
		} else {
			$http({
				method: 'GET',
				url: url,
				headers: AuthService.getHeader()
			}).then(
				(response) => {
					service.instance = true;
					service.data = response.data;
					def.resolve(service.data);
				},
				(err) => {
					message.error(err);
					def.reject(err);
				}
			);
		}

		return def.promise;
	};

	service.getById = (id) => {
		var def = $q.defer();
		if (service.instance) {
			var item = service.data.find((x) => x.idorder == id);
			def.resolve(item);
		} else {
			$http({
				method: 'GET',
				url: url + '/' + id,
				headers: AuthService.getHeader()
			}).then(
				(response) => {
					service.data.push(response.data);
					def.resolve(response.data);
				},
				(err) => {
					message.error(err);
					def.reject(err);
				}
			);
		}

		return def.promise;
	};

	service.delete = (id) => {
		var def = $q.defer();
		$http({
			method: 'DELETE',
			url: url + '/' + id,
			headers: AuthService.getHeader()
		}).then(
			(response) => {
				var index = service.data.find((x) => idorder == id);
				service.data.splice(index, 1);
				def.resolve(response.data);
			},
			(err) => {
				message.error(err);
				def.reject(err);
			}
		);

		return def.promise;
	};

	service.verifikasiPembayaran = (id) => {
		var def = $q.defer();
		$http({
			method: 'Get',
			url: url + '/verifikasiPembayaran/' + id,
			headers: AuthService.getHeader()
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
	};

	service.setBarang = (items) => {
		BarangService.get().then((data) => {
			items.data.forEach((element) => {
				if (!element.barang) {
					element.barang = data.find((x) => x.idbarang == element.idbarang);
				}
			});
		});
	};

	service.total = (data) => {
		return data.reduce((total, item) => {
			return total + item.jumlah * item.harga;
		}, 0);
	};

	service.jumlah = (source) => {
		return source.data.reduce((total, item) => {
			return total + item.jumlah;
		}, 0);
	};

	service.diantar = async (source) => {
		if (source && !source.diantar) {
			var jumlah = service.jumlah(source);
			var man = source.management;
			if (!man) await ManagemenTransaksiService.getById(source.idmanajemen);
			if (jumlah >= man.bts_jumlah_pengiriman) {
				source.diantar = 'Ya';
			} else {
				source.diantar = 'Tidak';
			}
		}
	};

	service.padLeft = (number, length) => {
		if (number && length) return number.padLeft(length);
		else {
			return null;
		}
	};

	service.getOrderByIdPenjual = (id) => {
		//GetOrderByPenjualId
		var def = $q.defer();
		$http({
			method: 'GET',
			url: url + '/GetOrderByPenjualId/' + id,
			headers: AuthService.getHeader()
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
	};

	service.createPengiriman = (model) => {
		var def = $q.defer();
		$http({
			method: 'Post',
			url: url + '/CreatePengiriman',
			headers: AuthService.getHeader(),
			data: model
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
	};

	return service;
}

function DataPembeliService($http, $q, message, helperServices, AuthService) {
	var url = helperServices.url + '/api/pembeli';
	var service = { Items: [] };

	service.get = function() {
		var def = $q.defer();
		if (service.instance) {
			def.resolve(service.Items);
		} else {
			$http({
				method: 'Get',
				url: url,
				headers: AuthService.getHeader()
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

	service.getById = function(id) {
		var def = $q.defer();
		if (service.instance) {
			var data = service.Items.find((x) => x.idbarang == id);
			def.resolve(data);
		} else {
			$http({
				method: 'Get',
				url: url + '/' + id,
				headers: AuthService.getHeader()
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

	service.post = function(param) {
		var def = $q.defer();
		$http({
			method: 'Post',
			url: url,
			headers: AuthService.getHeader(),
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

	service.put = function(param) {
		var def = $q.defer();
		$http({
			method: 'Put',
			url: url,
			headers: AuthService.getHeader(),
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

	service.delete = function(param) {
		var def = $q.defer();
		$http({
			method: 'Delete',
			url: url + '/' + param.idbarang,
			headers: AuthService.getHeader(),
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

function DataPenjualService($http, $q, message, helperServices, AuthService) {
	var url = helperServices.url + '/api/penjual';
	var service = { Items: [] };

	service.get = function() {
		var def = $q.defer();
		if (service.instance) {
			def.resolve(service.Items);
		} else {
			$http({
				method: 'Get',
				url: url,
				headers: AuthService.getHeader()
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

	service.getById = function(id) {
		var def = $q.defer();
		if (service.instance) {
			var data = service.Items.find((x) => x.idbarang == id);
			def.resolve(data);
		} else {
			$http({
				method: 'Get',
				url: url + '/' + id,
				headers: AuthService.getHeader()
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

	service.post = function(param) {
		var def = $q.defer();
		$http({
			method: 'Post',
			url: url,
			headers: AuthService.getHeader(),
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

	service.put = function(param) {
		var def = $q.defer();
		$http({
			method: 'Put',
			url: url,
			headers: AuthService.getHeader(),
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

	service.delete = function(param) {
		var def = $q.defer();
		$http({
			method: 'Delete',
			url: url + '/' + param.idbarang,
			headers: AuthService.getHeader(),
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
function CommentServices($http, $q, message, helperServices, AuthService) {
	var url = helperServices.url + '/api/comment';
	var service = {};

	service.get = function(idbarang) {
		var def = $q.defer();
		$http({
			method: 'Get',
			url: url + '/' + idbarang,
			headers: AuthService.getHeader()
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

	service.post = function(param) {
		var def = $q.defer();
		$http({
			method: 'Post',
			url: url,
			headers: AuthService.getHeader(),
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

	service.put = function(param) {
		var def = $q.defer();
		$http({
			method: 'Put',
			url: url,
			headers: AuthService.getHeader(),
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

	service.delete = function(param) {
		var def = $q.defer();
		$http({
			method: 'Delete',
			url: url + '/' + param.idcomment,
			headers: AuthService.getHeader()
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

function ParameterService($http, $q, message, helperServices, AuthService) {
	var url = helperServices.url + '/api/parameter';
	var service = { Items: [] };

	service.get = function() {
		var def = $q.defer();
		if (service.instance) {
			def.resolve(service.Items);
		} else {
			$http({
				method: 'Get',
				url: url,
				headers: AuthService.getHeader()
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

	service.getById = function(id) {
		var def = $q.defer();
		if (service.instance) {
			var data = service.Items.find((x) => x.idkategori == id);
			def.resolve(data);
		} else {
			$http({
				method: 'Get',
				url: url + '/' + id,
				headers: AuthService.getHeader()
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

	service.post = function(param) {
		var def = $q.defer();
		$http({
			method: 'Post',
			url: url,
			headers: AuthService.getHeader(),
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

	service.put = function(param) {
		var def = $q.defer();
		$http({
			method: 'Put',
			url: url,
			headers: AuthService.getHeader(),
			data: param
		}).then(
			(response) => {
				service.Items.forEach((x) => {
					if (x.idnilai == param.idnilai) {
						x.status = true;
					} else {
						x.status = false;
					}
				});

				def.resolve(response.data);
			},
			(err) => {
				message.error(err.data);
				def.reject(err);
			}
		);
		return def.promise;
	};

	service.delete = function(param) {
		var def = $q.defer();
		$http({
			method: 'Delete',
			url: url + '/' + param.idnilai,
			headers: AuthService.getHeader()
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
