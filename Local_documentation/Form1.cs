using HtmlAgilityPack;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Local_documentation
{
    public partial class Form1 : Form
    {

        string diry = Settings1.Default.folderpath + "\\LD_Website";
        List<TextBox> textBoxList = new List<TextBox>();
        List<string> content_List = new List<string>();
        string selectedPage = string.Empty;

        public Form1()
        {
            InitializeComponent();
            //change the design of buttons on top
            newButton.BackColor = Color.Orange;
            newButton.FlatStyle = FlatStyle.Flat;
            newButton.FlatAppearance.BorderSize = 0;
            saveButton.BackColor = Color.Orange;
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.FlatAppearance.BorderSize = 0;
            removebutton.BackColor = Color.Red;
            removebutton.FlatStyle = FlatStyle.Flat;
            removebutton.FlatAppearance.BorderSize = 0;
            SettingsButton.BackColor = Color.Orange;
            SettingsButton.FlatStyle = FlatStyle.Flat;
            SettingsButton.FlatAppearance.BorderSize = 0;
            // change the size of the drop down menu
            pageList.DropDownHeight = 500;


        }

        // "New" button click - creates new page
        private void New_Button_Click(object sender, EventArgs e)
        {
            //dialog function to create a new page
            static DialogResult InputBox(string title, string promptText, ref string value)
            {
                Form form = new Form();
                Label label = new Label();
                TextBox textBox = new TextBox();
                Button buttonOk = new Button();
                Button buttonCancel = new Button();
                form.Text = "create new page";
                label.Text = "What is the new page about?";
                buttonOk.Text = "OK";
                buttonCancel.Text = "Cancel";
                buttonOk.DialogResult = DialogResult.OK;
                buttonCancel.DialogResult = DialogResult.Cancel;
                label.SetBounds(36, 36, 372, 13);
                textBox.SetBounds(36, 86, 700, 20);
                buttonOk.SetBounds(228, 160, 160, 60);
                buttonCancel.SetBounds(400, 160, 160, 60);
                label.AutoSize = true;
                form.ClientSize = new Size(796, 307);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
                form.AcceptButton = buttonOk;
                form.CancelButton = buttonCancel;
                DialogResult dialogResult = form.ShowDialog();
                value = textBox.Text;
                return dialogResult;
            }
            // get return value from the dialog to create a new page
            string value = "";
            if (InputBox("Dialog Box", "What is your name?", ref value) == DialogResult.OK) { }

            if (value.Trim() != "")
            {
                //create html document and save it in page folder
                var doc = new HtmlAgilityPack.HtmlDocument();
                var node = HtmlNode.CreateNode("<html><head><link rel=\"stylesheet\" href=\"..\\design.css\"></head><body>" +
                    "<a href=\"..\\index.html\"><h1>" + Settings1.Default.title + "</h1></a><h2 id=\"title\">" + value + "</h2><ul id=\"menu\"></ul>" +
                    "<div id=\"content\"><h3 id=\"i1\">title</h3><p id=\"i1c\">Content</p></div></body></html>");
                doc.DocumentNode.AppendChild(node);
                FileStream sw = new FileStream(Settings1.Default.folderpath + "\\LD_Website" + "\\pages\\" + value.Replace(" ", "_") + ".html", FileMode.Create);
                doc.Save(sw);
                sw.Close();
                pageList.Items.Add(value);

                // add new page to the link list in index page
                string diryind = Settings1.Default.folderpath + "\\LD_Website";
                diryind += "\\index.html";
                var docind = new HtmlAgilityPack.HtmlDocument();
                docind.Load(diryind);
                var linktopage = docind.DocumentNode.SelectSingleNode("//body/ul");
                linktopage.AppendChild(HtmlNode.CreateNode("<li><a href=\"./pages/" + value.Replace(" ", "_") + ".html\">" + value + "</a></li>"));
                FileStream sww = new FileStream(diryind, FileMode.Create);
                docind.Save(sww);
                sww.Close();
            }


        }

        //"save" button - save the change made for pages
        private void Save_Button_Click(object sender, EventArgs e)
        {
            if(selectedPage != string.Empty)
            {
                //creates a whole new page and save it
                var doc = new HtmlAgilityPack.HtmlDocument();
                var node = HtmlNode.CreateNode("<html><head><link rel=\"stylesheet\" href=\"..\\design.css\"></head><body>" +
                    "<a href=\"..\\index.html\"><h1>" + Settings1.Default.title + "</h1></a><h2 id=\"title\">" + selectedPage + "</h2><ul id=\"menu\"></ul>" +
                    "<div id=\"content\"></div></body></html>");
                doc.DocumentNode.AppendChild(node);
                int pagenum = 0;
                // take each textbox and apend to the html document created above
                foreach (TextBox tb in textBoxList)
                {
                    var htmlBody = doc.DocumentNode.SelectSingleNode("//body/div");
                    var htmlmenu = doc.DocumentNode.SelectSingleNode("//body/ul");
                    if (pagenum % 2 == 0)
                    {
                        htmlBody.AppendChild(HtmlNode.CreateNode("<h3 id=\"i" + ((pagenum / 2) + 1).ToString() + "\">" + tb.Text + "</h3>"));
                        htmlmenu.AppendChild(HtmlNode.CreateNode("<li><a href=\"#i" + ((pagenum / 2) + 1).ToString() + "\">" + tb.Text + "</a></li>"));
                    }
                    else { htmlBody.AppendChild(HtmlNode.CreateNode("<p id=\"i" + ((pagenum / 2) + 1).ToString() + "c\">" + tb.Text.Replace("\r\n", "<br>").Replace("\n", "<br>").Replace("\r", "<br>") + "</p>")); }
                    pagenum++;
                }
                FileStream sw = new FileStream(Settings1.Default.folderpath + "\\LD_Website" + "\\pages\\" + selectedPage.Replace(" ", "_") + ".html", FileMode.Create);
                doc.Save(sw);
                sw.Close();
            }

        }

        // when an item gets selected from the drop down list
        // get contents from the html page and display 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            // get the value of the selected item
            ComboBox cmb = (ComboBox)sender;
            selectedPage = cmb.Text;
            showcontents(selectedPage);


            // parse through the html docuement
            void showcontents(string sp)
            {
                // clear the page first
                pageContents.Controls.Clear();
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.Load(Settings1.Default.folderpath + "\\LD_Website" + "\\pages\\" + sp.ToString().Replace(" ", "_") + ".html");
                int getItems = 1;
                textBoxList.Clear();
                while (doc?.GetElementbyId("i" + getItems.ToString())?.InnerText != null)
                {
                    textBoxList.Add(new TextBox());
                    textBoxList.Last().Text = doc.GetElementbyId("i" + getItems.ToString()).InnerText;
                    textBoxList.Add(new TextBox());
                    textBoxList.Last().Text = doc.GetElementbyId("i" + getItems.ToString() + "c").InnerHtml.Replace("<br>","\r\n");
                    getItems++;
                }
                // creates textbox for each element in the html docuemnt
                for (int i = 0; i < textBoxList.Count; i++)
                {
                    textBoxList[i].Font = new Font(TextBox.DefaultFont.FontFamily, TextBox.DefaultFont.Size * 1.5f);
                    if (i % 2 == 0)
                    {
                        textBoxList[i].Location = new Point(50, 150 * i + 50); textBoxList[i].Width = 500;
       
                        pageContents.Controls.Add(createdeleteButton(i));
                    }
                    else
                    {
                        textBoxList[i].Multiline = true; textBoxList[i].Height = 200; textBoxList[i].Width = 900;
                     
                        textBoxList[i].ScrollBars = ScrollBars.Vertical; textBoxList[i].Location = new Point(50, 150 * (i - 1) + 100);
                    }
                    pageContents.Controls.Add(textBoxList[i]);
                }
                //add button to add more item for the html page
                Button addButton = new Button();
                addButton.Size = new(87, 45);
                addButton.Text = "Add item";
                addButton.UseVisualStyleBackColor = false;
                addButton.TabIndex = 0;
                addButton.Location = new Point(850, textBoxList.Count * 150 + 20);
                addButton.Margin = new Padding(0);
                addButton.Name = "addButton";
                addButton.BackColor = Color.Orange;
                addButton.FlatStyle = FlatStyle.Flat;
                addButton.FlatAppearance.BorderSize = 0;
                pageContents.Controls.Add(addButton);
                addButton.Click += addButtonClick;

            }



            void addButtonClick(object sender, EventArgs e)
            {
                int amountlist = textBoxList.Count;
                textBoxList.Add(new TextBox());
                textBoxList.Add(new TextBox());
                for (int i = amountlist; i < textBoxList.Count; i++)
                {
                    textBoxList[i].Font = new Font(TextBox.DefaultFont.FontFamily, TextBox.DefaultFont.Size * 1.5f);
                    if (i % 2 == 0)
                    {
                        textBoxList[i].Text = "Title"; textBoxList[i].Location = new Point(50, 150 * i + 50); textBoxList[i].Width = 500;
                        pageContents.VerticalScroll.Value = 0;
                        pageContents.Controls.Add(createdeleteButton(i));
                    }
                    else
                    {
                        textBoxList[i].Text = "Contents"; textBoxList[i].Multiline = true; textBoxList[i].Height = 200; textBoxList[i].Width = 900;
                        textBoxList[i].ScrollBars = ScrollBars.Vertical; textBoxList[i].Location = new Point(50, 150 * (i - 1) + 100);
                    }
                    pageContents.VerticalScroll.Value = 0;
                    pageContents.Controls.Add(textBoxList[i]);

                }
                pageContents.VerticalScroll.Value = 0;
                Button bt = sender as Button;
                bt.Location = new Point(850, textBoxList.Count * 150 + 20);
            }

            //delete button
            Button createdeleteButton(int x)
            {
                Button deleteButton = new Button();
                deleteButton.Size = new(87, 35);
                deleteButton.Text = "delete item";
                deleteButton.UseVisualStyleBackColor = false;
                deleteButton.TabIndex = 0;
                deleteButton.Location = new Point(850, x * 150 + 50);
                deleteButton.Margin = new Padding(0);
                deleteButton.Name = "addButton";
                deleteButton.BackColor = Color.Red;
                deleteButton.FlatStyle = FlatStyle.Flat;
                deleteButton.FlatAppearance.BorderSize = 0;
                deleteButton.Click += (sender, e) => deleteButtonClick(sender, e, x); ;
                return deleteButton;
            }

            void deleteButtonClick(object sender, EventArgs e, int x)
            {
                static DialogResult InputBox(string title, string promptText)
                {
                    Form form = new Form();
                    Label label = new Label();

                    Button buttonOk = new Button();
                    Button buttonCancel = new Button();
                    form.Text = "Delete item";
                    label.Text = "Are you sure you want to delete this item?";
                    buttonOk.Text = "Yes";
                    buttonCancel.Text = "No";
                    buttonOk.DialogResult = DialogResult.OK;
                    buttonCancel.DialogResult = DialogResult.Cancel;
                    label.SetBounds(36, 36, 372, 13);
                    buttonOk.SetBounds(228, 160, 160, 60);
                    buttonCancel.SetBounds(400, 160, 160, 60);
                    label.AutoSize = true;
                    form.ClientSize = new Size(796, 307);
                    form.FormBorderStyle = FormBorderStyle.FixedDialog;
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.MinimizeBox = false;
                    form.MaximizeBox = false;
                    form.Controls.AddRange(new Control[] { label, buttonOk, buttonCancel });
                    form.AcceptButton = buttonOk;
                    form.CancelButton = buttonCancel;
                    DialogResult dialogResult = form.ShowDialog();

                    return dialogResult;
                }
                // get return value from the dialog to create a new page

                if (InputBox("Dialog Box", "What is your name?") == DialogResult.OK)
                {
                    textBoxList.RemoveAt(x);
                    textBoxList.RemoveAt(x);
                    Save_Button_Click(sender, e);
                    showcontents(selectedPage);
                }


            }

        }

        private void startnewform()
        {
            //initializing drop down menu
            string diry = Settings1.Default.folderpath + "\\LD_Website";
            string[] fileEntries;
            diry += "\\pages";
            try
            {
                fileEntries = Directory.GetFiles(diry);
                webnamelabel.Text = Settings1.Default.title;
            }
            catch (Exception ex)
            {
                fileEntries = new string[1] { " " };
                webnamelabel.Text = "create your website or select folder from settings";
                webnamelabel.ForeColor = Color.Red;
            }

            List<string> page_List = new List<string>();
            foreach (string filename in fileEntries)
            {
                page_List.Add(filename.Split('\\').Last().Split('.').First());
            }
            pageList.Sorted = true;
            foreach (string page in page_List)
            {
                pageList.Items.Add(page.Replace("_", " "));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            startnewform();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var settingsmenu = new Form2();
            settingsmenu.ShowDialog();
            string diry = Settings1.Default.folderpath + "\\LD_Website";
            diry += "\\pages";
            pageList.Items.Clear();
            pageContents.Controls.Clear();

            selectedPage = "";
            pageList.SelectedIndex = -1;
            try
            {
                webnamelabel.Text = Settings1.Default.title;
                webnamelabel.ForeColor = Color.White;
                startnewform();
            }
            catch (Exception ex)
            {
               
                webnamelabel.Text = "create your website or select folder from settings";
                webnamelabel.ForeColor = Color.Red;
            }


        }

        private void removebutton_Click(object sender, EventArgs e)
        {

            static DialogResult removeconfirmbox(string title, string promptText, string pagename)
            {
                Form form = new Form();
                Label label = new Label();
                Button buttonOk = new Button();
                Button buttonCancel = new Button();
                form.Text = "Removing an item";
                label.Text = "Are you sure you want to remove - " + pagename +" - ?";
                buttonOk.Text = "OK";
                buttonCancel.Text = "Cancel";
                buttonOk.DialogResult = DialogResult.OK;
                buttonCancel.DialogResult = DialogResult.Cancel;
                label.SetBounds(36, 36, 372, 13);
                buttonOk.SetBounds(228, 160, 160, 60);
                buttonCancel.SetBounds(400, 160, 160, 60);
                label.AutoSize = true;
                form.ClientSize = new Size(796, 307);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.Controls.AddRange(new Control[] { label, buttonOk, buttonCancel });
                form.AcceptButton = buttonOk;
                form.CancelButton = buttonCancel;
                DialogResult dialogResult = form.ShowDialog();
                return dialogResult;
            }
            if (removeconfirmbox("Dialog Box", "Are you sure you want to remove this item?", selectedPage) == DialogResult.OK) {
                var doc = new HtmlAgilityPack.HtmlDocument();

                doc.Load(Settings1.Default.folderpath + "\\LD_Website\\index.html");
                var liNode = doc.DocumentNode.SelectSingleNode("//li[a[@href='./pages/" + selectedPage.Replace(" ", "_") + ".html']]");
                liNode.Remove();
                FileStream sww = new FileStream(Settings1.Default.folderpath + "\\LD_Website\\index.html", FileMode.Create);
                doc.Save(sww);
                sww.Close();
                File.Delete(Settings1.Default.folderpath + "\\LD_Website\\pages\\" + selectedPage.Replace(" ", "_") + ".html");
                pageList.Items.Clear();
                pageContents.Controls.Clear();
                startnewform();
                selectedPage = "";
                pageList.SelectedIndex = -1;
            }

        }
    }
}
