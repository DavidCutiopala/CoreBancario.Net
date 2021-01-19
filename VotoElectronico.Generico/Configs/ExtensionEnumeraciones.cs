using System;
using System.Reflection;


namespace VotoElectronico.Generico.Configs
{
    public class Enumeration : Attribute
    {
        internal Enumeration(string descripcion)
        {
            this.Descripcion = descripcion;
        }
        public string Descripcion { get; private set; }

        public static string ObtenerDescripcion(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            Enumeration[] attributes = (Enumeration[])fi.GetCustomAttributes(typeof(Enumeration), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Descripcion;
            else
                return value.ToString();
        }
    }
}
