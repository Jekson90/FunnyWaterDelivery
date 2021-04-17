using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyWaterDelivery
{
    class Order
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public DateTime Date { get; set; }
    }
}
