var app = angular.module("myApp", ['ngCookies']);
 app.controller("Products", function ($scope, $http, $filter, $cookieStore) {
    Clear();
    function Clear() {
        $scope.btnAdd = 'Add Product';
        $scope.Product = {};
        $scope.Product.ProductId = 0;
        $scope.totalAmount = 0;
        $scope.totalQuantity = 0;
        $scope.Product.ProductId =Number($cookieStore.get("ProductId"));
        $cookieStore.remove("ProductId");
        $scope.ddlPage = {};
        $scope.PageList = [{ PageId: 1, PageNo: '10', PageNoShow: '10' }, { PageId: 2, PageNo: '50', PageNoShow: '50' }, { PageId: 3, PageNo: '100', PageNoShow: '100' }, { PageId: 4, PageNo: '500', PageNoShow: '500' }, { PageId: 5, PageNo: '10000000', PageNoShow: 'All' }]
        $scope.ddlPage.PageNo = '10';
        $scope.ProductTypeList = [];
        $scope.ProductList = [];
        $scope.ProductListTemp = [];
        $scope.ProductForDelete = [];
        $scope.BrandList = [];
        $scope.GroupList = [];
        $scope.CategoryList = [];
        GetAllProduct();
        ClearGroup();
        ClearBrand();
        ClearType();
        ClearCategory();
      
     }
     $("#startDate").datepicker({
         dateFormat: 'mm-dd-yy'
     });
     $("#endDate").datepicker({
         dateFormat: 'mm-dd-yy'
     });
    function ClearGroup() {
        $scope.Group = {};
        $scope.Group.GroupId = 0;
        $scope.GroupList = [];
        GetAllGroup();
    }
     function ClearBrand() {
         $scope.Brand = {};
         $scope.Brand.BrandId = 0;
         $scope.BrandList = [];
         GetAllBrand();
     }
     function ClearType() {
         $scope.Type = {};
         $scope.Type.TypeId = 0;
         $scope.TypeList = [];
         GetAllProductType();
     }
     function ClearCategory() {
         $scope.Category = {};
         $scope.Category.CategoryId = 0;
         $scope.CategoryList = [];
         GetAllCategory();
     }
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
             url: "../Products/GetDetails?startDate=" + $scope.StartDate + "&endDate=" + $scope.EndDate,
         }).then(function (response) {
             angular.forEach(response.data, function (e) {
                 var res = e.AddedDate.substring(0, 5);
                 if (res == "/Date") {
                     var parsedDate = new Date(parseInt(e.AddedDate.substr(6)));
                     var date = ($filter('date')(parsedDate, 'MM/dd/yyyy')).toString();
                     e.AddedDate = date;
                 }
             });
             $scope.ProductList = response.data;
             LoadProductDetails();
         }, function () {
             alert("Error Occur");
         })

     }

    function GetAllProduct() {
        $http({
            method: "get",
            url: "../Products/GetAllProduct",
        }).then(function (response) {
            angular.forEach(response.data, function (e) {
                var res = e.AddedDate.substring(0, 5);
                if (res == "/Date") {
                    var parsedDate = new Date(parseInt(e.AddedDate.substr(6)));
                    var date = ($filter('date')(parsedDate, 'MM/dd/yyyy')).toString();
                    e.AddedDate = date;
                }
            });
            $scope.ProductList = response.data;
            LoadProductDetails();
        }, function () {
            alert("Error Occur");
        })

    }
    function LoadProductDetails() {
        if ($scope.Product.ProductId != 0) {
            angular.forEach($scope.ProductList, function (data) {
                if (data.ProductId == $scope.Product.ProductId) {
                    $scope.btnAdd = 'Update Product';
                    data.AddedDate = new Date(data.AddedDate);
                    $scope.Product = data;
                    $scope.ddlBrand = { BrandId: data.BrandId };
                    $scope.ddlCategory = { CategoryId: data.CategoryId };
                    $scope.ddlType = { TypeId: data.TypeId };
                }
            });
           
        }
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
    function GetAllCategory() {
        $http({
            method: "get",
            url: "../Products/GetAllCategory"
        }).then(function (response) {
            $scope.CategoryList = response.data;
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

    $scope.GetGroupId = function () {
        var val = $('#GroupName').val()
        var xyz = $('#groups option').filter(function () {
            return this.value == val;
        }).data('xyz');
        $scope.Product.GroupName = val;
        $scope.Product.GroupId = xyz;

    }
   
    $scope.SaveProduct = function () {
        $http({
            method: "post",
            url: "../Products/SaveProduct",
            datatype: "json",
            data: JSON.stringify($scope.ProductListTemp)
        }).then(function (response) {
            alert("Product Saved Successfully");
            Clear();
            window.location.href = '/Products/Index';
        }, function () {
            alert("Error Occur");
        })
    }
    $scope.DeleteProduct = function (product) {
        var r = confirm("Are you sure you want to delete?");
        if (r == true) {
            product.IsActive = false;
            $scope.ProductForDelete.push(product);
            $http({
                method: "post",
                url: "../Products/SaveProduct",
                datatype: "json",
                data: JSON.stringify($scope.ProductForDelete)
            }).then(function (response) {
                alert("Product Deleted Successfully");
                Clear();
            }, function () {
                alert("Error Occur");
            })
        } else {
            return;
        }
        
    }
    $scope.SaveGroup = function () {
        $http({
            method: "post",
            url: "../Products/SaveGroup",
            datatype: "json",
            data: JSON.stringify($scope.Group)
        }).then(function (response) {
         
            ClearGroup();
        }, function () {
            alert("Error Occur");
        })
     }
     $scope.SaveCategory = function () {
         $http({
             method: "post",
             url: "../Products/SaveCategory",
             datatype: "json",
             data: JSON.stringify($scope.Category)
         }).then(function (response) {

             ClearCategory();
         }, function () {
             alert("Error Occur");
         })
     }
     $scope.SaveBrand = function () {
         $http({
             method: "post",
             url: "../Products/SaveBrand",
             datatype: "json",
             data: JSON.stringify($scope.Brand)
         }).then(function (response) {

             ClearBrand();
         }, function () {
             alert("Error Occur");
         })
     }
     $scope.SaveType = function () {
         $http({
             method: "post",
             url: "../Products/SaveType",
             datatype: "json",
             data: JSON.stringify($scope.Type)
         }).then(function (response) {

             ClearType();
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
        window.location.href = '/Products/Create';
    }
    $scope.RedirectToList = function () {
        window.location.href = '/Products/Index';
    }
    $scope.EditProduct = function (product) {
        $cookieStore.put("ProductId", product.ProductId);
        window.location.href = '/Products/Create';
    }
$scope.ResetForm = function () {
    ResetForm();
}
$scope.ResetGroupForm = function () {
    ResetGroupForm();
}
function ResetGroupForm() {
    ClearGroup();
}
     $scope.ResetBrandForm = function () {
         ResetBrandForm();
     }
     function ResetBrandForm() {
         ClearBrand();
     }
     $scope.ResetTypeForm = function () {
         ResetTypeForm();
     }
     function ResetTypeForm() {
         ClearType();
     }
     $scope.ResetCategoryForm = function () {
         ResetCategoryForm();
     }
     function ResetCategoryForm() {
         ClearCategory();
     }

function ResetForm() {
    $scope.btnAdd = 'Add Product';
    $scope.Product = {};
    $scope.Product.ProductId = 0;
    $scope.ddlCategory = null;
    $scope.ddlBrand = null;
    $scope.ddlType = null;
}
    $scope.AddProduct = function () {
        $scope.Product.IsActive = true;
    if ($scope.btnAdd == 'Add Product') {
        var productAdd = angular.copy($scope.Product);
        $scope.ProductListTemp.push(productAdd);
    }
    if ($scope.btnAdd == 'Update Product') {
        var productAdd = angular.copy($scope.Product);
        $scope.ProductListTemp.push(productAdd);
    }

    ResetForm();
}
$scope.RemoveProduct = function (product) {
    var index = $scope.ProductListTemp.indexOf(product);
    if (index != -1) {
        $scope.ProductListTemp.splice(index, 1);
    }
}
   
})  