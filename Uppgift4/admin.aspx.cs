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
        List<Anvandare> listadminanv = new List<Anvandare>();
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        private void laddatestare()
        {
                
                sql = "select * from webbutveckling.anvandare";
                NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["uppgift4"].ConnectionString);
                NpgsqlCommand command;
                conn.Open();
                command = new NpgsqlCommand(sql, conn);

                
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Anvandare adminanv = new Anvandare();
                    Anvandare.anvandarnamn = dr["anvandarnamn"].ToString();
                    Anvandare.losenord = dr["losenord"].ToString();
                    Anvandare.licensiering = dr["licensierad"].ToString();
                    //Anvandare.kunskapstest = dr["kunskapstest"].ToString();
                    //Anvandare.datumkunskapstest = dr["datumkunskapstest"];
                    Anvandare.roll = dr["roll"].ToString();
                    listadminanv.Add(adminanv);
                }

                admingrid.DataSource = listadminanv;
                admingrid.DataBind();
                
                
                
                //DataSet ds = new DataSet();

                //admingrid.DataSource = ds;
                //admingrid.DataBind();

                conn.Close();
                    



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