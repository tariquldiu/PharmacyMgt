var app = angular.module("myApp", ['ngCookies']);
app.controller("Sales", function ($scope, $http, $filter, $cookieStore) {
    Clear();
    function Clear() {
        $scope.btnAdd = 'Add Sale';
        $scope.Sale = {};
        $scope.Sale.SaleDate = new Date();
        $scope.Sale.SaleId = 0;
        $scope.totalAmount = 0;
        $scope.totalQuantity = 0;
        $scope.totalAmountFix = 0;
        $scope.Sale.SaleId = Number($cookieStore.get("SaleId"));
        $cookieStore.remove("SaleId");
        $scope.ddlPage = {};
        $scope.PageList = [{ PageId: 1, PageNo: '10', PageNoShow: '10' }, { PageId: 2, PageNo: '50', PageNoShow: '50' }, { PageId: 3, PageNo: '100', PageNoShow: '100' }, { PageId: 4, PageNo: '500', PageNoShow: '500' }, { PageId: 5, PageNo: '10000000', PageNoShow: 'All' }]
        $scope.DiscountTypeList = [{ DiscountType: 'Percent' }, { DiscountType: 'Taka' }];
        $scope.ddlPage.PageNo = '10';
        $scope.SaleList = [];
        $scope.SaleListTemp = [];
        $scope.ItemSaleCount = 0;
        $scope.SaleForDelete = [];
        $scope.PurchasedProductList = [];
        GetPurchasedProduct();
        GetAllSale();

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
            url: "../Sales/GetDetails?startDate=" + $scope.StartDate + "&endDate=" + $scope.EndDate,
        }).then(function (response) {
            angular.forEach(response.data, function (e) {
                var res1 = e.SaleDate.substring(0, 5);
                if (res1 == "/Date") {
                    var parsedDate1 = new Date(parseInt(e.SaleDate.substr(6)));
                    var date1 = ($filter('date')(parsedDate1, 'MM/dd/yyyy')).toString();
                    e.SaleDate = date1;
                }

            });
            $scope.SaleList = response.data;
            LoadSaleDetails();
        }, function () {
            alert("Error Occur");
        })

    }

    function GetPurchasedProduct() {
        $http({
            method: "get",
            url: "../Sales/GetPurchasedProduct"
        }).then(function (response) {
            $scope.PurchasedProductList = response.data;
        }, function () {
            alert("Error Occur");
        })
    }
    function GetAllSale() {
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

            });
            $scope.SaleList = response.data;
            LoadSaleDetails();
        }, function () {
            alert("Error Occur");
        })
    }
    function LoadSaleDetails() {

        if ($scope.Sale.SaleId > 0) {

            angular.forEach($scope.SaleList, function (data) {
                if (data.SaleId == $scope.Sale.SaleId) {
                    $scope.btnAdd = 'Update Sale';
                    data.SaleDate = new Date(data.SaleDate);
                    $scope.Sale = data;
                    $scope.isPopup = 0;
                    $http({
                        method: "get",
                        url: "../Sales/GetPurchaseForSaleEdit?purchaseId=" + $scope.Sale.PurchaseId + "&saleId=" + $scope.Sale.SaleId
                    }).then(function (response) {
                        $scope.PurchasedProductList = response.data;
                        if (data.DiscountType == 'Taka') {
                            $scope.Sale.Discount = data.Discount + ' Taka';
                        }
                        else {
                            $scope.Sale.Discount = data.Discount + ' %';
                        }
                        $scope.CurrentQty = data.Quantity;
                        $scope.ddlPurchasedProduct = { PurchaseId: data.PurchaseId };

                        $scope.ddlDiscountType = { DiscountType: data.DiscountType };
                    }, function () {
                        alert("Error Occur");
                    })
                    //if (data.DiscountType == 'Taka') {
                    //    $scope.Sale.Discount = data.Discount + ' Taka';
                    //}
                    //else {
                    //    $scope.Sale.Discount = data.Discount + ' %';
                    //}

                    //$scope.ddlPurchasedProduct = { PurchaseId: data.PurchaseId };

                    //$scope.ddlDiscountType = { DiscountType: data.DiscountType };
                }
            });
        }
    }

    $scope.GetTotalAmount = function (sale, index) {
        if (index == 0) {
            $scope.totalAmount = 0;
            $scope.totalQuantity = 0;
        }
        var amount = Number(sale.Quantity) * Number(sale.UnitPrice);
        $scope.totalAmount += amount;
        $scope.totalQuantity += Number(sale.Quantity);
        $scope.totalAmountFix = ($scope.totalAmount).toFixed(2);
    }
    $scope.ResetProduct = function () {
        $scope.ddlPurchasedProduct = null;
        $scope.Sale.BillNo = '';
        $scope.Sale.IsStock = '';
        $scope.Sale.UnitPrice = '';
        $scope.Sale.IsStock = 'Stock Editable';
        $scope.Sale.Quantity = '';
        $scope.Sale.Amount = '';
        $scope.Sale.Discount = '';
        $scope.ddlDiscountType = null;
    }

    $scope.ChangeAmountForUnitPrice = function () {
        $scope.Sale.Amount = $scope.Sale.Quantity * $scope.Sale.UnitPrice;
    }
    $scope.ChangeAmountForQty = function () {
        $scope.Sale.Quantity = parseInt($scope.Sale.Quantity);
        if ($scope.CurrentQty < $scope.Sale.Quantity) {
            alert("Not enaugh quantity!");
            $scope.Sale.Quantity = $scope.CurrentQty;
            $scope.Sale.Amount = $scope.Sale.Quantity * $scope.Sale.UnitPrice;
            return;
        }
        else {
            $scope.Sale.Amount = $scope.Sale.Quantity * $scope.Sale.UnitPrice;
        }

    }
    $scope.SetDiscountType = function (discountType) {
        if (discountType == 'Percent') {
            $scope.Sale.Discount = '0 %';
            $scope.Sale.Amount = $scope.Sale.UnitPrice * $scope.Sale.Quantity;
        }
        else if (discountType == 'Taka') {
            $scope.Sale.Discount = '0 Taka';
            $scope.Sale.Amount = $scope.Sale.UnitPrice * $scope.Sale.Quantity;
        }

    }
    $scope.SetDiscount = function () {

        if ($scope.Sale.Discount != '') {
            $scope.Sale.Discount = ($scope.Sale.Discount).trim();
            if ($scope.Sale.Discount == 'Taka' || $scope.Sale.Discount == '%') {
                $scope.Sale.Amount = 0;
                return;
            }
            var hasPercent = $scope.Sale.Discount.indexOf('%');
            var hasDot = $scope.Sale.Discount.indexOf('.');
            var hasTaka = $scope.Sale.Discount.indexOf('Taka');

            if (hasDot != -1) {
                alert("Can't accept decimol number!");
                if ($scope.Sale.DiscountType == 'Percent') {
                    $scope.Sale.Discount = '0 %';
                }
                else if ($scope.Sale.DiscountType == 'Taka') {
                    $scope.Sale.Discount = '0 Taka';
                }
                return;
            }
            if (hasPercent == -1 && hasTaka == -1) {
                if ($scope.Sale.DiscountType == 'Percent') {
                    $scope.Sale.Discount = '0 %';
                }
                else if ($scope.Sale.DiscountType == 'Taka') {
                    $scope.Sale.Discount = '0 Taka';
                }

            }
            if (hasPercent != -1 && hasTaka != -1) {
                if ($scope.Sale.DiscountType == 'Percent') {
                    $scope.Sale.Discount = '0 %';
                }
                else if ($scope.Sale.DiscountType == 'Taka') {
                    $scope.Sale.Discount = '0 Taka';
                }

            }
            var discountAmount = parseInt($scope.Sale.Discount);
            if (hasPercent != -1) {
                var amount = $scope.Sale.UnitPrice * $scope.Sale.Quantity;
                var devideByHundred = amount / 100;
                var diductableAmount = discountAmount * devideByHundred;
                var afterDiductionAmount = amount - diductableAmount;
                $scope.Sale.Amount = afterDiductionAmount;
            }
            if (hasTaka != -1) {
                var amount = $scope.Sale.UnitPrice * $scope.Sale.Quantity;
                var afterDiductionAmount = amount - discountAmount;
                $scope.Sale.Amount = afterDiductionAmount;
            }
        }
        else {
            if ($scope.Sale.DiscountType == 'Percent') {
                $scope.Sale.Discount = '0 %';
            }
            else if ($scope.Sale.DiscountType == 'Taka') {
                $scope.Sale.Discount = '0 Taka';
            }
        }
    }

    $scope.GetBillNo = function () {
        var selectedDatetime = $scope.Sale.SaleDate;

        var date = selectedDatetime.getDate();
        var month = selectedDatetime.getMonth();
        var monthadd = month + 1;
        var monthLength = month.toString().length;
        if (monthLength == 1) {
            monthadd = '0' + monthadd;
        }
        var year = selectedDatetime.getFullYear();

        var dateString = year + '-' + monthadd + '-' + date;
        $http({
            method: "get",
            url: "../Sales/GetBillNo?saleDate=" + dateString
        }).then(function (response) {
            $scope.Sale.BillNo = response.data;
        }, function () {
            alert("Error Occur");
        })
    }
    $scope.SetPurchasedProduct = function (purchasedProduct) {
        $scope.Sale.IsStock = 'Stock Available';
        $scope.Sale.UnitPrice = purchasedProduct.UnitPrice;
        $scope.Sale.Quantity = purchasedProduct.Quantity;
        $scope.Sale.Amount = purchasedProduct.Quantity * purchasedProduct.UnitPrice;
        $scope.CurrentQty = purchasedProduct.Quantity;
    }


    $scope.ResetForm = function () {
        ResetForm();
    }
    function ResetForm() {
        $scope.btnAdd = 'Add Sale';
        $scope.Sale.IsStock = '';
        $scope.Sale.UnitPrice = '';
        $scope.Sale.Quantity = '';
        $scope.Sale.Amount = '';
        $scope.Sale.Discount = '';
        $scope.Sale.SaleDate = new Date();
        $scope.ddlDiscountType = null;
        $scope.ddlPurchasedProduct = null;

    }
    $scope.SaveSale = function () {
        angular.forEach($scope.SaleListTemp, function (saleObj) {
            saleObj.Discount = parseInt(saleObj.Discount);
        });
        $http({
            method: "post",
            url: "../Sales/SaveSale",
            datatype: "json",
            data: JSON.stringify($scope.SaleListTemp)
        }).then(function (response) {
            $cookieStore.put("ItemSaleCount", $scope.ItemSaleCount);
            alertify.log('Sale Saved Successfully', 'success', '5000');
            // alert("Sale Saved Successfully");
            if ($scope.isPopup != 0) {
                window.open('../Sales/Vouchar', 'popup', 'location=yes,height=500,width=900,top=100, left=200');
            }
            $scope.isPopup = 1;
            window.location.href = '/Sales/Index';
            Clear();
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
        window.location.href = '/Sales/Create';
    }
    $scope.RedirectToList = function () {
        window.location.href = '/Sales/Index';
    }
    $scope.EditSale = function (sale) {
        $cookieStore.put("SaleId", sale.SaleId);
        window.location.href = '/Sales/Create';
    }
    $scope.DeleteSale = function (sale) {
        var r = confirm("Are you sure you want to delete?");
        if (r == true) {
            $http({
                method: "post",
                url: "../Sales/DeleteSale?saleId=" + sale.SaleId,
            }).then(function (response) {
                alert("Sale Deleted Successfully");
                Clear();
            }, function () {
                alert("Error Occur");
            })
        } else {
            return;
        }

    }
    $scope.AddSale = function () {

        $scope.Sale.Amount = ($scope.Sale.Amount).toFixed(2);
        $scope.Sale.UnitPrice = ($scope.Sale.UnitPrice).toFixed(2);
        $scope.Sale.Quantity = $scope.Sale.Quantity;
        $scope.ItemSaleCount++;
        if ($scope.btnAdd == 'Add Sale') {
            var purchaseObj = angular.copy($scope.Sale);
            if ($scope.SaleListTemp.length > 0) {
                angular.forEach($scope.SaleListTemp, function (dataCheck) {
                    if (dataCheck.PurchaseId == purchaseObj.PurchaseId) {
                        alert('Product already added.');
                        return;
                    }
                    else {
                        $scope.SaleListTemp.push(purchaseObj);
                    }
                });
            }
            else {
                $scope.SaleListTemp.push(purchaseObj);
            }
            
        }
        if ($scope.btnAdd == 'Update Sale') {
            $scope.Sale.IsActive = true;
            var purchaseObj = angular.copy($scope.Sale);
            $scope.SaleListTemp.push(purchaseObj);
            $scope.btnAdd = 'Add Sale';

        }
        ResetForm();
    }
    $scope.RemoveSale = function (sale) {
        var index = $scope.SaleListTemp.indexOf(sale);
        if (index != -1) {
            $scope.ItemSaleCount--;
            $scope.SaleListTemp.splice(index, 1);
        }
    }
})  