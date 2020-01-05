(function () {
    var app = angular.module("myapp", []);
    app.controller("TodoCtrl", ["$scope", function ($scope) {
        $scope.totalTodos = 4;
    }]);
})();
