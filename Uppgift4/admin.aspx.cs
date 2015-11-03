using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Uppgift4
{
    public partial class admin : System.Web.UI.Page
    {
        string sql;
        DataTable dt = new DataTable();
        NpgsqlDataAdapter da;
        List<AdmAnv> listadminanv = new List<AdmAnv>();
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        private void laddatestare()
        {

            sql = "select * from webbutveckling.anvandare;";
 NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["uppgift4"].ConnectionString);
                NpgsqlCommand command;
                conn.Open();
                command = new NpgsqlCommand(sql, conn);            
                NpgsqlDataReader dr = command.ExecuteReader();

                //string anv = null;
                //string licens = null;
                while (dr.Read())
                {

                    //AdmAnv nyanv = new AdmAnv();
                    //nyanv.anvandarnamn = dr["anvandarnamn"].ToString();
                    //nyanv.licensiering = dr["licensierad"].ToString();
                    //nyanv.losenord = dr["losenord"].ToString();
                    //nyanv.roll = dr["roll"].ToString();
                    ListItem anvnamn = new ListItem(dr["anvandarnamn"].ToString());
                    anvnamn.Attributes.Add("Licensierad", dr["licensierad"].ToString());
                    //listadminanv.Add(nyanv);
                    //ListBoxAnv.Items.Add
                    
                    ListBoxAnv.Items.Add(anvnamn);
                   
                }
               

          
            //    while (dr.Read())
            //    {
            //        Kunder kund = new Kunder();
            //        kund.namn = dr["namn"].ToString();
            //        kund.personnr = dr["personnummer"].ToString();
            //        kund.telefonnr = dr["telefonnummer"].ToString();
            //        kund.adress = dr["adress"].ToString();
            //        kund.epost = dr["epost"].ToString();
            //        listBoxKunder.Items.Add(kund);
            //    }
          



                //admingrid.DataSource = listadminanv;
                //admingrid.DataBind();
                //conn.Close();
            
        }
        private void sparaDatabas()
        {
            try
            {
                NpgsqlCommandBuilder cb = new NpgsqlCommandBuilder(da);
                da.Update(dt);
            }
            catch (Exception ex)
            {

            }
        }

        protected void ladda_Click(object sender, EventArgs e)
        {
            laddatestare();
        }
    }
}