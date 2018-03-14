using StrangeProject_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrangeProject_2.Repository.Abstract
{
    public interface IListRepository : IDisposable
    {
        List<ListModel> GetList();
        void InsertRow(ListModel model);
        void DeleteRow(int rowId);
        void UpdateRow(ListModel model);
        void Save();
    }
}
