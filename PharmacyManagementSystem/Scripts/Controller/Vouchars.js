var app = angular.module("myApp", ['ngCookies']);
app.controller("Vouchars", function ($scope, $http, $filter, $cookieStore) {
    Clear();
    function Clear() {
        $scope.Sale = {};
        $scope.Sale.SaleId = 0;
        $scope.totalAmountVouchar = 0;
        $scope.totalAmountVoucharFix = 0;
        $scope.ItemSaleCount = Number($cookieStore.get("ItemSaleCount"));
        $scope.SaleList = [];

        GetAllSale();
    }
    function GetAllSale() {
        $scope.SaleList = [];
        $http({
            method: "get",
            url: "../Sales/GetAllSale"
        }).then(function (response) {
            angular.forEach(response.data, function (e) {
                var res1 = e.SaleDate.substring(0, 5);
                if (res1 == "/Date") {
                    var parsedDate1 = new Date(parseInt(e.SaleDate.substr(6)));
                    var date1 = ($filter('date')(parsedDate1, 'MM/dd/yyyy')).toString();
                    e.SaleDate = date1;
                }
                var bill = e.BillNo.split(':');
                e.BillNo = 'Inv: ' + bill[1];
            })
            $scope.printedTime = new Date().toLocaleTimeString();
            $scope.SaleList = response.data;
            
        }, function () {
            alert("Error Occur");


        })
    }
    $scope.GetTotalAmountForVouchar = function (sale, index) {
        if (index == 0) {
            $scope.totalAmountVouchar = 0;
        }
        $scope.totalAmountVouchar += sale.Amount;
        $scope.totalAmountVoucharFix = ($scope.totalAmountVouchar).toFixed(2);
    }

});