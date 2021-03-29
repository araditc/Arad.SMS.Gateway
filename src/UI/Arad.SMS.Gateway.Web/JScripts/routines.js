$(document).ready(function () {

	//checkBrowserVersion();

	var item = "";
	item = $('*[rel="custom-scrollbar"]');
	for (i = 0; i < item.length; i++) {
		$(item[i]).mCustomScrollbar({
			advanced: {
				updateOnBrowserResize: true,
				updateOnContentResize: true,
				autoExpandHorizontalScroll: true
			},
			scrollButtons: {
				enable: true
			}
		});
	}
	// Horizantal
	item = $('*[rel="custom-scrollbar-horizantal"]');
	for (i = 0; i < item.length; i++) {
		$(item[i]).mCustomScrollbar({
			horizontalScroll: true,
			set_width: $(item[i]).attr('maxWidth'),
			advanced: {
				updateOnBrowserResize: true,
				updateOnContentResize: true,
				autoExpandHorizontalScroll: true
			},
			scrollButtons: {
				enable: true
			}
		});
	}
});

function detectBrowser() {
	var browserName;
	var browserVersion;

	$.browser.chrome = /chrome/.test(navigator.userAgent.toLowerCase());
	browserVersion = $.browser.version;
	if ($.browser.msie)
		browserName = 'msie';
	else if ($.browser.mozilla)
		browserName = 'mozilla';
	else if ($.browser.chrome) {
		browserName = 'chrome';
		var userAgent = navigator.userAgent.toLowerCase();
		userAgent = userAgent.substring(userAgent.indexOf('chrome/') + 7);
		userAgent = userAgent.substring(0, userAgent.indexOf('.'));
		browserVersion = userAgent;
	}
	else if ($.browser.safari)
		browserName = 'safari';
	else if ($.browser.opera)
		browserName = 'opera';

	var convertToInt = parseInt(browserVersion, 10);
	if (convertToInt != NaN)
		browserVersion = convertToInt;

	return [browserName.toString(), browserVersion.toString()];
}

function checkBrowserVersion() {
	var browserInfo = detectBrowser();
	var browserName = browserInfo[0];
	var browserVersion = browserInfo[1];
	switch (browserName) {
		case 'msie':
			if (browserVersion < 9)
				document.location = '../UI/AsreAsiaInfo/DownloadBrowsers.html';
			break;
		case 'chrome':
			if (browserVersion < 18)
				document.location = '../UI/AsreAsiaInfo/DownloadBrowsers.html';
			break;
		case 'opera':
			if (browserVersion < 5)
				document.location = '../UI/AsreAsiaInfo/DownloadBrowsers.html';
			break;
		case 'safari':
			if (browserVersion < 2)
				document.location = '../UI/AsreAsiaInfo/DownloadBrowsers.html';
			break;
		case 'mozilla':
			if (browserVersion < 11)
				document.location = '../UI/AsreAsiaInfo/DownloadBrowsers.html';
			break;
	}
}

String.prototype.trim = function () { return this.replace(/^\s\s*/, '').replace(/\s\s*$/, ''); };

String.prototype.ltrim = function () { return this.replace(/^\s+/, ''); };

String.prototype.rtrim = function () { return this.replace(/\s+$/, ''); };

String.prototype.fulltrim = function () { return this.replace(/(?:(?:^|\n)\s+|\s+(?:$|\n))/g, '').replace(/\s+/g, ' '); };

function importData(sourceString, tag) {
	try {
		if (sourceString.indexOf(tag + "{(") == -1)
			return "";
		var s;
		if (sourceString.indexOf(")}" + tag + "{(") != -1)
			s = sourceString.indexOf(")}" + tag + "{(") + tag.length + 4;
		else
			s = sourceString.indexOf(tag + "{(") + tag.length + 2;
		var e = sourceString.indexOf(")}", s);
		return sourceString.substring(s, e);
	}
	catch (e) {
		return "";
	}
}

function val(sourceString) {
	sourceString = replaceString(sourceString, ",", "");
	if (isNaN(sourceString) || sourceString == "")
		return 0
	else
		return parseInt(sourceString, 10);
}

function standardizeInputCharacters(input) {
	input.value = getStandardizeCharacters(input.value);
}

