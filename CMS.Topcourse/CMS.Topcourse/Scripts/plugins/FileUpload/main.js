/*
 * jQuery File Upload Plugin JS Example 6.5.1
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

/*jslint nomen: true, unparam: true, regexp: true */
/*global $, window, document */

$(function () {
    'use strict';
    var filesList = [];
    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload();

    $('#fileupload').fileupload('option', {
            disableImageResize: /Android(?!.*Chrome)|Opera/.test(window.navigator.userAgent),
            autoUpload: false,
            maxFileSize: 5000000,
            resizeMaxWidth: 1920,
            resizeMaxHeight: 1200,
            acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
            limitMultiFileUploads: 15,
            singleFileUploads: false,
        maxNumberOfFiles:5
    }).on('fileuploadadd', function (e, data) {
        var that = $(this).data('fileupload');
        $.each(data.files, function (index, file) {
            filesList.push(data.files[index]);
        });
    }).on('fileuploadfail', function (e, data) {
        var that = $(this).data('fileupload');
        $.each(data.files, function (index, file) {
            filesList.splice(index, 1);
        });
    });
    $("#fileupload").submit(function (event) {
        var that = $(this).data('fileupload');
        if (filesList.length > 0) {
            event.preventDefault();
            $('#fileupload').fileupload('send', { files: filesList })
                .success(function (result, textStatus, jqXHR) { console.log('success'); })
                .error(function (jqXHR, textStatus, errorThrown) { console.log('error'); })
                .complete(function (result, textStatus, jqXHR) {
                    filesList = [];
                    // window.location='back to view-page after submit?'
                });
        } else {
            console.log("plain default form submit");
        }
    });
});
