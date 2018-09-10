
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Net.Http.Headers;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace TestDownload
{
    /// <summary>
    /// 导出
    /// </summary>
    public class ExportHelper
    {
        public static IWorkbook CreateWorkbook(string extension = "xls")
        {
            IWorkbook workbook;

            if (extension == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("This format is not supported");
            }

            return workbook;
        }

        public static void WriteExcelWithNPOI(HttpContext httpContext, string templateName, string[] header, String extension = "xls", string[] side = null)
        {
            IWorkbook workbook = CreateWorkbook(extension);

            ISheet sheet1 = workbook.CreateSheet("Sheet 1");

            //make a header row
            IRow row1 = sheet1.CreateRow(0);

            for (int i = 0; i < header.Length; i++)
            {
                ICell cell = row1.CreateCell(i + 1);
                cell.SetCellValue(header[i]);
            }

            if (side != null)
            {
                for (int j = 0; j < side.Length; j++)
                {
                    IRow row = sheet1.CreateRow(j + 1);
                    ICell cell = row.CreateCell(0);
                    cell.SetCellValue(side[j]);
                }
            }
            workbook.WriteToResponse(httpContext, templateName);
        }

        public static void WriteExcelWithNPOI(HttpContext httpContext, string templateName, DataTable dt, String extension = "xls")
        {
            IWorkbook workbook = CreateWorkbook(extension);

            ISheet sheet1 = workbook.CreateSheet("Sheet 1");

            //make a header row
            IRow row1 = sheet1.CreateRow(0);

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell cell = row1.CreateCell(j);
                String columnName = dt.Columns[j].ToString();
                cell.SetCellValue(columnName);
            }

            //loops through data
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    String columnName = dt.Columns[j].ToString();
                    cell.SetCellValue(dt.Rows[i][columnName].ToString());
                }
            }

            workbook.WriteToResponse(httpContext, templateName);
        }
    }

    public static class NPOIWookBookExtension
    {
        public static IWorkbook CreateSheeet<T>(this IWorkbook book, string sheetName, string[] header, List<T> entities) where T : class
        {
            ISheet sheet = book.CreateSheet(sheetName);

            //make a header row
            IRow row1 = sheet.CreateRow(0);

            if (header != null)
            {
                for (int j = 0; j < header.Length; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(header[j]);
                }
            }

            Type type = typeof(T);
            if (type == typeof(string))
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 1);

                    ICell cell = row.CreateCell(0);
                    cell.SetCellValue(entities[i].ToString());
                }
                return book;
            }

            PropertyInfo[] properties = type.GetProperties();

            for (int i = 0; i < entities.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);

                for (int j = 0; j < properties.Length; j++)
                {
                    ICell cell = row.CreateCell(j);
                    var value = properties[j].GetValue(entities[i]);
                    cell.SetCellValue(value == null ? "" : value.ToString());
                }
            }

            return book;
        }

        public static IWorkbook CreateSheeet(this IWorkbook book, string sheetName, string[] header)
        {
            ISheet sheet = book.CreateSheet(sheetName);

            //make a header row
            IRow row1 = sheet.CreateRow(0);

            for (int j = 0; j < header.Length; j++)
            {
                ICell cell = row1.CreateCell(j);
                cell.SetCellValue(header[j]);
            }
            return book;
        }

        public static void WriteToResponse(this IWorkbook book, HttpContext httpContext, string fileName)
        {
            var response = httpContext.Response;

            using (var exportData = new MemoryStream())
            {
                response.Clear();
                book.Write(exportData);

                var bookType = book.GetType();

                if (bookType == typeof(XSSFWorkbook)) //xlsx file format
                {
                    response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    response.Headers.Add("Content-Disposition", string.Format("attachment;filename={0}.xlsx", fileName));
                    response.Body.Write(exportData.ToArray());
                }
                else if (bookType == typeof(HSSFWorkbook))  //xls file format
                {

                    response.ContentType = "application/vnd.ms-excel";
                    response.Headers.Add("Content-Disposition", $"attachment;filename=eee.xls");
                    response.Body.Write(exportData.GetBuffer());
                }
            }
        }

        public static void WriteToResponse2(this IWorkbook book, HttpContext httpContext, string templateName)
        {
            var response = httpContext.Response;
            response.ContentType = "application/vnd.ms-excel";
            SetContentDispositionHeader(httpContext, templateName);
            book.Write(response.Body);
        }

        private static void SetContentDispositionHeader(HttpContext httpContext, string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var contentDisposition = new Microsoft.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                contentDisposition.SetHttpFileName(fileName);
                httpContext.Response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();
            }
        }
    }
}