using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JustSay.Business
{
    public interface IBusiness<T>:IDisposable where T:class,new()
    {
        T Add(T entity);
        void AddUnSubmit(T entity);
        int Update(T entity);
        void UpdateUnSubmit(T entity);
        int Delete(T entity);
        void DeleteUnSubmit(T entity);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        int Delete(params int[] ids);
        void DeleteUnSubmit(params int[] ids);
        T GetDetail(int id);
        /// <summary>
        /// 得到第一个符合条件的值
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        T GetDetail(Func<T,bool> whereLambda);
        /// <summary>
        /// 得到可查询的数据源   延迟加载
        /// </summary>
        /// <returns></returns>
        IQueryable<T> LoadEntities();
        /// <summary>
        /// 得到满足条件的可查询的数据源   延迟加载
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<T> LoadEntities(Func<T, bool> whereLambda); 
        /// <summary>
        /// 得到强类型列表,由于用了MVCPager实现该方式,所以生成的是List
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="orderbyLambda"></param>
        /// <returns></returns>
        List<T> LoadPageEntities<S>(int pageSize, int pageIndex, Func<T, S> orderbyLambda);
        List<T> LoadPageEntities<S>(int pageSize, int pageIndex, Func<T, bool> whereLambda , Func<T, S> orderbyLambda);
        List<O> LoadPageEntities<S, O>(int pageSize, int pageIndex, Func<T, S> orderbyLambda, Func<T, O> selectLambda);
        List<O> LoadPageEntities<S, O>(int pageIndex, int pageSize, Func<T, bool> whereLambda, Func<T, S> orderbyLambda, Func<T, O> selectLambda);
        int Count(Func<T, bool> whereLambda);
        int Submit();
        
    }
}