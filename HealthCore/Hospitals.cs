using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Windows.Forms;

namespace HealthCore
{
    class Acibadem
    {
        static string url = "https://www.acibadem.com.tr/ilgi-alanlari/";
        public static IWebDriver driver;
        static int harfSayisi = 27;
        public static List<IWebElement> Basliklar = new List<IWebElement>();
        public static List<string> Locations = new List<string>();
        public Acibadem()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
        }
        public static void BasliklariBul(string aranacak)
        {
            try
            {
                driver.FindElement(By.XPath("/html/body/div/div/div/div/div/button[2]")).Click();
            }
            catch { };
            for (int i = 1; i <= harfSayisi; i++)
            {
                int baslikSayaci = 1;

                while (true)
                {
                    try
                    {
                        string location = $"/html/body/section[3]/div/main/article/section/ul[{i}]/li[{baslikSayaci}]/a";
                        IWebElement baslik = driver.FindElement(By.XPath(location));

                        if (baslik.Text.Contains(aranacak))
                        {
                            Basliklar.Add(baslik);
                            Locations.Add(location);
                        }
                        baslikSayaci++;
                    }
                    catch { break; }
                }
            }
        }

        public static void BasliklariYenile()
        {
            List<IWebElement> yedekBasliklar = new List<IWebElement>();
            foreach (IWebElement item in Basliklar) { yedekBasliklar.Add(item); }
            Basliklar.Clear();
            for (int i = 0; i < yedekBasliklar.Count; i++)
            {
                IWebElement element = Acibadem.driver.FindElement(By.XPath(Locations[i]));
                Basliklar.Add(element);
            }
        }
    }

    class Medipol
    {
        string url = "https://www.medicalpark.com.tr/saglik-rehberi";
        public static IWebDriver driver;
        public List<IWebElement> AnaBasliklar = new List<IWebElement>();
        int BaslikSayisi = 49;
        public Medipol()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
        }
        public void AnaBasliklariBul(string[] aranacaklar)
        {
            driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div/section[4]/div/div/div[1]/div/div[1]/div/div[1]/span/span[1]/span/span[1]")).Click();
            for (int i = 1; i <= BaslikSayisi; i++)
            {
                try
                {
                    IWebElement baslik = driver.FindElement(By.XPath($"/html/body/span/span/span[2]/ul/li[{i}]"));
                    for (int x = 0; x < aranacaklar.Length; x++)
                    {
                        if (baslik.Text.Contains(aranacaklar[x]))
                        {
                            AnaBasliklar.Add(baslik);
                        }
                    }
                }catch { }
            }
        }

        public static object[] ToString(List<IWebElement> liste)
        {
            List<object> yeniListe = new List<object>();

            foreach (IWebElement element in liste)
                yeniListe.Add(element.Text);

            return yeniListe.ToArray();
        }
    }
}
