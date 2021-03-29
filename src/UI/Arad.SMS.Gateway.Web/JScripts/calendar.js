var datePickerDivID = "datepicker";
var iFrameDivID = "datepickeriframe";

var dayArrayShort = new Array('&#1588;' , '&#1740;', '&#1583;', '&#1587;', '&#1670;', '&#1662;', '&#1580;');
var dayArrayMed = new Array('&#1588;&#1606;&#1576;&#1607;' , '&#1740;&#1705;&#1588;&#1606;&#1576;&#1607;', '&#1583;&#1608;&#1588;&#1606;&#1576;&#1607;', '&#1587;&#1607;&#32;&#1588;&#1606;&#1576;&#1607;', '&#1670;&#1607;&#1575;&#1585;&#1588;&#1606;&#1576;&#1607;' , '&#1662;&#1606;&#1580;&#1588;&#1606;&#1576;&#1607;' , '&#1580;&#1605;&#1593;&#1607;');
var dayArrayLong = dayArrayMed;
var monthArrayShort = new Array('&#1601;&#1585;&#1608;&#1585;&#1583;&#1740;&#1606;','&#1575;&#1585;&#1583;&#1740;&#1576;&#1607;&#1588;&#1578;','&#1582;&#1585;&#1583;&#1575;&#1583;','&#1578;&#1740;&#1585;','&#1605;&#1585;&#1583;&#1575;&#1583;','&#1588;&#1607;&#1585;&#1740;&#1608;&#1585;','&#1605;&#1607;&#1585;','&#1570;&#1576;&#1575;&#1606;','&#1570;&#1584;&#1585;','&#1583;&#1740;','&#1576;&#1607;&#1605;&#1606;','&#1575;&#1587;&#1601;&#1606;&#1583;');
var monthArrayMed = monthArrayShort;
var monthArrayLong = monthArrayShort;
var defaultDateSeparator = "/";				// common values would be "/" or "."
var defaultDateFormat = "ymd"		// valid values are "mdy", "dmy", and "ymd"
var dateSeparator = defaultDateSeparator;
var dateFormat = defaultDateFormat;

function displayDatePicker(showDateFieldName, dateFieldName, displayBelowThisObject, dtFormat, dtSep)
{
	var targetDateField = document.getElementsByName(dateFieldName).item(0);
	var showDateField = document.getElementsByName(showDateFieldName).item(0);

	if (!displayBelowThisObject)
		displayBelowThisObject = showDateField;
 
	if (dtSep)
		dateSeparator = dtSep;
	else
		dateSeparator = defaultDateSeparator;
 
	if (dtFormat)
		dateFormat = dtFormat;
	else
		dateFormat = defaultDateFormat;
 
	var x = displayBelowThisObject.offsetLeft;
	var y = displayBelowThisObject.offsetTop + displayBelowThisObject.offsetHeight ;
 
	var parent = displayBelowThisObject;
	while (parent.offsetParent) {
		parent = parent.offsetParent;
		x += parent.offsetLeft;
		y += parent.offsetTop ;
	}

	 drawDatePicker(showDateField, targetDateField, x + 15, y + 4);
}

function drawDatePicker(showDateField, targetDateField, x, y)
{
	var dt = getFieldDate(targetDateField.value );

	if (!document.getElementById(datePickerDivID)) {
		var newNode = document.createElement("div");
		newNode.setAttribute("id", datePickerDivID);
		newNode.setAttribute("class", "dpDiv");
		newNode.setAttribute("style", "visibility: hidden;");
		document.body.appendChild(newNode);
	}
 
	var pickerDiv = document.getElementById(datePickerDivID);
	pickerDiv.style.position = "absolute";
	pickerDiv.style.left = x + "px";
	pickerDiv.style.top = y + "px";
	pickerDiv.style.visibility = (pickerDiv.style.visibility == "visible" ? "hidden" : "visible");
	pickerDiv.style.display = (pickerDiv.style.display == "block" ? "none" : "block");
	pickerDiv.style.zIndex = 10000;
 
	refreshDatePicker(showDateField.name, targetDateField.name, dt[0], dt[1], dt[2]);
}

