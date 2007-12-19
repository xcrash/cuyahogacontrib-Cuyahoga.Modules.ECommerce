using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Cuyahoga.Web.UI;
using Cuyahoga.Core.Util;

using Cuyahoga.Core;
using Cuyahoga.Core.Domain;
using Cuyahoga.Core.Service;
using Cuyahoga.Core.Search;

using Cuyahoga.Core.Communication;
using Cuyahoga.Modules.ECommerce.Web.Admin.Controls;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce;
using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;

namespace Cuyahoga.Modules.ECommerce.Web.Admin.Controls {

    public class AttributeEditor : System.Web.UI.UserControl {

        protected PlaceHolder plhAttributeEditor;
        public IProduct product;
        public List<Domain.Catalogue.Interfaces.IActiveProductAttribute> attributes;
        public int previousID = 0;

        protected void Page_Load(object sender, EventArgs e) {
            Page.ClientScript.RegisterClientScriptInclude("attributes", "js/prices.js");
            DisplayAttributes(product);// need to DisplayAttributes with changes
        }

        protected void DisplayAttributes(Domain.Catalogue.Interfaces.IProduct product) {

            if (product == null) {
                return;
            }

            bool IsSelected = false;
            foreach (IActiveProductAttribute a in attributes) {

                CreateAttributeFields(a, product);

                foreach (IAttributeOption ao in a.AttributeOptionList) {
                    if (product != null) {
                        foreach (ActiveProductAttribute apo in product.ActiveAttributeList) {
                            if (IsOptionSelected(apo, ao, a)) {
                                IsSelected = true;
                            }
                            ao.Price = UpdateOptionPrice(apo, ao, a);
                        }
                    }

                    CreateOptionFields(ao, IsSelected, a.DataType);
                    IsSelected = false;
                }
            }
        }

        public bool IsAttributeSelected(IActiveProductAttribute a, Domain.Catalogue.Interfaces.IProduct product) {
            if (product != null) {
                foreach (ActiveProductAttribute apo in product.ActiveAttributeList) {
                    if (a.Name == apo.Name) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsOptionSelected(ActiveProductAttribute apo, IAttributeOption ao, IActiveProductAttribute a) {

            if (apo.Name == a.Name && apo.AttributeOptionList.Count > 0) {
                foreach (AttributeOption attributeOption in apo.AttributeOptionList) {
                    if (attributeOption.ShortCode == ao.ShortCode) {
                        return true;
                    }
                }
            }

            return false;
        }

        public decimal UpdateOptionPrice(ActiveProductAttribute apo, IAttributeOption ao, IActiveProductAttribute a) {

            if (apo.Name == a.Name && apo.AttributeOptionList.Count > 0) {
                foreach (AttributeOption lee in apo.AttributeOptionList) {
                    if (lee.ShortCode == ao.ShortCode) {
                        ao.Price = lee.Price;
                    }
                }
            }

            return ao.Price;
        }

        public CheckBox CreateCheckBox(string prefix, string name, bool selected) {

            CheckBox attChkBox = new CheckBox();
            attChkBox.Text = " " + name;
            attChkBox.ID = prefix + name.Replace(" ", "");
            attChkBox.EnableViewState = true;
            attChkBox.Enabled = true;
            attChkBox.Checked = selected;
            attChkBox.EnableViewState = true;
            if (name == "Tool Prices") {
                //hack for MJ -- otherwise we will end up in abstraction hell -- thought about each attribute could have it's own js function -- but
                // but im not putting js in a db
                attChkBox.Attributes.Add("onclick", "updatePrice();");
                // attChkBox.CheckedChanged += new EventHandler(attChkBox_CheckedChanged);
            }
            return attChkBox;
        }

        public void CreateAttributeFields(IActiveProductAttribute a, Domain.Catalogue.Interfaces.IProduct product) {

            HtmlGenericControl row = new HtmlGenericControl();
            row.InnerHtml = "<tr><td>";
            plhAttributeEditor.Controls.Add(row);

            Label l = new Label();

            if (a.Group.ID != previousID) {
                l.Text = a.Group.Name;
            } else {
                l.Text = "";
            }

            previousID = Convert.ToInt32(a.Group.ID);
            plhAttributeEditor.Controls.Add(l);

            HtmlGenericControl row2 = new HtmlGenericControl();
            row2.InnerHtml = "</td></tr><tr><td>";
            plhAttributeEditor.Controls.Add(row2);

            plhAttributeEditor.Controls.Add(CreateCheckBox("Attribute", a.Name, IsAttributeSelected(a, product)));

            HtmlGenericControl row3 = new HtmlGenericControl();
            row3.InnerHtml = "</td></tr>";
            plhAttributeEditor.Controls.Add(row3);
        }

        public void CreateOptionFields(IAttributeOption ao, bool IsSelected, string dataType) {

            HtmlGenericControl lblCol = new HtmlGenericControl();
            lblCol.InnerHtml = "<tr><td>";
            plhAttributeEditor.Controls.Add(lblCol);

            CheckBox rb = new CheckBox();
            rb.Text = ao.PickListValue;
            rb.ID = "option" + ao.ShortCode;

            if (ao.Price > 0 || IsSelected) {
                rb.Checked = true;
            }

            plhAttributeEditor.Controls.Add(rb);


            TextBox txtPrice = new TextBox();
            txtPrice.Text = ao.Price.ToString();
            txtPrice.ID = "txt" + ao.ShortCode;

            HtmlGenericControl lblColMiddle = new HtmlGenericControl();
            lblColMiddle.InnerHtml = "</td><td>";
            plhAttributeEditor.Controls.Add(lblColMiddle);

            plhAttributeEditor.Controls.Add(txtPrice);

            HtmlGenericControl lblColEnd = new HtmlGenericControl();
            lblColEnd.InnerHtml = "</td>";
            plhAttributeEditor.Controls.Add(lblColEnd);

        }

        public List<ProductAttributeOptionValue> GetUpdatedAttributes(long productID) {

            if (attributes == null) {
                return new List<ProductAttributeOptionValue>();
            }

            List<ProductAttributeOptionValue> aList = new List<ProductAttributeOptionValue>();

            foreach (IActiveProductAttribute a in attributes) {

                if (Request.Form["p$attributeEditor$Attribute" + a.Name.Replace(" ", "")] == "on") { //nasty hack FIXME

                    foreach (IAttributeOption ao in a.AttributeOptionList) {

                        if (Request.Form["p$attributeEditor$Option" + ao.ShortCode] == "on") {
                            ProductAttributeOptionValue attribute = new ProductAttributeOptionValue();
                            attribute.OptionPrice = Convert.ToDecimal(Request.Form["p$attributeEditor$txt" + ao.ShortCode]);
                            attribute.OptionValueCode = ao.ShortCode;
                            attribute.ProductID = productID;
                            attribute.OptionValueid = Convert.ToInt64(ao.ShortCode);
                            aList.Add(attribute);
                            attribute = null;
                        }
                    }
                }
            }

            return aList;
        }
    }
}