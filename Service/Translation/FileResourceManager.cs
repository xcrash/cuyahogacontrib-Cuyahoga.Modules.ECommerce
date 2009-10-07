namespace Cuyahoga.Modules.ECommerce.Service.Translation {

	using System.Globalization;
	using System.IO;
	using System.Reflection;
	using System.Resources;
	using System.Security.Permissions;
	using System.Text;

	using log4net;
	using Cuyahoga.Modules.ECommerce.Util;

	/// <summary>
	/// Reads non-compiled resource files
	/// </summary>
	public class FileResourceManager : ResourceManager {

		public const string DEFAULT_RESOURCE_PATH = "resources";
	
		private string _path;

		public FileResourceManager(string baseName, Assembly assembly, string path) : base(baseName, assembly) {
			this._path = path;
		}

		public static ResourceManager CreateResourceManager(string baseName, Assembly assembly) {
			return CreateResourceManager(baseName, assembly, null);
		}

		public static ResourceManager CreateResourceManager(string baseName, Assembly assembly, string path) {
			if ((path != null)) {
				path = WebHelper.GetPhysicalApplicationPath() + path.Replace("/", "\\");
			} else {
				path = WebHelper.GetPhysicalApplicationPath() + DEFAULT_RESOURCE_PATH;
			}
			LogManager.GetLogger(typeof(FileResourceManager)).Debug("Creating resource manager [" + baseName + ", " + assembly.GetName().Name + ", " + path + "]");
			return new FileResourceManager(baseName, assembly, path);
		}

		protected override ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents) {
			
			ResourceSet rs = (ResourceSet) base.ResourceSets[culture];
			
			if (rs == null) {

                string fileName = GetFileName(culture);

                if (fileName == null) {
					return base.InternalGetResourceSet(culture, createIfNotExists, tryParents);
				}

                rs = new ResXResourceSet(fileName);

				if (rs != null) {
					base.ResourceSets.Add(culture, rs);
				}
			}
			return rs;
		}

		private string GetFileName(CultureInfo culture) {
			
            StringBuilder sb = new StringBuilder();
            sb.Append(base.BaseNameField);

            if (!culture.Equals(CultureInfo.InvariantCulture)) {
                sb.Append('.');
                sb.Append(culture.Name);
            }

            sb.Append(".resx");
            string fileName = sb.ToString();

			if (this._path != null) {
				string fullFileName = Path.Combine(this._path, fileName);
				if (File.Exists(fullFileName)) {
					return fullFileName;
				}
			} else if (File.Exists(fileName)) {
				return fileName;
			}

			return null;
		}
	}
}