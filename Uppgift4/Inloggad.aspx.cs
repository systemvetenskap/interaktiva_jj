using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;


namespace Uppgift4
{
    public partial class Inloggad : System.Web.UI.Page
    {
        string kategori;
        string sql;
        DataTable dt = new DataTable();
        NpgsqlDataAdapter da;
        string XMLFragorna;       
        XmlNodeList aktieNodeListFragor;
        XmlDocument docFra = new XmlDocument();
        List<FrageKlass> frageLista = new List<FrageKlass>();
        List<Anvandare> anvandareLista = new List<Anvandare>();
        int FrageNummer;
        bool licens;
        string licensiering;
        //för att rättafdsfsd
        XmlNodeList XMLFragorMetodRatta;
        XmlNodeList XMLSvarMetodRatta;
        List<SvarFragor> SvarLista = new List<SvarFragor>();
        protected void Page_Load(object sender, EventArgs e)
        {

            ////ta bort, bara för att inlogg inte funkar för mig
            //Anvandare.anvandarnamn = "1";
            //Anvandare.licensiering = "nej";
            ////hit



            if (Anvandare.licensiering == "ja")
            {
                lblRubrik.Text = "Välkommen till testet " + Anvandare.anvandarnamn;
                licens = true;
            }
            else
            {
                licens = false;
                lblRubrik.Text = "Välkommen till testet " + Anvandare.anvandarnamn;
            }
        }
        private void sattIhop()
        {
            
            
        
        }
        private void andraTillLicensierad()
        {
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["uppgift4"].ConnectionString);
            NpgsqlCommand command;
            conn.Open();
            command = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader dr = command.ExecuteReader();
            sql = "update webbutveckling.anvandare set licensierad = 'ja' where anvandarnamn = '"+Anvandare.anvandarnamn+"';";
            sparaDatabas();

        }
        private void LasInFraga()
        {
            if (licens == false)
            {
                XMLFragorna = Server.MapPath("XMLFragorna.xml");
            }
            else
            {
                XMLFragorna = Server.MapPath("XMLKunskapstest.xml");
            }

            RadioButtonList1.SelectedIndex = -1;
            LabelNummer.Text = Convert.ToString(FrageNummer);
            

            aktieNodeListFragor = docFra.SelectNodes("/fragor/fraga");
            docFra.Load(XMLFragorna);
            int labelnummer = 0;
            string fragansNummer = Convert.ToString(FrageNummer);
            foreach (XmlNode nod in aktieNodeListFragor)
            {
                FrageKlass fraga = new FrageKlass();
                fraga.nummer = nod["nummer"].InnerText;
                fraga.fragan = nod["fragan"].InnerText;
                fraga.a = nod["a"].InnerText;
                fraga.b = nod["b"].InnerText;
                fraga.c = nod["c"].InnerText;
                fraga.Kategori = nod["Kategori"].InnerText;
                frageLista.Add(fraga);

                if (fragansNummer == FrageNummer.ToString() && fraga.nummer == FrageNummer.ToString())
                {
                        
                        frageLista.Add(fraga);
                        kategori = null;
                        LabelFraga.Text = ": " + frageLista[FrageNummer].fragan;
                        RadioButtonList1.Items.FindByValue("A").Text = frageLista[FrageNummer].a;
                        RadioButtonList1.Items.FindByValue("B").Text = frageLista[FrageNummer].b;
                        RadioButtonList1.Items.FindByValue("C").Text = frageLista[FrageNummer].c;
                        lblKategori.Text = frageLista[FrageNummer].Kategori;
                        
                    
                }
                else if (FrageNummer == 5)
                {
                    LabelFraga.Text = "Slut på frågor";
                    BtnNasta.Visible = false;
                    LabelNummer.Visible = false;
                    RadioButtonList1.Visible = false;
                    BtnRatta.Visible = true;
                }            
            }
         
            labelnummer++;

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
        private void sparaXml()
        {
            //fixa så rätt nummer 
            //int haj = Convert.ToInt16(LabelNummer.Text);
            string hej = LabelNummer.Text;
            

            //öppnar, dumt att inte göra/använda samma som tidigare?strul att använda samma?
            System.Xml.XmlDocument docX = new System.Xml.XmlDocument();
            docX.Load(Server.MapPath("XMLspara.xml"));
            System.Xml.XmlNode xmlNode = docX.DocumentElement.FirstChild;
            //skapa fixa så skapas klass sen

            System.Xml.XmlElement xmlElement = docX.CreateElement("fraga");
            xmlElement.SetAttribute("nummer", hej);
            xmlElement.SetAttribute("namn", Server.HtmlEncode(Anvandare.anvandarnamn));
            xmlElement.SetAttribute("mittSvar", RadioButtonList1.SelectedItem.Text);
            xmlElement.SetAttribute("Kategori", lblKategori.Text);
            //xmlElement.SetAttribute("frågan", LabelFraga.Text);//testar

            if (licens == false)
            {
                xmlElement.SetAttribute("test", "licens");
            }
            else
            {
                xmlElement.SetAttribute("test", "kunskap");
            }
            //inseet
            docX.DocumentElement.InsertBefore(xmlElement, xmlNode);
            docX.Save(Server.MapPath("XMLspara.xml"));

            //TextBox1.Text = "";
            RadioButtonList1.SelectedIndex = 0;
            BindData();
            
        }

        private void raderaXMLspara()
        {
        
            XmlDocument doc = new XmlDocument();
        
            doc.Load(Server.MapPath("XMLspara.xml"));
            doc.DocumentElement.RemoveAll();
            doc.Save(Server.MapPath("XMLspara.xml"));

        }
        void BindData()
        {
            XmlTextReader XmlReader = new XmlTextReader(Server.MapPath("XMLspara.xml"));
            XmlReader.Close();
        }

        private void rattaProv()
        {
            string metodRattaFragorna;
            if (licens == false)
            {
                metodRattaFragorna = Server.MapPath("XMLFragorna.xml");
            }
            else
            {
                metodRattaFragorna = Server.MapPath("XMLKunskapstest.xml");
            }



            //för att kolla procent på totalrtt
            decimal raknaTotal;
            decimal procentTotalDec = 0;
            string tabort;
            string procentTotal = "";
            decimal SjuttioProcent = 0.7M;
            //för att kolla rätt etik
            decimal raknaEtik;
            decimal procentEtikDec = 0;
            string tabortEtik;
            string EtikTotal = "";
            decimal SextioProcent = 0.6M;
            //för att kolla ekonomi
            decimal raknaEkonomi;
            decimal procentEkonomiDec = 0;
            string tabortEkonomi;
            string EkonomiTotal = "";
            //för att kolla produkt
            decimal raknaProdukt;
            decimal procentProduktDec = 0;
            string tabortProdukt;
            string ProduktTotal = "";


            int totalrattSvar = 0;
            int antalFragor = 0;
            int antalrattetik = 0;
            int antalFragorEtik = 0;
            int antalrattekonomi = 0;
            int antalFragorEkonomi = 0;
            int antalrattprodukter = 0;
            int antalFragorProdukter = 0;

            
            System.Xml.XmlDocument docX = new System.Xml.XmlDocument();
            docX.Load(Server.MapPath("XMLspara.xml"));
            System.Xml.XmlNode xmlNode = docX.DocumentElement.FirstChild;
         
       
            LabelNummer.Text = Convert.ToString(FrageNummer);
            
            
            XMLFragorMetodRatta = docFra.SelectNodes("/fragor/fraga");
            docFra.Load(metodRattaFragorna);



            foreach (XmlNode nod in XMLFragorMetodRatta)
            {
                
                FrageKlass fraga = new FrageKlass();
                fraga.nummer = nod["nummer"].InnerText;
                fraga.fragan = nod["fragan"].InnerText;
                fraga.a = nod["a"].InnerText;
                fraga.b = nod["b"].InnerText;
                fraga.c = nod["c"].InnerText;
                fraga.RattSvar = nod["RattSvar"].InnerText;
                fraga.Kategori = nod["Kategori"].InnerText;
                fraga.test = nod["test"].InnerText;
                frageLista.Add(fraga);
            }



           

            string metodRattaMinaSvar;
            metodRattaMinaSvar = Server.MapPath("XMLspara.xml");
            XMLSvarMetodRatta = docFra.SelectNodes("/sparaTest/fraga");
            docFra.Load(metodRattaMinaSvar);
        
           
            int i = 0;
            

            foreach (XmlNode nod in XMLSvarMetodRatta)
            {
                SvarFragor svar = new SvarFragor();
                svar.nummer = nod.Attributes["nummer"].Value;
                svar.namn = nod.Attributes["namn"].Value;
                svar.mittSvar = nod.Attributes["mittSvar"].Value;
                svar.Kategori = nod.Attributes["Kategori"].Value;
                svar.test = nod.Attributes["test"].Value;

                if (svar.namn == Anvandare.anvandarnamn)
                {
                    SvarLista.Add(svar);
                }
            }


            frageLista.Reverse();

            foreach (Object svar in SvarLista)
            {
                if (SvarLista[i].nummer == frageLista[i].nummer)
                {
                    if (SvarLista[i].mittSvar == frageLista[i].RattSvar)
                    {
                        
                        System.Xml.XmlElement xmlElement = docX.CreateElement("svar");
                        xmlElement.SetAttribute("nummer", frageLista[i].nummer);//i.ToString());
                        xmlElement.SetAttribute("namn", Server.HtmlEncode(Anvandare.anvandarnamn));
                        xmlElement.SetAttribute("mittSvar", SvarLista[i].mittSvar); //i.ToString());
                        xmlElement.SetAttribute("ratt", "ja");
                        xmlElement.SetAttribute("Kategori", SvarLista[i].Kategori);
                        //xmlElement.SetAttribute("frågan", LabelFraga.Text);


                        if (licens == false)
                        {
                            xmlElement.SetAttribute("test", "licens");
                        }
                        if (licens == true)
                        {
                            xmlElement.SetAttribute("test", "kunskap");
                        }


                        if (SvarLista[i].Kategori == "Ekonomi – nationalekonomi, finansiell ekonomi och privatekonomi")
                        {
                            antalrattekonomi++;
                            antalFragorEkonomi++;
                        }
                        if (SvarLista[i].Kategori == "Etik och regelverk.")
                        {
                            antalrattetik++;
                            antalFragorEtik++;
                        }
                        if (SvarLista[i].Kategori == "Produkter och hantering av kundens affärer")
                        {
                            antalrattprodukter++;
                            antalFragorProdukter++;
                        }
                        docX.DocumentElement.InsertBefore(xmlElement, xmlNode);
                        docX.Save(Server.MapPath("XMLspara.xml"));
                        totalrattSvar++;


                    }
                    if (SvarLista[i].mittSvar != frageLista[i].RattSvar)
                    {
                        System.Xml.XmlElement xmlElement = docX.CreateElement("svar");
                        xmlElement.SetAttribute("nummer", frageLista[i].nummer);//i.ToString());
                        xmlElement.SetAttribute("namn", Server.HtmlEncode(Anvandare.anvandarnamn));
                        xmlElement.SetAttribute("mittSvar", SvarLista[i].mittSvar);//i.ToString());
                        xmlElement.SetAttribute("ratt", "nej");
                        xmlElement.SetAttribute("Kategori", SvarLista[i].Kategori);
                        if (licens == false)
                        {
                            xmlElement.SetAttribute("test", "licens");
                        }
                        if (licens == true)
                        {
                            xmlElement.SetAttribute("test", "kunskap");
                        }

                        if (SvarLista[i].Kategori == "Ekonomi – nationalekonomi, finansiell ekonomi och privatekonomi")
                        {
                            antalFragorEkonomi++;
                        }
                        if (SvarLista[i].Kategori == "Etik och regelverk.")
                        {
                            antalFragorEtik++;
                        }
                        if (SvarLista[i].Kategori == "Produkter och hantering av kundens affärer")
                        {
                            antalFragorProdukter++;
                        }
                        docX.DocumentElement.InsertBefore(xmlElement, xmlNode);
                        docX.Save(Server.MapPath("XMLspara.xml"));
                    }         
                    i++;
                    antalFragor++;
                } 
            }
            //if (licens == false)
            //{
            //räkna på total och skicka in i databas
            raknaTotal = (decimal)totalrattSvar / (decimal)antalFragor;
            procentTotalDec = Math.Round(raknaTotal, 2);
            tabort = procentTotalDec.ToString();
            procentTotal = tabort.Replace(',', '.');

            //    sql = "insert into webbutveckling.test (testtyp,antalrattekonomi,antalrattprodukter,antalrattetik, antalratttotal, anvandarnamn, procenttotal) values ('licens'," + antalrattekonomi + "," + antalrattprodukter + "," + antalrattetik + "," + totalrattSvar + ",'" + Anvandare.anvandarnamn + "'," + procentTotal + ")";
                
            //}
            //if (licens == true)
            //{
            //    raknaTotal = (decimal)totalrattSvar / (decimal)antalFragor;
            //    procentTotalDec = Math.Round(raknaTotal, 2);
            //    tabort = procentTotalDec.ToString();
            //    procentTotal = tabort.Replace(',', '.');
                
            //    sql = "insert into webbutveckling.test (testtyp,antalrattekonomi,antalrattprodukter,antalrattetik, antalratttotal, anvandarnamn, procenttotal) values ('kunskap'," + antalrattekonomi + "," + antalrattprodukter + "," + antalrattetik + "," + totalrattSvar + ",'" + Anvandare.anvandarnamn + "'," + procentTotal + ")";
           
            //}

            string formprocentTotalDec = procentTotalDec.ToString("0.00%");
            //räkna på Etik ska överiga lagras i databas?
            //if (antalFragorEtik != 0)
            //{
                raknaEtik = (decimal)antalrattetik / (decimal)antalFragorEtik;
                procentEtikDec = Math.Round(raknaEtik, 2);
                tabortEtik = procentEtikDec.ToString();
                EtikTotal = tabortEtik.Replace(',', '.');
                string formprocentEtikDec = procentEtikDec.ToString("0.00%");
            //}
            //Räkna på ekonomi
            //if (antalFragorEkonomi != 0)
            //{
                raknaEkonomi = (decimal)antalrattekonomi / (decimal)antalFragorEkonomi;
                procentEkonomiDec = Math.Round(raknaEkonomi, 2);
                tabortEkonomi = procentEkonomiDec.ToString();
                EkonomiTotal = tabortEkonomi.Replace(',', '.');
                string formprocentEkonomiDec = procentEkonomiDec.ToString("0.00%");
            //}
            //if (antalFragorProdukter != 0)
            //{
                //Räkna på produkt
                raknaProdukt = (decimal)antalrattprodukter / (decimal)antalFragorProdukter;
                procentProduktDec = Math.Round(raknaProdukt, 2);
                tabortProdukt = procentProduktDec.ToString();
                ProduktTotal = tabortProdukt.Replace(',', '.');
                string formprocentProduktDec = procentProduktDec.ToString("0.00%");
            //}

            if (procentTotalDec < SjuttioProcent)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('DU har för många fel, försök igen.')", true);
                if (licens == false)
                {

                    sql = "insert into webbutveckling.test (testtyp,antalrattekonomi,antalrattprodukter,antalrattetik, antalratttotal, anvandarnamn, procenttotal,godkäntresultat) values ('licens'," + antalrattekonomi + "," + antalrattprodukter + "," + antalrattetik + "," + totalrattSvar + ",'" + Anvandare.anvandarnamn + "'," + procentTotal + ",'nej')";
                    
                }
                if (licens == true)
                {


                    sql = "insert into webbutveckling.test (testtyp,antalrattekonomi,antalrattprodukter,antalrattetik, antalratttotal, anvandarnamn, procenttotal,godkäntresultat) values ('kunskap'," + antalrattekonomi + "," + antalrattprodukter + "," + antalrattetik + "," + totalrattSvar + ",'" + Anvandare.anvandarnamn + "'," + procentTotal + ",'nej')";

                }
            }
            if (procentTotalDec > SjuttioProcent&& procentEtikDec>SextioProcent&&procentEkonomiDec>SextioProcent&&procentProduktDec>SextioProcent)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Du har ett godkännt resultat')", true);

                if (licens == false)
                {

                    sql = "insert into webbutveckling.test (testtyp,antalrattekonomi,antalrattprodukter,antalrattetik, antalratttotal, anvandarnamn, procenttotal,godkäntresultat) values ('licens'," + antalrattekonomi + "," + antalrattprodukter + "," + antalrattetik + "," + totalrattSvar + ",'" + Anvandare.anvandarnamn + "'," + procentTotal +",'ja')";
                    
                    andraTillLicensierad();
                    sparaDatabas(); 
                }
                if (licens == true)
                {

                    sql = "insert into webbutveckling.test (testtyp,antalrattekonomi,antalrattprodukter,antalrattetik, antalratttotal, anvandarnamn, procenttotal,godkäntresultat) values ('kunskap'," + antalrattekonomi + "," + antalrattprodukter + "," + antalrattetik + "," + totalrattSvar + ",'" + Anvandare.anvandarnamn + "'," + procentTotal + ",'ja')";
                    sparaDatabas(); 
                }

            }

