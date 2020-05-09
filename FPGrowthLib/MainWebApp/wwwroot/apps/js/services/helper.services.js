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

	service.groupBy = (list, keyGetter) => {
		const map = new Map();
		list.forEach((item) => {
			const key = keyGetter(item);
			const collection = map.get(key);
			if (!collection) {
				map.set(key, [ item ]);
			} else {
				collection.push(item);
			}
		});
		return map;
	};

	service.genders = [ 'L', 'P' ];

	return service;
}

function kodefikasiServices() {
	var service = {};

	service.kategori = (number) => {
		if (!number) return null;
		else return 'K' + number.padLeft(3);
	};
	service.manajemen = (number) => {
		if (!number) return null;
		else return 'MT' + number.padLeft(3);
	};
	service.penjual = (number, tgl) => {
		if (!number || !tgl) return null;
		tgl = new Date(tgl);
		return 'PJ' + tgl.getFullYear() + number.padLeft(4);
	};
	service.pembeli = (number, tgl) => {
		if (!number || !tgl) return null;
		tgl = new Date(tgl);
		return 'PB' + tgl.getFullYear() + number.padLeft(4);
	};
	service.barang = (number, tgl) => {
		if (!number || !tgl) return null;
		tgl = new Date(tgl);
		return 'BRG' + tgl.getFullYear() + tgl.getDay().padLeft(2) + tgl.getMonth().padLeft(2) + number.padLeft(4);
	};
	service.order = (number, tgl) => {
		if (!number || !tgl) return null;
		tgl = new Date(tgl);
		return 'OD' + tgl.getFullYear() + tgl.getDay().padLeft(2) + tgl.getMonth().padLeft(2) + number.padLeft(4);
	};
	service.pembayaran = (number, tgl) => {
		if (!number || !tgl) return null;
		tgl = new Date(tgl);
		return 'PMB' + tgl.getFullYear() + tgl.getDay().padLeft(2) + tgl.getMonth().padLeft(2) + number.padLeft(4);
	};
	service.pengiriman = (number, tgl) => {
		if (!number || !tgl) return null;
		tgl = new Date(tgl);
		return 'PMB' + tgl.getFullYear() + tgl.getDay().padLeft(2) + tgl.getMonth().padLeft(2) + number.padLeft(4);
	};

	return service;
}
