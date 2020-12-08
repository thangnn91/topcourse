
function recursive(bool, dataChild, inputCheckAll, inputCheck) {
    if (dataChild == null || dataChild == undefined || dataChild.length == 0)
        return;
    if (bool == null || bool == undefined)
        bool = true;
    $.each(dataChild, function () {
        if ($(this).treegrid('isLeaf')) {
            if (bool) {
                $(this).find('input.' + inputCheck).prop('checked', true);
                var a = $(this).find('input.IsView');
                $(this).find('input.IsView').prop('checked', true);
            }
            else {
                $(this).find('input.' + inputCheck).prop('checked', false);
            }
        }
        else {
            var item = $(this).find('input.' + inputCheckAll);
            if (bool) {
                item.prop('checked', true);
                $(this).find('input.CheckAllView').prop('checked', true);
            }
            else {
                item.prop('checked', false);
            }
            var id = $(this).find('input.' + inputCheckAll).attr('id');
            var listchild = $('.treegrid-' + id).treegrid('getChildNodes');
            recursive(bool, listchild, inputCheckAll, inputCheck);
        }
    });
}

function parentCheck($parent, $check, inputCheckAll, inputCheck) {
    if ($parent == null || $parent == undefined)
        return;
    var parent = $parent.treegrid('getParentNode');

    var id = $parent.treegrid('getNodeId');
    var parentId = $('.treegrid-' + id).treegrid('getParentNodeId');
    if (!$check) {
        $parent.find(inputCheckAll).prop('checked', false);
        if (parent != null && parent != undefined)
            parentCheck(parent, $check, inputCheckAll, inputCheck);
        else return;
    }
    else {
        var check = true;
        var children = $parent.treegrid('getChildNodes');
        $.each(children, function () {
            var $this = $(this);
            var checkChild = false;
            if ($this.treegrid('isLeaf')) {
                checkChild = $this.find(inputCheck).prop('checked');
            }
            else {
                checkChild = $this.find(inputCheckAll).prop('checked');
            }
            if (!checkChild) {
                check = false;
            }
        });
        $parent.find(inputCheckAll).prop('checked', check);
        if (parent != null && parent != undefined)
            parentCheck(parent, $check, inputCheckAll, inputCheck);
        else return;
    }
}

