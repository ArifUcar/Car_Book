using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Domain.Entities
{
    public class Log
    {
       
            [Key]
            [Column(TypeName = "char(36)")]
            public Guid Id { get; set; }

            [Required]
            [StringLength(100)]
            public string Title { get; set; }

            public string Description { get; set; }

            [Required]
            [StringLength(50)]
            public string ProcessType { get; set; }

            [Required]
            [StringLength(100)]
            public string ProcessLocation { get; set; }

            public DateTime CreatedDate { get; set; }
        }
    }

