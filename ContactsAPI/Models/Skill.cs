﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Models
{
    public partial class Skill
    {
        public Skill()
        {
            ContactSkills = new HashSet<ContactSkill>();
        }
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<ContactSkill> ContactSkills { get; set; }
    }
}