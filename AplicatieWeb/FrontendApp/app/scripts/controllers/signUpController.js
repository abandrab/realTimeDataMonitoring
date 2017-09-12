/* jshint latedef: false */
(function () {
    'use strict';
    angular.module('frontendApp')
       .controller('SignUpController', SignUpController);

    /*@ngInject*/
    function SignUpController($scope, $state, $auth, SERVER, $location, AccountService) {
       // $scope.myDate = new Date();
        //console.log($scope.date);
        $scope.dataObj = {
                Userdetails: {
                    FirstName: '',
                    LastName: '',
                    Sex: '',
                    Email: '',
                    Password: '',
                    Phone: '',
                    Address: null,
                CNP: '',
                RangeNumber: '',
                Birthdate: ''
            }
        };
        $scope.confirmPassword = '';
        $scope.signUp = signUp;

        $scope.namePattern = /^([a-zA-ZÀ-žȘ-ț '-]*)$/;
        $scope.emailPattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        $scope.cnpPattern = /\b[1-8]\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])(0[1-9]|[1-4]\d|5[0-2]|99)\d{4}\b/;
        $scope.phonePattern = /^\(\+\d+\)[\/\. \-]\(?\d{3}\)?[\/\. \-][\d\- \.\/]{7,11}$/m;
        $scope.streetPattern = /^([a-zA-ZÀ-žȘ-ț '-]*)$/;
        $scope.streetNumberPattern = /^(\s*|\d+)$/;
        $scope.rangeNumberPattern = /^[A-Z]{2}\d{6}/;

        $scope.checkEmail = checkEmail;
        $scope.checkEmpty = checkEmpty;
        $scope.check = check;

        function checkEmpty() {
            if ($scope.signupForm.email.$viewValue.length !== 0 && $scope.signupForm.fname.$viewValue.length !== 0 && $scope.signupForm.lname.$viewValue.length !== 0 &&
                $scope.signupForm.pwd.$viewValue.length !== 0 && $scope.signupForm.cpwd.$viewValue.length !== 0)
            {
                return false;
            }
            else {
                return true;
            }
        }

        function checkEmail() {
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

        function check() {
            if ($scope.dataObj.CNP !== undefined && $scope.dataObj.Birthdate !== undefined)
            {
                var parts = $scope.dataObj.CNP.substr(1, 6).match(/.{1,2}/g);
                var bday = new Date(parts[0], parts[1], parts[2]);
                var sbday = bday.getFullYear() + '-' + parts[1] + '-' + bday.getDate();

                
                if (sbday !== $scope.dataObj.Birthdate)
                {
                    console.log('CNP-ul și data nașterii nu se potrivesc!');
                    $scope.cnpbday = true;
                    $scope.signupForm.birthdate.$error.cnpbday = true;
                    $scope.signupForm.birthdate.$invalid = true;
                }
                else {
                    $scope.cnpbday = false;
                    $scope.signupForm.birthdate.$error.cnpbday = false;
                    $scope.signupForm.birthdate.$invalid = false;
                }
                console.log($scope.signupForm.birthdate.$error.cnpbday);
            }
        }
        function signUp() {
            var options = {
                 url: SERVER + 'patient/register'
            };
            $auth.signup($scope.dataObj, options)
                .then(function (response) {
                           $auth.setToken(response);
                           $location.path('/signin');
                 })
                 .catch(function (response) {
                           console.log(response);
                 });

        }
    }
})();