using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Model;

namespace Test.Pages.People
{
    public class DeleteItemViewModelList : ComponentBase
    {
        public bool DialogIsOpen { get; set; }
        public PeopleViewModel Model { get; set; }
    }
}
