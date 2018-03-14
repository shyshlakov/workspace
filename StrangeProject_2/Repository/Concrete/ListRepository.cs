using Microsoft.EntityFrameworkCore;
using StrangeProject_2.Models;
using StrangeProject_2.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrangeProject_2.Repository.Concrete
{
    public class ListRepository : IListRepository, IDisposable
    {
        public readonly ListContext _myDbContext;

        public ListRepository(ListContext context)
        {
            _myDbContext = context;
        }

        public List<ListModel> GetList()
        {
            return _myDbContext.MyList.ToList();
        }

        public void InsertRow(ListModel model)
        {
            _myDbContext.MyList.Add(model); //!!!!
        }

        public void UpdateRow(ListModel model)
        {
            _myDbContext.Entry(model).State = EntityState.Modified;
        }

        public void DeleteRow(int rowId)
        {
            ListModel row = _myDbContext.MyList.Find(rowId);
            _myDbContext.MyList.Remove(row);
        }

        public void Save()
        {
            _myDbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _myDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
