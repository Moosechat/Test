using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestProject.Models
{
    public class People
    {
        public int PeopleId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Otchestvo { get; set; }
        public string Location { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public DateTime Date { get; set; }
        public int AnimalId { get; set; }
        public byte[]? Img { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
