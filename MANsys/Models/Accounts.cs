using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MANsys.Models;

namespace MANsys.Models
{
    public class Accounts
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "RestaurantName is required.")]
        public string RestaurantName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Restaurant Email is required.")]
        public string RestaurantMail { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string RestaurantPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("RestaurantPassword", ErrorMessage = "Password and Comfirm Password are not same.")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Restaurant Adress is required.")]
        public string RestaurantAdress { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Restaurant District is required.")]
        public string RestaurantDistrict { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Restaurant City is required.")]
        public string RestaurantCity { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Restaurant Phone is required.")]
        public string RestaurantPhone { get; set; }

    }
}