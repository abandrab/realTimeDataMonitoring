/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp')
       .controller('NavbarController', NavbarController);

    /*@ngInject*/
    function NavbarController($scope, $rootScope, $interval, RequestService, $window, $auth, $location) {
        $scope.approved = false;
        $scope.rejected = false;
        $scope.more = false;
        $rootScope.allNotifications = [];
        $scope.data = null;
        $scope.message = '';

        $rootScope.resize();
        angular.element($window).bind('resize', function() {
            $scope.$apply(function() {
                $rootScope.resize();
            });
        });

        $scope.$watch('$auth', function () {
            $location.path('/signin');
        });

        
        $rootScope.$on('requests', function (e, data) {
            console.log("New Requests!");
            $scope.newRequests = $rootScope.newRequests;
            $scope.notifications = $rootScope.nrRequests;
            $scope.data = data;
            console.log(data);
            $scope.allNotifications = data;
            if ($rootScope.isPatient && data !== null && data.State === 'Rejected') {
                $scope.message = data.Doctor.UserDetails.LastName + ' ' + data.Doctor.UserDetails.FirstName + ' ți-a respins cererea';
            }
            if ($rootScope.isPatient && data !== null && data.State === 'Approved') {
                $scope.message = data.Doctor.UserDetails.LastName + ' ' + data.Doctor.UserDetails.FirstName + ' este noul tău medic';

            }
        }); 
        $scope.approve = approve;
        $scope.reject = reject;
        $scope.ok = ok;

        function approve(patientId) {
            RequestService.approveRequest(new String(patientId)).then(function (response) {
                console.log(response);
            });
        }

        function reject(patientId) {
            RequestService.rejectRequest(new String(patientId)).then(function (response) {
                console.log(response);
            });
        }

        function ok(patientId) {
            RequestService.deleteRequest(new String(patientId)).then(function (response) {
                console.log(response);
            });
        }
    }
})();