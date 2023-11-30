using GOP.BD.Data;
using GOP.BD.Data.Entity;
using GOP.Repositorio;
using GOP.Shared.ENUM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Security.Claims;

namespace GOP.Server.Controllers
{
    public class GOPBaseController : ControllerBase
    {
        protected string ObtenerUserId()
        {
            return HttpContext.User.Claims
                            .Where(claim => claim.Type == "Id")
                            .FirstOrDefault().Value;
        }

        protected GOPUser ObtenerGOPUser(UserManager<GOPUser> userManager)
        {
            return userManager.FindByIdAsync(ObtenerUserId()).Result;
        }
        protected Persona ObtenerPersona<T>(IRepositorio<T> repositorio,
                                            UserManager<GOPUser> userManager)
                                            where T : class, IEntidadBase
        {
            GOPUser usr = ObtenerGOPUser(userManager);
            Persona res = repositorio.Context
                          .Set<Persona>()
                          .FirstOrDefault(e => e.Id == usr.PersonaId);

            if (res == null) return null;
            return res;
        }
        protected string ObtenerRol()
        {
            return HttpContext.User.Claims
                            .Where(claim => claim.Type == ClaimTypes.Role)
                            .FirstOrDefault().Value;
        }
        protected void ActualizaEntidadBase<T>(T entidad) where T : class, IEntidadBase
        {
            string usuarioId = ObtenerUserId();

            if (entidad.Id == 0)
            {
                entidad.IdCrea = usuarioId;
                entidad.FechaCrea = DateTime.UtcNow;
                entidad.IdModif = usuarioId;
                entidad.FechaModif = DateTime.UtcNow;
            }
            else if (entidad.Id != 0
                     && entidad.EstadoRegistro != EnumEstadoRegistro.borrado)
            {
                entidad.IdModif = usuarioId;
                entidad.FechaModif = DateTime.UtcNow;
            }
            else if (entidad.Id != 0
                     && entidad.EstadoRegistro == Shared.ENUM.EnumEstadoRegistro.borrado)
            {
                entidad.IdBaja = usuarioId;
                entidad.FechaBaja = DateTime.UtcNow;
            }
        }
        protected bool EsProfesionalDeZona<T>(IRepositorio<T> repositorio,
                                              int personaId,
                                              int zonaId)
                                            where T : class, IEntidadBase
        {
            var list = repositorio.Context.Set<ZonaProfesional>()
                    .Where(x => x.ZonaId == zonaId && x.PersonaId == personaId)
                    .ToList();
            if (list == null || list.Count == 0) return false;
            return true;
        }
        protected bool EsProfesionalDeFrente<T>(IRepositorio<T> repositorio,
                                              int personaId,
                                              int frenteObraId)
                                            where T : class, IEntidadBase
        {
            var list = repositorio.Context.Set<FrenteObraProfesional>()
                    .Where(x => x.FrenteObraId == frenteObraId && x.PersonaId == personaId)
                    .ToList();
            if (list == null || list.Count == 0) return false;
            return true;
        }
        // Metodo para generar Reportes de Tipo Excel
        protected byte[] ExportToExcel<T>(List<T> table, string filename)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
            ws.Cells["A1"].LoadFromCollection(table, true, TableStyles.Light1);
            return pack.GetAsByteArray();
        }
    }
}
