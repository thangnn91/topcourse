var userInfoData = {};
var listCourse = {};
var customSearch = { IsFeatured: null, NumRate: 99, IsHot: null };
window.utils = {
    rootUrl: function () {
        var rooturl = 'https://edunet.vn/';
        //var rooturl = 'http://171.244.10.27:8080/';
        if (location.host.toString().indexOf('localhost') >= 0) { rooturl = 'http://localhost:1993/'; }
        return rooturl;
    },
    translateLang: function (namespace) {
        //test i18n
        i18n.setDefaultNamespace(namespace); //set file resource để sử dụng cho view tương ứng
        $(".translang").i18n(); //dịch
    },
    validateEmail: function (email) { var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/; return filter.test(email); },
    loading: function () {
        this.unLoading();
        var html = '<div id="LoadingContainer" class="loading">';
        html += '<div class = "nenxam"></div>';
        html += '<div class = "load"><span></span><em>Loading...</em></div>';
        html += '<style> #LoadingContainer{	position: fixed;top: 0;left: 0;width: 100%;height: 100%;z-index: 10001;}#LoadingContainer .nenxam{position: absolute;top: 0;left: 0;width: 100%;height: 100%;background: rgba(255,255,255,0.95);}';
        html += '#LoadingContainer .load{text-align: center;position: absolute;top: 50%;left: 50%;-webkit-transform: translate(-50%,-50%);-moz-transform: translate(-50%,-50%);-o-transform: translate(-50%,-50%);transform: translate(-50%,-50%);}';
        html += '#LoadingContainer .load span{height: 100px;width: 100px;border: 2px solid #3881ff;border-right-color: transparent;-moz-border-radius: 50%;border-radius: 50%;display: inline-block;-webkit-animation: rotate-forever 0.75s linear infinite;-moz-animation: rotate-forever 0.75s linear infinite;-o-animation: rotate-forever 0.75s linear infinite;animation: rotate-forever 0.75s linear infinite;}';
        html += '#LoadingContainer .load em{display: block;color: #a5a5a5;margin-top: 15px;font-style: normal;font-size: 14.5px;}';
        html += '</style></div>';
        $('body').append(html);
    },

    unLoading: function () {
        $('#LoadingContainer').remove();
    },
    setCookie: function (cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    },
    getCookie: function (name) {
        var value = '; ' + document.cookie;
        var parts = value.split('; ' + name + '=');
        if (parts.length == 2) return parts.pop().split(';').shift();
    },
    // lấy ngôn ngữ hiện tại
    getCurrentLanguage: function () {
        var lang = utils.getCookie('culture');
        if (!lang) {
            lang = 'vi';
        }
        return lang;
    },

    getParameterByName: function (name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    },
    formatMoney: function (argValue) {
        var comma = (1 / 2 + '').charAt(1);
        var digit = ',';
        if (comma == '.') {
            digit = '.';
        }

        var sSign = "";
        if (argValue < 0) {
            sSign = "-";
            argValue = -argValue;
        }

        var sTemp = "" + argValue;
        var index = sTemp.indexOf(comma);
        var digitExt = "";
        if (index != -1) {
            digitExt = sTemp.substring(index + 1);
            sTemp = sTemp.substring(0, index);
        }

        var sReturn = "";
        while (sTemp.length > 3) {
            sReturn = digit + sTemp.substring(sTemp.length - 3) + sReturn;
            sTemp = sTemp.substring(0, sTemp.length - 3);
        }
        sReturn = sSign + sTemp + sReturn;
        if (digitExt.length > 0) {
            sReturn += comma + digitExt;
        }
        return sReturn;
    },
    detectMobile: function () {
        if (navigator.userAgent.match(/Android/i)
            || navigator.userAgent.match(/webOS/i)
            || navigator.userAgent.match(/iPhone/i)
            || navigator.userAgent.match(/iPad/i)
            || navigator.userAgent.match(/iPod/i)
            || navigator.userAgent.match(/BlackBerry/i)
            || navigator.userAgent.match(/Windows Phone/i)
        ) {
            return true;
        }
        else {
            return false;
        }
    },
    //Hàm xử lý hệ thống
    registerStudent: function () {
        $('.center-error').hide();
        $('input').removeClass('input-error');
        $(".label-error").remove();
        var username = $('#reg_username').val();
        if (!username) {
            $('#reg_username').addClass('input-error');
            $('#reg_username').parent('.login-input-box').after("<span class='label-error'>Vui lòng nhập tài khoản</p>");
            return;
        }
        if (!utils.validateEmail(username)) {
            $('#reg_username').addClass('input-error');
            $('#reg_username').parent('.login-input-box').after("<span class='label-error'>Email không hợp lệ</p>");
            return;
        }
        var password = $('#reg_pass').val();
        if (!password) {
            $('#reg_pass').addClass('input-error');
            $('#reg_pass').parent('.login-input-box').after("<span class='label-error'>Vui lòng nhập mật khẩu</p>");
            return;
        }
        if (password.length < 6 || password.length > 18) {
            $('#reg_pass').addClass('input-error');
            $('#reg_pass').parent('.login-input-box').after("<span class='label-error'>Mật khẩu từ 6-18 ký tự</p>");
            return;
        }
        var repassword = $('#reg_repass').val();
        if (!repassword) {
            $('#reg_repass').addClass('input-error');
            $('#reg_repass').parent('.login-input-box').after("<span class='label-error'>Vui lòng nhập lại mật khẩu</p>");
            return;
        }

        if (password !== repassword) {
            $('#reg_repass').addClass('input-error');
            $('#reg_repass').parent('.login-input-box').after("<span class='label-error'>Mật khẩu nhập lại không hợp lệ</p>");
            return;
        }
        var mobile = $('#reg_mobile').val();
        if (!mobile) {
            $('#reg_mobile').addClass('input-error');
            $('#reg_mobile').parent('.login-input-box').after("<span class='label-error'>Vui lòng nhập số điện thoại</p>");
            return;
        }
        var firstname = $('#reg_firstname').val();
        if (!firstname) {
            $('#reg_firstname').addClass('input-error');
            $('#reg_firstname').parent('.login-input-box').after("<span class='label-error'>Vui lòng nhập tên và tên đệm</p>");
            return;
        }
        var lastname = $('#reg_lastname').val();
        if (!lastname) {
            $('#reg_lastname').addClass('input-error');
            $('#reg_lastname').parent('.login-input-box').after("<span class='label-error'>Vui lòng nhập họ của bạn</p>");
            return;
        }
        var recaptcha = $('#g-recaptcha-response').val();
        if (!recaptcha) {
            $('.g-recaptcha').after("<span class='label-error'>Vui lòng nhập mã kiểm tra</p>");
            return;
        }

        var param = {
            Username: username,
            Password: password,
            Mobile: mobile,
            Firstname: firstname,
            Lastname: lastname,
            CaptchaResponse: recaptcha
        }
        utils.loading();
        $.post(utils.rootUrl() + "Home/RegisterStudent", param).done(function (data) {
            utils.unLoading();
            if (data.ResponseCode >= 0) {
                $('#register-form .center-error').text('Đăng ký thành công, vui lòng vào email để kích hoạt tài khoản').show();
            } else {
                var des = common.getDescription(data.ResponseCode);
                $('#register-form .center-error').text(des).show();
                grecaptcha.reset();
            }
        }).fail(function () {
            utils.unLoading();
        });
    },

    authentication: function () {
        $('.center-error').hide();
        $('input').removeClass('input-error');
        $(".label-error").remove();
        var username = $('#login_username').val();
        if (!username) {
            $('#login_username').addClass('input-error');
            $('#login_username').parent('.login-input-box').after("<span class='label-error'>Vui lòng nhập tài khoản</p>");
            return;
        }

        var password = $('#login_pass').val();
        if (!password) {
            $('#login_pass').addClass('input-error');
            $('#login_pass').parent('.login-input-box').after("<span class='label-error'>Vui lòng nhập mật khẩu</p>");
            return;
        }

        var param = {
            Username: username,
            Password: password
        };
        utils.loading();
        $.post(utils.rootUrl() + "Home/UserAuthen", param).done(function (data) {
            utils.unLoading();
            if (data.ResponseCode >= 0) {
                window.location.href = utils.rootUrl();
            } else {
                var des = common.getDescription(data.ResponseCode);
                $('#login-form .center-error').text(des).show();
            }
        }).fail(function () {
            utils.unLoading();
        });
    },
    logout: function () {
        $.get(utils.rootUrl() + "Home/Logout").done(function (data) {
            window.location.href = utils.rootUrl();
        }).fail(function () {
        });
    },
    sendRequestChangePass: function () {
        $('.center-error').hide();
        $('input').removeClass('input-error');
        $(".label-error").remove();
        var username = $('#forgetpass_username').val();
        if (!username) {
            $('#forgetpass_username').addClass('input-error');
            $('#forgetpass_username').parent('.login-input-box').after("<span class='label-error'>Vui lòng nhập email</p>");
            return;
        }

        var param = {
            Username: username
        };

        utils.loading();
        $.post(utils.rootUrl() + "Home/SendRequestChangePassword", param).done(function (data) {
            utils.unLoading();
            if (data.ResponseCode >= 0) {
                $('#forget-form .center-error').text('Gửi yêu cầu lấy lại mật khẩu thành công, vui lòng vào email xác nhận').show();
            } else {
                var des = common.getDescription(data.ResponseCode);
                $('#forget-form .center-error').text(des).show();
            }
        }).fail(function () {
            utils.unLoading();
        });
    },
    forgetPassUpdateData: function () {
        $('.center-error').hide();
        $('input').removeClass('input-error');
        $(".label-error").remove();
        var password = $('#forgetpass_newpass').val();
        if (!password) {
            $('#forgetpass_newpass').addClass('input-error');
            $('#forgetpass_newpass').after("<span class='label-error'>Vui lòng nhập mật khẩu mới</p>");
            return;
        }
        var repassword = $('#forgetpass_renewpass').val();
        if (!repassword) {
            $('#forgetpass_renewpass').addClass('input-error');
            $('#forgetpass_renewpass').after("<span class='label-error'>Vui lòng nhập lại mật khẩu mới</p>");
            return;
        }
        if (repassword != password) {
            $('#forgetpass_renewpass').addClass('input-error');
            $('#forgetpass_renewpass').after("<span class='label-error'>Mật khẩu nhập lại không chính xác</p>");
            return;
        }
        var param = {
            password: password,
            securecode: utils.getParameterByName('securecode'),
            data: utils.getParameterByName('data'),
            sign: utils.getParameterByName('sign'),
        };

        utils.loading();
        $.post(utils.rootUrl() + "Home/ChangePasswordForgetAction", param).done(function (data) {
            utils.unLoading();
            if (data.ResponseCode >= 0) {
                $('#forget-form-update .center-error').text('Cập nhật mật khẩu thành công').show();
            } else {
                var des = common.getDescription(data.ResponseCode);
                $('#forget-form-update .center-error').text(des).show();
            }
        }).fail(function () {
            utils.unLoading();
        });
    },
    updateProfile: function () {
        $('.center-error').hide();
        $('input').removeClass('input-error');
        $(".label-error").remove();
        var fullname = $('#profile_fullname').val();
        if (!fullname) {
            $('#profile_fullname').addClass('input-error');
            $('#profile_fullname').after("<span class='label-error'>Vui lòng nhập họ tên</p>");
            return;
        }
        var mobile = $('#profile_mobile').val();
        if (!mobile) {
            $('#profile_mobile').addClass('input-error');
            $('#profile_mobile').after("<span class='label-error'>Vui lòng nhập số điện thoại</p>");
            return;
        }
        var address = $('#profile_address').val();
        if (!address) {
            $('#profile_address').addClass('input-error');
            $('#profile_address').after("<span class='label-error'>Vui lòng nhập địa chỉ</p>");
            return;
        }

        var param = {
            Fullname: fullname,
            Gender: $('#profile_gender').val(),
            Mobile: mobile,
            Address: address,
            Email: $('#profile_email').val()
        };

        utils.loading();
        $.post(utils.rootUrl() + "Home/UpdateProfile", param).done(function (data) {
            utils.unLoading();
            if (data.ResponseCode >= 0) {
                $('#form_update_profile .center-error').text('Cập nhật thông tin tài khoản thành công').show();
            } else {
                var des = common.getDescription(data.ResponseCode);
                $('#form_update_profile .center-error').text(des).show();
            }
        }).fail(function () {
            utils.unLoading();
        });
    },
    uploadAvatar: function (base64Data) {
        var param = {
            Avatar: base64Data.split(",")[1]
        };

        $.post(utils.rootUrl() + "Home/UploadAvatar", param).done(function (data) {
            console.log(data);
        }).fail(function () {
        });
    },
    changePassword: function () {
        $('.center-error').hide();
        $('input').removeClass('input-error');
        $(".label-error").remove();
        var password = $('#changepass_old').val();
        if (!password) {
            $('#changepass_old').addClass('input-error');
            $('#changepass_old').after("<span class='label-error'>Vui lòng nhập mật khẩu cũ</p>");
            return;
        }
        var newpassword = $('#changepass_new').val();
        if (!newpassword) {
            $('#changepass_new').addClass('input-error');
            $('#changepass_new').after("<span class='label-error'>Vui lòng nhập mật khẩu mới</p>");
            return;
        }
        var renewpassword = $('#changepass_renew').val();
        if (!renewpassword) {
            $('#changepass_renew').addClass('input-error');
            $('#changepass_renew').after("<span class='label-error'>Vui lòng nhập lại mật khẩu mới</p>");
            return;
        }
        if (renewpassword != newpassword) {
            $('#forgetpass_renewpass').addClass('input-error');
            $('#forgetpass_renewpass').after("<span class='label-error'>Mật khẩu nhập lại không chính xác</p>");
            return;
        }
        var param = {
            password: password,
            newPassword: newpassword
        };

        utils.loading();
        $.post(utils.rootUrl() + "Home/ChangePasswordAction", param).done(function (data) {
            utils.unLoading();
            if (data.ResponseCode >= 0) {
                $('#doimatkhau .center-error').text('Thay đổi mật khẩu thành công').show();
            } else {
                var des = common.getDescription(data.ResponseCode);
                $('#doimatkhau .center-error').text(des).show();
            }
        }).fail(function () {
            utils.unLoading();
        });
    },
    insertCourseFavorite: function (courseId) {
        var param = {
            courseId: courseId
        };

        utils.loading();
        $.post(utils.rootUrl() + "Home/CourseFavorite", param).done(function (data) {
            utils.unLoading();
            if (data.ResponseCode > 1) {
                $('#form_update_profile .center-error').text('Thêm khóa học yêu thích thành công').show();
            } if (data.ResponseCode === 1) {
                $('#form_update_profile .center-error').text('Xóa khóa học yêu thích thành công').show();
            }
            else {
                var des = common.getDescription(data.ResponseCode);
                $('#form_update_profile .center-error').text(des).show();
            }
        }).fail(function () {
            utils.unLoading();
        });
    },
    removeCourseFavorite: function (courseId) {
        var param = {
            courseId: courseId
        };

        utils.loading();
        $.post(utils.rootUrl() + "Home/CourseFavorite", param).done(function (data) {
            utils.unLoading();
            if (data.ResponseCode === 1) {
                $('#item-khoahoc-grid-' + courseId).remove();
            }
            else {
                var des = common.getDescription(data.ResponseCode);
                $('#form_update_profile .center-error').text(des).show();
            }
        }).fail(function () {
            utils.unLoading();
        });
    },
    loginByGoogle: function () {
        $.get(utils.rootUrl() + "api/oauth/google_authen").done(function (data) {
            window.location.href = data.AuthenUrl;
        }).fail(function () {
            utils.unLoading();
        });
    },
    loginByFacebook: function () {
        $.get(utils.rootUrl() + "Home/Facebook").done(function (data) {
            window.location.href = data.AuthenUrl;
        }).fail(function () {
            utils.unLoading();
        });
    },
    SearchCourse: function (type, page, sort, isBtClick) {
        var textSearch = $('#course_name_' + type).val();
        var monthOpen = 0;
        var locationID = 0;
        var districtID = 0;
        var studyTime = '';
        var languageInstruction = '';
        var level = '';
        var lecturer = '';
        var studyForm;
        var studyDuration = '';
        var listTag = '';
        var specialityID = 0;
        var studyType = 0;
        if (type === 1) {
            monthOpen = $('#dukienkhaigiang_1').val();
            locationID = $('#1_city').val();
            districtID = $('#1_district').val();
            specialityID = $('#1_danhmuckhoahoc').val();
            $("input[name='thoigianhoc_1']:checked").each(function () {
                studyTime += $(this).val() + ',';
            });
            $("input[name='ngonngugiangday_1']:checked").each(function () {
                languageInstruction += $(this).val() + ',';
            });
            $("input[name='trinhdobatdau_1']:checked").each(function () {
                level += $(this).val() + ',';
            });
            lecturer = $('#chongiangvien_1').val();
            studyForm = $('#1_train_type').val();
            //studyDuration = $('#1_train_time').val();

            //Tag left
            $('.1_dynamic_tag').each(function () {
                var tagGroupID = $(this).data('taggroupid');
                var dynamicTag = $('input[name=1_' + tagGroupID + ']:checked').val();
                if (dynamicTag)
                    listTag += dynamicTag + ';';
            });

            //Tag top
            $('.1_top_dynammic_tag').each(function () {
                var dynamicTag = $(this).data('tag_selected');               
                if (dynamicTag)
                    listTag += dynamicTag + ';';
            });

            //if ($("#kh-nganhan .btn-sort i").hasClass("active")) {
            //    sort = $("#kh-nganhan .btn-sort").find("i.active").data("sort");
            //}

            specialityID = $('#nganhhoc_1').val();
        }
        else {
            studyForm = !$('input[name=2_hinhthuc]:checked').val() ? 0 : $('input[name=2_hinhthuc]:checked').val();

            //tag left
            $('.2_dynamic_tag').each(function () {
                var tagGroupID = $(this).data('taggroupid');
                var dynamicTag = $('input[name=2_' + tagGroupID + ']:checked').val();
                if (dynamicTag)
                    listTag += dynamicTag + ';';
            });

            //Tag top
            $('.2_top_dynammic_tag').each(function () {
                var dynamicTag = $(this).data('tag_selected');
                if (dynamicTag)
                    listTag += dynamicTag + ';';
            });

            specialityID = $('#nganhhoc_2').val();
            locationID = $('#header_diadiem').val();

            //sort = $("#kh-daihan .btn-sort").find("i.active").data("sort");

            if (isBtClick) {
                studyType = $('#header_loaihinh').val();

            }
            else
                studyType = !$('input[name=' + type + '_loaihinh]:checked').val() ? '0' : $('input[name=' + type + '_loaihinh]:checked').val();

            level = $('#header_bachoc_2').val();
        }
        listTag = listTag.length ? listTag.slice(0, -1) : '';
        var param = {
            CourseType: type,
            TextSearch: textSearch,
            StudyType: studyType,
            StudyForm: studyForm,
            RequireAdmission: !$('input[name=' + type + '_yeucaunhaphoc]:checked').val() ? 0 : $('input[name=' + type + '_yeucaunhaphoc]:checked').val(),
            SchoolType: !$('input[name=' + type + '_loaihinhtruong]:checked').val() ? 0 : $('input[name=' + type + '_loaihinhtruong]:checked').val(),
            SelectRegister: !$('input[name=' + type + '_luachondangky]:checked').val() ? 0 : $('input[name=' + type + '_luachondangky]:checked').val(),
            NationalID: !$('input[name=' + type + '_diachidaotao]:checked').val() ? 0 : $('input[name=' + type + '_diachidaotao]:checked').val(),
            LocationID: locationID,
            DisctricID: districtID,
            MonthOpen: monthOpen,
            StudyTime: studyTime,
            LanguageInstruction: languageInstruction,
            Level: level,
            Lecturer: lecturer,
            StudyDuration: studyDuration,
            ListTagId: listTag,
            SpecialityID: specialityID,
            CurrentPage: !page ? 1 : page,
            PageSize: 12,
            Sort: !sort ? 0 : sort,
            TypeView: $("#hdTypeView").val(),
            IsFeatured: customSearch.IsFeatured,
            NumRate: customSearch.NumRate,
            IsHot: customSearch.IsHot
        };

        //var data = JSON.stringify(param);
        //var cookieSearchCourse = Base64.encode(data);
        //utils.setCookie('_cookieSearchCourse', cookieSearchCourse, 10);
        //var data = type + '|' + textSearch;
        //if ($('#list_school_form').length) {
        //    $.get(utils.rootUrl() + 'tim-kiem').done(function (data) {
        //        $('#page').html(data);
        //    });
        //}
        //else if (!$('#search_form').length) {
        //window.location.href = utils.rootUrl() + 'tim-kiem?' + query;
        if (!$('#search_form').length) {
            var query = Object.keys(param).map(function (k) {
                return encodeURIComponent(k) + "=" + ((!param[k] || param[k] == "undefined") ? '' : encodeURIComponent(param[k]));
            }).join('&');
            window.location.href = utils.rootUrl() + 'tim-kiem?' + query;
        }
        else {
            var tuitionFrom = 0;
            var tuitionTo = 500 * 1000000;
            var rangeTuition1 = $('#ex' + type).data('slider').getValue();
            tuitionFrom = rangeTuition1[0] * 1000000;
            tuitionTo = rangeTuition1[1] * 1000000;
            param.TuitionFrom = tuitionFrom;
            param.TuitionTo = tuitionTo;
            if (!isNaN(textSearch))
                param.CourseID = textSearch;

            $.get(utils.rootUrl() + 'Home/SearchAjax', param).done(function (result) {
                var query = Object.keys(param).map(function (k) {
                    return encodeURIComponent(k) + "=" + encodeURIComponent(param[k]);
                }).join('&');
                window.history.pushState({}, document.title, "/tim-kiem?" + query);
                //utils.setCookie('_cookieSearchCourse', cookieSearchCourse, 0.2);
                $('#content_khoahoc_' + type).html(result);
                var totalRecord = $('#content_khoahoc_' + type + ' #total_record').val();
                cardCompareArray = [];
                cardCompare.Load();
                if (totalRecord && totalRecord !== "0")
                    totalRecord = parseInt(totalRecord);
                $('#totalRecord').text(totalRecord);
                if (type === 1) {
                    $("#hdType").val(1);
                    $('#tab_header a[href="#kh-nganhan"]').tab('show');
                    $('#pagination_short').pagination({
                        itemsOnPage: 12,
                        cssStyle: 'light-theme',
                        items: totalRecord,
                        onPageClick: function (page) {
                            utils.CoursePageChange(1, page);
                        }
                    });
                }
                else {
                    $("#hdType").val(2);
                    $('#tab_header a[href="#kh-daihan"]').tab('show');
                    $('#pagination_long').pagination({
                        itemsOnPage: 12,
                        cssStyle: 'light-theme',
                        items: totalRecord,
                        onPageClick: function (page) {
                            utils.CoursePageChange(2, page);
                        }
                    });
                }
                //if (utils.detectMobile) {
                //    $('#m_search').toggle();
                //}
            });
        }
    },
    IdlePopupSearch(type) {
        if (type === 2) {
            $('#course_name_' + type).val($('#txtSearch_idle2').val());
            $('#header_loaihinh').val($('#idle2_loaihinh').val());
            $('#nganhhoc_2').val($('#idle2_nganhhoc').val());
            $('#header_bachoc_2').val($('#idle2_bachoc').val());
        }
        else {
            $('#course_name_' + type).val($('#txtSearch_idle1').val());
            $('#nganhhoc_1').val($('#idle1_nganhhoc').val());
            $('#1_train_type').val($('#idle1_hinhthuchoc').val());
        }
        utils.SearchCourse(type, null, null, true);
        $("#idleModal").modal("hide");
    },
    CoursePageChange: function (type, page) {
        var param = queryParamToObject(window.location.search);
        param.CourseType = type;
        param.CurrentPage = page;
        param.PageSize = 12;
        param.TypeView = $("#hdTypeView").val();
        $.get(utils.rootUrl() + 'Home/SearchAjax', param).done(function (result) {
            $('#content_khoahoc_' + type).html(result);
            $('#content_khoahoc_' + type).html(result);
            $(document).scrollTop($('#page').offset().top);
        });
    },

    SearchCourseByTag: function (studyType, specialityId) {
        var param = {
            CourseType: 2,
            StudyType: studyType,
            SpecialityID: specialityId
        };
        var data = JSON.stringify(param);
        var cookieSearchCourse = Base64.encode(data);
        utils.setCookie('_cookieSearchCourse', cookieSearchCourse, 0.2);
        window.location.href = utils.rootUrl() + 'tim-kiem';
    },
    SearchCourseByLocation: function (tagLocationId) {
        var param = {
            CourseType: 2,
            ListTagId: tagLocationId
        };
        var data = JSON.stringify(param);
        var cookieSearchCourse = Base64.encode(data);
        utils.setCookie('_cookieSearchCourse', cookieSearchCourse, 0.2);
        window.location.href = utils.rootUrl() + 'tim-kiem';
    },
    SearchCourseByCustomCondition: function (t, textSearch, numRate) {
        var courseType = 1;
        if ($('#kh-daihan').is(':visible'))
            courseType = 2;

        if (t) {
            if ($(t).data('type') === 'promotion') {
                if ($(t).hasClass("filter-promotion-selected")) {
                    $(t).removeClass("filter-promotion-selected");
                    customSearch.IsFeatured = null;
                }
                else {
                    $(t).addClass("filter-promotion-selected");
                    customSearch.IsFeatured = true;

                    $('.kh_hot, .2_top_dynammic_tag a, .1_top_dynammic_tag a').removeClass("filter-hot-selected").removeClass("filter-promotion-selected");
                    $('.2_top_dynammic_tag, .1_top_dynammic_tag').data('tag_selected','');
                    customSearch.IsHot = null;
                }
            }
            else if ($(t).data('type') === 'hot') {
                if ($(t).hasClass("filter-hot-selected")) {
                    $(t).removeClass("filter-hot-selected");
                    customSearch.IsHot = null;
                }
                else {
                    $(t).addClass("filter-hot-selected");
                    customSearch.IsHot = true;

                    $('.kh_feature, .2_top_dynammic_tag a, .1_top_dynammic_tag a').removeClass("filter-promotion-selected");
                    $('.2_top_dynammic_tag, .1_top_dynammic_tag').data('tag_selected', '');
                    customSearch.IsFeatured = null;
                }
            }

        }

        if (textSearch) {
            $('#course_name_' + courseType).val(textSearch);
        }

        if (numRate) {
            customSearch.NumRate = numRate;
        }

        utils.SearchCourse(courseType, 1);
        //return;
        //param.TypeView = $("#hdTypeView").val();
        //$.get(utils.rootUrl() + 'Home/SearchAjax', param).done(function (result) {
        //    $('#content_khoahoc_' + param.CourseType).html(result);
        //    var totalRecord = $('#content_khoahoc_' + param.CourseType + ' #total_record').val();
        //    if (totalRecord && totalRecord !== "0")
        //        totalRecord = parseInt(totalRecord);
        //    if (param.CourseType === 1) {
        //        $("#hdType").val(1);
        //        $('#pagination_short').pagination({
        //            itemsOnPage: 12,
        //            cssStyle: 'light-theme',
        //            items: totalRecord,
        //            onPageClick: function (page) {
        //                utils.CoursePageChange(1, page);
        //            }
        //        });
        //    }
        //    else {
        //        $("#hdType").val(2);
        //        $('#pagination_long').pagination({
        //            itemsOnPage: 12,
        //            cssStyle: 'light-theme',
        //            items: totalRecord,
        //            onPageClick: function (page) {
        //                utils.CoursePageChange(2, page);
        //            }
        //        });
        //    }
        //});
    },

    addNewCourse: function (t) {
        var currentIndex = $(t).data('count');
        var nextIndex = parseInt(currentIndex) + 1;
        utils.loading();
        $.get(utils.rootUrl() + "Home/AddCourse", { index: nextIndex }).done(function (data) {
            utils.unLoading();
            $("#reg_course_" + currentIndex).after(data);
            $(t).data('count', nextIndex);
        }).fail(function () {
            utils.unLoading();
        });
    },
    removeCourse: function (index) {
        $('#reg_course_' + index).remove();
        $('#add_new_course').data('count', index - 1);
    },
    registerPartner: function () {
        $('.center-error').hide();
        $('input').removeClass('input-error');
        $(".label-error").remove();
        var tentiengviet = $('#reg_vn_name').val();
        if (!tentiengviet) {
            $('#reg_vn_name').addClass('input-error');
            $('#reg_vn_name').before("<span class='label-error'>Vui lòng nhập tên tiếng Việt</p>");
            return;
        }
        var tentienganh = $('#reg_eng_name').val();
        if (!tentienganh) {
            $('#reg_eng_name').addClass('input-error');
            $('#reg_eng_name').before("<span class='label-error'>Vui lòng nhập tên tiếng Anh</p>");
            return;
        }

        var tenviettat = $('#reg_alias_name').val();

        var trusochinh = $('#reg_address').val();
        if (!trusochinh) {
            $('#reg_address').addClass('input-error');
            $('#reg_address').before("<span class='label-error'>Vui lòng nhập trụ sở chính</p>");
            return;
        }
        var phuongxa = $('#reg_ward').val();
        if (!phuongxa) {
            $('#reg_ward').addClass('input-error');
            $('#reg_ward').parent('span').before("<span class='label-error'>Vui lòng nhập phường xã</p>");
            return;
        }
        var quanhuyen = $('#reg_district').val();
        if (!quanhuyen) {
            $('#reg_district').addClass('input-error');
            $('#reg_district').parent('span').before("<span class='label-error'>Vui lòng nhập quận huyện</p>");
            return;
        }

        var thanhpho = $('#reg_city').val();
        if (!thanhpho) {
            $('#reg_city').addClass('input-error');
            $('#reg_city').parent('span').before("<span class='label-error'>Vui lòng nhập thành phố</p>");
            return;
        }
        var quocgia = $('#reg_country').val();
        if (!quocgia) {
            $('#reg_country').addClass('input-error');
            $('#reg_country').parent('span').before("<span class='label-error'>Vui lòng nhập quốc gia</p>");
            return;
        }

        var sodienthoai = $('#reg_phone').val();
        if (!sodienthoai) {
            $('#reg_phone').addClass('input-error');
            $('#reg_phone').before("<span class='label-error'>Vui lòng nhập số điện thoại</p>");
            return;
        }
        var sofax = $('#reg_fax').val();
        var email = $('#reg_email').val();
        if (!email) {
            $('#reg_email').addClass('input-error');
            $('#reg_email').before("<span class='label-error'>Vui lòng nhập email</p>");
            return;
        }
        var website = $('#reg_site').val();
        var chutich = $('#reg_president').val();
        var hieutruong = $('#reg_master').val();

        var loaihinhdaotao = $('input[name=loaihinhdaotao]:checked').val();
        if (!loaihinhdaotao) {
            bootbox.alert('Vui lòng chọn loại hình đào tạo');
            return;
        }

        if (loaihinhdaotao === 'other') {
            loaihinhdaotao = ('#reg_schooltype_other').val();
            if (!loaihinhdaotao) {
                bootbox.alert('Vui lòng nhập loại hình đào tạo');
                return;
            }
        }

        var nganhnghedaotao = '';
        $('#reg_career .reg_input_career').each(function () {
            nganhnghedaotao += $(this).val() + ';';
        });

        if (nganhnghedaotao.length <= 3) {
            bootbox.alert('Vui lòng nhập ngành nghề đào tạo');
            return;
        }

        var soluonggiangvien = $('#reg_number_teacher').val();

        var trusogiangduong = '';
        $("input[name='truso_thietbi[]']checked").each(function () {
            trusogiangduong += $(this).val() + ';';
        });

        trusogiangduong = trusogiangduong.slice(0, -1);

        var trusogiangduongother = $('#reg_headquarter').val();

        var tongdientich = $('#reg_total_area').val();
        var motathem = $('#reg_other_info').val();


        var arrayCourseInfo = [];
        $('.reg_course_info').each(function () {
            var index = $(this).data('index');
            var $currentParent = $('#reg_course_' + index);


            var _tenct = $currentParent.find('#reg_tenct').val();
            var _kiemdinh = $currentParent.find('#reg_kiemdinh').val();
            var _bachoc = $currentParent.find('input[name=bachoc]:checked').val();
            if (_bachoc === 'other') {
                _bachoc = $currentParent.find('#reg_bachoc_other').val();
            }
            var _diadiemdaotao = $currentParent.find('#reg_diadiemdaotao').val();
            var _hinhthucdatao = $currentParent.find('input[name=hinhthucdtao]:checked').val();

            var _yeucaubangcap = $currentParent.find('#reg_yeucaubangcap').val();
            var _yeucaungoaingu = $currentParent.find('#reg_yeucaungoaingu').val();
            var _lephixettuyen = $currentParent.find('#reg_lephixettuyen').val();

            var _yeucaukhac = $currentParent.find('#reg_yeucaukhac').val();

            var _thoigiandaotao = $currentParent.find('#reg_thoigiandaotao').val();
            var _ngonngugiangday = $currentParent.find('#reg_ngonngugiangday').val();

            var _tenmonhoc = $currentParent.find('#reg_tenmonhoc').val();
            var _sotinchi = $currentParent.find('#reg_sotinchi').val();
            var _danhgiatotnghiep = $currentParent.find('#reg_danhgiatotnghiep').val();
            var _hocphicongbo = $currentParent.find('#reg_hocphicongbo').val();
            var _uudai = $currentParent.find('#reg_uudai').val();
            var _hocbong = $currentParent.find('#reg_hocbong').val();
            var _muchocbong = $currentParent.find('#reg_muchocbong').val();


            var _dieukiennhan = $currentParent.find('#reg_dieukiennhan').val();
            var _dukienkhaigiang = $currentParent.find('#reg_ngaykg').val() + '/' + $currentParent.find('#reg_thangkg').val() + '/' + $currentParent.find('#reg_namkg').val();
            var _thongtinkhac = $currentParent.find('#reg_thongtinkhac').val();

            var objectinfo = {
                ID: index,
                EduName: _tenct,
                Acknowledge: _kiemdinh,
                EduLevel: _bachoc,
                EduAddress: _diadiemdaotao,
                EduType: _hinhthucdatao,
                CertificateRequire: _yeucaubangcap,
                ForeignLanguageRequire: _yeucaungoaingu,
                FeeAdmission: _lephixettuyen,
                OtherRequire: _yeucaukhac,
                Duration: _thoigiandaotao,
                EduLang: _ngonngugiangday,
                SubjectName: _tenmonhoc,
                CreditNumber: _sotinchi,
                RatedGraduateL: _danhgiatotnghiep,
                Tuition: _hocphicongbo,
                TuitionIncentives: _uudai,
                Scholarship: _hocbong,
                ScholarshipValue: _muchocbong,
                ScholarshipCondition: _dieukiennhan,
                OpenDate: _dukienkhaigiang,
                OtherInfo: _thongtinkhac
            };
            arrayCourseInfo.push(objectinfo);
        });

        var hodaidien = $('#reg_ho_daidien').val();
        if (!hodaidien) {
            $('#reg_ho_daidien').addClass('input-error');
            $('#reg_ho_daidien').before("<span class='label-error'>Vui lòng nhập họ người đại diện</p>");
            return;
        }
        var tendaidien = $('#reg_ten_daidien').val();
        if (!tendaidien) {
            $('#reg_ten_daidien').addClass('input-error');
            $('#reg_ten_daidien').before("<span class='label-error'>Vui lòng nhập tên người đại diện</p>");
            return;
        }

        var diachinguoidaidien = $('#reg_diachi_daidien').val();
        if (!diachinguoidaidien) {
            $('#reg_diachi_daidien').addClass('input-error');
            $('#reg_diachi_daidien').before("<span class='label-error'>Vui lòng nhập địa chỉ người đại diện</p>");
            return;
        }

        var sdtnguoidaidien = $('#reg_sdt_daidien').val();
        if (!sdtnguoidaidien) {
            $('#reg_sdt_daidien').addClass('input-error');
            $('#reg_sdt_daidien').parent('span').after("<span class='label-error'>Vui lòng nhập sđt người đại diện</p>");
            return;
        }
        var emaildaidien = $('#reg_email_daidien').val();
        if (!emaildaidien) {
            $('#reg_email_daidien').addClass('input-error');
            $('#reg_email_daidien').parent('span').after("<span class='label-error'>Vui lòng nhập email người đại diện</p>");
            return;
        }
        if (!$('#camket_daidien').is(":checked")) {
            bootbox.alert('Vui lòng cam kết thông tin đã khai báo');
            return;
        }

        var fileUpload = $('#chon_file').get(0);
        var files = fileUpload.files;
        var fileName = '';
        utils.loading();
        if (files.length > 0 && files[0].size / 1048576 <= 2) {
            // Create FormData object
            var fileData = new FormData();
            fileData.append(files[0].name, files[0]);


            var fileUpload = $('#chon_file').get(0);
            var files = fileUpload.files;
            var fileSize = files[0].size;
            if (fileSize / 1048576 > 2) {
                bootbox.alert("Kích thước size > 2MB, vui lòng upload file khác");
            }

            $.ajax({
                url: '/Home/UploadFiles',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: fileData,
                async: false,
                success: function (result) {
                    fileName = result;
                },
                error: function (err) {
                    console.log(err.statusText);
                }
            });
        }

        var param = {
            EduNameVI: tentiengviet,
            EduNameEn: tentienganh,
            ShortName: tenviettat,
            HeadOffice: trusochinh,
            Ward: phuongxa,
            EduEmail: email,
            District: quanhuyen,
            Location: thanhpho,
            National: quocgia,
            EduPhone: sodienthoai,
            EduFax: sofax,
            Website: website,
            President: chutich,
            SchoolMaster: hieutruong,
            SchoolType: loaihinhdaotao,
            Career: nganhnghedaotao,
            NumberLecturers: soluonggiangvien,
            Facilities: soluonggiangvien,
            FacilitiesMore: trusogiangduongother,
            TotalArea: tongdientich,
            InfomationMore: motathem,
            CoursesInfo: JSON.stringify(arrayCourseInfo),
            Firstname: hodaidien,
            Lastname: tendaidien,
            Address: diachinguoidaidien,
            Phone: sdtnguoidaidien,
            Email: emaildaidien,
            FileAttach: fileName
        }

        utils.loading();
        $.post(utils.rootUrl() + "Home/RegisterPartnerPost", N).done(function (data) {
            utils.unLoading();
            if (data >= 0) {
                bootbox.alert("Gửi yêu cầu thành công", function () { window.location.href = utils.rootUrl(); });
            } else {
                if (data === -1001) {
                    bootbox.alert('Gửi yêu cầu quá nhanh, vui lòng thử lại sau 1 phút');
                    return;
                }
                bootbox.alert('Gửi yêu cầu thất bại, mã lỗi: ' + data);
            }
        }).fail(function () {
            utils.unLoading();
        });
    },
    comment: function (id, type, parentid) {
        var comment = $('#write_comment_' + parentid).val();
        if (!comment) {
            bootbox.alert('Vui lòng nhập bình luận');
            return;
        }
        var param = {
            TargetID: id,
            Comment: comment,
            Type: type,
            ParentID: parentid
        };

        $.post(utils.rootUrl() + "Home/Comment", param).done(function (data) {
            if (data >= 0) {
                $('#write_comment_' + parentid).val('');
                var htmlComment = '';
                if (parentid === 0) {
                    htmlComment = `<li id="item_parent_` + data + `">
                            <div class="row">
                                <div class="col-xs-2 col-md-1 pl-0 pr-0">
                                    <div class="avatar">
                                        <img src="`+ utils.rootUrl() + `Upload/Avatar/` + userInfoData.UserID + `/` + userInfoData.Avatar + `">
                                    </div>
                                </div>
                                <div class="col-xs-10 col-md-11 pr-0">
                                    <div class="author">
                                        `+ userInfoData.UserName + `
                                    </div>
                                    <div class="content">
                                        `+ comment + `
                                    </div>
                                    <div class="replay">
                                        <a href="javascript:void(0);" class="btn-show">Trả lời</a>
                                        <div class="replay-form clearfix">
                                            <div class="row pl-3">
                                                <div class="col-xs-2 col-md-1 pl-0 pr-0">
                                                    <div class="avatar">
                                                       <img src="`+ utils.rootUrl() + `Upload/Avatar/` + userInfoData.UserID + `/` + userInfoData.Avatar + `">
                                                    </div>
                                                </div>
                                                <div class="col-xs-10 col-md-11">
                                                    <div class="form-group">
                                                        <textarea id="write_comment_`+ data + `" class="form-control" placeholder="Viết bình luận ..." rows="3" name="vi[content]" cols="50"></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="pl-3 pr-3">
                                                <button type="button" onclick="utils.comment(`+ id + `, 2, ` + data + `);" class="btn btn-primary pull-right ">Đăng bình luận</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="sub-comment clearfix">
                                        <ul id="child_comment" class="list-unstyled"></ul>
                                    </div>
                                </div>
                            </div>
                        </li>`;
                    $("#parent_comment").prepend(htmlComment);
                }
                else {
                    htmlComment = `<li>
                                        <div class="row">
                                            <div class="col-xs-2 col-md-1 pl-0 pr-0">
                                                <div class="avatar">
                                                     <img src="`+ utils.rootUrl() + `Upload/Avatar/` + userInfoData.UserID + `/` + userInfoData.Avatar + `">
                                                </div>
                                            </div>
                                            <div class="col-xs-10 col-md-11 pr-0">
                                                <div class="author">
                                                    `+ userInfoData.UserName + `
                                                </div>
                                                <div class="content">
                                                     `+ comment + `
                                                </div>
                                            </div>
                                        </div>
                                    </li>`;
                    $('#item_parent_' + parentid).find('.replay-form').hide();
                    $("#item_parent_" + parentid + " #child_comment").prepend(htmlComment);
                }

            } else {
                bootbox.alert('Yêu cầu chưa được xử lý, vui lòng thử lại');

            }
        }).fail(function () {
        });
    },

    bindDataToPage: function (page, type, perPage) {
        var $rootID = $('#content_khoahoc_' + type);
        if (page === 1) {
            $rootID.find('.v1_search_item').show();
            $rootID.find('.v1_search_item:gt(' + ((page * perPage) - 1) + ')').hide();
        } else {
            $rootID.find('.v1_search_item').hide();
            $rootID.find('.v1_search_item:gt(' + (((page - 1) * perPage) - 1) + ')').show();
            $rootID.find('.v1_search_item:gt(' + (page * perPage - 1) + ')').hide();
        }
    },

    bindDropdownLocation: function (locationID, elementID) {
        if (locationID == 0) {
            var htmlData = '<option class="sf-level-0 sf-item-0 sf-option-active" selected="selected" data-sf-depth="0" value="">Chọn Quận / Huyện</option>';
            $(elementID).html(htmlData);
            return;
        }
        $.get(utils.rootUrl() + 'Home/GetLocation', { parentID: locationID }).done(function (data) {
            if (data) {
                var htmlData = '<option class="sf-level-0 sf-item-0 sf-option-active" selected="selected" data-sf-depth="0" value="">Chọn Quận / Huyện</option>';
                $.each(data, function (key, val) {
                    htmlData += '<option class="sf-level-0 " data-sf-count="-1" data-sf-depth="0" value="' + val.LocationID + '">' + val.LocationName + '</option>';
                });
                $(elementID).html(htmlData);
            }
        });
    },

    sendAdvisory: function (id) {
        var hoten = $('#lienhe_hoten').val();
        if (!hoten) {
            alert('Vui lòng nhập họ tên');
            return;
        }

        var phone = $('#lienhe_sdt').val();
        if (!phone) {
            alert('Vui lòng nhập số điện thoại của bạn');
            return;
        }

        var email = $('#lienhe_email').val();
        if (!email) {
            alert('Vui lòng nhập email của bạn');
            return;
        }

        var param = {
            Fullname: hoten,
            Email: email,
            Phone: phone,
            CoursesID: id,
            CourseName: $('#course_name_display').text().trim()
        }
        utils.loading();
        $.post(utils.rootUrl() + "Home/SendAdvisory", param).done(function (data) {
            utils.unLoading();
            if (data > 0) {

                window.location.location = 'https://edunet.vn/cam-on';
                return;
            } else {
                alert('Gửi đăng ký tư vấn thất bại');
            }
        }).fail(function () {
            utils.unLoading();
        });

    },

    sendEducationContact: function (eduid) {
        var hoten = $('#lienhe_hoten').val();
        if (!hoten) {
            alert('Vui lòng nhập họ tên của bạn');
            return;
        }

        var phone = $('#lienhe_sdt').val();
        if (!phone) {
            alert('Vui lòng nhập số điện thoại của bạn');
            return;
        }

        var email = $('#lienhe_email').val();
        if (!email) {
            alert('Vui lòng nhập email của bạn');
            return;
        }

        var param = {
            Fullname: hoten,
            Email: email,
            Phone: phone,
            Address: $('#lienhe_diachi').val(),
            EducationID: eduid,
            CourseName: $('#course_name_display').text().trim()
        };

        utils.loading();
        $.post(utils.rootUrl() + "Home/SendEducationContact", param).done(function (data) {
            utils.unLoading();
            if (data > 0) {
                window.location.href = 'https://edunet.vn/cam-on';
                $('#form_dangkytuvan input').val();
                return;
            } else {
                alert('Gửi liên hệ thất bại');
            }
        }).fail(function () {
            utils.unLoading();
        });

    }
};

