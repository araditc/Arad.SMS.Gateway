<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePickerWithTime.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Controls.DatePickerWithTime" %>
<asp:Panel ID="pnlDatePicker" runat="server">
	<asp:TextBox ID="txtIsActive" style="display:none" runat="server"></asp:TextBox>
	<div style="width:400px">
		<div id="divDatePicker" style="float:right;width:170px;">
			<asp:TextBox ID="txtDate" style="display:none" runat="server"></asp:TextBox>
			<asp:TextBox CssClass="input" style="direction:rtl" ID="txtShowDate" runat="server" ReadOnly="true"></asp:TextBox>
			<img src="../../Images/calendar.jpg" alt="" style="margin:0px" onclick="if($('#<%=txtIsActive.ClientID %>')[0].value!='0') displayDatePicker('<%=txtShowDate.ClientID.Replace("_","$")%>','<%=txtDate.ClientID.Replace("_","$")%>', this);" />
			<img src="../../Images/clear.gif" alt="" style="margin:0px" onclick="if($('#<%=txtIsActive.ClientID %>')[0].value!='0') {$('#<%=txtShowDate.ClientID%>')[0].value = '';$('#<%=txtDate.ClientID%>')[0].value = '';}" />
		</div>
		<div style="width:100px;float:right;">
			<div style="float:left;">
				<div class="controlDivTime">
					<%--<asp:TextBox ID="txtHour" runat="server" CssClass="hourInput" Width="25px" MaxLength="2"></asp:TextBox>--%>
					<asp:DropDownList ID="drpHour" runat="server" CssClass="input">
						<asp:ListItem Value=""></asp:ListItem>
						<asp:ListItem Value="00">00</asp:ListItem>
						<asp:ListItem Value="01">01</asp:ListItem>
						<asp:ListItem Value="02">02</asp:ListItem>
						<asp:ListItem Value="03">03</asp:ListItem>
						<asp:ListItem Value="04">04</asp:ListItem>
						<asp:ListItem Value="05">05</asp:ListItem>
						<asp:ListItem Value="06">06</asp:ListItem>
						<asp:ListItem Value="07">07</asp:ListItem>
						<asp:ListItem Value="08">08</asp:ListItem>
						<asp:ListItem Value="09">09</asp:ListItem>
						<asp:ListItem Value="10">10</asp:ListItem>
						<asp:ListItem Value="11">11</asp:ListItem>
						<asp:ListItem Value="12">12</asp:ListItem>
						<asp:ListItem Value="13">13</asp:ListItem>
						<asp:ListItem Value="14">14</asp:ListItem>
						<asp:ListItem Value="15">15</asp:ListItem>
						<asp:ListItem Value="16">16</asp:ListItem>
						<asp:ListItem Value="17">17</asp:ListItem>
						<asp:ListItem Value="18">18</asp:ListItem>
						<asp:ListItem Value="19">19</asp:ListItem>
						<asp:ListItem Value="20">20</asp:ListItem>
						<asp:ListItem Value="21">21</asp:ListItem>
						<asp:ListItem Value="22">22</asp:ListItem>
						<asp:ListItem Value="23">23</asp:ListItem>
					</asp:DropDownList>
				</div>
				<div class="titleDivTime">
					hh
				</div>
			</div>
			<span>:</span>
			<div style="float:right;">
				<div class="controlDivTime">
					<%--<asp:TextBox ID="txtMinute" runat="server" CssClass="minuteInput" Width="25px" MaxLength="2"></asp:TextBox>--%>
					<asp:DropDownList ID="drpMinute" runat="server" CssClass="input">
						<asp:ListItem Value=""></asp:ListItem>
						<asp:ListItem Value="00">00</asp:ListItem>
						<asp:ListItem Value="01">01</asp:ListItem>
						<asp:ListItem Value="02">02</asp:ListItem>
						<asp:ListItem Value="03">03</asp:ListItem>
						<asp:ListItem Value="04">04</asp:ListItem>
						<asp:ListItem Value="05">05</asp:ListItem>
						<asp:ListItem Value="06">06</asp:ListItem>
						<asp:ListItem Value="07">07</asp:ListItem>
						<asp:ListItem Value="08">08</asp:ListItem>
						<asp:ListItem Value="09">09</asp:ListItem>
						<asp:ListItem Value="10">10</asp:ListItem>
						<asp:ListItem Value="11">11</asp:ListItem>
						<asp:ListItem Value="12">12</asp:ListItem>
						<asp:ListItem Value="13">13</asp:ListItem>
						<asp:ListItem Value="14">14</asp:ListItem>
						<asp:ListItem Value="15">15</asp:ListItem>
						<asp:ListItem Value="16">16</asp:ListItem>
						<asp:ListItem Value="17">17</asp:ListItem>
						<asp:ListItem Value="18">18</asp:ListItem>
						<asp:ListItem Value="19">19</asp:ListItem>
						<asp:ListItem Value="20">20</asp:ListItem>
						<asp:ListItem Value="21">21</asp:ListItem>
						<asp:ListItem Value="22">22</asp:ListItem>
						<asp:ListItem Value="23">23</asp:ListItem>
						<asp:ListItem Value="24">24</asp:ListItem>
						<asp:ListItem Value="25">25</asp:ListItem>
						<asp:ListItem Value="26">26</asp:ListItem>
						<asp:ListItem Value="27">27</asp:ListItem>
						<asp:ListItem Value="28">28</asp:ListItem>
						<asp:ListItem Value="29">29</asp:ListItem>
						<asp:ListItem Value="30">30</asp:ListItem>
						<asp:ListItem Value="31">31</asp:ListItem>
						<asp:ListItem Value="32">32</asp:ListItem>
						<asp:ListItem Value="33">33</asp:ListItem>
						<asp:ListItem Value="34">34</asp:ListItem>
						<asp:ListItem Value="35">35</asp:ListItem>
						<asp:ListItem Value="36">36</asp:ListItem>
						<asp:ListItem Value="37">37</asp:ListItem>
						<asp:ListItem Value="38">38</asp:ListItem>
						<asp:ListItem Value="39">39</asp:ListItem>
						<asp:ListItem Value="40">40</asp:ListItem>
						<asp:ListItem Value="41">41</asp:ListItem>
						<asp:ListItem Value="42">42</asp:ListItem>
						<asp:ListItem Value="43">43</asp:ListItem>
						<asp:ListItem Value="44">44</asp:ListItem>
						<asp:ListItem Value="45">45</asp:ListItem>
						<asp:ListItem Value="46">46</asp:ListItem>
						<asp:ListItem Value="47">47</asp:ListItem>
						<asp:ListItem Value="48">48</asp:ListItem>
						<asp:ListItem Value="49">49</asp:ListItem>
						<asp:ListItem Value="50">50</asp:ListItem>
						<asp:ListItem Value="51">51</asp:ListItem>
						<asp:ListItem Value="52">52</asp:ListItem>
						<asp:ListItem Value="53">53</asp:ListItem>
						<asp:ListItem Value="54">54</asp:ListItem>
						<asp:ListItem Value="55">55</asp:ListItem>
						<asp:ListItem Value="56">56</asp:ListItem>
						<asp:ListItem Value="57">57</asp:ListItem>
						<asp:ListItem Value="58">58</asp:ListItem>
						<asp:ListItem Value="59">59</asp:ListItem>
					</asp:DropDownList>
				</div>
				<div class="titleDivTime">
					mm
				</div>
			</div>
		</div>
	</div>
