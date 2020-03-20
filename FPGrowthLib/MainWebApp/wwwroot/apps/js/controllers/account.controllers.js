angular.module("account.controller", [])
  .controller("LoginController", LoginController);

function LoginController($scope, $state, AuthService) {
  $scope.login = function (user) {
    AuthService.login().then(x => {
      $state.go("home");
    })
  }
}