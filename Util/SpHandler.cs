using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Xml;
using log4net;

namespace Cuyahoga.Modules.ECommerce.Util {
    
	public class SpHandler : IDisposable {

        public const string PARAM_NAME_RETURN_VALUE = "@RETURN_VALUE";
        public const string DATA_SOURCE_NAME_DEFAULT = "default";
        public const string CONFIG_PARAMETER_SEPERATOR = ".";
        public const string CONNECTION_STRING_SUFFIX = "ConnectionString";
        public const string CASTLE_CONFIG_PATH = "Config\\Properties.config";

        protected string _connectionString = "";
        protected string _cleanConnectionString = "";
        protected ILog _logger;

        protected SqlCommand _command = new SqlCommand();
        protected DbConnection _connection = new SqlConnection();
        protected IDataReader _dataReader = null;

        public static string GetConnectionString() {
            return GetConnectionString(null);
        }

        public SpHandler() {

            _logger = LogManager.GetLogger(GetType());

            Command.CommandType = CommandType.StoredProcedure;
            Command.Connection = _connection;

            _connection.StateChange += new StateChangeEventHandler(Connection_StateChange);

            //Automatically add the return parameter
            //Too SQL Server specific?
            AddParameter(new SqlParameter(PARAM_NAME_RETURN_VALUE, SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 10, 0, "", DataRowVersion.Current, null));

        }

        // <summary>
        // Static Connection String. Initialised with a default value known to work
        // </summary>
        public SpHandler(string storedProcedure)
            : this() {
            Command.CommandText = storedProcedure;
        }

        public SpHandler(string storedProcedure, params DbParameter[] parameterList)
            : this(storedProcedure) {
            for (int i = 0; i < parameterList.Length; i++) {
                Command.Parameters.Add(parameterList[i]);
            }
        }

        /// <summary>
        /// Finds the connection string from the application configuration XML
        /// </summary>
        /// <param name="strDataSourceName"></param>
        /// <returns>connection string</returns>
        public static string GetConnectionString(string dataSourceName) {

            string strConn = null;
            if (string.IsNullOrEmpty(dataSourceName)) {
                dataSourceName = DATA_SOURCE_NAME_DEFAULT;
            }

            try {
                //try the castle db connection
                XmlDocument doc = new XmlDocument();
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                //tidy it up a bit
                path = path.Substring(6, path.Length - 9); //remove bin on the end and the file:// stuff
                path = String.Concat(path, CASTLE_CONFIG_PATH);
                if (System.IO.File.Exists(path)) {
                    doc.Load(path);
                    XmlNode node = doc.SelectSingleNode("configuration//properties//connectionString");
                    if (!string.IsNullOrEmpty(node.InnerText)) {
                        return node.InnerText;
                    }
                }
            } catch { }

            try {
                strConn = ConfigurationManager.ConnectionStrings[dataSourceName].ConnectionString;
            } catch { }

            //Old style configuration
            if (string.IsNullOrEmpty(strConn)) {
                string strKeyName = dataSourceName + CONFIG_PARAMETER_SEPERATOR + CONNECTION_STRING_SUFFIX;
                strConn = System.Configuration.ConfigurationManager.AppSettings[strKeyName];
            }

            return (strConn != null) ? strConn : "";
        }

        public static string GetObjectPrefix(string dataSourceName, string ownerName) {

            using (SpHandler dbh = new SpHandler()) {

                dbh.ConnectionString = GetConnectionString(dataSourceName);

                if (dbh.ConnectionString.Length == 0) {
                    dbh.ConnectionString = GetConnectionString();
                }

                string databaseName = "[" + dbh.Command.Connection.Database + "]";
                return databaseName + "." + ownerName + ".";
            }
        }

        //<comment><summary>
        // Closes and destroys the recordset and connection stored within this object
        public virtual void CloseConnection() {

            try {
                if (_dataReader != null) {
                    _dataReader.Close();
                    _dataReader = null;
                }
            } catch (Exception e) {
                _logger.Debug(e);
            }

            try {
                if (_connection != null && _connection.State != ConnectionState.Closed) {
                    _logger.Debug("Attempting to close database connection [Status: " + _connection.State.ToString() + "]");
                    _connection.Close();
                }
            } catch (Exception e) {
                _logger.Debug(e);
            }

            _dataReader = null;
            _command = null;
            _connection = null;
        }

        public string CommandText {
            set {
                Command.CommandText = value;
            }
            get {
                return Command.CommandText;
            }
        }

