using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using WebAppActividadesApoyo.Areas.Actividades.Services;
using WebAppActividadesApoyo.Areas.Actividades.Models;

namespace WebAppActividadesApoyo.Areas.Actividades.Controllers
{
    public class CatActividadesController : Controller
    {
        #region LIST 
        public IActionResult FicViCatActividadesList()
        {
            try
            {
                FicSrvCatActividadesList FicServicio = new FicSrvCatActividadesList();

                List<cat_actividades> FicLista = FicServicio.FicGetListCatActividades().Result;
                ViewBag.Title = "Catalogo de Actividades";
                return View(FicLista);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region DETALLE
        public IActionResult FicViCatActividadesDetail(Int16 IdActividad) //IActionResult es para llamar a la vista
        {
            try
            {
                FicSrvCatActividadesList FicServicio = new FicSrvCatActividadesList();

                List<cat_actividades> FicLista = FicServicio.FicGetListCatActividadesId(IdActividad).Result;
                ViewBag.Title = "Detalle";
                return View(FicLista);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region ACTUALIZAR
        public IActionResult FicViActividadesUpdate(Int16 IdActividad) //IActionResult es para llamar a la vista
        {
            try
            {
                FicSrvCatActividadesList FicServicio = new FicSrvCatActividadesList();

                List<cat_actividades> FicLista = FicServicio.FicGetListCatActividadesId(IdActividad).Result;
                ViewBag.Title = "| Edita... |";
                return View(FicLista);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<IActionResult> FicUpdate(cat_actividades item)
        {
            item.UsuarioMod = "Leo";
            item.FechaUltMod = DateTime.Now;

            FicSrvCatActividadesList FicServicio = new FicSrvCatActividadesList();
            bool FicLista = await FicServicio.FicGetListCatActividadesUpdate(item);
            return RedirectToAction("FicViCatActividadesList");
        }
        #endregion

        #region NUEVO
        public IActionResult FicViCatActividadesCreate()
        {
            try
            {
                ViewBag.Title = "Nuevo ";
                return View();
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<IActionResult> Create(cat_actividades item)
        {
            FicSrvCatActividadesList FicServicio = new FicSrvCatActividadesList();

            item.UsuarioReg = "Leo";
            item.FechaReg = DateTime.Now;

            bool FicLista = await FicServicio.FicGetListCatActividadesNuevo(item);

            return RedirectToAction("FicViCatActividadesList");
         
        }
        #endregion

        #region ELIMINAR
        public async Task<IActionResult> FicDelete(Int16 IdActividad)
        {
            FicSrvCatActividadesList FicServicio = new FicSrvCatActividadesList();

            bool FicLista = await FicServicio.FicGetListCatActividadesDelete(IdActividad);
            return RedirectToAction("FicViCatActividadesList");
        } 
        #endregion
    }
}