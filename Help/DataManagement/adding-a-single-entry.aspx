<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.master" AutoEventWireup="true"
    CodeFile="adding-a-single-entry.aspx.cs" Inherits="Help_DataManagement_adding_a_single_entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3 id="top">
        Adding A Single Data Entry</h3>
    <p>
        Jump To: <a href="#add-complex">Complexes</a> | <a href="#add-unit">Units</a> |
        <a href="#add-subcontractor">Subcontractor</a> | <a href="#add-insurer">Insurer</a>
        | <a href="#add-policy">Policy</a> | <a href="#add-job">Job</a></p>
    <p>
        SecMan's power and ease of use comes from it's simplicity and uniformity.<br />
        The design of the CRUD (Create, Read, Update, Delete) screens is meant to be as
        straightforward as possible in order to limit any "what is this" or "what do I do
        here" moments while working.</p>
    <p>
        There's nothing really complicated in any of the data entry screens that could really
        confuse anyone - all input fields are either text input where you can type a value
        or selection boxes that allow you to select an item from a pre-defined or derived
        list.</p>
    <p>
        Every screen follows the same "template" and they all look very similar to each
        other. With only minor differences between screens, any user should be able to easily
        grasp how a screen that they've never been to is meant to work. This means that
        once you're comfortable managing one set of data, you'll quickly become just as
        comfortable managing other data as well.</p>
    <p>
        Before adding any data to the SecMan system, it is vitally important that you attempt
        to locate the data that you intend to add by using either <a href="search.aspx">search</a>
        or <a href="filter.aspx">filtering</a>. This will help you to avoid adding in duplicate
        data which can cause a lot of headaches later on.</p>
    <h4 id="add-complex">
        Adding A Complex</h4>
    <p>
        SecMan can't do anything without any complex records to work with. Every other section
        of data relates to the complexes that are stored.<br />
        To add a complex into the database, follow the following steps:</p>
    <table>
        <tr>
            <td valign="top">
                <div style="margin-left: 20px; font-size: 80%; width: 200px;">
                    <asp:HyperLink runat="server" ID="lnkAddComplex" ImageUrl="~/Images/help-add-complex-thumb.jpg"
                        NavigateUrl="~/Images/help-add-complex.jpg" Target="_blank"></asp:HyperLink><br />
                    <i>Figure 1: Complex addition form with marked required fields</i>
                </div>
            </td>
            <td valign="top">
                <ol>
                    <li>After logging in, click on <b>Manage Complexes</b> in the main menu</li>
                    <li>Search for the complex you intend to add in order to avoid adding in duplicate data</li>
                    <li>If your search/filter found the data you queried, then there's no need to continue,<br />
                        however, if you didn't get any results back, proceed to the next step</li>
                    <li>Click on the <b>"Add New"</b> button</li>
                    <li>Type in the complex name, street address and the details for the contact person
                        that you'll be talking to about general issues with this complex that you're adding</li>
                    <li>Optionally, you can also add a policy number for an insurance policy linked to this
                        complex</li>
                </ol>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <a href="#top">Back to Top</a>
            </td>
        </tr>
    </table>
    <h4 id="add-unit">
        Adding A Unit</h4>
    <p>
        Now that you've added a complex, you'll need to add at least 1 unit to it before
        you can do anything else with SecMan. To do this, follow the steps below:</p>
    <table>
        <tr>
            <td valign="top">
                <div style="margin-left: 20px; font-size: 80%; width: 200px;">
                    <asp:HyperLink runat="server" ID="lnkAddUnit" ImageUrl="~/Images/help-add-unit-thumb.jpg"
                        NavigateUrl="~/Images/help-add-unit.jpg" Target="_blank"></asp:HyperLink><br />
                    <i>Figure 2: Unit addition form with marked required fields</i>
                </div>
            </td>
            <td valign="top">
                <ol>
                    <li>Click on <b>"Manage Units"</b> in the main menu</li>
                    <li>Try to find the unit in the system before you add it. The quickest way will be to
                        filter the unit listing by complex (see <a href="filter.aspx">filtering</a>)</li>
                    <li>If you don't see it, then click on the <b>"Add New"</b> button.</li>
                    <li>Fill in the form that appears. Select the complex to which the new unit belongs
                        and fill in the other fields (tenant information is not required)</li>
                </ol>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <a href="#top">Back to Top</a>
            </td>
        </tr>
    </table>
    <h4 id="add-subcontractor">
        Adding A Subcontractor</h4>
    <p>
        For a job to be completed, you need to have a subcontractor assigned to do the work.</p>
    <p>
        To add a subcontractor, you need to specify which type of business the subcontractor
        is in, what the subcontracting company's name is, and the contact details for them.<br />
        See below:</p>
    <table>
        <tr>
            <td valign="top">
                <div style="margin-left: 20px; font-size: 80%; width: 200px;">
                    <asp:HyperLink runat="server" ID="HyperLink1" ImageUrl="~/Images/help-add-subcontractor-thumb.jpg"
                        NavigateUrl="~/Images/help-add-subcontractor.jpg" Target="_blank"></asp:HyperLink><br />
                    <i>Figure 3: Subcontractor addition form with marked required fields</i>
                </div>
            </td>
            <td valign="top">
                <ol>
                    <li>Click on <b>Manage Subcontractors</b> in the main menu</li>
                    <li>After ensuring that the subcontractor doesn't already exist click on the <b>Add
                        New</b> button</li>
                    <li>Fill in the fields (all are required) and select a business type, and rating from
                        the selection box<br />
                        NOTE: Rating is from 1 to 5 where 1 represents a preferred subcontractor and 5 represents
                        a subcontractor that you prefer to use only when there's no other option</li>
                </ol>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <a href="#top">Back to Top</a>
            </td>
        </tr>
    </table>
    <h4 id="add-insurer">
        Adding An Insurer</h4>
    <p>
        In the same way that complexes "own" units, insurers also "own" insurance policies.</p>
    <p>
        The insurer records require the name of the insurance company, the name of your
        liason there, and their contact details. See below:</p>
    <table>
        <tr>
            <td valign="top">
                <div style="margin-left: 20px; font-size: 80%; width: 200px;">
                    <asp:HyperLink runat="server" ID="HyperLink2" ImageUrl="~/Images/help-add-insurer-thumb.jpg"
                        NavigateUrl="~/Images/help-add-insurer.jpg" Target="_blank"></asp:HyperLink><br />
                    <i>Figure 4: Insurer addition form with marked required fields</i>
                </div>
            </td>
            <td valign="top">
                <ol>
                    <li>Click on <b>Manage Insurers</b> in the main menu</li>
                    <li>After searching for the insurer that you'd like to add, if it wasn't found, click
                        on the <b>Add New</b> button</li>
                    <li>Fill in the fields (all of them are required)</li>
                    <li>When all fields have values, click on the Save button</li>
                </ol>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <a href="#top">Back to Top</a>
            </td>
        </tr>
    </table>
    <h4 id="add-policy">
        Adding An Insurance Policy</h4>
    <p>
        Insurance policies require an insurance company and a policy number and are used
        when a job being logged is an insurance claim.</p>
    <table>
        <tr>
            <td valign="top">
                <div style="margin-left: 20px; font-size: 80%; width: 200px;">
                    <asp:HyperLink runat="server" ID="HyperLink3" ImageUrl="~/Images/help-add-policy-thumb.jpg"
                        NavigateUrl="~/Images/help-add-policy.jpg" Target="_blank"></asp:HyperLink><br />
                    <i>Figure 5: Insurance policy addition form with marked required fields</i>
                </div>
            </td>
            <td valign="top">
                <ol>
                    <li>Click on <b>Manage Insurance Policies</b> in the main menu</li>
                    <li>After searching for the policy that you'd like to add, click on the <b>Add New</b>
                        button if you didn't find it</li>
                    <li>Select the insurance company from the selection list and enter in the policy number</li>
                    <li>Click the Save button to save the new policy into the database</li>
                </ol>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <a href="#top">Back to Top</a>
            </td>
        </tr>
    </table>
    <h4 id="add-job">
        Adding A Job</h4>
    <p>
        The <b>Manage Jobs</b> screen is where all of the data entered in the other screens
        comes together.</p>
    <p>
        When adding a job, you'll need to select the complex and unit where the problem
        is, enter a job description, specify a job status and enter a start date and expected
        completion date (the date when the issue will have been resolved). Optionally, you
        can assign a subcontractor, enter in quote and actual payment details as well as
        insurance information.</p>
    <p>
        When you select a complex and unit, the information that the system has for the
        owner of the unit will appear in the contact fields, but these can be changed if
        needed.</p>
    <p>
        The description basically gives a summary of what the problem is and what the progress
        is towards getting it resolved.<br />
        It is recommended that updates be entered into the description field with the following
        format:</p>
    <blockquote>
        &lt;date - any format you prefer to use&gt;<br />
        &lt;action - a short description of what action was taken on the date specified
        for this update&gt;
    </blockquote>
    <p>
        Example</p>
    <blockquote>
        2013-09-13<br />
        Called subcontractor to remind about quotation
    </blockquote>
    <p>
        The jobs page allows you to filter subcontractors by their business type (plumbers,
        electricians, etc). It also allows you to add multiple subcontractors to a job,
        and remove any from the job that were added in error. You can assign as many or
        as few subcontractors to a job as you want.</p>
    <p>
        Cost information is not required at any point during a job's life cycle, but it
        is highly recommended that this information be entered.</p>
    <p>
        If the job you're entering is an insurance claim, click on the <b>Yes</b> option
        and then select the insurance company and the policy number that corresponds to
        the complex from their respective selection boxes. By default, jobs are captured
        with no insurance information specified.</p>
    <div style="text-align: right;">
        <a href="#top">Back To Top</a>
    </div>
</asp:Content>
