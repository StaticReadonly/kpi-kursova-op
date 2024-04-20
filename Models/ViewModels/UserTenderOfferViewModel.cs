using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class UserTenderOfferViewModel
    {
        public Guid Id { get; set; }
        public OffererViewModel Offerer { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
