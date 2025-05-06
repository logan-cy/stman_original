<%@ Page Title="" Language="C#" MasterPageFile="~/Members/Members.master" AutoEventWireup="true"
    CodeFile="Units.aspx.cs" Inherits="Members_Units" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="upUnit" UpdateMode="Conditional">
        <ContentTemplate>
            <h2>
                Managing Units</h2>
            <div runat="server" id="divView">
                <div class="left-column">
                    <div class="form">
                        <div class="field">
                            <div class="fieldText">
                                <label>
                                    Search:<span>Search for a unit</span>
                                </label>
                            </div>
                            <div class="fieldInput">
                                <asp:TextBox runat="server" ID="txtSearch"></asp:TextBox>
                                <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                        <div class="field">
                            <div class="fieldText">
                                <label>
                                    Complex:<span>Filter units by complex</span>
                                </label>
                            </div>
                            <div class="fieldInput">
                                <div class="styled-select">
                                    <asp:DropDownList runat="server" ID="ddlFilter" AutoPostBack="true" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <asp:Button runat="server" ID="btnAdd" Text="Add New" OnClick="btnAdd_Click" />
                </div>
                <div class="right-column">
                    <div class="form">
                        <div class="field">
                            <div class="fieldText">
                                <label>
                                    Bulk Insert:<span>Upload a comma delimited CSV file to insert into the database.</span>
                                </label>
                            </div>
                            <div class="fieldInput">
                                <aj:AsyncFileUpload runat="server" ID="fuUnit" OnUploadedComplete="fuUnit_UploadedComplete"
                                    Width="280" />
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <asp:Button runat="server" ID="btnBulkAdd" Text="Bulk Add" OnClick="btnBulkAdd_Click" />
                </div>
                <div class="clear">
                </div>
                <hr />
                <div>
                    <asp:Label runat="server" ID="lblerr" ForeColor="Red"></asp:Label>
                </div>
                <asp:GridView runat="server" ID="gvItems" DataKeyNames="unitid" OnSelectedIndexChanging="gvItems_SelectedIndexChanging"
                    OnRowDataBound="gvItems_RowDataBound" OnRowDeleting="gvItems_RowDeleting" OnPageIndexChanging="gvItems_PageIndexChanging"
                    OnSorting="gvItems_Sorting">
                    <Columns>
                        <asp:BoundField DataField="complexname" HeaderText="Complex" SortExpression="complexname" />
                        <asp:BoundField DataField="unitnumber" HeaderText="Unit #" SortExpression="unitnumber" />
                        <asp:BoundField DataField="ownername" HeaderText="Owner" SortExpression="ownername" />
                        <asp:BoundField DataField="ownernumber" HeaderText="Owner Contact #" SortExpression="ownernumber" />
                        <asp:BoundField DataField="tenantname" HeaderText="Tenant" SortExpression="tenantname" />
                        <asp:BoundField DataField="tenantnumber" HeaderText="Tenant Contact #" SortExpression="tenantnumber" />
                        <asp:CommandField ShowSelectButton="true" SelectText="Edit" />
                        <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" />
                    </Columns>
                </asp:GridView>
            </div>
            <div runat="server" id="divAddEdit" visible="false">
                <asp:Label runat="server" ID="lblUnitID" Visible="false" Text="0"></asp:Label>
                <div class="form">
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Complex:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <div class="styled-select">
                                <asp:DropDownList runat="server" ID="ddlComplex">
                                </asp:DropDownList>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="ddlComplex_RFV" ControlToValidate="ddlComplex"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Unit Number:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtUnitNum"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="txtUnitNum_RFV" ControlToValidate="txtUnitNum"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Owner Account:<span>The owner's account number for this unit</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtOwnerAccount"></asp:TextBox>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Owner:<span>The name of the person who owns this unit</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtOwner"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="txtOwner_RFV" ControlToValidate="txtOwner"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Owner Contact:<span>The contact number for the owner of this unit</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtOwnerNum"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="txtOwnerNum_RFV" ControlToValidate="txtOwnerNum"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Tenant Account:<span>The tenant's account number for this unit</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtTenantAccount"></asp:TextBox>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Tenant:<span>The current tenant in this unit</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtTenant"></asp:TextBox>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Tenant Contact<span>The contact number for the tenant in this unit</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtTenantNum"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div>
                        <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click"
                            CausesValidation="false" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="uppUnit" DisplayAfter="10">
        <ProgressTemplate>
            <asp:Panel runat="server" ID="pnlProgress" CssClass="modal">
                <div>
                    <asp:Image runat="server" ID="imgProgress" ImageUrl="~/Images/progress.gif" AlternateText="Processing" />
                </div>
                <strong>Processing...</strong>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
