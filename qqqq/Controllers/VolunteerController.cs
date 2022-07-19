using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Pet.ViewModels;
using qqqq.Models;
using qqqq.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace final_test.Controllers
{
    public class VolunteerController : Controller
    {

        我救浪Context db = new 我救浪Context();
        List<VActivityViewModel> list = new List<VActivityViewModel>();
        public IActionResult Index()
        {
            //var a = list(db.Vactivities.Select(x => x).ToList());
            //var a = db.Vactivities.Include(x => x.ActivityCategory).AsSplitQuery().ToList();
            //var b = db.Members.Select(x=>x).AsSplitQuery().ToList();
            //foreach (var i in a)
            //{
            //    VActivityViewModel vac = new VActivityViewModel();
            //    vac.vactivity = i;
            //    vac.ActivityCategoryName = i.ActivityCategory.CategoryName;
            //    list.Add(vac);
            //}
            return View(/*list*/);
        }
        //public string GetCategoryName(int? id)
        //{
        //    return db.VactivityCategories.Where(x => x.ActivityCategoryId == id).Select(y => y.CategoryName).FirstOrDefault().ToString();
        //}
        //public IActionResult Info(int? id)
        //{
        //    var a = db.Vactivities.Where(x => x.ActivityId == id).Include(y => y.ActivityCategory).AsSplitQuery().FirstOrDefault();
        //    VActivityViewModel v = new VActivityViewModel();
        //    v.vactivity = a;
        //    v.ActivityCategoryName = a.ActivityCategory.CategoryName;
        //    return View(v);
        //}
        public IActionResult SignUp(int? id)
        {
            VActivityViewModel v = new VActivityViewModel();
            var a = db.Vactivities.Where(x => x.ActivityId == id).Include(y=>y.ActivityCategory).AsSplitQuery().FirstOrDefault();
            
            v.vactivity = a;
            v.ActivityCategoryName = a.ActivityCategory.CategoryName;
            //var b = db.Volunteers.Where(x => x.ActivityId == id).Where(y => y.ConfirmByEmp == true).ToList();
            //v.volunteer = b;
            //System.Diagnostics.Debug.WriteLine();

            return View(v);
        }
        public IActionResult loadCheckBox()
        {
            //new { x.ActivityCategoryId,x.CategoryName}
            var a = db.VactivityCategories.Select(x => new { x.ActivityCategoryId, x.CategoryName }).ToList();
            return Json(a);
            
        }
        public IActionResult loadList(int[] intarr,DateTime date,string keyString)
        {
            list.Clear();
            System.Diagnostics.Debug.WriteLine(date);
            System.Diagnostics.Debug.WriteLine(keyString);  //null
            int year = date.Year, month = date.Month, day = date.Day;
            System.Diagnostics.Debug.WriteLine(year.ToString());  //1
            System.Diagnostics.Debug.WriteLine(month.ToString());
            System.Diagnostics.Debug.WriteLine(day.ToString());
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
        public IActionResult getRegisteredMember(DateTime dateSelected,int actID)
        {
            int year = dateSelected.Year, month = dateSelected.Month, day = dateSelected.Day;
            System.Diagnostics.Debug.WriteLine(actID);
            System.Diagnostics.Debug.WriteLine(dateSelected);
            System.Diagnostics.Debug.WriteLine(year.ToString(),month.ToString(), day.ToString());
            var a = db.Volunteers.Where(x => x.ActivityId == actID).Where(y => y.ConfirmByEmp == true).ToList();
            int morning = 0,afternoon=0;
            foreach (var i in a)
            {
                string[] allowdate = i.AllowDate.Split('-');
                //int endarr = i.AllowTime.AllowTimeId;
                if (year == int.Parse(allowdate[0]) && month == int.Parse(allowdate[1]) && day == int.Parse(allowdate[2]))
                {
                    if (i.ActivityId == 1)
                    {
                        morning++;
                        afternoon++;
                    }
                    if (i.ActivityId == 2)
                    {
                        morning++;
                    }
                    if (i.ActivityId == 3)
                    {
                        afternoon++;
                    }

                }
            }
            return Json(new {morning,afternoon });
        }
    }
}
