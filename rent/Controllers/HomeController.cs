using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using rent.Models;

namespace rent.Controllers
{
    public class HomeController : Controller
    {
        private RentDBContext db = new RentDBContext();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.Rents.ToList());
        }

        // GET: Home/Details/5
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

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,address,top_region,sub_region,monthly_price,floor,property_type,building_type,deposit,additional_fee_internet,additional_fee_water,allow_pet,can_cook,facilities_refrigerator,facilities_Airconditioner,facilities_washing,facilities_heater,facilities_internet,facilities_tv,floor_ping,gender_restriction,has_parking,is_require_management_fee,is_require_parking_fee,living_functions_conv_store,living_functions_hospital,living_functions_park,living_functions_school,bathroom,bedroom,livingroom,latitude,longitude,total_floor,MRT,phone_num,houseimage,click_times,heart_times,houseimage_2,houseimage_3,houseimage_4,houseimage_5")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                db.Rents.Add(rent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rent);
        }

        // GET: Home/Edit/5
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

        // POST: Home/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,address,top_region,sub_region,monthly_price,floor,property_type,building_type,deposit,additional_fee_internet,additional_fee_water,allow_pet,can_cook,facilities_refrigerator,facilities_Airconditioner,facilities_washing,facilities_heater,facilities_internet,facilities_tv,floor_ping,gender_restriction,has_parking,is_require_management_fee,is_require_parking_fee,living_functions_conv_store,living_functions_hospital,living_functions_park,living_functions_school,bathroom,bedroom,livingroom,latitude,longitude,total_floor,MRT,phone_num,houseimage,click_times,heart_times,houseimage_2,houseimage_3,houseimage_4,houseimage_5")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rent);
        }

        // GET: Home/Delete/5
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

        // POST: Home/Delete/5
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
