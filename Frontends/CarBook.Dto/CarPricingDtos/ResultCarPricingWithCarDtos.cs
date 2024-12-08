using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.CarPricingDtos
{
    public class ResultCarPricingWithCarDtos
    {
        public int CarID { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public string CoverImageUrl { get; set; }
        public int Km { get; set; }
        public int Transmisson { get; set; }
        public byte Seat { get; set; }
        public string Fuel { get; set; }
        public byte Luggage { get; set; }

        public string BigImageUrl { get; set; }

        public string PricingName { get; set; }
        public decimal PricingAmount { get; set; }
    }
}
