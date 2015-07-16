using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gemi.Entities
{
    public class Ship
    {
        public string refId;
        public int shipId;
        public DateTime time;
        public string description;
        public string createdBy;
        public string createdPc;
        public DateTime createdDatetime;
        public string  updatedBy;
        public string  updatedPc;
        public DateTime ? updatedDatetime;

    }
}
