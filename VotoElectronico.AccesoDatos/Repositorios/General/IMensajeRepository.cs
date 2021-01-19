using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotoElectronico.Entidades;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace VotoElectronico.AccesoDatos.Repositorios
{
    public interface IMensajeRepository: IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Ge01_Mensaje> FiltrarUsuario(ISpecification<Ge01_Mensaje> specification);
    }
}
