<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.master" AutoEventWireup="true"
    CodeFile="understanding-subcontractors.aspx.cs" Inherits="Help_UnderstandingData_understanding_subcontractors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3 id="top">
        Understanding Data About Subcontractors</h3>
    <blockquote>
        Subcontractor ID | Business ID | Subcontractor Name | Physical Address | Contact
        Person | Contact Number | Rating</blockquote>
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
                Subcontractor ID
            </td>
            <td>
                integer
            </td>
            <td>
                <p>
                    A unique, system generated value that is used to identify each subcontractor record
                    in the database</p>
                <p>
                    The user should never need this value</p>
            </td>
        </tr>
        <tr>
            <td>
                Business ID
            </td>
            <td>
                integer
            </td>
            <td>
                <p>
                    A unique, system generated value that is used to identify each subcontractor industry.</p>
                <p>
                    This value is only needed by the user when bulk uploading subcontractors (see
                    <asp:HyperLink runat="server" ID="lnkBulk" Text="Bulk Upload" NavigateUrl="Help/DataManagement/bulk-upload.aspx#add-subcontractor"></asp:HyperLink>)</p>
            </td>
        </tr>
        <tr>
            <td>
                Subcontractor Name
            </td>
            <td>
                text(50)
            </td>
            <td>
                <p>
                    Required; the name of the subcontractor business (eg. AA One Stop)</p>
            </td>
        </tr>
        <tr>
            <td>
                Subcontractor Address
            </td>
            <td>
                text(200)
            </td>
            <td>
                <p>
                    Required; the street at where the subcontractor's business premises are</p>
            </td>
        </tr>
        <tr>
            <td>
                Contact Person
            </td>
            <td>
                varchar(50)
            </td>
            <td>
                <p>
                    Required; the name of your liason at this subcontractor - the person you normally
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
                    Required; the telephone number at which you can contact your liason at this subcontractor</p>
            </td>
        </tr>
        <tr>
            <td>
                Rating
            </td>
            <td>
                integer
            </td>
            <td>
                <p>
                    Rating out of 5 where 1 marks a subcontractor as preferred (first call on the list)
                    and 5 marks the subcontractor as a last resort.</p>
            </td>
        </tr>
    </table>
    <div style="text-align: right;">
        <a href="#top">Back To Top</a>
    </div>
</asp:Content>
