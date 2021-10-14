using Alfatraining.Vertrag.Db.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestProject.Models
{
    public class Animal : IEntity, ICloneable
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime DateOfArrival { get; set; }
        public bool Dangerous { get; set; }
        public string Color { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
