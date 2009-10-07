using System;
using System.Reflection;
using log4net;

namespace Cuyahoga.Modules.ECommerce.Util {

	/// <summary>
	/// Controls life cycle of late bound COM objects and simplifies interface
	/// </summary>
	public class ComFacade : IDisposable {

		public const int MAX_DESTROY_LOOP_COUNT = 10000;

		protected Type _comType = null;
		protected object _comInstance = null;

		public ComFacade(string progID) {
			_comType = Type.GetTypeFromProgID(progID);
			CreateComObject();
		}

		public ComFacade(Type type) {
			_comType = type;
			CreateComObject();
		}

		private void CreateComObject() {
			_comInstance = Activator.CreateInstance(_comType);
		}
		
		public object InvokeMethod(string methodName) {
			return InvokeMethod(methodName, null);
		}

		public object InvokeMethod(string methodName, params object[] arguments) {
			return _comType.InvokeMember(methodName, BindingFlags.InvokeMethod, null, _comInstance, arguments);
		}

		public void SetProperty(string propertyName, object Value) {
			_comType.InvokeMember(propertyName, BindingFlags.SetProperty, null, _comInstance, new object[]{Value});
		}

		public object GetProperty(string propertyName) {
			return _comType.InvokeMember(propertyName, BindingFlags.GetProperty, null, _comInstance, null);
		}

		public static void DestroyComObject(ref object comObject) {
			if (comObject != null) {
				//Want to avoid infinite loops
				for (int loopCount = 0; loopCount < MAX_DESTROY_LOOP_COUNT; loopCount++) {
					if (System.Runtime.InteropServices.Marshal.ReleaseComObject(comObject) <= 0) {
						LogManager.GetLogger(typeof(ComFacade)).Debug("Destroyed object [" + comObject.GetType().FullName + "]");
						return;
					}
				}
				LogManager.GetLogger(typeof(ComFacade)).Warn("Failed to destroy object [" + comObject.GetType().FullName + "]");
			}
		}

		public static void ForceGarbageCollection() {
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		#region IDisposable Members
		public void Dispose() {
			DestroyComObject(ref _comInstance);
			try {_comInstance = null;} catch {}
			ForceGarbageCollection();
		}
		#endregion
	}
}