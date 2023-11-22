using AutoMapper;
using IISPI.BD.Data.Entity;
using IISPI.Shared.DTOs.Identity;
using IISPI.Shared.DTOs.IINSPIDtos;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EFCorePeliculas.Servicios
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegistrarUserDTO, Persona>();

            //MODULO ITEMS
            CreateMap<UnidadesDTO, Unidad>();

            CreateMap<ItemsDTO, Item>();

            CreateMap<ItemsControlsDTO, ItemControl>();

            CreateMap<ItemsControlsDocsDTO, ItemControlDoc>();

            CreateMap<ItemsControlsParamsDTO, ItemControlParam>();
        }
    }
}
