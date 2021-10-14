using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;

namespace Test.Model 
{
    public class AnimalViewModel : ICloneable
    {
        private Animal _item;

        public AnimalViewModel()
        {
            _item = new Animal();
        }

        public AnimalViewModel(Animal model)
        {
            _item = model;
        }

        public Animal Item => _item;

        public int AnimalId
        {
            get
            {
                return _item.AnimalId;
            }
            set
            {
                _item.AnimalId = value;
            }
        }
        public string Name
        {
            get
            {
                return _item.Name;
            }
            set
            {
                _item.Name = value;
            }
        }
        public string Location
        {
            get
            {
                return _item.Location;
            }
            set
            {
                _item.Location = value;
            }
        }
        public bool Dangerous
        {
            get
            {
                return _item.Dangerous;
            }
            set
            {
                _item.Dangerous = value;
            }
        }
        public DateTime DateOfArrival
        {
            get
            {
                return _item.DateOfArrival;
            }
            set
            {
                _item.DateOfArrival = value;
            }
        }
        public string Color
        {
            get
            {
                return _item.Color;
            }
            set
            {
                _item.Color = value;
            }
        }
        public bool IsRefreshed { get; set; } = false;
        public object Clone()
        {
            AnimalViewModel tempObject = (AnimalViewModel)this.MemberwiseClone();
            tempObject._item = (Animal)_item.Clone();
            return tempObject;
        }
    }
}
