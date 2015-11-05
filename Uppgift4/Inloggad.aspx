<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inloggad.aspx.cs" Inherits="Uppgift4.Inloggad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    
    <link rel="stylesheet" type="text/css" href="Stilmall2.css"/>
    <%--<link rel="stylesheet" type="text/css" href="Responsiv.css"/>--%>
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
               
                <asp:Label ID="lblKategori" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblRubrik" runat="server" Text="Label"></asp:Label>
                <p>
                    <asp:Button ID="BtnStartatest" runat="server" Text="Starta Testet" OnClick="BtnStartatest_Click" />
                </p>
            <asp:Label ID="LabelNummer" runat="server" Text="1" Visible="False"></asp:Label>
            <asp:Label ID="LabelFraga" runat="server" Text="En fråga" Visible="False"></asp:Label>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Visible="False">
            <asp:ListItem  Value="A" Text="A"></asp:ListItem>
            <asp:ListItem Value="B" Text="B"></asp:ListItem>
            <asp:ListItem Value="C" Text="C"></asp:ListItem>
            </asp:RadioButtonList>
            
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" Visible="False">
                <asp:ListItem  Value="A" Text="alla"></asp:ListItem>
                <asp:ListItem  Value="B" Text="ni"></asp:ListItem>
                <asp:ListItem  Value="C" Text="du"></asp:ListItem>
                <asp:ListItem  Value="D" Text="jag"></asp:ListItem>
                </asp:CheckBoxList>
            <asp:Button ID="BtnForegaende" runat="server" Text="Föregående" OnClick="BtnForegaende_Click" Visible="False" Width="80px" />
                <asp:Button ID="BtnNasta" runat="server" Text="Nästa" OnClick="BtnNasta_Click" Visible="False" Width="80px" />
                <asp:Button ID="BtnRatta" runat="server" OnClick="BtnRatta_Click" Text="Rätta" Width="80px" Visible="False" />
            </div>
            <div class="sektion sektion-andra mobil">
                <asp:Label ID="lbl1" runat="server" Text="Dina Svar:" Visible="False"></asp:Label> 
                <asp:Image id="bilden" runat="server" src="http://www.w3schools.com/images/w3schools_green.jpg" alt="W3Schools.com" hidden="hidden"></asp:Image> 
            </div>
                <div class="sektion sektion-tredje mobil">
                <asp:Label ID="lbl2" runat="server" Text="Rätta Svar:" Visible="False"></asp:Label>              
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
