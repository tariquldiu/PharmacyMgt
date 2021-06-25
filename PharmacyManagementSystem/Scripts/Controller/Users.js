var app = angular.module("myApp", ['ngCookies']);
app.controller("Users", function ($scope, $http, $filter, $cookieStore) {
    Clear();
    function Clear() {
        $scope.User = {};
        $scope.UserId = 0;
        $scope.UserList = [];


    }
    $scope.Login = function () {
        $http({
            method: "post",
            url: "../Users/Login",
            datatype: "json",
            data: JSON.stringify($scope.User)
        }).then(function (response) {
            if (response.data == 1) {
                alert("Login Successfull!");
                window.location.href = '/Home/Index';
            }
            else {
                alert("Invalid Username or Password!");
            }
            Clear();
        }, function () {
            alert("Error Occur");
        })
    }
    $scope.Logout = function () {
        $http({
            method: "get",
            url: "../Users/Logout",
        }).then(function (response) {
            if (response.data == 1) {
                alert("Logout Successfull!");
                window.location.href = '/Login/Index';
                Clear();
            }
        }, function () {
            alert("Error Occur");
        })
    }
    $scope.SetPassFocus = function (e) {
        if (e.keyCode === 13) {
            $("#exampleInputPassword").focus();
        }
    }
    $scope.SetLoginFocus = function (e) {
        if (e.keyCode === 13) {
            $("#btnLogin").focus();
        }
    }
});
