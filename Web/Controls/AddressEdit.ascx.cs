namespace Cuyahoga.Modules.ECommerce.Web.Controls {

	using System;
	using System.Collections;
	using System.Web.UI.WebControls;

    using Cuyahoga.Modules.ECommerce.Core;
    using Cuyahoga.Modules.ECommerce.Util;
	using Cuyahoga.Modules.ECommerce.Util.Interfaces;
	using log4net;
	using Cuyahoga.Modules.ECommerce.Service.Translation;
	using Cuyahoga.Modules.ECommerce.Util.Location;
    using Cuyahoga.Modules.ECommerce.DataAccess;
	using IgCountryCode = Cuyahoga.Modules.ECommerce.Util.Location.CountryCode;

	public enum AddressValidationMode {
		None,
		Full,
		CountryOnly,
		CountryAndPostcode
	}

	/// <summary>
	///		Summary description for AddressWuc.
	/// </summary>
	public class AddressEdit : LocalizedModuleConsumerControl, IAddress {

		public TextBox txtContactName;

		protected TextBox txtAddress1;
		protected TextBox txtAddress2;
		protected TextBox txtAddress3;
		protected TextBox txtRegion;
		protected TextBox txtCity;
		protected TextBox txtPostcode;

		protected DropDownList ddlCountry;
		protected PlaceHolder phFullInfo;
		protected PlaceHolder phError;

		protected RequiredFieldValidator rfvAddress1;
		protected RequiredFieldValidator rfvRegion;
		protected RequiredFieldValidator rfvPostCode;
		protected RequiredFieldValidator rfvCountry;
		protected RequiredFieldValidator rfvCity;
		protected RequiredFieldValidator rfvCompanyName;

		private bool _showError = false;
		private bool _autoPostBackOnCountryChange = false;

        private IList _countryList;
		private AddressValidationMode _validationMode = AddressValidationMode.Full;

		public AddressValidationMode ValidationMode {
			get { return _validationMode; }
			set { _validationMode = value; }
		}

		public bool ShowErrorMessage {
			get {
				return _showError;
			}
			set {
				_showError = value;
			}
		}

		public bool AutoPostBackOnCountryChange {
			get {
				return _autoPostBackOnCountryChange;
			}
			set {
				_autoPostBackOnCountryChange = value;
			}
		}

		public bool CountryEnabled {
			get {
				return ddlCountry.Enabled;
			}
			set {
				ddlCountry.Enabled = value;
				rfvCountry.Enabled = value;
			}
		}

		public bool Enabled {
			get {
				try {
					return (bool) ViewState["enabled"];
				} catch {
					return true;
				}
			}
			set {
				ViewState["enabled"] = value;
				txtRegion.Enabled 
					= txtContactName.Enabled 
					= txtAddress1.Enabled 
					= txtAddress2.Enabled 
					= txtAddress3.Enabled 
					= txtCity.Enabled
					= txtRegion.Enabled
					= txtPostcode.Enabled
					= value;

				SetValidatorEnabled(value);
			}
		}

		protected virtual void SetValidatorEnabled(bool isEnabled) {

			if (!isEnabled) {
				
				rfvCompanyName.Enabled
					= rfvAddress1.Enabled
					= rfvCity.Enabled
					= rfvRegion.Enabled
					= rfvPostCode.Enabled
					= isEnabled;

			} else {
				//disable all
				SetValidatorEnabled(false);
				rfvCompanyName.Enabled = true;

				switch (ValidationMode) {
					case AddressValidationMode.Full:
						rfvCountry.Enabled
							= rfvPostCode.Enabled
							= rfvAddress1.Enabled
							= rfvCity.Enabled
							= rfvRegion.Enabled
							= true;
						break;
					case AddressValidationMode.CountryAndPostcode:
						rfvCountry.Enabled
							= rfvPostCode.Enabled
							= true;
						break;
					case AddressValidationMode.CountryOnly:
						rfvCountry.Enabled
							= true;
						break;
					case AddressValidationMode.None:
						//do nothing
						break;
				}
			}
		}

        public void SetAvailableCountries(IList countryList) {
            _countryList = countryList;
            PopulateCountryDdl(ddlCountry);
        }

        private void PopulateCountryDdl(DropDownList list) {

            string selectedValue = null;

            if (ddlCountry.Items.Count > 0 && ddlCountry.SelectedValue != null) {
                selectedValue = ddlCountry.SelectedValue;
            }

            ddlCountry.Items.Clear();

            //Show 'please select' only if there is a choice
            if (_countryList.Count > 1) {
                list.Items.Add(new ListItem(GetText("please select"), ""));
            }

            foreach (Domain.Country country in _countryList) {
                list.Items.Add(new ListItem(country.CountryName, country.CountryCode.ToLower()));
            }

            if (selectedValue != null) {
                try {
                    ddlCountry.SelectedValue = selectedValue;
                } catch { }
            }
        }

		public void ClearControls() {
			txtAddress1.Text = txtAddress2.Text = txtAddress3.Text = txtRegion.Text = txtCity.Text = txtPostcode.Text = "";
			ddlCountry.ClearSelection();
		}

		#region exposed properties

        public string ContactName {
			get {
				if (txtContactName != null) {
					return txtContactName.Text;
				} else {
					return "";
				}
			}
			set {
				if (txtContactName != null) {
					txtContactName.Text = value;
				}
			}
		}

		public string AddressLine1 {
			get {
				if (txtAddress1 != null) {
					return txtAddress1.Text;
				} else {
					return "";
				}
			}
			set {
				if (txtAddress1 != null) {
					txtAddress1.Text = value;
				}
			}
		}

		public string AddressLine2 {
			get {
				if (txtAddress2 != null) {
					return txtAddress2.Text;
				} else {
					return "";
				}
			}
			set {
				if (txtAddress2 != null) {
					txtAddress2.Text = value;
				}
			}
		}

		public string AddressLine3 {
			get {
				if (txtAddress3 != null) {
					return txtAddress3.Text;
				} else {
					return "";
				}
			}
			set {
				if (txtAddress3 != null) {
					txtAddress3.Text = value;
				}
			}
		}

		public string Region {
			get {
				if (txtRegion != null) {
					return txtRegion.Text;
				} else {
					return "";
				}
			}
			set {
				if (txtRegion != null) {
					txtRegion.Text = value;
				}
			}
		}

		public string City {
			get {
				if (txtCity != null) {
					return txtCity.Text;
				} else {
					return "";
				}
			}
			set {
				if (txtCity != null) {
					txtCity.Text = value;
				}
			}
		}

		public string Postcode {
			get {
				if (txtPostcode != null) {
					return txtPostcode.Text;
				} else {
					return "";
				}
			}
			set {
				txtPostcode.Text = value;
			}
		}

		public string CountryCode {
			get {
				try {
					return ddlCountry.SelectedValue;
				} catch {
					return "";
				}
			}
			set {
				try {
					ddlCountry.SelectedValue = value;
				} catch (Exception e) {
					LogManager.GetLogger(GetType()).Info(e);
				}			
			}
		}
		#endregion

		public void BindAddress(IAddress address) {			
			if (address != null) {
				AddressHelper.CopyAddress(address, this);
			} else {
				ClearControls();
			}
		}

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                /*
                _countryList = new ArrayList();

                ITextTranslator translator = TranslatorUtils.GetTextTranslator(typeof(IgCountryCode), CultureCode);
                foreach (string countryCode in IgCountryCode.CountryList) {
                    _countryList.Add(new Domain.Country(countryCode, IgCountryCode.GetCountryName(countryCode, translator)));
                }

                PopulateCountryDdl(ddlCountry);
                 */
            }
            ConfigureValidators();
        }

		protected void CustomValidation(object source, ServerValidateEventArgs e) {			
			e.IsValid = IsValidAddress;
			phError.Visible = _showError && !e.IsValid;
		}

		public bool IsValidAddress {
			get {
				return true;
			}
		}

		public DropDownList CountryList {
			get {
				return ddlCountry;
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e) {
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {

            ddlCountry.AutoPostBack = AutoPostBackOnCountryChange;
			this.Load += new System.EventHandler(this.Page_Load);

	

			ConfigureValidators();
		}
		#endregion

		protected virtual void ConfigureValidators() {

			rfvCompanyName.Text 
				= rfvAddress1.Text 
				= rfvRegion.Text 
				= rfvCountry.Text 
				= rfvPostCode.Text
                = rfvCity.Text
				= "<div class=\"error\">" + GetText("required_field") + "</div>";

			SetValidatorEnabled(Enabled);
		}
	}
}