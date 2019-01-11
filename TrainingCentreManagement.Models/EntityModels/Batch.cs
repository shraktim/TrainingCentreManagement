﻿using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using TrainingCentreManagement.Models.Contracts;

namespace TrainingCentreManagement.Models.EntityModels
{
  public class Batch:IEntity
  {
        public int Id { get; set; }

      public string Name { get; set; }
      public string EntityDescription { get; set; }
      public string Description { get; set; }

        [Required]
        [Display(Name= "Total Seats")]
        public int TotalSeats { get; set; }
        [Required]
        [Display(Name = "Registration Start")]
        public DateTime RegistrationStart { get; set; }
        [Required]
        [Display(Name = "Registration End")]
        public DateTime RegistrationEnd { get; set; }
        [Required]
        [Display(Name = "Class Start")]
        public DateTime ClassStart { get; set; }
      public int CourseId { get; set; }
      public virtual Course Course { get; set; }


      public DateTime CreatedAt { get; set; }
      public DateTime UpdatedAt { get; set; }
  }
}
