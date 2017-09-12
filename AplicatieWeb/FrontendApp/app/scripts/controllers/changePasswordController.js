/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp')
        .controller('ChangePasswordController', ChangePasswordController);

    /*@ngInject*/
    function ChangePasswordController($scope, AccountService, $location, UserService) {
        $scope.changePass = changePass;
        $scope.correct = true;
        function changePass() {
            AccountService.changePass($scope.oldPassword, $scope.newPassword).then(function (response) {
                console.log(response);
                if (response)
                {
                    console.log(UserService.firstChange());
                    if (UserService.firstChange() === 'True') {
                        $location.path('/editAccount');
                    }
                    else {
                        $location.path('/');
                    }
                    
                }
                $scope.correct = response;
            })
                .catch(function (response) { console.log(response); });
        }
    }
})();