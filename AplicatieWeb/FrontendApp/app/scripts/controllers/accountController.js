/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp')
        .controller('AccountController', AccountController);

    /*@ngInject*/
    function AccountController($scope, AccountService, $location, ModalService) {
        $scope.user = {};
        $scope.deleteModal = deleteModal;
        $scope.deleteAccount = deleteAccount;
        AccountService.getAccount().then(function (data) {
            $scope.user = data;
            $scope.birthdate = $scope.user.Birthdate.toString().substring(0, 10);
            console.log($scope.user);
        }).catch();

        function deleteModal() {
            ModalService.deleteAccount("După ștergere contul nu poate fi recuperat. Sunteți sigur că vreți să continuați?");
        }

        function deleteAccount() {
            AccountService.deleteAccount().then(function () {
                $location.path('/signout');
            });
        }
    }
})();