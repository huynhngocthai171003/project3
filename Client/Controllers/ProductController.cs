using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Web;
using System;
/*using Microsoft.AspNetCore.Http;
using inject Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor;*/
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;

namespace Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly ePRJContext _context;
        private static int test = (int)DateTime.Now.Ticks;
        Random rand = new Random((int)test);

        public ProductController(ePRJContext context)
        {
            _context = context;
        }

        /*public IActionResult _Header1()
        {
            var danhmuc = _context.Categorys.ToList();
            ViewBag.Category = danhmuc;
            return View(); 



        }*/
        public IActionResult IndexProduct(int? page, int Id)
        {
            try
            {
                var recentPost = _context.Comments.AsNoTracking().Include(x => x.Customer).Include(x => x.Product).Take(3).OrderByDescending(x => x.Id).ToList(); 
                var danhmuc1 = _context.Categorys.AsNoTracking().SingleOrDefault(x => x.Id == Id);
                var danhmuc = _context.Categorys.ToList();
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 9;
                var IsProduct = _context.Products
                    .AsNoTracking()
                    .OrderByDescending(x => x.Id);
                PagedList<Product> models = new PagedList<Product>(IsProduct, pageNumber, pageSize);
                ViewBag.Category = danhmuc;
                ViewBag.Post = recentPost;
                /*                ViewBag.CurrentCat = danhmuc1;*/
                ViewBag.CurrentPage = pageNumber;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }




        [Route("/{Avatar}-{id}.html", Name = ("ProductDetails"))]
        public IActionResult Details(int id)
        {
            try
            {
                //Comment? comment1 = _context.Comments.Find(id);
                /*int count = (from z in _context.Comments
                             where z.ProductId == comment1.ProductId
                             select z.Rate).Count();
                ViewBag.CountReview = HttpContext.Session.GetInt32("count");*/
/*                HttpContext.Session.Remove("count");*/
                var p = _context.Products.Find(id);
                var p1 = _context.Comments.AsNoTracking().Include(x => x.Customer).Where(x => x.ProductId == id).Take(5).OrderByDescending(x => x.Id).ToList();
                var product = _context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
                if (product == null)
                {
                    return RedirectToAction("IndexProduct");
                }
                var lsProduct = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CategoryId == product.CategoryId && x.Id != id)
                    .Take(4)
                    .OrderByDescending(x => x.Id)
                    .ToList();
                /*ViewBag.Comment = p;*/
                ViewBag.SanPham = lsProduct;
                ViewBag.Product = p;
                ViewBag.Comment = p1;
                var comment = new Comment()
                {
                    ProductId = product.Id
                };
                return View(comment);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }


        }

        [Route("/Category/{Id}", Name = ("CategoryProduct"))]
        public IActionResult Category(int Id, int page = 1)
        {
            try
            {
                var recentPost = _context.Comments.AsNoTracking().Include(x => x.Customer).Include(x => x.Product).Take(3).OrderByDescending(x => x.Id).ToList();
                var danhmuc1 = _context.Categorys.ToList();
                var pageSize = 9;
                var danhmuc = _context.Categorys.AsNoTracking().SingleOrDefault(x => x.Id == Id);

                var lsTinDangs = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CategoryId == danhmuc.Id)
                    .OrderByDescending(x => x.Id);
                PagedList<Product> models = new PagedList<Product>(lsTinDangs, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = danhmuc;
                ViewBag.Category = danhmuc1;
                ViewBag.Post = recentPost;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }


        }
        public int countViewer(Comment cmt)
        {
            int id = cmt.ProductId;
            Comment? comment = _context.Comments.Find(id);
            int count = (from z in _context.Comments
                         where z.ProductId == comment.ProductId
                         select z.Rate).Count();
            return ViewBag.Count;
        }

        [HttpPost]
        public IActionResult Send(Comment comment, int rating)
        {

            var s = HttpContext.Session.GetString("Username");
            string username = s.ToString();
            comment.CustomerId = _context.Customers.Single(x => x.UserName.Equals(username)).Id;
            comment.Rate = rating;
            _context.Comments.Add(comment);
            _context.SaveChanges();

            int count = (from p in _context.Comments
                         where p.ProductId == comment.ProductId
                         select p.Rate).Count();
          /*  HttpContext.Session.SetInt32("count", count);*/
            int sum = (from p in _context.Comments
                       where p.ProductId == comment.ProductId
                       select p.Rate).Sum();
            int avg = sum / count;

            Product? old = _context.Products!.FirstOrDefault(x => x.Id == comment.ProductId);
            old!.Rate = avg;
            _context.Products.Update(old);
            _context.SaveChanges();

            /*return RedirectToAction("Detail", "Product", new {id = comment.ProductId});*/
            return RedirectToRoute("ProductDetails", new {Avatar = comment.Product.Avatar, Id = comment.ProductId});
        }




    }
}
