﻿using System.ComponentModel.DataAnnotations;

namespace AutoTradeHubViewService.Models
{
    public class Marka
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
