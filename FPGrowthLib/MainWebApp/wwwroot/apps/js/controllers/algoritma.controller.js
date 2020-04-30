angular
	.module('algoritma.controller', [])
	.controller('AlgoritmaController', AlgoritmaController)
	.controller('AlgoritmaHomeController', AlgoritmaHomeController);

function AlgoritmaController($scope, $state, AuthService, AlgoritmaService) {
	$scope.IsBusy = false;
	$scope.analisa = (param) => {
		$scope.IsBusy = true;
		AlgoritmaService.get(param).then((result) => {
			$scope.model = result;
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
}
function AlgoritmaHomeController($scope, $state, AuthService) {}
