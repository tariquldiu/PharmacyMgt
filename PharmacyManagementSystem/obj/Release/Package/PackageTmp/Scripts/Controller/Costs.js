var app = angular.module("myApp", ['ngCookies']);
app.controller("Costs", function ($scope, $http, $filter, $cookieStore) {
    Clear();
    function Clear() {
        $scope.btnAdd = 'Add Cost';
        $scope.Cost = {};
        $scope.Cost.CostId = 0;
        $scope.totalAmount = 0;
        $scope.totalQuantity = 0;
        $scope.totalAmountFix = 0;
        $scope.Cost.CostId = $cookieStore.get("CostId");
        if ($scope.Cost.CostId == undefined) {
            $scope.Cost.CostId = 0;
        }
        $cookieStore.remove("CostId");
        $scope.ddlPage = {};
        $scope.PageList = [{ PageId: 1, PageNo: '10', PageNoShow: '10' }, { PageId: 2, PageNo: '50', PageNoShow: '50' }, { PageId: 3, PageNo: '100', PageNoShow: '100' }, { PageId: 4, PageNo: '500', PageNoShow: '500' }, { PageId: 5, PageNo: '10000000', PageNoShow: 'All' }]
        $scope.CostTypeList = [{ CostType: 'Others'}];
        $scope.ddlPage.PageNo = '10';
        $scope.CostList = [];
        $scope.CostListTemp = [];
        $scope.CostForDelete = [];
        $scope.BrandList = [];
        $scope.ProductList = [];
        $scope.PurchaseList = [];
        GetPurchaseByCostEntry();
        GetAllPurchase();
        GetAllProduct();
        GetAllCost();
       
    }

    $("#startDate").datepicker({
        dateFormat: 'mm-dd-yy'
    });
    $("#endDate").datepicker({
        dateFormat: 'mm-dd-yy'
    });

    $scope.GetDetails = function () {
        GetDetails();
    }
    function GetDetails() {
        $scope.StartDate = $("#startDate").datepicker({ dateFormat: 'yy-mm-dd' }).val();
        $scope.EndDate = $("#endDate").datepicker({ dateFormat: 'yy-mm-dd' }).val();
        if (($scope.StartDate == "" || $scope.StartDate == undefined) || ($scope.EndDate == "" || $scope.EndDate == undefined)) {
            alert("Please select start date and end date");
            return;
        }

        $http({
            method: "get",
            url: "../Costs/GetDetails?startDate=" + $scope.StartDate + "&endDate=" + $scope.EndDate,
        }).then(function (response) {
            angular.forEach(response.data, function (e) {
                var res1 = e.CostDate.substring(0, 5);
                if (res1 == "/Date") {
                    var parsedDate1 = new Date(parseInt(e.CostDate.substr(6)));
                    var date1 = ($filter('date')(parsedDate1, 'MM/dd/yyyy')).toString();
                    e.CostDate = date1;
                }
                angular.forEach($scope.PurchaseList, function (pur) {
                    if (pur.PurchaseId == e.PurchaseId) {
                        angular.forEach($scope.ProductList, function (pro) {
                            if (pro.ProductId == pur.ProductId) {
                                e.ProductName = pro.ProductName;
                            }
                        });
                    }
                });
            });
            $scope.CostList = response.data;
            LoadCostDetails();
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
    function GetPurchaseByCostEntry() {
        $http({
            method: "get",
            url: "../Purchases/GetPurchaseByCostEntry",
        }).then(function (response) {
            $scope.PurchaseFilteredList = response.data;
        }, function () {
            alert("Error Occur");
        })

    }
    function GetAllCost() {
        $http({
            method: "get",
            url: "../Costs/GetAllCost",
        }).then(function (response) {
            angular.forEach(response.data, function (e) {
                var res1 = e.CostDate.substring(0, 5);
                if (res1 == "/Date") {
                    var parsedDate1 = new Date(parseInt(e.CostDate.substr(6)));
                    var date1 = ($filter('date')(parsedDate1, 'MM/dd/yyyy')).toString();
                    e.CostDate = date1;
                }
                angular.forEach($scope.PurchaseList, function (pur) {
                    if (pur.PurchaseId == e.PurchaseId) {
                        angular.forEach($scope.ProductList, function (pro) {
                            if (pro.ProductId == pur.ProductId) {
                                e.ProductName = pro.ProductName;
                            }
                        });
                    }
                });
            });
            $scope.CostList = response.data;
           
            LoadCostDetails();
        }, function () {
            alert("Error Occur");
        })
    }
    function LoadCostDetails() {
        //if ($scope.btnAdd != 'Update Cost') {
        //    $scope.Cost.CostId = 0;
        //}
        if ($scope.Cost.CostId != 0) {
            $scope.CostTypeList = [{ CostType: 'Medicine' },{ CostType: 'Others' }];
            angular.forEach($scope.CostList, function (data) {
                if (data.CostId == $scope.Cost.CostId) {
                    $scope.btnAdd = 'Update Cost';
                    data.CostDate = new Date(data.CostDate);
                    $("#CostType").prop("disabled", true);
                    if (data.CostType == 'Medicine') {
                        $("#Amount").prop("disabled", true);
                    }
                    $("#CostType").prop("disabled", true);
                    $scope.DisbaleCostProduct = true;
                    $scope.Cost = data;
                    $scope.ddlPurchase = { PurchaseId: data.PurchaseId };
                    $scope.ddlCostType = { CostType: data.CostType };
                }
            });
        }
    }
    $scope.GetTotalAmount = function (cost, index) {
        if (index == 0) {
            $scope.totalAmount = 0;
        }
        $scope.totalAmount += cost.Amount;
        $scope.totalAmountFix = ($scope.totalAmount).toFixed(2);
    }
    $scope.DeleteCost = function (cost) {
        var r = confirm("Are you sure you want to delete?");
        if (r == true) {
            cost.IsActive = false;
            $scope.CostForDelete.push(cost);
            $http({
                method: "post",
                url: "../Costs/SaveCost",
                datatype: "json",
                data: JSON.stringify($scope.CostForDelete)
            }).then(function (response) {
                alert("Purchase Deleted Successfully");
                Clear();
            }, function () {
                alert("Error Occur");
            })
        } else {
            return;
        }

    }
    $scope.SetCostTypeInfo = function (costType) {
        if (costType.CostType == "Medicine") {
            $http({
                method: "get",
                url: "../Purchases/GetPurchaseInfoByCostType",
            }).then(function (response) {
                $scope.PurchaseList = response.data;
            }, function () {
                alert("Error Occur");
                })
            $("#PurchaseId").prop("disabled", false);
            $scope.SelectPurchase = null;
        }
        else {
            $scope.Cost.PurchaseId = null;
            $("#PurchaseId").prop("disabled", true);
            $scope.Cost.Amount = '';
            $scope.ddlPurchase = null;
            $scope.SelectPurchase = 'HasValue';
        }
    }
    $scope.SetPurchaseInfo = function () {
        $scope.Cost.Amount = '';
        $scope.Cost.Amount = $scope.ddlPurchase.UnitPrice * $scope.ddlPurchase.Quantity;
        $scope.SelectPurchase = 'HasValue';
    }
    $scope.LoadCost = function (Cost) {
        $scope.btnAdd = 'Update Cost';
        $scope.Cost = Cost;
       
        $scope.ddlType = { TypeId: Cost.TypeId };
        $scope.ddlBrand = { BrandId: Cost.BrandId };
        $scope.ddlCategory = { CategoryId: Cost.CategoryId };
    }
    $scope.SaveCost = function () {
        $http({
            method: "post",
            url: "../Costs/SaveCost",
            datatype: "json",
            data: JSON.stringify($scope.CostListTemp)
        }).then(function (response) {
            alert("Cost Saved Successfully");
            Clear();
            window.location.href = '/Costs/Index';
        }, function () {
            alert("Error Occur");
        })
    }
    $scope.PrintDoc = function () {
        $('.widget').attr('class', 'forprint green');
        window.print();
        $('.forprint').attr('class', 'widget green');

    }
    $scope.RedirectToCreate = function () {
        window.location.href = '/Costs/Create';
    }
    $scope.RedirectToList = function () {
        window.location.href = '/Costs/Index';
    }
    $scope.EditCost = function (cost) {
        $cookieStore.put("CostId", cost.CostId);
        window.location.href = '/Costs/Create';
    }
    $scope.ResetForm = function () {
        ResetForm();
    }
  
    function ResetForm() {
        $scope.btnAdd = 'Add Cost';
        $scope.Cost = {};
        $scope.Cost.CostId = 0;
        $scope.ddlPurchase = null;
        $scope.ddlProduct = null;
        $scope.ddlCostType = null;
       
    }
    $scope.AddCost = function () {
        if ($scope.ddlPurchase != null) {
            $scope.Cost.ProductName = $scope.ddlPurchase.ProductName;
            $scope.Cost.Quantity = $scope.ddlPurchase.Quantity;
            $scope.Cost.UnitPrice = $scope.ddlPurchase.UnitPrice;
        }
        else {
            $scope.Cost.ProductName = 'Not Purchased Product';
            $scope.Cost.Quantity = 0;
            $scope.Cost.UnitPrice = 0;
            $scope.Cost.PurchaseId = 0;
        }
        
       

        if ($scope.btnAdd == 'Add Cost') {
            var CostAdd = angular.copy($scope.Cost);
            $scope.CostListTemp.push(CostAdd);
        }
        if ($scope.btnAdd == 'Update Cost') {
            $scope.Cost.IsActive = true;
            var CostAdd = angular.copy($scope.Cost);
            $scope.CostListTemp.push(CostAdd);
        }
        ResetForm();
    }
    $scope.RemoveCost = function (Cost) {
        var index = $scope.CostListTemp.indexOf(Cost);
        if (index != -1) {
            $scope.CostListTemp.splice(index, 1);
        }
    }

})  