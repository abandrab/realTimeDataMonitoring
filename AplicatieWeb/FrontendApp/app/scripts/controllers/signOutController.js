/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp')
       .controller('SignOutController', SignOutController);

    /*@ngInject*/
    function SignOutController($scope, $location, $auth, NotifySocket, UserService, $rootScope) {
        if (!$auth.isAuthenticated()) { return; }
        else if (UserService.getRole() === 'Doctor' || UserService.getRole() === 'Assistant') {
            NotifySocket.disconnect();
        }
        $auth.logout()
          .then(function () {
              $rootScope.init();
              $location.path('/signin');               
          });
    }
})();