function checkBoxControlChecked(checkBox) {
	var childrenCheckBoxes = $(checkBox).parent().parent().children('ul').find('input');
	if ($(checkBox).attr('checked') == 'checked')
		for (counter = 0; counter < childrenCheckBoxes.length; counter++) {
			$(childrenCheckBoxes[counter]).attr('checked', 'checked');
		}
	else
		for (counter = 0; counter < childrenCheckBoxes.length; counter++) {
			$(childrenCheckBoxes[counter]).removeAttr('checked');
		}
}