using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControlInventarioMVC.Models;

namespace ControlInventarioMVC.Controllers
{
    public class MovimientosInventarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovimientosInventarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovimientosInventario
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MovimientosInventario.Include(m => m.Articulo).Include(m => m.Ubicacion);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MovimientosInventario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimientoInventario = await _context.MovimientosInventario
                .Include(m => m.Articulo)
                .Include(m => m.Ubicacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimientoInventario == null)
            {
                return NotFound();
            }

            return View(movimientoInventario);
        }

        // GET: MovimientosInventario/Create
        public IActionResult Create()
        {
            // Asignamos las opciones para los select
            ViewBag.ArticuloId = new SelectList(_context.Articulos, "Id", "Nombre");
            ViewBag.UbicacionId = new SelectList(_context.Ubicaciones, "Id", "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArticuloId,UbicacionId,Cantidad,TipoMovimiento,FechaMovimiento")] MovimientoInventario movimiento)
        {
            List<string> errores = new List<string>();

          
            var validaciones = new Dictionary<string, Func<bool>>
    {
        { "ArticuloId", () => movimiento.ArticuloId != 0 },
        { "UbicacionId", () => movimiento.UbicacionId != 0 },
        { "Cantidad", () => movimiento.Cantidad > 0 },
        { "TipoMovimiento", () => !string.IsNullOrEmpty(movimiento.TipoMovimiento) },
        { "FechaMovimiento", () => movimiento.Fecha != default(DateTime) }
    };

            foreach (var validacion in validaciones)
            {
                if (!validacion.Value())
                {
                    errores.Add($"{validacion.Key} es obligatorio o tiene un valor inválido.");
                }
            }

          
            if (movimiento.TipoMovimiento == "Salida")
            {
                var articulo = await _context.Articulos.FindAsync(movimiento.ArticuloId);
                if (articulo == null)
                {
                    errores.Add("El artículo no existe.");
                }
                else if (articulo.Stock < movimiento.Cantidad)
                {
                    errores.Add("No hay suficiente stock para realizar esta salida.");
                    ViewBag.StockInsuficiente = true; 
                }
            }

   
            if (errores.Any())
            {
                ViewBag.Errores = errores;
                ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Nombre", movimiento.ArticuloId);
                ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Nombre", movimiento.UbicacionId);
                return View(movimiento);
            }

          
            var articuloActual = await _context.Articulos.FindAsync(movimiento.ArticuloId);
            if (articuloActual != null)
            {
                if (movimiento.TipoMovimiento == "Entrada")
                {
                    articuloActual.Stock += movimiento.Cantidad;
                }
                else if (movimiento.TipoMovimiento == "Salida")
                {
                    articuloActual.Stock -= movimiento.Cantidad;
                }
                _context.Update(articuloActual);
            }

            _context.Add(movimiento);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }





        // GET: MovimientosInventario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimientoInventario = await _context.MovimientosInventario.FindAsync(id);
            if (movimientoInventario == null)
            {
                return NotFound();
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Id", movimientoInventario.ArticuloId);
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Nombre", movimientoInventario.UbicacionId);
            return View(movimientoInventario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArticuloId,UbicacionId,Cantidad,Fecha,TipoMovimiento")] MovimientoInventario movimientoInventario)
        {
            if (id != movimientoInventario.Id)
            {
                return NotFound();
            }

            List<string> errores = new List<string>();


            var validaciones = new Dictionary<string, Func<bool>>
    {
        { "ArticuloId", () => movimientoInventario.ArticuloId != 0 },
        { "UbicacionId", () => movimientoInventario.UbicacionId != 0 },
        { "Cantidad", () => movimientoInventario.Cantidad > 0 },
        { "TipoMovimiento", () => !string.IsNullOrEmpty(movimientoInventario.TipoMovimiento) },
        { "FechaMovimiento", () => movimientoInventario.Fecha != default(DateTime) }
    };


            foreach (var validacion in validaciones)
            {
                if (!validacion.Value())
                {
                    errores.Add($"{validacion.Key} es obligatorio o tiene un valor inválido.");
                }
            }


            if (errores.Any())
            {
                ViewBag.Errores = errores;
                ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Nombre", movimientoInventario.ArticuloId);
                ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Nombre", movimientoInventario.UbicacionId);
                return View(movimientoInventario);
            }

            try
            {
                _context.Update(movimientoInventario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimientoInventarioExists(movimientoInventario.Id))
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

        // GET: MovimientosInventario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimientoInventario = await _context.MovimientosInventario
                .Include(m => m.Articulo)
                .Include(m => m.Ubicacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimientoInventario == null)
            {
                return NotFound();
            }

            return View(movimientoInventario);
        }

        // POST: MovimientosInventario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimientoInventario = await _context.MovimientosInventario.FindAsync(id);
            if (movimientoInventario != null)
            {
                _context.MovimientosInventario.Remove(movimientoInventario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimientoInventarioExists(int id)
        {
            return _context.MovimientosInventario.Any(e => e.Id == id);
        }
    }
}
