/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp').factory('NotifyService', NotifyService);
    /*@ngInject*/
    function NotifyService($rootScope, ModalService, PatientService) {
        var sendMsg = function (type, value, patientId) {
            PatientService.getPatient(patientId).then(function (response) {
                var name = response.Patient.UserDetails.LastName + ' ' + response.Patient.UserDetails.FirstName;
                $rootScope.$emit('alert', { 'type': type, 'value': value, 'patientName': name });
            });         
        };
        var createText = function (data) {

            var message = '';
            switch (data.type) {
                case 'EP':
                case 'SP':
                    message = 'Pulsul pacientului ' + data.patientName + ' se află în afara intervalului de valori normale: ' + data.value + 'BPM';
                    break;
                case 'Temp':
                    message = 'Temperatura pacientului ' + data.patientName + ' se află în afara intervalului de valori normale: ' + data.value + '°C';
                    break;
                case 'SO':
                    message = 'Saturația de oxigen a pacientului ' + data.patientName + ' se află în afara intervalului de valori normale: ' + data.value + '%';
                    break;
                default:
                    break;
            }
            return message;
        };
        var playAlarm = function (message) {
            var modal = ModalService.alert(message);
            var play = true;
            var alarm = new Audio('../../styles/audio/analog-watch-alarm_daniel-simion.wav');
            if (play) {
                alarm.play();
            }
            alarm.addEventListener('ended', function () {
                if (play) {
                    alarm.play();
                }
                console.log('ended');
            });
            modal.result.then(function (data) {
                console.log('closed! ', data);
                play = false;
                alarm.pause();
            }, function (data) {
                console.log('dismissed ', data);
                play = false;
                alarm.pause();
            });
        };
    
        return {
            sendMsg: sendMsg,
            playAlarm: playAlarm,
            createText: createText
        };
    }
})();