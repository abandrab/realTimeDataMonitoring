(function () {
    'use strict';

    var passwordMatch = function () {
        return {
            require: 'ngModel',
            scope: {
                otherModelValue: '=passwordMatch'
            },
            link: function (scope, element, attributes, ngModel) {
                ngModel.$validators.compareTo = function (modelValue) {
                    return modelValue === scope.otherModelValue;
                };
                scope.$watch('otherModelValue', function () {
                    ngModel.$validate();
                });
            }
        };
    };

    angular.module('frontendApp')
        .directive('passwordMatch', passwordMatch);
}());