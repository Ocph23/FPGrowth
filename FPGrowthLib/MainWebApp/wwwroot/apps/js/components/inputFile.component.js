angular.module('app.fileInput.conponent', []).component('fileinput', {
	bindings: {
		model: '=',
		showview: '<',
		height: '<',
		width: '<',
		idname: '<',
		src: '<'
	},
	controller: function($scope) {
		$scope.fileName = 'test';
		setTimeout((x) => {
			var inp = document.getElementById($scope.$ctrl.idname);
			inp.addEventListener('change', function(e) {
				var files = e.target.files;
				$scope.change(files[0]);
				if (files[0]) {
					var f = files[0];
					r = new FileReader();
					r.onload = (function(theFile) {
						return function(e) {
							//var binaryData = e.target.result;
							var img = document.createElement('img');
							img.src = e.target.result;
							setTimeout((z) => {
								var canvas = document.createElement('canvas');
								var ctx = canvas.getContext('2d');
								ctx.drawImage(img, 0, 0);

								var MAX_WIDTH = 400;
								var MAX_HEIGHT = 400;
								var width = img.width;
								var height = img.height;

								if (width > height) {
									if (width > MAX_WIDTH) {
										height *= MAX_WIDTH / width;
										width = MAX_WIDTH;
									}
								} else {
									if (height > MAX_HEIGHT) {
										width *= MAX_HEIGHT / height;
										height = MAX_HEIGHT;
									}
								}
								canvas.width = width;
								canvas.height = height;
								var ctx = canvas.getContext('2d');
								ctx.drawImage(img, 0, 0, width, height);

								dataurl = canvas.toDataURL(f.type);

								var parts = dataurl.split(';base64,');
								var contentType = parts[0].split(':')[1];
								var raw = window.atob(parts[1]);
								var base64String = window.btoa(raw);
								$scope.$apply((x) => {
									$scope.$ctrl.fileName = f.name;
									$scope.$ctrl.model = base64String;
								});
							}, 200);
						};
					})(f);
					r.readAsDataURL(f);
				} else {
					$scope.$ctrl.model = null;
				}
			});
		}, 1000);

		$scope.change = (file) => {
			var d = 1;
		};

		$scope.openFile = function() {
			var inp = document.getElementById($scope.$ctrl.idname);
			inp.click();
		};
	},
	templateUrl: 'apps/js/components/templates/fileInput.html'
});
