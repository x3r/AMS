using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AMS.Models;

namespace AMS.Repository
{
    public class AddressRepository : IRepository<Address>
    {
        private readonly DatabaseContext _databaseContext;

        public AddressRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(Address model)
        {
            _databaseContext.Addresses.Add(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Remove(Address model)
        {
            throw new NotImplementedException();
        }

        public bool Update(Address model)
        {
            var oldModel = Get(model.AddressId);
            oldModel.HouseNo = model.HouseNo;
            oldModel.RoadNo = model.RoadNo;
            oldModel.Thana = model.Thana;
            oldModel.Zilla = model.Zilla;
            _databaseContext.Entry(oldModel).State = EntityState.Modified;
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Update(Address model, int userId)
        {
            var oldModel = GetByUser(userId);
            oldModel.HouseNo = model.HouseNo;
            oldModel.RoadNo = model.RoadNo;
            oldModel.Thana = model.Thana;
            oldModel.Zilla = model.Zilla;
            _databaseContext.Entry(oldModel).State = EntityState.Modified;
            return _databaseContext.SaveChanges() > 0;
        }

        public List<Address> GetAll()
        {
            throw new NotImplementedException();
        }

        public Address Get(int id)
        {
            return _databaseContext.Addresses.Find(id);
        }
        public Address GetByUser(int id)
        {
            return _databaseContext.Addresses.FirstOrDefault(u => u.UserId == id);
        }
    }
}