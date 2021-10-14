using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;

namespace Test.Model
{
    public class PeopleViewModel
    {
        private People _item;

        public PeopleViewModel()
        {
            _item = new People();
        }

        public PeopleViewModel(People model)
        {
            _item = model;
        }

        public People Item => _item;

        public int PeopleId
        {
            get
            {
                return _item.PeopleId;
            }
            set
            {
                _item.PeopleId = value;
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
        public string Surname
        {
            get
            {
                return _item.Surname;
            }
            set
            {
                _item.Surname = value;
            }
        }
        public string Otchestvo
        {
            get
            {
                return _item.Otchestvo;
            }
            set
            {
                _item.Otchestvo = value;
            }
        }
        public DateTime Date
        {
            get
            {
                return _item.Date;
            }
            set
            {
                _item.Date = value;
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
        public string animalIdToString
        {
            get
            {
                return _item.AnimalId.ToString();
            }
            set
            {
                _item.AnimalId = Convert.ToInt32(value);
            }
        }
        public byte[]? Img
        {
            get
            {
                return _item.Img;
            }
            set
            {
                _item.Img = value;
            }
        }
        public bool IsRefreshed { get; set; } = false;
        public object Clone()
        {
            PeopleViewModel tempObject = (PeopleViewModel)this.MemberwiseClone();
            tempObject._item = (People)_item.Clone();
            return tempObject;
        }
    }
}
