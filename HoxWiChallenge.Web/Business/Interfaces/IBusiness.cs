using System.Collections.Generic;

namespace HoxWiChallenge.Web.Business
{
    public interface IBusiness<TEntity> where TEntity : class
    {
        #region Methods

        dynamic[] GetAll(dynamic document = null, int limit = 0, string orderBy = "Id");
        dynamic GetById(string hid);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Remove(string hid);
        List<TEntity> ParseToList(dynamic data);

        #endregion
    }
}
