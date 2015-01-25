using sample.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace sample.Service
{
    public class TermService : BaseService
    {
        public static List<TermResponseModel> GetTerms(string term = null)
        {
            List<TermResponseModel> model = null;
            bool isEdit = false;
            string storedProc = "dbo.GetTermsList";

            List<SqlParameter> collection = null;
            if (!string.IsNullOrEmpty(term))
            {
                isEdit = true;
                storedProc = "dbo.GetEditableTerm";
                collection = new List<SqlParameter>();
                collection.Add(CreateParameter("@Term", term, SqlDbType.VarChar, ParameterDirection.Input));
            }

            Func<IDataRecord, TermResponseModel> termReader = delegate(IDataRecord record)
            {
                TermResponseModel resp = new TermResponseModel();
                resp.Term = record.GetString(0);
                resp.Definition = record.GetString(1);
                resp.ExternalId = record.GetGuid(2);

                return resp;
            };

            try
            {
                model = ExecuteReader<TermResponseModel>("Sample", storedProc, termReader, collection);
                if ((model == null || model.Count != 1) && isEdit)
                {
                    throw new Exception("Search results came back wonky.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }

        public static Guid EditTerm(TermRequestModel model)
        {
            bool isEdit = false;
            string storedProc = "dbo.Terms_Insert";

            List<SqlParameter> collection = new List<SqlParameter>();
            collection.Add(CreateParameter("@Term", model.Term, SqlDbType.VarChar, ParameterDirection.Input));
            collection.Add(CreateParameter("@Definition", model.Definition, SqlDbType.VarChar, ParameterDirection.Input));

            if (model.ExternalId.HasValue && model.ExternalId.Value != Guid.Empty)
            {
                isEdit = true;
                storedProc = "dbo.EditTerm";
                collection.Add(CreateParameter("@UID", model.ExternalId, SqlDbType.UniqueIdentifier, ParameterDirection.Input));
            }
            else
            {
                collection.Add(CreateParameter("@newUId", Guid.Empty, SqlDbType.UniqueIdentifier, ParameterDirection.InputOutput));
            }

            try
            {
                ExecuteNonQuery("Sample", storedProc, collection);
                if (isEdit)
                {
                    return model.ExternalId.Value;
                }
                else
                {
                    return Guid.Parse(collection.SingleOrDefault(x => x.ParameterName == "@newUId").Value.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteTerm(TermRequestModel model)
        {
            List<SqlParameter> collection = new List<SqlParameter>();
            collection.Add(CreateParameter("@UID", model.ExternalId, SqlDbType.UniqueIdentifier, ParameterDirection.Input));

            try
            {
                ExecuteNonQuery("Sample", "dbo.DeleteTerm", collection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}