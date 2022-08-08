using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace qqqq.Controllers
{
    public class serviceController : Controller
    {
        public IActionResult service()
        {
            return View();
        }
        public IActionResult getNews()
        {
            ChromeOptions op = new ChromeOptions();
            op.AddArguments("--headless");
            ChromeDriver cd = new ChromeDriver(Environment.CurrentDirectory, op, TimeSpan.FromSeconds(180));
            cd.Navigate().GoToUrl("https://www.tcapo.gov.taipei/Default.aspx");
            cd.FindElement(By.XPath("/html/body/form/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div/div/div[4]/div/div/div/div[1]/div/div/div/div[3]/div/div/div/ul/li[2]/div/div/div[2]/div/ul/li/span/a")).Click();
            cd.Navigate().GoToUrl(cd.FindElement(By.XPath("/html/body/form/div[3]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div/div/div/div/div/div/div[1]/div/div/div/div[2]/div/div/div/div[2]/div/div/div/div[2]/div/div[2]/div/ul/li[3]/span/a")).GetAttribute("href"));
            string text = cd.FindElement(By.XPath("/html/body/pre")).Text;

            JArray jsonArr = JArray.Parse(text);
            List<newslist> list = new List<newslist>();

            for(int i = 0; i < 20; i++)
            {
                list.Add(new newslist
                {
                    picture = jsonArr[i]["相關圖片"][0]["url"].ToString(),
                    date = DateTime.Parse(jsonArr[i]["發布日期"].ToString()).ToString("yyyy-MM-dd"),
                    title = jsonArr[i]["title"].ToString(),
                    source = jsonArr[i]["Source"].ToString()
                });
            }
            cd.Quit();
            return Json(list.Select(x=>new {picture = x.picture,date = x.date,title = x.title,source = x.source }));
        }
        public class newslist
        {
            public string picture;
            public string date;
            public string title;
            public string source;
        }
    }
}
