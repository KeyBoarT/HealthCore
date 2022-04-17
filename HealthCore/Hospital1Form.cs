using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenQA.Selenium;

namespace HealthCore
{
    public partial class Hospital1Form : Form
    {
        int guncelx = 25;
        int guncely = 20;
        public List<Button> Butonlar = new List<Button>();

        public Hospital1Form()
        {
            InitializeComponent();
        }

        private void Hospital1Form_Load(object sender, EventArgs e)
        {
            panel2.Hide();
            panel1.Dock = DockStyle.Fill;
            panel2.Dock = DockStyle.Fill;
            bool inilsinMi = false;
            for (int i = 0; i < Acibadem.Basliklar.Count; i++)
            {
                CommentOlustur(guncelx, guncely, Acibadem.Basliklar[i].Text);
                guncelx += 400;
                if (inilsinMi)
                {
                    guncely += 220;
                    inilsinMi = false;
                    guncelx = 25;
                }
                else { inilsinMi = true; }
            }

            for (int i = 0; i < Butonlar.Count; i++)
            {
                Butonlar[i].Click += new System.EventHandler(commonButtonClick);
            }
        }

        public void CommentOlustur(int x, int y, string baslik)
        {
            Panel panel = new Panel
            {
                Size = new Size(300, 200),
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(x, y)
            };

            Label label1 = new Label
            {
                Size = new Size(300, 40),
                Text = baslik,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Button buton = new Button
            {
                Size = new Size(100, 40),
                Text = "SİTEYE GİT",
                Font = new Font("Potta One", 12, FontStyle.Bold),
                Dock = DockStyle.Bottom
            };
            buton.FlatAppearance.BorderSize = 0;
            buton.FlatStyle = FlatStyle.Flat;
            buton.TextAlign = ContentAlignment.MiddleCenter;
            Butonlar.Add(buton);

            PictureBox pcBox = new PictureBox
            {
                Image = Properties.Resources.HealthCore_removebg_preview__3_,
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill
            };
            panel1.Controls.Add(panel);
            panel.Controls.Add(label1);
            panel.Controls.Add(buton);
            panel.Controls.Add(pcBox);
        }

        public void commonButtonClick(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            int guncely = 0;
            for (int i = 0; i < Butonlar.Count; i++)
            {
                if (sender.Equals(Butonlar[i]))
                {
                    commentTopicLabel.Text = Acibadem.Basliklar[i].Text;
                    Acibadem.Basliklar[i].Click();
                    List<Control> controls = new List<Control>();
                    IWebElement element = Acibadem.driver.FindElement(By.Id("general"));
                    var childements = element.FindElements(By.XPath(".//*"));
                    foreach (IWebElement item in childements)
                    {
                        if (item.TagName == "p")
                        {
                            Label label = new Label
                            {
                                Text = item.Text,
                                MaximumSize = new Size(740, 0),
                                Location = new Point(0, guncely),
                                AutoSize = true,
                                Font = new Font("Arial", 12, FontStyle.Bold),
                                ForeColor = Color.White
                            };
                            contentPanel.Controls.Add(label);
                            label.Visible = true;
                            guncely += label.Height;
                            controls.Add(label);
                        }
                        if (item.TagName == "h3")
                        {
                            Label label = new Label
                            {
                                Text = item.Text,
                                MaximumSize = new Size(740, 0),
                                AutoSize = true,
                                Location = new Point(0, guncely),
                                Font = new Font("Potta One", 24, FontStyle.Bold),
                                ForeColor = Color.Red
                            };
                            Controls.Add(label);
                            contentPanel.Controls.Add(label);
                            label.Visible = true;
                            guncely += label.Height;
                        }
                        

                    }
                    panel1.Hide();
                    panel2.Show();
                }
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Acibadem.driver.Navigate().GoToUrl("https://www.acibadem.com.tr/ilgi-alanlari/");
            Acibadem.BasliklariYenile();
            panel2.Hide();
            panel1.Show();
        }
    }
}
