using System.Web.Optimization;

namespace CMS.Topcourse
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                       "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/pluginStyle/css").Include(

                //TreeView
                "~/Content/plugins/treeview/bootstrap-treeview.css",
                "~/Content/plugins/treeview/jquery.treegrid.css",

                // dataTable
                "~/Content/plugins/dataTables/buttons.dataTables.min.css",
                "~/Content/plugins/dataTables/dataTables.bootstrap.min.css",
                "~/Content/plugins.css"

                ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/Scripts/bootstrap.js",
                       "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css"));


            bundles.Add(new ScriptBundle("~/bundles/JqueryJs").Include(
                "~/Scripts/plugins/bootbox/bootbox.min.js",

                //TrueViewJS
                "~/Scripts/plugins/treeview/bootstrap-treeview.js",
                "~/Scripts/plugins/treeview/jquery.nestable.js",
                "~/Scripts/plugins/treeview/jquery.treegrid.js",

                //Maxlength
                "~/Scripts/plugins/bootstrap-maxlength/bootstrap-maxlength.min.js",

                //MultiSelected
                "~/Scripts/plugins/multi-selected/bootstrap-multiselect.js",

                // table Export
                "~/Scripts/plugins/tableExport/tableExport.min.js",
                "~/Scripts/plugins/tableExport/html2canvas.min.js",
                "~/Scripts/plugins/tableExport/jspdf.min.js",
                "~/Scripts/plugins/tableExport/jspdf.plugin.autotable.js",
                "~/Scripts/plugins/tableExport/xlsx.core.min.js",

                // Json export excel
                "~/Scripts/plugins/jsonExport/excelexportjs.js",

                // dataTable
                "~/Scripts/plugins/dataTables/jquery.dataTables.js",
                "~/Scripts/plugins/dataTables/dataTables.bootstrap.js",
                "~/Scripts/plugins/dataTables/jszip.min.js",
                "~/Scripts/plugins/dataTables/pdfmake.min.js",
                "~/Scripts/plugins/dataTables/vfs_fonts.js"

                //"~/Scripts/plugins/dataTables/html5-button/dataTables.buttons.min.js",
                //"~/Scripts/plugins/dataTables/html5-button/buttons.html5.min.js",
                //"~/Scripts/plugins/dataTables/html5-button/buttons.colVis.min.js",
                //"~/Scripts/plugins/dataTables/html5-button/buttons.print.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                 "~/Scripts/Common/common.js",
                 "~/Scripts/Common/utils.js"
             ));
        }
    }
}
