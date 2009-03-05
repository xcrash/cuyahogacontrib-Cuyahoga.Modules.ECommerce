using System;
using System.Collections;
using System.Web;

namespace Cuyahoga.Modules.ECommerce.Util {

	/// <summary>
	/// Summary description for IPaymentProvider.
	/// </summary>
	public interface IConfiguredObject {
		void LoadConfiguration(Hashtable configuration);
		object GetConfigurationValue(string key);
	}

	/// <summary>
	/// Summary description for AbstractSmsProvider.
	/// </summary>
	public abstract class ConfiguredObject : IConfiguredObject {

		private Hashtable _configuration;

		public ConfiguredObject() {
			_configuration = new Hashtable();
		}

		#region IConfiguredObject Members

		public virtual void LoadConfiguration(Hashtable configuration) {
			_configuration = configuration;
		}

		public virtual object GetConfigurationValue(string key) {
			if (_configuration == null) {
				return null;
			}
			if (_configuration.ContainsKey(key)) {

				object val = _configuration[key];

				if (val != null) {
					string stringVal = val as string;
					if (stringVal != null) {
						return StringUtils.StripTrailingCrLf(stringVal);
					}
				}

				return val;
			}
			return null;
		}
		#endregion

		protected string GetStringConfigItem(string name) {
			if (_configuration.ContainsKey(name)) {
				return _configuration[name].ToString();
			}
			return "";
		}

		protected decimal GetDecimalConfigItem(string name) {
			if (_configuration.ContainsKey(name)) {
				try {
					return (decimal) _configuration[name];
				} catch {}
			}
			return 0;
		}

		protected long GetInt64ConfigItem(string name) {
			if (_configuration.ContainsKey(name)) {
				try {
					return (long) _configuration[name];
				} catch {}
			}
			return 0;
		}

		protected int GetInt32ConfigItem(string name) {
			if (_configuration.ContainsKey(name)) {
				try {
					return (int) _configuration[name];
				} catch {}
			}
			return 0;
		}

		protected short GetInt16ConfigItem(string name) {
			if (_configuration.ContainsKey(name)) {
				try {
					return (short) _configuration[name];
				} catch {}
			}
			return 0;
		}

		protected bool GetBooleanConfigItem(string name) {
			if (_configuration.ContainsKey(name)) {
				try {
					return (bool) _configuration[name];
				} catch {}
			}
			return false;
		}
	}
}