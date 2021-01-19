using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;

namespace VotoElectronico.LogicaCondicional
{
    public class Sg01_UsuarioCondicional
    {

        public ISpecification<Sg01_Usuario> FiltrarUsuario(string nombre = "", string identificacion = "")
        {
            return  new Specification<Sg01_Usuario>(
                x=>(string.IsNullOrEmpty(nombre) || x.NombreUsuario.Contains(nombre)) 
                && (string.IsNullOrEmpty(nombre) || x.Persona.Identificacion.Contains(identificacion))
                );
        }
    }
}
