/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp').factory('SearchService', SearchService);

    /*@ngInject*/
    function SearchService($http, $q, SERVER) {
        function getResults(searchString) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: SERVER + 'search/search',
                params: {
                    searchString: searchString
                }
            }).then(function (response) {
                console.log(response);
                deferred.resolve(response.data);
            }, function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        }

        return {
            getResults: getResults
        };

    }
})();