angular.module('app.kategori.conponent', []).component('kategori', {
	controller: function($scope, AuthService, $state, KategoriService, kodefikasiService, $rootScope) {
		$scope.kodefikasi = kodefikasiService;

		KategoriService.get().then((x) => {
			$scope.kategories = x;
		});

		$scope.Select = (item) => {
			$state.go('pembeli-home');
			setTimeout(() => {
				$rootScope.$broadcast('selectedKategori', item);
			}, 500);
		};
	},
	templateUrl: 'apps/js/components/templates/kategori.html'
});