common = new function () {
    this.getDescription = function (c) {
        utils.translateLang('common.errorcode');
        var description = "";
        switch (c) {
            case 0:
                description = "Thành công";
                break;
            case -99:
                description = "Yêu cầu chưa được xử lý, vui lòng thử lại sau";
                break;
            case -600:
                description = "Dữ liệu không hợp lệ";
                break;
            case -1001:
                description = "Captcha không đúng";
                break;
            case -1002:
                description = "Tài khoản bị khóa";
                break;
            case -1003:
                description = "Tài khoản bị khóa";
                break;
            case -1004:
                description = "Mật khẩu không đúng";
                break;
            case -1005:
                description = "Tài khoản chưa kích hoạt";
                break;
            case -1006:
                description = "Tài khoản không tồn tại";
                break;
            case -1007:
                description = "Tài khoản đã tồn tại";
                break;
            case -1008:
                description = "Mã bảo mật không hợp lệ";
                break;
            case -1009:
                description = "Chữ ký không hợp lệ";
                break;
            case -1010:
                description = "Dữ liệu đã hết hiệu lực";
                break;
            default:
                description = "Yêu cầu chưa được xử lý, vui lòng thử lại sau";
                break;
        }
        //public const int SECURECODE_INVALID = -1008;
        //public const int SIGN_INVALID = -1009;
        //public const int DATA_EXPIRE = -1010;
        return description;
    };
};

