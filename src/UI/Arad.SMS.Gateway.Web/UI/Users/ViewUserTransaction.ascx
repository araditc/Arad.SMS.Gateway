<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewUserTransaction.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Users.ViewUserTransaction" %>

<asp:Panel ID="pnlChangeCredit" runat="server">
	<hr style="margin: 5px" />
	<div class="row">
		<div class="col-xs-12 col-md-12">
			<div class="form-inline" role="form">
				<div class="form-group col-md-4">
					<div class="input-group">
						<span class="input-group-addon">
							<% if(Session["Language"].ToString() == "fa") { %>
								<asp:DropDownList ID="drpType" runat="server">
								<asp:ListItem Value="" Selected="True"></asp:ListItem>
								<asp:ListItem Value="1">افزایش</asp:ListItem>
								<asp:ListItem Value="2">کاهش</asp:ListItem>
							    </asp:DropDownList>
							<% }  else
							   {%>
								<asp:DropDownList ID="DropDownList1" runat="server">
								<asp:ListItem Value="" Selected="True"></asp:ListItem>
								<asp:ListItem Value="1">Increase</asp:ListItem>
								<asp:ListItem Value="2">Decrease</asp:ListItem>
							    </asp:DropDownList>
							<%  } %>
						</span>
						<asp:TextBox ID="txtAmount" class="form-control numberInput" autoFormatDecimal="true" allowdecimal="true" runat="server"></asp:TextBox>
						<span class="input-group-addon"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsCount") %></span>
					</div>
				</div>
				<div class="form-group">
					<label><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Description")%></label>
					<asp:TextBox ID="txtDescription" class="form-control input-sm" runat="server"></asp:TextBox>
				</div>
				<asp:Button ID="btnSave" runat="server" Text="Register" CssClass="btn btn-success" Style="border: 0" OnClick="btnSave_Click" />
				<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" Style="border: 0" OnClick="btnCancel_Click" />
			</div>
			<div id="result" class="div-save-result"></div>
		</div>
	</div>
	<hr style="margin: 5px" />
</asp:Panel>

<GeneralTools:DataGrid runat="server" ShowAdvancedSearch="false" ID="gridUserTransaction" DefaultSortField="CreateDate" ListCaption="TransactionList" ListHeight="420"
	SearchDivID="advanceSearchContainer" ShowToolbar="false" ShowRowNumber="true" GridComplete="setRowColor" ShowSearchToolbar="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="TypeCreditChange" FieldName="TypeCreditChange" SearchType="Select" Search="true" SearchOptions="{postData:{id:1},clearSearch:false,buildSelect:'createSelect'}" Sortable="false" CellWidth="85" Align="Center" Frozen="true" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Search="true" SearchType="Date" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" Sortable="false" CellWidth="100" Align="Center" Frozen="true" FormattingMethod="DateTimeShortDateTime" />
		<GeneralTools:DataGridColumnInfo Caption="PreviousCredit" FieldName="CurrentCredit" Sortable="false" CellWidth="110" Align="Center" FormattingMethod="NumberDecimal" />
		<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="Type" Hidden="true" Sortable="false" CellWidth="40" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="TypeTransaction" SearchType="Select" Search="true" SearchOptions="{postData:{id:2},clearSearch:false,buildSelect:'createSelect'}" Sortable="false" CellWidth="100" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="SmsCount" FieldName="Amount" Search="true" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" SearchFilterCssClass="numberInput" Sortable="false" CellWidth="110" Align="Center" FormattingMethod="NumberDecimal" />
		<GeneralTools:DataGridColumnInfo Caption="CurrentCredit" FieldName="NextCredit" Search="true" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" SearchFilterCssClass="numberInput" Sortable="false" CellWidth="110" FormattingMethod="NumberDecimal" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Description" FieldName="Description" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" Sortable="false" Align="Center" CellWidth="200" />
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function createSelect(element) {
		var data = element.d;
		var s = '<select>';
		if (data.length) {
			s += '<option value="0"></option>';
			for (var i = 0, l = data.length; i < l ; i++) {
				s += '<option value="' + data[i].Value + '">' + data[i].Text + '</option>';
			}
		}
		return s += "</select>";
	}

	function search(gridId) {
		gridUserTransaction.Search();
	}

	function setRowColor(listId) {
		var rowIDs = $(listId).jqGrid('getDataIDs');
		var ret;
		if (rowIDs.length > 0) {
			{
				for (var i = 0; i < rowIDs.length; i++) {
					{
						ret = $(listId).jqGrid('getRowData', rowIDs[i]);
						if (ret['Type'] == 1)
							$('#' + $.jgrid.jqID(rowIDs[i])).addClass('success-gridRowColor');
						else
							$('#' + $.jgrid.jqID(rowIDs[i])).addClass('warning-gridRowColor');
					}
				}
			}
		}
	}

	function result(type, message) {
		if (type == "OK") {
			$("#result").addClass("bg-success div-save-result");
			$("#result").html("<span class='fa fa-check fa-2x green'></span>" + message);
		}
		else {
			$("#result").addClass("bg-danger div-save-result");
			$("#result").html("<span class='fa fa-times fa-2x red'></span>" + message);
		}
	}
</script>