function refreshDatePicker(showDateFieldName, dateFieldName, year, month, day) {
	var thisDay = getTodayPersian();

	if (!year && !month && !day) {
		year = thisDay[0];
		month = thisDay[1];
		day = thisDay[2];
	}

	var weekday = (thisDay[3] - thisDay[2] + 1) % 7;
	if (!day)
		day = 1;
	if ((month >= 1) && (year > 0)) {
		thisDay = calcPersian(year, month, 1);
		weekday = thisDay[3];
		thisDay = new Array(year, month, day, weekday);
		thisDay[2] = 1;
	} else {
		day = thisDay[2];
		thisDay[2] = 1;
	}

	var crlf = "\r\n";
	var TABLE = "<table cols=7 class='dpTable' cellpadding='0'>" + crlf;
	var TABLE_Title = "<table cellpadding='0' cellspacing='0' style='width:100%;margin:0px'>" + crlf;
	var xTABLE = "</table>" + crlf;
	var TR = "<tr class='dpTR'>";
	var TR_title = "<tr class='dpTitleTR'>";
	var TR_days = "<tr class='dpDayTR'>";
	var TR_todaybutton = "<tr class='dpTodayButtonTR'>";
	var xTR = "</tr>" + crlf;
	var TD = "<td class='dpTD' onMouseOut='this.className=\"dpTD\";' onMouseOver=' this.className=\"dpTDHover\";' ";		// leave this tag open, because we'll be adding an onClick event
	var TD_titleBar = "<td colspan=7 class='dpTitleBarTD'>";
	var TD_buttons = "<td class='dpButtonTD'>";
	var TD_todaybutton = "<td colspan=7 class='dpTodayButtonTD'>";
	var TD_days = "<td class='dpDayTD'>";
	var TD_selected = "<td class='dpDayHighlightTD' onMouseOut='this.className=\"dpDayHighlightTD\";' onMouseOver='this.className=\"dpTDHover\";' ";		// leave this tag open, because we'll be adding an onClick event
	var xTD = "</td>" + crlf;
	var TD_title = "<td class='dpTitleTD' nowarp>";
	var DIV_selected = "<div class='dpDayHighlight'>";
	var xDIV = "</div>";

	var html = TABLE;

	html += TR_title;
	html += TD_titleBar;
	html += TABLE_Title;
	html += TD_buttons + getButtonCode(showDateFieldName, dateFieldName, thisDay, -1, "&lt;") + xTD;
	html += TD_title + monthArrayLong[thisDay[1] - 1] + " " + thisDay[0] + xTD;
	html += TD_buttons + getButtonCode(showDateFieldName, dateFieldName, thisDay, 1, "&gt;") + xTD;
	html += xTABLE;
	html += xTD; ;
	html += xTR;

	html += TR_days;
	var i;
	for (i = 0; i < dayArrayShort.length; i++)
		html += TD_days + dayArrayShort[i] + xTD;
	html += xTR;

	html += TR;

	if (weekday != 6)
		for (i = 0; i <= weekday; i++)
			html += TD + "&nbsp;" + xTD;

	var len = 31;
	if (thisDay[1] > 6)
		len = 30;
	if (thisDay[1] == 12 && !leap_persian(thisDay[0]))
		len = 29;

	for (var dayNum = thisDay[2]; dayNum <= len; dayNum++) {
		TD_onclick = " onclick=\"updateDateField('" + showDateFieldName + "','" + dateFieldName + "', '" + getDateString(thisDay) + "');\">";

		if (dayNum == day)
			html += TD_selected + TD_onclick + DIV_selected + dayNum + xDIV + xTD;
		else
			html += TD + TD_onclick + dayNum + xTD;

		if (weekday == 5)
			html += xTR + TR;
		weekday++;
		weekday = weekday % 7;

		thisDay[2]++;
	}

	if (weekday > 0) {
		for (i = 6; i > weekday; i--)
			html += TD + "&nbsp;" + xTD;
	}
	html += xTR;

	var today = new Date()
	var todayString = "Today is " + dayArrayMed[today.getDay()] + ", " + monthArrayMed[today.getMonth()] + " " + today.getDate();
	html += TR_todaybutton + TD_todaybutton;
	html += "<button class='smallButton' style='padding: 0px 2px 1px 2px;' onClick='refreshDatePicker(\"" + showDateFieldName + "\",\"" + dateFieldName + "\");'>&#1575;&#1605;&#1585;&#1608;&#1586;</button> ";
	html += "<button class='smallButton' style='padding: 0px 2px 1px 2px;' onClick='updateDateField(\"" + showDateFieldName + "\",\"" + dateFieldName + "\");'>&#1576;&#1587;&#1578;&#1606;</button>";
	html += xTD + xTR;

	html += xTABLE;

	document.getElementById(datePickerDivID).innerHTML = html;
	adjustiFrame();

	var maskHeight = "100%";
	var maskWidth = "100%";

	//Set heigth and width to mask to fill up the whole screen
	$("#lightMask").css({ 'width': maskWidth, 'height': maskHeight });
	$("#lightMask").show();
	
	$("#lightMask").click(function (e) {
		var pickerDiv = document.getElementById(datePickerDivID);
		pickerDiv.style.visibility = "hidden";
		pickerDiv.style.display = "none";
		$("#lightMask").hide();
		adjustiFrame();
		return false;
	});

	$("#" + datePickerDivID).focus();
}

