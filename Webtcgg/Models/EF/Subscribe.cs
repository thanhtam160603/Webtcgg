using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Webtcgg.Models.EF
{
    [Table("tb_Subscribe")]
    public class Subscribe
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Không được để trống")]
        [StringLength(100, ErrorMessage ="không được quá 150 ký tự")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Không được để trống")]
        public DateTime CreateDate { get; set; }
    }
}