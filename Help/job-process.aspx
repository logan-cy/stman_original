<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.master" AutoEventWireup="true"
    CodeFile="job-process.aspx.cs" Inherits="Help_job_process" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        White House Job Management Process</h3>
    <p>
        This document details how maintenance jobs should be handled within White House
        Estates.</p>
    <p>
        This procedure should be adhered to and followed as closely as possible at all times.</p>
    <h4>
        The Process</h4>
    <ol>
        <li>When someone calls in to report a maintenance problem - whether in a unit or in
            public areas within the complex - the employee handling the call should ensure that
            all relevant detail is captured. Ask the following questions:<br />
            Which complex is it?<br />
            Is it a unit, or communal area? (if communal, specify unit number 0)</li>
        <li>Get the name and phone number of the person who called to report the problem and
            capture this either in the contact details for the job, or as a note in the job
            description. This is the person you will be contacting regularly to get an update
            on the status of the job.</li>
        <li>Choose a subcontractor to go out and fix the problem and send them out to fix it
            or get a quote. Capture this action with the date at the top of the Description
            field</li>
        <li>Contact the owner/tenant/caretaker involved with the job, as well as the assigned
            subcontractor every day for a progress update. Capture this action with the date
            at the top of the Description field as well as detail ("subcontractor going out
            today")</li>
        <li>If there is any reason why there may be a delay in the progress of the job, log
            this in the Description field and only call the relevant parties when necessary.<br />
            e.g. If the subcontractor or tenant is on holiday, call the other parties once to
            inform them, update the description field and then only call again to get updates
            on this job when all parties are once again available.</li>
        <li>When the owner/tenant/caretaker confirms that the problem has been resolved, log
            this in the description field on the job and update the Status of the job reflecting
            that the job has been completed.</li>
    </ol>
</asp:Content>
