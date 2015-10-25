using System.Web;
using System.Web.UI;

namespace JustSay.Common.DotNetWeb
{
    public class ShowMsgHelper
    {
        public static void Alert(string message)
        {
            ShowMsgHelper.ExecuteScript(string.Format("showTipsMsg('{0}','2500','4');", message));
        }

        public static void AlertMsg(string message)
        {
            ShowMsgHelper.ExecuteScript(string.Format("showTipsMsg('{0}','2500','4');top.main.windowload();OpenClose();", message));
        }

        public static void ParmAlertMsg(string message)
        {
            ShowMsgHelper.ExecuteScript(string.Format("showTipsMsg('{0}','2500','4');top.main.target_right.windowload();OpenClose();", message));
        }

        public static void Alert_Error(string message)
        {
            ShowMsgHelper.ExecuteScript(string.Format("showTipsMsg('{0}','5000','5');", message));
        }

        public static void Alert_Wern(string message)
        {
            ShowMsgHelper.ExecuteScript(string.Format("showTipsMsg('{0}','3000','3');", message));
        }

        public static void showFaceMsg(string message)
        {
            ShowMsgHelper.ExecuteScript(string.Format("showFaceMsg('{0}');", message));
        }

        public static void showWarningMsg(string message)
        {
            ShowMsgHelper.ExecuteScript(string.Format("showWarningMsg('{0}');", message));
        }

        public static void ShowScript(string strobj)
        {
            Page p = HttpContext.Current.Handler as Page;
            p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), "myscript", "<script>" + strobj + "</script>");
        }

        public static void ExecuteScript(string scriptBody)
        {
            string scriptKey = "Somekey";
            Page p = HttpContext.Current.Handler as Page;
            p.ClientScript.RegisterStartupScript(typeof(string), scriptKey, scriptBody, true);
        }
    }
}