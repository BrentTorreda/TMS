using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;

namespace TaskManager.Controllers.SQL
{
    public class InsertEndlessTask
    {
        private ApplicationDbContext _context;

        public InsertEndlessTask()
        {
            _context = new ApplicationDbContext();
        }
    }
}