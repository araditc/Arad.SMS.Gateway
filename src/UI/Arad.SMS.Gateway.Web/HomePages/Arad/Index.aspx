<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" MasterPageFile="~/HomePages/Arad/Main.Master" Inherits="Arad.SMS.Gateway.Web.HomePages.Arad.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
   
	<link href="/aradcss/carousel" rel="stylesheet" />
	<style type="text/css">
		a:hover {
			color: #ff8100;
		}
	</style>
	<script type="text/javascript">
		$(function () {
			var addPackagesToDom = function (packages) {
				var packageTemplate = $("#packageTemplate").template("packageTemplate");
				$.tmpl(packageTemplate, packages).appendTo("#plans");
			};

			$.ajax({
				url: "/homepages/arad/index.aspx/GetSalePackages",
				contentType: 'application/json; charset=utf-8',
				dataType: "json",
				type: 'POST',
				async: false,
				cache: false
			}).done(function (data) {
				var packages = { items: data.d };
				addPackagesToDom(packages);
			});

			$("#slideShow div").first().addClass('active');
		});
	</script>

	<script id="packageTemplate" type="text/html">
		<div class="content-item pricing" style="direction: rtl">
            <div class="container">
			    {{if items.length>0}}
                <div class="content-headline">
                    <h2> <strong><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsPackage") %></strong></h2>
				</div>
                {{/if}}
				<div class="row">
                    {{each items}}
				    <div class="col-sm-4">
					    <div class="pricing-plan">
                            <h4>${title}</h4>
                            <div class="price"><span>${price} </span><%--ریال--%></div>
						    {{if services}}
							    <ul class="list-unstyled">
								    {{each services}}
									    <li><i class="fa fa-check green"></i>${$value}</li>
								    {{/each}}
							    </ul>
						    {{/if}}
						    {{if panelprice>0}}
                            <div class="order-wrapper"><a href="/package/${id}/Register" class="btn btn-normal btn-order"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SalePackage") %></a></div>
                            {{else}}
                            <div class="order-wrapper"><a href="/Register" class="btn btn-normal btn-order"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SalePackage") %></a></div>
                            {{/if}}
					    </div>
				    </div>
			    {{/each}}
                </div>
		    </div>
		</div>
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="clearfix"></div>
	<div class="jumbotron jumbotron-carousel">
		<div class="nav-inside owl-animation" id="homepage-1-carousel">
			<asp:Literal ID="ltrSlideShow" runat="server"></asp:Literal>
		</div>
    </div>
	<div class="clearfix"></div>

	<div class="site_punchtext"></div>
	<div class="clearfix mar_top4"></div>
    <!-- Plans -->
	<div class="hosting_plans" id="plans"></div>
    <!-- ==========================
    RECENT POSTS - START
    =========================== -->
    <section class="content-item" id="recent-posts">
        <div class="container">
            <div class="content-headline">
                <h2><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RecentPosts") %></h2>
            </div>
            <div class="row">
            	
                <!-- RECENT POST - START -->
                <asp:Literal ID="ltrNews" runat="server"></asp:Literal>
                <!-- RECENT POST - START -->
            </div>
        </div>
    </section>
    <!-- ==========================
    RECENT POSTS - END
    =========================== -->
	<div class="clearfix mar_top5"></div>
	<div class="features_sec01">
		<div class="container">
			<div id="centerContent">
				<asp:DataList ID="dtlContents" runat="server" RepeatColumns="2">
					<ItemStyle Width="50%" />
					<ItemTemplate>
						<ul>
							<h4>
								<a href='/<%#Eval("ID")%>/<%#Eval("Title").ToString().Replace(" ","-") %>'><%#Eval("Title") %></a>
							</h4>
							<asp:Literal ID="ltrSummary" runat="server" Text='<%# Eval("Summary") %>'></asp:Literal>
						</ul>
					</ItemTemplate>
				</asp:DataList>
			</div>
		</div>
	</div>
</asp:Content>

