using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using HtmlAgilityPack;

namespace Local_documentation
{
    public partial class Form2 : Form
    {
        Dictionary<string, RadioButton> buttonsdict = new Dictionary<string, RadioButton>();



        public Form2()
        {
            InitializeComponent();
            buttonsdict.Add("Red", radioButton1);
            buttonsdict.Add("Green", radioButton2);
            buttonsdict.Add("blue", radioButton3);
            buttonsdict.Add("Black", radioButton4);
            buttonsdict.Add("Yellow", radioButton5);
            buttonsdict.Add("Gray", radioButton6);
            buttonsdict.Add("Pink", radioButton7);
            buttonsdict.Add("Orange", radioButton8);
            buttonsdict.Add("Purple", radioButton9);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings1.Default.title = titletextbox.Text;
            Settings1.Default.themecolor = buttonsdict.Where(x => x.Value.Checked == true).Select(x => x.Key).First();
            Settings1.Default.Save();
            string cssContent = "h1{color:" + Settings1.Default.themecolor + ";font-style: italic;font-size:3em;} h2{color:Black;font-size:2em;} a{text-decoration: none;color:" + Settings1.Default.themecolor + ";} a:hover {font-weight:bold;} li{line-height:1.5em;} p{margin-bottom:50px;margin-left:0px;line-height:2em;color:gray;} *{font-family: Arial, Helvetica, sans-serif;} img{width:700px;}body{margin:30px;} h3 {border-bottom: solid 3px gray;position: relative;margin-bottom:-0.5em;} h3:after {position: absolute;content: \" \";display: block;border-bottom: solid 3px " + Settings1.Default.themecolor + " ;bottom: -3px;width: 10%;}";
            try
            {
                File.WriteAllText(Settings1.Default.folderpath + "\\LD_Website\\design.css", cssContent);
            }
            catch (Exception w) { }
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            folderpathlabel.Text = Settings1.Default.folderpath;
            titletextbox.Text = Settings1.Default.title;
            buttonsdict[Settings1.Default.themecolor].Checked = true;
            webtitlelabel.Text = Settings1.Default.title;
            messagelabel.Visible = false;
        }

        // change file path for a website
        private void button4_Click(object sender, EventArgs e)
        {

            folderBrowserDialog1.ShowDialog();
            Settings1.Default.folderpath = folderBrowserDialog1.SelectedPath;
            folderpathlabel.Text = Settings1.Default.folderpath;
            var doc = new HtmlAgilityPack.HtmlDocument();
            try
            {

                doc.Load(Settings1.Default.folderpath + "\\LD_Website\\index.html");
                var h1Node = doc.DocumentNode.SelectSingleNode("//h1");
                if (h1Node != null)
                {

                    string h1Text = h1Node.InnerText;
                    Settings1.Default.title = h1Text;
                    Settings1.Default.Save();
                    webtitlelabel.Text = Settings1.Default.title;
                }
                else
                {
                    webtitlelabel.Text = "Could not find a website!!";
                }
            }
            catch (Exception ex)
            {
                webtitlelabel.Text = "Could not find a website";
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(newwebfolderpath.Text == string.Empty)
            {
                messagelabel.Text = "chose folder path to the new website";
                messagelabel.ForeColor = Color.Red;
                messagelabel.Visible = true;
            }
            else
            {
                Settings1.Default.themecolor = buttonsdict.Where(x => x.Value.Checked == true).Select(x => x.Key).First();
                Settings1.Default.title = titletextbox.Text;
                System.IO.Directory.CreateDirectory(Settings1.Default.folderpath + "\\LD_Website");
                System.IO.Directory.CreateDirectory(Settings1.Default.folderpath + "\\LD_Website\\pages");
                var doc = new HtmlAgilityPack.HtmlDocument();
                var node = HtmlNode.CreateNode("<html><head><link rel=\"stylesheet\" href=design.css></head>" +
                    "<body>" +
                    "<h1>" + Settings1.Default.title + "</h1>" +
                    "<h2 id=\"title\">Items</h2>" +
                    "<ul id = \"menu\" ></ul> " +
                    "</body></html>");
                doc.DocumentNode.AppendChild(node);
                FileStream sw = new FileStream(Settings1.Default.folderpath + "\\LD_Website\\index.html", FileMode.Create);
                doc.Save(sw);
                sw.Close();
                string cssContent = "h1{color:" + Settings1.Default.themecolor + ";font-style: italic;font-size:3em;} h2{color:Black;font-size:2em;} a{text-decoration: none;color:" + Settings1.Default.themecolor + ";} a:hover {font-weight:bold;} li{line-height:1.5em;} p{margin-bottom:50px;margin-left:0px;line-height:2em;color:gray;} *{font-family: Arial, Helvetica, sans-serif;} img{width:700px;}body{margin:30px;} h3 {border-bottom: solid 3px gray;position: relative;margin-bottom:-0.5em;} h3:after {position: absolute;content: \" \";display: block;border-bottom: solid 3px " + Settings1.Default.themecolor + " ;bottom: -3px;width: 10%;}";
                File.WriteAllText(Settings1.Default.folderpath + "\\LD_Website\\design.css", cssContent);
                Settings1.Default.Save();
                titletextbox.Text = Settings1.Default.title;
                webtitlelabel.Text = Settings1.Default.title;
                messagelabel.ForeColor = Color.Lime;
                messagelabel.Text = "website created";
                messagelabel.Visible = true;
                folderpathlabel.Text = Settings1.Default.folderpath;
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            Settings1.Default.folderpath = folderBrowserDialog1.SelectedPath;
            newwebfolderpath.Text = Settings1.Default.folderpath;
        }

    }
}
