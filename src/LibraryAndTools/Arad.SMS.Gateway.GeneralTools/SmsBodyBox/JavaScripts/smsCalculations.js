function calculateSmsCount(txtSmsBodyName, smsInfoCellName, characterTitle, smsTitle) {
	var smsInfoCell = $("#" + smsInfoCellName)[0];
	var txtSmsBody = $("#" + txtSmsBodyName)[0];
	var standardSmsLen = 160;
	var standardUdhLen = 7;

	if (hasUniCodeCharacter(txtSmsBody.value)) {
		standardSmsLen = 70;
		standardUdhLen = 4;
	}

	var smsLen = replaceString(txtSmsBody.value,"\r\n","\n").length;
	var smsCount = 0

	if (smsLen > standardSmsLen)
		smsCount = Math.ceil(smsLen / (standardSmsLen - standardUdhLen));
	else
		smsCount = Math.ceil(smsLen / standardSmsLen);

	smsInfoCell.innerHTML = smsLen + "&nbsp;<B>" + characterTitle + "</B>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + smsCount + "&nbsp;<B>" + smsTitle + "</B>";
}

function hasUniCodeCharacter(value) {
	return /[^\u0000-\u00ff]/.test(value);
}

var farsiNumbers = new Array("۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹");
var arabicNumbers = new Array("٠", "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩");

function getArabicNumber(message) { var str = message + ""; for (var num in arabicNumbers) { var reg = new RegExp(num, "g"); str = str.replace(reg, arabicNumbers[num]); } return str; };
function getEnglishNumber(message) { str = message + ""; for (var x = 0; x < farsiNumbers.length; x++) { var reg = new RegExp(farsiNumbers[x], "g"); str = str.replace(reg, x); } for (var x = 0; x < arabicNumbers.length; x++) { var reg = new RegExp(arabicNumbers[x], "g"); str = str.replace(reg, x); } return str; };
function cahngeNumber(checkBox, messageBox) {var checked = checkBox.checked;  if (checked) { messageBox.value = getArabicNumber(messageBox.value); } else { messageBox.value = getEnglishNumber(messageBox.value); } };
