<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Arad.SMS.Gateway.Web.HomePages.AdminPanel.Arad.Index" %>

<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Globalization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta id="keywords" name="keywords" content="" runat="server" />
	<meta id="description" runat="server" name="description" content="" />
	<link id="favicon" runat="server" rel="shortcut icon" />
	<%: Styles.Render("~/Content/bootstrap") %>
    <%--<link href="../../../Content/bootstrap.css" rel="stylesheet" />--%>
	<%: Styles.Render("~/Content/font-awesome") %>
	<%: Styles.Render("~/Content/ace") %>
	<%: Styles.Render("~/Content/easyui") %>
	<%: Scripts.Render("~/Scripts/jquery") %>
	<%: Scripts.Render("~/Scripts/bootstrap") %>
    <script src="../../../Scripts/ace-extra.min.js"></script>
    <script src="/script/routines.min.js" type="text/javascript"></script>
	<link href="/Content/mycss.css" rel="stylesheet" />

	<script type="text/javascript">
        $(function () {
            var addMenusToDom = function (menuData) {
                var menuTemplate = $("#menuTemplate").template("menuTemplate");
                $.tmpl(menuTemplate, menuData).appendTo("#nav");
            };

            var addShortcutsToDom = function (shortcutData) {
                var shortcutTemplate = $("#shortcutTemplate").template("shortcutTemplate");
                $.tmpl(shortcutTemplate, shortcutData).appendTo("#shortcutList");
            };

            $.ajax({
                url: "/homepages/adminpanel/arad/index.aspx/GetMenu",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                type: 'POST',
                async: false,
                cache: false
            }).done(function (data) {
                var menuData = { items: data.d };
                addMenusToDom(menuData);
            });

            $.ajax({
                url: "/homepages/adminpanel/arad/index.aspx/GetShortcut",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                type: 'POST',
                async: false,
                cache: false
            }).done(function (data) {
                var shortcutData = { items: data.d };
                addShortcutsToDom(shortcutData);
            });

            $('#tabList').tabs({
                border: false,
                onAdd: function (title, index) {
                    $(".tabs-panels").setOverlay('/pic/loader.gif');
                }
            });
        });

        var index = 1;
        function addTab(title, url) {
            var winHeight = jQuery(window).height();
            var winWidth = jQuery(window).width();
            if (!$("#sidebar").hasClass('menu-min'))
                winWidth -= 190;
            else
                winWidth -= 45;

            $(".panel").css("width", winWidth + "px");
            $(".panel-body").css("width", winWidth + "px");

            $("#tabList").attr("width", winWidth);
            if ($('#tabList').tabs('exists', title)) {
                $('#tabList').tabs('select', title);
                reloadTab($('#tabList').tabs('getSelected'));
            } else {
                var content = '<iframe id=iframe' + index + '  frameborder="0"  src="' + url + '" style="width:' + winWidth + 'px;height:1100px;" onload="loadComplete();" ></iframe>';

                $('#tabList').tabs('add', {
                    title: title,
                    content: content,
                    closable: true,
                    fit: true,
                    tools: [{
                        iconCls: 'icon-mini-refresh',
                        handler: function () {
                            reloadTab($('#tabList').tabs('getSelected'));
                        }
                    }]
                });
            }
            index++;
        }

        function reloadTab(tab) {
            $(".tabs-panels").setOverlay('/pic/loader.gif');
            var src = $(tab).find('iframe')[0].src;
            $(tab).find('iframe')[0].src = src;

        }

        function loadComplete() {
            $(".tabs-panels").removeOverlay();
        }

        $(document).ready(function () {
            setInterval(function () {
                var winHeight = jQuery(window).height();
                var winWidth = jQuery(window).width();
                if (winWidth <= 990) {
                    $("#sidebar").addClass("mobileDisplay");
                }
                
                if (!$("#sidebar").hasClass('menu-min') && !$("#sidebar").hasClass('mobileDisplay'))
                    winWidth -= 190;
                else
                    winWidth -= 0; 

                $(".panel").css("width", winWidth + "px");
                $(".panel-body").css("width", winWidth + "px");
                
                var iframeID = "iframe";

                for (var i = 0; i < index; i++) {
                    $("#" + iframeID + i).css({ width: winWidth + "px" });
                }
            }, 150);
        });
    </script>

	<script id="menuTemplate" type="text/html">
		<ul id="navList" class="nav nav-list">
			{{each items}}
			{{if Children.length > 0 && !ActiveLink}}
				<li class="hsub">
					<a href="#" class="dropdown-toggle">
						<span class="${SmallIcon}"></span>
						<span class="menu-text"><span>${Title}</span></span>
						<b class="arrow fa fa-angle-down"></b>
					</a>
					<b class="arrow"></b>
					<ul class="submenu">
						{{each Children}}
							{{if ActiveLink}}
								<li class="">
									<a href="#" onclick="addTab('${Title}','${Path}')">
										<span class="${SmallIcon}"></span>
										${Title}
									</a>
									<b class="arrow"></b>
								</li>
						{{else Children.length > 0 && !ActiveLink}}
								<li class="hsub">
									<a href="#" class="dropdown-toggle">
										<img src="/${SmallIcon.replace('Images','pic')}" />
										<span class="menu-text">${Title}</span>
										<b class="arrow fa fa-angle-down"></b>
									</a>
									<b class="arrow"></b>
									<ul class="submenu">
										{{tmpl({items:Children}) "menuTemplate"}}
									</ul>
								</li>
						{{/if}}
						{{/each}}
					</ul>
				</li>
			{{else}}
				<li class="">
					<a href="#" onclick="addTab('${title}','${Path}')">
						<img src="/${SmallIcon.replace('Images','pic')}" />
						${Title}
					</a>
					<b class="arrow"></b>
				</li>
			{{/if}}
		{{/each}}
		</ul>
	</script>

	<script id="shortcutTemplate" type="text/html">
		{{each items}}
			{{if LargeIcon != '' }}
				<a href="#" onclick="addTab('${Title}','${Path}')" style="display: inline;">
					<span class="thumbnail ${LargeIcon}" title="${Title}"></span>
				</a>
		{{/if}}
		{{/each}}
	</script>

	<style>
		.flatButton {
			border: 0;
			background-color: transparent;
			width: 90%;
			text-align: right;
		}

		.panel-body {
			padding: 0;
		}

        @media (max-width:991px) {
            .navbar-toggle {
                display: block;
            }
        }

        
	</style>
