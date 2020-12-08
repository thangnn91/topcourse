var homeconfig = {
    pageSize: 9,
    pageIndex: 1,
}
var tintucController = {
    init: function () {
        tintucController.registerEvent();
        tintucController.loadData();
    },
    registerEvent: function () {       
    },
    loadData: function (changePageSize) {
        $.ajax({
            url: 'Home/LoadData',
            type: 'GET',
            data: {
                page: homeconfig.pageIndex,
                pageSize: homeconfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var tempalte = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(tempalte, {
                            PostID: item.PostID,
                            Alias: item.Alias,
                            TitleDisplay: item.TitleDisplay,
                            Title: item.Title,
                            Description: item.Description,
                            ThumbailImage: item.ThumbailImage
                        });
                    });
                    $('#tblDL').html(html);
                    tintucController.paging(response.totalRow, function () {
                        tintucController.loadData();
                    }, changePageSize);
                    tintucController.registerEvent();
                }
            }
        })
    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / homeconfig.pageSize);

    if ($('#pagination a').length === 0 || changePageSize === true) {
        $('#pagination').empty();
        $('#pagination').removeData("twbs-pagination");
        $('#pagination').unbind("page");
    }

    $('#pagination').twbsPagination({
        totalPages: totalPage,
        first: "Đầu",
        next: "Tiếp",
        last: "Cuối",
        prev: "Trước",
        visiblePages: 10,
        onPageClick: function (event, page) {
            homeconfig.pageIndex = page;
            setTimeout(callback, 200);
        }
      });
    }
}
tintucController.init();