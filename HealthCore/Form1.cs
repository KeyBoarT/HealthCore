using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HealthCore
{
    public partial class HealthCore : Form
    {
        public HealthCore()
        {
            InitializeComponent();
            CustomizeDesign();
        }

        private void HealthCore_Load(object sender, EventArgs e)
        {

        }

        private void HealthCore_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (activeDriver != null)
            {
                activeDriver.Quit();
            }
        }

        private void CustomizeDesign()
        {
            panelDeveloperInfos.Visible = false;
            panelHospital1.Visible = false;
            panelHospital2.Visible = false;
            panelHospital3.Visible = false;
        }

        private void hideHospitalMenu()
        {
            if (panelDeveloperInfos.Visible == true)
                panelDeveloperInfos.Visible = false;
            if (panelHospital1.Visible == true)
                panelHospital1.Visible = false;
            if (panelHospital2.Visible == true)
                panelHospital2.Visible = false;
            if (panelHospital3.Visible == true)
                panelHospital3.Visible = false;
        }

        private void showHospitalMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideHospitalMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        #region childForm
        private Form activeForm = null;
        private IWebDriver activeDriver = null;
        private void openChildForm(Form childForm, IWebDriver childDriver)
        {
            if (activeForm != null)
                activeForm.Close();
            if (activeDriver != null)
                activeDriver.Quit();
            activeForm = childForm;
            activeDriver = childDriver;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        #endregion

        #region Hospital1
        private void btnHospital1_Click(object sender, EventArgs e)
        {
            showHospitalMenu(panelHospital1);
        }

        private void btnKalpHospital1_Click(object sender, EventArgs e)
        {
            hideHospitalMenu();
            Acibadem acibadem = new Acibadem();
            Acibadem.BasliklariBul("Kalp");
            Hospital1Form childform = new Hospital1Form();
            openChildForm(childform, Acibadem.driver);
        }

        private void btnGozHospital1_Click(object sender, EventArgs e)
        {
            hideHospitalMenu();
        }

        private void btnBeyinHospital1_Click(object sender, EventArgs e)
        {
            hideHospitalMenu();
        }
        #endregion

        #region Hospital2
        private void btnHospital2_Click(object sender, EventArgs e)
        {
            showHospitalMenu(panelHospital2);
        }

        private void btnKalpHospital2_Click(object sender, EventArgs e)
        {
            hideHospitalMenu();
            Medipol medipol = new Medipol();
            string[] aranacaklar = { "Kalp", "Kardiyo" };
            medipol.AnaBasliklariBul(aranacaklar);
            Hospital2Form childform = new Hospital2Form();
            childform.comboBox1.Items.AddRange(Medipol.ToString(medipol.AnaBasliklar));
            openChildForm(childform, Medipol.driver);
        }

        private void btnGozHospital2_Click(object sender, EventArgs e)
        {
            hideHospitalMenu();
        }

        private void btnBeyinHospital2_Click(object sender, EventArgs e)
        {
            hideHospitalMenu();
        }
        #endregion

        #region Hospital3
        private void btnHospital3_Click(object sender, EventArgs e)
        {showHospitalMenu(panelHospital3);
        }

        private void btnKalpHospital3_Click(object sender, EventArgs e)
        {hideHospitalMenu();
        }

        private void btnGozHospital3_Click(object sender, EventArgs e)
        {hideHospitalMenu();
        }

        private void btnBeyinHospital3_Click(object sender, EventArgs e)
        {hideHospitalMenu();
        }
        #endregion

        #region DeveloperInfos
        private void btnDeveloperInfos_Click(object sender, EventArgs e)
        {
            showHospitalMenu(panelDeveloperInfos);
        }

        private void btnDeveloperGitHub_Click(object sender, EventArgs e)
        {
            DeveloperInfosForm form = new DeveloperInfosForm();
            form.webBrowser1.Navigate(DeveloperInfos.GitHubAddress);
            openChildForm(form);
            hideHospitalMenu();
        }

        private void btnDeveloperInstagram_Click(object sender, EventArgs e)
        {
            DeveloperInfosForm form = new DeveloperInfosForm();
            form.webBrowser1.Navigate(DeveloperInfos.InstagramAddress);
            openChildForm(form);
            hideHospitalMenu();
        }

        private void btnDeveloperStackOF_Click(object sender, EventArgs e)
        {
            DeveloperInfosForm form = new DeveloperInfosForm();
            form.webBrowser1.Navigate(DeveloperInfos.StackOFAddress);
            openChildForm(form);
            hideHospitalMenu();
        }
        #endregion
    }
}
