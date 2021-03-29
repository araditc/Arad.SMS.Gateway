<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="500.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI._500" %>

<div class="page-content">
	<div class="ace-settings-container" id="ace-settings-container">
		<div class="btn btn-app btn-xs btn-warning ace-settings-btn" id="ace-settings-btn">
			<i class="ace-icon fa fa-cog bigger-130"></i>
		</div>

		<div class="ace-settings-box clearfix" id="ace-settings-box">
			<div class="pull-left width-50">
				<div class="ace-settings-item">
					<div class="pull-left">
						<select id="skin-colorpicker" class="hide">
							<option data-skin="no-skin" value="#438EB9">#438EB9</option>
							<option data-skin="skin-1" value="#222A2D">#222A2D</option>
							<option data-skin="skin-2" value="#C6487E">#C6487E</option>
							<option data-skin="skin-3" value="#D0D0D0">#D0D0D0</option>
						</select><div class="dropdown dropdown-colorpicker"><a data-toggle="dropdown" class="dropdown-toggle" href="#"><span class="btn-colorpicker" style="background-color: #438EB9"></span></a>
							<ul class="dropdown-menu dropdown-caret">
								<li><a class="colorpick-btn selected" href="#" style="background-color: #438EB9;" data-color="#438EB9"></a></li>
								<li><a class="colorpick-btn" href="#" style="background-color: #222A2D;" data-color="#222A2D"></a></li>
								<li><a class="colorpick-btn" href="#" style="background-color: #C6487E;" data-color="#C6487E"></a></li>
								<li><a class="colorpick-btn" href="#" style="background-color: #D0D0D0;" data-color="#D0D0D0"></a></li>
							</ul>
						</div>
					</div>
					<span>&nbsp; Choose Skin</span>
				</div>

				<div class="ace-settings-item">
					<input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-navbar">
					<label class="lbl" for="ace-settings-navbar">Fixed Navbar</label>
				</div>

				<div class="ace-settings-item">
					<input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-sidebar">
					<label class="lbl" for="ace-settings-sidebar">Fixed Sidebar</label>
				</div>

				<div class="ace-settings-item">
					<input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-breadcrumbs">
					<label class="lbl" for="ace-settings-breadcrumbs">Fixed Breadcrumbs</label>
				</div>

				<div class="ace-settings-item">
					<input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-rtl">
					<label class="lbl" for="ace-settings-rtl">Right To Left (rtl)</label>
				</div>

				<div class="ace-settings-item">
					<input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-add-container">
					<label class="lbl" for="ace-settings-add-container">
						Inside
											<b>.container</b>
					</label>
				</div>
			</div>
			<!-- /.pull-left -->

			<div class="pull-left width-50">
				<div class="ace-settings-item">
					<input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-hover">
					<label class="lbl" for="ace-settings-hover">Submenu on Hover</label>
				</div>

				<div class="ace-settings-item">
					<input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-compact">
					<label class="lbl" for="ace-settings-compact">Compact Sidebar</label>
				</div>

				<div class="ace-settings-item">
					<input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-highlight">
					<label class="lbl" for="ace-settings-highlight">Alt. Active Item</label>
				</div>
			</div>
			<!-- /.pull-left -->
		</div>
		<!-- /.ace-settings-box -->
	</div>
	<!-- /.ace-settings-container -->

	<div class="row">
		<div class="col-xs-12">
			<!-- PAGE CONTENT BEGINS -->

			<div class="error-container">
				<div class="well">
					<h1 class="grey lighter smaller">
						<span class="blue bigger-125">
							<i class="ace-icon fa fa-random"></i>
							500
						</span>
						Something Went Wrong
					</h1>

					<hr>
					<h3 class="lighter smaller">But we are working
											<i class="ace-icon fa fa-wrench icon-animated-wrench bigger-125"></i>
						on it!
					</h3>

					<div class="space"></div>

					<div>
						<h4 class="lighter smaller">Meanwhile, try one of the following:</h4>

						<ul class="list-unstyled spaced inline bigger-110 margin-15">
							<li>
								<i class="ace-icon fa fa-hand-o-right blue"></i>
								Read the faq
							</li>

							<li>
								<i class="ace-icon fa fa-hand-o-right blue"></i>
								Give us more info on how this specific error occurred!
							</li>
						</ul>
					</div>

					<hr>
					<div class="space"></div>

					<div class="center">
						<a href="javascript:history.back()" class="btn btn-grey">
							<i class="ace-icon fa fa-arrow-left"></i>
							Go Back
						</a>

						<a href="#" class="btn btn-primary">
							<i class="ace-icon fa fa-tachometer"></i>
							Dashboard
						</a>
					</div>
				</div>
			</div>

			<!-- PAGE CONTENT ENDS -->
		</div>
		<!-- /.col -->
	</div>
	<!-- /.row -->
</div>
