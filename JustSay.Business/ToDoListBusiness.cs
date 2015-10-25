using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justsay.Models;
using JustSay.Common.DotNetCode;

namespace JustSay.Business
{
    public class ToDoListBusiness : BaseBusiness<ToDoList>, IToDoListBusiness
    {
        public ToDoListBusiness(DbContext db)
            : base(db)
        {

        }

       

        public  static bool WriteLog(string msg,string logClass,string className , bool IsAddToList=false,bool IsSms=false)
        {

            if (IsAddToList)
            {
                ToDoList entity = new ToDoList
                {
                    Content = msg,
                    From = className,
                    Time = DateTime.Now,
                    Status = 3
                };
                 JustSayEntities db=new JustSayEntities();
                 db.ToDoLists.Add(entity);
                 db.SaveChanges();
#warning 通知
            }
             LogHelper.WriteLog(msg, logClass, className);
             return false;
        }

        public override ToDoList Add(ToDoList entity)
        {
#warning 通知
            return base.Add(entity);
        }
    }

}
