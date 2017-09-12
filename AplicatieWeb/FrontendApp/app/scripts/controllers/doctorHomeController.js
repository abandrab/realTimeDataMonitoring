/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp')
        .controller('DoctorHomeController', DoctorHomeController);

    /*@ngInject*/
    function DoctorHomeController($scope, $state, $rootScope, DoctorService) {
        $scope.patients = [];
        $scope.view = view;

        function getAllPatients(){
            DoctorService.getPatients()
                .then(function (patients){
                  $scope.patients = patients;
                });
        }
        getAllPatients();

        function view(patient) {
            $rootScope.Id = patient.Id;
            $rootScope.patientEmail = patient.UserDetails.Email;
            $state.transitionTo('patient');
        }

        $rootScope.$on('alert', function (e, data) { console.log(data + 'aaaaaaa'); });
        $scope.$on('alert', function (e, data) { console.log(data+ 'aaaaaaa'); });

    }
})();