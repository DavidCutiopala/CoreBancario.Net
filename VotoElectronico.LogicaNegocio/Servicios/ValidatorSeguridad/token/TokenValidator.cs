using EcVotoElectronico.Repositorios;
using System.Net;
using System.Web;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico.Propiedades;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class TokenValidator: ITokenValidator
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public TokenValidator(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public void ValidarToken(string token)
        {
            if (!_usuarioRepository.GetExists<Sg01_Usuario>(x => x.Token == token && x.Estado == Auditoria.EstadoActivo && x.InicioSesion))
                throw new HttpException(HttpStatusCode.Unauthorized.ToString());
        }
        public bool ExisteTokenUsuarioCambioClave(string token)
        => _usuarioRepository.GetExists<Sg01_Usuario>(x => x.TokenCambioClave.Equals(token) && x.Estado == Auditoria.EstadoActivo);
    }
}
