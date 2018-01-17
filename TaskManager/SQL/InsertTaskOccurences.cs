using System;
using System.Linq;
using TaskManager.Models;
using System.Data.SqlClient;

namespace TaskManager.SQL
{
    public class InsertTaskOccurences
    {

        private ApplicationDbContext _context;

        public InsertTaskOccurences()
        {
            _context = new ApplicationDbContext();
        }

        public int InsertOccurrence(int id, DateTime startDate)
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
            SqlParameter param2 = new SqlParameter("param2", startDate);
            int newTaskId = _context.Database.SqlQuery<Int32>("sp_addtaskoccurrence @param1, @param2", param1, param2).FirstOrDefault();

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

        public DayOfWeek[] SetDaysOfWeekArray(int[] weekDays)
        {
            int i = 0;
            int i2 = 0;

            //get max number of days checked for array
            foreach (int weekDay in weekDays)
            {
                if (weekDay == 1)
                    i++;
            }

            DayOfWeek[] daysInWords = new DayOfWeek[i];

            if (weekDays[0] == 1)
                daysInWords[i2++] = DayOfWeek.Sunday;
            if (weekDays[1] == 1)
                daysInWords[i2++] = DayOfWeek.Monday;
            if (weekDays[2] == 1)
                daysInWords[i2++] = DayOfWeek.Tuesday;
            if (weekDays[3] == 1)
                daysInWords[i2++] = DayOfWeek.Wednesday;
            if (weekDays[4] == 1)
                daysInWords[i2++] = DayOfWeek.Thursday;
            if (weekDays[5] == 1)
                daysInWords[i2++] = DayOfWeek.Friday;
            if (weekDays[6] == 1)
                daysInWords[i2++] = DayOfWeek.Saturday;

            return daysInWords;
        }

        // WEEKLY
        public DateTime[] GetWeeklyOccurrenceDates(int repetition, int repeatEvery, int[] weekDays, DateTime startDate, DateTime endDate)
        {
            if (repetition < 1)            
                return null;                      

            DateTime[] insertDates = new DateTime[repetition];

            //get days of week in an array
            DayOfWeek[] daysRepeated = SetDaysOfWeekArray(weekDays);

            int i = 0;
            DateTime tempDate = startDate;
            do
            {
                foreach (DayOfWeek day in daysRepeated)
                {
                    if (tempDate.DayOfWeek == day)
                    {
                        if (i < repetition) //1/17/18 - BTo - make sure it doesn't exceed array size
                        {
                            insertDates[i++] = tempDate;
                        }
                    }  
                }
                if (tempDate.DayOfWeek == DayOfWeek.Sunday) //if sunday check how many weeks have to be skipped
                {
                    for (int r = 1; r < repeatEvery; r++) //1 since it already repeats every week
                    {
                        tempDate = tempDate.AddDays(7); 
                    }                   
                }
                tempDate = tempDate.AddDays(1);                            
            }
            while (tempDate <= endDate); //must not exceed end date specified            
            return insertDates;
        }

        // MONTHLY
        public bool InsertTasks(int taskId, int insertType, int repeatEvery, int[] weekDays, int repetition, DateTime startDate, DateTime endDate)
        {
            if (repetition < 1)
                return false;

            //weekly
            if (insertType == 0)
            {
                DateTime[] insertDates = new DateTime[repetition];

                insertDates = GetWeeklyOccurrenceDates(repetition, repeatEvery, weekDays, startDate, endDate);

                for (var i = 1; i <= repetition; i++)
                {
                    if (insertDates[i-1] != null ) //date might be null if enddate set could not accommodate the max number of repetitions
                        InsertOccurrence(taskId, insertDates[i-1]);
                }
            }

            return true;
        }
    }
}