function ImageResizeByCanvas(base64, width, height) {
    return new Promise(function (resolve, reject) {
        var image = new Image();
        image.src = base64;
        var canvas = document.createElement("canvas");
        var ctx = canvas.getContext("2d");
        var maxW = width;
        var maxH = height;
        image.onload = function () {
            var iw = image.width;
            var ih = image.height;
            var scale = Math.min((maxW / iw), (maxH / ih));
            var iwScaled = iw * scale;
            var ihScaled = ih * scale;
            canvas.width = iwScaled; // target width
            canvas.height = ihScaled; // target height
            ctx.drawImage(image,
                0, 0, iw, ih,
                0, 0, iwScaled, ihScaled
            );
            resolve(canvas.toDataURL());
        };
    })
}

function UnicodeConvert(str) {
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
    return str;
}

function setItemInStorage(dataKey, data) {
    var arrayData = getItemFromStorage(dataKey);
    //Chua co data thi push vao array
    if (!arrayData) {
        var newArray = [];
        newArray.push(data);
        localStorage.setItem(dataKey, JSON.stringify(newArray));
        return;
    }
    if (arrayData.filter(item => item.CoursesID == data.CoursesID).length !== 0) {
        console.log('item exist');
        return;
    }
    if (arrayData.length < 5) {
        arrayData.push(data);
        localStorage.setItem(dataKey, JSON.stringify(arrayData));
        return;
    }
    arrayData.shift();
    arrayData.push(data);
    localStorage.setItem(dataKey, JSON.stringify(arrayData));
}

