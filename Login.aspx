<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="App_Themes/Default/core.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <h1>
            Sectional Title Management</h1>
        <div class="wrap">
            <asp:Panel runat="server" ID="pnlLogin" DefaultButton="btnLogin" CssClass="loginPanel">
                <h2>Login</h2>
                <div class="form">
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Email Address:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Password:<span></span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="field">
                        <div class="fieldText">
                            <label>
                                Remember Me?<span>Saves your login data for 7 days</span>
                            </label>
                        </div>
                        <div class="fieldInput">
                            <asp:CheckBox runat="server" ID="chkCookie" />
                        </div>
                    </div>
                    <div class="clear"></div>

                    <div>
                        <asp:Label runat="server" ID="lblerr" ForeColor="Red"></asp:Label>
                    </div>

                    <div>
                        <asp:Button runat="server" ID="btnLogin" Text="Log In" OnClick="btnLogin_Click" />
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="footer">
            <a href="http://www.loganyoung.za.net/" target="_blank">Logan Young</a> &copy; 2013
        </div>
    </form>
</body>
</html>
