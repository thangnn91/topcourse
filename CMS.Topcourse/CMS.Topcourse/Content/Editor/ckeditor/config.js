/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
    config.showContextMenuArrow = true;
    config.selectMultiple = true;
	config.filebrowserBrowseUrl = Utils.UrlRoot + 'Content/Editor/ckfinder/ckfinder.htm';
    config.filebrowserImageBrowseUrl = Utils.UrlRoot + 'Content/Editor/ckfinder/ckfinder.htm?type=Images';
    config.filebrowserFlashBrowseUrl = Utils.UrlRoot + 'Content/Editor/ckfinder/ckfinder.htm?type=Flash';
    config.filebrowserUploadUrl = Utils.UrlRoot + 'Content/Editor/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = Utils.UrlRoot + 'Content/Editor/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = Utils.UrlRoot + 'Content/Editor/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Flash';
    config.extraPlugins = 'accordionlist';
    config.contentsCss = 'https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css';
	//config.toolbar = [
 //       { name: 'insert', items: ['LocationMap', 'Source'] }
 //   ];
};
