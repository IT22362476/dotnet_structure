using Audit.EntityFramework;
using AutoMapper.Configuration.Annotations;
using System.Text.Json;

namespace Inv.WebAPI.Utility
{
    public class Helper
    {
        public static string AuditData(EventEntry eventEntry)
        {
            Root root = new Root();
            root.Action = eventEntry.Action;
            root.OrginalSerialID = Convert.ToInt64(eventEntry.PrimaryKey.FirstOrDefault().Value);
            root.EditedSerialID = eventEntry.Action switch
            {
                "Insert" => 0,
                "Update" => Convert.ToInt64(eventEntry.PrimaryKey.FirstOrDefault().Value),
                "Delete" => 0,
                _ => 0,
            };
            List<Column_Name> columns = new List<Column_Name>();
            switch (eventEntry.Action)
            {
                case "Insert":
                    foreach (var item in eventEntry.ColumnValues)
                    {
                        if(item.Value!=null)
                        columns.Add(new Column_Name { ColumnName = item.Key, OriginalValue = item.Value, NewValue = "" });
                    }
                    root.ColumnNames = null;
                    break;
                case "Update":
                    foreach (var item in eventEntry.Changes)
                    {
                        if(item.ColumnName== "IsDelete")
                        {
                            bool newValue = (bool)item.NewValue;
                            if (newValue)
                            {
                                root.Action = "Delete";
                                root.EditedSerialID = 0;
                            }
                        }

                        if (item.NewValue != null)
                            columns.Add(new Column_Name { ColumnName = item.ColumnName, OriginalValue = item.OriginalValue, NewValue = item.NewValue });
                    }
                    if(root.Action=="Update")
                    root.ColumnNames = columns;
                    break;
                case "Delete":
                    break;
                default:
                    break;
            }

            string auditData = JsonSerializer.Serialize(root);
            return auditData;
        }
    }
    
    public class Column_Name
    {
        public string? ColumnName { get; set; }
        public object? OriginalValue { get; set; }
        public object? NewValue { get; set; }
    }

    public class Root
    {
        [MappingOrder(0)]
        public string? Action { get; set; }
        public long OrginalSerialID { get; set; }
        public long? EditedSerialID { get; set; }
        public List<Column_Name>? ColumnNames { get; set; }
    }
}
