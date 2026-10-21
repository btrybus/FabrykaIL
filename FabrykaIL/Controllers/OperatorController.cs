
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FabrykaIL.Models;
using FabrykaIL.Data;

public class OperatorController : Controller
{
    private readonly ApplicationDbContext _context;

    public OperatorController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: OPERATORS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Operatorzy.ToListAsync());
    }

    // GET: OPERATORS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var o = await _context.Operatorzy
            .FirstOrDefaultAsync(m => m.Id == id);
        if (o == null)
        {
            return NotFound();
        }

        return View(o);
    }

    // GET: OPERATORS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: OPERATORS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nazwisko,Imie,Placa,Maszyny")] Operator o)
    {
        if (ModelState.IsValid)
        {
            _context.Add(o);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(o);
    }

    // GET: OPERATORS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var o = await _context.Operatorzy.FindAsync(id);
        if (o == null)
        {
            return NotFound();
        }
        return View(o);
    }

    // POST: OPERATORS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nazwisko,Imie,Placa,Maszyny")] Operator o)
    {
        if (id != o.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(o);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperatorExists(o.Id))
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
        return View(o);
    }

    // GET: OPERATORS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var o = await _context.Operatorzy
            .FirstOrDefaultAsync(m => m.Id == id);
        if (o == null)
        {
            return NotFound();
        }

        return View(o);
    }

    // POST: OPERATORS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var o = await _context.Operatorzy.FindAsync(id);
        if (o != null)
        {
            _context.Operatorzy.Remove(o);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool OperatorExists(int? id)
    {
        return _context.Operatorzy.Any(e => e.Id == id);
    }
}
