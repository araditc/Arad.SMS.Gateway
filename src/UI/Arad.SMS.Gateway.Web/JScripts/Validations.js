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
			messageBox("فرمت عدد وارد شده اشتباه می باشد.");
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
		if (srcEl.value.length >= 2)
			return false;
	});

	$(".hourInput").blur(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;
		var hour = val(srcEl.value);

		if (srcEl.value == "") {
			srcEl.style.border = "1px solid red";
			messageBox('<%=GeneralLibrary.Language.GetString("CheckEmptyHourInput") %>');
		}
		else {
			if (hour > 23) {

				messageBox('<%=GeneralLibrary.Language.GetString("FailHourInput") %>');
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
		if (srcEl.value.length >= 2)
			return false;
	});

	$(".minuteInput").blur(function (event) {
		if (!event) return;
		var srcEl = event.srcElement ? event.srcElement : event.target;
		var minute = val(srcEl.value);

		if (srcEl.value == "") {
			srcEl.style.border = "1px solid red";
			messageBox('<%=GeneralLibrary.Language.GetString("CheckEmptyMinuteInput") %>');
		}
		else {
			if (minute > 59) {
				messageBox('<%=GeneralLibrary.Language.GetString("FailMinuteInput") %>');
				srcEl.style.border = "1px solid red";
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
		else
			isValid = false;


		srcEl.setAttribute("isValid", (srcEl.getAttribute("isValid") && isValid && val(controlValue) != 0));

		if (!eval(srcEl.getAttribute("isValid"))) {
			if (srcEl.style.border.toString().indexOf("red") != -1)
				return false;

			srcEl.style.border = "1px solid red";
			messageBox("فرمت شماره وارد شده اشتباه می باشد.");
			//srcEl.focus();

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
		var mobileNumbers = srcEl.value.split('\n');
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
			else {
				if ($("#" + srcEl.id + "Invalid")[0].value.indexOf(controlValue) == -1) {
					invalidMobileNumbers += (invalidMobileNumbers != "" ? "\r\n" : "") + controlValue;
					//	isValid = false;
					//	break;
				}
			}
		}

		var countCorrectMobileNumbers = Math.floor(allMobileNumbers.length / 11);
		$("#txtCorrectNumber")[0].value = "تعداد شماره های صحیح : " + countCorrectMobileNumbers + "عدد";

		if (isValid)
			srcEl.value = allMobileNumbers;

		if ($("#" + srcEl.id + "Invalid")[0]) {
			$("#" + srcEl.id + "Invalid")[0].value = (($("#" + srcEl.id + "Invalid")[0].value) != "" && invalidMobileNumbers != "" ? "\r\n" : "") + invalidMobileNumbers;
		}

		//				var countFailedMobileNumbers = $("#" + srcEl.id + "Invalid")[0].value.split('\r\n');
		//				$("#txtFailNumber")[0].value = "تعداد شماره های نادرست : " + countFailedMobileNumbers.length + "عدد";

		srcEl.setAttribute("isValid", (srcEl.getAttribute("isValid") && isValid));

		if (!eval(srcEl.getAttribute("isValid"))) {
			if (srcEl.style.border.toString().indexOf("red") != -1)
				return false;

			srcEl.style.border = "1px solid red";
			messageBox("فرمت شماره های وارد شده اشتباه می باشد.");
			//srcEl.focus();

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
		var atIndex = input.indexOf("@");
		var dotIndex = input.lastIndexOf(".");
		var invalidEmail = false;

		if (input == "") {
			this.style.border = "1px solid silver";
			srcEl.setAttribute("isValid", true);
			return true;
		}

		if (atIndex == -1 || dotIndex == -1 || dotIndex < atIndex)
			invalidEmail = true;

		if (srcEl.getAttribute("isValid") == undefined)
			srcEl.setAttribute("isValid", true);

		srcEl.setAttribute("isValid", (srcEl.getAttribute("isValid") && !invalidEmail));

		if (invalidEmail) {
			this.style.border = "1px solid red";
			messageBox("فرمت ایمیل وارد شده صحیح نمی باشد.");
			//this.focus();
			return false;
		} else {
			this.style.border = "1px solid silver";
		}
	});
});