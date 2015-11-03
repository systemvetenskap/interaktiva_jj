<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="Uppgift4.admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    
    <link rel="stylesheet" type="text/css" href="Stilmall1.css"/>
    
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
                <asp:GridView ID="admingrid" runat="server"></asp:GridView>              
            </div>
            <div class="sektion sektion-andra mobil">
              <asp:Button ID="ladda" runat="server" Text="Button" OnClick="ladda_Click" /> 
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

    </form>
</body>
</html>