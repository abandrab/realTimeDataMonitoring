/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp')
        .controller('Spo2Controller', Spo2Controller);

    /*@ngInject*/
    function Spo2Controller($scope, $timeout, $rootScope) {
        $scope.axes = ['right', 'left', 'bottom'];
        $scope.height = 200;
        $scope.width = 500;
        $scope.rangeSW = [0, 247];

        $scope.lineSW = [{ values: [{ time: (new Date()).getTime() / 1000, y: 80 }] }];
        $scope.feedSW = [{}];
        function updateSW(values) {
            for (var i in values) {
                console.log(values[i]);
                var record = {
                    time: (new Date()).getTime() / 1000,
                    y: values[i]
                };
                $scope.feedSW = [record];
            }
        }

        $scope.message = '';
        function updateState(value) {
            switch (value) {
                case 1:
                    $scope.message = 'Senzor deconectat!';
                    break;
                case 2:
                    $scope.message = 'Senzorul nu este pe deget!';
                    break;
                case 3:
                    $scope.message = 'Index de perfuzie mic!';
                    break;
                case 69:
                    $scope.message = 'Eroare!';
                    break;
                default:
                    $scope.message = '';
                    break;
            }
        }
        $rootScope.$on('spo2Data', function (e, data) {
            $scope.$apply(function () {
                updateSW(data.SW);
                updateState(data.SI);
            });
        });
    }
})();