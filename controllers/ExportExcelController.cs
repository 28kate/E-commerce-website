using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalonApp.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace SalonApp.Controllers
{
    public class ExportExcelController : Controller
    {
        SalonContext db = new SalonContext();

        // GET: ExportExcel
        public ActionResult ExportToExcel()
        {
            try
            {
                var Stock = db.OrderMain.ToList();
                GridView gv = new GridView();
                gv.DataSource = Stock;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=Stock_Order_Data.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter swriter = new StringWriter();
                HtmlTextWriter htwriter = new HtmlTextWriter(swriter);
                gv.RenderControl(htwriter);
                Response.Output.Write(swriter.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex) { }
            return View();
        }
    }
}