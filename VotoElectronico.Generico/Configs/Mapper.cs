using System;


namespace VotoElectronico.Generico.Configs
{ 
    [AttributeUsage(AttributeTargets.All)]
    public class Column : Attribute
    {
        public int ColumnIndex { get; set; }

        public Column(int column)
        {
            ColumnIndex = column;
        }
    }

    public static class Constantes
    {
        public const int RESOURCES_WORKSHEET = 1;
    }
}
