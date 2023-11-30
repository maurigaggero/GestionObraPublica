using AutoMapper;
using GOP.BD.Data;
using GOP.BD.Data.Entity;
using GOP.Repositorio.Repos;
using GOP.Shared.DTOs.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GOP.Server.Controllers.GOPControllers
{
    [ApiController]
    [Route("GOP/api/contratoitem")]
    public class ContratoItemController : GOPBaseController
    {
        private readonly IContratoItemsRepositorio repositorio;
        private readonly IMapper mapper;

        public ContratoItemController(IContratoItemsRepositorio repositorio, 
                                      IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ContratoItemDTO>>> GetFull()
        {
            try
            {
                List<ContratoItem> lista = await repositorio.GetActivos();

                List<ContratoItemDTO> result = mapper.Map<List<ContratoItemDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getContratoItem")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ContratoItemDTO>>> GetContratoItem() //CON INCLUDE
        {
            try
            {
                List<ContratoItem> lista = await repositorio.GetContratoItems();

                List<ContratoItemDTO> result = mapper.Map<List<ContratoItemDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<ContratoItemDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetContratoItemById(id);
                if (res == null) return NotFound();

                ContratoItemDTO result = mapper.Map<ContratoItemDTO>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getByContratoId/{idContrato:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ContratoItemDTO>>> GetContratoItemByContratoId(int idContrato) //CON INCLUDE
        {
            try
            {
                List<ContratoItem> lista = await repositorio.GetContratoItemsByContratoId(idContrato);

                List<ContratoItemDTO> result = mapper.Map<List<ContratoItemDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos")]
        public async Task<ActionResult<List<ContratoItemDTO>>> Post(ContratoItemDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");
                List<int> resp = new();

                foreach (int iditem in dto.IdsItems)
                {
                    ContratoItem entidad = new()
                    {
                        ContratoId = dto.ContratoId
                    };

                    //ITEM
                    Item item = await repositorio.GetItemDelContrato(iditem);
                    entidad.ItemId = iditem;
                    entidad.DescItem = item.DescItem;
                    entidad.CodItem = item.CodItem;

                    List<ContratoItemControl> listcontratoitemcontrol = new();
                    foreach (ItemControl itemcontrol in item.ItemControles)
                    {
                        //ITEM CONTROL
                        ContratoItemControl contratoitemcontrol = new()
                        {
                            ItemControlId = itemcontrol.Id,
                            DescControl = itemcontrol.DescControl,
                            CodControl = itemcontrol.CodControl
                        };

                        List<ContratoItemControlParam> contratoparametros = new();
                        foreach (ItemControlParam itemparam in itemcontrol.Parametros)
                        {
                            //PARAMETROS
                            ContratoItemControlParam contratoparam = new()
                            {
                                ItemControlParamId = itemparam.Id,
                                Parametro = itemparam.Parametro,
                                TipoParam = itemparam.TipoParam,
                                Descripción = itemparam.Descripción,
                                ValorMinimo = itemparam.ValorMinimo,
                                ValorMaximo = itemparam.ValorMaximo,
                                UnidadId = itemparam.UnidadId
                            };

                            #region Actualiza idcrea ContratoItemControlParam
                            ActualizaEntidadBase<ContratoItemControlParam>(contratoparam);
                            #endregion

                            contratoparametros.Add(contratoparam);
                        }
                        contratoitemcontrol.Parametros = contratoparametros;

                        #region Actualiza idcrea ContratoItemControl
                        ActualizaEntidadBase<ContratoItemControl>(contratoitemcontrol);
                        #endregion

                        listcontratoitemcontrol.Add(contratoitemcontrol);
                    }
                    entidad.ContratoItemControls = listcontratoitemcontrol;

                    #region Actualiza idcrea ContratoItem
                    ActualizaEntidadBase<ContratoItem>(entidad);
                    #endregion

                    int result = await repositorio.Insert(entidad);
                    if (result > 0)
                        resp.Add(result);
                    else
                        return BadRequest("No se pudo agregar el tipo de estado");
                }

                if (resp.Count > 0)
                {
                    List<ContratoItem> listObj = new();
                    foreach (int i in resp)
                    {
                        var obj = await repositorio.GetById(i);
                        listObj.Add(obj);
                    }

                    List<ContratoItemDTO> list = mapper.Map<List<ContratoItemDTO>>(listObj);
                    return Ok(list);
                }
                else
                    return BadRequest("No se pudo agregar el tipo de estado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos")]
        public async Task<ActionResult<bool>> Put(ContratoItemDTO dto, int id)
        {
            try
            {
                dto.Item = null;
                dto.Contrato = null;
                dto.ContratoItemControls = null;
                ContratoItem entidad = mapper.Map<ContratoItem>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<ContratoItem>(entidad);
                #endregion

                var resp = await repositorio.Update(entidad, id);
                if (resp)
                {
                    return Ok(resp);
                }
                else
                {
                    return BadRequest("Datos incorrectos");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("baja/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos")]
        public async Task<ActionResult> Baja(int id)
        {
            try
            {
                if (await repositorio.Baja(id, ObtenerUserId()))
                {
                    return Ok($"El ContratoItem ha sido dado de baja del sistema");
                }
                return NotFound("El ContratoItem a dar de baja no a sido encontrado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpDelete("{id:int}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        //Roles = "Admin, BaseDatos")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        if (await repositorio.Delete(id))
        //        {
        //            return Ok($"El ContratoItem ha sido borrado");
        //        }
        //        return NotFound("El Tipo de Estado que desea borrar no a sido encontrado");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