function getButtonCode(showDateFieldName, dateFieldName, dateVal, adjust, label) {
	var oldMonth = dateVal[1];
	var oldYear = dateVal[0];
	var newMonth = (dateVal[1] + adjust) % 12;
	var newYear = dateVal[0] + parseInt((dateVal[1] + adjust) / 12);
	if (newMonth < 1) {
		newMonth += 12;
		newYear += -1;
	 }

	if(adjust < 0)
		return "<button class='smallButton' style='font-family:Arial;' onClick='refreshDatePicker(\"" + showDateFieldName + "\", \"" + dateFieldName + "\", " + newYear + ", " + newMonth + ");'>" + label + "</button>" +
						"<button class='smallButton' style='font-family:Arial;' onClick='refreshDatePicker(\"" + showDateFieldName + "\", \"" + dateFieldName + "\", " + (oldYear + adjust) + ", " + oldMonth + ");'>" + label + label + "</button>";
	else
		if (adjust > 0)
			return "<button class='smallButton' style='font-family:Arial;' onClick='refreshDatePicker(\"" + showDateFieldName + "\", \"" + dateFieldName + "\", " + (oldYear + adjust) + ", " + oldMonth + ");'>" + label + label + "</button>" +
							"<button class='smallButton' style='font-family:Arial;' onClick='refreshDatePicker(\"" + showDateFieldName + "\", \"" + dateFieldName + "\", " + newYear + ", " + newMonth + ");'>" + label + "</button>";

}

function getDateString(dateVal)
{
	var dayString = "00" + dateVal[2];
	var monthString = "00" + (dateVal[1]);
	dayString = dayString.substring(dayString.length - 2);
	monthString = monthString.substring(monthString.length - 2);
 
	switch (dateFormat) {
		case "dmy" :
			return dayString + dateSeparator + monthString + dateSeparator + dateVal[0];
		case "ymd" :
			return dateVal[0] + dateSeparator + monthString + dateSeparator + dayString;
		case "mdy" :
		default :
			return monthString + dateSeparator + dayString + dateSeparator + dateVal[0];
	}
}

