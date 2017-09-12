/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp').factory('socket', socket);
    /*@ngInject*/
    function socket($rootScope, $auth) {
        var service = {};
        var patientId;

        service.setId = function (id) {
            patientId = id;
        };
        service.connect = function () {

            var ws = new WebSocket('ws://192.168.43.86:5000', [$auth.getToken()]);
            ws.onopen = function () {
                ws.send(patientId);
                console.log('Succeeded to open a connection');
                $rootScope.socketConnection = true;
            };
            ws.onerror = function () {
                console.log('Failed to open a connection');
                $rootScope.socketConnection = false;

            };
            ws.onmessage = function (record) {
                var data = JSON.parse(record.data);
                console.log(data);
                switch (data.nodeType) {
                    case 'ECG':
                        $rootScope.$emit('ekgData', data.package);
                        $rootScope.$emit('epData', data.package.EP);
                        break;
                    case 'SpO2':
                        $rootScope.$emit('spo2Data', data.package);
                        $rootScope.$emit('spData', data.package.SP);
                        $rootScope.$emit('soData', data.package.SO);
                        break;
                    case 'TAC':
                        $rootScope.$emit('acData', data.package);
                        $rootScope.$emit('tcData', data.package.TC);
                        break;
                    default:
                        break;
                }
            };
            ws.onclose = function () {
                console.log('Connection closed');
            };
            service.ws = ws;
        };

        service.send = function (message) {
            service.ws.send(message);
        };
        service.disconnect = function () {
            service.ws.close();
            console.log('Connection closed');
        };

        return service;
    }
})();