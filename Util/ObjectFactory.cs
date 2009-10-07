using System;

namespace Cuyahoga.Modules.ECommerce.Util {

	/// <summary>
	/// Creates objects depending upon their types.
	/// Useful for turning string values into <c>int</c>s, <c>bool</c>s etc
	/// </summary>
	public class ObjectFactory {

		private ObjectFactory() {
		}

		/// <summary>
		/// Creates an instance of a received Type. Looks for a constructor with a single parameter
		/// </summary>
		/// <param name="classType">The Type of the new class instance to return.</param>
		/// <param name="constructorValue">The single string value used as a constructor</param>
		/// <returns>An Object containing the new instance.</returns>
		/// <remarks>
		/// Invalid numbers will be returned as a zero equivalent
		/// </remarks>
		public static object CreateNewInstance(string classType, string constructorValue) {

			Type finalType = Type.GetType(classType);
			return CreateNewInstance(finalType, constructorValue);

		}

		public static object CreateNewInstance(string classType) {
			return CreateNewInstance(classType, null);
		}

		public static object CreateNewInstance(System.Type classType, string constructorValue) {

			switch (classType.FullName) {
				case "System.Byte":
					try {
						return Byte.Parse(constructorValue);
					} catch {
						return CreateNewInstance(classType, "0");
					}
				case "System.Char":
					try {
						return Char.Parse(constructorValue);
					} catch {
						return CreateNewInstance(classType, "0");
					}
				case "System.Int16":
					try {
						return Int16.Parse(constructorValue);
					} catch {
						return CreateNewInstance(classType, "0");
					}
				case "System.Int32":
					try {
						return Int32.Parse(constructorValue);
					} catch {
						return CreateNewInstance(classType, "0");
					}
				case "System.Int64":
					try {
						return Int64.Parse(constructorValue);
					} catch {
						return CreateNewInstance(classType, "0");
					}
				case "System.UInt16":
					try {
						return UInt16.Parse(constructorValue);
					} catch {
						return CreateNewInstance(classType, "0");
					}
				case "System.UInt32":
					try {
						return UInt32.Parse(constructorValue);
					} catch {
						return CreateNewInstance(classType, "0");
					}
				case "System.UInt64":
					try {
						return UInt64.Parse(constructorValue);
					} catch {
						return CreateNewInstance(classType, "0");
					}
				case "System.DateTime":
					try {
						return DateTime.Parse(constructorValue);
					} catch {
						return SqlUtils.MinSqlDateTime;
					}
				case "System.Boolean":
				switch (constructorValue.ToLower()) {
					case "true":
					case "t":
					case "y":
					case "yes":
					case "ok":
					case "1":
					case "-1":
						return true;
					default:
						return false;
				}
				case "System.Decimal":
					try {
						return Decimal.Parse(constructorValue);
					} catch {
						return CreateNewInstance(classType, "0");
					}
				case "System.Double":
					try {
						return Double.Parse(constructorValue);
					} catch {
						return CreateNewInstance(classType, "0");
					}
				case "System.Single":
					try {
						return Single.Parse(constructorValue);
					} catch {
						return CreateNewInstance(classType, "0");
					}

				case "object":
				case "System.Object":
					return constructorValue;
				case "System.String":
				case "string":
					if (constructorValue != null) {
						return constructorValue;
					} else {
						return "";
					}
				default:
					try {
						if (constructorValue != null) {
							return Activator.CreateInstance(classType, new object[]{constructorValue});  
						} else {
							return Activator.CreateInstance(classType);  
						}
					} catch {
						if (constructorValue != null && classType != null) {
							throw new Exception("Could not cast defined value [" + constructorValue + "] to type [" + classType.FullName + "]");
						} else {
							if (classType != null) {
								throw new Exception("Could not create type [" + classType.FullName + "]");
							} else {
								throw new Exception("null class type supplied");
							}
						}
					}
			}
		}

		/// <summary>
		/// Creates an instance of a received Type. Looks for a constructor with no parameters
		/// </summary>
		/// <param name="classType">The Type of the new class instance to return.</param>
		/// <returns>An Object containing the new instance.</returns>
		public static object CreateNewInstance(System.Type classType) {

			object instance = null;

			switch (classType.FullName) {
				case "System.Char":
				case "System.Byte":
				case "System.Int16":
				case "System.Int32":
				case "System.Int64":
				case "System.UInt16":
				case "System.UInt32":
				case "System.UInt64":
				case "System.DateTime":
				case "System.Boolean":
				case "System.Decimal":
				case "System.Double":
				case "System.Single":
				case "System.String":
				case "string":
				case "object":
				case "System.Object":
					return CreateNewInstance(classType, "");
				default:
					System.Type[] constructor = new System.Type[]{};
					System.Reflection.ConstructorInfo[] constructors = null;
       
					constructors = classType.GetConstructors();

					if (constructors.Length == 0)
						throw new System.UnauthorizedAccessException();
					else {
						for(int i = 0; i < constructors.Length; i++) {
							System.Reflection.ParameterInfo[] parameters = constructors[i].GetParameters();

							if (parameters.Length == 0) {
								instance = classType.GetConstructor(constructor).Invoke(new object[]{});
								break;
							}
							else if (i == constructors.Length -1) {
								throw new System.MethodAccessException();
							}
						}                       
					}			
					return instance;
			}
		}
	}

}