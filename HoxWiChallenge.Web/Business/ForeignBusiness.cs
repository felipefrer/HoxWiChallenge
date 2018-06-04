using HoxWi.Db;
using HoxWiChallenge.Web.Business.Interfaces;
using HoxWiChallenge.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;

namespace HoxWiChallenge.Web.Business
{
    public class ForeignBusiness : BusinessBase<Foreign>, IForeignBusiness
    {
        #region Constructors

        public ForeignBusiness(IClient client, string secretKey)
            :base(client, "Foreign", secretKey)
        { }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchPhrase"></param>
        /// <param name="skip"></param>
        /// <param name="rowCount"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public List<Foreign> GetAllForeign(string searchPhrase, int skip, int rowCount, string sort)
        {
            dynamic document = null;

            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                //TODO: Preciso consultar por mais de uma propriedade, tipo utilizar o OR, 
                // pois quando o usuario digitar alguma coisa no searchPhase eu preciso verificar todos os campos.
                document = new { FirstName = searchPhrase };
            }

            List<Foreign> lstForeigns = ParseToList(GetAll(document, 0, sort));

            return lstForeigns.Skip(skip).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Foreign GetForeignById(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var result = GetById(id);

                if (result != null)
                {
                    var lstResult = ParseToList(result);

                    return lstResult[0];
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foreign"></param>
        /// <returns></returns>
        public Tuple<bool, string> InsertNewForeign(Foreign foreign)
        {
            Tuple<bool, string> insertResponse;

            if (!ForeignExist(foreign))
            {
                Insert(foreign);
                insertResponse = new Tuple<bool, string>(true, "Foreign has been included!");
            }
            else
            {
                insertResponse = new Tuple<bool, string>(false, "There has already been a foreign with first name, nationality and birthday included!");
            }

            return insertResponse;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="foreign"></param>
        /// <returns></returns>
        public Tuple<bool, string> UpdateForeign(Foreign foreign)
        {
            Tuple<bool, string> updateResponse;

            if (!ForeignExist(foreign))
            {
                Update(foreign);
                updateResponse = new Tuple<bool, string>(true, "Foreign has been included!");
            }
            else
            {
                updateResponse = new Tuple<bool, string>(false, "There has already been a foreign with first name, nationality and birthday included!");
            }

            return updateResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foreign"></param>
        /// <returns></returns>
        private bool ForeignExist(Foreign foreign)
        {
            /* If there has been already any foreign with first name, nationality and birthday on the hoxwi 
               we can't include another with the same details.*/

            var documentFilter = new { foreign.FirstName, foreign.BirthdayDate, foreign.Nationality };

            var dataResult = GetAll(documentFilter);

            if (dataResult != null && dataResult.Length > 0)
            {
                var existingForeign = ParseToList(dataResult)[0];

                if (foreign._id == existingForeign._id)
                {
                    return false;
                }
            }

            return dataResult.Length > 0;
        }
        
        #endregion
    }
}