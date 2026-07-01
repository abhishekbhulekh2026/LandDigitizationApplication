using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class DistrictAbbreviationListModel
    {
        public int Id { get; set; }
        public string? DistrictName { get; set; }
        public string? Abbreviation { get; set; }
    }
}
