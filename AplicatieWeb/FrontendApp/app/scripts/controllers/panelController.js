/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp')
        .controller('PanelController', PanelController);

    /*@ngInject*/
    function PanelController($scope, $rootScope) {
        $scope.EKGPulse = '';
        function updateEKGPulse(value) {
            $scope.EKGPulse = 'Puls (ECG): ' + value +' BPM';
        }
        $scope.temperature = '';
        function updateTemperature(value) {
            if (value !== null){
                $scope.temperature = 'Temperatură:' + value + '°C';
            }
        }
        $scope.SPO2Pulse = '';
        function updateSPO2Pulse(value) {
            $scope.SPO2Pulse = 'Puls (SpO2): '+ value + ' BPM';
        }
        $scope.SO = '';
        function updateSO(value) {
            $scope.SO = 'Saturația de Oxigen: ' + value + '%';
        }

        $rootScope.$on('epData', function (e, data) {
            $scope.$apply(function () {
                updateEKGPulse(data);
            });
        });
        $rootScope.$on('soData', function (e, data) {
            $scope.$apply(function () {
                updateSO(data);
            });
        });
        $rootScope.$on('spData', function (e, data) {
            $scope.$apply(function () {
                updateSPO2Pulse(data);
            });
        });
        $rootScope.$on('tcData', function (e, data) {
            $scope.$apply(function () {
                updateTemperature(data);
            });
        });
    }
})();