using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppScanning.Models
{
    public class Roster
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public byte[] Picture { get; set; }
    }
}
