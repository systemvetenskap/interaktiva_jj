using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
//HEHEJJEFJHKSDFHJKSDEFsdfgdsgsdfgsdfgsdf

namespace Uppgift4
{
    public partial class Inloggad : System.Web.UI.Page
    {//hejhej
        
        string XMLFragorna;       
        XmlNodeList aktieNodeListFragor;
        XmlDocument docFra = new XmlDocument();
        List<FrageKlass> frageLista = new List<FrageKlass>();
        List<Anvandare> anvandareLista = new List<Anvandare>();
        int FrageNummer;
        bool licens;
        string licensiering = "";
        //för att rättafdsfsd
        XmlNodeList XMLFragorMetodRatta;
        XmlNodeList XMLSvarMetodRatta;
        List<SvarFragor> SvarLista = new List<SvarFragor>();
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    LabelFraga.Text = ": " + frageLista[FrageNummer].fragan;
                    RadioButtonList1.Items.FindByValue("A").Text = frageLista[FrageNummer].a;
                    RadioButtonList1.Items.FindByValue("B").Text = frageLista[FrageNummer].b;
                    RadioButtonList1.Items.FindByValue("C").Text = frageLista[FrageNummer].c;
                    lblKategori.Text = frageLista[FrageNummer].Kategori;
                }
                else if (FrageNummer == 4)
                {
                    LabelFraga.Text = "Slut på frågor";
                    BtnNasta.Visible = false;
                    LabelNummer.Visible = false;
                    RadioButtonList1.Visible = false;
                }            
            }
         
            labelnummer++;

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

            if (licens == false)
            {
                xmlElement.SetAttribute("Kategori", "licens");
            }
            else
            {
                xmlElement.SetAttribute("Kategori", "kunskap");
            }
            //inseet
            docX.DocumentElement.InsertBefore(xmlElement, xmlNode);
            docX.Save(Server.MapPath("XMLspara.xml"));

            //TextBox1.Text = "";
            RadioButtonList1.SelectedIndex = 0;
            BindData();
        }

        private void sparaAnvandareXml()
        {
            //fixa så rätt nummer 
            //int haj = Convert.ToInt16(LabelNummer.Text);
            //string hej = LabelNummer.Text;

            //öppnar, dumt att inte göra/använda samma som tidigare?strul att använda samma?
            System.Xml.XmlDocument docX = new System.Xml.XmlDocument();
            docX.Load(Server.MapPath("XMLAnvandare.xml"));
            System.Xml.XmlNode xmlNode = docX.DocumentElement.FirstChild;
            //skapa fixa så skapas klass sen

            System.Xml.XmlElement xmlElement = docX.CreateElement("Anvanadre");
            xmlElement.SetAttribute("anvandarnamn", Server.HtmlEncode(Anvandare.anvandarnamn));
            xmlElement.SetAttribute("losenord", Server.HtmlEncode(Anvandare.losenord));
            xmlElement.SetAttribute("licensiering", licensiering);

            //inseet
            docX.DocumentElement.InsertBefore(xmlElement, xmlNode);
            docX.Save(Server.MapPath("XMLAnvandare.xml"));

            //TextBox1.Text = "";
            RadioButtonList1.SelectedIndex = 0;
            BindData();
        }
        void BindData()
        {
            XmlTextReader XmlReader = new XmlTextReader(Server.MapPath("XMLspara.xml"));
            XmlReader.Close();
        }

        private void andrabehorighet()
        {

            XMLFragorna = Server.MapPath("XMLAnvandare.xml");
            aktieNodeListFragor = docFra.SelectNodes("/Allaanvandare/Anvandare");
            docFra.Load(XMLFragorna);

            foreach (XmlNode nod in aktieNodeListFragor)
            {
                //Anvandare anv = new Anvandare();
                //anv.anvandarnamn = nod["anvandarnamn"].InnerText;
                //anv.licensiering = nod["licensiering"].InnerText;

                //if(Anvandare.anvandarnamn == "1") //hårdkodat
                //{
                //    Anvandare.licensiering = "ja";
                //    licensiering = Anvandare.licensiering;
                //    sparaAnvandareXml();
                //}
            }
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
            //öppnar, dumt att inte göra/använda samma som tidigare?strul att använda samma?
            System.Xml.XmlDocument docX = new System.Xml.XmlDocument();
            docX.Load(Server.MapPath("XMLspara.xml"));
            System.Xml.XmlNode xmlNode = docX.DocumentElement.FirstChild;
            //skapa fixa så skapas klass sen

            //inseet
       
            //förstaListan
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
                frageLista.Add(fraga);
            }
            GridView2.DataSource = frageLista;
            GridView2.DataBind();

            string metodRattaMinaSvar;
            metodRattaMinaSvar = Server.MapPath("XMLspara.xml");
            XMLSvarMetodRatta = docFra.SelectNodes("/sparaTest/fraga");
            docFra.Load(metodRattaMinaSvar);
            int antalrattSvar = 0;
            int i = 0;
            

            foreach (XmlNode nod in XMLSvarMetodRatta)
            {
                SvarFragor svar = new SvarFragor();
                svar.nummer = nod.Attributes["nummer"].Value;
                svar.namn = nod.Attributes["namn"].Value;
                svar.mittSvar = nod.Attributes["mittSvar"].Value;
                svar.Kategori = nod.Attributes["Kategori"].Value;

                if (svar.namn == Anvandare.anvandarnamn)
                {
                    SvarLista.Add(svar);
                }
            }



            foreach (Object svar in SvarLista)
            {
                frageLista.Reverse();
                if (SvarLista[i].nummer == frageLista[i].nummer)
                {
                    if (SvarLista[i].mittSvar == frageLista[i].RattSvar)
                    {
                        System.Xml.XmlElement xmlElement = docX.CreateElement("svar");
                        xmlElement.SetAttribute("nummer", i.ToString());
                        xmlElement.SetAttribute("namn", Server.HtmlEncode(Anvandare.anvandarnamn));
                        xmlElement.SetAttribute("mittSvar", i.ToString());
                        xmlElement.SetAttribute("ratt", "ja");
                        
                        if (licens == false)
                        {
                            xmlElement.SetAttribute("Kategori", "licens");
                        }
                        else
                        {
                            xmlElement.SetAttribute("Kategori", "kunskap");
                        }

                            docX.DocumentElement.InsertBefore(xmlElement, xmlNode);
                            docX.Save(Server.MapPath("XMLspara.xml"));
                            antalrattSvar++;
                        
                    }
                    if (SvarLista[i].mittSvar != frageLista[i].RattSvar)
                    {
                        System.Xml.XmlElement xmlElement = docX.CreateElement("svar");
                        xmlElement.SetAttribute("nummer", i.ToString());
                        xmlElement.SetAttribute("namn", Server.HtmlEncode(Anvandare.anvandarnamn));
                        xmlElement.SetAttribute("mittSvar", i.ToString());
                        xmlElement.SetAttribute("ratt", "nej");
                        if (licens == false)
                        {
                            xmlElement.SetAttribute("Kategori", "licens");
                        }
                        else
                        {
                            xmlElement.SetAttribute("Kategori", "kunskap");
                        }
                        docX.DocumentElement.InsertBefore(xmlElement, xmlNode);
                        docX.Save(Server.MapPath("XMLspara.xml"));
                    }         
                    i++;
                }
                lblKategori.Text = "Antal rätt svar: " + antalrattSvar.ToString();
                GridView3.DataSource = SvarLista;
                GridView3.DataBind();
                
            }
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
            BtnRatta.Visible = true;
            lblRubrik.Visible = false;
            FrageNummer = 1;
            LasInFraga();
        }

        protected void BtnRatta_Click(object sender, EventArgs e)
        {
            rattaProv();
            //andrabehorighet();
        }
    }
}