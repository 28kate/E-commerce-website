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
    public class ExportItemExcelController : Controller
    {
        // GET: ExportItemExcel
        public ActionResult ExportItem()
        {

            SalonContext db = new SalonContext();

            try
            {
                var Items = db.OrderItems.ToList();
                GridView gv = new GridView();
                gv.DataSource = Items;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=Product_Sales.xls");
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