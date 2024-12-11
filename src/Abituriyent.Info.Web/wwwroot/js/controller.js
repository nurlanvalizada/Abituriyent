var myApp = angular.module('angularApp', []);
myApp.controller('myController', function controller($scope,$location,$anchorScroll) {
    $scope.showTests = { "display": "none" }

    $scope.scrollTo = function (scrollLocation) {
        $scope.showTests = { "display": "block" }
        $location.hash(scrollLocation);
        $anchorScroll();
    };
});

   