</asp:Panel>
<script type="text/javascript">
	$("#<%=txtShowDate.ClientID%>")[0].value = $("#<%=txtDate.ClientID%>")[0].value;

	function getChristianDate(datePickerID) {
		var dtp = "#" + datePickerID + "_txtDate"
		solarDate = $(dtp)[0].value;
		solarYear = solarDate.split('/')[0];
		solarMounth = solarDate.split('/')[1];
		solarDay = solarDate.split('/')[2];
		var objDate = new Date();
		objDate.setFullYear(solarYear);
		objDate.setMonth(new Number(solarMounth) - 1);
		objDate.setDate(solarDay);
		var jdArray = new Array(
									objDate.getFullYear(),
									objDate.getMonth() + 1,
									objDate.getDate()
									);
		var christianDate = ArrayToGregorianDate(jdArray);
		christianDate = christianDate.replace(/-/g, '/');
		return new Date(christianDate + " " + $("#" + datePickerID + "_drpHour")[0].value + ":" + $("#" + datePickerID + "_drpMinute")[0].value);
	}

	function getStringChristianDate(datePickerID) {
		var dtp = "#" + datePickerID + "_txtDate"
		solarDate = $(dtp)[0].value;
		solarYear = solarDate.split('/')[0];
		solarMounth = solarDate.split('/')[1];
		solarDay = solarDate.split('/')[2];
		var objDate = new Date();
		objDate.setFullYear(solarYear);
		objDate.setMonth(new Number(solarMounth) - 1);
		objDate.setDate(solarDay);
		var jdArray = new Array(
									objDate.getFullYear(),
									objDate.getMonth() + 1,
									objDate.getDate()
									);
		var christianDate = ArrayToGregorianDate(jdArray);
		christianDate = christianDate.replace(/-/g, '/');
		return christianDate + " " + $("#" + datePickerID + "_drpHour")[0].value + ":" + $("#" + datePickerID + "_drpMinute")[0].value;
	}

	function getDatePickerValue(datePickerID) {
		var dtp = "#" + datePickerID + "_txtDate";
		var hour = "#" + datePickerID + "_drpHour";
		var minute = "#" + datePickerID + "_drpMinute";
		if ($(dtp)[0].value != "") {
			solarDate = $(dtp)[0].value;
			if ($(hour)[0].value != '' && $(minute)[0].value != '')
				solarDate += ' ' + $(hour)[0].value + '-' + $(minute)[0].value;
			return solarDate;
		}
		else
			return "";
	}

	function setDatePickerValue(datePickerID, value) {
		var dtp = "#" + datePickerID + "_txtDate";
		var dtpShowDate = "#" + datePickerID + "_txtShowDate";
		var hour = "#" + datePickerID + "_drpHour";
		var minute = "#" + datePickerID + "_drpMinute";

		$(dtp)[0].value = value.substring(0, 10);
		$(dtpShowDate)[0].value = value.substring(0, 10);
		$(hour)[0].value = value.substring(11, 13);
		$(minute)[0].value = value.substring(14, 16);
	}
</script>