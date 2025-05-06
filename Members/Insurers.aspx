<%@ Page Title="" Language="C#" MasterPageFile="~/Members/Members.master" AutoEventWireup="true" CodeFile="Insurers.aspx.cs" Inherits="Members_Insurers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="upInsurers">
        <ContentTemplate>
            <h2>Managing Insurers</h2>
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
                                <aj:AsyncFileUpload runat="server" ID="fuInsurer" Width="280" 
                                    onuploadedcomplete="fuInsurer_UploadedComplete" />
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
                <asp:GridView runat="server" ID="gvItems" DataKeyNames="insurerid" OnSelectedIndexChanging="gvItems_SelectedIndexChanging" OnRowDataBound="gvItems_RowDataBound" OnRowDeleting="gvItems_RowDeleting" OnPageIndexChanging="gvItems_PageIndexChanging" OnSorting="gvItems_Sorting">
                    <Columns>
                        <asp:BoundField DataField="insurerid" HeaderText="ID" SortExpression="insurerid" />
                        <asp:BoundField DataField="name" HeaderText="Insurer Name" SortExpression="name" />
                        <asp:BoundField DataField="contactname" HeaderText="Contact Person" SortExpression="contactname" />
                        <asp:BoundField DataField="contactnumber" HeaderText="Contact Number" />
                        <asp:CommandField ShowSelectButton="true" SelectText="Edit" />
                        <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" />
                    </Columns>
                </asp:GridView>
            </div>

            <div runat="server" id="divAddEdit" visible="false">
                <asp:Label runat="server" ID="lblInsurerID" Visible="false" Text="0"></asp:Label>
                <div class="form">
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Name:<span>The name of this insurer</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtName"></asp:TextBox> <asp:RequiredFieldValidator runat="server" ID="txtName_RFV" ControlToValidate="txtName" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Contact:<span>The name of the contact person for this insurer</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtContact"></asp:TextBox> <asp:RequiredFieldValidator runat="server" ID="txtContact_RFV" ControlToValidate="txtContact" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Number:<span>The telephone number where the contact person can be reached</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtNumber"></asp:TextBox> <asp:RequiredFieldValidator runat="server" ID="txtNumber_RFV" ControlToValidate="txtNumber" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="clear"></div>
                    <div>
                        <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="uppInsurer" DisplayAfter="10">
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