function rowspanDTable(oSettings, divId, time) { // time = 1 (theo giờ) = 0 theo ngày, sum = 1 (sum dòng) = 0 ( bỏ sum )
    if (time == null || time == undefined || time == "") {
        time = -1;
    }
    if (oSettings.aiDisplay.length <= 0)
        return;
    $("#" + divId + " tbody tr td").removeAttr("hidden").removeAttr("rowspan");
    for (i = 0; i < oSettings.nTBody.childElementCount; i++) {
        for (j = 0; j < oSettings.nTBody.rows[i].childElementCount; j++) {
            var count = 1;
            var cellIndex = parseInt(oSettings.nTBody.rows[i].cells[j]._DT_CellIndex.column);
            if (oSettings.aoColumns[cellIndex].sType != "num" && oSettings.aoColumns[cellIndex].sType != "num-fmt") {
                if (oSettings.aoColumns[cellIndex].sType == "date") {
                    var strdate = oSettings.nTBody.rows[i].cells[j].textContent.toString().trim();
                    var n = strdate.indexOf("T");
                    if (n >= 0) {
                        if (n > 0) {
                            strdate = strdate.replace("T", " ");// replace time
                        }
                        else {
                            strdate = strdate.replace("T", "");// replace time
                        }
                        if (parseInt(time) == 0) { //ngày

                            var date = Utils.dateTypeFormatter(strdate, 0);
                            $('#' + divId + ' tbody tr:eq(' + i + ') td:eq(' + j + ')').text(date);
                        }
                        else if (parseInt(time) == 2) {
                            var date = Utils.dateTypeFormatter(strdate, 2);
                            $('#' + divId + ' tbody tr:eq(' + i + ') td:eq(' + j + ')').text(date);
                        }
                        else { // giờ
                            var date = Utils.dateTypeFormatter(strdate, 1);
                            $('#' + divId + ' tbody tr:eq(' + i + ') td:eq(' + j + ')').text(date);
                        }
                    }
                }
                if (i == 0) {
                    for (index = i + 1; index < oSettings.nTBody.childElementCount; index++) {
                        var strdate = oSettings.nTBody.rows[index].cells[j].textContent.toString();
                        if (oSettings.aoColumns[cellIndex].sType == "date") {
                            var n = strdate.indexOf("T");
                            if (n >= 0) {
                                strdate = strdate.replace("T", " ");// replace time
                                if (parseInt(time) == 0) {
                                    strdate = Utils.dateTypeFormatter(strdate, 0);
                                    //$('#' + divId + ' tbody tr:eq(' + i + ') td:eq(' + j + ')').text(date);
                                }
                                else if (parseInt(time) == 2) {
                                    strdate = Utils.dateTypeFormatter(strdate, 2);
                                }
                                else {
                                    strdate = Utils.dateTypeFormatter(strdate, 1);
                                    //$('#' + divId + ' tbody tr:eq(' + i + ') td:eq(' + j + ')').text(date);
                                }
                            }
                        }
                        if (oSettings.nTBody.rows[i].cells[j].textContent != strdate)
                            break;
                        count++;
                    }
                    if (count > 1) {
                        $('#' + divId + ' tbody tr:eq(' + i + ') td:eq(' + j + ')').attr("rowspan", count);
                    }
                }
                else {
                    if (oSettings.nTBody.rows[i - 1].cells[j].textContent == oSettings.nTBody.rows[i].cells[j].textContent)
                        $('#' + divId + ' tbody tr:eq(' + i + ') td:eq(' + j + ')').attr("hidden", "true");
                    else {
                        for (index = i + 1; index < oSettings.nTBody.childElementCount; index++) {
                            var strdate = oSettings.nTBody.rows[index].cells[j].textContent.toString();
                            if (oSettings.aoColumns[cellIndex].sType == "date") {
                                var n = strdate.indexOf("T");
                                if (n >= 0) {
                                    strdate = strdate.replace("T", " ");// replace time
                                    if (parseInt(time) == 0) {
                                        strdate = Utils.dateTypeFormatter(strdate, 0);
                                        //$('#' + divId + ' tbody tr:eq(' + i + ') td:eq(' + j + ')').text(date);
                                    }
                                    else if (parseInt(time) == 2) {
                                        strdate = Utils.dateTypeFormatter(strdate, 2);
                                    }
                                    else {
                                        strdate = Utils.dateTypeFormatter(strdate, 1);
                                        //$('#' + divId + ' tbody tr:eq(' + i + ') td:eq(' + j + ')').text(date);
                                    }
                                }
                            }
                            if (oSettings.nTBody.rows[i].cells[j].textContent != strdate)
                                break;
                            count++;
                        }
                        if (count > 1) {
                            $('#' + divId + ' tbody tr:eq(' + i + ') td:eq(' + j + ')').attr("rowspan", count);
                        }
                    }
                }
            }
            else {
                var number = oSettings.nTBody.rows[i].cells[j].textContent.toString();
                if (number != "" && number != null) {
                    var n = number.indexOf(" ");
                    if (n < 0) {
                        number = Utils.formatMoney(number);
                        $('#' + divId + ' tbody tr:eq(' + i + ') td:eq(' + j + ')').css("text-align", "right").text(number);
                    }
                    else {
                        $('#' + divId + ' tbody tr:eq(' + i + ') td:eq(' + j + ')').text(number);
                    }
                }
            }
        }

    }
}

function jSonToDate(value) {
    value = value.replace('/Date(', '');
    value = value.replace(')/', '');
    var expDate = new Date(parseInt(value)); return expDate;
};

