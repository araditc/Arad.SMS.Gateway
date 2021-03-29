<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Maps.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsSends.Maps" %>

<script type="text/javascript">
	$(function () {
		$('.linkMap').click(function () {
			debugger;
			var map = $(this).attr('map');
			$("#imgMap").attr('src', '/maps/' + map);
		});
	});
</script>
<div class="col-xs-12 col-md-12">
	<div class="row">
		<hr />
		<div class="col-md-2 col-sm-2">
			<a href="#" map="esfahan" class="btn btn-primary linkMap"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Esfahan") %></a>
		</div>
		<div class="clear"></div>
		<hr />
		<div id="map">
			<img id="imgMap" style="width: 100%; height: 100%" />
		</div>
	</div>
</div>
