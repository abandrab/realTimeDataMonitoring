/* jshint latedef: false */

(function () {
    'use strict';
    angular.module('frontendApp')
       .controller('ProfileController', ProfileController);

    /*@ngInject*/
    function ProfileController($scope, $auth, toastr, AccountService) {
        $scope.getProfile = getProfile;
        $scope.updateProfile = updateProfile;

       function getProfile() {
            AccountService.getProfile()
              .then(function(response) {
                  $scope.user = response.data;
              })
              .catch(function(response) {
                  toastr.error(response.data.message, response.status);
              });
       }
       
       function updateProfile() {
           AccountService.updateProfile($scope.user)
             .then(function () {
                 toastr.success('Profile has been updated');
             })
             .catch(function (response) {
                 toastr.error(response.data.message, response.status);
             });
       }

        $scope.getProfile();
    }
})();