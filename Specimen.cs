using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thaw_Mix_Dashboard
{
    public class Specimen
    {
        public string specimenNumber { get; set; }
        public string createWhen { get; set; }
        public string createBy { get; set; }
        public string foreground { get; set; }

        public Specimen(string specimenNumber, string createWhen, string createBy)
        {
            this.specimenNumber = specimenNumber;
            this.createWhen = createWhen;
            this.createBy = createBy;
            this.foreground = "DarkBlue";
        }
    }
}
