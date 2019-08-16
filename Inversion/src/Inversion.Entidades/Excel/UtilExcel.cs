using Inversion.Entidades.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Inversion.Entidades
{
    public class UtilExcel
    {
        public static ExcelPackage CrearExcelPackage(String pathFile)
        {
            FileInfo newFile = new FileInfo(pathFile);

            if (newFile.Exists)
            {
                newFile.Delete();
            }

            newFile = new FileInfo(pathFile);// ensures we create a new workbook

            //Create the workbook       
            ExcelPackage pck = new ExcelPackage(newFile);
            return pck;
        }

        public static ExcelWorksheet CrearHojaExcel(ExcelPackage pck, String nomHoja)
        {
            //Add the Content sheet
            return pck.Workbook.Worksheets.Add(nomHoja);
        }
        
    

        public static void ListToExcel<T>(ExcelPackage pck,string sheetName, List<T> lista)
        {     
            ExcelWorksheet ws = CrearHojaExcel(pck, sheetName);

            // Se ordenan las columnas en funcion de la propiedad Order del atributo EpplusCol
            var columnas = (from property in typeof(T).GetProperties()
                                    where Attribute.IsDefined(property, typeof(EpplusCol))
                                    orderby ((EpplusCol)property
                                             .GetCustomAttributes(typeof(EpplusCol), false)
                                             .Single()).Order
                                    select property).ToArray();


            ws.Cells["A1"].LoadFromCollection<T>(lista,true, TableStyles.Light1, BindingFlags.Public | BindingFlags.Instance,  columnas);
            ws.Cells.AutoFitColumns();
            pck.Workbook.Calculate();
            pck.Save();        
        }
        public static void OpenMicrosoftExcel(string f)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE";
            startInfo.Arguments = f;
            Process.Start(startInfo);
        }

    }








}
