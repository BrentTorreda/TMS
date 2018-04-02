using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;
using TaskManager.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.ViewModels
{
    public class CompanyFormViewModel
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int TaskId { get; set; }

        public int TaskCategoryId { get; set; }

        public int TaskTypeId { get; set; }

        public IEnumerable<Tasks> Tasks { get; set; }

        public CompanyFormViewModel()
        {

        }

        public CompanyFormViewModel(Companies companies)
        {
            CompanyId = companies.CompanyId;
            CompanyName = companies.CompanyName;
            Email = companies.Email;
            Phone = companies.Phone;        

        }
    }
}