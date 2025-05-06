<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.master" AutoEventWireup="true" CodeFile="understanding-jobs.aspx.cs" Inherits="Help_UnderstandingData_understanding_jobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3 id="top">
        Understanding Data About Jobs</h3>
    <blockquote>
        Job ID | Unit ID | Capture Data | Job Number | Contact Name | Contact Number | Description | Quoted Cost | Actual Cost | Subcontractors | Start Date | Due Date | Status | Insurance Claim | Policy ID | Comeback</blockquote>
    <table class="helpTable">
        <tr>
            <td width="20%">Column Name</td>
            <td width="20%">Data Type(length)</td>
            <td width="60%">Description</td>
        </tr>
        <tr>
            <td>Job ID</td>
            <td>integer</td>
            <td>
                <p>
                    A unique, system generated value that is used to identify each job recorded in the database.</p>
                <p>
                    The user should never need this value.</p>
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
            <td>Capture Date</td>
            <td>date</td>
            <td>
                <p>
                    A system generated value that indicates when the job was logged.</p>
                <p>
                    This is created automatically.</p>
            </td>
        </tr>
        <tr>
            <td>Job Number</td>
            <td>text(50)</td>
            <td>
                <p>
                    A unique, system generated value that is created when a job is saved for the first time.</p>
                <p>
                    The job number provides a quick reference to access the job.</p>
            </td>
        </tr>
        <tr>
            <td>Contact Name</td>
            <td>text(50)</td>
            <td>
                <p>
                    The name of the caretaker, owner, or tenant for the unit/complex where the job needs to be done.</p>
                <p>
                    This will default to use the name of the owner of the unit.</p>
            </td>
        </tr>
        <tr>
            <td>Contact Number</td>
            <td>text(50)</td>
            <td>
                <p>
                    The number at which you can contact the owner, caretaker, or tenant for the unit/complex where the job needs to be done.</p>
                <p>
                    This will default to use the contact number for the owner of the unit.</p>
            </td>
        </tr>
        <tr>
            <td>Description</td>
            <td>text</td>
            <td>
                <p>
                    required; a basic description of the issue that needs to be dealt with and any updates that have been made on it over time.</p>
            </td>
        </tr>
        <tr>
            <td>Quoted Cost</td>
            <td>decimal</td>
            <td>
                <p>
                    The amount of money that was quoted by the subcontractor to complete the job.</p>
            </td>
        </tr>
        <tr>
            <td>Actual Cost</td>
            <td>decimal</td>
            <td>
                <p>
                    The amount of money that was actually required to complete the job.</p>
            </td>
        </tr>
        <tr>
            <td>Subcontractors</td>
            <td>text(50)</td>
            <td>
                <p>
                    A comma-delimited list that represents each subcontractor selected to work on this job.</p>
                <p>
                    Subcontractors are selected from a drop-down list.</p>
            </td>
        </tr>
        <tr>
            <td>Start Date</td>
            <td>Date</td>
            <td>
                <p>
                    Required; the date when work on this job is required to start.</p>
                <p>
                    This field allows you to select a date from a calendar that appears when you click on it.<br />
                    If you elect to type a date, please use the following date format: <b>yyyy/MM/dd</b>.</p>
            </td>
        </tr>
        <tr>
            <td>Due Date</td>
            <td>Date</td>
            <td>
                <p>
                    Required; the date when the job is expected to have been completed.</p>
                <p>
                    This field allows you to select a date from a calendar that appears when you click on it.<br />
                    If you elect to type a date, please use the following date format: <b>yyyy/MM/dd</b>.</p>
            </td>
        </tr>
        <tr>
            <td>Status</td>
            <td>text(50)</td>
            <td>
                <p>
                    The status of the job.</p>
                <p>
                    A drop-down list allows you to select whether the job is Pending, Complete or Overdue.</p>
            </td>
        </tr>
        <tr>
            <td>Insurance Claim</td>
            <td>true/false</td>
            <td>
                <p>
                    Required; select whether or not the job in question is an insurance claim.</p>
                <p>
                    The default value for this field is <b>false</b> and should be left as is, if the job is not an insurance claim.</p>
            </td>
        </tr>
        <tr>
            <td>Policy ID</td>
            <td>integer</td>
            <td>
                <p>
                    A unique, system generated value that is used to identify each insurance policy recorded in the database.</p>
                <p>If this job is an insurance claim, select which policy it will be recorded for.</p>
            </td>
        </tr>
        <tr>
            <td>Comeback</td>
            <td>true/false</td>
            <td>
                <p>
                    Select whether or not this job is a comeback.</p>
                <p>
                    A comeback is a job which has been logged before, and reported to have been resolved, but the fix was ineffective and the problem now persists.</p>
            </td>
        </tr>
    </table>
    <div style="text-align: right;">
        <a href="#top">Back To Top</a>
    </div>
</asp:Content>

