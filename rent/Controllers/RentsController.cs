using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using rent.Models;
using PagedList;

namespace rent.Controllers
{
    //[Authorize(Roles = "administrator")]
    public class RentsController : Controller
    {
        private RentDBContext db = new RentDBContext();


        // GET: Rents
        public ActionResult Index(string rentGenre, string rentGender, string renttitle, string searchString, string currentFilter, int page = 1)
        {

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            //地址搜尋資料輸入
            var rents = from m in db.Rents
                        select m;


            //地址搜尋
            if (!String.IsNullOrEmpty(searchString))
            {
                rents = rents.Where(m => m.address.Contains(searchString));
            }

            //房屋型態篩選資料輸入
            var GenreLst = new List<string>();

            var GenreQry = from d in db.Rents
                           orderby d.property_type
                           select d.property_type;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.rentGenre = new SelectList(GenreLst);

            //房屋型態篩選
            if (!string.IsNullOrEmpty(rentGenre))
            {
                rents = rents.Where(m => m.property_type == rentGenre);
            }

            //性別篩選資料輸入
            var GenderLst = new List<string>();

            var GenderQry = from g in db.Rents
                            orderby g.gender_restriction
                            select g.gender_restriction;

            GenderLst.AddRange(GenderQry.Distinct());
            ViewBag.rentGender = new SelectList(GenderLst);

            //性別篩選
            if (!String.IsNullOrEmpty(rentGender))
            {
                rents = rents.Where(m => m.gender_restriction == rentGender);
            }


            switch (renttitle)
            {
                case "address":
                    rents = rents.OrderByDescending(m => m.address);
                    break;
                case "property_type":
                    rents = rents.OrderBy(s => s.property_type);
                    break;
                case "gender":
                    rents = rents.OrderByDescending(s => s.gender_restriction);
                    break;
                default:  // Name ascending 
                    rents = rents.OrderBy(s => s.address);
                    break;
            }

            //起始頁
            int currentPage = page < 1 ? 1 : page;
            //頁面顯示多寡
            int pageSize = 30;


            return View(rents.ToPagedList(currentPage, pageSize));
        }

        // GET: Rents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.Rents.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        // GET: Rents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rents/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create
            ([Bind(Include = "ID,address,top_region,sub_region,monthly_price,floor," +
            "property_type,building_type," + //dropdownlist 2項
            "deposit,additional_fee_internet,additional_fee_water," +//需要 不需要 2項
            "allow_pet,can_cook," +//可 不可 2項
            "facilities_refrigerator,gender_restriction" //有 沒有 4項
            )] Rent rent)
        {

            if (ModelState.IsValid)
            {
                db.Rents.Add(rent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rent);
        }

        // GET: Rents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.Rents.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        // POST: Rents/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,address,top_region,sub_region,monthly_price,floor," +
            "property_type,building_type," + //dropdownlist 2項
            "deposit,additional_fee_internet,additional_fee_water," +//需要 不需要 2項
            "allow_pet,can_cook," +//可 不可 2項
            "facilities_refrigerator,gender_restriction" //有 沒有 4項
         )] Rent rent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rent);
        }

        // GET: Rents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.Rents.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        // POST: Rents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rent rent = db.Rents.Find(id);
            db.Rents.Remove(rent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
