<%@ Page Title="" Language="C#" MasterPageFile="~/Members/Members.master" AutoEventWireup="true" CodeFile="Jobs.aspx.cs" Inherits="Members_Jobs" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="aj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="upJobs">
        <ContentTemplate>
            <h2>Managing Jobs</h2>
            <div runat="server" id="divView">
                <div class="form">
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Job Number:<span></span>
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
                                Filter By Complex:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <div class="styled-select">
                                <asp:DropDownList runat="server" ID="ddlFilterComplex" AutoPostBack="true" OnSelectedIndexChanged="ddlFilterComplex_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Filter By Unit:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <div class="styled-select">
                                <asp:DropDownList runat="server" ID="ddlFilterUnit" AutoPostBack="true" OnSelectedIndexChanged="ddlFilterUnit_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Filter By Status:<span>See jobs that are...</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <div class="styled-select">
                                <asp:DropDownList runat="server" ID="ddlFilterStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlFilterStatus_SelectedIndexChanged">
                                    <asp:ListItem Text="Please select job status" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                                    <asp:ListItem Text="Complete" Value="Complete"></asp:ListItem>
                                    <asp:ListItem Text="Overdue" Value="Overdue"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="clear"></div>
                </div>
                <div>
                    <asp:Button runat="server" ID="btnAdd" Text="Add New" OnClick="btnAdd_Click" />
                </div>
                <hr />
                <div>
                    <asp:Label runat="server" ID="lblerr" ForeColor="Red"></asp:Label>
                </div>
                <asp:GridView runat="server" ID="gvItems" DataKeyNames="jobid" OnRowDataBound="gvItems_RowDataBound" OnSelectedIndexChanging="gvItems_SelectedIndexChanging" OnRowDeleting="gvItems_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="jobnumber" HeaderText="Job #" />
                        <asp:BoundField DataField="capturedate" HeaderText="Date Logged" DataFormatString="{0:yyyy/MM/dd}" />
                        <asp:BoundField DataField="startdate" HeaderText="Start Date" DataFormatString="{0:yyyy/MM/dd}" />
                        <asp:BoundField DataField="duedate" HeaderText="Due Date" DataFormatString="{0:yyyy/MM/dd}" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                        <asp:BoundField DataField="comeback" HeaderText="Is Comeback?" />
                        <asp:BoundField DataField="description" HeaderText="Description" />
                        <asp:CommandField ShowSelectButton="true" SelectText="Edit" />
                        <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" />
                    </Columns>
                </asp:GridView>
            </div>

            <div runat="server" id="divAddEdit" visible="false">
                <asp:Label runat="server" ID="lblJobID" Text="0" Visible="false"></asp:Label>
                <div class="form">
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Complex:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <div class="styled-select">
                                <asp:DropDownList runat="server" ID="ddlComplex" AutoPostBack="true" OnSelectedIndexChanged="ddlComplex_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Unit:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <div class="styled-select">
                                <asp:DropDownList runat="server" ID="ddlUnit" AutoPostBack="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged"></asp:DropDownList>
                            </div>
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
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Contact Number:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtContactNumber"></asp:TextBox>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Description:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Width="280" Height="150"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="txtDescription_RFV" ControlToValidate="txtDescription" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Status:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <div class="styled-select">
                                <asp:DropDownList runat="server" ID="ddlStatus">
                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                    <asp:ListItem Text="Complete" Value="Complete"></asp:ListItem>
                                    <asp:ListItem Text="Overdue" Value="Overdue"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Callback:<span>Select whether or not this issue's been logged recently, indicating that it wasn't properly dealt with previously</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:RadioButtonList runat="server" ID="rblCallback" AutoPostBack="true" OnSelectedIndexChanged="rblCallback_SelectedIndexChanged">
                                <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
                                <asp:ListItem Text="No" Value="false"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <asp:Panel runat="server" ID="pnlCallback" style="margin: 30px;">
                        <div class="field">
                            <div class="fieldText">
                                <label>
                                    Callback Job:<span>Select the job that was originally logged to deal with this issue</span>
                                </label>
                            </div>
                            <div class="fieldInput">
                                <div class="styled-select">
                                    <asp:DropDownList runat="server" ID="ddlCallbackJobNumber" AutoPostBack="true" OnSelectedIndexChanged="ddlCallbackJobNumber_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="clear"></div>
                        <asp:Label runat="server" ID="lblCallbackJobDescription"></asp:Label>
                    </asp:Panel>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Subcontractor Business:<span>Filter subcontractors by their business</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <div>
                                <div class="styled-select">
                                    <asp:DropDownList runat="server" ID="ddlFilterBusiness" AutoPostBack="true" OnSelectedIndexChanged="ddlFilterBusiness_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="field">
                            <div class="fieldText">
                                <label>
                                    Subcontractors:<span>Select one or more subcontractors for this job</span>
                                </label>
                            </div>
                            <div class="fieldInput">
                                <div>
                                    <div class="styled-select">
                                        <asp:DropDownList runat="server" ID="ddlSubcontractor"></asp:DropDownList>
                                    </div>
                                    <asp:Button runat="server" ID="btnAddSubcontractor" Text="Add" OnClick="btnAddSubcontractor_Click" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                        <div class="field">
                            <div class="fieldText">
                                <label>
                                    Selected Subcontractors:<span>Shows which subcontractors you've selected for this job</span>
                                </label>
                            </div>
                            <div class="fieldInput">
                                <div>
                                    <asp:ListBox runat="server" ID="lstSubcontractors" Width="280"></asp:ListBox><br />
                                    <asp:Button runat="server" ID="btnDelSubcontractor" Text="Remove Selected Subcontractor" OnClick="btnDelSubcontractor_Click" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                        <div class="field">
                            <div class="fieldText">
                                <label>
                                    Start Date:<span></span>
                                </label>
                            </div>
                            <div class="fieldInput">
                                <asp:TextBox runat="server" ID="txtStartDate"></asp:TextBox>
                                <aj:CalendarExtender runat="server" ID="txtStartDate_CE" Format="yyyy/MM/dd" TargetControlID="txtStartDate"></aj:CalendarExtender>
                            </div>
                        </div>
                        <div class="field">
                            <div class="fieldText">
                                <label>
                                    End Date:<span></span>
                                </label>
                            </div>
                            <div class="fieldInput">
                                <asp:TextBox runat="server" ID="txtEndDate"></asp:TextBox>
                                <aj:CalendarExtender runat="server" ID="txtEndDate_CE" Format="yyyy/MM/dd" TargetControlID="txtEndDate"></aj:CalendarExtender>
                            </div>
                        </div>
                        <div class="field">
                            <div class="fieldText">
                                <label>
                                    Quoted Cost:<span></span>
                                </label>
                            </div>
                            <div class="fieldInput">
                                <asp:TextBox runat="server" ID="txtQuote"></asp:TextBox>
                            </div>
                        </div>
                        <div class="field">
                            <div class="fieldText">
                                <label>
                                    Actual Cost:<span></span>
                                </label>
                            </div>
                            <div class="fieldInput">
                                <asp:TextBox runat="server" ID="txtActual"></asp:TextBox>
                            </div>
                        </div>
                        <div class="field">
                            <div class="fieldText">
                                <label>
                                    Insurance:<span>Is this job an insurance claim?</span>
                                </label>
                            </div>
                            <div class="fieldInput">
                                <asp:RadioButtonList runat="server" ID="rblInsurance" AutoPostBack="true" OnSelectedIndexChanged="rblInsurance_SelectedIndexChanged">
                                    <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="false" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnlInsurer" style="margin: 30px;">
                            <div class="field">
                                <div class="fieldText">
                                    <label>
                                        Insurer:<span></span>
                                    </label>
                                </div>
                                <div class="fieldInput">
                                    <div class="styled-select">
                                        <asp:DropDownList runat="server" ID="ddlInsurer" AutoPostBack="true" OnSelectedIndexChanged="ddlInsurer_SelectedIndexChanged"></asp:DropDownList>
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
                                        <asp:DropDownList runat="server" ID="ddlPolicy"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="clear"></div>
                        <div>
                            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
