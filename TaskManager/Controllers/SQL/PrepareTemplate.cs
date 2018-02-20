using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;
using System.Data;
using System.Data.SqlClient;

namespace TaskManager.SQL
{
    public class PrepareTemplate
    {
        private ApplicationDbContext _context;

        public PrepareTemplate()
        {
            _context = new ApplicationDbContext();
        }

        public int InsertTemplate(int id)
        {
            var oldSubTasks = _context.SubTasksLevel1.Where(s => s.TaskId == id).ToList();
            int[] oldSubTaskIds = new int[20];
            int i = 0;
            int? newId;

            //get all original subtask IDs for use in queyring task procs
            foreach (var st in oldSubTasks)
            {
                oldSubTaskIds[i++] = st.SubTaskId;
            }

            //task
            SqlParameter param1 = new SqlParameter("param1", id);
            int newTaskId = _context.Database.SqlQuery<Int32>("sp_addtasktemplate @param1", param1).FirstOrDefault();
            
            //subtask     
            newId = _context.Database.SqlQuery<Int32>("sp_addsubtasktemplate @param2, @param3", new SqlParameter("param2", id), new SqlParameter("param3", newTaskId)).FirstOrDefault();
            var subTasks = _context.SubTasksLevel1.Where(s => s.TaskId == newTaskId).ToList();

            //loop through subtasks and insert the procedures for each
            i = 0;
            foreach (var st in subTasks)
            {
                newId = _context.Database.SqlQuery<Int32>("sp_addtaskproceduretemplate @param4, @param5, @param6", new SqlParameter("param4", oldSubTaskIds[i++]), new SqlParameter("param5", newTaskId), new SqlParameter("param6", st.SubTaskId)).FirstOrDefault();
            }

            return newTaskId;
        }

        public int RollbackInsertTemplate(int id)
        {
            return 0;
        }
    }
}