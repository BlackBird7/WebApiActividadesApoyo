using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using WebAppActividadesApoyo.Areas.Actividades.Models;

namespace WebAppActividadesApoyo.Areas.Actividades.Services
{
    public class FicSrvCatActividadesList
    {
        HttpClient FicCliente = new HttpClient();

        public FicSrvCatActividadesList()
        {
            FicCliente.BaseAddress = new Uri("https://localhost:44311/");
            FicCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }//CONSTRUCTOR

        #region LISTA DE EDIFICIOS
        public async Task<List<cat_actividades>> FicGetListCatActividades()
        {

            HttpResponseMessage FicResponse = await FicCliente.GetAsync("GetListActividades2");
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<cat_actividades>>(FicRespuesta);
            }
            return new List<cat_actividades>();
        }
        #endregion

        #region DETALLE EDIFICIO (GET BY ID)
        public async Task<List<cat_actividades>> FicGetListCatActividadesId(Int16 IdActividad)
        {
            HttpResponseMessage FicResponse = await FicCliente.GetAsync("GetActividad?IdActividad=" + IdActividad);
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<cat_actividades>>(FicRespuesta);
            }
            return new List<cat_actividades>();
        }
        #endregion

        #region CREAR NUEVO EDIFICIO
        public async Task<bool> FicGetListCatActividadesNuevo(cat_actividades item)
        {
            string url = "CreateActividades" +
                "2";
            HttpResponseMessage response = await FicCliente.PostAsync(
                new Uri(string.Format(FicCliente.BaseAddress + url, string.Empty)),
                new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json")
            );
            if (response.IsSuccessStatusCode) { }
            return response.IsSuccessStatusCode;
        }
        #endregion

        #region ACTUALIZAR EDIFICIO
        public async Task<bool> FicGetListCatActividadesUpdate(cat_actividades item)
        {
            string url = "updateActividad";
            HttpResponseMessage response = await FicCliente.PostAsync(
                new Uri(string.Format(FicCliente.BaseAddress + url, string.Empty)),
                new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json")
            );
            if (response.IsSuccessStatusCode)
            {
                //response.Content.ReadAsStringAsync()
            }
            return response.IsSuccessStatusCode;
        }
        #endregion

        #region ELIMINAR EDIFICIO
        public async Task<bool> FicGetListCatActividadesDelete(Int16 IdActividad)
        {
            HttpResponseMessage FicResponse = await FicCliente.GetAsync("deleteActividad?IdActividad=" + IdActividad);
            if (FicResponse.IsSuccessStatusCode) { }
            return true;
        }
        #endregion
    }
}
