﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace BookReviewingMVC.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100,ErrorMessage = "First Name cannot be more than 100 characters")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "First Name cannot be more than 100 characters")]
        public string LastName { get; set; }
        // public int CountryId {get; set;} //foreing key
        public virtual Country Country { get; set; }
        [JsonIgnore]
        public virtual ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}
