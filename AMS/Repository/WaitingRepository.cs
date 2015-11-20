using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Models;

namespace AMS.Repository
{
    public class WaitingRepository : IRepository<Waiting>
    {
        private readonly DatabaseContext _databaseContext;
        public WaitingRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(Waiting model)
        {
            _databaseContext.Waitings.Add(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Remove(Waiting model)
        {
            _databaseContext.Waitings.Remove(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Update(Waiting model)
        {
            throw new NotImplementedException();
        }

        public List<Waiting> GetAll()
        {
            throw new NotImplementedException();
        }

        public Waiting Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Waiting> GetAllByDoctor(int doctorId)
        {
            return _databaseContext.Waitings.Where(e => e.DoctorId == doctorId).ToList();
        }

        public List<Waiting> GetAllByDoctorByDate(int doctorId, String date)
        {
            return _databaseContext.Waitings.Where(e => e.DoctorId == doctorId && e.Date == date).ToList();
        }

    }
}