function jSonDateToString(value, option) {
    debugger;
    if (typeof (option) == 'undefined') {
        option = 0;
    }
    var expDate = jSonToDate(value);
    var _day = expDate.getDate();
    var _month = expDate.getMonth() + 1;
    var _year = expDate.getFullYear();
    var _hour = expDate.getHours();
    var _minute = expDate.getMinutes();
    var _second = expDate.getSeconds();
    if (_day < 10) _day = "0" + _day;
    if (_month < 10) _month = "0" + _month;
    if (_hour < 10) _hour = "0" + _hour;
    if (_minute < 10) _minute = "0" + _minute;
    if (_second < 10) _second = "0" + _second;
    if (_year <= 1900 || _year >= 9000) return '';
    switch (option) {
        case 0:
            return _day + '/' + _month + '/' + _year + ' ' + _hour + ':' + _minute + ':' + _second;
            break;
        case 1:
            return _day + '/' + _month + '/' + _year;
            break;
        case 2:
            return _hour + ':' + _minute + ':' + _second + ' ' + _day + '/' + _month + '/' + _year;
            break;
        case 3:
            return _year + '/' + _month + '/' + _day + ' ' + _hour + ':' + _minute + ':' + _second;
            break;
        case 4:
            return _year + '/' + _month + '/' + _day;
            break;
        case 5:
            return _day + 'h' + _minute;
            break;
        default:
            return expDate.toString();
            break;
    };

};

// Lấy Category Theo Language
function LoadCategoryByLanguage(systemid, language) {
    $("#ddlParentId").html('');
    $.ajax({
        type: 'GET',
        url: Utils.UrlRoot + "Common/GetCategoryByLanguage",
        data: {
            systemId: systemid,
            languageId: language
        },
        success: function (data) {
            var allobj = "<option value='" + 0 + "'>" + 'Chức năng' + "</option>";
            $("#ddlParentId").append(allobj);
            $.each(data.item, function (key, val) {
                var obj = "";
                obj = "<option value='" + val.Id + "'" + " title='" + val.CategoryName + "'" + ">" + val.CategoryName + "</option>";
                $("#ddlParentId").append(obj);
            });
         
        }
    });
};

function LoadTreeCategoryByLanguageIn(systemid, language) {
    var listCategory = [];
    $("#contextCategoryIn").val('chọn chuyên mục');
    $.ajax({
        type: 'GET',
        url: Utils.UrlRoot + "Common/GetTreeCategoryByLanguage",
        data: {
            systemId: systemid,
            languageId: language
        },
        success: function (data) {
            if (data.status > 0 && data.item.length > 0) {
                listCategory = data.item;
                console.log(data.item);
            }
            var treeCategory = $('#treeCategoryIn').treeview({
                color: "#428bca",
                enableAction: false,
                nodeIcon: "fa fa-shopping-cart",
                collapseIcon: "fa fa-chevron-down",
                expandIcon: "fa	fa-chevron-right",
                selectedColor: "white",
                data: listCategory,
                showCheckbox: true,
                levels: 1,
                checkAllNode: false,
                showTags: true
            });
        }
    });
};

function LoadTreeCategoryMain(systemid, language) {
    var listCategory = [];
    $("#contextCategoryMain").val('chọn chuyên mục chính');
    $.ajax({
        type: 'GET',
        url: Utils.UrlRoot + "Common/GetTreeCategoryByLanguage_NoSelect",
        data: {
            systemId: systemid,
            languageId: language
        },
        success: function (data) {
            if (data.status > 0 && data.item.length > 0) {
                listCategory = data.item;
                console.log(data.item);
            }
            var treeCategoryMain = $('#treeCategoryMain').treeview({
                color: "#428bca",
                enableAction: false,
                nodeIcon: "fa fa-shopping-cart",
                collapseIcon: "fa fa-chevron-down",
                expandIcon: "fa	fa-chevron-right",
                selectedColor: "white",
                data: listCategory,
                showCheckbox: false,
                levels: 2,
                checkAllNode: false,
                showTags: false,
                onNodeSelected: function (event, dataTree) {
                    debugger;
                    console.log(dataTree);
                    var categoryId = dataTree.CategoryId;
                    var categoryName = dataTree.text;
                    $("#contextCategoryMain").val(categoryName);//lôi giá trị ra button
                    $("#dllCategoryMainId").val(categoryId);// lấy id

                    // Cate chính
                },
                onNodeUnselected: function (event, dataTree) {
                    debugger;
                    var categoryId = 0;
                    var categoryName = "Chọn chuyên mục chính";
                    $("#contextCategoryMain").val(categoryName);
                    $("#dllCategoryMainId").val(categoryId);// lấy id

                }
            });
        }
    });
};

