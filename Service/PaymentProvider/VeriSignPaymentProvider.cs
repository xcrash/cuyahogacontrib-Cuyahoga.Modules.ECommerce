using System;
using System.Web;
using System.Text;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

	/// <summary>
	/// Summary description for VeriSignPaymentProvider.
	/// </summary>
	public class VeriSignPaymentProvider : AbstractWebFormPaymentProvider {

		private const string CC_TRANSACTION_APPROVED = "0";

		public VeriSignPaymentProvider() {
		}

		protected void CheckRequestParameters(IElectronicPayment payment) {
		}

		protected override string RenderFormHiddenValues(IElectronicPayment payment, PaymentRequestTypes paymentType) {

			StringBuilder html = new StringBuilder();

			html.Append("<input type=\"hidden\" name=\"MFCIsapiCommand\" value=\"Orders\">");
			html.Append("<input type=\"hidden\" name=\"LOGIN\" value=\"" + MerchantID + "\">");
			html.Append("<input type=\"hidden\" name=\"PARTNER\" value=\"" + "wfb\">");
    
			html.Append("<input type=\"hidden\" name=\"AMOUNT\" value=\"" + payment.PaymentAmount.Amount + "\">");
			html.Append("<input type=\"hidden\" name=\"TYPE\" value=\"A\">");
			html.Append("<input type=\"hidden\" name=\"ECHODATA\" value=\"True\">");

			string strCustIDConcat;
			string strSep = "|";
			strCustIDConcat = payment.UserInfo.AccountID + strSep
                + payment.UserInfo.UserDetails.FirstName + " " + payment.UserInfo.UserDetails.LastName + strSep 
				+ payment.LocalRequestReference + strSep 
				+ payment.CustomValues["strSalesOrderNumber"];
 
			html.Append("<input type=\"hidden\" name=\"CUSTID\" value=\"" + strCustIDConcat + "\">");
			html.Append("<input type=\"hidden\" name=\"METHOD\" value=\"CC\">");                                  
    
			html.Append("<input type=\"hidden\" name=\"EMAIL\" value=\"" + payment.UserInfo.UserDetails.EmailAddress + "\">");
            html.Append("<input type=\"hidden\" name=\"NAME\" value=\"" + payment.UserInfo.UserDetails.FirstName + " " + payment.UserInfo.UserDetails.LastName + "\">");
			html.Append("<input type=\"hidden\" name=\"ADDRESS\" value=\"" + payment.UserInfo.UserAddress.AddressLine1 + "\">");
			html.Append("<input type=\"hidden\" name=\"CITY\" value=\"" + payment.UserInfo.UserAddress.City + "\">");                  
			html.Append("<input type=\"hidden\" name=\"ZIP\" value=\"" + payment.UserInfo.UserAddress.Postcode + "\">");
			html.Append("<input type=\"hidden\" name=\"COUNTRY\" value=\"" + payment.UserInfo.UserAddress.CountryCode + "\">");   
    
			html.Append("<input type=\"hidden\" name=\"M_paymentType\" value=\"" + payment.CustomValues["strPaymentType"] + "\">");
			html.Append("<input type=\"hidden\" name=\"USER1\" value=\"" + payment.CustomValues["strPONumber"] + "\">");                             
			html.Append("<input type=\"hidden\" name=\"USER2\" value=\"" + payment.CustomValues["strText"] + "\">");
			html.Append("<input type=\"hidden\" name=\"USER3\" value=\"" + payment.UserInfo.UserAddress.AddressLine1 + "\">");
			html.Append("<input type=\"hidden\" name=\"USER4\" value=\"" + payment.UserInfo.UserAddress.AddressLine2 + "\">");
			html.Append("<input type=\"hidden\" name=\"USER5\" value=\"" + payment.UserInfo.UserAddress.City + "\">");
			html.Append("<input type=\"hidden\" name=\"USER6\" value=\"" + payment.UserInfo.UserAddress.Region + "\">");
			html.Append("<input type=\"hidden\" name=\"USER7\" value=\"" + payment.UserInfo.UserDetails.TelephoneNumber + "\">");
			html.Append("<input type=\"hidden\" name=\"USER8\" value=\"" + payment.UserInfo.UserDetails.FaxNumber + "\">");                          
			html.Append("<input type=\"hidden\" name=\"USER9\" value=\"" + payment.CustomValues["strdispatchLabel"] + "\">");
			html.Append("<input type=\"hidden\" name=\"USER10\" value=\"" + payment.CustomValues["strdispatchAccount"] + "\">"); 

			return html.ToString();

		}

		public override IElectronicPayment ProcessHttpResponse(System.Web.HttpRequest postBackData, PaymentRequestTypes paymentType) {

			IElectronicPayment payment = new ElectronicPayment();

			try {
        
				if (postBackData["RESULT"] == CC_TRANSACTION_APPROVED 
					&& postBackData["USER10"].ToUpper() != "UPDATE_DATEBASE_DELAYED_CAPTURE") {
					payment.TransactionStatus = PaymentStatus.Approved;                                                                                                                                                     
				} else {
					payment.TransactionStatus = PaymentStatus.Declined;
				}

				payment.TransactionReference = postBackData["PNREF"];

                payment.UserInfo = new Cuyahoga.Modules.ECommerce.Util.WebStoreUser();
                string[] name = postBackData["Name"].Split(" ".ToCharArray());
				payment.UserInfo.UserDetails.FirstName = name[0];
                payment.UserInfo.UserDetails.LastName = name[1];
				payment.UserInfo.UserDetails.EmailAddress = postBackData["Email"];

				payment.UserInfo.UserAddress.Postcode = postBackData["Zip"];
				payment.UserInfo.UserAddress.AddressLine1 = postBackData["Address"];
				payment.UserInfo.UserAddress.AddressLine2 = postBackData["USER4"];
				payment.UserInfo.UserAddress.City = postBackData["USER5"];
				payment.UserInfo.UserAddress.Region = postBackData["USER6"];
				payment.UserInfo.UserDetails.TelephoneNumber = postBackData["USER7"];
                payment.UserInfo.UserDetails.FaxNumber = postBackData["USER8"];

			} catch (Exception e) {
				LogManager.GetLogger(GetType()).Error(e);
			}

			return null;
		}
	}
}