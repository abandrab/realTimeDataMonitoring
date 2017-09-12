'use strict';

/**
 * @ngdoc overview
 * @name frontendAppApp
 * @description
 * # frontendAppApp
 *
 * Main module of the application.
 */
var frontendApp = angular
    .module('frontendApp', [
        'ui.bootstrap',
        'ui.router',
        'permission', 'permission.ui', 'permission.ng',
        'satellizer',
        'toastr',
        'LocalStorageModule',
        'ngAnimate',
        'ngAria',
        'ngCookies',
        'ngMessages',
        'ngResource',
        'ngSanitize',
        'ngTouch',
        'ngPatternRestrict',
        'angularXRegExp',
        'angular-data-service',
        'ngWebsocket',
        'angularMoment',
        'ng.epoch',
        'countrySelect',
        'ui.bootstrap.datetimepicker',
        'SignalR',
        'moment-picker',
        'ngAudio',
        'matchmedia-ng'
    ]);

frontendApp.config(function ($stateProvider, $urlRouterProvider, $authProvider, $locationProvider, $qProvider) {
    $locationProvider.hashPrefix('');
    $qProvider.errorOnUnhandledRejections(false);

    var skipIfLoggedIn = ['$q', '$auth', function ($q, $auth) {
        var deferred = $q.defer();
        if ($auth.isAuthenticated()) {
            deferred.reject();
        } else {
            deferred.resolve();
        }
        return deferred.promise;
    }];

    var loginRequired = ['$q', '$location', '$auth', function ($q, $location, $auth) {
        var deferred = $q.defer();
        if ($auth.isAuthenticated()) {
            deferred.resolve();
        } else {
            $location.path('/signin');
        }
        return deferred.promise;
    }];

    var roleAccess = function (access) {
        return ['$q', 'UserService', '$location', function ($q, UserService, $location) {
            var deferred = $q.defer();
            var role = UserService.getRole();
                console.log(role);
                if (role!== null && role === access) {
                    deferred.resolve();
                } else {
                    $location.path('/signin');
                }
            return deferred.promise;
        }];
    };

    var getHomeTemplate = function () {
        return [ 'UserService', '$location', '$templateFactory', function (UserService, $location, $templateFactory) {
            var role = UserService.getRole();
            console.log(role);
            if (role !== null) {
                if (role === 'Doctor') {
                    return $templateFactory.fromUrl('views/doctorHomeView.html');
                }
                if (role === 'Patient') {
                    return $templateFactory.fromUrl('views/patientHomeView.html');
                }
                if (role === 'Administrator') {
                    return $templateFactory.fromUrl('views/addUserView.html');
                }
            } else {
                $location.path('/signin');
            }
        }];
    };
    var getHomeController = function () {
        return ['UserService', '$location', function (UserService, $location) {
            var role = UserService.getRole();
            console.log(role);
            if (role !== null) {
                if (role === 'Doctor') {
                    return 'DoctorHomeController';
                }
                if (role === 'Patient') {
                    return 'PatientHomeController';
                }
                if (role === 'Administrator') {
                    return 'AddUserController';
                }
            } else {
                $location.path('/signin');
            }
        }];
    };

    var phoneAccess =  ['$q', '$rootScope', '$location', function ($q, $rootScope, $location) {
            var deferred = $q.defer();
            if ($rootScope.isPhone) {
                deferred.resolve();
            } else {
                $location.path('/');
            }
            return deferred.promise;
        }];

    $stateProvider
        .state('home', {
            url: '/',
            controllerProvider: getHomeController(),
            templateProvider: getHomeTemplate(),
            resolve: {
                loginRequired: loginRequired
            }
        })
        .state('signin', {
            url: '/signin',
            templateUrl: 'views/signInView.html',
            controller: 'SignInController',
            resolve: {
                skipIfLoggedIn: skipIfLoggedIn
            }
        })
        .state('signup', {
            url: '/signup',
            templateUrl: 'views/signUpView.html',
            controller: 'SignUpController',
            resolve: {
                skipIfLoggedIn: skipIfLoggedIn
            }
        })
        .state('signout', {
            url: '/signout',
            template: null,
            controller: 'SignOutController'
        })
        .state('patientHome', {
            url: '/startapp',
            templateUrl: 'views/patientHomeView.html',
            controller: 'PatientHomeController',
            resolve: {
                loginRequired: loginRequired,
                roleAccess: roleAccess('Patient')
            }
        })
        .state('doctorHome', {
            url: '/patients',
            templateUrl: 'views/doctorHomeView.html',
            controller: 'DoctorHomeController',
            resolve: {
                loginRequired: loginRequired,
                roleAccess: roleAccess('Doctor')
            }
        })
        .state('assistantHome', {
            url: '/patientlist',
            templateUrl: 'views/assistantHomeView.html',
            controller: 'AssistantHomeController',
            resolve: {
                loginRequired: loginRequired,
                roleAccess: roleAccess('Assistant')
            }
        })
        .state('adminHome', {
            url: '/admin',
            templateUrl: 'views/adminHomeView.html',
            controller: 'AdminHomeController',
            resolve: {
                loginRequired: loginRequired,
                roleAccess: roleAccess('Administrator')
            }
        })
        .state('addUser', {
            url: '/adduser',
            templateUrl: 'views/addUserView.html',
            controller: 'AddUserController',
            resolve: {
                loginRequired: loginRequired,
                roleAccess: roleAccess('Administrator')
            }

        })
        .state('account', {
            url: '/account',
            templateUrl: 'views/accountView.html',
            controller: 'AccountController',
            resolve: {
                loginRequired: loginRequired
            }

        })
        .state('editAccount', {
          url: '/editAccount',
          templateUrl: 'views/editAccountView.html',
          controller: 'EditAccountController',
          resolve: {
              loginRequired: loginRequired
          }

        })
        .state('patient', {
            url: '/patient/:patientId',
            templateUrl: 'views/patientView.html',
            controller: 'PatientController',
            resolve: {
                loginRequired: loginRequired,
                roleAccess: roleAccess('Doctor')
            }
        })
        .state('search', {
            url: '/search',
            templateUrl: 'views/searchView.html',
            controller: 'SearchController',
            resolve: {
                loginRequired: loginRequired,
                roleAccess: roleAccess('Patient')
            }
        })
        .state('changePass', {
            url: '/changepwd',
            templateUrl: 'views/changePasswordView.html',
            controller: 'ChangePasswordController',
            resolve: {
                loginRequired: loginRequired
            }
        })
        .state('notifications', {
            url: '/notifications',
            templateUrl: 'views/notificationsView.html',
            controller: 'NavbarController',
            resolve: {
                loginRequired: loginRequired,
                phoneAccess: phoneAccess
            }
        });
  //  $locationProvider.html5Mode(true);
    $urlRouterProvider.otherwise('/');
    $authProvider.tokenType = 'Bearer';
    $authProvider.loginUrl = '/oauth2/token';
    var protocol = 'http://';
    //var host = 'localhost:';
    //var port = '81';
    //var page = '/Auth.Server';
    var host = '192.168.43.33:';
    var port = '82';
    var page = '/authorization';
    $authProvider.baseUrl = protocol + host + port + page;
    $authProvider.tokenName = 'access_token';
});
frontendApp.run(function (amMoment, $rootScope, UserService, $auth, NotifyService,
    NotifySocket, RequestService, $interval, matchmedia) {
    amMoment.changeLocale('ro');

    $rootScope.init = function () {
        $rootScope.isAuthenticated = $auth.isAuthenticated();
        if ($rootScope.isAuthenticated) {
            var role = $auth.getPayload().role;
            $rootScope.isDoctor = role === 'Doctor';
            $rootScope.isAssistant = role === 'Assistant';
            $rootScope.isAdministrator = role === 'Administrator';
            $rootScope.isPatient = role === 'Patient';
            $rootScope.resize();
        }
    };

    $rootScope.resize = function () {
        $rootScope.isScreen = matchmedia.isScreen();
        $rootScope.isDesktop = matchmedia.isDesktop();
        $rootScope.isTablet = matchmedia.isTablet();
        $rootScope.isPhone = matchmedia.isPhone();
    };



    $rootScope.notificationInterval = function () {
        if ($auth.isAuthenticated()) {

            if (UserService.getRole() === 'Doctor' || UserService.getRole() === 'Assistant') {
                NotifySocket.connect();
                var interval;
                $rootScope.newRequests = false;
                $rootScope.nrRequests = 0;

                if (angular.isDefined(interval)) { return; }
                interval = $interval(function () {
                    if (!$auth.isAuthenticated()) { $interval.cancel(interval); }
                    RequestService.notify();
                }, 5000);
            }
            if (UserService.getRole() === 'Patient') {
                var interval2;
                $rootScope.sentRequest = false;
                if (angular.isDefined(interval2)) { return; }
                interval2 = $interval(function () {
                    if (!$auth.isAuthenticated()) { $interval.cancel(interval2); }
                    RequestService.notify();
                }, 5000);

            }
        }
    };
    $rootScope.init();
    $rootScope.notificationInterval();

   if ($auth.isAuthenticated() && UserService.getRole() === 'Doctor') {

        $rootScope.$on('alert', function (e, data) {
            console.log('on');
            var message = NotifyService.createText(data);
            if (message !== '') {
                NotifyService.playAlarm(message);
            }
        });
    }
});