</head>

<% if(Session["Language"].ToString() == "en") { %>
<body class="no-skin">
<% }  else
   {%>
    <%: Styles.Render("~/Content/bootstrapRtl") %>
<body class="no-skin rtl">
<%  } %>
<form id="form1" runat="server">
		<div id="navbar" class="navbar navbar-default" role="navigation">
			<script type="text/javascript">
                try { ace.settings.check('navbar', 'fixed') } catch (e) { }
            </script>
			<div class="navbar-container" id="navbar-container">
				<button type="button" class="pull-right navbar-toggle collapsed" id="menu-toggler" data-toggle="collapse" data-target="#sidebar">
					<span class="sr-only">Toggle sidebar</span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
				</button>
				<%--<div class="col-xs-4 col-md-4">--%>
				<div class="navbar-header pull-right">
					<a href="#" class="navbar-brand" style="padding: 0;">
						<small>
							<asp:Image ID="imgLogo" runat="server" Style="height: 45px" />
							<asp:Literal ID="ltrCompanyName" runat="server"></asp:Literal>
						</small>
					</a>
				</div>
				<%--</div>
				<div class="col-xs-4 col-md-4">
				</div>
				<div class="col-xs-4 col-md-4">--%>
				<div class="navbar-buttons navbar-header pull-left" role="navigation">
					<ul class="nav ace-nav">
						<li class="purple">
							<a data-toggle="dropdown" class="dropdown-toggle" href="#">
								<i style="cursor: pointer; vertical-align: middle" class="fa fa-2x fa-th"></i>
								<span class="badge badge-grey"></span>
							</a>
                            <% if(Session["Language"].ToString() == "en") { %>
                            <ul class="dropdown-menu-left dropdown-navbar dropdown-menu dropdown-caret dropdown-close">
                            <% }  else
                               {%>
                                <%: Styles.Render("~/Content/bootstrapRtl") %>
                            <ul class="dropdown-menu-right dropdown-navbar dropdown-menu dropdown-caret dropdown-close">
                            <%  } %>
							
								<li class="dropdown-header">
									<i class="ace-icon fa fa-check"></i>
									<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ShortcutList") %>
										<i onclick="addTab('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DefineUserShortcut") %>','/PageLoader.aspx?c=<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_UserShortcuts,Session) %>')" style="cursor: pointer; float: right; padding-top: 4px;" class="fa fa-plus-square fa-2x"></i>
								</li>
								<li id="shortcutList" style="margin: 5px;"></li>
							</ul>
						</li>
						<li>
                            
                            <%--<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="changeLang" />--%>
                            
                        </li>
						<li class="light-blue" style="width: 200px;">
							<a data-toggle="dropdown" href="#" class="dropdown-toggle">
								<span class="fa fa-2x fa-user" style="margin: 5px;"></span>
								<span class="user-info">
									<small><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("WelcomeToPanel") %></small>
									<asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
									<br />
								</span>
								<i class="ace-icon fa fa-caret-down"></i>
							</a>
							<ul class="user-menu dropdown-menu-left dropdown-menu dropdown-yellow dropdown-caret dropdown-close">
								<li>
									<a href="#" onclick="addTab('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Profile") %>','/PageLoader.aspx?c=<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_EditProfile,Session) %>')">
										<i class="ace-icon fa fa-user blue"></i>
										<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Profile") %>
									</a>
								</li>
								<li>
									<a href="#" onclick="addTab('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ChangePassword") %>','/PageLoader.aspx?c=<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_ChangePassword,Session) %>')">
										<i class="ace-icon fa fa-key orange"></i>
										<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ChangePassword") %>
									</a>
								</li>
								<li>
									<a href="#" onclick="addTab('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("LoginStat") %>','/PageLoader.aspx?c=<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_LoginStat,Session) %>')">
										<i class="ace-icon fa fa-bar-chart-o green"></i>
										<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("LoginStat") %>
									</a>
								</li>
								<li class="divider"></li>
								<li>
									<a href="#">
										<i class="ace-icon fa fa-power-off red"></i>
										<% if(Session["Language"].ToString() == "en") { %>
												<asp:Button ID="btnSignOut" runat="server" CssClass="flatButton" Text="Logout" OnClick="btnSignOut_Click" />
										<% }  else
										   {%>
											<asp:Button ID="Button2" runat="server" CssClass="flatButton" Text="خروج" OnClick="btnSignOut_Click" />
										<%  } %>
									</a>
								</li>
							</ul>
						</li>
					</ul>
				</div>
				<table style="direction: ltr; float: left; height: 45px;">
					<tr style="border-bottom: 1px solid #fff">
						<td style="background-color: rgb(0,102,153)">
							<img src="/pic/clock/dk8.gif" id="yr1" />
							<img src="/pic/clock/dk8.gif" id="yr2" />
							<img src="/pic/clock/dk8.gif" id="yr3" />
							<img src="/pic/clock/dk8.gif" id="yr4" />
							<img src="/pic/clock/dkc.gif" />
							<img src="/pic/clock/dk8.gif" id="mt1" />
							<img src="/pic/clock/dk8.gif" id="mt2" />
							<img src="/pic/clock/dkc.gif" />
							<img src="/pic/clock/dk8.gif" id="dy1" />
							<img src="/pic/clock/dk8.gif" id="dy2" />
						</td>
					</tr>
					<tr>
						<td style="background-color: rgb(0,102,153); text-align: center;">
							<img src="/pic/clock/dk8.gif" id="hr1" />
							<img src="/pic/clock/dk8.gif" id="hr2" />
							<img src="/pic/clock/dkc.gif" />
							<img src="/pic/clock/dk8.gif" id="mn1" />
							<img src="/pic/clock/dk8.gif" id="mn2" />
							<img src="/pic/clock/dkc.gif" />
							<img src="/pic/clock/dk8.gif" id="se1" />
							<img src="/pic/clock/dk8.gif" id="se2" />
						</td>
					</tr>
				</table>
				<%--</div>--%>
			</div>
		</div>

		<div class="main-container ace-save-state" id="main-container">
			<div id="sidebar" class="sidebar navbar-collapse collapse ace-save-state">
				<div class="sidebar-shortcuts" id="sidebar-shortcuts">
					<div class="sidebar-shortcuts-large" id="sidebar-shortcuts-large">
						<a href="#" onclick="addTab('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("TariffLists")%>','/PageLoader.aspx?c=<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_ShowUserSmsRates, Session)%>')">
							<span class="btn btn-success" style="width: 100%;">
								<span class="ui-icon fa fa-usd white"></span>
								<%=credit %> <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Sms") %>
							</span>
						</a>
					</div>
					<div class="sidebar-shortcuts-mini" id="sidebar-shortcuts-mini">
						<span class="ui-icon fa fa-usd green"></span>
					</div>
				</div>
				<div id="nav"></div>
				<div class="sidebar-toggle sidebar-collapse" id="sidebar-collapse">
					<i class="ace-icon fa fa-angle-double-right" data-icon1="ace-icon fa fa-angle-double-right" data-icon2="ace-icon fa fa-angle-double-left"></i>
				</div>
			</div>
			<div class="main-content">
				<div style="width: 100%">
					<div id="tabList" class="easyui-tabs">
						<div title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Dashboard") %>">
							<iframe class="contentFrame" id="iframe0" scrolling="no" runat="server" name="contentFrame" frameborder="0" style="width: 100%; height: 1100px;"></iframe>
						</div>
					</div>
				</div>
			</div>
		</div>
	</form>

	<%--<script src="../../../Scripts/bootstrap.min.js"></script>--%>

	<%--	<%: Scripts.Render("~/Scripts/ace1") %>
	<%: Scripts.Render("~/Scripts/easyui") %>--%>

