﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magic_Villa_VillaAPI.Models
{
    public class Villa
    {
        [Key] //Pk
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // automatically increament by one 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]    
        public double Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }



    }
}
