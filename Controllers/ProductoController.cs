#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD_PRODUCTOSMVC;
using CRUD_PRODUCTOSMVC.Models;
using System.Linq.Dynamic.Core;
using System.Collections.Specialized;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace CRUD_PRODUCTOSMVC.Controllers
{
    public class ProductoController : Controller
    {
        private readonly productoContext _context;

        public ProductoController(productoContext context)
        {
            _context = context;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            var draw = HttpContext.Request.Query["draw"].FirstOrDefault();

            var start = HttpContext.Request.Query["start"].FirstOrDefault();
            var length = HttpContext.Request.Query["length"].FirstOrDefault();
            var sortColumn = HttpContext.Request.Query["columns[" + HttpContext.Request.Query["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = HttpContext.Request.Query["order[0][dir]"].FirstOrDefault();
            var searchValue = HttpContext.Request.Query["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var p = (from t in _context.Productos orderby t.id select t);
            recordsTotal = p.Count();
            var data = p.Skip(skip).Take(pageSize).ToListAsync();

            return View(await data);
        }
        public IActionResult get()
        {
            // var draw = HttpContext.Request.Query["draw"].FirstOrDefault();

            var draw = HttpContext.Request.Query["draw"].FirstOrDefault();

            var start = HttpContext.Request.Query["start"].FirstOrDefault();
            var length = HttpContext.Request.Query["length"].FirstOrDefault();
            var sortColumn = HttpContext.Request.Query["columns[" + HttpContext.Request.Query["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = HttpContext.Request.Query["order[0][dir]"].FirstOrDefault();
            var searchValue = HttpContext.Request.Query["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var p = from t in _context.Productos select t;
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                p = p.OrderBy(x => x.nombre + " " + x.nombre);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                p = p.Where(m => m.nombre.Contains(searchValue));
            }
            recordsTotal = p.Count();
            var data = p.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
        }

        

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,caracteristicas,calidad,precio,fecha_ingreso,unidades,estado")] Producto producto)
        {

            if (ModelState.IsValid)
            {
                
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,caracteristicas,calidad,precio,fecha_ingreso,unidades,estado")] Producto producto)
        {
            if (id != producto.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /*
        public JsonResult CustomServerSideSearchAction(DataTableAjaxPostModel model)
        {
            // action inside a standard controller
            int filteredResultsCount;
            int totalResultsCount;
            var res = YourCustomSearchFunc(model, out filteredResultsCount, out totalResultsCount);

            var result = new List<YourCustomSearchClass>(res.Count);
            foreach (var s in res)
            {
                // simple remapping adding extra info to found dataset
                result.Add(new YourCustomSearchClass
                {
                    EmployerId = User.ClaimsUserId(),
                    Id = s.Id,
                    Pin = s.Pin,
                    Firstname = s.Firstname,
                    Lastname = s.Lastname,
                    RegistrationStatusId = DoSomethingToGetIt(s.Id),
                    Address3 = s.Address3,
                    Address4 = s.Address4
                });
            };

            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
            });
        }

        public IList<YourCustomSearchClass> YourCustomSearchFunc(DataTableAjaxPostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;

            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }

            // search the dbase taking into consideration table sorting and paging
            var result = GetDataFromDbase(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            if (result == null)
            {
                // empty collection...
                return new List<YourCustomSearchClass>();
            }
            return result;
        }

        public List<YourCustomSearchClass> GetDataFromDbase(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            // the example datatable used is not supporting multi column ordering
            // so we only need get the column order from the first column passed to us.        
            var whereClause = BuildDynamicWhereClause(Db, searchBy);

            if (String.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                sortBy = "Id";
                sortDir = true;
            }

            var result = Db.DatabaseTableEntity
                           .AsExpandable()
                           .Where(whereClause)
                           .Select(m => new YourCustomSearchClass
                           {
                               Id = m.Id,
                               Firstname = m.Firstname,
                               Lastname = m.Lastname,
                               Address1 = m.Address1,
                               Address2 = m.Address2,
                               Address3 = m.Address3,
                               Address4 = m.Address4,
                               Phone = m.Phone,
                               Postcode = m.Postcode,
                           })
                           .OrderBy(sortBy, sortDir) // have to give a default order when skipping .. so use the PK
                           .Skip(skip)
                           .Take(take)
                           .ToList();

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            filteredResultsCount = Db.DatabaseTableEntity.AsExpandable().Where(whereClause).Count();
            totalResultsCount = Db.DatabaseTableEntity.Count();

            return result;
        }

*//*
        public paginador<Producto> Get()
        {
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request);
            string sEcho = nvc["sEcho"].ToString();
            string sSearch = nvc["sSearch"].ToString();
            int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
            int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);


            //get total value count
            var Count = _context.Productos.Count();

            //get data from database
         /*   var proc = _context.Productos. //specify conditions if there is any using .Where(Condition)                             
                             Select(d => new Producto
                             {
                                 
                             }).OrderBy(d => d.Id).Skip(iDisplayStart).Take(10);

            var p = _context.Productos.Take(10);

            //Create a model for datatable paging and sending data & enter all the required values
            var CustomerPaged = new paginador<Producto> (p, Count, Count, sEcho);



            return CustomerPaged;
             

        }
    */

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.id == id);
        }
    }
}