function getStandardizeCharacters(inputString) {
	t = replaceString(inputString, "'", "`");

	re = /ى/i;
	while (t.search(re) != -1)
		t = t.replace(re, "ي");
	re = /ی/i;
	while (t.search(re) != -1)
		t = t.replace(re, "ي");
	re = /ک/i;
	while (t.search(re) != -1)
		t = t.replace(re, "ك");

	return t;
}

function replaceString(sourceString, searchText, replaceText) {
	if (sourceString == "" || sourceString == null || sourceString == undefined)
		return "";

	sourceString = sourceString.toString();
	while (sourceString.search(searchText) != -1)
		sourceString = sourceString.replace(searchText, "_temp_string_");

	while (sourceString.search("_temp_string_") != -1)
		sourceString = sourceString.replace("_temp_string_", replaceText);

	return sourceString;
}

function handleRequiredInputBlur(input) {
	if (input.getAttribute("isRequired") == "true") {
		var originalBGColor = "white";
		if (input.getAttribute("originalBGColor") != undefined)
			originalBGColor = input.getAttribute("originalBGColor");

		if ((input.value != "" && input.value != undefined) || input.disabled == true)
			input.style.backgroundColor = originalBGColor;
	}
}

function hasParentsControlRequiredValidationConditions(control) {
	while (control) {
		if (control.style.display == "none")
			return false;
		else
			control = control.parentElement;
	}

	return true;
}

function hasControlRequiredValidationConditions(control, validationSet) {
	var ret =
		(control.getAttribute("isRequired") == "true" || control.getAttribute("isValid") == "false") &&
		(control.getAttribute("validationSet") == validationSet || control.getAttribute("ValidationSet") == validationSet) &&
		!control.disabled &&
		hasParentsControlRequiredValidationConditions(control);

	handleRequiredInputBlur(control);

	return ret;
}

function clearHighlightedInputs() {
	var inputsArray = document.getElementsByTagName("input");
	var textareasArray = document.getElementsByTagName("textarea");
	var selectsArray = document.getElementsByTagName("select");

	for (var i = 0; i < inputsArray.length; i++) {
		if (inputsArray[i].type == 'text' || inputsArray[i].type == 'password') {
			inputsArray[i].style.backgroundColor = 'white';
			inputsArray[i].value = '';
		}
	}
	for (var i = 0; i < textareasArray.length; i++) {
		textareasArray[i].style.backgroundColor = 'white';
	}
	for (var i = 0; i < selectsArray.length; i++) {
		selectsArray[i].style.backgroundColor = 'white';
	}
}

function findRootParrentWindow(findMainPageWindow) {
	var parentElement = parent;
	var prevParentElement = null;
	var prevPrevParentElement = null;

	do {
		prevPrevParentElement = prevParentElement;
		prevParentElement = parentElement;
		parentElement = parentElement.parent;
	}
	while (parentElement != prevParentElement)

	if (!prevPrevParentElement) 
		prevPrevParentElement = jQuery(window)[0];

	if (!prevParentElement)
		prevParentElement = jQuery(window)[0];

	if (findMainPageWindow)
		return prevParentElement;
	else
		return prevPrevParentElement;
}

function getAjaxResponse(methodName, parameters) {
	var extaraParams = "&PageRequseter=" + this.location.href;

	try {
		if (authenticationString != undefined)
			extaraParams += "&AuthenticationString=" + authenticationString;

		if (validationString != undefined)
			extaraParams += "&ValidationString=" + validationString;
	}
	catch (e) {
	}
	finally {
		var result = jQuery.ajax({
			url: "../ClientRequestHandlers.aspx",
			data: "method=" + methodName + extaraParams + "&" + parameters,
			type: 'POST',
			async: false, 	//Wait for result true for dont waitting and false for waitting
			cache: false, 	//Dont cache 
			timeout: 30000, //Wait 3 second for recieve result
			error: function () {
				return "";
			},
			success: function (msg) {
				return msg;
			}

		});

		if (result.status == 200) {
			if (importData(result.responseText, "ClientRequestHandlerError") == "1") {
				relaodMainPage();
				return "";
			}
			return result.responseText;
		}
		else
			return "";
	}
}

