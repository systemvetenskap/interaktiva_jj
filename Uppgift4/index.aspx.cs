using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Npgsql;
using System.Configuration;
namespace Uppgift4
{
    public partial class index : System.Web.UI.Page
    {
        string sql;
        Anvandare anvandare = new Anvandare();
        string Anvandarna;
        XmlNodeList aktieNodeListFragor;
        XmlDocument docAnv = new XmlDocument();
        //List<Anvandare> anvandarlista = new List<Anvandare>();
        //hejehj
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLoggain_Click(object sender, EventArgs e)
        {
            //Kollainlogg(); 
            if (TextBox1.Text != "" && TextBox2.Text != "")
            {
                bool pissBool = false;
                NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["uppgift4"].ConnectionString);
                conn.Open();
                NpgsqlCommand cmdSql = new NpgsqlCommand("select * from webbutveckling.anvandare where anvandarnamn ='" + TextBox1.Text + "'and losenord ='" + TextBox2.Text + "'", conn);
                NpgsqlDataReader dr = cmdSql.ExecuteReader();

                if (dr.Read())
                {
                    pissBool = true;
                    Anvandare.anvandarnamn = TextBox1.Text;
                    Anvandare.licensiering = dr["licensierad"].ToString();
                    Anvandare.roll = dr["roll"].ToString();

                    if (Anvandare.roll == "admin")
                    {
                        Response.Redirect("~/admin.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/Inloggad.aspx");
                    }
                }

                
                else if (pissBool == false)
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Du har skrivit fel lösenord')", true); 
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Du har inte fyllt i anv eller losen')", true); 
            }
        }


        //private void kollabehorighet()
        //{
        //    try
        //    {
        //        sql = "select licensierad from webbutveckling.anvandare where anvandarnamn = '" + Anvandare.anvandarnamn + "'";
        //        NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["uppgift4"].ConnectionString);
        //        NpgsqlCommand command;
        //        conn.Open();
        //        command = new NpgsqlCommand(sql, conn);
        //        NpgsqlDataReader dr = command.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            Anvandare anvandare = new Anvandare();
        //            Anvandare.anvandarnamn = dr["anvandarnamn"].ToString();
        //            Anvandare.losenord = dr["losenord"].ToString();
        //            Anvandare.licensiering = dr["licensierad"].ToString();

        //        }
        //        conn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('FEL')", true);
        //    }
        //}

        //private void Kollainlogg()
        //{

        //    Anvandarna = Server.MapPath("XMLAnvandare.xml");

        //    aktieNodeListFragor = docAnv.SelectNodes("/Allaanvandare/Anvandare");
        //    docAnv.Load(Anvandarna);            
        //    foreach (XmlNode nod in aktieNodeListFragor)
        //    {
                
        //        Anvandare.anvandarnamn = nod["anvandarnamn"].InnerText;
        //        Anvandare.losenord = nod["losenord"].InnerText;
        //        Anvandare.licensiering = nod["licensiering"].InnerText;
        //        if (Anvandare.anvandarnamn == TextBox1.Text && Anvandare.losenord == TextBox2.Text)
        //        {
        //            //anvandarlista.Add(anvandare);
        //            Response.Redirect("~/Inloggad.aspx");
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Du har skrivit fel lösenord')", true); 
        //        }
                
        //    }

        //}
    }
}