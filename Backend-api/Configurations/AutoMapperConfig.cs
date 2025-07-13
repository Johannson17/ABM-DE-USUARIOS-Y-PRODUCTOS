using AutoMapper;
using Backend.Api.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Backend.Api.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Ejemplo: CreateMap<Origen, Destino>();
            // CreateMap<Usuario, UsuarioDto>();
            // CreateMap<Articulo, ArticuloDto>();
        }
    }
}
