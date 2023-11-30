using AutoMapper;
using GOP.BD.Data.Entity;
using GOP.Shared.DTOs.Identity;
using GOP.Shared.DTOs.Entity;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Globalization;

namespace EFCorePeliculas.Servicios
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<RegistrarUserDTO, Persona>();
            CreateMap<PersonaDTO, Persona>();
            CreateMap<Persona, PersonaDTO>();

            //MODULO ITEMS
            CreateMap<UnidadDTO, Unidad>();
            CreateMap<Unidad, UnidadDTO>();

            CreateMap<ItemDTO, Item>();
            CreateMap<Item, ItemDTO>();

            CreateMap<ItemDoc, ItemDocDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.UbicacionDoc.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.UbicacionDoc.X));

            CreateMap<ItemDocDTO, ItemDoc>()
                .ForMember(x => x.UbicacionDoc, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<ItemControlDTO, ItemControl>();
            CreateMap<ItemControl, ItemControlDTO>();

            CreateMap<ItemControlDocDTO, ItemControlDoc>();
            CreateMap<ItemControlDoc, ItemControlDocDTO>();

            CreateMap<ItemControlParamDTO, ItemControlParam>();
            CreateMap<ItemControlParam, ItemControlParamDTO>();

            CreateMap<EstructuraTipoDTO, EstructuraTipo>();
            CreateMap<EstructuraTipo, EstructuraTipoDTO>();

            CreateMap<CalleDTO, Calle>()
                .ForMember(x => x.UbicacionInicio, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.LongitudInicio, y.LatitudInicio))))
                .ForMember(x => x.UbicacionCentral, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.LongitudCentral, y.LatitudCentral))))
                .ForMember(x => x.UbicacionFin, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.LongitudFin, y.LatitudFin))));

            CreateMap<Calle, CalleDTO>()
                .ForMember(x => x.LatitudInicio, x => x.MapFrom(y => y.UbicacionInicio.Y))
                .ForMember(x => x.LongitudInicio, x => x.MapFrom(y => y.UbicacionInicio.X))
                .ForMember(x => x.LatitudCentral, x => x.MapFrom(y => y.UbicacionCentral.Y))
                .ForMember(x => x.LongitudCentral, x => x.MapFrom(y => y.UbicacionCentral.X))
                .ForMember(x => x.LatitudFin, x => x.MapFrom(y => y.UbicacionFin.Y))
                .ForMember(x => x.LongitudFin, x => x.MapFrom(y => y.UbicacionFin.X));

            //MODULO ORGANIZACIONES
            CreateMap<EmpresaDTO, Empresa>()
                .ForMember(x => x.UbicacionEmpresa, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<Empresa, EmpresaDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.UbicacionEmpresa.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.UbicacionEmpresa.X));

            CreateMap<EmpresaProfesionalDTO, EmpresaProfesional>();
            CreateMap<EmpresaProfesional, EmpresaProfesionalDTO>();

            CreateMap<ZonaDTO, Zona>()
                .ForMember(x => x.UbicacionZona, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));
            CreateMap<Zona, ZonaDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.UbicacionZona.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.UbicacionZona.X));

            CreateMap<ZonaProfesionalDTO, ZonaProfesional>();
            CreateMap<ZonaProfesional, ZonaProfesionalDTO>();

            CreateMap<FrenteObraDTO, FrenteObra>()
                .ForMember(x => x.UbicacionFrente, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));
            CreateMap<FrenteObra, FrenteObraDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.UbicacionFrente.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.UbicacionFrente.X));

            CreateMap<FrenteObraProfesionalDTO, FrenteObraProfesional>();
            CreateMap<FrenteObraProfesional, FrenteObraProfesionalDTO>();

            //MODULO CONTRATO
            CreateMap<Contrato, ContratoDTO>();
            CreateMap<ContratoDTO, Contrato>();

            CreateMap<ContratoDoc, ContratoDocDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.UbicacionDoc.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.UbicacionDoc.X));

            CreateMap<ContratoDocDTO, ContratoDoc>()
                .ForMember(x => x.UbicacionDoc, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<ContratoItem, ContratoItemDTO>();
            CreateMap<ContratoItemDTO, ContratoItem>();

            CreateMap<ContratoEstructura, ContratoEstructuraDTO>()
                .ForMember(x => x.LatitudInicio, x => x.MapFrom(y => y.UbicacionInicio.Y))
                .ForMember(x => x.LongitudInicio, x => x.MapFrom(y => y.UbicacionInicio.X))
                .ForMember(x => x.LatitudCentral, x => x.MapFrom(y => y.UbicacionCentral.Y))
                .ForMember(x => x.LongitudCentral, x => x.MapFrom(y => y.UbicacionCentral.X))
                .ForMember(x => x.LatitudFin, x => x.MapFrom(y => y.UbicacionFin.Y))
                .ForMember(x => x.LongitudFin, x => x.MapFrom(y => y.UbicacionFin.X));

            CreateMap<ContratoEstructuraDTO, ContratoEstructura>()
                .ForMember(x => x.UbicacionInicio, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.LongitudInicio, y.LatitudInicio))))
                .ForMember(x => x.UbicacionCentral, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.LongitudCentral, y.LatitudCentral))))
                .ForMember(x => x.UbicacionFin, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.LongitudFin, y.LatitudFin))));

            CreateMap<ContratoEstructuraDoc, ContratoEstructuraDocDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.UbicacionDoc.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.UbicacionDoc.X));

            CreateMap<ContratoEstructuraDocDTO, ContratoEstructuraDoc>()
                .ForMember(x => x.UbicacionDoc, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<ContratoItemControl, ContratoItemControlDTO>();
            CreateMap<ContratoItemControlDTO, ContratoItemControl>();

            CreateMap<ContratoItemControlDoc, ContratoItemControlDocDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.UbicacionDoc.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.UbicacionDoc.X));
            CreateMap<ContratoItemControlDocDTO, ContratoItemControlDoc>()
                .ForMember(x => x.UbicacionDoc, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<ContratoItemControlParam, ContratoItemControlParamDTO>();
            CreateMap<ContratoItemControlParamDTO, ContratoItemControlParam>();

            //MODULO CERTIFICADO
            CreateMap<Certificado, CertificadoDTO>();
            CreateMap<CertificadoDTO, Certificado>();

            CreateMap<CertificadoDoc, CertificadoDocDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.UbicacionDoc.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.UbicacionDoc.X));

            CreateMap<CertificadoDocDTO, CertificadoDoc>()
                .ForMember(x => x.UbicacionDoc, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));


            CreateMap<CertificadoItem, CertificadoItemDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.Ubicacion.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.Ubicacion.X));
            CreateMap<CertificadoItemDTO, CertificadoItem>()
                .ForMember(x => x.Ubicacion, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<CertificadoItemDef, CertificadoItemDefDTO>();
            CreateMap<CertificadoItemDefDTO, CertificadoItemDef>();

            CreateMap<CertificadoItemControl, CertificadoItemControlDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.UbicacionControl.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.UbicacionControl.X));

            CreateMap<CertificadoItemControlDTO, CertificadoItemControl>()
                .ForMember(x => x.UbicacionControl, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<CertificadoItemControlDoc, CertificadoItemControlDocDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.UbicacionDoc.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.UbicacionDoc.X));

            CreateMap<CertificadoItemControlDocDTO, CertificadoItemControlDoc>()
                .ForMember(x => x.UbicacionDoc, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<CertificadoItemControlParam, CertificadoItemControlParamDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.UbicacionParam.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.UbicacionParam.X));

            CreateMap<CertificadoItemControlParamDTO, CertificadoItemControlParam>()
                .ForMember(x => x.UbicacionParam, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            //MODULO EVENTOS

            CreateMap<Evento, EventoDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.UbicacionEvento.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.UbicacionEvento.X));

            CreateMap<EventoDTO, Evento>()
                .ForMember(x => x.UbicacionEvento, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<EventoTipo, EventoTipoDTO>();
            CreateMap<EventoTipoDTO, EventoTipo>();

            CreateMap<EventoDoc, EventoDocDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.UbicacionDoc.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.UbicacionDoc.X));

            CreateMap<EventoDocDTO, EventoDoc>()
                .ForMember(x => x.UbicacionDoc, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<EventoParam, EventoParamDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.UbicacionDoc.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.UbicacionDoc.X));

            CreateMap<EventoParamDTO, EventoParam>()
                .ForMember(x => x.UbicacionDoc, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<EventoParamDoc, EventoParamDocDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.UbicacionDoc.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.UbicacionDoc.X));

            CreateMap<EventoParamDocDTO, EventoParamDoc>()
                .ForMember(x => x.UbicacionDoc, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));
        }
    }
}
