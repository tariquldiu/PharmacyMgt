var app = angular.module("myApp", ['ngCookies']);
  app.controller("Purchases", function ($scope, $http, $filter, $cookieStore) {
    Clear();
    function Clear() {
        $scope.btnAdd = 'Add Purchase';
        $scope.StockList = [{ StockId: 1, StockType: 'Stock In' }, { StockId: 0, StockType: 'Stock Out' }]
        $scope.Purchase = {};
        $scope.Purchase.PurchaseId = 0;
        $scope.totalAmount = 0;
        $scope.incdec = true;
        $scope.totalQuantity = 0;
        $scope.currentDatetime = new Date();
        var dd = String($scope.currentDatetime.getDate()).padStart(2, '0');
        var mm = String($scope.currentDatetime.getMonth() + 1).padStart(2, '0'); 
        var yyyy = $scope.currentDatetime.getFullYear();
        $scope.currentDatetime = yyyy + '-' + mm + '-' +dd;
        $scope.totalAmountFix = 0;
        $scope.Purchase.PurchaseId = Number($cookieStore.get("PurchaseId"));
        $cookieStore.remove("PurchaseId");
        $scope.ddlPage = {};
        $scope.PageList = [{ PageId: 1, PageNo: '10', PageNoShow: '10' }, { PageId: 2, PageNo: '50', PageNoShow: '50' }, { PageId: 3, PageNo: '100', PageNoShow: '100' }, { PageId: 4, PageNo: '500', PageNoShow: '500' }, { PageId: 5, PageNo: '10000000', PageNoShow: 'All' }]
        $scope.ddlPage.PageNo = '10';
        $scope.PurchaseList = [];
        $scope.PurchaseListTemp = [];
        $scope.PurchaseForDelete = [];
        $scope.WarehouseList = [];
        $scope.ProductList = [];
        $scope.GroupList = [];
        $scope.ProductTypeList = [];
        $scope.BrandList = [];
        GetAllWarehouse();
        GetAllProduct();
        GetAllBrand();
        GetAllProductType();
        GetAllGroup();
        GetAllPurchase();
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
              url: "../Purchases/GetDetails?startDate=" + $scope.StartDate + "&endDate=" + $scope.EndDate,
          }).then(function (response) {
              angular.forEach(response.data, function (e) {
                  var res1 = e.PurchaseDate.substring(0, 5);
                  var res2 = e.ExpireDate.substring(0, 5);
                  var res3 = e.ManufacturingDate.substring(0, 5);
                  if (res1 == "/Date" && res2 == "/Date" && res3 == "/Date") {
                      var parsedDate1 = new Date(parseInt(e.PurchaseDate.substr(6)));
                      var parsedDate2 = new Date(parseInt(e.ExpireDate.substr(6)));
                      var parsedDate3 = new Date(parseInt(e.ManufacturingDate.substr(6)));
                      var date1 = ($filter('date')(parsedDate1, 'MM/dd/yyyy')).toString();
                      var datematch = ($filter('date')(parsedDate2, 'yyyy-MM-dd')).toString();
                      var date2 = ($filter('date')(parsedDate2, 'MM/dd/yyyy')).toString();
                      var date3 = ($filter('date')(parsedDate3, 'MM/dd/yyyy')).toString();
                      e.PurchaseDate = date1;
                      e.ExpireDate = date2;
                      if (datematch < $scope.currentDatetime) {
                          e.IsExpired = true;
                          console.log(e.IsExpired);
                          console.log($scope.currentDatetime);
                      }
                      else {
                          e.IsExpired = false;
                          console.log(e.IsExpired);
                          console.log($scope.currentDatetime);
                      }
                      e.ManufacturingDate = date3;
                  }
                  e.Amount = e.UnitPrice * e.TotalQuantity;
                  if (e.IsStock) {
                      e.StockStatus = 'Stock In';

                  }
                  else {
                      e.StockStatus = 'Stock Out';

                  }
              });

              $scope.PurchaseList = response.data;
              LoadPurchaseDetails();
          }, function () {
              alert("Error Occur");
          })

      }

    function GetAllWarehouse() {
        $http({
            method: "get",
            url: "../Purchases/GetAllWarehouse"
        }).then(function (response) {
            $scope.WarehouseList = response.data;
        }, function () {
            alert("Error Occur");
        })
      }
      function GetAllPurchase() {
          $http({
              method: "get",
              url: "../Purchases/GetAllPurchase",
          }).then(function (response) {
              angular.forEach(response.data, function (e) {
                  var res1 = e.PurchaseDate.substring(0, 5);
                  var res2 = e.ExpireDate.substring(0, 5);
                  var res3 = e.ManufacturingDate.substring(0, 5);
                  if (res1 == "/Date" && res2 == "/Date" && res3 == "/Date") {
                      var parsedDate1 = new Date(parseInt(e.PurchaseDate.substr(6)));
                      var parsedDate2 = new Date(parseInt(e.ExpireDate.substr(6)));
                      var parsedDate3 = new Date(parseInt(e.ManufacturingDate.substr(6)));
                      var date1 = ($filter('date')(parsedDate1, 'MM/dd/yyyy')).toString();
                      var datematch = ($filter('date')(parsedDate2, 'yyyy-MM-dd')).toString();
                      var date2 = ($filter('date')(parsedDate2, 'MM/dd/yyyy')).toString();
                      var date3 = ($filter('date')(parsedDate3, 'MM/dd/yyyy')).toString();
                      e.PurchaseDate = date1;
                      e.ExpireDate = date2;

                      if (datematch < $scope.currentDatetime) {
                          e.IsExpired = true;
                          console.log(e.IsExpired);
                          console.log($scope.currentDatetime);
                      }
                      else {
                          e.IsExpired = false;
                          console.log(e.IsExpired);
                          console.log($scope.currentDatetime);
                      }
                      e.ManufacturingDate = date3;
                  }
                  e.Amount = e.UnitPrice * e.TotalQuantity;
                  if (e.IsStock) {
                      e.StockStatus = 'Stock In';
                      
                  }
                  else {
                      e.StockStatus = 'Stock Out';
                     
                  }
              });
              
              $scope.PurchaseList = response.data;
              LoadPurchaseDetails();
          }, function () {
              alert("Error Occur");
          })

      }
      function LoadPurchaseDetails() {
          $scope.PurchaseArr = angular.copy($scope.PurchaseList);
          if ($scope.Purchase.PurchaseId != 0) {
              angular.forEach($scope.PurchaseList, function (data) {
                  if (data.PurchaseId == $scope.Purchase.PurchaseId) {
                      $scope.btnAdd = 'Update Purchase';
                      data.PurchaseDate = new Date(data.PurchaseDate);
                      data.ExpireDate = new Date(data.ExpireDate);
                      data.ManufacturingDate = new Date(data.ManufacturingDate);
                      $scope.Purchase = data;
                      if (data.IsStock) {
                          data.StockId = 1;
                      }
                      else {
                          data.StockId = 2;
                      }
                      $scope.ddlStock = { StockId: data.StockId };
                      $scope.ddlProduct = { ProductId: data.ProductId };
                      $scope.ddlWarehouse = { WarehouseId: data.WarehouseId };
                  }
              });

          }
      }
    function GetAllProduct() {
        $http({
            method: "get",
            url: "../Products/GetAllProduct"
        }).then(function (response) {
            $scope.ProductList = response.data;
        }, function () {
            alert("Error Occur");
        })
    }
    function GetAllProductType() {
        $http({
            method: "get",
            url: "../Products/GetAllProductType",
        }).then(function (response) {
            $scope.ProductTypeList = response.data;
        }, function () {
            alert("Error Occur");
        })

    }
    function GetAllBrand() {
        $http({
            method: "get",
            url: "../Products/GetAllBrand"
        }).then(function (response) {
            $scope.BrandList = response.data;
        }, function () {
            alert("Error Occur");
        })
    }

    function GetAllGroup() {
        $http({
            method: "get",
            url: "../Products/GetAllGroup"
        }).then(function (response) {
            $scope.GroupList = response.data;
        }, function () {
            alert("Error Occur");
        })
      }
      $scope.GetTotalAmount = function (purchase, index) {
          if (index == 0) {
              $scope.totalAmount = 0;
              $scope.totalQuantity = 0;
          }
          var amount = Number(purchase.TotalQuantity) * Number(purchase.UnitPrice);
          $scope.totalAmount += amount;
          $scope.totalQuantity += Number(purchase.TotalQuantity);
          $scope.totalAmountFix = ($scope.totalAmount).toFixed(2);
      }
    $scope.SetStoctType = function (stock) {
        if (stock.StockId == 1) {
            $scope.Purchase.StockId = true;
        }
        else {
            $scope.Purchase.StockId = false;
        }

    }
    $scope.GetProductInfo = function (product) {
        angular.forEach($scope.GroupList, function (data) {
            if (product.GroupId == data.GroupId) {
                $scope.Purchase.GroupName = data.GroupName;
            }
        });
        angular.forEach($scope.ProductTypeList, function (data) {
            if (product.TypeId == data.TypeId) {
                $scope.Purchase.TypeName = data.TypeName;
            }
        });
        angular.forEach($scope.BrandList, function (data) {
            if (product.BrandId == data.BrandId) {
                $scope.Purchase.BrandName = data.BrandName;
            }
        });
    }
    $scope.LoadPurchase = function (Purchase) {
        $scope.btnAdd = 'Update Purchase';
        $scope.Purchase = Purchase;
        $scope.ddlType = { TypeId: Purchase.TypeId };
        $scope.ddlBrand = { BrandId: Purchase.BrandId };
        $scope.ddlCategory = { CategoryId: Purchase.CategoryId };
        $scope.ddlWarehouse = { WarehouseId: Purchase.WarehouseId };
    }

    $scope.ResetForm = function () {
        ResetForm();
    }
    function ResetForm() {
        $scope.btnAdd = 'Add Purchase';
        $scope.Purchase = {};
        $scope.ddlCategory = null;
        $scope.ddlBrand = null;
        $scope.ddlType = null;
        $scope.ddlWarehouse = null;
        $scope.ddlStock = null;
        $scope.ddlProduct = null;
    }
    $scope.SavePurchase = function () {
        $http({
            method: "post",
            url: "../Purchases/SavePurchase",
            datatype: "json",
            data: JSON.stringify($scope.PurchaseListTemp)
        }).then(function (response) {
            alert("Purchase Saved Successfully");
            Clear();
            window.location.href = '/Purchases/Index';
        }, function () {
            alert("Error Occur");
        })
    }
    $scope.DeletePurchase = function (purchase) {
        var r = confirm("Are you sure you want to delete?");
        if (r == true) {
            purchase.IsActive = false;
            $scope.PurchaseForDelete.push(purchase);
            $http({
                method: "post",
                url: "../Purchases/SavePurchase",
                datatype: "json",
                data: JSON.stringify($scope.PurchaseForDelete)
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
    $scope.PrintDoc = function () {
        $('.widget').attr('class', 'forprint green');
        window.print();
        $('.forprint').attr('class', 'widget green');

    }
    $scope.RedirectToCreate = function () {
        window.location.href = '/Purchases/Create';
    }
    $scope.RedirectToList = function () {
        window.location.href = '/Purchases/Index';
    }
    $scope.EditPurchase = function (purchase) {
        $cookieStore.put("PurchaseId", purchase.PurchaseId);
        window.location.href = '/Purchases/Create';
    }
    $scope.AddPurchase = function () {
        var unitPrice = Number($scope.Purchase.UnitPrice);
        var quantity = Number($scope.Purchase.Quantity)
        $scope.Purchase.Amount = (unitPrice * quantity).toFixed(2);
        var price = ($scope.Purchase.UnitPrice).toFixed(2);
        var qty = ($scope.Purchase.Quantity).toFixed(2);
        $scope.Purchase.UnitPrice = Number(price);
        $scope.Purchase.Quantity = Number(qty);

        if ($scope.btnAdd == 'Add Purchase') {
            var purchaseObj = angular.copy($scope.Purchase);
            $scope.PurchaseListTemp.push(purchaseObj);
        }
        if ($scope.btnAdd == 'Update Purchase') {
            angular.forEach($scope.PurchaseArr, function (checkData) {
                if (checkData.PurchaseId == $scope.Purchase.PurchaseId) {
                    if ((checkData.Quantity > $scope.Purchase.Quantity && checkData.UnitPrice < $scope.Purchase.UnitPrice) || (checkData.Quantity < $scope.Purchase.Quantity && checkData.UnitPrice > $scope.Purchase.UnitPrice)) {
                        alert("You can increase/decrease quantity and unit price at a time.");
                        $scope.incdec = false;
                        return;
                    }
                }
            });

            if ($scope.incdec == true) {
                $scope.Purchase.IsActive = true;
                var purchaseObj = angular.copy($scope.Purchase);
                $scope.PurchaseListTemp.push(purchaseObj);
            }
           
        }
        ResetForm();
    }
    $scope.RemovePurchase = function (purchase) {
        var index = $scope.PurchaseListTemp.indexOf(purchase);
        if (index != -1) {
            $scope.PurchaseListTemp.splice(index, 1);
        }
    }
})  