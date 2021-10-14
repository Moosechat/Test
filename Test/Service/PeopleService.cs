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
    public class PeopleService 
    {
        EFGenericRepository<People> mTsetRepository { get; set; }
        private EFDbContext _dbContext;

        public PeopleService(EFDbContext _dbCon)
        {
            _dbContext = _dbCon;
            this.mTsetRepository = new EFGenericRepository<People>(_dbCon);
        }
        public async Task<List<PeopleViewModel>> Get()
        {
            var items = mTsetRepository.Get().Select(r => Conver(r)).ToList();
            return await Task.FromResult(items);
        }
        private static PeopleViewModel Conver(People r)
        {
            return new PeopleViewModel(r);
        }
        public PeopleViewModel Update(PeopleViewModel item)
        {
            var x = mTsetRepository.FindByIdForReload(item.PeopleId);
            x.PeopleId = item.PeopleId;
            x.Name = item.Name;
            x.Surname = item.Surname;
            x.Otchestvo = item.Otchestvo;
            x.Location = item.Location;
            x.Date = item.Date;
            x.AnimalId = item.AnimalId;
            x.Img = item.Img;

            return Conver(mTsetRepository.Update(x, item.Item.RowVersion));
        }
        public PeopleViewModel Create(PeopleViewModel item)
        {
            var newItem = mTsetRepository.Create(item.Item);
            return Conver(newItem);
        }
        public PeopleViewModel ReloadItem(PeopleViewModel item)
        {
            var x = Conver(mTsetRepository.Reload(item.PeopleId));
            return x;
        }
        public void Delete(PeopleViewModel item)
        {
            var x = mTsetRepository.FindById(item.PeopleId);
            mTsetRepository.Remove(x);
        }
        public List<PeopleViewModel> GetOrderByUserID(int id)
        {
           //int b = Convert.ToInt32(id);
            var items = _dbContext.RepoGetUserId(id).Select(r => Conver(r)).ToList();
            return (items);
        }
    }
}
