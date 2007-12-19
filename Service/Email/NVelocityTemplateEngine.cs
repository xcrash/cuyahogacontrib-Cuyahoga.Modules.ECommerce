using System;
using System.Collections;
using System.IO;

using Commons.Collections;
using Cuyahoga.Modules.ECommerce.Util;
using log4net;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;

namespace Cuyahoga.Modules.ECommerce.Service.Email {

	/// <summary>
	/// Summary description for NVelocityTemplateEngine.
	/// </summary>
	public class NVelocityTemplateEngine : TemplateEngineBase {

		public const string CULTURE_CODE_DEFAULT = "en";
		public const string VELOCITY_EXTENSION = "vtl";

		public NVelocityTemplateEngine() {
		}

		/// <summary>
		/// Get email text for the template
		/// </summary>
		/// <param name="marketCode">Market code</param>
		/// <param name="cultureCode">Culture Code</param>
		/// <param name="templateName">Name of template</param>
		/// <param name="data">Hastable containing data to be used within the template</param>
		/// <returns>text from template, if it could be found, otherwise <c>null</c></returns>
		public override string GetTemplateText(string templateName, Hashtable data) {
			return GetGenericTemplateText(GetTemplateFileName(templateName), data);
		}

		protected virtual void AddData(object key, object val, VelocityContext context) {
			context.Put(key.ToString(), val);
		}

		private bool IsValidTemplate(string fullTemplateFileName) {

			if (TemplatePath == null || TemplatePath.Length == 0) {
				LogManager.GetLogger(GetType()).Info("Invalid template path [" + TemplatePath + "]");
				return false;
			} else if (!File.Exists(fullTemplateFileName)) {
				LogManager.GetLogger(GetType()).Info("Invalid template file [" + fullTemplateFileName + "]");
				return false;
			}

			return true;
		}

		protected void InitialiseNVelocity(string templatePath) {

			ExtendedProperties props = new ExtendedProperties();
			props.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, TemplatePath);
			
			Velocity.Init(props);
		}

		private string GetPropertyClassName(Type type) {
			return type.AssemblyQualifiedName.Replace(",", "\\\\\\,");
		}

		protected virtual string GetGenericTemplateText(string templateFileName, Hashtable data) {

			string fullTemplateFileName = TemplatePath + templateFileName;

			//Check file
			if (!IsValidTemplate(fullTemplateFileName)) {
				return null;
			}

			InitialiseNVelocity(templateFileName);				
			VelocityContext context = new VelocityContext();

			foreach (object key in data.Keys) {
				AddData(key.ToString(), data[key], context);
			}

			//Standard things used in email
			AddStandardItems(context);

			StringWriter writer = new StringWriter();

			Template template = null;

			try {
				template = Velocity.GetTemplate(templateFileName);
				template.Merge(context, writer);

				return writer.ToString();

			} catch (Exception e) {
				LogManager.GetLogger(GetType()).Error(e);
			}

			return null;
		}

		protected virtual void AddStandardItems(VelocityContext context) {

			///Do not delete these lines.  It's to be used for comparing decimals within NVelocity templates, as they did not yet support the
			///Decimal type on 19th December 2005.
			decimal zeroDecimal = 0.0M;
			AddData("zeroDecimal", zeroDecimal, context);

			//Standard stuff
			AddData("urlSchemeAndDomain", WebHelper.GetSchemeAndDomain(false), context); //Don't want secure version
			AddData("appPath", WebHelper.GetAppPath(), context);
		}

		protected virtual string GetTemplateFileName(string templateName) {
			return templateName + "." + VELOCITY_EXTENSION;
		}
	}
}
