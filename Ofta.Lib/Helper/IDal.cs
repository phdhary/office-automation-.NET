using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Helper
{
    public interface IInsert<TEntity>
    {
        void Insert(TEntity entity);
    }

    public interface IUpdate<TEntity>
    {
        void Update(TEntity entity);
    }
    public interface IDelete<TKey>
    {
        void Delete(TKey key);
    }
    public interface IGetData<TModel, TKey>
    {
        TModel GetData(TKey key);
    }

    public interface IListData<TModel>
    {
        IEnumerable<TModel> ListData();
    }

    public interface IListFilter<TModel, TFilter>
    {
        IEnumerable<TModel> ListData(TFilter filter);
    }


    public interface ISearch<T>
    {
        IEnumerable<T> Search(string keyword);
    }

    public interface IListPeriode<T>
    {
        IEnumerable<T> ListData(DateTime tgl1, DateTime tgl2);
    }
}
