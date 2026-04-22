using ClosedXML.Excel;
using Papa.Facturacion.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Papa.Facturacion.Business.Implementations
{
    public class ExcelService : IExcelService
    {
        public MemoryStream ExportExcel<TData>(ICollection<TData> data, string namePage)
        {
            using var workbook = new XLWorkbook();
            var workSheet = workbook.Worksheets.Add(namePage);

            var properties = typeof(TData).GetProperties();
            var rows = 1;
            int columnNumber = 0;
            int longProperties = 0;


            //Header
            foreach (var item in properties)
            {
                var attribute = item.GetCustomAttributes<DisplayAttribute>();
                if (!attribute.Any()) continue;

                columnNumber++;
                var columnName = attribute.FirstOrDefault()!.Name;
                longProperties++;
                // Si el Name del Display es nulo, usamos el nombre de la propiedad
                workSheet.Cell(rows, columnNumber).Value = columnName ?? item.Name;
            }

            // AJUSTE DE SEGURIDAD: Si no hay propiedades con Display, evitamos que se caiga
            if (longProperties == 0)
            {
                workSheet.Cell(1, 1).Value = "No hay columnas configuradas para exportación (Falta atributo Display).";
                workSheet.Columns().AdjustToContents();
                //
                var streamError = new MemoryStream();
                workbook.SaveAs(streamError);
                streamError.Position = 0;
                return streamError;
            }

            var header = workSheet.Range(rows, 1, rows, longProperties);
            header.Style.Font.Bold = true;
            header.Style.Fill.BackgroundColor = XLColor.LightBlue;
            header.Style.Font.FontColor = XLColor.White;
            header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            header.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            header.Style.Border.OutsideBorderColor = XLColor.Black;

            //Body
            foreach (var item in data)
            {
                rows++;
                int column = 0;
                foreach (var property in properties)
                {
                    var attribute = property.GetCustomAttributes<DisplayAttribute>();
                    if (!attribute.Any()) continue;
                    column++;
                    var value = property.GetValue(item, null);
                    workSheet.Cell(rows, column).Value = value?.ToString() ?? string.Empty;
                }

                var body = workSheet.Range(rows, 1, rows, longProperties);
                header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                body.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                body.Style.Border.OutsideBorderColor = XLColor.Black;
            }

            //Page
            workSheet.Columns().AdjustToContents();

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;
            return stream;
        }
    }
}