            lblKategori.Text = "Antal rätt svar: " + totalrattSvar.ToString() + "<br />" + "Andel rätt procent total: " + formprocentTotalDec + "<br />" + "Andel rätt procent Etik: " + formprocentEtikDec + "<br />" + "Andel rätt procent Ekonomi: " + formprocentEkonomiDec + "<br />" + "Andel rätt procent Produkt: " + formprocentProduktDec;
                sparaDatabas();
               
                int x = 0;
                foreach (Object svar in SvarLista)
                {   
                    string fragan = frageLista[x].fragan.ToString();
                    
                    if(SvarLista[x].mittSvar == frageLista[x].RattSvar)
                    {
                        string html = "<font style='color: green'>" + fragan + "</font>";
                        lbl1.Text += "<br />" + html + ": " + SvarLista[x].mittSvar.ToString();
                    }
                    if (SvarLista[x].mittSvar != frageLista[x].RattSvar)
                    {
                        string html = "<font style='color: red'>" + fragan + "</font>";
                        lbl1.Text += "<br />" + html + ": " + SvarLista[x].mittSvar.ToString();
                    }
                    
                    
                    lbl2.Text += "<br />" + frageLista[x].RattSvar.ToString();
                    x++;

                }
                lbl1.Visible = true;
                lbl2.Visible = true;
        }
            
        protected void BtnForegaende_Click(object sender, EventArgs e)
        {
            int haj = Convert.ToInt16(LabelNummer.Text);
            FrageNummer = haj - 1;
            LasInFraga();
        }

        protected void BtnNasta_Click(object sender, EventArgs e)
        {
            if(RadioButtonList1.SelectedIndex == -1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Välj ett svarsalternativ')", true); 
            }
            else
            {
                sparaXml();
                int haj = Convert.ToInt16(LabelNummer.Text);
                FrageNummer = haj + 1;
                LasInFraga();
            }       
        }

        protected void BtnStartatest_Click(object sender, EventArgs e)
        {           
            RadioButtonList1.Visible = true;
            //BtnForegaende.Visible = true;
            BtnNasta.Visible = true;
            LabelFraga.Visible = true;
            LabelNummer.Visible = true;
            BtnStartatest.Visible = false;
            //BtnRatta.Visible = true;
            lblRubrik.Visible = false;
            FrageNummer = 1;
            LasInFraga();
        }

        protected void BtnRatta_Click(object sender, EventArgs e)
        {
            rattaProv();
            raderaXMLspara();
            BtnRatta.Visible = false;           
        }

    }
}