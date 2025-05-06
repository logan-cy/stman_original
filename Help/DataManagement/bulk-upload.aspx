<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.master" AutoEventWireup="true"
    CodeFile="bulk-upload.aspx.cs" Inherits="Help_DataManagement_bulk_upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3 id="top">
        Uploading Bulk Data</h3>
    <p>
        Jump To: <a href="#missing-data">Missing Data</a> | <a href="#building-csv">Building
            The CSV Files</a> | <a href="#add-complex">Complexes</a> | <a href="#add-unit">Units</a>
        | <a href="#add-subcontractor">Subcontractor</a> | <a href="#add-insurer">Insurer</a>
        | <a href="#add-policy">Policy</a></p>
    <p>
        SecMan provides functionality that allows you to upload large quantities of data
        quickly by making use of comma-delimited CSV files which it reads from before writing
        the data in the CSV file into the database.</p>
    <p>
        Each section requires a particular, and different set of information as detailed
        below, but all CSV files that you'll use to bulk upload data will follow the same
        basic format.</p>
    <p>
        While CSV upload allows you to import up to hundreds of thousands of records at
        the click of a button, there are limitations that you need to consider in the format
        of your CSV file before commencing with the upload.</p>
    <p>
        The following characters are used by the system for copying your CSV data into SecMan's
        database and it is highly recommended that you do not use them under any circumstances
        when capturing your data for upload:</p>
    <table class="helpTableBasic">
        <tr>
            <td>
                Comma (<b>,</b>)
            </td>
            <td>
                Used to delimit column values in the data. SecMan will split a line/row in the CSV
                file into it's constituent columns using commas to tell it where the splits should
                happen
            </td>
        </tr>
        <tr>
            <td>
                Pipe (<b>|</b>)
            </td>
            <td>
                The system convert standard carriage-return line-feed characters to this character
                and uses this to split the data into it's individual rows. Each row is inserted
                into the database individually.
            </td>
        </tr>
    </table>
    <p>
        Any usage of these characters in your files except as outlined in the table above
        will result in errors and/or data corruption. For this reason, it is highly recommended
        that you use alternatives for these characters where you need to.</p>
    <h4 id="missing-data">
        Missing Data</h4>
    <p>
        It's not always the case that you'll have all the data you need to add a set of
        complete records for a given section. Unfortunately, SecMan is rigid about the way
        it reads CSV files, and as such, it requires a value to be entered for all columns
        when inserting in bulk. If you don't have a value for a given column, it is recommended
        that you use a placeholder. Leaving it blank can cause errors and your data probably
        won't import properly.</p>
    <p>
        There's no right or wrong placeholder to use when you don't have data to put into
        a column, but it is highly recommended that you use <b>^N</b> as a general rule.</p>
    <p>
        When the system's importing your bulk data, it checks each value for this placeholder.
        If this placeholder is found anywhere, the system replaces it with a nonexistent
        value for the column in the database table. This keeps screens clear because, rather
        than fill up space with values that don't really mean anything, the columns will
        appear empty which helps you to identify where more complete data will be needed
        from time to time.</p>
    <h4 id="building-csv">
        Building The CSV Files</h4>
    <p>
        The CSV data is going to be inserted into the database table that corresponds with
        the section you're inserting from. These database tables have a certain structure
        and set of rules that have to be adhered to for accurate data entry. Any deviation
        from these rules or any attempt to add data differently to what the table structure
        specifies will cause errors or prevent your data from being saved correctly (if
        at all).</p>
    <p>
        The following sections will show you what the structure of each table looks like.
        SecMan reads the data you're importing into the database and will format the data
        so that rules are obeyed wherever they need to be. All you need to do is make sure
        you have the structure correct before trying to import.</p>
    <h4 id="add-complex">
        Adding A List Of Complexes</h4>
    <p>
        Complexes require the complex name, street address of the complex, the contact details
        for your liason at the complex and, optionally, an insurance policy number (specify
        <b>^N</b> if you don't have one). With these columns in mind, you'll need to add
        the information to the CSV file simply with commas(,) between the values.</p>
    <p>
        This can be done in plain text, or in Excel as the following figures illustrate:</p>
    <div style="margin-left: 20px; margin-bottom: 20px;">
        Complex Name,Contact Person,Complex Address,Contact Number,Policy Number<br />
        Sunshine Valley,69 Sunset Boulevard; Heraldon,Jason,0116938524 / 0825551111,^N<br />
        Dawn Boulevard,23 Ridge Rd; Rockport,Chris,0114563892,SL927635
        <div style="font-style: italic; font-size: 80%;">
            Figure 1: Complex CSV file in plain text
        </div>
    </div>
    <table class="figureTable">
        <tr>
            <td>
                Complex Name
            </td>
            <td>
                Complex Address
            </td>
            <td>
                Contact Person
            </td>
            <td>
                Contact Number
            </td>
            <td>
                Policy Number
            </td>
        </tr>
        <tr>
            <td>
                Sunshine Valley
            </td>
            <td>
                69 Sunset Boulevard; Heraldon
            </td>
            <td>
                Jason
            </td>
            <td>
                0116938524 / 0825551111
            </td>
            <td>
                ^N
            </td>
        </tr>
        <tr>
            <td>
                Dawn Boulevard
            </td>
            <td>
                23 Ridge Rd; Rockport
            </td>
            <td>
                Chris
            </td>
            <td>
                0114563892
            </td>
            <td>
                SL927635
            </td>
        </tr>
    </table>
    <div style="margin-left: 20px; font-style: italic; font-size: 80%;">
        Figure 2: Complex CSV file as it appears in Microsoft&reg; Excel
    </div>
    <div style="text-align: right;">
        <a href="#top">Back To Top</a>
    </div>
    <h4 id="add-unit">
        Adding A List Of Units</h4>
    <p>
        Units require a complex ID, a unit number, the owner's account number and owner
        contact information to be specified in order to import correctly into the database.</p>
    <p>
        The complex ID value links the unit to the complex in the database. This is what
        allows you to filter units by a given complex. The complex ID is required for this
        table and leaving it out will cause errors on the database and your data will not
        import correctly so make sure you have the complex ID on hand when you're building
        the CSV file for your units.</p>
    <p>
        You can find the complex ID for each complex by searching for the complex on the
        <b>Manage Complexes</b> screen (see figure 3).</p>
    <asp:HyperLink runat="server" ID="lnkComplexID" ImageUrl="~/Images/help-complex-id-thumb.jpg"
        NavigateUrl="~/Images/help-complex-id.jpg"></asp:HyperLink>
    <div style="font-style: italic; font-size: 80%;">
        Figure 3: Complex ID shown in Complexes list
    </div>
    <p>
        Tenant information is not required as units won't always have a tenant living in
        them, but if you do have tenant information for a unit, you'll need the tenant's
        account number and his/her name (contact information isn't required). If there is
        no tenant information available for a unit, you can use <b>^N</b> as a placeholder
        for those fields.</p>
    <p>
        Your CSV file should look similar to the following:</p>
    <div style="margin-left: 20px; margin-bottom: 20px;">
        Complex ID,Unit Number,Owner Account,Owner Name,Owner Number,Tenant Account,Tenant
        Name,Tenant Number<br />
        16,1,DNB001,John Doe,0114356972 / 0825551111,^N,^N,^N<br />
        16,10,DNB010,Henry James,011555111,DNT010,Andrew Forester,0860555111
        <div style="font-style: italic; font-size: 80%;">
            Figure 4: Units CSV file in plain text
        </div>
    </div>
    <table class="figureTable">
        <tr>
            <td>
                Complex ID
            </td>
            <td>
                Unit Number
            </td>
            <td>
                Owner Account
            </td>
            <td>
                Owner Name
            </td>
            <td>
                Owner Number
            </td>
            <td>
                Tenant Account
            </td>
            <td>
                Tenant Name
            </td>
            <td>
                Tenant Number
            </td>
        </tr>
        <tr>
            <td>
                16
            </td>
            <td>
                1
            </td>
            <td>
                DNB001
            </td>
            <td>
                John Doe
            </td>
            <td>
                0114356972 / 0825551111
            </td>
            <td>
                ^N
            </td>
            <td>
                ^N
            </td>
            <td>
                ^N
            </td>
        </tr>
        <tr>
            <td>
                16
            </td>
            <td>
                10
            </td>
            <td>
                DNB010
            </td>
            <td>
                Henry James
            </td>
            <td>
                011555111
            </td>
            <td>
                DNT010
            </td>
            <td>
                Andrew Forester
            </td>
            <td>
                0860555111
            </td>
        </tr>
    </table>
    <div style="margin-left: 20px; font-style: italic; font-size: 80%;">
        Figure 5: Units CSV file as it appears in Microsoft&reg; Excel
    </div>
    <div style="text-align: right;">
        <a href="#top">Back To Top</a>
    </div>
    <h4 id="add-subcontractor">
        Adding A List Of Subcontractors</h4>
    <p>
        Subcontractors require a business ID, company name, company address, contact person
        and contact number, and rating (out of 5) to be specified when bulk importing.</p>
    <p>
        Business ID, like the complex ID that's required for the bulk import of units, is
        used to filter the list of subcontractors by the type of their business. Unlike
        the bulk import of units, however, the list of business ID values aren't accessible
        from within the SecMan system. This is because it's a finite list and very unlikely
        to change over a long period of time.</p>
    <p>
        Figure 6 below shows you what the currently stored business types for subcontractors
        are along with their relevant business ID values.</p>
    <asp:GridView runat="server" ID="gvBusinesses">
        <Columns>
            <asp:BoundField DataField="businessid" HeaderText="Business ID" />
            <asp:BoundField DataField="businessname" HeaderText="Business Name" />
        </Columns>
    </asp:GridView>
    <div style="font-style: italic; font-size: 80%;">
        Figure 6: Subcontractor business types currently saved in your copy of SecMan
    </div>
    <div style="text-align: right;">
        <a href="#top">Back To Top</a>
    </div>
    <h4 id="add-insurer">
        Adding A List of Insurers</h4>
    <p>
        Insurers require the name of the Insurance company, the name of your liason there,
        and his/her contact number.</p>
    <div style="margin-left: 20px; margin-bottom: 20px;">
        Insurer Name,Contact Person,Contact Number<br />
        Sanlam,Mr. Broker,0515221346<br />
        ABSA,Mr. Broker,0515221346<br />
        CIA,Mr. Broker,0515221346
        <div style="font-style: italic; font-size: 80%;">
            Figure 7: Insurers CSV file in plain text
        </div>
    </div>
    <table class="helpTable nocenter">
        <tr>
            <td>
                Insurer Name
            </td>
            <td>
                Contact Person
            </td>
            <td>
                Contact Number
            </td>
        </tr>
        <tr>
            <td>
                Sanlam
            </td>
            <td>
                Mr. Broker
            </td>
            <td>
                0515221346
            </td>
        </tr>
        <tr>
            <td>
                ABSA
            </td>
            <td>
                Mr. Broker
            </td>
            <td>
                0515221346
            </td>
        </tr>
        <tr>
            <td>
                CIA
            </td>
            <td>
                Mr. Broker
            </td>
            <td>
                0515221346
            </td>
        </tr>
    </table>
    <div style="margin-left: 20px; font-style: italic; font-size: 80%;">
        Figure 8: Insurers CSV file as it appears in Microsoft&reg; Excel
    </div>
    <div style="text-align: right;">
        <a href="#top">Back To Top</a>
    </div>
    <h4 id="add-policy">
        Adding A List of Insurance Policies</h4>
    <p>
        To add a list of insurance policies, you'll need to know the Insurer ID and the
        Policy Number.<br />
        You can find the Insurer ID for the insurance company on the <b>Manage Insurers</b>screen.</p>
    <div style="margin-left: 20px; margin-bottom: 20px;">
        Insurer ID,Policy Number<br />
        1,CIA02402<br />
        1,CIA10441<br />
        2,263119044840
        <div style="font-style: italic; font-size: 80%;">
            Figure 9: Policy CSV file in plain text
        </div>
    </div>
    <table class="helpTable nocenter">
        <tr>
            <td>
                Insurer ID
            </td>
            <td>
                CIA02402
            </td>
        </tr>
        <tr>
            <td>
                1
            </td>
            <td>
                CIA02402
            </td>
        </tr>
        <tr>
            <td>
                1
            </td>
            <td>
                CIA10441
            </td>
        </tr>
        <tr>
            <td>
                2
            </td>
            <td>
                263119044840
            </td>
        </tr>
    </table>
    <div style="font-style: italic; font-size: 80%;">
        Figure 10: Insurers CSV file as it appears in Microsoft&reg; Excel
    </div>
    <div style="text-align: right;">
        <a href="#top">Back To Top</a>
    </div>
</asp:Content>
