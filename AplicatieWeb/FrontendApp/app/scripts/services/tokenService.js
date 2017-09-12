/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp').factory('TokenService', TokenService);
    /*@ngInject*/
    function TokenService() {

        var getRefreshToken = function getRefreshToken() {
            
        };
        
        return {
            getRefreshToken: getRefreshToken
        };
    }
})();