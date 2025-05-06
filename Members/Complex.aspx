<%@ Page Title="" Language="C#" MasterPageFile="~/Members/Members.master" AutoEventWireup="true"
    CodeFile="Complex.aspx.cs" Inherits="Members_Complex" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="upComplex" UpdateMode="Conditional">
        <ContentTemplate>
            <h2>
                Managing Complexes</h2>
            <div runat="server" id="divView">
                <div class="left-column">
                    <div class="form">
                        <div class="field">
                            <div class="fieldText">
                                <label>
                                    Search:<span>Search for a complex</span>
                                </label>
                            </div>
                            <div class="fieldInput">
                                <asp:TextBox runat="server" ID="txtSearch"></asp:TextBox>
                                <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
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
                                <aj:AsyncFileUpload runat="server" ID="fuComplex" OnUploadedComplete="fuComplex_UploadedComplete"
                                    Width="280" />
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <asp:Button runat="server" ID="btnAddBulk" Text="Add Bulk" OnClick="btnAddBulk_Click" />
                </div>
                <div class="clear">
                </div>
                <hr />
                <div>
                    <asp:Label runat="server" ID="lblerr" ForeColor="Red"></asp:Label>
                </div>
                <asp:GridView runat="server" ID="gvItems" DataKeyNames="complexid" OnSelectedIndexChanging="gvItems_SelectedIndexChanging"
                    OnRowDataBound="gvItems_RowDataBound" OnRowDeleting="gvItems_RowDeleting" OnPageIndexChanging="gvItems_PageIndexChanging"
                    OnSorting="gvItems_Sorting">
                    <Columns>
                        <asp:BoundField DataField="complexid" HeaderText="ID" SortExpression="complexid" />
                        <asp:BoundField DataField="name" HeaderText="Complex Name" SortExpression="name" />
                        <asp:BoundField DataField="contactperson" HeaderText="Contact Person" SortExpression="contactperson" />
                        <asp:BoundField DataField="contactnumber" HeaderText="Contact number" SortExpression="contactnumber" />
                        <asp:BoundField DataField="address" HeaderText="Address" SortExpression="address" />
                        <asp:CommandField ShowSelectButton="true" SelectText="Edit" />
                        <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" />
                    </Columns>
                </asp:GridView>
            </div>
            <div runat="server" id="divAddEdit" visible="false">
                <asp:Label runat="server" ID="lblComplexID" Visible="false" Text="0"></asp:Label>
                <div class="form">
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Complex Name:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtComplexName"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="txtComplexName_RFV" ControlToValidate="txtComplexName"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Street Address:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtComplexAddress"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="txtComplexAddress_RFV" ControlToValidate="txtComplexName"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Contact Person:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtContactPerson"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="txtContactPerson_RFV" ControlToValidate="txtContactPerson"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Contact Number:<span>The number of the contact person for this complex</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtContactNumber"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="txtContactNumber_RFV" ControlToValidate="txtContactNumber"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <%--<div class="field">
                        <div class="fieldText">
                            <label>
                                Policy Number:<span>The insurance policy number for this complex</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtPolicyNumber"></asp:TextBox>
                        </div>
                    </div>--%>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Insurer:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <div class="styled-select">
                                <asp:DropDownList runat="server" ID="ddlInsurer" AutoPostBack="true" OnSelectedIndexChanged="ddlInsurer_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Insurance Policy:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <div class="styled-select">
                                <asp:DropDownList runat="server" ID="ddlPolicy">
                                </asp:DropDownList>
                            </div>
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
    <asp:UpdateProgress runat="server" ID="uppComplex" DisplayAfter="10">
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
