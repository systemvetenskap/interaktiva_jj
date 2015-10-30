<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Uppgift4.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="Stilmall.css"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
     <header>
</header>
            <nav>
            </nav>         


<div class="body">
                   <div class="sektioner clearfix"/>
            <div class="sektion mobil">
                <h2>Logga in</h2>

            <form name="loggain">      
                Användarnamn
                <br/>   
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <br/>   
                Lösenord
                <br/>        
                <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
                <br/>   
                <asp:Button ID="BtnLoggain" runat="server" Text="Logga in" Onclick="BtnLoggain_Click"/>
            </form>
            </div>
            <div class="sektion sektion-andra mobil">
             
            </div>
           <div class="sektion sektion-tredje mobil">         
                   </div>
       </div>   
</div>
            <footer>      
                <h6>
                     Hej  <br>
                    å Hå
                    </h6>
                <p> </p>
                </footer>
            </div>
        </div>
    </form>
</body>
</html>
