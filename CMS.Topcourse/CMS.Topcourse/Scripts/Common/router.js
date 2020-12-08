var route = location.pathname + location.search;

var hideAll = function () {
    $('#div_content').hide();
    active_MenuLeft();
}

var active_MenuLeft = function () {
    $('#m_ver_menu').find('li').removeClass('m-menu__item--active');
    var pathname = location.hash;
    var detectPath = pathname.split('/');
    if (detectPath != null && detectPath.length > 0) {
        var actionName = detectPath[detectPath.length - 1];
        if (actionName == "")
            return;
        var $f_Active = $("[data-action='" + actionName + "']");
        if ($f_Active.length <= 0)
            return;
        $f_Active.addClass("m-menu__item--active");

        setTimeout(function () {
            $f_Active.parent().parent().parent().addClass('m-menu__item--open');
            $f_Active.parent().parent().parent().parent().parent().parent().addClass('m-menu__item--open');
        }, 100);
    }
}

var checkAuthenticate = function () {
    $.ajax({
        type: 'POST',
        url: Utils.UrlRoot + "Account/CheckAuthen",
        data: {},
        success: function (data) {
            console.log(data);
            if (data.statusCode >= 0) {
            }
            else {
                var url = location.hash;
                window.location.href = Utils.UrlRoot + "Account/FormLogin?UrlReturn=" + url.substring(2, url.length);
            }
        }
    });
};

var app = $.sammy(function () {
    this.get('/', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Home/DashBoard";
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: "",
            success: function (data) {
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    // chức năng hệ thống

    // quản trị chức năng
    this.get('/Home/ManagerFunction', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Home/ManagerFunction";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: "",
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    this.get('/#/Home/GetFunctionInfo', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Home/GetFunctionInfo";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
                id: 0
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    this.get('/#/Home/GetFunctionInfo/:id', function () {
        var id = this.params['id'];
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Home/GetFunctionInfo";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
                id: id
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    this.get('/#/Home/FunctionOrder', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Home/FunctionOrder";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });
    //


    // Quản trị người dùng
    this.get('/#/Home/ManagerUser', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Home/ManagerUser";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    //
    this.get('/#/Home/GetUserInfo/:id', function () {
        var id = this.params['id'];
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Home/GetUserInfo";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
                id: id
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    // phân quyền người dùng
    this.get('/#/Home/GetGrantUser/:id', function () {
        var id = this.params['id'];
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Home/GetGrantUser";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
                id: id
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    // Log người dùng
    this.get('/#/Home/ManagerUserLog', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Home/ManagerUserLog";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    // chức năng hệ thống
    this.get('/#/Home/System', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Home/System";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    // chức năng nhóm người dùng
    this.get('/#/Home/GroupUser', function () {
        var id = this.params['id'];
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Home/GroupUser";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
                id: id
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    // thêm sửa nhóm người dùng
    this.get('/#/Home/GroupUser_GetInfo/:id', function () {
        var id = this.params['id'];
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Home/GroupUser_GetInfo";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
                id: id
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    // tài khoản học viên
    this.get('/#/UserAccount/Account', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "UserAccount/Account";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });


    // tài khoản trung tâm
    this.get('/#/UserAccount/Account_Education', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "UserAccount/Account_Education";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    this.get('/#/UserAccount/Account_Education_GetInfo/:id', function () {
        var id = this.params['id'];
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "UserAccount/Account_Education_GetInfo";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
                id: id
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });


    // Khóa  ngắn hạn
    this.get('/#/Course/Course_Short', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Course/Course_Short";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    this.get('/#/Course/Course_Short_GetInfo/:id', function () {
        var id = this.params['id'];
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Course/Course_Short_GetInfo";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
                id: id
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });


    // Khóa dài hạn
    this.get('/#/Course/Course_Long', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Course/Course_Long";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    this.get('/#/Course/Course_Long_GetInfo/:id', function () {
        var id = this.params['id'];
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Course/Course_Long_GetInfo";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
                id: id
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });


    // học bổng
    this.get('/#/Course/Scholarship', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Course/Scholarship";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    this.get('/#/Course/Scholarship_GetInfo/:id', function () {
        var id = this.params['id'];
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Course/Scholarship_GetInfo";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
                id: id
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });


    // trường học
    this.get('/#/Education/Education', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Education/Education";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    this.get('/#/Education/Education_GetInfo/:id', function () {
        var id = this.params['id'];
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Education/Education_GetInfo";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
                id: id
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    // danh muc
    this.get('/#/Course/Specility', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Course/Specility";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    // Comment 
    this.get('/#/UserAccount/Comment', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "UserAccount/Comment";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });


    //Group Tag
    this.get('/#/Tag/TagGroup', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Tag/TagGroup";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    this.get('/#/Tag/TagGroup_Position', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Tag/TagGroup_Position";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });


    // Tag
    this.get('/#/Tag/Tag', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Tag/Tag";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });
    // sắp xếp
    this.get('/#/Tag/Tag_Position', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Tag/Tag_Position";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });


    // Đăng ký trực tuyến (Log)
    this.get('/#/Course/Register_Course', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Course/Register_Course";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    // liên hệ trường - cơ sở 
    this.get('/#/Course/Education_Contact', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Course/Education_Contact";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    // quản trị thanh toán
    this.get('/#/UserAccount/PaymentOrder', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "UserAccount/PaymentOrder";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    // Log đk cơ sở
    this.get('/#/Course/RegisterEdu', function () {
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Course/RegisterEdu";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

    this.get('/#/Course/RegisterEdu_GetInfo/:id', function () {
        var id = this.params['id'];
        hideAll();
        var urlRequestAns = Utils.UrlRoot + "Course/RegisterEdu_GetInfo";
        Utils.Loading();
        $.ajax({
            type: 'GET',
            url: urlRequestAns,
            data: {
                id: id
            },
            success: function (data) {
                Utils.UnLoading();
                if (data.redirecturl) {
                    var url = location.hash;
                    window.location.href = data.redirecturl + "?UrlReturn=" + url.substring(2, url.length);;
                } else {
                    $("#div_content").html(data);
                    $("#div_content").fadeIn();
                }
            },
            error: function () {
            }
        });
    });

}).run(route);

