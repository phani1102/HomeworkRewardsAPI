
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
namespace GRM_Budget.Data.Utility
{
    public class SummitDBUtils
    {
        #region "Variable declaration"
        private SqlConnection m_sqlCon;
        private int m_sqlCommandTimeout = 0;
        private string m_strConnectionString = string.Empty;
        private bool _returnProviderSpecificTypes = false;

        private int _sqlCommandTimeout = 0;
        public int CommandTimeout
        {
            get
            {
                return _sqlCommandTimeout;
            }
            set
            {
                _sqlCommandTimeout = value;
            }
        }

        private string _sqlConnectionString = string.Empty;
        public string ConnectionString
        {
            get
            {
                return _sqlConnectionString;
            }
            set
            {
                _sqlConnectionString = value;
            }
        }
        public Boolean ReturnProviderSpecificTypes
        {
            get
            {
                return _returnProviderSpecificTypes;
            }
            set
            {
                _returnProviderSpecificTypes = value;
            }
        }

        public const string strSuccess = "success";
        #endregion

        #region "Transactional method"
        //private SqlTransaction m_sqlTransaction;
        //public void BeginTransaction(string argStrDBConnName, IConfigurationRoot config)
        //{
        //    try
        //    {

        //        // IConfigurationRoot config;
        //        m_strConnectionString = config.GetSection("AppSettings").GetSection(argStrDBConnName).Value;
        //        m_sqlCon = new SqlConnection(m_strConnectionString);
        //        m_sqlCon.Open();

        //        m_sqlTransaction = m_sqlCon.BeginTransaction();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //    }
        //}
        //public void EndTransaction(bool rollback = false)
        //{
        //    try
        //    {

