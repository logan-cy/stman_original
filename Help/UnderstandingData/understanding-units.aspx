<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.master" AutoEventWireup="true"
    CodeFile="understanding-units.aspx.cs" Inherits="Help_DataManagement_understanding_units" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3 id="top">
        Understanding Data About Units</h3>
    <blockquote>
        Unit ID | Complex ID | Unit Number | Owner Account | Owner Name | Owner Number |
        Tenant Account | Tenant Name | Tenant Number</blockquote>
    <table class="helpTable">
        <tr>
            <td width="20%">
                Column Name
            </td>
            <td width="20%">
                Data Type(length)
            </td>
            <td>
                Description
            </td>
        </tr>
        <tr>
            <td>
                Unit ID
            </td>
            <td>
                integer
            </td>
            <td>
                <p>
                    A unique, system generated value that is used to identify each unit recorded in the
                    database.</p>
                <p>
                    The user should never need this value.</p>
            </td>
        </tr>
        <tr>
            <td>
                Complex ID
            </td>
            <td>
                integer
            </td>
            <td>
                <p>
                    A unique, system generated value that is used to identify the complex to which a
                    unit (or set of units) belongs.</p>
                <p>
                    See <a href="understanding-complexes.aspx">Understanding Complexes</a> for more
                    detail).</p>
            </td>
        </tr>
        <tr>
            <td>
                Unit Number
            </td>
            <td>
                text(50)
            </td>
            <td>
                <p>
                    Required; the unit number identifies the unit within the complex.</p>
            </td>
        </tr>
        <tr>
            <td>
                Owner Account
            </td>
            <td>
                text(50)
            </td>
            <td>
                <p>
                    Required: identifies the account number for the owner of the unit</p>
            </td>
        </tr>
        <tr>
            <td>
                Owner Name
            </td>
            <td>
                text(50)
            </td>
            <td>
                <p>
                    Required: the name of the owner of the unit.</p>
                <p>
                    This is the name of the person who you'll be contacting with any issues regarding
                    this unit.</p>
            </td>
        </tr>
        <tr>
            <td>
                Owner Number
            </td>
            <td>
                text(50)
            </td>
            <td>
                <p>
                    Required; the telephone number at which you can contact the owner of this unit</p>
            </td>
        </tr>
        <tr>
            <td>
                Tenant Account
            </td>
            <td>
                text(50)
            </td>
            <td>
                <p>
                    The account number for the tenant living in the unit</p>
            </td>
        </tr>
        <tr>
            <td>
                Tenant Number
            </td>
            <td>
                text(50)
            </td>
            <td>
                <p>
                    The contact number for the tenant. use this if you're unable to reach the owner
                    for any reason</p>
            </td>
        </tr>
    </table>
    <div style="text-align: right;">
        <a href="#top">Back To Top</a>
    </div>
</asp:Content>
