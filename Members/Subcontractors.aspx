<%@ Page Title="" Language="C#" MasterPageFile="~/Members/Members.master" AutoEventWireup="true"
    CodeFile="Subcontractors.aspx.cs" Inherits="Members_Subcontractors" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="upSubcontractors">
        <ContentTemplate>
            <h2>
                Managing Subcontractors</h2>
            <div runat="server" id="divView">
                <div class="left-column">
                    <div class="form">
                        <div class="field">
                            <div class="fieldText">
                                <label>
                                    Search:<span>Search for a subcontractor</span>
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
                                    Business:<span>Filter subcontractors by their business</span>
                                </label>
                            </div>
                            <div class="fieldInput">
                                <div class="styled-select">
                                    <asp:DropDownList runat="server" ID="ddlFilter" AutoPostBack="true" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div>
                            <asp:Button runat="server" ID="btnAdd" Text="Add New" OnClick="btnAdd_Click" />
                        </div>
                    </div>
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
                                <aj:AsyncFileUpload runat="server" ID="fuSubcontractor" OnUploadedComplete="fuSubcontractor_UploadedComplete"
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
                <asp:GridView runat="server" ID="gvItems" DataKeyNames="subcontractorid" OnRowDataBound="gvItems_RowDataBound"
                    OnSelectedIndexChanging="gvItems_SelectedIndexChanging" OnRowDeleting="gvItems_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="name" HeaderText="Subcontractor Name" SortExpression="name" />
                        <asp:BoundField DataField="address" HeaderText="Subcontractor Address" SortExpression="address" />
                        <asp:BoundField DataField="businessname" HeaderText="Business" SortExpression="buesiness" />
                        <asp:BoundField DataField="rating" HeaderText="Rating (out of 5)" SortExpression="rating" />
                        <asp:CommandField ShowSelectButton="true" SelectText="Edit" />
                        <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" />
                    </Columns>
                </asp:GridView>
            </div>
            </div>
            <div runat="server" id="divAddEdit" visible="false">
                <asp:Label runat="server" ID="lblSubcontractorID" Text="0" Visible="false"></asp:Label>
                <div class="form">
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Name:<span>The name of this subcontractor</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="txtName_RFV" ControlToValidate="txtName"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Address:<span>The address of this subcontractor</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtAddress">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="txtAddress_RFV" ControlToValidate="txtAddress"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Contact Person:<span>The name of the contact person at this subcontractor</span>
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
                                Contact Number:<span>The contact number for this subcontractor</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtContactNumber"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="txtContactNumber_RFV" ControlToValidate="txtContactNumber"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Business:<span>The business that this subcontractor is in</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <div class="styled-select">
                                <asp:DropDownList runat="server" ID="ddlBusiness">
                                </asp:DropDownList>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="ddlBusiness_RFV" ControlToValidate="ddlBusiness"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Rating:<span>Priority of this subcontractor when assigning jobs</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <div class="styled-select">
                                <asp:DropDownList runat="server" ID="ddlRating">
                                    <asp:ListItem Text="1 - First Choice" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5 - Last Choice" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div>
                        <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="uppSubcontractor" DisplayAfter="10">
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
