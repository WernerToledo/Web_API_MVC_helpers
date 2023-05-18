using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_API_MVC_helpers.Models;


namespace Web_API_MVC_helpers.Controllers
{
    public class equiposController : Controller
    {
        private readonly equiposContext _equiposContext;
        public equiposController(equiposContext equiposContext)
        {
            _equiposContext= equiposContext;
        }
        //esta es una forma
        /*
        public async Task<IActionResult> IndexAsync()
        {
            return _equiposContext.marcas != null ?
                         View(await _equiposContext.marcas.ToListAsync()) :
                         Problem("Entity set 'equiposContext.equipos'  is null.");
        }
        */

        public IActionResult Index()
        { 
            var listadoequipo = (from e in _equiposContext.equipos
                                 join t in _equiposContext.tipo_equipo on e.tipo_equipo_id equals t.id_tipo_equipo
                                 join et in _equiposContext.estados_equipo on e.estado_equipo_id equals et.id_estados_equipo
                                 join m in _equiposContext.marcas on e.marca_id equals m.id_marca
                                 select new
                                 {
                                     nombre = e.nombre,
                                     descrip = e.descripcion,
                                     tipo_equipo = t.descripcion,
                                     marca = m.nombre_marca,
                                     modelo = e.modelo,
                                     anio = e.anio_compra,
                                     costo = e.costo,
                                     vida_util = e.vida_util,
                                     estados_equipo = et.descripcion,
                                     estado = e.estado
                                 }).ToList();
            ViewData["Listadoequipo"] = listadoequipo;
            return View();
        }

            
        public IActionResult create() 
        {
            var listadoTipoEquipo = (from te in _equiposContext.tipo_equipo select te).ToList();
            var listadoestado = (from e in _equiposContext.estados_equipo select e).ToList();
            var listadoDeMarcas = (from m in _equiposContext.marcas select m).ToList();
            //variable, valor del campo, campo que se quiere                                                
            ViewData["tipo_equipo"] = new SelectList(listadoTipoEquipo, "id_tipo_equipo", "descripcion");
            ViewData["Marcas"] = new SelectList(listadoDeMarcas, "id_marca", "nombre_marca");
            ViewData["estado_equipo"] = new SelectList(listadoestado, "id_estados_equipo", "descripcion");
            return View();
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult CrearEquipo(equipos nuevoequipo)
        {
            _equiposContext.Add(nuevoequipo);
            _equiposContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
