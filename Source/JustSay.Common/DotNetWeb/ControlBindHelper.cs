using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace JustSay.Common.DotNetWeb
{
    public class ControlBindHelper
    {
        public static void BindGridViewList(DataTable table, GridView grid)
        {
            if (table == null || table.Rows.Count == 0)
            {
                table = table.Clone();
                table.Rows.Add(table.NewRow());
                grid.DataSource = table;
                grid.DataBind();
                int columnCount = table.Columns.Count;
                grid.Rows[0].Cells.Clear();
                grid.Rows[0].Cells.Add(new TableCell());
                grid.Rows[0].Cells[0].ColumnSpan = columnCount;
                grid.Rows[0].Cells[0].Text = "没有找到您要的相关数据";
                grid.Rows[0].Cells[0].Style.Add("text-align", "center");
            }
            else
            {
                grid.DataSource = table;
                grid.DataBind();
            }
        }

        public static void BindGridViewList(IList list, GridView grid)
        {
            grid.DataSource = list;
            grid.DataBind();
        }

        public static void BindRepeaterList(DataTable dt, Repeater repeater)
        {
            repeater.DataSource = dt;
            repeater.DataBind();
        }

        public static void BindRepeaterList(IList list, Repeater repeater)
        {
            repeater.DataSource = list;
            repeater.DataBind();
        }

        public static void BindRepeaterList<T>(IList<T> list, Repeater repeater)
        {
            repeater.DataSource = list;
            repeater.DataBind();
        }

        public static void BindDropDownList(DataTable dt, DropDownList dropdownlists, string _Name, string _ID, string _Memo)
        {
            dropdownlists.DataSource = dt;
            dropdownlists.DataTextField = _Name;
            dropdownlists.DataValueField = _ID;
            dropdownlists.DataBind();
            if (!string.IsNullOrEmpty(_Memo.Trim()))
            {
                dropdownlists.Items.Insert(0, new ListItem(_Memo, ""));
            }
        }

        public static void BindDropDownList(IList list, DropDownList dropdownlists, string _Name, string _ID, string _Memo)
        {
            dropdownlists.DataSource = list;
            dropdownlists.DataTextField = _Name;
            dropdownlists.DataValueField = _ID;
            dropdownlists.DataBind();
            if (!string.IsNullOrEmpty(_Memo.Trim()))
            {
                dropdownlists.Items.Insert(0, new ListItem(_Memo, ""));
            }
        }

        public static void BindDropDownList<T>(IList<T> list, DropDownList dropdownlists, string _Name, string _ID, string _Memo)
        {
            dropdownlists.DataSource = list;
            dropdownlists.DataTextField = _Name;
            dropdownlists.DataValueField = _ID;
            dropdownlists.DataBind();
            if (!string.IsNullOrEmpty(_Memo.Trim()))
            {
                dropdownlists.Items.Insert(0, new ListItem(_Memo, ""));
            }
        }

        public static void BindHtmlSelect(DataTable dt, HtmlSelect select, string _Name, string _ID, string _Memo)
        {
            select.DataSource = dt;
            select.DataTextField = _Name;
            select.DataValueField = _ID;
            select.DataBind();
            if (!string.IsNullOrEmpty(_Memo.Trim()))
            {
                select.Items.Insert(0, new ListItem(_Memo, ""));
            }
        }

        public static void BindHtmlSelect(IList list, HtmlSelect select, string _Name, string _ID, string _Memo)
        {
            select.DataSource = list;
            select.DataTextField = _Name;
            select.DataValueField = _ID;
            select.DataBind();
            if (!string.IsNullOrEmpty(_Memo.Trim()))
            {
                select.Items.Insert(0, new ListItem(_Memo, ""));
            }
        }

        public static void BindHtmlSelect<T>(IList<T> list, HtmlSelect select, string _Name, string _ID, string _Memo)
        {
            select.DataSource = list;
            select.DataTextField = _Name;
            select.DataValueField = _ID;
            select.DataBind();
            if (!string.IsNullOrEmpty(_Memo.Trim()))
            {
                select.Items.Insert(0, new ListItem(_Memo, ""));
            }
        }

        public static void BindRadioButtonList(DataTable dt, RadioButtonList rbllist, string _Name, string _ID)
        {
            rbllist.DataSource = dt;
            rbllist.DataTextField = _Name;
            rbllist.DataValueField = _ID;
            rbllist.DataBind();
        }

        public static void BindCheckBoxList(DataTable dt, CheckBoxList checkList, string _Name, string _ID)
        {
            checkList.DataSource = dt;
            checkList.DataTextField = _Name;
            checkList.DataValueField = _ID;
            checkList.DataBind();
        }

        public static Hashtable GetWebControls(Control page)
        {
            Hashtable ht = new Hashtable();
            int size = HttpContext.Current.Request.Params.Count;
            for (int i = 0; i < size; i++)
            {
                string id = HttpContext.Current.Request.Params.GetKey(i);
                Control control = page.FindControl(id);
                if (control != null)
                {
                    control = page.FindControl(id);
                    if (control is HtmlInputText)
                    {
                        HtmlInputText txt = (HtmlInputText)control;
                        ht[txt.ID] = txt.Value.Trim();
                    }
                    if (control is TextBox)
                    {
                        TextBox txt2 = (TextBox)control;
                        ht[txt2.ID] = txt2.Text.Trim();
                    }
                    if (control is HtmlSelect)
                    {
                        HtmlSelect txt3 = (HtmlSelect)control;
                        ht[txt3.ID] = txt3.Value.Trim();
                    }
                    if (control is HtmlInputHidden)
                    {
                        HtmlInputHidden txt4 = (HtmlInputHidden)control;
                        ht[txt4.ID] = txt4.Value.Trim();
                    }
                    if (control is HtmlInputPassword)
                    {
                        HtmlInputPassword txt5 = (HtmlInputPassword)control;
                        ht[txt5.ID] = txt5.Value.Trim();
                    }
                    if (control is HtmlInputCheckBox)
                    {
                        HtmlInputCheckBox chk = (HtmlInputCheckBox)control;
                        ht[chk.ID] = (chk.Checked ? 1 : 0);
                    }
                    if (control is HtmlTextArea)
                    {
                        HtmlTextArea area = (HtmlTextArea)control;
                        ht[area.ID] = area.Value.Trim();
                    }
                }
            }
            return ht;
        }

        public static void SetWebControls(Control page, Hashtable ht)
        {
            if (ht.Count != 0)
            {
                int size = ht.Keys.Count;
                foreach (string key in ht.Keys)
                {
                    object val = ht[key];
                    Control control = page.FindControl(key);
                    if (control != null)
                    {
                        if (control is HtmlInputText)
                        {
                            HtmlInputText txt = (HtmlInputText)control;
                            txt.Value = val.ToString();
                        }
                        if (control is TextBox)
                        {
                            TextBox txt2 = (TextBox)control;
                            txt2.Text = val.ToString();
                        }
                        if (control is DropDownList)
                        {
                            DropDownList txt3 = (DropDownList)control;
                            txt3.SelectedValue = val.ToString();
                        }
                        if (control is HtmlSelect)
                        {
                            HtmlSelect txt4 = (HtmlSelect)control;
                            txt4.Value = val.ToString();
                        }
                        if (control is HtmlInputHidden)
                        {
                            HtmlInputHidden txt5 = (HtmlInputHidden)control;
                            txt5.Value = val.ToString();
                        }
                        if (control is HtmlInputPassword)
                        {
                            HtmlInputPassword txt6 = (HtmlInputPassword)control;
                            txt6.Value = val.ToString();
                        }
                        if (control is Label)
                        {
                            Label txt7 = (Label)control;
                            txt7.Text = val.ToString();
                        }
                        if (control is HtmlTextArea)
                        {
                            HtmlTextArea area = (HtmlTextArea)control;
                            area.Value = val.ToString();
                        }
                    }
                }
            }
        }

        public static string GetControlProperty(string Control_Type, string Property_Name, string Control_ID, string Control_Style, string Control_Length, string Control_Validator, int j, string Colspan, string DataSource, string Event, string Maxlength)
        {
            StringBuilder property = new StringBuilder();
            if (Colspan == "")
            {
                if (j == 0)
                {
                    property.Append("<tr>");
                    property.Append("<th>" + Property_Name + "</th>");
                    property.Append("<td>");
                    property.Append(ControlBindHelper.GetControl_Type(Control_Type, Control_ID, Control_Style, Control_Length, Control_Validator, DataSource, Event, Maxlength));
                    property.Append("</td>");
                }
                else
                {
                    if (j == 1)
                    {
                        property.Append("<th>" + Property_Name + "</th>");
                        property.Append("<td>");
                        property.Append(ControlBindHelper.GetControl_Type(Control_Type, Control_ID, Control_Style, Control_Length, Control_Validator, DataSource, Event, Maxlength));
                        property.Append("</td>");
                        property.Append("</tr>");
                    }
                    else
                    {
                        property.Append("<tr>");
                        property.Append("<th>" + Property_Name + "</th>");
                        property.Append("<td>");
                        property.Append(ControlBindHelper.GetControl_Type(Control_Type, Control_ID, Control_Style, Control_Length, Control_Validator, DataSource, Event, Maxlength));
                        property.Append("</td>");
                        property.Append("</tr>");
                    }
                }
            }
            else
            {
                property.Append("<tr>");
                property.Append("<th>" + Property_Name + "</th>");
                property.Append("<td " + Colspan + ">");
                property.Append(ControlBindHelper.GetControl_Type(Control_Type, Control_ID, Control_Style, Control_Length, Control_Validator, DataSource, Event, Maxlength));
                property.Append("</td>");
                property.Append("</tr>");
            }
            return property.ToString();
        }

        public static string GetControl_Type(string Control_Type, string Control_ID, string Control_Style, string Control_Length, string Control_Validator, string DataSource, string Event, string Maxlength)
        {
            StringBuilder str_Control_Type = new StringBuilder();
            string strMaxlength = "";
            if (Maxlength != "")
            {
                strMaxlength = "maxlength=" + Maxlength;
            }
            string result;
            if (Control_Type != null)
            {
                if (Control_Type == "1")
                {
                    str_Control_Type.Append(string.Concat(new string[]
					{
						"<input id=\"",
						Control_ID,
						"\" ",
						Event,
						" ",
						strMaxlength,
						" type=\"text\" class=\"",
						Control_Style,
						"\" style=\"width: ",
						Control_Length,
						"\" ",
						Control_Validator,
						"/>"
					}));
                    result = str_Control_Type.ToString();
                    return result;
                }
                if (Control_Type == "2")
                {
                    str_Control_Type.Append(string.Concat(new string[]
					{
						"<select id=\"",
						Control_ID,
						"\" ",
						Event,
						" class=\"",
						Control_Style,
						"\" style=\"width: ",
						Control_Length,
						"\" ",
						Control_Validator,
						"/>"
					}));
                    if (DataSource != "")
                    {
                        string[] strSource = DataSource.Split(new char[]
						{
							'|'
						});
                        string[] array = strSource;
                        for (int i = 0; i < array.Length; i++)
                        {
                            string item = array[i];
                            str_Control_Type.Append(string.Concat(new string[]
							{
								"<option value=",
								item,
								">",
								item,
								"</option>"
							}));
                        }
                    }
                    str_Control_Type.Append("</select>");
                    result = str_Control_Type.ToString();
                    return result;
                }
                if (Control_Type == "3")
                {
                    str_Control_Type.Append(string.Concat(new string[]
					{
						"<input id=\"",
						Control_ID,
						"\" ",
						Event,
						" ",
						strMaxlength,
						" type=\"text\" class=\"",
						Control_Style,
						"\" style=\"width: ",
						Control_Length,
						"\" ",
						Control_Validator,
						"/>"
					}));
                    result = str_Control_Type.ToString();
                    return result;
                }
                if (Control_Type == "4")
                {
                    str_Control_Type.Append("<lable id=\"" + Control_ID + "\"/>");
                    result = str_Control_Type.ToString();
                    return result;
                }
            }
            result = "内部错误";
            return result;
        }
    }
}