function getAjaxResponseAsync(methodName, parameters, successCallBack) {
	var extaraParams = "&PageRequseter=" + this.location.href;

	if (authenticationString != undefined)
		extaraParams += "&AuthenticationString=" + authenticationString;

	if (validationString != undefined)
		extaraParams += "&ValidationString=" + validationString;

	jQuery.ajax({
		url: "../ClientRequestHandlers.aspx",
		data: "method=" + methodName + extaraParams + "&" + parameters,
		type: 'POST',
		async: true, 	//Wait for result true for dont waitting and false for waitting
		cache: false, 	//Dont cache 
		timeout: 30000, //Wait 3 second for recieve result
		error: function () {
			return "";
		},
		success: function (msg) {
			if (importData(msg, "ClientRequestHandlerError") == "1") {
				relaodMainPage();
			} else if (successCallBack && typeof (successCallBack === 'function')) {
				successCallBack(msg);
			}
		}
	});
}

function redirectToUrl(url,redirectMainPage) {
	if (url) {
		var rootWindowElement = findRootParrentWindow(redirectMainPage);
		rootWindowElement.location.href = url;
	}
}

function relaodMainPage() {
	var rootWindowElement = findRootParrentWindow(true);
	rootWindowElement.location.reload(true);
}

function isValidPassword(password, confirmPassword) {
	if (password == confirmPassword)
		return true;
	else
		return false;
}

function toUTF8(szInput) {
	if (szInput == null || szInput == undefined || szInput == "")
		return szInput;

	var wch, x, uch = "", szRet = "";
	for (x = 0; x < szInput.length; x++) {
		wch = szInput.charCodeAt(x);

		if (!(wch & 0xFF80)) {
			szRet += "%" + wch.toString(16);
		} else if (!(wch & 0xF800)) {
			uch = "%" + (wch >> 6 | 0xC0).toString(16) + "%" + (wch & 0x3F | 0x80).toString(16);
			szRet += uch;
		} else {
			uch = "%" + (wch >> 12 | 0xE0).toString(16) + "%" + (((wch >> 6) & 0x3F) | 0x80).toString(16) + "%" + (wch & 0x3F | 0x80).toString(16);
			szRet += uch;
		}
	}

	return (szRet);
}

function getFormatDecimal(number) {
	var newNumber = number.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
	return newNumber;
}

//------------------------------ Modal Handler ------------------------------
function hideModal(divID) {
	jQuery("#" + divID).dialog("close");
}

function showModal(divID, destroy) {
	jQuery("#" + divID).dialog("destroy");
	jQuery("#" + divID).dialog({
		modal: true,
		autoOpen: false,
		resizable: false,
		width: jQuery("#" + divID).attr("width"),
		height: jQuery("#" + divID).attr("height"),
		show: "blind", //{ effect: "blind", mode: "show" },
		hide: "blind", //{ effect: "blind", mode: "hide" },
		beforeClose: function (event, ui) {
			if (destroy) {
				closeThisModal(jQuery("#" + divID)[0], 'CANCEL');
			}
		}
	});

	jQuery("#" + divID).dialog("open");
}

function showModalPage(id) {
	var maskHeight = "100%";
	var maskWidth = "100%";

	jQuery('#mask').css({ 'width': maskWidth, 'height': maskHeight });
	jQuery('#mask').show();

	var winH = jQuery('#mask').height();
	var winW = jQuery('#mask').width();

	jQuery("#" + id).css('top', winH / 2 - jQuery("#" + id).height() / 2);
	jQuery("#" + id).css('left', winW / 2 - jQuery("#" + id).width() / 2);
	jQuery("#" + id).css('display', '');

	jQuery("#" + id).draggable({ handle: jQuery("#modalHeader") });
	jQuery("#modalHeader").css({ cursor: 'move' });

	jQuery("#" + id).fadeIn(1000);
}

function closeModal(resultValue) {
	var parentModal = parent;
	var iframeModalParent = null;
	var divModal = null;

	do {
		divModal = parentModal.jQuery("#modalLoader")[0];
		iframeModalParent = parentModal;
		parentModal = parentModal.parent;
	}
	while (parentModal != iframeModalParent && !divModal)

	if (iframeModalParent && divModal)
		iframeModalParent.closeThisModal(divModal, resultValue);
}

function closeThisModal(modalLoader, resultValue) {
	var callback = jQuery(modalLoader).attr('callbackMethodName');
	jQuery(modalLoader).fadeOut(150, function () {
		jQuery('#mask').fadeOut();
		jQuery(modalLoader).remove();

		if (callback) {
			resultValue = resultValue.replace(/\n/g, "\\n");
			resultValue = resultValue.replace(/\r/g, "\\r");
			callback += "('" + resultValue + "');";
			eval(callback);
		}
	});
}

