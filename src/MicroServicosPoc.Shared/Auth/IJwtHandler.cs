using System;

namespace MicroServicosPoc.Shared.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken Create(string idUsuarioEmail);     
    }
}