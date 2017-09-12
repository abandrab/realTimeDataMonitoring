/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp')
        .controller('EkgController', EkgController);

    /*@ngInject*/
    function EkgController($scope, $rootScope) {
        $scope.axes = ['right', 'left', 'bottom'];
        $scope.height = 200;
        $scope.width = 500;
        $scope.rangeEKG = [-4, 4];

        $scope.lineEKG1 = [{ values: [{ time: (new Date()).getTime() / 1000, y: 50 }] }];
        $scope.feedEKG1 = [{}];
        function updateEKG1(values) {
            for (var i in values) {
                console.log(values[i]);
                var record = {
                    time: (new Date()).getTime() / 1000,
                    y: (values[i]-128) * 0.03125
                };
                $scope.feedEKG1 = [record];
            } 
        }

        $scope.lineEKG2 = [{ values: [{ time: (new Date()).getTime() / 1000, y: 50 }] }];
        $scope.feedEKG2 = [{}];
        function updateEKG2(values) {
            for (var i in values) {
                var record = {
                    time: (new Date()).getTime() / 1000,
                    y: (values[i] - 128) * 0.03125
                };
                $scope.feedEKG2 = [record];
            } 
        }

        $scope.lineEKG3 = [{ values: [{ time: (new Date()).getTime() / 1000, y: 50 }] }];
        $scope.feedEKG3 = [{}];
        function updateEKG3(values) {
            for (var i in values) {
                var record = {
                    time: (new Date()).getTime() / 1000,
                    y: (values[i] - 128) * 0.03125
                };
                $scope.feedEKG3 = [record];
            }
        }
        $scope.message = '';
        function updateState(values) {
            switch (values[3]){
                case 5:
                    $scope.message = 'Electrozi deconectați!';
                    break;
                case 8:
                    $scope.message = 'Ieșire simulată!';
                    break;
                case 10:
                    $scope.message = 'Eroare!';
                    break;
                default:
                    $scope.message = '';
                    break;
            }
        }

        $rootScope.$on('ekgData', function (e, data) {
            $scope.$apply(function () {
                updateEKG1(data.ECG1);
                updateEKG2(data.ECG2);
                updateEKG3(data.ECG3);
                updateState(data.ES);
            });
        });
    }
})();