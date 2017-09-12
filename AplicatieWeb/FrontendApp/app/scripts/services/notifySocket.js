/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp').factory('NotifySocket', NotifySocket);
    /*@ngInject*/
    function NotifySocket($rootScope, $auth, SERVER, $http, UserService, NotifyService, PatientService) {
        var service = {};
        var getPatientName = function (patientId) {
            PatientService.getPatient(patientId).then(function (response) {
                var name = response.UserDetails.LastName + ' ' + response.UserDetails.FirstName;
                return name;
            });
        };

        service.connect = function () {
            var ws;
            var ip = '192.168.43.86';
            ws = new WebSocket('ws://' + ip + ':5000', [$auth.getToken()], {
               // perMessageDeflate: false
            });
            ws.onopen = function () {
                console.log('Succeeded to open a notify connection');
            };
            ws.onerror = function () {
                console.log('Failed to open a notify connection');
            };
            ws.onmessage = function (record) {
                var data = JSON.parse(record.data);
                console.log(data);
                    switch (data.nodeType) {
                        case 'ECG':
                            var value = data.package.EP;
                            var stateECG = data.package.ES[3];
                            if (stateECG === 0) {
                                if (value < 60 || value > 100) { NotifyService.sendMsg('EP', value, data.patientId); }
                            }
                            break;
                        case 'SpO2':
                            var stateSpo2 = data.package.SI;
                            if (stateSpo2 === 0) {
                                var valueSP = data.package.SP;
                                var valueSO = data.package.SO;
                                if (valueSP !== 0 && (valueSP < 60 || valueSP > 100)) { NotifyService.sendMsg('SP', valueSP, data.patientId); }
                                if (valueSO !== 0 && valueSO < 95) { NotifyService.sendMsg('SO', valueSO, data.patientId); }
                            }
                            break;
                        case 'TAC':
                            var valueT = data.package.TC;
                            if (valueT < 36.5 || valueT > 37) {
                                NotifyService.sendMsg('Temp', valueT, data.patientId);
                            }
                            break;
                        default:
                            break;
                    }
               // }
            };
            ws.onclose = function () {
                console.log('Notify connection closed');
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