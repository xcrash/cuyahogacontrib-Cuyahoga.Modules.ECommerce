using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Util {
    
	public class SpHandler : IDisposable {

		private ILog logger;

		//<comment><summary>
		// Simplifies database and stored procedure handling.<br/><br/>
		// This class handles connections to a database and deliberately keeps all related objects within this class.
		// It ensures the RecordSet and Connection objects are closed and destroyed when this object is destroyed.
		//<remarks>
		//<author>Simon Williams
		public const string PARAM_NAME_RETURN_VALUE = "@RETURN_VALUE";
        public const string DATASOURCE_NAME_DEFAULT = "dbDefault";

		//This used to be static - beware subtle bugs needing this to be set
		private string _connectionString = "";

		private SqlCommand _command = new SqlCommand();
		private SqlConnection _connection = new SqlConnection();
		private IDataReader _dataReader = null;

		public static string GetConnectionString() {
			return GetConnectionString(null);
		}

		/// <summary>
		/// Finds the connection string from the application configuration XML
		/// </summary>
		/// <param name="strDataSourceName"></param>
		/// <returns>connection string</returns>
		public static string GetConnectionString(string strDataSourceName) {

            string strConn;

            if (!string.IsNullOrEmpty(strDataSourceName)) {
                strConn = ConfigurationManager.ConnectionStrings[strDataSourceName].ConnectionString;
            } else {
                strConn = ConfigurationManager.ConnectionStrings[DATASOURCE_NAME_DEFAULT].ConnectionString;
            }

			if (strConn == null) {
				return "";
			} else {
				return strConn;
			}
		}

		public SpHandler() {

			logger = LogManager.GetLogger(GetType());

			Command.CommandType = CommandType.StoredProcedure;
			Command.Connection = _connection;

			_connection.StateChange += new StateChangeEventHandler(Connection_StateChange);

			//Automatiy add the return parameter
			AddParameter(new SqlParameter(PARAM_NAME_RETURN_VALUE, SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 10, 0, "", DataRowVersion.Current, null));

		}

		// <summary>
		// Static Connection String. Initialised with a default value known to work
		// </summary>
		public SpHandler(string storedProcedure) : this() {
			Command.CommandText = storedProcedure;            
		}

        public SpHandler(string storedProcedure, params SqlParameter[] parameterList) : this(storedProcedure) {
            for (int i = 0; i < parameterList.Length; i++) {
                Command.Parameters.Add(parameterList[i]);
            }
        }

		//<comment><summary>
		// Closes and destroys the recordset and connection stored within this object
		public void CloseConnection() {

			try {
				if (_dataReader != null) {
					_dataReader.Close();
					_dataReader = null;
				}
			} catch (Exception e) {
				logger.Debug(e);
			}

			try {
				if (_connection != null &&_connection.State != ConnectionState.Closed) {
					logger.Debug("Attempting to close database connection [Status: " + _connection.State.ToString() + "]");
					_connection.Close();
				}
			} catch (Exception e) {
				logger.Debug(e);
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
				logger.Debug("Setting connection string to [" + value + "]");
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
		public SqlCommand Command {
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
				logger.Debug("Connected to database using [" + _connection.ConnectionString + "]");
				logger.Debug("Executing Stored Procedure [" + _command.CommandText + "]");
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
			logger.Debug("Connected to database using [" + _connection.ConnectionString + "]");
			logger.Debug("Executing Stored Procedure [" + _command.CommandText + "]");
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
			logger.Debug("Connected to database using [" + _connection.ConnectionString + "]");
			logger.Debug("Executing Stored Procedure [" + _command.CommandText + "]");
			ShowParameters();

			//Do something
			_command.ExecuteNonQuery();

		}

		// <summary>
		// Adds a parameter to the stored procedure. The RETURN_VALUE parameter is automatiy added on creation.
		// </summary>
		// <param name="param">The parameter to be added</param>
		public void AddParameter(System.Data.SqlClient.SqlParameter param) {
			_command.Parameters.Add(param);
		}

        public void AddParameter(string parameterName, object value) {
            _command.Parameters.Add(new SqlParameter(parameterName, value));
        }

		public void AddInParameter(string parameterName, SqlDbType dbType, int size, object value) {
			AddParameter(parameterName, dbType, ParameterDirection.Input, size, value);
		}

		public void AddOutParameter(string parameterName, SqlDbType dbType, int size, object value) {
			AddParameter(parameterName, dbType, ParameterDirection.Output, size, value);
		}

		public void AddParameter(string parameterName, SqlDbType dbType, ParameterDirection direction, int size, object value) {

			SqlParameter objParam = new SqlParameter(parameterName, value);

			objParam.DbType = (DbType) dbType;
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
			foreach (SqlParameter param in _command.Parameters) {
				logger.Debug("Param: " + param.ParameterName + " = [" + ((param.Value == null) ? "NULL" : param.Value.ToString()) + "]");
			}
		}

		private void CheckConnectionString() {
			if (ConnectionString == null || ConnectionString.Length == 0) {
				ConnectionString = GetConnectionString();
				if (ConnectionString == null || ConnectionString.Length == 0) {
					Exception e = new MissingFieldException("Missing value for database connection string");
					logger.Fatal(e);
					throw e;
				}
			}
		}

		private void Connection_StateChange(object sender, StateChangeEventArgs e) {
			logger.Debug("Database connection status changed from [" + e.OriginalState + "] to [" + e.CurrentState + "]");
		}
	}
}