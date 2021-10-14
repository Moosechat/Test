using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Model;
using Test.Shared;
using Test.Service;
using System.Data.Entity.Infrastructure;

namespace Test.Pages.Animal
{
    public class AnimalListViewModel : ComponentBase
    {
        protected List<AnimalViewModel> Model { get; set; } = new List<AnimalViewModel>();
        [Inject] protected AnimalService Service { get; set; }
        protected AnimalViewModel currentItem;
        protected EditAnimalViewModel mEditViewModel = new EditAnimalViewModel();
        protected InformarionDialogViewModel mInformationDialog = new InformarionDialogViewModel();
        protected ErrorWindowViewModel newEditViewModel = new ErrorWindowViewModel();
        protected InformarionDialogViewModel newInformationDialog = new InformarionDialogViewModel();
        private bool isDangerous = false;
        public bool IsCatched { get; set; } = false;
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Model = await Service.Get();
                Reset();
            }
            StateHasChanged();
        }
        public void CreateItem()
        {
            currentItem = new AnimalViewModel();
            mEditViewModel.Model = currentItem;
            mEditViewModel.DialogIsOpen = true;
        }
        protected void Save(AnimalViewModel item)
        {
            try
            {
                if (item.AnimalId > 0)
                {
                    var newItem = Service.Update(item);
                    mEditViewModel.DialogIsOpen = false;
                    var index = Model.FindIndex(x => x.AnimalId == this.currentItem.AnimalId);
                    Model[index] = newItem;
                    StateHasChanged();

                }
                else
                {
                    var newItem = Service.Create(item);
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
                currentItem = Service.ReloadItem(item);
                item.IsRefreshed = true;
                CloseInformationDialog();
                Error();
                StateHasChanged();
            }
            
        }
        public void ReloadItem(AnimalViewModel item)
        {

            var reloadItem = Service.ReloadItem(item);
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
        protected void Edit(AnimalViewModel r)
        {
            currentItem = (AnimalViewModel)r.Clone();
            mEditViewModel.Model = currentItem;
            mEditViewModel.DialogIsOpen = true;
        }
        protected void Error()
        {
            newEditViewModel.DialogIsOpen = true;
        }
        protected void DeletePosition(AnimalViewModel item)
        {
            Service.Delete(item);
            Model.Remove(item);
        }
        public async Task Filtering()
        {
            Model.Clear();
            Model = await Service.Get();
            if (isDangerous == false)
            {
                Model = Model.Where(x => x.Dangerous == true).ToList();
            }
            else
            {
                Model = Model.Where(x => x.Dangerous == false).ToList();
            }
            StateHasChanged();
        }
        public async Task Reset()
        {
            Model.Clear();
            Model = await Service.Get();
            StateHasChanged();
        }
        protected bool IsDangerous
        {
            get { return isDangerous; }
            set { isDangerous = value; Filtering(); }
        }
    }
}
