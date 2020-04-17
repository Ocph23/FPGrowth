angular
	.module('app.service', [
		'message.service',
		'auth.service',
		'storage.services',
		'helper.service',
		'data.service',
		'app.pembeli.service',
		'app.penjual.service',
		'app.algoritma.service'
	])
	.controller('homeController', homeController);