<script src="../../../Scripts/ace-elements.min.js"></script>
	<script src="../../../Scripts/ace.min.js">
    </script>



<script src="../../../Scripts/jquery.easyui.min.js"></script>
<script src="../../../Scripts/easyui-rtl.js"></script>
    <script>
        var flag = false;
        $(document).ready(function() {
            $(window).on("resize", function () {
                
                
            });
            $(document).on("click","#sidebar-collapse",
                function() {
					if (!flag) {
                        $(this).find("i").removeClass("fa-angle-double-right");
                        $(this).find("i").addClass("fa-angle-double-left");
                        $(this).parent().addClass("menu-min");
                    } else {
                        $(this).find("i").addClass("fa-angle-double-right");
                        $(this).find("i").removeClass("fa-angle-double-left");
                        $(this).parent().removeClass("menu-min");
                    }
                    console.log("click");
                    flag = !flag;
                });
        });
    </script>

<script type="text/javascript">
    dg0 = new Image(); dg0.src = "/pic/clock/dk0.gif";
    dg1 = new Image(); dg1.src = "/pic/clock/dk1.gif";
    dg2 = new Image(); dg2.src = "/pic/clock/dk2.gif";
    dg3 = new Image(); dg3.src = "/pic/clock/dk3.gif";
    dg4 = new Image(); dg4.src = "/pic/clock/dk4.gif";
    dg5 = new Image(); dg5.src = "/pic/clock/dk5.gif";
    dg6 = new Image(); dg6.src = "/pic/clock/dk6.gif";
    dg7 = new Image(); dg7.src = "/pic/clock/dk7.gif";
    dg8 = new Image(); dg8.src = "/pic/clock/dk8.gif";
    dg9 = new Image(); dg9.src = "/pic/clock/dk9.gif";

    function dotime() {
        theTime = setTimeout('dotime()', 1000);
        d = new Date();
    
        hr = d.getHours() + 100;
        mn = d.getMinutes() + 100;
        se = d.getSeconds() + 100;
        tot = '' + hr + mn + se;
        $("#hr1").attr('src', '/pic/clock/dk' + tot.substring(1, 2) + '.gif');
        $("#hr2").attr('src', '/pic/clock/dk' + tot.substring(2, 3) + '.gif');
        $("#mn1").attr('src', '/pic/clock/dk' + tot.substring(4, 5) + '.gif');
        $("#mn2").attr('src', '/pic/clock/dk' + tot.substring(5, 6) + '.gif');
        $("#se1").attr('src', '/pic/clock/dk' + tot.substring(7, 8) + '.gif');
        $("#se2").attr('src', '/pic/clock/dk' + tot.substring(8, 9) + '.gif');
    }
    dotime();

	function dodate() {
		<% if(Session["Language"].ToString() == "fa") { %>
        <% var newDateObject = new PersianCalendar(); %>
			dy = '<%=newDateObject.GetDayOfMonth(DateTime.Now).ToString("00")%>';
			mn = '<%=newDateObject.GetMonth(DateTime.Now).ToString("00")%>';
			yr = '<%=newDateObject.GetYear(DateTime.Now).ToString("0000")%>';
		<% }  else
		   {%>
			dy = '<%=DateTime.Now.Day.ToString("00")%>';
			mn = '<%=DateTime.Now.Month.ToString("00")%>';
			yr = '<%=DateTime.Now.Year.ToString()%>';
		<%  } %>

        tot = '' + dy + mn + yr;
        $("#dy1").attr('src', '/pic/clock/dk' + tot.substring(0, 1) + '.gif');
        $("#dy2").attr('src', '/pic/clock/dk' + tot.substring(1, 2) + '.gif');
        $("#mt1").attr('src', '/pic/clock/dk' + tot.substring(2, 3) + '.gif');
        $("#mt2").attr('src', '/pic/clock/dk' + tot.substring(3, 4) + '.gif');
        $("#yr1").attr('src', '/pic/clock/dk' + tot.substring(4, 5) + '.gif');
        $("#yr2").attr('src', '/pic/clock/dk' + tot.substring(5, 6) + '.gif');
        $("#yr3").attr('src', '/pic/clock/dk' + tot.substring(6, 7) + '.gif');
        $("#yr4").attr('src', '/pic/clock/dk' + tot.substring(7, 8) + '.gif');
    }
    dodate();
</script>
</body>
</html>
