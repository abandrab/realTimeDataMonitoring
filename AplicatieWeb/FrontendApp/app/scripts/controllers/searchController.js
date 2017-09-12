/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp')
        .controller('SearchController', SearchController);

    /*@ngInject*/
    function SearchController($scope, $filter, SearchService, RequestService, $rootScope, PatientService) {
        $scope.searchString='';
        $scope.searchResults = searchResults;
        $scope.search = search;
        $scope.add = add;
        $scope.sendRequest = $rootScope.sentRequest;
        $scope.myDoctorId = '';

        function getDoctor() {
            PatientService.getDoctor().then(function (response) {
                $scope.myDoctorId = response.Id;
            });
        }

        function search() {
            return SearchService.getResults($scope.searchString)
                .then(function (response) {

                    return response;
                });
        }

        function searchResults() {
            $scope.allResults = [];
            $scope.message = '';
            var regex = new RegExp("['\\'\\[%@()*!^=~$/?<>:;{}|,'\\]'\"']");
            var invalidChar = false;
            if ($scope.searchString !== ' ' && $scope.searchString !== '') {
                for (var s = 0; s < $scope.searchString.length; s++) {
                    if (regex.test($scope.searchString[s])) {
                        invalidChar = true;
                    }
                }
                if (invalidChar === false) {
                    check();
                    $scope.search().then(function (response) {
                        $scope.allResults = response;
                        console.log($scope.allResults);
                        if (Object.keys($scope.allResults).length === 0 && $scope.allResults.constructor === Object) {
                            $scope.message = 'Nici un rezultat';
                        } else {
                            // $scope.allResults = $filter('orderBy')($scope.allResults, '');
                        }
                    });
                } else {
                    $scope.message = 'Nici un rezultat';
                }
            }
            else {
                $scope.message = 'Căutare invalidă';
            }
        }

        function add(id) {
            RequestService.sendRequest(new String(id)).then(function (response) {
                console.log(response);
                check();
            }).catch(function (response) {
                console.log(response);
            });
        }

        function check() {
            RequestService.check().then(function (response) {
                console.log(response);
                if (response !== null && response.State === 'Pending') {
                    $scope.sentRequest = true;
                }               
            }).catch(function (response) {
                console.log(response);
            });
        }

        getDoctor();
    }
})();