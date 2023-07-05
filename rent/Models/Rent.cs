using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace rent.Models
{
    public class Rent
    {
        public int ID { get; set; }
        [Display(Name = "地址")]
        [Required]
        public string address { get; set; }
        [Display(Name = "縣市")]
        public string top_region { get; set; }
        [Display(Name = "區域")]
        public string sub_region { get; set; }
        [Display(Name = "租金")]
        [Required]
        public int monthly_price { get; set; }
        [Display(Name = "樓層")]
        [Required]
        public int floor { get; set; }
        [Display(Name = "租屋類型")]
        public string property_type { get; set; }
        [Display(Name = "房屋型態")]
        public string building_type { get; set; }
        [Display(Name = "押金")]
        public string deposit { get; set; }
        [Display(Name = "額外網路費")]
        public string additional_fee_internet { get; set; }
        [Display(Name = "額外水費")]
        public string additional_fee_water { get; set; }
        [Display(Name = "養寵物")]
        public string allow_pet { get; set; }
        [Display(Name = "開伙")]
        public string can_cook { get; set; }
        [Display(Name = "冰箱")]
        public string facilities_refrigerator { get; set; }
        [Display(Name = "冷氣")]
        public string facilities_Airconditioner { get; set; }
        [Display(Name = "洗衣機")]
        public string facilities_washing { get; set; }
        [Display(Name = "熱水器")]
        public string facilities_heater { get; set; }
        [Display(Name = "網路")]
        public string facilities_internet { get; set; }
        [Display(Name = "電視")]
        public string facilities_tv { get; set; }
        [Display(Name = "坪數")]
        public string floor_ping { get; set; }
        [Display(Name = "性別限制")]
        public string gender_restriction { get; set; }
        [Display(Name = "停車位")]
        public string has_parking { get; set; }
        [Display(Name = "管理費")]
        public string is_require_management_fee { get; set; }
        [Display(Name = "停車費")]
        public string is_require_parking_fee { get; set; }
        [Display(Name = "附近有無超商")]
        public string living_functions_conv_store { get; set; }
        [Display(Name = "附近有無醫院")]
        public string living_functions_hospital { get; set; }
        [Display(Name = "附近有無公園")]
        public string living_functions_park { get; set; }
        [Display(Name = "附近有無學校")]
        public string living_functions_school { get; set; }
        [Display(Name = "衛")]
        public string bathroom { get; set; }
        [Display(Name = "房")]
        public string bedroom { get; set; }
        [Display(Name = "廳")]
        public string livingroom { get; set; }
        [Display(Name = "緯度")]
        public string latitude { get; set; }
        [Display(Name = "經度")]
        public string longitude { get; set; }
        [Display(Name = "總樓層")]
        public string total_floor { get; set; }
        [Display(Name = "捷運")]
        public string MRT { get; set; }
        [Display(Name = "手機號碼")]
        public string phone_num { get; set; }
        [Display(Name = "租屋圖片")]
        public string houseimage { get; set; }
        [Display(Name = "點擊次數")]
        public int click_times { get; set; }
        [Display(Name = "最愛次數")]
        public int heart_times { get; set; }
        public string houseimage_2 { get; set; }
        public string houseimage_3 { get; set; }
        public string houseimage_4 { get; set; }
        public string houseimage_5 { get; set; }
    }
    public class RentDBContext : DbContext
    {
        public DbSet<Rent> Rents { get; set; }
    }
}