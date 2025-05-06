<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.master" AutoEventWireup="true"
    CodeFile="additional-support.aspx.cs" Inherits="Help_additional_support" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Additional Support</h3>
    <div runat="server" id="divIntro">
        <p>
            If you have been unable to find any information in these help files that has helped
            you to solve your problem, you can submit a support ticket by filling in the following
            form:</p>
        <p>
            Please be aware that support on SecMan is handled at a cost of R 50.00 for each
            hour that it takes to resolve your problem.<br />
            If you don't need help, but are instead submitting a ticket to request new features
            or changes to your customised copy of SecMan, please note that this does not fall
            under support and you will be quoted separately on the changes/updates you request.</p>
        <div class="form">
            <div class="field">
                <div class="fieldText">
                    <label>
                        Name:<span>Your name</span>
                    </label>
                </div>
                <div class="fieldInput">
                    <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                </div>
            </div>
            <div class="field">
                <div class="fieldText">
                    <label>
                        Email Address:<span>Your email address</span>
                    </label>
                </div>
                <div class="fieldInput">
                    <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                </div>
            </div>
            <div class="field">
                <div class="fieldText">
                    <label>
                        Subject:<span>Specify the type of ticket you're opening</span>
                    </label>
                </div>
                <div class="fieldInput">
                    <div class="styled-select">
                        <asp:DropDownList runat="server" ID="ddlSubject">
                            <asp:ListItem Text="Support Query" Value="support"></asp:ListItem>
                            <asp:ListItem Text="New Feature Request" Value="newFeature"></asp:ListItem>
                            <asp:ListItem Text="Feature Change Request" Value="changeFeature"></asp:ListItem>
                            <asp:ListItem Text="Other" Value="other"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="field">
                <div class="fieldText">
                    <label>
                        Message:<span>Please provide more detail about your request</span>
                    </label>
                </div>
                <div class="fieldInput">
                    <asp:TextBox runat="server" ID="txtMessage" TextMode="MultiLine" Width="280" Height="75"></asp:TextBox>
                </div>
            </div>
            <div class="clear">
            </div>
            <div>
                <asp:Button runat="server" ID="btnSend" Text="Send" OnClick="btnSend_Click" />
                <asp:Button runat="server" ID="btnReset" Text="Reset" OnClick="btnReset_Click" />
            </div>
        </div>
    </div>
    <div runat="server" id="divThankYou" visible="false">
        <p>
            <asp:Label runat="server" ID="lblSuccess" ForeColor="Green">Thank you. Your email has been sent and someone will get back to you on the email address you supplied as soon as possible</asp:Label>
            <asp:Label runat="server" ID="lblError" ForeColor="Red">Oops... Something went wrong and your email could not be sent. Please try again later.</asp:Label></p>
    </div>
</asp:Content>
