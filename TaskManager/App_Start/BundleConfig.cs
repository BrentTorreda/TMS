﻿using System.Web;
using System.Web.Optimization;

namespace TaskManager
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/respond.js",

                        "~/Scripts/datatables/jquery.dataTables.min.js",
                        "~/Scripts/datatables/dataTables.buttons.min.js",
                        "~/Scripts/datatables/dataTables.select.min.js",
                        "~/Scripts/datatables/dataTables.responsive.min.js",
                        "~/Scripts/dataTables.altEditor.free.js",

                        "~/Scripts/Countimer/dist/ez.countimer.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/bootstrap-datetimepicker.min.js",
                        "~/Scripts/summernote.js",
                        "~/Scripts/js.cookie.js",
                        "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));            

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/site.css",
                    "~/Content/toastr.css",
                    "~/Content/bootstrap-datetimepicker.css",
                    "~/Content/summernote.css",
                    "~/Content/font-awesome-4.7.0/css/font-awesome.css",

                    "~/Content/datatables/css/datatables.bootstrap4.css",
                    "~/Content/datatables/css/jquery.dataTables.min.css",
                    "~/Content/datatables/css/buttons.dataTables.min.css",  
                    "~/Content/datatables/css/select.dataTables.min.css",
                    "~/Content/datatables/css/responsive.dataTables.min.css"));
        }
    }
}
