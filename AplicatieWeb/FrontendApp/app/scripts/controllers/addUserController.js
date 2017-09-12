/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp')
        .controller('AddUserController', AddUserController);

    /*@ngInject*/
    function AddUserController($scope, AddUserService, AccountService, $location) {
        $scope.type = '';
        $scope.dataObj = {
            Userdetails: {
                FirstName: '',
                LastName: '',
                Email: '',
                Password: '',
            }
        };

        $scope.addUser =  function () {
            console.log($scope.type);
            switch ($scope.type) {
                case 'Doctor':
                    addDoctor();
                    break;
                case 'Assistant':
                    addAssistant();
                    break;
                default:
                    break;
            }
        };

        $scope.checkEmail = checkEmail;
        function checkEmail() {
            console.log('in');
            if ($scope.signupForm.email.$dirty && !$scope.signupForm.email.$error.pattern && $scope.dataObj.Userdetails.Email !== undefined) {
                console.log($scope.dataObj.Userdetails.Email);
                AccountService.verifyEmail($scope.dataObj.Userdetails.Email)
                    .then(function (response) {
                        console.log(response);
                        $scope.signupForm.email.$error.exists = response.data;
                        if (response.data) {
                            $scope.signupForm.email.$invalid = true;
                            $scope.exists = true;
                        }
                        else {
                            $scope.signupForm.email.$invalid = false;
                            $scope.exists = false;
                        }
                        console.log($scope.signupForm.email.$error.exists);
                    })
                    .catch(function (err) {
                        console.log(err);
                    });
            }
        }

        var addDoctor = function () {
            AddUserService.addDoctor($scope.dataObj)
                .then(function (response) {
                    console.log(response);
                    $location.path('/');
                })
                .catch(function (response) {
                    console.log(response);
                });
        };
        var addAssistant = function () {
            AddUserService.addAssistant($scope.dataObj)
                .then(function (response) {
                    console.log(response);
                    $location.path('/');

                })
                .catch(function (response) {
                    console.log(response);
                });
        };
    }
})();