/* jshint latedef: false */
(function () {
    'use strict';
    angular.module('frontendApp').factory('ModalService', ModalService);
    /*@ngInject*/
    function ModalService($uibModal, RequestService) {
        var openModal = function(template, data) {
            var modalOpts = {
                animation: true,
                templateUrl: template,
                controller: function ($scope, $uibModalInstance) {
                    $scope.message = data;
                    $scope.cancel = function () {
                        $uibModalInstance.dismiss('cancel');
                    };
                    $scope.delete =  function deleteRequest() {
                        RequestService.deleteRequest(function (response) {
                            console.log(response);
                            $scope.cancel();
                        });
                    };
                },
                size: 'medium',
                backdrop: 'static'
            };

            var modalInstance = $uibModal.open(modalOpts);

            modalInstance.result.then(function (data) {
                return data;
            }, function (data) {
                console.log('modal dismissed');
                return data;
            });

            return modalInstance;
        };

        // one type of modal
        var alert = function (text) {
            var template = '../../views/modals/alertModal.html';
            return openModal(template, text);
        };

        //var notifyDoctor = function (text) {
        //    var template = '../../views/modals/doctorModal.html';
        //    return openModal(template, text);
        //};
        //var notifyPatient = function (text) {
        //    var template = '../../views/modals/patientModal.html';
        //    return openModal(template, text);
        //};
        var deleteAccount = function (text) {
            var template = '../../views/modals/deleteModal.html';
            return openModal(template, text);
        };



        return {
            alert: alert,
            //notifyDoctor: notifyDoctor,
          //  notifyPatient: notifyPatient,
            deleteAccount: deleteAccount
        };
    }
})();