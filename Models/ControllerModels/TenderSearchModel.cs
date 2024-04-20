using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ControllerModels
{
    public class TenderSearchModel
    {
        public int Page { get; set; } = 1;
        public string? Query { get; set; } = null;
    }
}
