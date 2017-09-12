/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp').factory('DoctorService', DoctorService);
    /*@ngInject*/
    function DoctorService($http, $q, SERVER) {
        var getPatients = function () {
            var deferred = $q.defer();
            $http.get(SERVER + 'doctor/getpatients')
                .then(function (response) { console.log(response); deferred.resolve(response.data); },
                function (response) { deferred.reject(response); }
                );
            return deferred.promise;
        };
    
        return {
            getPatients: getPatients
        };
    }
})();