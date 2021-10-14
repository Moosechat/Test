using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Model;
using Test.Pages.Animal;
using Test.Service;
using Test.Shared;

namespace Test.Pages.People
{
    public class PeopleListViewModel : ComponentBase
    {
        protected List<PeopleViewModel> Model { get; set; } = new List<PeopleViewModel>();
        [Inject] protected PeopleService PService { get; set; }
        [Inject] protected AnimalService AService { get; set; }
        protected PeopleViewModel currentItem;
        public int getId = 0;
        protected ErrorWindowViewModel newEditViewModel = new ErrorWindowViewModel();
        protected InformarionDialogViewModel mInformationDialog = new InformarionDialogViewModel();
        protected EditPeopleViewModel mEditViewModel = new EditPeopleViewModel();
        protected DeleteItemViewModelList DeleteViewModel = new DeleteItemViewModelList();
        protected List<AnimalViewModel> AnimalModel { get; set; } = new List<AnimalViewModel>();
        public string FValue { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Model = await PService.Get();
                AnimalModel = await AService.Get();
            }
            StateHasChanged();
        }
        public async Task Reset()
        {
            Model.Clear();
            Model = await PService.Get();
            StateHasChanged();
        }
        public void CreateItem()
        {
            currentItem = new PeopleViewModel();
            mEditViewModel.Model = currentItem;
            mEditViewModel.DialogIsOpen = true;
        }
        protected void Save(PeopleViewModel item)
        {
            try
            {
                if (item.AnimalId > 0)
                {
                    var newItem = PService.Update(item);
                    mEditViewModel.DialogIsOpen = false;
                    var index = Model.FindIndex(x => x.AnimalId == this.currentItem.AnimalId);
                    Model[index] = newItem;
                    StateHasChanged();

                }
                else
                {
                    var newItem = PService.Create(item);
                    mEditViewModel.DialogIsOpen = false;
                    if (newItem != null)
                    {
                        Model.Add(newItem);
                    }
                }
                StateHasChanged();
                Console.WriteLine("Animal saved successfully.");
            }
            catch (Exception)
            {
                currentItem = PService.ReloadItem(item);
                item.IsRefreshed = true;
                CloseInformationDialog();
                Error();
                StateHasChanged();
            }

        }
        public void ReloadItem(PeopleViewModel item)
        {

            var reloadItem = PService.ReloadItem(item);
            reloadItem.IsRefreshed = false;

            if (mEditViewModel.DialogIsOpen)
            {
                mEditViewModel.Model = reloadItem;
            }
            if (reloadItem.Item == null)
            {
                mEditViewModel.DialogIsOpen = false;
                var index = Model.FindIndex(x => x.AnimalId == item.AnimalId);
                Model.RemoveAt(index);
            }
            else
            {
                mEditViewModel.IsConcurrencyError = false;
                var index = Model.FindIndex(x => x.AnimalId == item.AnimalId);
                Model[index] = reloadItem;
            }
            StateHasChanged();
        }
        protected void CloseInformationDialog()
        {
            mInformationDialog = new InformarionDialogViewModel();
            if (mEditViewModel?.DialogIsOpen == true)
            {
                mEditViewModel.DialogIsOpen = false;
            }
        }
        protected void Error()
        {
            newEditViewModel.DialogIsOpen = true;
        }
        public async Task Filtering()
        {
            Model.Clear();
            Model = await PService.Get();
            foreach(var i in AnimalModel)
            {
                if(FValue == i.Name)
                {
                    Model = Model.Where(x => x.AnimalId == i.AnimalId).ToList();
                }
            }
            StateHasChanged();
        }
        protected void Edit(PeopleViewModel r)
        {
            currentItem = (PeopleViewModel)r.Clone();
            mEditViewModel.Model = currentItem;
            mEditViewModel.DialogIsOpen = true;
        }
        protected void DeleteItem(PeopleViewModel r)
        {
            currentItem = (PeopleViewModel)r.Clone();
            DeleteViewModel.Model = currentItem;
            DeleteViewModel.DialogIsOpen = true;
        }
        protected void DeletePosition(PeopleViewModel item)
        {
            PService.Delete(item);
            DeleteViewModel.DialogIsOpen = false;
            Model.Remove(item);
            StateHasChanged();
        }
        protected int GetOrderByUserID
        {
            get => getId;
            set { getId = value; FilterId(); }
        }
        public async Task FilterId()
        {
            Model.Clear();
            if (getId == 0)
            {
                Model.Clear();
                Model = await PService.Get();
                StateHasChanged();
            }
            else
            {
                Model = PService.GetOrderByUserID(getId);
            }
            
        }
        
    }
}
