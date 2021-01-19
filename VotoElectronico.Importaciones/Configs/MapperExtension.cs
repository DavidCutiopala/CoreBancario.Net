using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VotoElectronico.Generico.Configs;

namespace VotoElectronico.Importaciones.Configs
{
    public static class MapperExtention
    {
        public static IEnumerable<T> ConvertSheetToObjects<T>(this ExcelWorksheet worksheet) where T : new()
        {

            Func<CustomAttributeData, bool> columnOnly = y => y.AttributeType == typeof(Column);

            var columns = typeof(T)
                    .GetProperties()
                    .Where(x => x.CustomAttributes.Any(columnOnly))
            .Select(p => new
            {
                Property = p,
                Column = p.GetCustomAttributes<Column>().First().ColumnIndex
            }).ToList();


            var rows = worksheet.Cells
                .Select(cell => cell.Start.Row)
                .Distinct()
                .OrderBy(x => x);


            var collection = rows.Skip(1)
                .Select(row =>
                {
                    var tnew = new T();
                    columns.ForEach(col =>
                    {
                        var val = worksheet.Cells[row, col.Column];
                        try
                        {
                            if (val.Value == null)
                            {
                                col.Property.SetValue(tnew, null);
                                return;
                            }
                            if (col.Property.PropertyType == typeof(Int32))
                            {
                                col.Property.SetValue(tnew, val.GetValue<int>());
                                return;
                            }
                            if (col.Property.PropertyType == typeof(double))
                            {
                                col.Property.SetValue(tnew, val.GetValue<double>());
                                return;
                            }
                            if (col.Property.PropertyType == typeof(DateTime))
                            {
                                var s = val.GetValue<string>();
                                DateTime d;
                                if (DateTime.TryParse(s, out d))
                                    col.Property.SetValue(tnew, val.GetValue<DateTime>());
                                else
                                    col.Property.SetValue(tnew, DateTime.MinValue);
                                return;
                            }
                            if (col.Property.PropertyType == typeof(Boolean))
                            {
                                col.Property.SetValue(tnew, val.GetValue<bool>());
                                return;
                            }
                            if (col.Property.PropertyType == typeof(String))
                            {
                                col.Property.SetValue(tnew, val.GetValue<string>()?.Trim());
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    });

                    return tnew;
                });
            return collection;
        }
    }
}
