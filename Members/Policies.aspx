<%@ Page Title="" Language="C#" MasterPageFile="~/Members/Members.master" AutoEventWireup="true"
    CodeFile="Policies.aspx.cs" Inherits="Members_Policies" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="upPolicies">
        <ContentTemplate>
            <h2>
                Managing Insurance Policies</h2>
            <div runat="server" id="divView">
                <div class="left-column">
                    <div class="form">
                        <div class="field">
                            <div class="fieldText">
                                <label>
                                    Search:<span>Search for an insurance policy</span>
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
                                    Filter:<span>Filter by insurer</span>
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
                                <aj:AsyncFileUpload runat="server" ID="fuPolicy" OnUploadedComplete="fuPolicy_UploadedComplete"
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
                <asp:Label runat="server" ID="lblerr" ForeColor="Red"></asp:Label>
                <asp:GridView runat="server" ID="gvItems" DataKeyNames="policyid" OnRowDataBound="gvItems_RowDataBound"
                    OnRowDeleting="gvItems_RowDeleting" OnSelectedIndexChanging="gvItems_SelectedIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="name" HeaderText="Insurer" SortExpression="name" />
                        <asp:BoundField DataField="policynumber" HeaderText="Policy Number" SortExpression="policynumber" />
                        <asp:CommandField ShowSelectButton="true" SelectText="Edit" />
                        <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" />
                    </Columns>
                </asp:GridView>
            </div>
            <div runat="server" id="divAddEdit" visible="false">
                <asp:Label runat="server" ID="lblPolicyID" Text="0" Visible="false"></asp:Label>
                <div class="form">
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Insurer:<span>The insurer who manages this policy</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <div class="styled-select">
                                <asp:DropDownList runat="server" ID="ddlInsurer">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="ddlInsurer_RFV" ControlToValidate="ddlInsurer"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Policy Number:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtPolicyNumber"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="txtPolicyNumber_RFV" ControlToValidate="txtPolicyNumber"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
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
</asp:Content>
