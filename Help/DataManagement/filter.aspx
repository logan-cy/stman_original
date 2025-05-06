<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.master" AutoEventWireup="true"
    CodeFile="filter.aspx.cs" Inherits="Help_DataManagement_filter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Filtering Your Data</h3>
    <p>
        In a collection of millions of records, one would naturally only want to see a specific
        set of them, rather than all those many millions. For that reason, SecMan comes
        with built-in data filters to give you just the records that you're looking for.</p>
    <p>
        Wherever a filter widget exists, you can use it to filter a large amount of data
        by certain criteria to limit the amount of data you're looking at so that you only
        see that data which is most relevant to you at the time.</p>
    <h4>
        How To Filter Data</h4>
    <p>
        Data Filters exist on the Units, Subcontractors, Insurance Policies and Jobs screens.<br />
        These allow you to filter each set by it's "owner" (units are "owned" by complexes,
        subcontractors by industries, insurance policies by insurers).</p>
    <p>
        Simply select the "owner" that you want to filter the data by to get a list of records
        that relate only to that "owner" and hide everything else.<br />
        <b>Example:</b> Lets say you only want to see a list of subcontractors who are plumbers.
        Simply select the Plumber option from the filter field and you'll see only those
        subcontractors who are plumbers now appear on your screen. Filter again by Electrician,
        and your list of plumbers will disappear and be replaced by a new list of electrians.</p>
</asp:Content>
