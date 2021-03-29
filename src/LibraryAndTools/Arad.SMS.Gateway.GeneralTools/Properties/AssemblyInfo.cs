using System.Reflection;
using System.Runtime.InteropServices;
using System.Web.UI;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Arad.SMS.Gateway.GeneralTools")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Arad ITC")]
[assembly: AssemblyProduct("Arad.SMS.Gateway.GeneralTools")]
[assembly: AssemblyCopyright("Copyright © Arad ITC 2005 - 2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("56629113-a014-4de9-9860-0cadcae131aa")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

#region DataGrid
[assembly: WebResourceAttribute("GeneralTools.DataGrid.JavaScripts.i18n.en.js", "text/javascript")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.JavaScripts.i18n.fa.js", "text/javascript")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.JavaScripts.jqueryGrid.js", "text/javascript")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.JavaScripts.jqueryGrid.min.js", "text/javascript")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.JavaScripts.jqueryJson_min.js", "text/javascript")]

[assembly: WebResourceAttribute("GeneralTools.DataGrid.Images.BooleanOn.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Images.BooleanOff.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Images.Excel.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Images.Pdf.png", "image/png")]

[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.uiGrid.css", "text/css", PerformSubstitution = true)]

[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.redmond.css", "text/css", PerformSubstitution = true)]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.darkHive.darkHive.css", "text/css", PerformSubstitution = true)]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.lightness.lightness.css", "text/css", PerformSubstitution = true)]

[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-icons_f9bd01_256x240.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-icons_d8e7f3_256x240.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-icons_cd0a0a_256x240.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-icons_6da8d5_256x240.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-icons_469bdd_256x240.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-icons_2e83ff_256x240.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-icons_217bc0_256x240.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-bg_inset-hard_100_fcfdfd_1x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-bg_inset-hard_100_f5f8f9_1x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-bg_gloss-wave_55_5c9ccc_500x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-bg_glass_95_fef1ec_1x400.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-bg_glass_85_dfeffc_1x400.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-bg_glass_75_d0e5f5_1x400.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-bg_flat_55_fbec88_40x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-bg_flat_0_aaaaaa_40x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.redmond.images.ui-anim_basic_16x16.gif", "image/gif")]

[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.darkHive.images.ui-bg_flat_30_cccccc_40x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.darkHive.images.ui-bg_flat_50_5c5c5c_40x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.darkHive.images.ui-bg_glass_40_ffc73d_1x400.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.darkHive.images.ui-bg_highlight-hard_20_0972a5_1x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.darkHive.images.ui-bg_highlight-soft_33_003147_1x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.darkHive.images.ui-bg_highlight-soft_35_222222_1x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.darkHive.images.ui-bg_highlight-soft_44_444444_1x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.darkHive.images.ui-bg_highlight-soft_80_eeeeee_1x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.darkHive.images.ui-bg_loop_25_000000_21x21.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.darkHive.images.ui-icons_222222_256x240.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.darkHive.images.ui-icons_4b8e0b_256x240.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.darkHive.images.ui-icons_a83300_256x240.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.darkHive.images.ui-icons_cccccc_256x240.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.darkHive.images.ui-icons_ffffff_256x240.png", "image/png")]

[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.lightness.images.ui-bg_diagonals-thick_18_b81900_40x40.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.lightness.images.ui-bg_diagonals-thick_20_666666_40x40.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.lightness.images.ui-bg_flat_10_000000_40x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.lightness.images.ui-bg_glass_100_f6f6f6_1x400.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.lightness.images.ui-bg_glass_100_fdf5ce_1x400.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.lightness.images.ui-bg_glass_65_ffffff_1x400.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.lightness.images.ui-bg_gloss-wave_35_f6a828_500x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.lightness.images.ui-bg_highlight-soft_100_eeeeee_1x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.lightness.images.ui-bg_highlight-soft_75_ffe45c_1x100.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.lightness.images.ui-icons_222222_256x240.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.lightness.images.ui-icons_228ef1_256x240.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.lightness.images.ui-icons_ef8c08_256x240.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.lightness.images.ui-icons_ffd27a_256x240.png", "image/png")]
[assembly: WebResourceAttribute("GeneralTools.DataGrid.Themes.lightness.images.ui-icons_ffffff_256x240.png", "image/png")]
#endregion

#region SearchBox
[assembly: WebResourceAttribute("GeneralTools.SearchBox.JavaScripts.jquery_Autocomplete_min.js", "text/javascript")]

[assembly: WebResourceAttribute("GeneralTools.SearchBox.Css.styles_min.css", "text/css", PerformSubstitution = true)]

[assembly: WebResourceAttribute("GeneralTools.SearchBox.Images.indicator.gif", "image/gif")]
#endregion

#region SmsBodyBox
[assembly: WebResourceAttribute("GeneralTools.SmsBodyBox.JavaScripts.smsCalculations.js", "text/javascript")]
#endregion

#region SmsBodyControl
[assembly: WebResourceAttribute("GeneralTools.SmsBodyControl.Scripts.smsCalculations.js", "text/javascript")]
[assembly: WebResourceAttribute("GeneralTools.SmsBodyControl.Images.voice.gif", "image/gif")]
#endregion

#region TreeView
[assembly: WebResourceAttribute("GeneralTools.TreeView.JavaScripts.treeView.js", "text/javascript")]
#endregion