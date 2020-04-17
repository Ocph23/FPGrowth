angular
	.module('helper.service', [])
	.factory('helperServices', helperServices)
	.factory('kodefikasiService', kodefikasiServices);

function helperServices($location) {
	var service = {};
	service.url = $location.$$protocol + '://' + $location.$$host;
	if ($location.$$port) {
		service.url = service.url + ':' + $location.$$port;
	}

	// '    http://localhost:5000';

	return { url: service.url };
}

function kodefikasiServices() {
	var service = {};

	service.kategori = (number) => {
		return 'K' + number.padLeft(3);
	};
	service.manajemen = (number) => {
		return 'MT' + number.padLeft(3);
	};
	service.penjual = (number, tgl) => {
		tgl = new Date(tgl);

		return 'PJ' + tgl.getFullYear() + number.padLeft(4);
	};
	service.pembeli = (number, tgl) => {
		tgl = new Date(tgl);
		return 'PB' + tgl.getFullYear() + number.padLeft(4);
	};
	service.barang = (number, tgl) => {
		tgl = new Date(tgl);
		return 'BRG' + tgl.getFullYear() + tgl.getDay().padLeft(2) + tgl.getMonth().padLeft(2) + number.padLeft(4);
	};
	service.order = (number, tgl) => {
		tgl = new Date(tgl);
		return 'OD' + tgl.getFullYear() + tgl.getDay().padLeft(2) + tgl.getMonth().padLeft(2) + number.padLeft(4);
	};
	service.pembayaran = (number, tgl) => {
		tgl = new Date(tgl);
		return 'PMB' + tgl.getFullYear() + tgl.getDay().padLeft(2) + tgl.getMonth().padLeft(2) + number.padLeft(4);
	};
	service.pengiriman = (number, tgl) => {
		tgl = new Date(tgl);
		return 'PMB' + tgl.getFullYear() + tgl.getDay().padLeft(2) + tgl.getMonth().padLeft(2) + number.padLeft(4);
	};

	function extra(params) {}

	return service;
}
