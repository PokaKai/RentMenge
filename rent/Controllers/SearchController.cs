using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using rent.Models;

namespace rent.Controllers
{
    public class SearchController : Controller
    {
        private RentDBContext db = new RentDBContext();

        // GET: Search
        public ActionResult Index(string rentGender, string rentRegion, string rentGenre, string rentBuild, string rentBedroom, string searchString, string currentFilter, string renttitle, int page = 1)
        {
            ViewBag.monthly_price = string.IsNullOrEmpty(renttitle) ? "price" : "";
            var rents = from m in db.Rents
                        select m;
            switch (renttitle)
            {
                case "price":
                    rents = rents.OrderByDescending(s => s.monthly_price);
                    break;

                default:  // Name ascending 
                    rents = rents.OrderBy(s => s.monthly_price);
                    break;
            }
            //起始頁
            int currentPage = page < 1 ? 1 : page;
            //頁面顯示多寡
            int pageSize = 10;
            if (searchString != null)
            { page = 1; }
            else
            { searchString = currentFilter; }

            ViewBag.CurrentFilter = searchString;

            //地址搜尋
            if (!String.IsNullOrEmpty(searchString))
            { rents = rents.Where(m => m.address.Contains(searchString)); }

            //縣市篩選資料輸入
            var CityLst = new List<string>();
            var CityQry = from g in db.Rents
                          orderby g.top_region
                          select g.top_region;

            CityLst.AddRange(CityQry.Distinct());
            ViewBag.rentRegion = new SelectList(CityLst);
            //縣市篩選
            if (!String.IsNullOrEmpty(rentRegion))
            { rents = rents.Where(m => m.top_region == rentRegion); }
            //租屋類型資料輸入
            var GenreLst = new List<string>();

            var GenreQry = from g in db.Rents
                           orderby g.property_type
                           select g.property_type;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.rentGenre = new SelectList(GenreLst);
            //租屋類型篩選
            if (!String.IsNullOrEmpty(rentGenre))
            { rents = rents.Where(m => m.property_type == rentGenre); }

            //建築類型資料輸入
            var BuildLst = new List<string>();

            var BuildQry = from g in db.Rents
                           orderby g.building_type
                           select g.building_type;

            BuildLst.AddRange(BuildQry.Distinct());
            ViewBag.rentBuild = new SelectList(BuildLst);
            //建築類型篩選
            if (!String.IsNullOrEmpty(rentBuild))
            { rents = rents.Where(m => m.building_type == rentBuild); }

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


            return View(rents.ToPagedList(currentPage, pageSize));
        }

        // GET: Search/Details/5
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

        // GET: Search/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,address,top_region,sub_region,monthly_price,floor,property_type,building_type,deposit,additional_fee_internet,additional_fee_water,allow_pet,can_cook,facilities_refrigerator,facilities_Airconditioner,facilities_washing,facilities_heater,facilities_internet,facilities_tv,floor_ping,gender_restriction,has_parking,is_require_management_fee,is_require_parking_fee,living_functions_conv_store,living_functions_hospital,living_functions_park,living_functions_school,bathroom,bedroom,livingroom,latitude,longitude,total_floor,MRT,phone_num,houseimage,click_times,heart_times")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                db.Rents.Add(rent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rent);
        }

        // GET: Search/Edit/5
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

        // POST: Search/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,address,top_region,sub_region,monthly_price,floor,property_type,building_type,deposit,additional_fee_internet,additional_fee_water,allow_pet,can_cook,facilities_refrigerator,facilities_Airconditioner,facilities_washing,facilities_heater,facilities_internet,facilities_tv,floor_ping,gender_restriction,has_parking,is_require_management_fee,is_require_parking_fee,living_functions_conv_store,living_functions_hospital,living_functions_park,living_functions_school,bathroom,bedroom,livingroom,latitude,longitude,total_floor,MRT,phone_num,houseimage,click_times,heart_times")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rent);
        }

        // GET: Search/Delete/5
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

        // POST: Search/Delete/5
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
        public JsonResult GetRent()
        {
            var rent = db.Rents.ToList();
            return Json(new { data = rent }, JsonRequestBehavior.AllowGet);
        }
    }
}

