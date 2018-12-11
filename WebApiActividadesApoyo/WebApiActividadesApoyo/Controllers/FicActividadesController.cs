using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebApiActividadesApoyo.Models;
using WebApiActividadesApoyo.Data;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace WebApiActividadesApoyo.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class FicActividadesController : ControllerBase
    {
        private readonly FicDBContext _context;

        public FicActividadesController(FicDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return null;
        }
        #region Get
        [HttpGet]
        [Route("GetListActividades")]
        public ContentResult GetList()
        {
            var consulta = from c in _context.cat_actividades
                           select new
                           {
                               c.IdActividad,
                               c.DesActividad,
                               c.Detalle,
                               c.UsuarioReg,
                               c.FechaReg,
                               c.UsuarioMod,
                               c.FechaUltMod,
                               c.Activo,
                               c.Borrado,
                           };

            String FicSerializa = JsonConvert.SerializeObject(consulta);
            return Content(FicSerializa, "application/json");
        }
        #endregion
        //GET ALL (LISTA)

        #region GET TODO WORKiNG
        [HttpGet]
        [Route("GetListActividades2")]
        public async Task<ActionResult> GetListActividades2()
        {
            try
            {
                var response = _context.cat_actividades.ToList();
                return Ok(response);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                return NotFound("No hay Registros");
            }
        }
        #endregion


        #region GET BY ID (DETALLE)

        [HttpGet("{IdActividad}")]
        [Route("GetActividad")]
        public async Task<ActionResult> GetById(Int32 IdActividad)
        {
            try
            {
                var response = _context.cat_actividades.Where(x => x.IdActividad == IdActividad).ToList();

                return Ok(response);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                return NotFound("No existe la colonia con el id: " + IdActividad);
            }

        }
        #endregion


        #region CREATE
        [HttpPost]
        [Route("CreateActividad")]
        public ContentResult CreateNivelCompetencia([FromBody] cat_actividades item)
        {
            if (item != null)
            {
                _context.cat_actividades.Add(item);
                try
                {
                    var response = _context.SaveChanges();
                    return Content("{ Se han creado: " + response + " Registro(s) exitosamente}", "application/json");
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
                {
                    return Content("{'Respuesta':'Error!','Exception':'" + e.Message + "'}", "application/json");
                }
            }
            else
            {
                return Content("{'Respuesta':'HORROR!!'}", "application/json");
            }
        }
        #endregion


        #region NUEVO WORKING

        [HttpPost]
        [Route("CreateActividad2")]
        public ContentResult CreateActividad2([FromBody] cat_actividades item)
        {
            if (item != null)
            {
                _context.cat_actividades.Add(item);
                try
                {
                    var getMaxId = _context.cat_actividades.Max(x => x.IdActividad);

                    item.IdActividad = (Int32)(getMaxId + 1);

                    var response = _context.SaveChanges();
                    return Content("{ Se han creado: " + response + " Registro(s) exitosamente}", "application/json");

                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
                {
                    return Content("{'Respuesta':'Error!','Exception':'" + e.Message + "'}", "application/json");
                }
            }
            else
            {
                return Content("{'Respuesta':'HORROR!!'}", "application/json");
            }
        }

        #endregion


        #region ACTUALIZA
        [HttpPost]
        [Route("updateActividad")]
        public ContentResult ActividadUpdate([FromBody] cat_actividades item)
        {
            if (item != null)
            {
                _context.cat_actividades.Update(item);
                try
                {

                    var response = _context.SaveChanges();
                    return Content("{'Respuesta':'Operacion exitosa','registros actualizados ':'" + response + "'}", "application/json");
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
                {
                    return Content("{'Respuesta':Error!','Exception':'" + e.Message + "'}", "application/json");
                }
            }
            else
            {
                return Content("{'Respuesta':'HORROR!!'}", "application/json");
            }
        }
        #endregion


        #region DELETE
        [HttpDelete("{IdActividad}")]
        [Route("deleteActividad")]
        public async Task<ActionResult> DeleteAsync(Int32 IdActividad)
        {
            if (IdActividad != 0)
            {
                try
                {
                    cat_actividades e = new cat_actividades();
                    e.IdActividad = IdActividad;
                    _context.cat_actividades.Remove(e);
                    _context.SaveChanges();
                    return Ok(true);
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
                {
                    return NotFound("Sin Edificios");
                }
            }
            else
            {
                return Content("{'Respuesta':'HORROR!'}", "application/json");
            }
        }
        #endregion


        #region IMPORTAR
        [HttpGet]
        [Route("ImportarActividades")]
        public async Task<ActionResult> GetListActividades()
        {
            try
            {
                var response = _context.cat_actividades.ToList();
                CatActividades item = new CatActividades();
                item.cat_Actividades = response;
                return Ok(item);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                return NotFound("No hay Registros");
            }
        }

        [HttpGet("{IdActividad}")]
        [Route("GetByIdActividad")]
        public async Task<ActionResult> GetByIdActividad(Int32 IdActividad)
        {

            try
            {
                

                var response = _context.cat_actividades.Where(
                        data => data.IdActividad == IdActividad).ToList();
                CatActividades temp = new CatActividades();
                temp.cat_Actividades = response;
                
                return Ok(temp);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                return NotFound("No existe la colonia con el id: " + IdActividad);
            }
        }

        #endregion


        #region EXPORTAR
        //EXPORTAR
        [HttpPost]
        [Route("ExportActividades")]
        public async Task<ActionResult> ActividadesExport([FromBody] CatActividades value)
        {
            if (value != null)
            {
                try
                {

                    foreach (cat_actividades item
                                in (value.cat_Actividades))
                    {
                        //checa si existe algo en la base
                        var FicResult = (from cat_actividades in _context.cat_actividades
                                         where cat_actividades.IdActividad == item.IdActividad
                                         select new
                                         {
                                             cat_actividades
                                         }).Count() > 0;

                        if (FicResult)
                        {
                            _context.cat_actividades.Update(item);
                        }
                        else
                        {
                            _context.cat_actividades.Add(item);
                        }
                        _context.SaveChanges();
                    }
                    return Ok();
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
                {
                    return NotFound("Error");
                }
            }
            else
            {
                return NotFound("Error");
            }
        }
        #endregion
    }
}