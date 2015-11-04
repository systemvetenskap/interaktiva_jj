<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="Uppgift4.admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    
    <link rel="stylesheet" type="text/css" href="Stilmall2.css"/>
    
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div class="container">
<header>
</header>
        <nav>
                <ul class="clearfix">
                    <li><a href="kompetens1.html">Hem</a></li>
                    <li><a href="">Saker</a>
                     <ul>
                       <li><a href="kompetens2.html">Något</a></li>
                        <li><a href="">Annat</a></li>
            
                        </ul>            </li> 
                    <li><a href="">Om oss</a></li>                       
                </ul>            
       </nav>
    <div class="body">


       
       <div class="sektioner clearfix">
            <div class="sektion mobil">
                <asp:Label ID="lbllicens" runat="server" Text="Ska göra Licenstest"></asp:Label>
                <br />
                <asp:Label ID="lblkunskap" runat="server" Text="Ska göra Kunskapstest"></asp:Label>
            </div>
            <div class="sektion sektion-andra mobil">
               
                 <asp:ListBox ID="ListBoxAnv" runat="server" Width="199px" AutoPostBack="True" OnSelectedIndexChanged="ListBoxAnv_SelectedIndexChanged"></asp:ListBox>
                 <br />
                Användarnamn:
                <asp:TextBox ID="anv" runat="server" Width="69px"></asp:TextBox>
                <br />
                Licensierad:
                <asp:TextBox ID="licensierad" runat="server" Width="69px"></asp:TextBox>
                 <br />
                Roll:
                <asp:TextBox ID="roll" runat="server" Width="69px"></asp:TextBox>
                 <br />
              <asp:Button ID="ladda" runat="server" Text="Visa personal" OnClick="ladda_Click" /> 
              <asp:Button ID="visaprov" runat="server" Text="Visa Senaste Prov" OnClick="visaprov_Click" />
            </div>
           
                <div class="sektion sektion-tredje mobil">
                <p>Testtyp:<asp:TextBox ID="testtyp" runat="server" Width="69px"></asp:TextBox></p>
                <p>Antalrätt ekonomi:<asp:TextBox ID="antek" runat="server" Width="69px"></asp:TextBox></p>
                <p>Antalrätt produkter:<asp:TextBox ID="antpr" runat="server" Width="69px"></asp:TextBox></p>
                <p>Antalrätt etik<asp:TextBox ID="antet" runat="server" Width="69px"></asp:TextBox></p>
                <p>Antalrätt total:<asp:TextBox ID="antto" runat="server" Width="69px"></asp:TextBox></p>
                <p>Testid<asp:TextBox ID="testid" runat="server" Width="69px"></asp:TextBox></p>
                <p>Procenttotal:<asp:TextBox ID="procto" runat="server" Width="69px"></asp:TextBox></p>
                <p>Godkänt resultat:<asp:TextBox ID="godkand" runat="server" Width="69px"></asp:TextBox></p>
                 
            </div>

       </div>   
</div>
            <footer>      
               <p>  JE-Bankens Licenstester <br />
                Skapad av Johan Eklund och Joachim Hörsell</p>
                
                </footer>

    </form>
</body>
</html>