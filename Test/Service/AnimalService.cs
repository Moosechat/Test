using Alfatraining.Vertrag.Db;
using Alfatraining.Vertrag.Db.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Model;
using TestProject.Models;

namespace Test.Service
{
    public class AnimalService
    {
        EFGenericRepository<Animal> mTsetRepository { get; set; }

        public AnimalService(EFDbContext _dbContext)
        {
            this.mTsetRepository = new EFGenericRepository<Animal>(_dbContext);
        }
        public async Task<List<AnimalViewModel>> Get()
        {
            var items = mTsetRepository.Get().Select(r => Convert(r)).ToList();
            return await Task.FromResult(items);
        }
        private static AnimalViewModel Convert(Animal r)
        {
            return new AnimalViewModel(r);
        }
        public AnimalViewModel Update(AnimalViewModel item)
        {
            var x = mTsetRepository.FindByIdForReload(item.AnimalId);
            x.AnimalId = item.AnimalId;
            x.Name = item.Name;
            x.Location = item.Location;
            x.DateOfArrival = item.DateOfArrival;
            x.Dangerous = item.Dangerous;
            x.Color = item.Color;

            return Convert(mTsetRepository.Update(x, item.Item.RowVersion));
        }
        public AnimalViewModel Create(AnimalViewModel item)
        {
            var newItem = mTsetRepository.Create(item.Item);
            return Convert(newItem);
        }
        public AnimalViewModel ReloadItem(AnimalViewModel item)
        {
            var x = Convert(mTsetRepository.Reload(item.AnimalId));
            return x;
        }
        public void Delete(AnimalViewModel item)
        {
            var x = mTsetRepository.FindById(item.AnimalId);
            mTsetRepository.Remove(x);
        }

    }
}
