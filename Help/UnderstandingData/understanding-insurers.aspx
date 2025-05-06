<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.master" AutoEventWireup="true"
    CodeFile="understanding-insurers.aspx.cs" Inherits="Help_UnderstandingData_understanding_insurers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3 id="top">
        Understanding Data About Insurers</h3>
    <blockquote>
        Insurer ID | Insurer Name | Contact Name | Contact Number
    </blockquote>
    <table class="helpTable">
        <tr>
            <td width="20%">
                Column Name
            </td>
            <td width="20%">
                Data Type(length)
            </td>
            <td width="60%">
                Description
            </td>
        </tr>
        <tr>
            <td>
                Insurer ID
            </td>
            <td>
                integer
            </td>
            <td>
                <p>
                    A unique, system generated value that is used to identify each insurer record in
                    the database.</p>
                <p>
                    This value is only needed by the user when bulk uploading insurance policies (see
                    <asp:HyperLink runat="server" ID="lnlBulk" NavigateUrl="~/Help/DataManagement/bulk-upload.aspx#add-policy"
                        Text="Bulk Upload"></asp:HyperLink></p>
            </td>
        </tr>
        <tr>
            <td>
                Insurer Name
            </td>
            <td>
                text(50)
            </td>
            <td>
                <p>
                    Required; the name of the insurance company (eg. Sanlam)</p>
            </td>
        </tr>
        <tr>
            <td>
                Contact Name
            </td>
            <td>
                text(50)
            </td>
            <td>
                <p>
                    Required; the name of your liason at this insurance company - the person you normally
                    deal with</p>
            </td>
        </tr>
        <tr>
            <td>
                Contact Number
            </td>
            <td>
                text(50)
            </td>
            <td>
                <p>
                    Required; the telephone number at which you can contact your liason at this insurer</p>
            </td>
        </tr>
    </table>
    <div style="text-align: right;">
        <a href="#top">Back To Top</a>
    </div>
</asp:Content>
