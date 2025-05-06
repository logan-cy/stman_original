<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.master" AutoEventWireup="true" CodeFile="updating-data.aspx.cs" Inherits="Help_DataManagement_updating_data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3 id="top">
        Updating Existing Data</h3>
    <p>
        Because one of the goals of the SecMan system is to be as simple as possible, it's just as easy to update your data as it is to add new date.</p>
        <p>The basic idea is that you just have to select the record you'd like to update, fill in the new data for it and hit Save. See below:</p>
        <ol>
            <li>Access whatever management screen you want to work on by clicking on the item for it in the main menu</li>
            <li><a href="filter.aspx">Filter</a> or <a href="search.aspx">search</a> for the record that you'd like to update and click on the <b>Select</b> near the bottom-right of the screen that corresponds with the record you'd like to update</li>
            <li>Replace any of the existing data saved for the record with your new value</li>
            <li>Click on Save</li>
        </ol>
</asp:Content>

