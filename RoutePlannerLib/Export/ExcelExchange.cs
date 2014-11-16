using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Export
{
    public class ExcelExchange
    {
        public void WriteToFile(String fileName, City from, City to, List<Link> links)
        {
            dynamic excel = new Excel.Application();
            //excel.Visible = true;
            dynamic workbook = excel.Workbooks.Add();
            dynamic sheet = workbook.Sheets.Add() as Worksheet;
            sheet.Name = "Lab7Sheet";
            
            makeTableHeader(sheet);            
            writeContents(sheet, from, to, links);

            excel.DisplayAlerts = false;            
            workbook.SaveAs(fileName);
            excel.Quit();
        }

        private void writeContents(Worksheet sheet, City from, City to, List<Link> links)
        {
            var i=2;
            foreach(var link in links)
            {
               
                sheet.Cells[i, 1] =  String.Format("{0} ({1})", link.FromCity.Name, link.FromCity.Country);
                sheet.Cells[i, 2] = String.Format("{0} ({1})", link.ToCity.Name, link.ToCity.Country); 
                sheet.Cells[i, 3] = link.Distance.ToString();
                sheet.Cells[i, 4] = link.TransportMode.ToString();
                i++;
            }
        }

        private void makeTableHeader(Worksheet sheet)
        {
            sheet.Cells[1, 1] = "From";
            sheet.Cells[1, 2] = "To";
            sheet.Cells[1, 3] = "Distance";
            sheet.Cells[1, 4] = "Transport Mode";

            for (int i = 1; i < 5; ++i)
            {
                Font font = sheet.Cells[1, i].Font;
                font.Bold = true;
                font.Size = 14;

                Borders border = sheet.Cells[1, i].Borders;
                border.LineStyle = XlLineStyle.xlContinuous;

            }
      
        }
    }
}
