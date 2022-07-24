using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjMVCDemo.vModel;
using qqqq.Models;
using qqqq.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace final_test.Controllers
{
    public class VolunteerController : Controller
    {

        我救浪Context db = new 我救浪Context();
        List<VActivityViewModel> list = new List<VActivityViewModel>();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp(int? id)
        {
            VActivityViewModel v = new VActivityViewModel();
            var a = db.Vactivities.Where(x => x.ActivityId == id).Include(y => y.ActivityCategory).AsSplitQuery().FirstOrDefault();

            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER))
            {
                var b = HttpContext.Session.GetString((CDictionary.SK_LOGIN_USER));
                CLoginAccountViewModel memberview = JsonSerializer.Deserialize<CLoginAccountViewModel>(b);
                var c = db.Members.Where(x => x.Email == memberview.txtAccount).FirstOrDefault();
                v.MemberID = c.MemberId;
                v.MemberAddress = c.Address;
                v.MemberEmail = c.Email;
                v.MemberName = c.Name;
                v.MemberPhone = c.MemberPhone;
            }

            v.vactivity = a;
            v.ActivityCategoryName = a.ActivityCategory.CategoryName;
            return View(v);
        }
        public IActionResult loadCheckBox()
        {
            var a = db.VactivityCategories.Select(x => new { x.ActivityCategoryId, x.CategoryName }).ToList();
            return Json(a);
            
        }
        public IActionResult loadList(int[] intarr,DateTime date,string keyString)
        {
            list.Clear();
            int year = date.Year, month = date.Month, day = date.Day;
            if(keyString == "")
            {
                keyString = null;
            }
            IEnumerable<Vactivity> a = db.Vactivities.Include(x => x.ActivityCategory).AsSplitQuery().ToList();
            foreach (Vactivity j in a)
            {
                VActivityViewModel v = new VActivityViewModel()
                {
                    vactivity = j,
                    ActivityCategoryName = j.ActivityCategory.CategoryName,
                };
                list.Add(v);
            }

            List<VActivityViewModel> resultList = new List<VActivityViewModel>();
            if (intarr.Length == 0  && year == 1 && keyString == null)
            {
                foreach (VActivityViewModel j in list)
                {
                    resultList.Add(j);
                }
            }
            else if (intarr.Length != 0 && year == 1 && keyString==null)
            {
                foreach (int i in intarr)
                {
                    foreach (VActivityViewModel j in list)
                    {
                        if (j.ActivityCategoryID == i)
                        {
                            resultList.Add(j);
                        }
                    }
                }
            }
            else if (intarr.Length == 0 && year != 1 && keyString == null)
            {
                foreach (var i in list)
                {
                    string[] startarr = i.StartDate.Split('-');
                    string[] endarr = i.EndDate.Split('-');
                    if(year <= int.Parse(endarr[0]) )
                    {
                        if(month>= int.Parse(startarr[1]) && month <= int.Parse(endarr[1]))
                        {
                            if(day >= int.Parse(startarr[2]) && day <= int.Parse(endarr[2]))
                            {
                                resultList.Add(i);
                            }
                        }
                    }
                }
            }
            else if (intarr.Length == 0 && year == 1 && keyString != null)
            {
                foreach (var i in list)
                {
                    if (i.Description.Contains(keyString) || i.Title.Contains(keyString))
                    {
                        resultList.Add(i);
                    }
                }
            }
            else if(intarr.Length != 0 && year != 1 && keyString == null)
            {
                List<VActivityViewModel> localList = new List<VActivityViewModel>();
                foreach (int i in intarr)
                {
                    foreach (VActivityViewModel j in list)
                    {
                        if (j.ActivityCategoryID == i)
                        {
                            localList.Add(j);
                        }
                    }
                }
                foreach (var i in localList)
                {
                    string[] startarr = i.StartDate.Split('-');
                    string[] endarr = i.EndDate.Split('-');
                    if (year <= int.Parse(endarr[0]))
                    {
                        if (month >= int.Parse(startarr[1]) && month <= int.Parse(endarr[1]))
                        {
                            if (day >= int.Parse(startarr[2]) && day <= int.Parse(endarr[2]))
                            {
                                resultList.Add(i);
                            }
                        }
                    }
                }
            }
            else if (intarr.Length != 0 && year == 1 && keyString != null)
            {
                List<VActivityViewModel> localList = new List<VActivityViewModel>();
                foreach (int i in intarr)
                {
                    foreach (VActivityViewModel j in list)
                    {
                        if (j.ActivityCategoryID == i)
                        {
                            localList.Add(j);
                        }
                    }
                }
                foreach (var i in localList)
                {
                    if (i.Description.Contains(keyString) || i.Title.Contains(keyString))
                    {
                        resultList.Add(i);
                    }
                }
            }
            else if (intarr.Length == 0 && year != 1 && keyString != null)
            {
                List<VActivityViewModel> localList = new List<VActivityViewModel>();
                foreach (var i in list)
                {
                    string[] startarr = i.StartDate.Split('-');
                    string[] endarr = i.EndDate.Split('-');
                    if (year <= int.Parse(endarr[0]))
                    {
                        if (month >= int.Parse(startarr[1]) && month <= int.Parse(endarr[1]))
                        {
                            if (day >= int.Parse(startarr[2]) && day <= int.Parse(endarr[2]))
                            {
                                localList.Add(i);
                            }
                        }
                    }
                }
                foreach (var i in localList)
                {
                    if (i.Description.Contains(keyString) || i.Title.Contains(keyString))
                    {
                        resultList.Add(i);
                    }
                }
            }
            else
            {
                List<VActivityViewModel> localList = new List<VActivityViewModel>();
                List<VActivityViewModel> localList2 = new List<VActivityViewModel>();
                foreach (int i in intarr)
                {
                    foreach (VActivityViewModel j in list)
                    {
                        if (j.ActivityCategoryID == i)
                        {
                            localList.Add(j);
                        }
                    }
                }
                foreach (var i in localList)
                {
                    string[] startarr = i.StartDate.Split('-');
                    string[] endarr = i.EndDate.Split('-');
                    if (year <= int.Parse(endarr[0]))
                    {
                        if (month >= int.Parse(startarr[1]) && month <= int.Parse(endarr[1]))
                        {
                            if (day >= int.Parse(startarr[2]) && day <= int.Parse(endarr[2]))
                            {
                                localList2.Add(i);
                            }
                        }
                    }
                }
                foreach (var i in localList2)
                {
                    if (i.Description.Contains(keyString) || i.Title.Contains(keyString))
                    {
                        resultList.Add(i);
                    }
                }
            }
            return Json(resultList.Select(x => new { x.ActivityCategoryID, x.Title, x.PeopleInNeed, x.ActivityCategoryName, x.ActivityID, x.ActivityPhoto, x.Description, x.EndDate, x.StartDate }));

        }

        //A:正取 B:候補
        public IActionResult getBySession(DateTime dateSelected, int actID)
        {
            string dateFormat = $"{dateSelected.Year}-{String.Format("{0:00}", dateSelected.Month)}-{String.Format("{0:00}", dateSelected.Day)}";
            int morningCountA = db.Volunteers.Where(x => x.ActivityId == actID && x.AllowDate == dateFormat && (x.AllowTimeId == 1 || x.AllowTimeId == 2) && x.Waiting == false).ToList().Count;
            int morningCountB = db.Volunteers.Where(x => x.ActivityId == actID && x.AllowDate == dateFormat && (x.AllowTimeId == 1 || x.AllowTimeId == 2) && x.Waiting == true).ToList().Count;
            int afternoonCountA = db.Volunteers.Where(x => x.ActivityId == actID && x.AllowDate == dateFormat && (x.AllowTimeId == 1 || x.AllowTimeId == 3) && x.Waiting == false).ToList().Count;
            int afternoonCountB = db.Volunteers.Where(x => x.ActivityId == actID && x.AllowDate == dateFormat && (x.AllowTimeId == 1 || x.AllowTimeId == 3) && x.Waiting == true).ToList().Count;

            return Json(new
            {
                morningCountA = morningCountA,
                morningCountB = morningCountB,
                afternoonCountA = afternoonCountA,
                afternoonCountB = afternoonCountB
            });
        }

        public IActionResult saveToDB(int MemID,int actID,string actDate,string actTime,string Name,string Phone,string Email ,string Status)
        {

            //System.Diagnostics.Debug.WriteLine(MemID.ToString(),actID.ToString(),actTime,Name,Phone,Email, actDate, Status);
            Volunteer v = new Volunteer();
            v.MemberId = MemID;
            v.ActivityId = actID;
            var date = actDate.Split('/');
            v.AllowDate = $"{date[0]}-{date[1].PadLeft(2,'0')}-{date[2].PadLeft(2, '0')}";
            v.AllowTimeId = db.VallowTimes.Where(x => x.TimeRange == actTime).Select(y => y.AllowTimeId).FirstOrDefault();
            v.Name = Name;
            v.Phone = Phone;
            v.Email = Email;
            v.CheckEmail = false;
            if (Status == "候補")
            {
                v.Waiting = true;
            }
            else
            {
                v.Waiting = false;
            }
            System.Diagnostics.Debug.WriteLine($"{date[0]}-{date[1].PadLeft(2, '0')}-{date[2].PadLeft(2, '0')}");
            db.Volunteers.Add(v);
            db.SaveChanges();
            return Json("");
        }
        //public ActionResult checkRemaining(DateTime date, int actID, string time)
        //{
        //    int timeid = db.VallowTimes.Where(x => x.TimeRange == time).Select(y => y.AllowTimeId).FirstOrDefault();
        //    string dateFormat = $"{date.Year}-{String.Format("{0:00}", date.Month)}-{String.Format("{0:00}", date.Day)}";
        //}

       
    }
}