function getAjaxPage(pageID, parameters, title, callbackMethodName) {
	var iframeTag = document.createElement("iframe");
	var divTag = document.createElement("div");
	iframeTag.id = "loadIframe";
	iframeTag.src = "../PageLoader.aspx?IsModal=1&c=" + pageID + "&" + parameters;
	iframeTag.style.display = "none";
	iframeTag.style.backgroundColor = 'gray';
	iframeTag.style.width = "0px";
	iframeTag.style.height = "0px";
	iframeTag.style.overflow = "hidden";
	iframeTag.style.border = "0px";
	iframeTag.scrolling = "no";

	divTag.id = "modalLoader";
	divTag.style.width = "140px";
	divTag.style.height = "70px";
	divTag.className = "iframeWindow";
	divTag.style.padding = "2px";

	divTag.innerHTML = "<div id='modalHeader' class='ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix' style='position:relative;padding:0.4em 1em;text-align: center;margin-bottom:2px;'>" +
										 "	<span class='ui-dialog-title' id='ui-dialog-title'>" + title + "</span>" +
										 "	<a href='#' id='closeButton' class='ui-dialog-titlebar-close ui-corner-all' style='position:relative;float: right;border:1px;margin-right:-8px'>" +
										 "		<span class='ui-icon ui-icon-closethick'>close</span>" +
										 "	</a>" +
										 "</div>" +
										 "<div id='loaderImageDiv'><center><img style='margin:10px' src='../Images/loader.gif'/></center></div>";

	if (callbackMethodName)
		jQuery(divTag).attr('callbackMethodName', callbackMethodName);

	divTag.appendChild(iframeTag);
	document.body.appendChild(divTag);
	showModalPage('modalLoader');
}

function modalLoadCompleted(modalWindow) {
	var iframeElement = jQuery("#loadIframe")[0];
	var divElement = jQuery("#modalLoader")[0];
	
	jQuery("#loaderImageDiv")[0].style.display = 'none';
	iframeElement.style.display = '';

	var winW = jQuery(window).width();
	var winH = jQuery(window).height();
	var iframeWidth = jQuery(iframeElement.contentWindow.document).width() + 15;
	var iframeHeight = jQuery(iframeElement.contentWindow.document).height() + 5;

	if (iframeHeight - 20 > jQuery(window).height() - 75) {
		iframeHeight = jQuery(window).height() - 75;
		iframeWidth += 20;
	}

	var divTop = winH / 2 - (iframeHeight + 20) / 2;
	var divLeft = winW / 2 - iframeWidth / 2;

	jQuery(divElement).animate({
		"height": (iframeHeight + 28) + 'px',
		"width": iframeWidth + 'px',
		"top": divTop + 'px',
		"left": divLeft + 'px'
	},'slow',
			function () {
				iframeElement.style.display = 'none';
				jQuery(iframeElement).css({ "height": iframeHeight + 'px', "width": iframeWidth + 'px' });
				jQuery("#closeButton").fadeIn(500);
				jQuery("#closeButton").click(function (e) { closeThisModal(divElement, 'CANCEL'); });
				jQuery("#closeButton").mouseenter(function (e) { jQuery("#closeButton").addClass("ui-state-hover"); });
				jQuery("#closeButton").mouseout(function (e) { jQuery("#closeButton").removeClass("ui-state-hover"); });
				jQuery(iframeElement).fadeIn(500);
			});

	jQuery(modalWindow).keyup(function (e) {
		if (e.keyCode == 27)
			closeThisModal(divElement, 'CANCEL');
	});

	modalWindow.focus();
}

//*****Load()*****************
/////Load()-----------------------------------------------------------------------
jQuery.fn.getSerializeElement = function () {
	var resultString = '';
	var inputs = jQuery(this).find('input[type=text],input[type=password],input[type=hidden]');
	jQuery.each(inputs, function () {
		var el_name = encodeURIComponent(jQuery(this).attr('id'));
		var el_val = encodeURIComponent(jQuery(this).val());
		resultString = resultString + el_name + '=' + el_val + '&';
	});

	inputs = jQuery(this).find('input[type=checkbox],input[type=radio]');
	jQuery.each(inputs, function () {
		var el_name = encodeURIComponent(jQuery(this).attr('id'));
		var el_val = jQuery(this).attr('checked');
		if (el_val == 'checked')
			el_val = true;
		else
			el_val = false;
		resultString = resultString + el_name + '=' + el_val + '&';
	});

	inputs = jQuery(this).find('select');
	jQuery.each(inputs, function () {
		var el_name = encodeURIComponent(jQuery(this).attr('id'));
		var selectedOption = jQuery(this).find('option:selected');
		var el_val = jQuery(selectedOption).html();
		resultString = resultString + el_name + '=' + el_val + '&';
	});

	if (resultString.lastIndexOf('&') == resultString.length - 1)
		resultString = resultString.substring(0, resultString.length - 1);
	resultString = resultString.replace(/%26/g, '');

	return resultString;
}

