using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotNetBlainsBoredClient.Models
{
    [JsonObject]
    public class ActivityModel
    {
        //Description
        [Display(Name = "Description")]
        public string? activity { get; set; }

        //Accessibility rating
        [RegularExpression(@"[0-9]{0,}\.[0-9]{1}", ErrorMessage = "Enter a positive number value containing a single decimal.")]
        [Display(Name = "Accessibility rating")]
        public decimal? accessibility { get; set; }

        //Type
        [Display(Name = "Type")]
        [Required]
        public string? type { get; set; }

        //Number of participants
        [Display(Name = "Number of participants")]
        public int? participants { get; set; }

        //Relative price
        [Display(Name = "Relative price")]
        public decimal? price { get; set; }

        public string? key { get; set; }

        public string? error { get; set; }

    }
}
