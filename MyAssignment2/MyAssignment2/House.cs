using System;
using System.Collections.Generic;
using System.Text;

namespace MyAssignment2
{
    public class House : Entity
    {
        public string HouseName { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
