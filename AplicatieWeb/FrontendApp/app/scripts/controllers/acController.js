/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp')
        .controller('AcController', AcController);

    /*@ngInject*/
    function AcController($scope, $rootScope) {
        $scope.axes = ['right', 'left', 'bottom'];
        $scope.height = 200;
        $scope.width = 500;
        $scope.range = [0, 247];

        $scope.lineX= [{ values: [{ time: (new Date()).getTime() / 1000, y: 80 }] }];
        $scope.feedX = [{}];
        function updateX(values) {
            for (var i in values) {
                var record = {
                    time: (new Date()).getTime() / 1000,
                    y: values[i]
                };
                $scope.feedX = [record];
            }
        }

        $scope.lineY = [{ values: [{ time: (new Date()).getTime() / 1000, y: 80 }] }];
        $scope.feedY = [{}];
        function updateY(values) {
            for (var i in values) {
                var record = {
                    time: (new Date()).getTime() / 1000,
                    y: values[i]
                };
                $scope.feedY = [record];
            }
        }

        $scope.lineZ = [{ values: [{ time: (new Date()).getTime() / 1000, y: 80 }] }];
        $scope.feedZ = [{}];
        function updateZ(values) {
            for (var i in values) {
                var record = {
                    time: (new Date()).getTime() / 1000,
                    y: values[i]
                };
                $scope.feedZ = [record];
            }
        }

        $rootScope.$on('acData', function (e, data) {
            $scope.$apply(function () {
                updateX(data.X);
                updateY(data.Y);
                updateZ(data.Z);
            });
         });
    }
})();