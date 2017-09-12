/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp')
       .controller('SignInController', SignInController);

    /*@ngInject*/
    function SignInController($scope, $location, $auth, UserService, NotifySocket, RequestService, $rootScope) {
        $scope.login = login;
        $scope.user = {
            email: '',
            password: ''
        };

        function login() {
            var dataObj = 'grant_type=password&username=' + $scope.user.email + '&password=' + $scope.user.password + '&client_id=099153c2625149bc8ecb3e85e03f0022';
            var options = {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            };

            $auth.login(dataObj, options)
                .then(function () {
                    console.log(UserService.firstChange());
                    $rootScope.init();
                    if (UserService.firstChange() ==='True') {
                        console.log("in");
                        $location.path('/changepwd');
                    }
                    else {
                        $location.path('/');
                    }
                    if (UserService.getRole() === 'Doctor') {
                        NotifySocket.connect();
                        
                    }
                    if (UserService.getRole() === 'Doctor' || UserService.getRole() === 'Assistant' || UserService.getRole() === 'Patient') {
                        $rootScope.notificationInterval();
                    }                  
              })
                .catch(function (response) {
                    console.log(response);
                    $scope.wrongCredentials = 'Date de autentificare incorecte!';
              });
        }
    }
})();