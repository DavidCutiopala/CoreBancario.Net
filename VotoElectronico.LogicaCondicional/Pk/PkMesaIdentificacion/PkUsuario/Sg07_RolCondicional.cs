using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;

namespace VotoElectronico.LogicaCondicional
{
    public class Sg07_RolCondicional
    {

        public ISpecification<Sg07_Rol> FiltraarRoles(string nombreRol = "", string estado = "")
        {
           

            return  new Specification<Sg07_Rol>(
                rol =>(string.IsNullOrEmpty(nombreRol) || rol.NombreRol.Contains(nombreRol))
                && (string.IsNullOrEmpty(estado) || rol.Estado.Equals(estado))
                );
        }
    }
}
