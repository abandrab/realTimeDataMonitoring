/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp').factory('UserService', UserService);
    /*@ngInject*/
    function UserService($auth) {

        var getRole = function() {
            return $auth.getPayload().role;
        };
        var firstChange = function () {
            return $auth.getPayload().firstChange;
        };

        return {
            getRole: getRole,
            firstChange: firstChange
        };
    }
})();