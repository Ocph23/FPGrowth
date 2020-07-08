angular
	.module('algoritma.controller', [])
	.controller('AlgoritmaController', AlgoritmaController)
	.controller('AlgoritmaHomeController', AlgoritmaHomeController);

function AlgoritmaController($scope, $state, message, AlgoritmaService, ParameterService) {
	$scope.IsBusy = false;

	$scope.analisa = (param) => {
		$scope.IsBusy = true;
		var dataparam = { MinSupport: param.nilai_minimum_support, Confidance: param.nilai_minimum_confidancce };

		AlgoritmaService.get(dataparam).then((result) => {
			$scope.model = result;

			$scope.confidances = [];
			var index = 0;
			result.frekuensiItemSet.forEach((elements) => {
				var items = [];
				elements.forEach((element) => {
					items.push(element / result.resultX[index].count);
				});
				index++;
				$scope.confidances.push(items);
			});

			var vertices = [];
			var edgesData = [];
			vertices.push({ id: 0, label: 'null', level: 0 });
			edgesData.push({ from: null, to: 0 });
			$scope.model.itemSortPriority.item1.forEach((element, index) => {
				vertices.push({ id: element.id, label: element.name, level: element.index + 1 });

				edgesData.push({ from: element.parenId, to: element.id });
			});

			var nodes = new vis.DataSet(vertices);

			var edges = new vis.DataSet(edgesData);

			// create a network
			var container = document.getElementById('mynetwork');
			var data = {
				nodes: nodes,
				edges: edges
			};
			var options = {
				layout: {
					hierarchical: {
						direction: 'UD'
					}
				}
			};
			var network = new vis.Network(container, data, options);
			$scope.IsBusy = false;
		});
	};

	ParameterService.get().then((x) => {
		$scope.source = x;
	});

	$scope.save = (data) => {
		if (data.idnilai) {
			ParameterService.put(data).then((res) => {});
		} else {
			ParameterService.post(data).then((res) => {});
		}
	};
	$scope.delete = (data) => {
		message.dialog('Hapus Data Parameter').then((x) => {
			ParameterService.delete(data).then((res) => {
				message.info('Data Berhasil Dihapus');
			});
		});
	};

	$scope.select = (item) => {
		$scope.param = angular.copy(item);
	};
}
function AlgoritmaHomeController($scope, $state, AuthService) {}
