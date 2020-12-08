Utils = new function () {

    function rootUrl() {
        var rooturl = 'http://localhost:4111/';
        return rooturl;
    };

    function webUrl() {
        var rooturl = 'http://alpha.edunet.vn/';
        return rooturl;
    }

    function mediaUrl() {
        var rooturl = 'http://media.edunet.vn/';
        return rooturl;
    };

    function mediaFolder() {
        var rooturl = 'http://media.edunet.vn/Media/News/images/';
        return rooturl;
    };

    this.UrlRoot = rootUrl();

    this.webUrl = webUrl();

    this.mediaUrl = mediaUrl();

    this.mediaFolder = mediaFolder();

    this.DocumentHeght = function () {
        return $(document).height();
    };

    this.GetFullHeight = function () {
        return parseInt($(document).scrollTop() + $('html').height());
    };

    this.DocumentWidth = function () { return $(document).width(); };

    this.WindowHeight = function () { return $(window).height(); };

    this.WindowWidth = function () { return $(window).width(); };

    this.Loading = function (id) {
        if (id) {
            mApp.block('#' + id, {});
            return;
        }
        mApp.blockPage({
            overlayColor: '#000000',
            type: 'loader',
            state: 'success',
            message: 'Please wait...'
        });
    }

    this.UnLoading = function (id) {
        if (id) {
            mApp.unblock('#' + id);
            return;
        }
        mApp.unblockPage();
    };

    this.formatDate = function (value, option) {
        if (!value || value == '' || value == null)
            value = new Date();
        else
            value = new Date(value);
        var day = value.getDate();
        var month = value.getMonth() + 1;
        var year = value.getFullYear();
        var hour = value.getHours();
        var minute = value.getMinutes();
        var second = value.getSeconds();
        if (day < 10) day = "0" + day;
        if (month < 10) month = "0" + month;
        if (hour < 10) hour = "0" + hour;
        if (minute < 10) minute = "0" + minute;
        if (second < 10) second = "0" + second;
        switch (option) {
            case 0:
                return day + '/' + month + '/' + year + ' ' + hour + ':' + minute + ':' + second;
                break;
            case 1:
                return day + '/' + month + '/' + year;
                break;
            case 2:
                return hour + ':' + minute + ':' + second + ' ' + day + '/' + month + '/' + year;
                break;
            case 3:
                return year + '/' + month + '/' + day + ' ' + hour + ':' + minute + ':' + second;
                break;
            case 4:
                return year + '/' + month + '/' + day;
                break;
            case 5:
                return day + 'h' + minute;
                break;
            case 6:
                return month + '/' + year.substr(2, 2);
            default:
                return value.toString();
                break;
        }
    }

    this.formatGetDate = function (jsonDate, type) {
        if (jsonDate == null || jsonDate === "") {
            return "";
        }
        var subdate = jsonDate.substr(0, 10);
        var year = subdate.substr(0, 4);
        var month = subdate.substring(5, 7);
        var day = subdate.substr(8, 10);

        var subdate2 = jsonDate.substring(11, 19);
        var hour = subdate2.substring(0, 2);
        var minute = subdate2.substring(3, 5);
        var second = subdate2.substring(6, 8);
        if (type == 0 || type == null || type == undefined) {
            return day + "/" + month + "/" + year;
        } else {
            return day + "/" + month + "/" + year + " " + hour + ":" + minute + ":" + second;
        }
    };

    // Hàm convert chuỗi json Datetime sang Date
    // value: chuỗi jSon datetime
    this.jSonToDate = function (value) {
        value = value.replace('/Date(', '');
        value = value.replace(')/', '');
        var expDate = new Date(parseInt(value));
        return expDate;
    };

    this.jSonDateToString = function (value, option) {
        if (typeof (option) == 'undefined') {
            option = 0;
        }
        var expDate = this.jSonToDate(value);
        var day = expDate.getDate();
        var month = expDate.getMonth() + 1;
        var year = expDate.getFullYear();
        var hour = expDate.getHours();
        var minute = expDate.getMinutes();
        var second = expDate.getSeconds();
        if (day < 10) day = "0" + day;
        if (month < 10) month = "0" + month;
        if (hour < 10) hour = "0" + hour;
        if (minute < 10) minute = "0" + minute;
        if (second < 10) second = "0" + second;
        switch (option) {
            case 0:
                return day + '/' + month + '/' + year + ' ' + hour + ':' + minute + ':' + second;
                break;
            case 1:
                return day + '/' + month + '/' + year;
                break;
            case 2:
                return hour + ':' + minute + ':' + second + ' ' + day + '/' + month + '/' + year;
                break;
            case 3:
                return year + '/' + month + '/' + day + ' ' + hour + ':' + minute + ':' + second;
                break;
            case 4:
                return year + '/' + month + '/' + day;
                break;
            case 5:
                return day + 'h' + minute;
                break;
            default:
                return expDate.toString();
                break;
        }
    };

    this.getParameterByName = function (name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    };

    this.checkOnlyNumber = function (obj, e) {
        var whichCode =
            (window.Event && e.which)
                ? e.which
                : e.keyCode; /*if (whichCode == 13) { this.onPlaceOrder(); return false; }*/
        if (whichCode == 9) return true;
        if ((whichCode >= 48 && whichCode <= 57) || whichCode == 8) {
            var n = obj.value.replace(/,/g, "");
            if (whichCode == 8) {
                if (n.length != 0)
                    n = n.substr(0, n.length - 1);
            }
            if (parseFloat(n) == 0) n = '';
            return true;
        }
        e.returnValue = false;
        return false;
    };

    this.formatPhoneInput = function (ctrl, e) {
        //Check if arrow keys are pressed - we want to allow navigation around textbox using arrow keys
        var evnt = e || window.event;
        var keyCode = (window.event) ? e.keyCode : e.which;
        if (keyCode == 37 || keyCode == 38 || keyCode == 39 || keyCode == 40) {
            return;
        }
        var val = ctrl.value;
        val = val.replace(/[-]/g, "");
        ctrl.value = "";
        val += '';

        var rgx = /\D*(\d{3})/;
        if (val.length > 3 && val.length <= 6) {
            rgx = /\D*(\d{3})\D*(\d{1})/;
            val = val.replace(rgx, '$1-$2');
        } else if (val.length > 6 && val.length <= 10) {
            rgx = /\D*(\d{3})\D*(\d{3})\D*(\d{1})\D*/;
            val = val.replace(rgx, '$1-$2-$3');
        }
        if (val.length > 10) {
            rgx = /\D*(\d{4})\D*(\d{4})\D*(\d{3})\D*/;
            val = val.replace(rgx, '$1-$2-$3');
        }

        ctrl.value = val;
    };

    this.formatCurrency = function (ctrl, e) {
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
        x2 = x.length > 1 ? ',' + x[1] : '';

        var rgx = /(\d+)(\d{3})/;

        //custom dot (.) or comma(,) here
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }

        ctrl.value = x1 + x2;
    };

    this.ChangeText = function (t) {
        if (t.which == 13)
            return;

        var selector = t.currentTarget;
        $(selector).parents(".p-error-text").removeClass('help-block').html('');
        $(".add-fund .kind span.active").removeClass('active');
    };

    this.formatMoney = function (argValue) {
        if (argValue == null || argValue == "")
            return argValue;

        var str1 = argValue.toString();
        if (str1.indexOf(",") >= 0 || str1.indexOf(".") >= 0)
            str1 = argValue.replace(/[,.]/g, "");

        argValue = parseInt(str1);
        var _comma = (1 / 2 + '').charAt(1);
        var _digit = ',';
        if (_comma == '.') {
            _digit = '.';
        }

        var _sSign = "";
        if (argValue < 0) {
            _sSign = "-";
            argValue = -argValue;
        }

        var _sTemp = "" + argValue;
        var _index = _sTemp.indexOf(_comma);
        var _digitExt = "";
        if (_index != -1) {
            _digitExt = _sTemp.substring(_index + 1);
            _sTemp = _sTemp.substring(0, _index);
        }

        var _sReturn = "";
        while (_sTemp.length > 3) {
            _sReturn = _digit + _sTemp.substring(_sTemp.length - 3) + _sReturn;
            _sTemp = _sTemp.substring(0, _sTemp.length - 3);
        }
        _sReturn = _sSign + _sTemp + _sReturn;
        if (_digitExt.length > 0) {
            _sReturn += _comma + _digitExt;
        }
        return _sReturn;
    };

    this.ReplaceAll = function (sources, strTarget, strSubString) {
        var strText = sources;

        var intIndexOfMatch = strText.indexOf(strTarget);

        // Keep looping while an instance of the target string
        // still exists in the string.
        while (intIndexOfMatch != -1) {
            // Relace out the current instance.
            strText = strText.replace(strTarget, strSubString);

            // Get the index of any next matching substring.
            intIndexOfMatch = strText.indexOf(strTarget);
        }

        return (strText);
    };

    this.IsvalidateDate = function (_text) {
        var filter = /^\d{2}[\-\/.]\d{2}[\-\/.]\d{4}\s+\d{2}:\d{2}:\d{2}$/; return filter.test(_text);
    };

    this.validateDateTime = function (_text) {
        var filter = /^\d{4}[\-\/.]\d{2}[\-\/.]\d{2}T\d{2}:\d{2}:\d{2}$/; return filter.test(_text);
    };

    //Format số
    this.FormatNumber = function (_str) {
        _str += ''; x = _str.split(','); x1 = x[0]; x2 = x.length > 1 ? ',' + x[1] : ''; var rgx = /(\d+)(\d{3})/; while (rgx.test(x1)) x1 = x1.replace(rgx, '$1' + '.' + '$2'); var result = (x1 + x2).split('.'); if (result.length <= 1) return x1 + x2;
        return result[0] + '.' + parseInt(result[1].length > 2 ? result[1].substr(0, 2) : result[1]);
    };
    this.FormatNumberRate = function (_str) {
        if (parseFloat(_str) > 100)
            _str = 100;
        _str += ''; x = _str.split(','); x1 = x[0]; x2 = x.length > 1 ? ',' + x[1] : ''; var rgx = /(\d+)(\d{3})/; while (rgx.test(x1)) x1 = x1.replace(rgx, '$1' + '.' + '$2'); var result = (x1 + x2).split('.'); if (result.length <= 1) return x1 + x2;
        return result[0] + '.' + parseInt(result[1].length > 2 ? result[1].substr(0, 2) : result[1]);
    };


    // format alias
    this.format_alias = function (str, ctrl) {
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
        str = str.replace(/đ/g, "d");
        str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
        str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
        str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
        str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
        str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
        str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
        str = str.replace(/Đ/g, "D");
        str = str.replace(/Đ/g, "D");
        str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~/g, "");
        if (ctrl != null || ctrl != undefined) {
            ctrl.value = str.replace(/\s/g, "-");
        }
        return str.replace(/\s/g, "-");
    }
};

