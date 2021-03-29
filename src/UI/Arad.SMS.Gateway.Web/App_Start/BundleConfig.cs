// --------------------------------------------------------------------
// Copyright (c) 2005-2020 Arad ITC.
//
// Author : Ammar Heidari <ammar@arad-itc.org>
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// --------------------------------------------------------------------

using System.Web.Optimization;

namespace Arad.SMS.Gateway.Web
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css",
																	    "~/Content/bootstrap-dialog.css",
																	    "~/Content/bootstrap-theme.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrapRtl").Include("~/Content/bootstrap-rtl.css"));

            //bundles.Add(new ScriptBundle("~/Scripts/bootstrap-datepicker-fa").Include("~/Scripts/bootstrap-datepicker.js",
            //                                                            "~/Scripts/bootstrap-datepicker.fa.js",
            //                                                            "~/Scripts/bootstrap-timepicker.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap-datepicker").Include("~/Scripts/bootstrap-datepicker-main.min.js",
                                                                        "~/Scripts/bootstrap-datepicker.en-GB.main.js"));

            bundles.Add(new ScriptBundle("~/Scripts/fullcalendar").Include(
																	 "~/Scripts/fullcalender/moment.js",
																	 "~/Scripts/fullcalender/moment-jalaali.js",
																	 "~/Scripts/fullcalender/fullcalendar.js",
																	 "~/Scripts/fullcalender/fa.js",
																	 "~/Scripts/fullcalender/bootbox.min.js"));


			bundles.Add(new StyleBundle("~/Content/bootstrap-datepicker").Include("~/Content/bootstrap-timepicker.css",
																	"~/Content/bootstrap-datepicker.css",
																	"~/Content/fullcallender/fullcalendar.min.css"));

			bundles.Add(new ScriptBundle("~/Scripts/datetimepicker").Include("~/Scripts/MdBootstrapPersianDateTimePicker/jalaali.js", "~/Scripts/MdBootstrapPersianDateTimePicker/jquery.Bootstrap-PersianDateTimePicker.js"));
			bundles.Add(new StyleBundle("~/Content/datetimepicker").Include("~/Content/MdBootstrapPersianDateTimePicker/jquery.Bootstrap-PersianDateTimePicker.css"));


			bundles.Add(new StyleBundle("~/Content/font-awesome").Include("~/Content/font-awesome.css"));

			bundles.Add(new StyleBundle("~/Content/ace").Include("~/Content/ace.css",
																													 "~/Content/ace-skins.css",
																													 "~/Content/ace-rtl.css"));

			bundles.Add(new StyleBundle("~/Content/easyui").Include("~/Content/themes/bootstrap/easyui.css", new CssRewriteUrlTransform())
																										 .Include("~/Content/themes/default/easyui-rtl.css", new CssRewriteUrlTransform())
																										 .Include("~/Content/themes/icon.css", new CssRewriteUrlTransform()));

			bundles.Add(new ScriptBundle("~/Scripts/acescript").Include("~/Scripts/ace.js",
																																	"~/Scripts/ace-extra.js",
																																	"~/Scripts/fuelux.wizard.js",
																																	"~/Scripts/ace-elements.js"
																																	));

			//bundles.Add(new ScriptBundle("~/Scripts/jseasyui").Include("~/Scripts/jquery.easyui.min.js",
			//																												 "~/Scripts/easyui-rtl.js"));

			bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include("~/Scripts/bootstrap.js",
																																	"~/Scripts/bootstrap-dialog.js"));

			bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
								"~/Scripts/jquery-{version}.js",
								"~/Scripts/jquery-migrate-{version}.js",
								"~/Scripts/jquery.unobtrusive-ajax.js",
								"~/JScripts/jquery.tmpl.min.js"));

			BundleTable.EnableOptimizations = true;
		}
	}
}
