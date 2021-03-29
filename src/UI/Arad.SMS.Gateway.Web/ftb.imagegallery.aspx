<%@ Page Language="C#" ValidateRequest=false Trace="false" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<script runat="server">
protected void Page_Load(Object Src, EventArgs E) {
	ImageGallery1.CurrentImagesFolder = "~/Images/" + GeneralLibrary.Helper.GetHostOfDomain(Request.Url.Host);
	ImageGallery1.RootImagesFolder = "~/Images/" + GeneralLibrary.Helper.GetHostOfDomain(Request.Url.Host);
	ImageGallery1.AcceptedFileTypes = new String[] { "jpeg", "jpg", "jpe", "bmp", "png", "gif", "swf","rar","zip"};
}
</script>
<html>
<head>
	<title>Image Gallery</title>
</head>
<body>
	<form id="Form1" runat="server" enctype="multipart/form-data">  
		<FTB:ImageGallery id="ImageGallery1" 
			JavaScriptLocation="ExternalFile" 
			UtilityImagesLocation="ExternalFile" 
			SupportFolder="~/aspnet_client/FreeTextBox/"
			AllowImageDelete=false AllowImageUpload=true AllowDirectoryCreate=false AllowDirectoryDelete=false runat="Server"/>
	</form>
</body>
</html>
