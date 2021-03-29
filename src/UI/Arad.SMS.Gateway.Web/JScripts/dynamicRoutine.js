
function validateRequiredFields(validationSet) {
	var inputsArray = document.getElementsByTagName("input");
	var tablesArray = document.getElementsByTagName("table");
	var textareasArray = document.getElementsByTagName("textarea");
	var selectsArray = document.getElementsByTagName("select");
	var invalidControlsArray = new Array();;
	var controlArray = new Array();
	var arrayIndex = 0;
	var invalidCount = 0;
	for (var i = 0; i < inputsArray.length; i++) {
		if (hasControlRequiredValidationConditions(inputsArray[i], validationSet))
			controlArray[arrayIndex++] = inputsArray[i];
	}

	for (var i = 0; i < tablesArray.length; i++) {
		if (hasControlRequiredValidationConditions(tablesArray[i], validationSet))
			controlArray[arrayIndex++] = tablesArray[i];
	}

	for (var i = 0; i < textareasArray.length; i++) {
		if (hasControlRequiredValidationConditions(textareasArray[i], validationSet))
			controlArray[arrayIndex++] = textareasArray[i];
	}

	for (var i = 0; i < selectsArray.length; i++) {
		if (hasControlRequiredValidationConditions(selectsArray[i], validationSet))
			controlArray[arrayIndex++] = selectsArray[i];
	}

	for (var i = 0; i < controlArray.length; i++) {
		var forceInvalid = null;
		if (controlArray[i].tagName.toLowerCase() == "select" && controlArray[i].validateCount == "true")
			forceInvalid = controlArray[i].children.length == 0;
		if (controlArray[i].localName == 'table') {
			var validate = false;
			var controlTable = controlArray[i].getElementsByTagName("input");
			for (var j = 0; j < controlTable.length; j++) {
				if (controlTable[j].checked)
					validate = true;
			}
			if (!validate) {
				for (var j = 0; j < controlTable.length; j++) {
					if (forceInvalid == null && controlArray[i].getElementsByTagName("input")[j].style.display != "none")
						controlArray[i].getElementsByTagName("td")[j].style.backgroundColor = 'pink';
					invalidControlsArray[invalidCount++] = controlArray[i];
				}
			}
		}
		else if ((forceInvalid == null && (controlArray[i].value == "" || controlArray[i].value.trim().length == 0 || controlArray[i].value == undefined || controlArray[i].getAttribute("isValid") == "false")) || forceInvalid) {
			if (controlArray[i].style.display != "none") {
				controlArray[i].originalBGColor = controlArray[i].style.backgroundColor;
				controlArray[i].style.backgroundColor = 'pink';

				if (controlArray[i].tagName.toLowerCase() == "select") {
					jQuery("#" + controlArray[i].id).change(function (e) {
						var srcEl = e.srcElement ? e.srcElement : e.target;
						srcEl.style.backgroundColor = 'white';
					});
				}
				else {
					jQuery("#" + controlArray[i].id).keypress(function (e) {
						var srcEl = e.srcElement ? e.srcElement : e.target;
						srcEl.style.backgroundColor = 'white';
					});
				}

				try {
					controlArray[i].scrollIntoView();
				} catch (e) { }

				invalidControlsArray[invalidCount++] = controlArray[i];
			}
		}
	}



	return invalidCount == 0;
}

function messageBox(messageText, messageBoxTitle, messageBoxStyle, messageType, callbackFunction) {
	var result = "";
	var type;

	switch (messageType) {
		case "info":
			type = BootstrapDialog.TYPE_INFO;
			break;
		case 'primary':
			type = BootstrapDialog.TYPE_PRIMARY;
			break;
		case 'success':
			type = BootstrapDialog.TYPE_SUCCESS;
			break;
		case 'warning':
			type = BootstrapDialog.TYPE_WARNING;
			break;
		case 'danger':
			type = BootstrapDialog.TYPE_DANGER;
			break;
		default:
			type = BootstrapDialog.TYPE_DEFAULT;
			break;
	}

	if (messageBoxTitle == undefined || messageBoxTitle == "")
        messageBoxTitle = 'Alert';

	if (messageBoxStyle == undefined || messageBoxStyle.toLowerCase() == "alert") {
		BootstrapDialog.show({
			type: type,
			title: messageBoxTitle,
			message: messageText,
			label: 'Close',
			action: function (dialogRef) {
				dialogRef.close();
			}
		});
	}
	else if (messageBoxStyle.toLowerCase() == "confirm") {
		BootstrapDialog.show({
			type: type,
			title: messageBoxTitle,
			message: messageText,
			buttons: [{
				label: 'Confirm',
				cssClass: 'btn-primary',
				action: function (dialogItself) {
					if (callbackFunction) {
						dialogItself.close();
						callbackFunction(true);
					}
				}
			}, {
				label: 'Cancel',
				action: function (dialogItself) {
					dialogItself.close();
				}
			}]
		});
	}
}

