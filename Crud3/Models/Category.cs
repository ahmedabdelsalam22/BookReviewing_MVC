﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace BookReviewingMVC.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage ="Category name can't be more than 100 characters")]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<BookCategory>? BookCategories { get; set; }
    }
}
