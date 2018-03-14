using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StrangeProject_2.Models;
using StrangeProject_2.Repository.Abstract;
using StrangeProject_2.Repository.Concrete;

namespace StrangeProject_2.Controllers
{
    public class HomeController : Controller
    {

        private readonly IListRepository _repository;

        public HomeController(IListRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var list = _repository.GetList().OrderByDescending(x => x.Index);
            return View(list);
        }

        public void UpdatePosition(int id, int newIndex)
        {
            int tempIndex;

            var firstItem = _repository.GetList().Where(x => x.Id == id).FirstOrDefault();
            var secondItem = _repository.GetList().Where(x => x.Index == newIndex).FirstOrDefault();

            tempIndex = firstItem.Index;

            firstItem.Index = newIndex;
            secondItem.Index = tempIndex;

            _repository.UpdateRow(firstItem);
            _repository.UpdateRow(secondItem);
            _repository.Save();
        }

        public ListModel AddRow(string value)
        {
            ListModel model = new ListModel();
            var maxIndex = _repository.GetList().Max(x => x.Index);
            model.Index = ++maxIndex;
            model.Text = value;

            _repository.InsertRow(model);
            _repository.Save();

            return model;
        }

        public void EditRow(int id, string value)
        {
            var model = _repository.GetList().Where(x => x.Id == id).FirstOrDefault();
            model.Text = value;
            _repository.UpdateRow(model);
            _repository.Save();
        }

        public void DeleteRow(int id)
        {
            _repository.DeleteRow(id);
            _repository.Save();
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