function showNotification(messageText) {
	var result = "";

	var rootWindowElement = findRootParrentWindow(false);
	var states =
		{
			state0:
					{
						html: messageText,
						position: { container: '#notification', x: -10, y: -70, width: 200, arrow: 'bl' },
						buttons: {}
					}
		};

	rootWindowElement.$.prompt(states, { timeout: 5000, prefix: 'jqiAsr', zIndex: 99999 });
	$("#jqiAsrfade").hide();
	$("#jqiAsrbox")[0].style.position = "";
	$("#jqiAsrbox")[0].style.height = "";
}

$(document).ready(function () {
	$(".numberInput").keypress(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;
		var keyCode = event.which;

		if ((srcEl.getAttribute("allowNegativeSign") == "true" || srcEl.getAttribute("AllowNegativeSign") == "true") && keyCode == 45 && srcEl.value.length == 0)
			return true;

		if ((srcEl.getAttribute("allowDecimal") == "true" || srcEl.getAttribute("AllowDecimal") == "true") && keyCode == 46 && srcEl.value.indexOf('.') == -1)
			return true;

		if ((keyCode < 48 && keyCode != 8 && keyCode != 0 && keyCode != 13 && keyCode != 44) || keyCode > 57)
			return false;
	});

	$(".numberInput").keyup(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;

		if (!(srcEl.getAttribute("autoFormatDecimal") == "true" || srcEl.getAttribute("AutoFormatDecimal") == "true"))
			return;

		var separator = ",";
		var int = srcEl.value.replace(new RegExp(separator, "g"), "");
		var regexp = new RegExp("\\B(\\d{3})(" + separator + "|$)");
		do {
			int = int.replace(regexp, separator + "$1");
		}
		while (int.search(regexp) >= 0)
		srcEl.value = int;
	});

	$(".numberInput").blur(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;

		standardizeInputCharacters(srcEl);
		handleRequiredInputBlur(srcEl);

		if (srcEl.value == "") {
			srcEl.style.border = "1px solid silver";
			srcEl.setAttribute("isValid", true);
			return true;
		}

		srcEl.value = replaceString(srcEl.value, "'", "");
		if (!(srcEl.getAttribute("AllowSpaces") == "true" || srcEl.getAttribute("allowSpaces") == "true"))
			srcEl.value = replaceString(srcEl.value, " ", "");

		if (!(srcEl.getAttribute("AllowCommas") == "true" || srcEl.getAttribute("allowCommas") == "true"))
			srcEl.value = replaceString(srcEl.value, ",", "");

		if (!(srcEl.getAttribute("AllowDecimal") == "true" || srcEl.getAttribute("allowDecimal") == "true"))
			srcEl.value = srcEl.value.replace(".", "");

		var controlValue = srcEl.value;

		if (srcEl.getAttribute("isValid") == undefined)
			srcEl.setAttribute("isValid", true);

		srcEl.setAttribute("isValid", (srcEl.getAttribute("isValid") && !(controlValue != "0" && val(controlValue) == 0)));
		if (!eval(srcEl.getAttribute("isValid"))) {
			if (srcEl.style.border.toString().indexOf("red") != -1)
				return false;
			srcEl.style.border = "1px solid red";
			messageBox('Invalid number format');
			//srcEl.focus();
			return false;
		} else {
			if (eval(srcEl.getAttribute("isvalid")))
				srcEl.style.border = "1px solid silver";

			if (srcEl.getAttribute("autoFormatDecimal") == "true" || srcEl.getAttribute("AutoFormatDecimal") == "true")
				$("#" + srcEl.id).keyup();

			return true;
		}
	});

	$(".numberInput").focus(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;

		try {
			if (srcEl.getAttribute("avoidSelection") != "true" || srcEl.getAttribute("AvoidSelection") != "true") {
				srcEl.select();
			}
		} catch (e) { }
	});
	//----------------------------------------------------
	$(".hourInput").keypress(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;
		var keyCode = event.which;

		if ((keyCode < 48 && keyCode != 8 && keyCode != 0 && keyCode != 13 && keyCode != 44) || keyCode > 57)
			return false;
	});

	$(".hourInput").blur(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;
		var hour = val(srcEl.value);

		if (srcEl.value == "") {
			srcEl.style.border = "1px solid red";
			srcEl.style.backgroundColor = '#f2dede';
		}
		else {
			if (hour > 23) {
				srcEl.style.backgroundColor = '#f2dede';
				srcEl.style.border = "1px solid red";
				srcEl.value = "00";
			}
			else if (hour <= 9) {
				srcEl.value = "0" + hour;
				srcEl.style.border = "1px solid silver";
			}
			else {
				srcEl.value = hour;
				srcEl.style.border = "1px solid silver";
			}
		}
	});
	//-----------------------------------------------------------------------------
	$(".minuteInput").keypress(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;
		var keyCode = event.which;

		if ((keyCode < 48 && keyCode != 8 && keyCode != 0 && keyCode != 13 && keyCode != 44) || keyCode > 57)
			return false;
	});

	$(".minuteInput").blur(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;
		var minute = val(srcEl.value);

		if (srcEl.value == "") {
			srcEl.style.border = "1px solid red";
			srcEl.style.backgroundColor = '#f2dede';
		}
		else {
			if (minute > 59) {
				srcEl.style.border = "1px solid red";
				srcEl.style.backgroundColor = '#f2dede';
				srcEl.value = "00";
			}
			else if (minute <= 9) {
				srcEl.value = "0" + minute;
				srcEl.style.border = "1px solid silver";
			}
			else {
				srcEl.value = minute;
				srcEl.style.border = "1px solid silver";
			}
		}
	});
	//----------------------------------------------------
	$(".mobileNumberInput").keypress(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;
		var keyCode = event.which;

		if ((keyCode < 48 && keyCode != 8 && keyCode != 0 && keyCode != 13 && keyCode != 44) || keyCode > 57)
			return false;
	});

	$(".mobileNumberInput").blur(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;

		standardizeInputCharacters(srcEl);
		handleRequiredInputBlur(srcEl);

		if (srcEl.value == "") {
			srcEl.style.border = "1px solid silver";
			srcEl.setAttribute("isValid", true);
			return true;
		}

		srcEl.value = replaceString(srcEl.value, "'", "");
		var controlValue = srcEl.value;
		var isValid = false;

		if (srcEl.getAttribute("isValid") == undefined)
			srcEl.setAttribute("isValid", true);

		if (controlValue.length == 11 && controlValue.substring(0, 2) == "09")
			isValid = true;
		else if (controlValue.length == 10 && controlValue.substring(0, 1) == "9") {
			srcEl.value = "0" + srcEl.value;
			isValid = true;
		}
		else if (controlValue.length == 12 && controlValue.substring(0, 2) == "98") {
			isValid = true;
		}
		else
			isValid = false;


		srcEl.setAttribute("isValid", (srcEl.getAttribute("isValid") && isValid && val(controlValue) != 0));

		if (!eval(srcEl.getAttribute("isValid"))) {
			if (srcEl.style.border.toString().indexOf("red") != -1)
				return false;

			srcEl.style.border = "1px solid red";
			srcEl.style.backgroundColor = '#f2dede';

			return false;
		}
		else {
			if (eval(srcEl.getAttribute("isvalid")))
				srcEl.style.border = "1px solid silver";

			return true;
		}
	});

	//----------------------------------------------------

	$(".mobileNumberInputList").keypress(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;
		var keyCode = event.which;

		if ((keyCode < 48 && keyCode != 8 && keyCode != 0 && keyCode != 13 && keyCode != 44) || keyCode > 57)
			return false;
	});

	$(".mobileNumberInputList").blur(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;

		standardizeInputCharacters(srcEl);
		handleRequiredInputBlur(srcEl);

		if (srcEl.value == "") {
			srcEl.style.border = "1px solid silver";
			srcEl.setAttribute("isValid", true);

			$("#txtCorrectNumber")[0].value = "";
			$("#txtFailNumber")[0].value = "";
			return true;
		}

		srcEl.value = replaceString(srcEl.value, "'", "");
		var controlValue;
		var allMobileNumbers = "";
		var invalidMobileNumbers = "";
		var mobileNumbers = srcEl.value.replace("\r\n", "\n").split('\n');
		var isValid = false;

		if (srcEl.getAttribute("isValid") == undefined)
			srcEl.setAttribute("isValid", true);


		for (var i = 0; i < mobileNumbers.length; i++) {
			controlValue = replaceString(mobileNumbers[i], '\r', '');
			controlValue = replaceString(controlValue, ' ', '');
			if (controlValue == '') continue;

			if (controlValue.length == 11 && controlValue.substring(0, 2) == "09" && val(controlValue) != 0) {
				if (allMobileNumbers.indexOf(controlValue) == -1) {
					allMobileNumbers += (allMobileNumbers != "" ? "\r\n" : "") + controlValue;
				}
				isValid = true;

			}
			else if (controlValue.length == 10 && controlValue.substring(0, 1) == "9" && val(controlValue) != 0) {
				if (allMobileNumbers.indexOf(controlValue) == -1) {
					allMobileNumbers += (allMobileNumbers != "" ? "\r\n" : "") + "0" + controlValue;
				}
				isValid = true;
			}
			else if (controlValue.length == 12 && controlValue.substring(0, 2) == "98" && val(controlValue) != 0) {
				if (allMobileNumbers.indexOf(controlValue) == -1) {
					allMobileNumbers += (allMobileNumbers != "" ? "\r\n" : "") + "0" + controlValue.substring(2);
				}
				isValid = true;
			}
			else {
				if ($("#" + srcEl.id + "Invalid")[0].value.indexOf(controlValue) == -1) {
					invalidMobileNumbers += (invalidMobileNumbers != "" ? "\r\n" : "") + controlValue;
				}
			}
		}

		var countCorrectMobileNumbers = 0;
		if (allMobileNumbers != '')
			countCorrectMobileNumbers = allMobileNumbers.split('\n').length;
		$("#txtCorrectNumber")[0].value = 'Correct Number' + " : " + countCorrectMobileNumbers;

		if (isValid)
			srcEl.value = allMobileNumbers;

		if ($("#" + srcEl.id + "Invalid")[0]) {
			$("#" + srcEl.id + "Invalid")[0].value = (($("#" + srcEl.id + "Invalid")[0].value) != "" && invalidMobileNumbers != "" ? "\r\n" : "") + invalidMobileNumbers;
		}

		srcEl.setAttribute("isValid", (srcEl.getAttribute("isValid") && isValid));

		if (!eval(srcEl.getAttribute("isValid"))) {
			if (srcEl.style.border.toString().indexOf("red") != -1)
				return false;

			srcEl.style.border = "1px solid red";
			srcEl.style.backgroundColor = '#f2dede';

			return false;
		}
		else {
			if (eval(srcEl.getAttribute("isvalid")))
				srcEl.style.border = "1px solid silver";

			return true;
		}
	});

	//----------------------------------------------------
	$(".emailInput").blur(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;

		standardizeInputCharacters(srcEl);
		handleRequiredInputBlur(srcEl);

		var input = srcEl.value;
		var invalidEmail = false;

		if (input == "") {
			this.style.border = "1px solid silver";
			srcEl.setAttribute("isValid", true);
			return true;
		}

		var pattern = new RegExp(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/i);
		invalidEmail = pattern.test(input);

		if (!invalidEmail) {
			this.style.border = "1px solid red";
			this.style.backgroundColor = '#f2dede';
			srcEl.setAttribute("isValid", false);
			return false;
		} else {
			this.style.border = "1px solid silver";
			srcEl.setAttribute("isValid", true);
		}
	});
	//----------------------------------------------------
	$(".postalCodeInput").keypress(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;
		var keyCode = event.which;

		if ((keyCode < 48 && keyCode != 8 && keyCode != 0 && keyCode != 13 && keyCode != 44) || keyCode > 57)
			return false;
	});

	$(".postalCodeInput").blur(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;
		var postalCode = srcEl.value;
		if (postalCode.length != 10) {
			srcEl.style.border = "1px solid red";
			srcEl.style.backgroundColor = '#f2dede';
			srcEl.value = "00";
		}
		else {
			srcEl.value = postalCode;
			srcEl.style.border = "1px solid silver";
		}
	});

	//----------------------------------------------------
	$(".natinalCodeInput").keypress(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;
		var keyCode = event.which;

		if ((keyCode < 48 && keyCode != 8 && keyCode != 0 && keyCode != 13 && keyCode != 44) || keyCode > 57)
			return false;
	});

	$(".natinalCodeInput").blur(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;
		var nationalCode = srcEl.value;
		if (nationalCode.length != 10) {
			srcEl.style.border = "1px solid red";
			srcEl.style.backgroundColor = '#f2dede';
			srcEl.value = "00";
		}
		else {
			srcEl.value = nationalCode;
			srcEl.style.border = "1px solid silver";
		}
	});

	//----------------------------------------------------
	$(".phoneNumberInput").keypress(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;
		var keyCode = event.which;

		if ((keyCode < 48 && keyCode != 8 && keyCode != 0 && keyCode != 13 && keyCode != 44) || keyCode > 57)
			return false;
	});

	$(".phoneNumberInput").blur(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;
		var phoneNumberInput = srcEl.value;
		if (phoneNumberInput.length > 10) {
			srcEl.style.border = "1px solid red";
			srcEl.style.backgroundColor = '#f2dede';
			srcEl.value = "00";
		}
		else {
			srcEl.value = nationalCode;
			srcEl.style.border = "1px solid silver";
		}
	});
	//----------------------------------------------------
	$(".emailInputList").blur(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;

		standardizeInputCharacters(srcEl);
		handleRequiredInputBlur(srcEl);

		if (srcEl.value == "") {
			srcEl.style.border = "1px solid silver";
			srcEl.setAttribute("isValid", true);

			$("#txtCorrectEmail")[0].value = "";
			$("#txtFailEmail")[0].value = "";
			return true;
		}

		srcEl.value = replaceString(srcEl.value, "'", "");
		var controlValue;
		var allEmails = "";
		var invalidEmails = "";
		var emails = srcEl.value.split('\n');
		var isValid = false;
		var pattern = new RegExp(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/i);

		if (srcEl.getAttribute("isValid") == undefined)
			srcEl.setAttribute("isValid", true);

		for (var i = 0; i < emails.length; i++) {
			controlValue = replaceString(emails[i], '\r', '');
			if (controlValue == '') continue;

			if (pattern.test(controlValue)) {
				if (allEmails.indexOf(controlValue) == -1) {
					allEmails += (allEmails != "" ? "\r\n" : "") + controlValue;
				}
				isValid = true;
			}
			else {
				if ($("#" + srcEl.id + "Invalid")[0].value.indexOf(controlValue) == -1) {
					invalidEmails += (invalidEmails != "" ? "\r\n" : "") + controlValue;
				}
			}
		}

		var countCorrectEmails = allEmails != "" ? allEmails.split('\r\n').length : 0;
		$("#txtCorrectEmail")[0].value = 'Correct emails:' + " : " + countCorrectEmails;

		if (isValid)
			srcEl.value = allEmails;

		if ($("#" + srcEl.id + "Invalid")[0]) {
			$("#" + srcEl.id + "Invalid")[0].value = (($("#" + srcEl.id + "Invalid")[0].value) != "" && invalidEmails != "" ? "\r\n" : "") + invalidEmails;
		}

		srcEl.setAttribute("isValid", (srcEl.getAttribute("isValid") && isValid));

		if (!eval(srcEl.getAttribute("isValid"))) {
			if (srcEl.style.border.toString().indexOf("red") != -1)
				return false;

			srcEl.style.border = "1px solid red";
			srcEl.style.backgroundColor = '#f2dede';

			return false;
		}
		else {
			if (eval(srcEl.getAttribute("isvalid")))
				srcEl.style.border = "1px solid silver";

			return true;
		}
	});
});

