/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp').factory('SERVER', SERVER);

    /*@ngInject*/
    function SERVER() {
        //var protocol = 'http://';
        //var host = 'localhost:';//192.168.43.33:
        //var port = '81'; //82
        //var api = '/Web.Api/api/'; //backend/api

        var protocol = 'http://';
        var host = '192.168.43.33:';
        var port = '82';
        var api = '/backend/api/';
        
        return protocol + host + port + api;
    }
})();