jQuery.fn.setOverlayWithElement = function (elementId) {
	jQuery(this).each(function () {
		var overlay = document.createElement('div');
		jQuery(overlay).addClass('overlayWithElement');
		var overlayParentPaddingTop = jQuery(this).css('padding-top');
		var overlayParentPaddingLeft = jQuery(this).css('padding-left');
		overlayParentPaddingTop = parseInt(overlayParentPaddingTop.substring(0, overlayParentPaddingTop.indexOf('px')), 10);
		overlayParentPaddingLeft = parseInt(overlayParentPaddingLeft.substring(0, overlayParentPaddingLeft.indexOf('px')), 10);
		var t = jQuery(this).offset().top;
		var l = jQuery(this).offset().left;
		if (overlayParentPaddingTop)
			t += overlayParentPaddingTop;
		if (overlayParentPaddingLeft)
			l += overlayParentPaddingLeft;
		var h = jQuery(this).height();
		var w = jQuery(this).width();

		jQuery(overlay).append($("#" + elementId));
		jQuery(this).append(overlay);
		jQuery(overlay).css({
			top: t + 'px',
			left: l + 'px',
			height: h + 'px',
			width: w + 'px',
			display: 'block',
			opacity: '0'
		});
		jQuery("#" + elementId).css({
			position: 'absolute',
			top: (h - jQuery("#" + elementId).height()) / 2 + 'px',
			left: (w - jQuery("#" + elementId).width()) / 2 + 'px'
		});
		jQuery(overlay).animate({ opacity: 1.0 }, 'slow');
	});
}

jQuery.fn.setOverlay = function (loadingImagePath) {
	jQuery(this).each(function () {
		var overlay = document.createElement('div');
		jQuery(overlay).addClass('overlay');

		var overlayParentPaddingTop = jQuery(this).css('padding-top');
		var overlayParentPaddingLeft = jQuery(this).css('padding-left');
		overlayParentPaddingTop = parseInt(overlayParentPaddingTop.substring(0, overlayParentPaddingTop.indexOf('px')), 10);
		overlayParentPaddingLeft = parseInt(overlayParentPaddingLeft.substring(0, overlayParentPaddingLeft.indexOf('px')), 10);

		//var overlayParentPosition = jQuery(this).css('position');
		//var top = (overlayParentPosition == 'static' || overlayParentPosition == 'relative') ? 0 : jQuery(this).offset().top;
		//var left = (overlayParentPosition == 'static' || overlayParentPosition == 'relative') ? 0 : jQuery(this).offset().left;
		var top = jQuery(this).offset().top;
		var left = jQuery(this).offset().left;

//		if (overlayParentPaddingTop)
//			top += overlayParentPaddingTop;

//		if (overlayParentPaddingLeft)
//			left += overlayParentPaddingLeft;

		var height = jQuery(this).height();
		var width = jQuery(this).width();

		var loadingImageElement = document.createElement('img');
		loadingImageElement.src = loadingImagePath;

		jQuery(overlay).append(loadingImageElement);

		jQuery(this).append(overlay);

		jQuery(overlay).css({
			top: top + 'px',
			left: left + 'px',
			height: height + 'px',
			width: width + 'px',
			display: 'block',
			opacity: '0'
		});

		//jQuery(loadingImageElement).css({
		//	position: 'absolute',
		//	top: (height - jQuery(loadingImageElement).height()) / 2 + 'px',
		//	left: (width - jQuery(loadingImageElement).width()) / 2 + 'px'
		//});

		jQuery(loadingImageElement).css({
			position: 'absolute',
			top: 50 + 'px',
			left: (width - jQuery(loadingImageElement).width()) / 2 + 'px'
		});

		jQuery(overlay).animate({ opacity: 0.5 }, 'slow');
	});
}

