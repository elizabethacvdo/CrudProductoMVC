using CRUD_PRODUCTOSMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_PRODUCTOSMVC.Controllers
{
    public class productosController1 : Controller
    {
       
        private readonly productoContext _contexto;
        
        public productosController1(productoContext pc)
        {
            
                _contexto = pc; 
        }
        /*
        [HttpGet]
        [Route("productos")]
        public IActionResult get()
        {
             IEnumerable<Models.Producto> l = (from m in _contexto.Producto select m);
             if (l != null)
            {
               return View(l);

             }
             return NotFound();
         //   return Ok("hello");

            
        }

        [HttpGet]
        [Route("productos/{id}")]
        public IActionResult getid(int id)
        {
            Producto m = (from e in _contexto.Producto where e.id == id select e).FirstOrDefault();

            if (m != null)
            {
                return View(m);
            }
            return NotFound();
            //   return Ok("hello");

        }


        [HttpPost]
        [Route("productos/add/")]
        public IActionResult add([FromBody] Producto nuevo)
        {

            try
            {
                _contexto.Producto.Add(nuevo);
                _contexto.SaveChanges();
                return View(nuevo);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpPut]
        [Route("productos/")]
        public IActionResult actualizar([FromBody] Producto nuevo)
        {
            Producto existe = (from e in _contexto.Producto where e.id == nuevo.id select e).FirstOrDefault();
            if (existe is null)
            {
                return NotFound();

            }

            existe.nombre = nuevo.nombre;
            existe.precio = nuevo.precio;
            _contexto.Entry(existe).State = EntityState.Modified;
            _contexto.SaveChanges();
            return View(existe);
        }

        [HttpPut]
        [Route("productos/")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Productos producto = _contexto.Producto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }
        */
    }


}

