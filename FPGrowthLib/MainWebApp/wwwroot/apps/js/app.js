angular.module("app",
    [
        "swangular",
        "message.service",
        "auth.service",
        "storage.services",
        "helper.service",

        "app.router",
        "app.conponent",
        "app.controller",



    ]).controller("homeController", homeController)

;


function homeController($scope, AuthService) {
    $scope.logOff = function () {
        AuthService.logOff();
    }
}