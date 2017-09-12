/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp').factory('AddUserService', AddUserService);
    /*@ngInject*/
    function AddUserService($http, $q, SERVER) {

        var addDoctor = function addDoctor(data) {
            var deferred = $q.defer();
            $http.post(SERVER + 'doctor/register', data)
                .then(
                function (response) {
                    deferred.resolve(response);
                },
                function (response) {
                    deferred.reject(response);
                });
            return deferred.promise;
        };
        var addAssistant = function addAssistant(data) {
            var deferred = $q.defer();
            console.log(data);
            $http.post(SERVER + 'assistant/register', data)
                .then(
                function (response) {
                    deferred.resolve(response);
                },
                function (response) {
                    deferred.reject(response);
                });
            return deferred.promise;
        };
        return {
            addDoctor: addDoctor,
            addAssistant: addAssistant
        };
    }
})();