// đổi mật khẩu
$('#btn_changePass').click(function () {
    $('#m_ChangePassword').modal('show');
});

function ChangePass() {
    var passwordOld = $('#txtOldpassword').val();
    var newpassword = $('#txtNewpassword').val();
    var renewpassword = $('#txtReNewpassword').val();

    if (newpassword !== renewpassword) {
        toastr.error("Nhập lại mật khẩu mới không giống nhau. !");
        return;
    }
    var param = {
        PasswordOld: passwordOld,
        PasswordNew: newpassword
    };
    $('#btn_saveChangePass').addClass('m-loader m-loader--right m-loader--light').attr('disabled', true);
    $.ajax({
        type: 'POST',
        url: Utils.UrlRoot + "Home/ChangePass",
        data: param,
        async: true,
        success: function (data) {

            $('#btn_saveChangePass').addClass('m-loader m-loader--right m-loader--light').attr('disabled', false);

            if (data.ResponseCode >= 0) {
                toastr.success(data.Description);
                setTimeout(function () {
                    $('#m_ChangePassword').modal('hide');
                }, 2000);
                setTimeout(function () {
                    var url = location.hash;
                    window.location.href = Utils.UrlRoot + "Account/FormLogin?UrlReturn=" + url.substring(2, url.length);
                }, 4000);
            }
            else {
                toastr.error(data.Description);
                return;
            }
        }
    });
};

function Substring_textnews(str, lengsub) {
    if (str === null || str === "" || str === undefined) {
        return str;
    }
    if (lengsub > str.length) {
        return str;
    }
    var st = str.substring(0, lengsub);
    return st + " ...";
};
//