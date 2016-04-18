using System.Web;
using System.Web.Optimization;

namespace Tamil.Grammar.Prosody.Parser.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/winstrap/dist/js/vendor/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                "~/Content/winstrap/dist/js/vendor/bootstrap.js"
                ));
            // The Kendo JavaScript bundle
            bundles.Add(new ScriptBundle("~/Scripts/kendo").Include(
                    "~/Scripts/kendo/2016.1/kendo.web.js"
                    ));

            // BOGDAN UI BUNDLE
            bundles.Add(new ScriptBundle("~/Scripts/customui").Include(
                    "~/Scripts/ui/ui.*"
                    ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
                "~/Content/prosody.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
            // Add boostrap css
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-responsive.css"));
            // Add kendo css
            bundles.Add(new StyleBundle("~/Content/kendoui").Include(
                "~/Content/kendo/2016.1/kendo.common.*"
                ));

            bundles.Add(new StyleBundle("~/Content/prosody").Include(
                "~/Content/prosody.css"));
            // Add winstrap
            bundles.Add(new StyleBundle("~/Content/winstrapcss").Include(
                "~/Content/winstrap/dist/css/winstrap.css"
            ));

            bundles.Add(new StyleBundle("~/Aurelia/styles").Include(
            "~/Aurelia/node_modules/font-awesome/css/font-awesome.css",
            "~/Aurelia/styles/styles.css"));
        }
    }
}