        // <summary>
        // The connection string to be used for this operation. Usually this class will try to find the default connection string.
        // However, since this is static, it can be set once and the value will be remembered
        // </summary>
        public string ConnectionString {
            set {
                _cleanConnectionString = Regex.Replace(";" + value + ";", @";(pwd|password)\s*=\s*.*?;", ";password=****;");
                if (_cleanConnectionString.Length > 2) {
                    _cleanConnectionString = _cleanConnectionString.Substring(1, _cleanConnectionString.Length - 2);
                }

                _logger.Debug("Setting connection string to [" + _cleanConnectionString + "]");
                _connectionString = value;
                _connection.ConnectionString = value;
            }
            get {

                string conn = _connection.ConnectionString;

                if ((conn == null || conn.Length == 0) && _connectionString != null && _connectionString.Length > 0) {
                    _connection.ConnectionString = _connectionString;
                    conn = _connectionString;
                }

                return conn;

            }
        }

        // <summary>
        // Exposes the Command object associated with this class
        // </summary>
        public DbCommand Command {
            get {
                return _command;
            }
        }

        public IDataReader DataReader {
            get {
                if (_dataReader != null) {
                    return _dataReader;
                } else {
                    throw new Exception("DataReader not initialised");
                }
            }
        }

        // <summary>
        // Executes the stored procedure and returns a DataReader object. Quicker than the GetDataTableResults() method,
        // but not suitable for some applications and requires more management
        // </summary>
        // <returns>DataReader </returns>
        public void ExecuteReader() {

            CheckConnectionString();

            if (_dataReader == null) {

                //If a data reader is already used with this connection an exception occurs
                _connection.Open();

                //Diagnositics
                _logger.Debug("Connected to database using [" + _connection.ConnectionString + "]");
                _logger.Debug("Executing Stored Procedure [" + _command.CommandText + "]");
                ShowParameters();

                //Actually do something
                _dataReader = _command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            } else {
                CloseConnection();
                throw new Exception("DataReader already in use");
            }
        }

        // <summary>
        // Executes the stored procedure and returns a single value
        // </summary>
        // <returns>Single value</returns>
        public object ExecuteScalar() {

            CheckConnectionString();
            _connection.Open();

            //Diagnositics
            _logger.Debug("Connected to database using [" + _connection.ConnectionString + "]");
            _logger.Debug("Executing Stored Procedure [" + _command.CommandText + "]");
            ShowParameters();

            //Actually do something
            return _command.ExecuteScalar();
        }

        // <summary>
        // Executes the stored procedure only. There are <b>no</b> results associated with this method. Used for Updates, Inserts and Deletes
        // </summary>
        public void ExecuteNonQuery() {

            CheckConnectionString();
            _connection.Open();

            //Diagnostics
            _logger.Debug("Connected to database using [" + _connection.ConnectionString + "]");
            _logger.Debug("Executing Stored Procedure [" + _command.CommandText + "]");
            ShowParameters();

            //Do something
            _command.ExecuteNonQuery();

        }

        // <summary>
        // Adds a parameter to the stored procedure. The RETURN_VALUE parameter is automatiy added on creation.
        // </summary>
        // <param name="param">The parameter to be added</param>
        public void AddParameter(DbParameter param) {
            _command.Parameters.Add(param);
        }

        public void AddParameter(string parameterName, object value) {
            _command.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void AddInParameter(string parameterName, DbType dbType, int size, object value) {
            AddParameter(parameterName, dbType, ParameterDirection.Input, size, value);
        }

        public void AddOutParameter(string parameterName, DbType dbType, int size, object value) {
            AddParameter(parameterName, dbType, ParameterDirection.Output, size, value);
        }

        public void AddParameter(string parameterName, DbType dbType, ParameterDirection direction, int size, object value) {

            SqlParameter objParam = new SqlParameter(parameterName, value);

            objParam.DbType = (DbType)dbType;
            objParam.Direction = direction;
            objParam.Size = size;
            AddParameter(objParam);
        }

        // <summary>
        // Destructor<br></br>
        // Makes sure that the data reader and connection are closed when this object is destroyed
        // </summary>
        protected void Finalise() {
            CloseConnection();
        }

        ~SpHandler() {
            Finalise();
        }

        public void Dispose() {
            Finalise();
        }

        public void ShowParameters() {
            //Loop through all of the parameters showing their properties
            foreach (DbParameter param in _command.Parameters) {
                _logger.Debug("Param: " + param.ParameterName + " = [" + ((param.Value == null) ? "NULL" : param.Value.ToString()) + "]");
            }
        }

        protected void CheckConnectionString() {
            if (ConnectionString == null || ConnectionString.Length == 0) {
                ConnectionString = GetConnectionString();
                if (ConnectionString == null || ConnectionString.Length == 0) {
                    Exception e = new MissingFieldException("Missing value for database connection string");
                    _logger.Fatal(e);
                    throw e;
                }
            }
        }

        private void Connection_StateChange(object sender, StateChangeEventArgs e) {
            _logger.Debug("Database connection status changed from [" + e.OriginalState + "] to [" + e.CurrentState + "]");
        }
    }
}