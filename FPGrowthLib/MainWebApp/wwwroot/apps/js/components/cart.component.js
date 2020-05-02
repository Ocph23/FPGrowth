angular.module('app.cart.conponent', []).component('cart', {
	controller: function($scope, $state, AuthService, PembeliCartService, message) {
		$scope.source = PembeliCartService.get();

		$scope.add = (item) => {
			PembeliCartService.ad(item);
		};

		$scope.delete = (item) => {
			PembeliCartService.delete(item);
		};

		$scope.lanjut = () => {
			if (AuthService.userIsLogin()) {
				$state.go('pembeli-order', { data: $scope.source });
			} else {
				message.error('Anda Belum Login !');
				$state.go('login');
			}
		};

		$scope.Up = (item) => {
			if (item.stock > item.jumlah) {
				item.jumlah++;
			}
		};
		$scope.Down = (item) => {
			if (item.jumlah > 0) {
				item.jumlah--;
			}
		};

		$scope.hitung = (item) => {
			if (item.jumlah > item.stock) {
				item.jumlah = item.stock;
			}

			PembeliCartService.saveCart();
			return item.jumlah * item.harga;
		};

		$scope.total = () => {
			return $scope.source.reduce((total, item) => {
				return total + item.jumlah * item.harga;
			}, 0);
		};
	},
	templateUrl: 'apps/js/components/templates/cart.html'
});
