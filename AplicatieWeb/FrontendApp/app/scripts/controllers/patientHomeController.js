/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp')
        .controller('PatientHomeController', PatientHomeController);

    /*@ngInject*/
    function PatientHomeController($scope, socket, $rootScope, PatientService){
        $scope.start = start;
        $scope.started = false;
        $scope.stop = stop;

        PatientService.getSensors().then(function (response) {
            console.log(response);
            var s = response;
            for (var i = 0; i < s.length; i++) {
                if (s[i].SensorType === 3) {
                    $scope.tac = true;
                }
                if (s[i].SensorType === 1) {
                    $scope.spo2 = true;
                }
                if (s[i].SensorType === 0) {
                    $scope.ecg = true;
                }
            }
        }).catch(function (response) {
            console.log(response);
        });

        function start() {
            socket.connect();
           // if ($rootScope.socketConnection){
                $scope.started = true;
            //}
           // else {
             //   $scope.msg = 'Nu se poate deschide o conexiune';
            //}
        }
        function stop() {
            socket.disconnect();
        }
    }
})();