function getFieldDate(dateString)
{
	var dateVal;
	var dArray;
	var d, m, y;
 
	try {
		dArray = splitDateString(dateString);
		if (dArray) {
			switch (dateFormat) {
				case "dmy" :
					d = parseInt(dArray[0], 10);
					m = parseInt(dArray[1], 10);
					y = parseInt(dArray[2], 10);
					break;
				case "ymd" :
					d = parseInt(dArray[2], 10);
					m = parseInt(dArray[1], 10);
					y = parseInt(dArray[0], 10);
					break;
				case "mdy" :
				default :
					d = parseInt(dArray[1], 10);
					m = parseInt(dArray[0], 10);
					y = parseInt(dArray[2], 10);
					break;
			}
			dateVal = new Array(y, m, d);
		} else if (dateString) {
			dateVal = getTodayPersian();
		} else {
			dateVal = getTodayPersian();
		}
	} catch(e) {
		dateVal = getTodayPersian();
	}
 
	return dateVal;
}

function splitDateString(dateString)
{
	var dArray;
	if (dateString.indexOf("/") >= 0)
		dArray = dateString.split("/");
	else if (dateString.indexOf(".") >= 0)
		dArray = dateString.split(".");
	else if (dateString.indexOf("-") >= 0)
		dArray = dateString.split("-");
	else if (dateString.indexOf("\\") >= 0)
		dArray = dateString.split("\\");
	else
		dArray = false;
 
	return dArray;
}

/**

function datePickerClosed(dateField)
{
	var dateObj = getFieldDate(dateField.value);
	var today = new Date();
	today = new Date(today.getFullYear(), today.getMonth(), today.getDate());
 
	if (dateField.name == "StartDate") {
		if (dateObj < today) {
			// if the date is before today, alert the user and display the datepicker again
			alert("Please enter a date that is today or later");
			dateField.value = "";
			document.getElementById(datePickerDivID).style.visibility = "visible";
			adjustiFrame();
		} else {
			// if the date is okay, set the EndDate field to 7 days after the StartDate
			dateObj.setTime(dateObj.getTime() + (7 * 24 * 60 * 60 * 1000));
			var endDateField = document.getElementsByName ("EndDate").item(0);
			endDateField.value = getDateString(dateObj);
		}
	}
}

*/
function updateDateField(showDateFieldName, dateFieldName, dateString) {
	var targetDateField = document.getElementsByName(dateFieldName).item(0);
	var showDateField = document.getElementsByName(showDateFieldName).item(0);
	if (dateString) {
		targetDateField.value = dateString;
		showDateField.value = dateString;
	}
 
	var pickerDiv = document.getElementById(datePickerDivID);
	pickerDiv.style.visibility = "hidden";
	pickerDiv.style.display = "none";
	$("#lightMask").hide();
 
	adjustiFrame();
	showDateField.focus();

	if ((dateString) && (typeof(datePickerClosed) == "function"))
		datePickerClosed(targetDateField);
}

function adjustiFrame(pickerDiv, iFrameDiv)
{
	var is_opera = (navigator.userAgent.toLowerCase().indexOf("opera") != -1);
	if (is_opera)
		return;

	try {
		if (!document.getElementById(iFrameDivID)) {
			var newNode = document.createElement("iFrame");
			newNode.setAttribute("id", iFrameDivID);
			newNode.setAttribute("src", "javascript:false;");
			newNode.setAttribute("scrolling", "no");
			newNode.setAttribute ("frameborder", "0");
			document.body.appendChild(newNode);
		}
		
		if (!pickerDiv)
			pickerDiv = document.getElementById(datePickerDivID);
		if (!iFrameDiv)
			iFrameDiv = document.getElementById(iFrameDivID);
		
		try {
			iFrameDiv.style.position = "absolute";
			iFrameDiv.style.width = pickerDiv.offsetWidth;
			iFrameDiv.style.height = pickerDiv.offsetHeight ;
			iFrameDiv.style.top = pickerDiv.style.top;
			iFrameDiv.style.left = pickerDiv.style.left;
			iFrameDiv.style.zIndex = pickerDiv.style.zIndex - 1;
			iFrameDiv.style.visibility = pickerDiv.style.visibility ;
			iFrameDiv.style.display = pickerDiv.style.display;
		} catch(e) {
		}
 
	} catch (ee) {
	}
}