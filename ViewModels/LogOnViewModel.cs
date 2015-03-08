using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class LogOnViewModel
    {
        [Required(ErrorMessage = "You should write your email")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public LogOnViewModel() { }
    }
}