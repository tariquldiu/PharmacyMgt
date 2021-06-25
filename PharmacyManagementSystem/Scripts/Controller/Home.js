var app = angular.module("myApp", ['ngCookies']);
app.controller("Home", function ($scope, $http, $filter, $cookieStore) {
    Clear();
    function Clear() {
       
        //let date = ("0" + date_ob.getDate()).slice(-2);
        //let month = ("0" + (date_ob.getMonth() + 1)).slice(-2);
        //let year = date_ob.getFullYear();

        GetAllPurchase();
        GetAllProduct();
        GetAllCost();
        GetAllSale();
        //GetDetails();

    }
    $("#startDate").datepicker({
        dateFormat: 'mm-dd-yy'
    });
    $("#endDate").datepicker({
        dateFormat: 'mm-dd-yy'
    });
    
    function GetDetails() {
        $scope.StartDate = $("#startDate").datepicker({ dateFormat: 'yy-mm-dd' }).val();
        $scope.EndDate = $("#endDate").datepicker({ dateFormat: 'yy-mm-dd' }).val();
        if (($scope.StartDate == "" || $scope.StartDate == undefined) || ($scope.EndDate == "" || $scope.EndDate == undefined)) {
            alert("Please select start date and end date");
            return;
        }
       
        $http({
            method: "get",
            url: "../Home/GetDetails?startDate=" + $scope.StartDate + "&endDate=" + $scope.EndDate,
        }).then(function (response) {
            $scope.DetailList = response.data;
        }, function () {
            alert("Error Occur");
        })

    }
    function GetAllProduct() {
        
        $http({
            method: "get",
            url: "../Products/GetAllProduct",
        }).then(function (response) {
            $scope.ProductList = response.data;
        }, function () {
            alert("Error Occur");
        })

    }
    function GetAllPurchase() {
        $http({
            method: "get",
            url: "../Purchases/GetAllPurchase",
        }).then(function (response) {
            $scope.PurchaseList = response.data;
        }, function () {
            alert("Error Occur");
        })

    }
    function GetAllCost() {
        $http({
            method: "get",
            url: "../Costs/GetAllCost",
        }).then(function (response) {
            $scope.CostList = response.data;
        }, function () {
            alert("Error Occur");
        })

    }
    function GetAllSale() {
        $http({
            method: "get",
            url: "../Sales/GetAllSale",
        }).then(function (response) {
            $scope.SaleList = response.data;
        }, function () {
            alert("Error Occur");
        })

    }
    $scope.GetDetails = function () {
        GetDetails();
    }
})  