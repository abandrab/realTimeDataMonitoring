/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp')
        .controller('PatientController', PatientController);

    /*@ngInject*/
    function PatientController($scope, socket, $rootScope, PatientService) {
        $scope.patientId = $rootScope.Id;
        $scope.patientEmail = $rootScope.patientEmail;
        $scope.start = start;
        $scope.started = false;
        $scope.stop = stop;

        PatientService.getSensorsD($scope.patientEmail).then(function (response) {
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
            socket.setId($scope.patientId);
            socket.connect();
         //   if ($rootScope.socketConnection) {
                $scope.started = true;
           // }
           // else {
                $scope.msg = 'Pacientul nu este conectat';
           // }
        }
        function stop() {
            socket.disconnect();
        }
    }
})();