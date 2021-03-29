<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveSmsSenderAgent.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsSenderAgents.SaveSmsSenderAgent" %>

<asp:HiddenField ID="hdnPass" runat="server" />

<div class="row">
	<div class="col-md-5">
		<div class="panel panel-primary">
			<div class="panel-heading"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GeneralInformation") %></div>
			<div class="panel-body">
				<div class="form-horizontal" role="form">
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Name") %></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtName" class="form-control input-sm" isrequired="true" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DefaultNumber") %></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtDefaultNumber" class="form-control input-sm numberInput" allowCamas isrequired="true" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SenderAgentType")%></label>
						<div class="col-sm-7">
							<asp:DropDownList ID="drpType" class="form-control input-sm" isrequired="true" runat="server"></asp:DropDownList>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsSenderAgent")%></label>
						<div class="col-sm-7">
							<asp:DropDownList ID="drpSmsSenderAgents" class="form-control input-sm" isrequired="true" runat="server"></asp:DropDownList>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("StartSendTime") %></label>
						<div class="col-sm-7">
							<asp:DropDownList ID="drpStartMinute" class="form-control input-sm col-sm-4" runat="server">
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
							<asp:DropDownList ID="drpStartHour" class="form-control input-sm col-sm-4" runat="server">
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
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("EndSendTime") %></label>
						<div class="col-sm-7">
							<asp:DropDownList ID="drpEndMinute" class="form-control input-sm col-sm-4" runat="server">
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
							<asp:DropDownList ID="drpEndHour" class="form-control input-sm col-sm-4" runat="server">
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
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendSmsAlert") %></label>
						<div class="col-sm-7">
							<label>
								<asp:CheckBox ID="chkSmsAlert" runat="server" /></label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendActive") %></label>
						<div class="col-sm-7">
							<label>
								<asp:CheckBox ID="chkIsSendActive" runat="server" /></label>
						</div>
					</div>
					<div class="form-group" style="display: none">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RecieveActive")%></label>
						<div class="col-sm-7">
							<label>
								<asp:CheckBox ID="chkIsRecieveActive" runat="server" /></label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendBulkActive")%></label>
						<div class="col-sm-7">
							<label>
								<asp:CheckBox ID="chkIsSendBulkActive" runat="server" /></label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendBulkIsAutomatic")%></label>
						<div class="col-sm-7">
							<label>
								<asp:CheckBox ID="chkSendBulkIsAutomatic" runat="server" /></label>
						</div>
					</div>
					<div class="form-group" style="display: none">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CheckMessageID")%></label>
						<div class="col-sm-7">
							<label>
								<asp:CheckBox ID="chkCheckMessageID" runat="server" /></label>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
	<div class="col-md-6">
		<div class="panel panel-primary">
			<div class="panel-heading"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AccessInformation") %></div>
			<div class="panel-body">
				<div class="form-horizontal" role="form">
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UserName") %></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtUsername" class="form-control input-sm" isrequired="true" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Password") %></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtPassword" class="form-control input-sm" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendLink")%></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtSendLink" class="form-control input-sm" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ReceiveLink")%></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtReceiveLink" class="form-control input-sm" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeliveryLink")%></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtDeliveryLink" class="form-control input-sm" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ExtraParameter") %></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtDomain" class="form-control input-sm" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("QueueLength") %></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtQueueLength" class="form-control input-sm numberInput" isrequired="true" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("HasRoute") %></label>
						<div class="col-sm-7">
							<label>
								<asp:CheckBox ID="chkRouteActive" runat="server" /></label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("QueuelengthsBaseOnThePage") %></label>
						<div class="col-sm-7">
							<label>
								<asp:CheckBox ID="chbIsSmpp" runat="server" /></label>
						</div>
					</div>
				</div>
			</div>
		</div>
		<hr />
		<div class="buttonControlDiv">
			<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Register" />
			<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
		</div>
	</div>
</div>
