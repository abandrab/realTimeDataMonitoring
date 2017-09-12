/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp').factory('AccountService', AccountService);
    /*@ngInject*/
    function AccountService($http, $q, SERVER, UserService) {

        var verifyEmail = function (email) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: SERVER + 'account/verifyemail',
                params: {
                    email: email
                }
            })
                .then(function (response) { console.log(response); deferred.resolve(response);},
                function (response) { deferred.reject(response); }
            );
            return deferred.promise;
        };

        var getAccount = function getProfile() {
            var deferred = $q.defer();

            var role = UserService.getRole();
                console.log(role);
                if (role === 'Patient') {
                    $http.get(SERVER + 'patient')
                        .then(function (response) { console.log(response); deferred.resolve(response.data.Patient); },
                        function (response) { deferred.reject(response); }
                        );
                    return deferred.promise;
                }
                else if (role === 'Doctor') {
                    $http.get(SERVER + 'doctor')
                        .then(function (response) { console.log(response); deferred.resolve(response.data.Doctor); },
                        function (response) { deferred.reject(response); }
                        );
                    return deferred.promise;
                }           
            return deferred.promise;
        };

        var test = function test() {
            var deferred = $q.defer();
            $http.get(SERVER + 'account/test')
                .then(function (response) { console.log(response); deferred.resolve(response.data); },
                function (response) { deferred.reject(response); }
                );
            return deferred.promise;
        };

        var updateAccount = function updateAccount(data, role) {
            var deferred = $q.defer();
            if (role === 'Patient') {
                $http.post(SERVER + 'patient/edit', data)
                    .then(
                    function (response) {
                        deferred.resolve(response);
                    },
                    function (response) {
                        deferred.reject(response);
                    });
                return deferred.promise;
            }
            else if (role === 'Doctor') {
                $http.post(SERVER + 'doctor/edit', data)
                    .then(
                    function (response) {
                        deferred.resolve(response);
                    },
                    function (response) {
                        deferred.reject(response);
                    });
                return deferred.promise;
            }
            return deferred.promise;
        };
        function changePass(oldp, newp) {
            var deferred = $q.defer();
            var data = {
                oldPassword: oldp,
                newPassword: newp
            };
            $http.post(SERVER + 'account/ChangePass', data)
                .then(
                function (response) {
                    deferred.resolve(response);
                },
                function (response) {
                    deferred.reject(response);
                });
            return deferred.promise;
        }
        function deleteAccount() {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: SERVER + 'account/delete'
            })
                .then(function (response) { console.log(response); deferred.resolve(response); },
                function (response) { deferred.reject(response); }
                );
            return deferred.promise;
        }
        return {
            test: test,
            getAccount: getAccount,
            updateAccount: updateAccount,
            verifyEmail: verifyEmail,
            changePass: changePass,
            deleteAccount: deleteAccount
        };
    }
})();