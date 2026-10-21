
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FabrykaIL.Models;
using FabrykaIL.Data;

public class MaszynaController : Controller
{
    private readonly ApplicationDbContext _db;

    public MaszynaController(ApplicationDbContext context)
    {
        _db = context;
    }

    // GET: MASZYNAS
    public async Task<IActionResult> Index(string? search)    
    {
        if (string.IsNullOrEmpty(search))
            return View(await _db.Maszyny.OrderBy(m => m.Nazwa).ToListAsync()); 
        else
            return View(await _db.Maszyny.Where(m => m.Nazwa.Contains(search)).OrderBy(m => m.Nazwa).ToListAsync());
    }

    // GET: MASZYNAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var maszyna = await _db.Maszyny.FirstOrDefaultAsync(m => m.Id == id);
        if (maszyna == null)
        {
            return NotFound();
        }

        return View(maszyna);
    }

    // GET: MASZYNAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MASZYNAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nazwa,DataUruchomienia,HalaId,Hala,Operatorzy")] Maszyna maszyna)
    {
        if (ModelState.IsValid)
        {
            _db.Add(maszyna);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(maszyna);
    }

    // GET: MASZYNAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var maszyna = await _db.Maszyny.FindAsync(id);
        if (maszyna == null)
        {
            return NotFound();
        }
        return View(maszyna);
    }

    // POST: MASZYNAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nazwa,DataUruchomienia,HalaId,Hala,Operatorzy")] Maszyna maszyna)
    {
        if (id != maszyna.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _db.Update(maszyna);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaszynaExists(maszyna.Id))
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
        return View(maszyna);
    }

    // GET: MASZYNAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var maszyna = await _db.Maszyny
            .FirstOrDefaultAsync(m => m.Id == id);
        if (maszyna == null)
        {
            return NotFound();
        }

        return View(maszyna);
    }

    // POST: MASZYNAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var maszyna = await _db.Maszyny.FindAsync(id);
        if (maszyna != null)
        {
            _db.Maszyny.Remove(maszyna);
        }

        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MaszynaExists(int? id)
    {
        return _db.Maszyny.Any(e => e.Id == id);
    }
}
