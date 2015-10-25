using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Objects;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Justsay.Models;
using Webdiyer.WebControls.Mvc;
namespace JustSay.Business
{
    public class BaseBusiness<T>:IBusiness<T> where T:class,IModel,new()
    {
        public DbContext db;
         public BaseBusiness(DbContext dbContext)
        {
            db = dbContext;
        }
        #region IRepository<T> 成员
        public virtual T Add(T entity)  
        {
           db.Set<T>().Add(entity);
           try
           {
               db.SaveChanges();
           }
           catch
           {
              
                   //throw;
                   db.Configuration.ValidateOnSaveEnabled = false;
                   db.SaveChanges();
                   db.Configuration.ValidateOnSaveEnabled = true;

           }
            return entity;  
        }

        public void AddUnSubmit(T entity)
       {
           db.Set<T>().Add(entity);
       }
        public virtual int Update(T entity)  
        {  
            //可以不用附加：   
           // db.Set<T>().Attach(entity);// 内部就是只是把实体的 状态改成unchange跟数据库查询出来的状态时一样的。  
            Debug.WriteLine(db.Entry(entity).State.ToString(), "entity state");
            db.Entry(entity).State = EntityState.Modified;
            try
            {
               
                return db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                Debug.WriteLine("异常：", dbEx.Message);
              //  throw;
                db.Configuration.ValidateOnSaveEnabled = false;
                return db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
            }
 
        }
        public void UpdateUnSubmit(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
        public virtual int Delete(T entity)  
        {  
            db.Entry(entity).State = EntityState.Deleted;
            return db.SaveChanges();
  
        }
        public void DeleteUnSubmit(T entity)
        {
            db.Entry(entity).State = EntityState.Deleted;
        }
        public virtual T GetDetail(int id)
        {
            return db.Set<T>().Find(id);//如果实体已经在内存中，那么就直接从内存拿，如果内存中跟踪实体没有，那么才查询数据库。 
        }
        public virtual T GetDetail(Func<T, bool> whereLambda)
        {
            return db.Set<T>().FirstOrDefault(whereLambda);
        }
        public virtual int Delete(params int[] ids)  
        {  
            foreach (int item in ids)  
            {  
              //方法1
              //  var entity = db.Set<T>().Find(item); 
              //  if(entity!=null)db.Set<T>().Remove(entity);  //方法1 end
              //方法2
                T entity = new T();           
                entity.ID = item; //此方法不查询数据库，只在最后SaveChanges的时候生成Delete语句，性能更好，缺点：当id不存在时有异常
                db.Entry(entity).State = EntityState.Deleted;  //方法2 end
            }
            return db.SaveChanges();
  
        }

        public void DeleteUnSubmit(params int[] ids)
        {
            foreach (int item in ids)
            {
                //方法1
                //  var entity = db.Set<T>().Find(item); 
                //  if(entity!=null)db.Set<T>().Remove(entity);  //方法1 end
                //方法2
                T entity = new T();
                entity.ID = item; //此方法不查询数据库，只在最后SaveChanges的时候生成Delete语句，性能更好，缺点：当id不存在时有异常
                db.Entry(entity).State = EntityState.Deleted;  //方法2 end
            }
        }
        public IQueryable<T> LoadEntities()
        {
            return db.Set<T>().AsQueryable();
        }
  
        public virtual IQueryable<T> LoadEntities(Func<T, bool> whereLambda)  
        {  
            return db.Set<T>().Where(whereLambda).AsQueryable();  
        }
        public virtual List<T> LoadPageEntities<S>(int pageSize, int pageIndex, Func<T, S> orderbyLambda)
        {
            return db.Set<T>().OrderByDescending(orderbyLambda).ToPagedList(pageIndex, pageSize);
        }
        public virtual List<T> LoadPageEntities<S>(int pageSize,int pageIndex,  Func<T, bool> whereLambda, Func<T, S> orderbyLambda)  
        {  
           return db.Set<T>().Where(whereLambda).OrderByDescending(orderbyLambda).ToPagedList(pageIndex,pageSize);  
  
        }
        public virtual List<O> LoadPageEntities<S, O>(int pageSize, int pageIndex, Func<T, S> orderbyLambda, Func<T, O> selectLambda)
        {

            return db.Set<T>().OrderByDescending(orderbyLambda).Select(selectLambda).ToPagedList(pageIndex, pageSize);
        }
        public virtual List<O> LoadPageEntities<S, O>(int pageSize,int pageIndex,  Func<T, bool> whereLambda, Func<T, S> orderbyLambda, Func<T, O> selectLambda)
        {
            return db.Set<T>().Where(whereLambda).OrderByDescending(orderbyLambda).Select(selectLambda).ToPagedList(pageIndex, pageSize);

        }
        public int Count(Func<T, bool> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).Count();
        }
        public void Dispose()
        {
            db.Dispose();

        }
        public int Submit()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                Debug.WriteLine("异常：", dbEx.Message);
                return 0;
            }
            catch
            {
               
                //throw;
                db.Configuration.ValidateOnSaveEnabled = false;
                int result= db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
                return result;
            }
        }

        #endregion
    }
}