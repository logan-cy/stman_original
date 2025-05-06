<%@ Page Title="" Language="C#" MasterPageFile="~/Members/Members.master" AutoEventWireup="true"
    CodeFile="Reports.aspx.cs" Inherits="Members_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="upReports">
        <ContentTemplate>
            <h2>
                Reporting</h2>
            Select all jobs that are
            <asp:DropDownList runat="server" ID="ddlStatus">
                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                <asp:ListItem Text="Complete" Value="Complete"></asp:ListItem>
                <asp:ListItem Text="Overdue" Value="Overdue"></asp:ListItem>
            </asp:DropDownList>
            and filtered by
            <asp:DropDownList runat="server" ID="ddlFilter" AutoPostBack="true" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                <asp:ListItem Text="Complex" Value="complex"></asp:ListItem>
                <asp:ListItem Text="Date Range" Value="date range"></asp:ListItem>
                <asp:ListItem Text="Subcontractor" Value="contractor"></asp:ListItem>
                <asp:ListItem Text="Multiple Subcontractors" Value="multiple"></asp:ListItem>
            </asp:DropDownList>
            <div runat="server" id="divComplexFilter" class="form">
                <div class="field">
                    <div class="fieldText">
                        <label>
                            Complex Name:<span></span>
                        </label>
                    </div>
                    <div class="fieldInput">
                        <asp:DropDownList runat="server" ID="ddlComplex" AutoPostBack="true" OnSelectedIndexChanged="ddlComplex_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div runat="server" id="divContractorFilter" class="form" visible="false">
                <div class="field">
                    <div class="fieldText">
                        <label>
                            Subcontractor:<span></span>
                        </label>
                    </div>
                    <div class="fieldInput">
                        <asp:DropDownList runat="server" ID="ddlContractor" AutoPostBack="true" OnSelectedIndexChanged="ddlContractor_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div runat="server" id="divDateFilter" class="form" visible="false">
                <div class="field">
                    <div class="fieldText">
                        <label>
                            Date From:<span></span>
                        </label>
                    </div>
                    <div class="fieldInput">
                        <asp:TextBox runat="server" ID="txtDateFrom"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender runat="server" ID="ceDateFrom" TargetControlID="txtDateFrom"
                            Format="yyyy/MM/dd">
                        </ajaxToolkit:CalendarExtender>
                    </div>
                </div>
                <div class="field">
                    <div class="fieldText">
                        <label>
                            Date To:<span></span>
                        </label>
                    </div>
                    <div class="fieldInput">
                        <asp:TextBox runat="server" ID="txtDateTo"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender runat="server" ID="ceDateTo" TargetControlID="txtDateTo"
                            Format="yyyy/MM/dd">
                        </ajaxToolkit:CalendarExtender>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
            <div runat="server" id="divReport" visible="false" style="overflow: auto;">
                <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" />
                <hr />
                <asp:ImageButton runat="server" ID="imbExcel" ImageUrl="~/Images/Excel-icon.png"
                    OnClick="imbExcel_Click" ToolTip="Export your selected report to Excel-CSV" />
                <asp:GridView runat="server" ID="gvItems" SkinID="unformatted">
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imbExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="uppReport" DisplayAfter="10">
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