function getItemFromStorage(dataKey) {
    var data = localStorage.getItem(dataKey);
    return data ? JSON.parse(data) : null;
}

function queryParamToObject(str) {
    if (str.charAt(0) === '?')
        str = str.substr(1);
    return str.split('&').reduce(function (params, param) {
        var paramSplit = param.split('=').map(function (value) {
            return decodeURIComponent(value.replace(/\+/g, ' '));
        });
        params[paramSplit[0]] = paramSplit[1];
        return params;
    }, {});
}

function updateQueryString(key, value, url) {
    if (!url) url = window.location.href;
    var re = new RegExp("([?&])" + key + "=.*?(&|#|$)(.*)", "gi"),
        hash;

    if (re.test(url)) {
        if (typeof value !== 'undefined' && value !== null) {
            
            url = url.replace(re, '$1' + key + "=" + value + '$2$3');
        }
        else {
            hash = url.split('#');
            url = hash[0].replace(re, '$1$3').replace(/(&|\?)$/, '');
            if (typeof hash[1] !== 'undefined' && hash[1] !== null) {
                url += '#' + hash[1];
            }
        }
    }
    else {
        if (typeof value !== 'undefined' && value !== null) {
            var separator = url.indexOf('?') !== -1 ? '&' : '?';
            hash = url.split('#');
            url = hash[0] + separator + key + '=' + value;
            if (typeof hash[1] !== 'undefined' && hash[1] !== null) {
                url += '#' + hash[1];
            }
        }
        
    }
    window.history.pushState({}, document.title, url.replace(window.location.origin, ''));
}
function swal_popup(title, msg, type) {
    Swal.fire({
        title: title,
        text: msg,
        type: type,
        width: '50rem'
    });
}
var Base64 = {

    // private property
    _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",

    // public method for encoding
    encode: function (input) {
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;

        input = Base64._utf8_encode(input);

        while (i < input.length) {

            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }

            output = output +
                this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) +
                this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);

        }

        return output;
    },

    // public method for decoding
    decode: function (input) {
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var i = 0;

        input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

        while (i < input.length) {

            enc1 = this._keyStr.indexOf(input.charAt(i++));
            enc2 = this._keyStr.indexOf(input.charAt(i++));
            enc3 = this._keyStr.indexOf(input.charAt(i++));
            enc4 = this._keyStr.indexOf(input.charAt(i++));

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            output = output + String.fromCharCode(chr1);

            if (enc3 != 64) {
                output = output + String.fromCharCode(chr2);
            }
            if (enc4 != 64) {
                output = output + String.fromCharCode(chr3);
            }

        }

        output = Base64._utf8_decode(output);

        return output;

    },

    // private method for UTF-8 encoding
    _utf8_encode: function (string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";

        for (var n = 0; n < string.length; n++) {

            var c = string.charCodeAt(n);

            if (c < 128) {
                utftext += String.fromCharCode(c);
            }
            else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }

        }

        return utftext;
    },

    // private method for UTF-8 decoding
    _utf8_decode: function (utftext) {
        var string = "";
        var i = 0;
        var c = c1 = c2 = 0;

        while (i < utftext.length) {

            c = utftext.charCodeAt(i);

            if (c < 128) {
                string += String.fromCharCode(c);
                i++;
            }
            else if ((c > 191) && (c < 224)) {
                c2 = utftext.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            }
            else {
                c2 = utftext.charCodeAt(i + 1);
                c3 = utftext.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }
        }

        return string;
    }
};

//$(document).bind('keydown', function (e) {
//    e = e || window.event;//Get event
//    if (e.ctrlKey) {
//        var c = e.which || e.keyCode;//Get key code
//        switch (c) {
//            case 83://Block Ctrl+S
//                e.preventDefault();
//                e.stopPropagation();
//                break;
//        }
//    }
//});
//document.addEventListener('contextmenu', event => event.preventDefault());