//Chọn nhiều chuyên mục
function LoadTreeCategoryByLanguage(systemid, language) {
    var listCategory = [];
    $("#contextCategory").val('chọn chuyên mục');
    $.ajax({
        type: 'GET',
        url: Utils.UrlRoot + "Common/GetTreeCategoryByLanguage",
        data: {
            systemId: systemid,
            languageId: language
        },
        success: function (data) {
            if (data.status > 0 && data.item.length > 0) {
                listCategory = data.item;
                console.log(data.item);
            }
            var tree = $('#treeCategory').treeview({
                color: "#428bca",
                enableAction: false,
                nodeIcon: "fa fa-shopping-cart",
                collapseIcon: "fa fa-chevron-down",
                expandIcon: "fa	fa-chevron-right",
                selectedColor: "white",
                data: listCategory,
                showCheckbox: true,
                levels: 1,
                checkAllNode: true,
                showTags: true
            });
        }
    });
};

// Chọn 1 chuyên mục
function LoadTreeCategoryByLanguageSelected(systemid, language) {
    var listCategory = [];
    $("#contextCategory").val('chọn chuyên mục');
    $.ajax({
        type: 'GET',
        url: Utils.UrlRoot + "Common/GetTreeCategoryByLanguage_NoSelect",
        data: {
            systemId: systemid,
            languageId: language
        },
        success: function (data) {
            if (data.status > 0 && data.item.length > 0) {
                listCategory = data.item;
                console.log(data.item);
            }
            $('#treeCategory').treeview({
                color: "#428bca",
                enableAction: false,
                nodeIcon: "fa fa-shopping-cart",
                collapseIcon: "fa fa-chevron-down",
                expandIcon: "fa	fa-chevron-right",
                selectedColor: "white",
                data: listCategory,
                levels: 2,
                checkAllNode: false,
                showTags: false,
                onNodeSelected: function (event, dataTree) {
                    var categoryId = dataTree.CategoryId;
                    var categoryName = dataTree.text;
                    $("#contextCategory").val(categoryName);//lôi giá trị ra button
                    $("#dllCategoryId").val(categoryId);// lấy id
                },
                onNodeUnselected: function (event, dataTree) {
                    var categoryId = 0;
                    var categoryName = "Chọn chuyên mục";
                    $("#contextCategory").val(categoryName);
                    $("#dllCategoryId").val(categoryId);// lấy id
                }
            });
        }
    });
};

function CheckNumeric(e) {
    var keyCode = (window.event) ? e.keyCode : e.which;
    return (keyCode >= 48 && keyCode <= 57) || keyCode === 8;
}

function FormatCurrency(ctrl, e) {
    //Check if arrow keys are pressed - we want to allow navigation around textbox using arrow keys
    var evnt = e || window.event;
    var keyCode = (window.event) ? e.keyCode : e.which;
    if (keyCode == 37 || keyCode == 38 || keyCode == 39 || keyCode == 40) {
        return;
    }
    var val = ctrl.value;
    val = val.replace(/[.,]/g, "");
    ctrl.value = "";
    val += '';
    x = val.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';

    var rgx = /(\d+)(\d{3})/;

    //custom dot (.) or comma(,) here
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + '.' + '$2');
    }

    ctrl.value = x1 + x2;
}
