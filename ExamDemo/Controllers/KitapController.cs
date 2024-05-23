﻿using ExamDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamDemo.Controllers
{
    public class KitapController : Controller
    {
        private MyDbContext _dbContext;

        public KitapController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public IActionResult Index()
        {
           List<Kitap> kitaplar= _dbContext.Kitaplar.ToList();
          
            return View(kitaplar);
        }
        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(KitapVM kitapVM)
        {
            //kitapVM olarak gelen modelin Validation kontrolunu yapiniz.


            if (ModelState.IsValid)
            {

                Kitap newKitap = new Kitap();
                newKitap.Id = kitapVM.Id;
                newKitap.KitapAdi = kitapVM.KitapAdi;
                newKitap.Yazar = kitapVM.Yazar;
                _dbContext.Kitaplar.Add(newKitap);
                _dbContext.SaveChanges();
                return  View("Index");
            }
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }


        public IActionResult Delete(int id) {
            //id'si gelen kitabi veritabanindan siliniz.
            var kitap = _dbContext.Kitaplar.Find(id);
            _dbContext.Kitaplar.Remove(kitap);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}