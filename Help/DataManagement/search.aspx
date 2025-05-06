<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.master" AutoEventWireup="true"
    CodeFile="search.aspx.cs" Inherits="Help_DataManagement_search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Searching For Data</h3>
    <p>
        SecMan was built very "fuzzy" search features to help you find the data you're looking
        for.<br />
        This means that to search for a data record, you need only enter a part of the data
        that relates to it. Once you click on Search, SecMan will find every record within
        you chosen section that relates to your search query.</p>
    <h4>
        How to Search</h4>
    <p>
        Regardless of the screen you're on, all searches work exactly the same.<br />
        As an example search, if you have a complex saved who's name is <i>Lions Rock</i>,
        a search under <b>Manage Complexes</b> for any section of text within this complex
        name ("lion", "rock", "ro", "ck", li", "on", etc) will allow you to find it and
        see it's details.</p>
    <p>
        Search checks against every saved value for each record in SecMan's database. This
        means that whatever you search, you'll find every record where any value is like
        your search term.<br />
        Example: If our "Lions Rock" complex was saved with a contact person who's name
        is Andrew, then a search for "andrew", "and", "rew" or any combination of letters
        in the name will also find it for you.</p>
    <h4>
        Summary</h4>
    <p>
        SecMan is capable of managing hundreds of thousands (if not millions) of records,
        but being human, nobody can ever be expected to remember exact details about this
        many records. This is why SecMan comes with a general search feature that allows
        you to search for any bit of information you can remember, and still find what you're
        looking for.</p>
    <blockquote><i>
        Search for something that's like the thing that you want to find, and, in the collection
        of results you get from your search, you'll see the thing you wanted in the first
        place! - Logan, System Architect</i></blockquote>
</asp:Content>
