using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Model;

namespace Test.Pages.Animal
{
    public class EditAnimalViewModel
    {
        public bool DialogIsOpen { get; set; }
        public bool IsConcurrencyError { get; set; } = false;
        public AnimalViewModel Model { get; set; }
    }
}
