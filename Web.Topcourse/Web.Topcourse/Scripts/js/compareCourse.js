var cardCompareArray = [];
var cardCompare = {
    // Collapse compare box
    Collapse: function (isMin) {
        if (isMin) {
            var totalCompareItem = (cardCompareArray != null && cardCompareArray != undefined) ? cardCompareArray.length : 0;
            $(".group-compare-bt").animate({ "bottom": "-95%" }, 500);
        } else {
            $(".group-compare-bt").animate({ "bottom": "0" }, 240);
        }
    },
    // Load data to box
    Load: function () {
        if (cardCompareArray !== null && cardCompareArray.length > 0) {

            var maxCompare = 3;
            var itemnotText = $(".not-empty-card-compare").html();
            $(".group-compare-bt .list-course").html("");

            for (var i = 0; i < cardCompareArray.length; i++) {
                console.log(cardCompareArray[i].EduName);
                var itemCard = $(itemnotText).find("img").attr("src", cardCompareArray[i].BankLogo);
                itemCard = itemCard.parent().parent().find("h5.course-name").html(cardCompareArray[i].CourseName);
                itemCard = itemCard.parent().parent().find("i").attr("data-prodId", cardCompareArray[i].Id);
                itemCard = itemCard.parent().parent().find("p.edu-name").html(cardCompareArray[i].EduName);
                itemCard = itemCard.parent().parent();
                $(".group-compare-bt .list-course").append(itemCard);
            }

            if (cardCompareArray.length < maxCompare) {
                var emptyCard = $(".empty-card-compare").html();
                for (var j = 0; j < maxCompare - cardCompareArray.length; j++) {
                    $(".group-compare-bt .list-course").append(emptyCard);
                }
            }

            if (cardCompareArray.length <= 1) {
                $(".compare-button-view").addClass("btn-regis-disable");
            } else {
                $(".compare-button-view").removeClass("btn-regis-disable");
            }

            $(".group-compare-bt").fadeIn(500);

            if ($(".group-compare-bt").css("bottom") !== "0") {
                $(".group-compare-bt").animate({ "bottom": "0" }, 240);
                $(".group-compare-bt .collapse-compare").animate({ "bottom": "147px" }, 300);
                //$(".group-compare-bt .collapse-compare i").removeClass("icon-up-w");
                //$(".group-compare-bt .collapse-compare i").addClass("icon-down-w");
            }

        } else {
            $(".group-compare-bt .list-course").html("");
            $(".group-compare-bt").fadeOut(500);
        }
    },
    // Add proc to list
    Add: function (elm, prodId, logo, courseName, eduName) {

        // thêm card vào danh sách và hiển thị lại ds
        cardCompareArray = cardCompareArray === null ? [] : cardCompareArray;
        var maxCompare = 3;
        if (cardCompareArray.length < maxCompare) {
            var item = { Id: prodId, BankLogo: logo, CourseName: courseName, EduName: eduName };
            cardCompareArray.push(item);

            cardCompare.Load();
            $(elm).parent().find(".remove-card").attr("style", "display :inline-block !important");
            $(elm).parent().find(".add-card").attr("style", "display :none !important");
        }

        // Đổi lại url
        //cardCompare.ChangeUrl();
    },
    // Remove prod from list
    Remove: function (elm, cardId) {

        // thêm card vào danh sách và hiển thị lại ds
        if (cardCompareArray.length > 0) {

            var index = -1;
            for (var i = 0; i < cardCompareArray.length; i++) {
                if (cardCompareArray[i].Id === cardId) {
                    index = i;
                }
            }

            cardCompareArray.splice(index, 1);

            $(elm).parent().find(".remove-card").fadeOut(50);
            $(elm).parent().find(".add-card").fadeIn(50);

        }

        cardCompare.Load();

        // Đổi lại url
        // cardCompare.ChangeUrl();
    },
    // Remove Item from list compare
    Remove2: function (elm) {

        // thêm card vào danh sách và hiển thị lại ds
        if (elm !== undefined) {
            var cardId = $(elm).attr("data-prodId");
            if (!isNaN(cardId) && parseInt(cardId) > 0) {

                if (cardCompareArray.length > 0) {
                    var index = -1;
                    for (i = 0; i < cardCompareArray.length; i++) {
                        if (cardCompareArray[i].Id === parseInt(cardId)) {
                            index = i;
                        }
                    }

                    cardCompareArray.splice(index, 1);

                    $('#control-card-' + cardId).find(".remove-card").fadeOut(50);
                    $('#control-card-' + cardId).find(".add-card").fadeIn(50);

                }

                cardCompare.Load();
            }
        }
    },
    RemoveCompare: function (elm) {

        // thêm card vào danh sách và hiển thị lại ds
        if (elm !== undefined) {
            var prodId = $(elm).attr("data-prodId");
            if (!isNaN(prodId) && parseInt(prodId) > 0) {

                if (cardCompareArray.length > 0) {
                    var index = -1;
                    for (i = 0; i < cardCompareArray.length; i++) {
                        if (cardCompareArray[i].Id === parseInt(prodId)) {
                            index = i;
                        }
                    }

                    cardCompareArray.splice(index, 1);

                    $('#control-card-' + prodId).find(".remove-card").fadeOut(0);
                    $('#control-card-' + prodId).find(".add-card").fadeIn(0);

                    // set lại value cho hidden text
                    
                    $(".prod-compare-common-" + prodId).html("&nbsp;");
                    $(elm).parent().parent().parent().html("");

                    //$(elm).parent().find("div").html('<div class="add-compare-popup" onclick="cardCompare.CloseCompare()"></div>');
                    //$(elm).remove();
                }

                if (cardCompareArray.length <= 0) {
                    cardCompare.CloseCompare();
                }

                cardCompare.Load();

                //// Đổi lại url
                //cardCompare.ChangeUrl();
            }
        }
    },
    Compare: function (elm) {
        if (!$(elm).hasClass("btn-regis-disable")) {
            if (cardCompareArray !== null && cardCompareArray.length > 0) {
                var lstCompareId = "";
                for (var i = 0; i < cardCompareArray.length; i++) {
                    lstCompareId = lstCompareId + cardCompareArray[i].Id + ",";
                }

                lstCompareId = lstCompareId.substr(0, lstCompareId.length - 1);

                if (lstCompareId !== "") {

                    //var paramSearch = cardApp.GetSearchParam();
                    //paramSearch.Selected = $("#hdCardSelected").val();
                    //paramSearch.PageIndex = cardCompare.PageIndex;
                    var param = {
                        courseIds: lstCompareId,
                        courseType:$("#hdType").val()
                    };

                    utils.loading();
                    $.post(utils.rootUrl() + "Home/CourseCompare", param).done(function (data) {
                        utils.unLoading();
                        if (data!==null) {
                            $(".popup-comapre .compare-content").html(data);

                            $("body").addClass("overflow-hidden");
                            $(".popup-comapre").animate({ bottom: "0px" }, 500);

                            //1. Set width for popup
                            var widthCompare = 1024;
                            if (cardCompareArray.length === 2) {
                                widthCompare = 615;
                                $(".popup-comapre .content-view .header-popup-comapre .item-compare").css("width", "33.33333%");
                                $(".popup-comapre .content-view .body-popup-comapre table td").css("width", "33.33333%");
                            }

                            if (cardCompareArray.length === 3) {
                                widthCompare = 820;
                                $(".popup-comapre .content-view .header-popup-comapre .item-compare").css("width", "25%");
                                $(".popup-comapre .content-view .body-popup-comapre table td").css("width", "25%");
                            }

                            $(".popup-comapre .content-view").css("width", widthCompare + "px");

                            //3. Create custome scroll 
                        } 
                    }).fail(function () {
                        utils.unLoading();
                    });
                    
                }
            }
        }

    },
    CloseCompare: function () {
        $(".popup-comapre").animate({ bottom: "-100%" }, 500, function () { $("body").removeClass("overflow-hidden"); });
    }
};