jQuery.fn.setOverlayOnDialog = function (loadingImagePath) {
	jQuery(this).each(function () {
		var overlay = document.createElement('div');
		jQuery(overlay).addClass('overlay');
		var t = 0;
		var l = 0;
		var h = Math.round(jQuery(document).find('.ui-dialog-content.ui-widget-content').height());
		var w = Math.round(jQuery(document).find('.ui-dialog-content.ui-widget-content').width());
		jQuery(overlay).css({
			top: t + 'px',
			left: l + 'px',
			height: h + 10 + 'px',
			width: w + 23 + 'px',
			display: 'block',
			opacity: '0'
		});
		var loadingImageElement = document.createElement('img');
		loadingImageElement.src = loadingImagePath;
		jQuery(loadingImageElement).css({
			position: 'absolute',
			top: (h - jQuery(loadingImageElement).height()) / 2 + 'px',
			left: (w - jQuery(loadingImageElement).width()) / 2 + 'px'
		});
		jQuery(loadingImageElement).appendTo(jQuery(overlay));
		jQuery(overlay).appendTo(this);
		jQuery(overlay).animate({ opacity: 0.5 }, 'slow');
	});
}

jQuery.fn.removeOverlay = function () {
	jQuery(this).each(function () {
		jQuery(this).find('div.overlay').stop().animate({ opacity: 0.5 }, 'slow').remove();
	});
}

jQuery.fn.removeOverlayWithElement = function (element) {
	jQuery(this).each(function () {
		if (jQuery(this).find('div.overlayWithElement #' + element).length > 0)
			jQuery(this).find('div.overlayWithElement').stop().animate({ opacity: 0.5 }, 'slow').hide();
	});
}

jQuery.ajaxSetup({ cache: false });

jQuery.fn.loadPage = function (pageUrl, encryptUrl, beforeLoadCallBack, completeCallBack) {
	jQuery(this).each(function () {
		if (beforeLoadCallBack && typeof (beforeLoadCallBack) === 'function')
			beforeLoadCallBack();
		if (encryptUrl)
			pageUrl = encodeURIComponent(pageUrl);
		jQuery(this).load(pageUrl, function () {
			if (completeCallBack && typeof (completeCallBack) === 'function')
				completeCallBack();
		});
	});
}

jQuery.loadPageModal = function (pageUrl, modalPageTitle, beforeLoadCallBack, completeCallBack) {
	var divModalForm = document.createElement('div');
	if (beforeLoadCallBack && typeof (beforeLoadCallBack) === 'function')
		beforeLoadCallBack();
	var pageAddress = 'PageLoader.aspx?c=' + pageUrl;
	jQuery(divModalForm).loadPage(pageAddress, null, null);
	jQuery(divModalForm).appendTo(document);
	jQuery(divModalForm).dialog({ modal: true, title: modalPageTitle });
}

function doAjax(url, data, beforeSendCallBack, errorCallBack, successCallBack, completeCallBack) {
	jQuery.ajax({
		async: false,
		type: 'post',
		dataType: 'html',
		url: url,
		data: data,
		beforeSend: function () {
			if (beforeSendCallBack && typeof (beforeSendCallBack === 'function'))
				beforeSendCallBack();
		},
		error: function (jqXHR, textStatus, errorThrown) {
			if (errorCallBack && typeof (errorCallBack === 'function'))
				errorCallBack(errorThrown);
		},
		success: function (data) {
			if (successCallBack && typeof (successCallBack === 'function'))
				successCallBack(data);
		},
		complete: function () {
			if (completeCallBack && typeof (completeCallBack === 'function'))
				completeCallBack();
		}
	});
}

var getAjaxResult = function (data) {
	var returnValue = {
		result: (data.substring(7, 8) == '1') ? true : false,
		message: data.substring(17)
	}
	return returnValue;
}

function clearAllInputsValue(element) {
	for (var i = 0; i < element.childNodes.length; i++) {
		var e = element.childNodes[i];
		if (e.tagName)
			switch (e.tagName.toLowerCase()) {
			case 'input':
				switch (e.type) {
					case 'text':
					case 'password':
					case 'email':
					case 'tel':
					case 'file':
						e.value = '';
						break;
					case "radio":
					case "checkbox":
						e.checked = false;
						break;
					case 'button':
					case 'submit':
					case 'reset':
					case 'image':

						break;
				}
				break;
			case 'select':
				e.selectedIndex = 0;
				break;
			case 'textarea':
				e.value = '';
				break;
			default:
				clearAllInputsValue(e);
				break;
		}
	}
}