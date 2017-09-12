/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp').factory('PatientService', PatientService);
    /*@ngInject*/
    function PatientService($q, SERVER, $http) {

        function getSensors() {
            var deferred = $q.defer();
            $http.get(SERVER + 'patient/getSensors').then(function (response) {
                deferred.resolve(response.data);

            }, function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        }

        function getSensorsD(email) {
            var deferred = $q.defer();
            $http.get(SERVER + 'patient/getSensors?email='+email).then(function (response) {
                deferred.resolve(response.data);

            }, function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        }

        function getDoctor() {
            var deferred = $q.defer();
            $http.get(SERVER + 'patient/getDoctor').then(function (response) {
                deferred.resolve(response.data);

            }, function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        }
        function getPatient(id) {
            var deferred = $q.defer();
            $http.get(SERVER + 'patient/getbyid?id=' + id).then(function (response) {
                deferred.resolve(response.data);
            }, function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        }

        return {
            getDoctor: getDoctor,
            getPatient: getPatient,
            getSensors: getSensors,
            getSensorsD: getSensorsD
        };
    }
})();