        //        if (m_sqlTransaction != null)
        //        {
        //            if (rollback)
        //                m_sqlTransaction.Rollback();
        //            else
        //                m_sqlTransaction.Commit();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //        try
        //        {
        //            m_sqlTransaction = null;
        //            if ((m_sqlCon.State == System.Data.ConnectionState.Open))
        //                m_sqlCon.Close();
        //            m_sqlCon.Dispose();
        //            m_strConnectionString = string.Empty;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}
        public string ExecuteSQLNonQuery(string argStrCmdText, string argStrDBConnName, IConfigurationRoot config, SqlParameter[] argSpParameters = null)
        {
            string strStat = string.Empty;
            SqlCommand ScCommandToExecute;
            SqlConnection m_sqlConn = new SqlConnection();
            try
            {

                // Selecting the appropriate DB connection string from the configuration and opening the connection
                m_strConnectionString = config.GetSection("AppSettings").GetSection(argStrDBConnName).Value;
                m_sqlConn = new SqlConnection(m_strConnectionString);
                m_sqlConn.Open();

                // Creating the SQL Command object. If there are parameters, then the command object with parameters are 
                // created
                if ((!(argSpParameters == null)))
                    ScCommandToExecute = CreateSqlCommand(ref argStrCmdText, ref argSpParameters, ref m_sqlConn);
                else
                    ScCommandToExecute = new SqlCommand(argStrCmdText, m_sqlConn);

                ScCommandToExecute.CommandType = CommandType.StoredProcedure;

                // Setting the command time out, if given in the configuration
                try
                {
                    m_sqlCommandTimeout = Int32.Parse(config.GetSection("AppSettings").GetSection("sqlCommandTimeout").Value);
                }
                catch (Exception ex)
                {
                    m_sqlCommandTimeout = 0;
                    throw ex;
                }

                // If (m_sqlCommandTimeout > 0) Then
                ScCommandToExecute.CommandTimeout = m_sqlCommandTimeout;
                // End If

                //SummitDBTrace _objTrace = new SummitDBTrace(argSpParameters);
                //_objTrace.CommandText = argStrCmdText;

                //_objTrace.WriteBeforeExecuteSQL();

                ScCommandToExecute.ExecuteNonQuery();
                strStat = strSuccess;

                //_objTrace.WriteAfterExecuteSQL();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                try
                {
                    m_sqlConn.Close();
                    m_sqlConn.Dispose();
                    m_strConnectionString = string.Empty;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return strStat;
        }

        public DataSet ExecuteSQLQuery(string argStrCmdText, string argStrDBConnName, SqlParameter[] argSpParameters = null, string dbConnection = "DVServiceBaseURL")
        {
            DataSet ds = new DataSet();
            SqlCommand ScCommandToExecute;
            SqlConnection m_sqlConn = new SqlConnection();
            try
            {
                // Selecting the appropriate DB connection string from the configuration and opening the connection
                m_strConnectionString = dbConnection;
                m_sqlConn = new SqlConnection(m_strConnectionString);
                m_sqlConn.Open();

                // Creating the SQL Command object. If there are parameters, then the command object with parameters are 
                // created
                if ((!(argSpParameters == null)))
                    ScCommandToExecute = CreateSqlCommand(ref argStrCmdText, ref argSpParameters, ref m_sqlConn);
                else
                    ScCommandToExecute = new SqlCommand(argStrCmdText, m_sqlConn);

                ScCommandToExecute.CommandType = CommandType.StoredProcedure;

                // Setting the command time out, if given in the configuration
                try
                {
                    m_sqlCommandTimeout = 20;// Int32.Parse(config.GetSection("AppSettings").GetSection("sqlCommandTimeout").Value);
                }
                catch (Exception ex)
                {
                    m_sqlCommandTimeout = 0;
                    throw ex;
                }

                // If (m_sqlCommandTimeout > 0) Then
                ScCommandToExecute.CommandTimeout = m_sqlCommandTimeout;
                // End If

                SqlDataAdapter da = new SqlDataAdapter(ScCommandToExecute);
                da.SelectCommand = ScCommandToExecute;
                da.ReturnProviderSpecificTypes = ReturnProviderSpecificTypes;
                //  SummitDBTrace _objTrace = new SummitDBTrace(argSpParameters);
                // _objTrace.CommandText = argStrCmdText;

                //  _objTrace.WriteBeforeExecuteSQL();
                da.Fill(ds);
                // _objTrace.WriteAfterExecuteSQL();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    m_sqlConn.Close();
                    m_sqlConn.Dispose();
                    m_strConnectionString = string.Empty;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return ds;
        }

        public string ExecuteSQLNonQuery(string argStrCmdText, SqlParameter[] argSpParameters = null)
        {
            string strStat = string.Empty;
            SqlCommand ScCommandToExecute;
            SqlConnection m_sqlConn = new SqlConnection();
            try
            {

                // Setting connection string and opening the connection
                m_strConnectionString = ConnectionString;
                m_sqlConn = new SqlConnection(m_strConnectionString);
                m_sqlConn.Open();

                // Creating the SQL Command object. If there are parameters, then the command object with parameters are 
                // created
                if ((!(argSpParameters == null)))
                    ScCommandToExecute = CreateSqlCommand(ref argStrCmdText, ref argSpParameters, ref m_sqlConn);
                else
                    ScCommandToExecute = new SqlCommand(argStrCmdText, m_sqlConn);

                ScCommandToExecute.CommandType = System.Data.CommandType.StoredProcedure;

                // Setting the command time out, if set
                // If (CommandTimeout > 0) Then
                ScCommandToExecute.CommandTimeout = CommandTimeout;
                // End If

                //SummitDBTrace _objTrace = new SummitDBTrace(argSpParameters);
                //_objTrace.CommandText = argStrCmdText;

                //_objTrace.WriteBeforeExecuteSQL();

                ScCommandToExecute.ExecuteNonQuery();
                strStat = strSuccess;

                //_objTrace.WriteAfterExecuteSQL();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                try
                {
                    m_sqlConn.Close();
                    m_sqlConn.Dispose();
                    m_strConnectionString = string.Empty;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return strStat;
        }

        public System.Data.DataSet ExecuteSQLQuery(string argStrCmdText, SqlParameter[] argSpParameters = null)
        {
            DataSet ds = new DataSet();
            SqlCommand ScCommandToExecute;
            SqlConnection m_sqlConn = new SqlConnection();
            try
            {
                // Setting connection string and opening the connection
                m_strConnectionString = ConnectionString;
                m_sqlConn = new SqlConnection(m_strConnectionString);
                m_sqlConn.Open();

                // Creating the SQL Command object. If there are parameters, then the command object with parameters are 
                // created
                if ((!(argSpParameters == null)))
                    ScCommandToExecute = CreateSqlCommand(ref argStrCmdText, ref argSpParameters, ref m_sqlConn);
                else
                    ScCommandToExecute = new SqlCommand(argStrCmdText, m_sqlConn);

                ScCommandToExecute.CommandType = CommandType.StoredProcedure;

                // Setting the command time out, if set
                // If (CommandTimeout > 0) Then
                ScCommandToExecute.CommandTimeout = CommandTimeout;
                // End If

                SqlDataAdapter da = new SqlDataAdapter(ScCommandToExecute);
                da.SelectCommand = ScCommandToExecute;
                da.ReturnProviderSpecificTypes = ReturnProviderSpecificTypes;

                //SummitDBTrace _objTrace = new SummitDBTrace(argSpParameters);
                //_objTrace.CommandText = argStrCmdText;

                //_objTrace.WriteBeforeExecuteSQL();
                da.Fill(ds);
                //  _objTrace.WriteAfterExecuteSQL();
            }


            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    m_sqlConn.Close();
                    m_sqlConn.Dispose();
                    m_strConnectionString = string.Empty;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return ds;
        }

        private SqlCommand CreateSqlCommand(ref string argStrCmdText, ref SqlParameter[] argSpParams, ref SqlConnection argSqlCon)
        {
            SqlCommand ScWithArgs = new SqlCommand(argStrCmdText, argSqlCon);
            int intCount = 0;
            while ((intCount < argSpParams.Length))
            {
                ScWithArgs.Parameters.Add(argSpParams[intCount]);
                intCount = intCount + 1;
            }

            return ScWithArgs;
        }

        public static List<T> ConvertDataTableToGenericList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                   .Select(c => String.Join("", c.ColumnName.Split('_')).ToLower())
                   .ToList();

            var properties = typeof(T).GetProperties();
            DataRow[] rows = dt.Select();
            return rows.Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                        pro.SetValue(objT, row[pro.Name.ToLower()]);
                }

                return objT;
            }).ToList();
        }
        #endregion
    }
}
