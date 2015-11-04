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
        AdmAnv akutellanvandare = new AdmAnv();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        private void fyllerlista()
        {
            
                sql = "select * from webbutveckling.anvandare;";
                NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["uppgift4"].ConnectionString);
                NpgsqlCommand command;
                conn.Open();
                command = new NpgsqlCommand(sql, conn);            
                NpgsqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {     
                    ListItem anvnamn = new ListItem(dr["anvandarnamn"].ToString());            
                    ListBoxAnv.Items.Add(anvnamn);                   
                }
                

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
            fyllerlista();
        }

        protected void ListBoxAnv_SelectedIndexChanged(object sender, EventArgs e)
        {
                akutellanvandare.anvandarnamn = ListBoxAnv.SelectedItem.Text;
                sql = "select * from webbutveckling.anvandare where anvandarnamn = '" + akutellanvandare.anvandarnamn + "'";
                NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["uppgift4"].ConnectionString);
                NpgsqlCommand command;
                conn.Open();
                command = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    anv.Text = dr["anvandarnamn"].ToString();
                    licensierad.Text = dr["licensierad"].ToString();
                    roll.Text = dr["roll"].ToString();
                }
               
        }
        private void hamtaprov()
        {
            akutellanvandare.anvandarnamn = ListBoxAnv.SelectedItem.Text;
            sql = "select * from webbutveckling.test where anvandarnamn = '" + akutellanvandare.anvandarnamn + "'";
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["uppgift4"].ConnectionString);
            NpgsqlCommand command;
            conn.Open();
            command = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                testtyp.Text = dr["testtyp"].ToString();
                antek.Text = dr["antalrattekonomi"].ToString();
                antpr.Text = dr["antalrattprodukter"].ToString();
                antet.Text = dr["antalrattetik"].ToString();
                antto.Text = dr["antalratttotal"].ToString();
                testid.Text = dr["testid"].ToString();
                procto.Text = dr["procenttotal"].ToString();
                godkand.Text = dr["godkäntresultat"].ToString();

            }
        }
        protected void visaprov_Click(object sender, EventArgs e)
        {
            try
            {
                hamtaprov();
            }
            catch
            {

            }
            
        }
    }
}