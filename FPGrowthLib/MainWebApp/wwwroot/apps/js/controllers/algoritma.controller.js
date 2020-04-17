angular
	.module('algoritma.controller', [])
	.controller('AlgoritmaController', AlgoritmaController)
	.controller('AlgoritmaHomeController', AlgoritmaHomeController);

function AlgoritmaController($scope, $state, AuthService, AlgoritmaService) {
	AlgoritmaService.get(1).then((result) => {
		$scope.model = result;

		var vertices = [];
		var edgesData = [];
		vertices.push({ id: 0, label: 'null', level: 0 });
		edgesData.push({ from: null, to: 0 });
		$scope.model.itemSortPriority.item1.forEach((element, index) => {
			vertices.push({ id: element.id, label: element.name, level: element.index + 1 });

			edgesData.push({ from: element.parenId, to: element.id });
		});

		// var edgesData = [];
		// edgesData.push({ to: 0 });
		// $scope.model.itemSortPriority.item2.forEach((element) => {
		// 	edgesData.push({ from: element.item1, to: element.item2 });
		// });

		// var arrs = Object.values($scope.model.graphs.adjacencyList);

		// for (let element of arrs) {
		// 	var item = { from: 0, to: 0 };
		// 	element.forEach((d) => {
		// 		item.to = d;
		// 		if (item.from != item.to) {
		// 			edgesData.push(angular.copy(item));
		// 		}
		// 		item.from = d;
		// 	});
		// 	break;
		// }

		// arrs.forEach((element, index) => {
		// 	var item = { from: 0, to: 0 };
		// 	element.forEach((d) => {
		// 		item.to = d;
		// 		if (item.from != item.to) edgesData.push(angular.copy(item));
		// 		item.from = d;
		// 	});
		// });

		// create an array with edges
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
	});
}
function AlgoritmaHomeController($scope, $state, AuthService) {}
