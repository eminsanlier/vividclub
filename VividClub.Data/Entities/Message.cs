﻿using System;
using System.ComponentModel.DataAnnotations;

namespace VividClub.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public User Sender { get; set; }

        [Required]
        public User Receiver { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxMessageLength)]
        public string MessageText { get; set; }

        [Required]
        public DateTime DateSent { get; set; }

        public bool IsSeen { get; set; }
    }
}