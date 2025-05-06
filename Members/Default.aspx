<%@ Page Title="" Language="C#" MasterPageFile="~/Members/Members.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Members_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        Overview</h2>
    <h3>
        Incomplete Jobs</h3>
    <asp:Label runat="server" ID="lblerr" ForeColor="Red"></asp:Label>
    <asp:GridView runat="server" ID="gvItems" DataKeyNames="jobid" OnSelectedIndexChanging="gvItems_SelectedIndexChanging"
            AllowPaging="true" PagerSettings-Mode="NumericFirstLast" PagerSettings-PageButtonCount="4"
            PageSize="10" OnPageIndexChanging="gvItems_PageIndexChanging" OnSorting="gvItems_Sorting">
            <EmptyDataTemplate>
                <p>
                    There are no jobs due today</p>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="jobnumber" HeaderText="Job #" />
                <asp:BoundField DataField="complexname" HeaderText="Complex" />
                <asp:BoundField DataField="capturedate" HeaderText="Date Logged" DataFormatString="{0:yyyy/MM/dd}" />
                <asp:BoundField DataField="status" HeaderText="Status" />
                <asp:BoundField DataField="description" HeaderText="Description" />
                <asp:CommandField ShowSelectButton="true" SelectText="Select" />
            </Columns>
        </asp:GridView>
</asp:Content>
