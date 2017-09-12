/* jshint latedef: false */
(function () {
    'use strict';
    angular.module('frontendApp')
        .controller('EditAccountController', EditAccountController);

    /*@ngInject*/
    function EditAccountController($scope, AccountService) {

        $scope.namePattern = /^([a-zA-ZÀ-žȘ-ț '-]*)$/;
        $scope.emailPattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        $scope.cnpPattern = /\b[1-8]\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])(0[1-9]|[1-4]\d|5[0-2]|99)\d{4}\b/;
        $scope.phonePattern = /^\(\+\d+\)[\/\. \-]\(?\d{3}\)?[\/\. \-][\d\- \.\/]{7,11}$/m;
        $scope.streetPattern = /^([a-zA-ZÀ-žȘ-ț '-]*)$/;
        $scope.streetNumberPattern = /^(\s*|\d+)$/;
        $scope.rangeNumberPattern = /^[A-Z]{2}\d{6}/;

        $scope.update = update;

        AccountService.getAccount().then(function (data) {
            $scope.dataObj = data;
            console.log($scope.dataObj);
        });
        $scope.checkEmpty = checkEmpty;
        $scope.check = check;
        function checkEmpty() {
            if ($scope.dataObj.UserDetails !== undefined) {
                if ($scope.dataObj.UserDetails.Email.length !== 0 && $scope.dataObj.UserDetails.FirstName.length !== 0 && $scope.dataObj.UserDetails.LastName.length !== 0) {
                    return false;
                }
                else {
                    return true;
                }
            }
        }
        function check() {
            if ($scope.dataObj.CNP !== undefined && $scope.dataObj.Birthdate !== undefined) {
                var parts = $scope.dataObj.CNP.substr(1, 6).match(/.{1,2}/g);
                var bday = new Date(parts[0], parts[1], parts[2]);
                var sbday = bday.getFullYear() + '-' + parts[1] + '-' + bday.getDate();


                if (sbday !== $scope.dataObj.Birthdate) {
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
        function update() {
            console.log($scope.dataObj);
            AccountService.updateAccount($scope.dataObj, $scope.dataObj.UserDetails.Role).then(function (response) {
                console.log('Datele au fost salvate cu succes! ' + response);
            }).catch(function (response) {
                console.log('Eroare! ' + response);
            });
        }
    }
})();