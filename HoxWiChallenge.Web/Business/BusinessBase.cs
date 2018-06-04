using HoxWi.Db;
using HoxWiChallenge.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Configuration;

namespace HoxWiChallenge.Web.Business
{
    public class BusinessBase<TEntity> : IBusiness<TEntity> where TEntity : class
    {
        #region Constructors

        public BusinessBase(IClient client, string container, string secretKey)
        {
            _hoxWiClient = client;
            _container = container;
            _secretKey = secretKey;
        }

        #endregion

        #region Properties

        private readonly IClient _hoxWiClient;
        private readonly string _container;
        private readonly string _secretKey;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <param name="limit"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public dynamic[] GetAll(dynamic document = null, int limit = 0, string orderby = "Id")
        {
            //TODO Apenas as sobrecargas que recebem a secretKey como parâmetro estão funcionando.
            //TODO: Esse método deve implementar skip() and Take();
            //TODO: Parâmetro orderBy deve aceitar ASC or DESC.
            //TODO: Não consigo filtrar passando OR como filtro. Ex: FirstName == "Felipe" or Nationality == "Felipe".
            //TODO: Não consigo utilizar o contains na consulta... Dynamic Linq seria uma opção.
            //TODO: Como utilizar o predicate.
            
            // Retrieving data from Foreign container on HoxWi.
            var searchForeignHoxWi = new SearchRequest(_container, _secretKey, limit, orderby);

            if (document != null)
            {
                searchForeignHoxWi.document = document;
            }
            
            var hoxWiSearchResult = _hoxWiClient.Search(searchForeignHoxWi);

            return hoxWiSearchResult.Results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hid"></param>
        /// <returns></returns>
        public dynamic GetById(string hid)
        {
            var document = new { _id = hid };

            var searchForeignHoxWi = new SearchRequest(_container, document);
            var hoxWiSearchResult = _hoxWiClient.GetById(searchForeignHoxWi);

            return hoxWiSearchResult.Results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foreign"></param>
        public void Insert(TEntity foreign)
        {
            var insertForeignHoxWi = new InsertRequest(_container, foreign);

            _hoxWiClient.Add(insertForeignHoxWi);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hid"></param>
        public void Remove(string hid)
        {
            var deleteSearchRequest = new SearchRequest(_container, new { _id = hid });

            _hoxWiClient.Delete(deleteSearchRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            var updateForeignHoxWi = new UpdateRequest(_container, entity);

            _hoxWiClient.Update(updateForeignHoxWi);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual List<TEntity> ParseToList(dynamic data)
        {
            var jsonResult = JsonConvert.SerializeObject(data);
            var dataParse = JsonConvert.DeserializeObject<List<TEntity>>(jsonResult);

            return dataParse;
        }

        #endregion
    }
}