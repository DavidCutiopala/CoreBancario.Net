
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IUsuarioCBService
    {


        //DtoApiResponseMessage CrearClienteCB(DtoClienteCB dto);
        DtoApiResponseMessage Login(DtoClienteCB dto);


    }
}

