/* jshint latedef: false */

(function () {
    'use strict';

    angular.module('frontendApp').factory('RequestService', RequestService);

    /*@ngInject*/
    function RequestService($http, $q, SERVER, $state, $rootScope, UserService) {

        function getRequests() {
            var deferred = $q.defer();
            $http.get(SERVER + 'request/getall').then(function (response) {
                deferred.resolve(response.data);

            }, function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        }

        function check() {
            var deferred = $q.defer();
            $http.get(SERVER + 'request/check').then(function (response) {
                deferred.resolve(response.data);
            }, function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        }
        function approveRequest(dataObj) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: SERVER + 'request/approve',
                data: dataObj

            })
                .then(function successCallback(response) {

                    deferred.resolve(response.status);
                })
                .then(null,
                function (error) {

                    console.log('server fails:', error.statusText);
                });
            return deferred.promise;

        }
        function rejectRequest(dataObj) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: SERVER + 'request/reject',
                data: dataObj

            })
                .then(function successCallback(response) {

                    deferred.resolve(response.status);
                })
                .then(null,
                function (error) {

                    console.log('server fails:', error.statusText);
                });
            return deferred.promise;

        }
        var sendRequest = function (id) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: SERVER + 'request/add',
                data: id
            })
                .then(function successCallback(response) {

                    deferred.resolve(response.status);
                })
                .then(null,
                function (error) {

                    console.log('server fails:', error.statusText);
                });
            return deferred.promise;
        };

        var deleteRequest = function () {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: SERVER + 'request/delete',
            })
                .then(function successCallback(response) {

                    deferred.resolve(response.status);
                })
                .then(null,
                function (error) {

                    console.log('server fails:', error.statusText);
                });
            return deferred.promise;
        };

        function notify() {
            var data;
            var role = UserService.getRole();
            $rootScope.nrRequests = 0;
            if (role === 'Doctor' || role === 'Assistant') {
                getRequests().then(function (object) {
                    $rootScope.nrRequests = object.length;
                    data = object;
                        $rootScope.newRequests = true;
                        $rootScope.$emit('requests', data);
                    
                });
            }
            else if (role === 'Patient') {
                 check().then(function (response) {
                     if (response !== null && response.State === 'Pending') {
                         $rootScope.sentRequest = true;
                     }
                     else {
                         $rootScope.sentRequest = false;
                     }
                    if (response !== null && response.State !== 'Pending') {
                        $rootScope.nrRequests = 1;
                        data = response;
                        $rootScope.newRequests = true;
                        $rootScope.$emit('requests', data);
                    }  
                    if (response === null)
                    {
                        $rootScope.$emit('requests', response);
                    }
                });
            }
        }
        return {
            getRequests: getRequests,
            approveRequest: approveRequest,
            check: check,
            sendRequest: sendRequest,
            notify: notify,
            rejectRequest: rejectRequest,
            deleteRequest: deleteRequest
        };
    }
})();