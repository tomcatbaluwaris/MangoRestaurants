﻿using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public class ProductDto
    {
        public ProductDto()
        {
            Count = 1;
        }
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        public double Price { get; set; }


        public string Description { get; set; }


        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        [Range(0,100)]
        public int Count { get; set; }


    }
}
