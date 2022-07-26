using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using qqqq.Models;
using qqqq.ViewModels;
using System.Diagnostics;

namespace qqqq.Controllers
{
    public class BK_CommentController : Controller
    {
        我救浪Context db = new 我救浪Context();
        int delayTime = 1000;
        public IActionResult List(int id,string sortString)
        {
            string[] sortArray = null;
            string sortType = null;
            string order = "";
            if (sortString == null)
            {
                sortType = "CommentDate";
                ViewBag.sortType = "CommentDate/";
            }
            else
            {
                sortArray = sortString.Split('/');
                sortType = sortArray[0];
                order = sortArray[1];
                ViewBag.sortType = sortString;
            }
     
            if (order=="")
            {
                var q = db.MemberComments.AsEnumerable().Where(mc => mc.ProductId == id).Select(mc => new CCommentView(mc)).OrderByDescending(cv => cv.GetType().GetProperty(sortType).GetValue(cv)).Take(10).ToList();
                return View(q);
            }
            else
            {
                var q = db.MemberComments.AsEnumerable().Where(mc => mc.ProductId == id).Select(mc => new CCommentView(mc)).OrderBy(cv => cv.GetType().GetProperty(sortType).GetValue(cv)).Take(10).ToList();
                return View(q);
            }
      
        }
        public IActionResult AjaxForComment(int id, string sortString,int count)
        {
            System.Threading.Thread.Sleep(delayTime);
            string[] sortArray = sortString.Split('/');
            string sortType = sortArray[0];
            string order = sortArray[1];
            if (order == "")
            {
                var q = db.MemberComments.AsEnumerable().Where(mc => mc.ProductId == id).Select(mc => new CCommentView(mc)).OrderByDescending(cv => cv.GetType().GetProperty(sortType).GetValue(cv)).Skip(count).Take(10).ToList();
                if(q.Count==0)  return Json(false);
                return PartialView("CommentView",q);
            }
            else
            {
                var q = db.MemberComments.AsEnumerable().Where(mc => mc.ProductId == id).Select(mc => new CCommentView(mc)).OrderBy(cv => cv.GetType().GetProperty(sortType).GetValue(cv)).Skip(count).Take(10).ToList();
                if (q.Count == 0)   return Json(false);
                return PartialView("CommentView", q);
            }
        }
        
        public IActionResult GetResponse(int id,int count)
        {
            System.Threading.Thread.Sleep(delayTime);
            var q = db.CommentResponses.Where(cr => cr.CommentId == id).OrderBy(cr=>cr.CommentDate).Skip(count).Take(11).Select(cr=>new CResponseView(cr)).ToList();
            ViewBag.isMoreResponse = q.Count == 11 ? true : false ;
            return PartialView("ResponseView", q.Take(10));
        }
        public IActionResult AddResponse(int id,string response)
        {
            CommentResponse commentResponse=new CommentResponse();
            commentResponse.CommentDate=DateTime.Now;
            commentResponse.EmployeeId = 1;//之後要用session修改////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            commentResponse.Description = response;
            commentResponse.CommentId = id;
            commentResponse.IsEmployee=true;
            db.CommentResponses.Add(commentResponse);
            db.SaveChanges();

            //db = new 我救浪Context();
            var q = db.CommentResponses.Include(cr=>cr.Employee).Include(cr=>cr.Member).Where(cr => cr.ResponseId == commentResponse.ResponseId).ToList();


            System.Threading.Thread.Sleep(delayTime);
            return PartialView("ResponseView", CResponseView.CResponseViews(q));
        }
        public string EditResponse(int id,string response)
        {
            var q = db.CommentResponses.Where(cr => cr.ResponseId == id).FirstOrDefault();
            q.Description = response;
            db.SaveChanges();
            System.Threading.Thread.Sleep(2000);
            return response;
        }
        public bool DeleteResponse(int id)
        {
            try
            {
                var q = db.CommentResponses.Where(cr => cr.ResponseId == id).FirstOrDefault();
                db.CommentResponses.Remove(q);
                db.SaveChanges();
                System.Threading.Thread.Sleep(delayTime);
                return true;
            }
            catch
            {
                System.Threading.Thread.Sleep(delayTime);
                return false;
            }

        }
    }
}
