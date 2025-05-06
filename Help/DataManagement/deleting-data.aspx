<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.master" AutoEventWireup="true"
    CodeFile="deleting-data.aspx.cs" Inherits="Help_DataManagement_deleting_data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3 id="top">
        Deleting Data</h3>
    <p>
        The SecMan system allows you to delete old data that you don't need anymore. This
        means that, if you maintain your data well, you'll never have to filter through
        obsolete data to find what you're looking for.</p>
    <p>
        It's important to be aware that when you delete data, most other data that relates
        to it is also deleted.<br />
        For example, if you delete a complex record, all of the unit records that belong
        to that complex, and every job that was ever captured for any of those units, will
        also be deleted.</p>
    <p>
        This is called a cascading delete. It ensures that the database doesn't fill up
        with unnecessary, inaccessible data. SecMan cascades the following delete operations:</p>
    <ul>
        <li><i>Delete Complex > Delete Units > Delete Jobs</i><br />
            When a complex is deleted, all jobs recorded for every unit in that complex is deleted,
            then each unit is deleted, and lastly, the complex itself is deleted</li>
        <li><i>Delete Unit > Delete Jobs</i><br />
            When a unit is deleted, all jobs for that unit will also be deleted</li>
        <li><i>Delete Insurer > Delete Policies</i><br />
            When an insurer is deleted, all insurance policies captured under that insurer are
            also deleted</li>
    </ul>
    <p>
        When you click on the delete button for a record on any of the management screens,
        SecMan will ask you to confirm that you want to delete the record. This is to prevent
        unintentional deletion of data from the database - you have to be absolutely sure
        that you intend on deleting the record</p>
    <p>
        In summary,</p>
    <p>
        To delete data, simply click on the <b>Delete</b> on the record and confirm the
        action, but be aware that deleting some data will delete other data as well.</p>
</asp:Content>
