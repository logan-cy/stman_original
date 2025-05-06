<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.master" AutoEventWireup="true"
    CodeFile="understanding-policies.aspx.cs" Inherits="Help_UnderstandingData_understanding_policies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3 id="top">
        Understanding Data About Insurance Policies</h3>
    <blockquote>
        Policy ID | Insurer ID | Policy Number</blockquote>
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
                Policy ID
            </td>
            <td>
                integer
            </td>
            <td>
                <p>
                    A unique, system generated value that is used to identify each policy record in
                    the database.</p>
                <p>
                    The user should never need this value.</p>
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
                    A unique, system generated value that is used to identify the insurance company
                    associated with the policy.</p>
                <p>
                    This value is only needed by the user when bulk uploading policies (see
                    <asp:HyperLink runat="server" ID="lnkBulk" NavigateUrl="~/Help/DataManagement/bulk-upload.aspx#add-policy"
                        Text="Bulk Upload"></asp:HyperLink></p>
            </td>
        </tr>
        <tr>
            <td>
                Policy Number
            </td>
            <td>
                text(50)
            </td>
            <td>
                <p>
                    Required; the policy number for the insurance policy (eg. CIA02402)</p>
            </td>
        </tr>
    </table>
    <div style="text-align: right;">
        <a href="#top">Back To Top</a>
    </div>
</asp:Content>
