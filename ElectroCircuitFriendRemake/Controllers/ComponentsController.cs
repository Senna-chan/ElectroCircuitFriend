using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectroCircuitFriendRemake.Data;
using ElectroCircuitFriendRemake.Models;
using ElectroCircuitFriendRemake.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace ElectroCircuitFriendRemake.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _hostingEnvironment;
        public ComponentsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
            _context = context;    
        }

        // GET: Components
        public async Task<IActionResult> Index()
        {
            return View(await _context.Components.ToListAsync());
        }

        // GET: Components/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .SingleOrDefaultAsync(m => m.Id == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // GET: Components/Create
        public IActionResult Create()
        {
            var viewModel = new CreateComponentViewModel();
            return View(viewModel);
        }

        // POST: Components/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateComponentViewModel model)
        {
            if (ModelState.IsValid)
            {

                var component = new Component
                {
                    Name = model.Name,
                    Description = model.Description,
                    ExtraDescription = model.ExtraDescription,
                    InStock = model.InStock,
                    Used = model.Used
                };
                var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
                if (model.DataSheet != string.Empty)
                {
                    if (model.DataSheet.StartsWith("http"))
                    {
                        using (var httpClient = new HttpClient())
                        {
                            using (var ms = new MemoryStream())
                            {
                                var downloadStream = await httpClient.GetStreamAsync(new Uri(model.DataSheet));
                                downloadStream.CopyTo(ms);
                                component.DataSheet = Convert.ToBase64String(ms.ToArray());
                            }
                        }
                    }
                    else if(model.DatasheetFile != null)
                    {
                        await model.DatasheetFile.CopyToAsync(System.IO.File.Create(Path.Combine(uploads, model.Name + "-datasheet")));
                    }
                }

                if (model.ComponentImage != string.Empty)
                {
                    if (model.ComponentImage.StartsWith("http"))
                    {
                        using (var httpClient = new HttpClient())
                        {
                            using (var ms = new MemoryStream())
                            {
                                var downloadStream = await httpClient.GetStreamAsync(new Uri(model.ComponentImage));
                                downloadStream.CopyTo(ms);
                                component.ComponentImage = Convert.ToBase64String(ms.ToArray());
                            }
                        }
                    }
                    else if(model.ComponentImageFile != null)
                    {
                        using (var fileStream = model.ComponentImageFile.OpenReadStream())
                        {
                            using (var ms = new MemoryStream())
                            {
                                fileStream.CopyTo(ms);
                                component.ComponentImage = Convert.ToBase64String(ms.ToArray());
                            }
                        }
                    }
                }

                if (model.ComponentPinoutImage != string.Empty)
                {
                    if (model.ComponentPinoutImage.StartsWith("http"))
                    {
                        using (var httpClient = new HttpClient())
                        {
                            using (var ms = new MemoryStream())
                            {
                                var downloadStream = await httpClient.GetStreamAsync(new Uri(model.ComponentPinoutImage));
                                downloadStream.CopyTo(ms);
                                component.ComponentPinoutImage = Convert.ToBase64String(ms.ToArray());
                            }
                        }
                    }
                    else if(model.ComponentPinoutImageFile != null)
                    {
                        using (var fileStream = model.ComponentPinoutImageFile.OpenReadStream())
                        {
                            using (var ms = new MemoryStream())
                            {
                                fileStream.CopyTo(ms);
                                component.ComponentPinoutImage = Convert.ToBase64String(ms.ToArray());
                            }
                        }
                    }
                }
                


                _context.Add(component);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Components/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components.SingleOrDefaultAsync(m => m.Id == id);
            if (component == null)
            {
                return NotFound();
            }
            return View(component);
        }

        // POST: Components/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ComponentCategory,Name,Description,ExtraDescription,ComponentImage,ComponentPinoutImage,DataSheet,InStock,Used")] Component component)
        {
            if (id != component.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(component);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentExists(component.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(component);
        }

        // GET: Components/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .SingleOrDefaultAsync(m => m.Id == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var component = await _context.Components.SingleOrDefaultAsync(m => m.Id == id);
            _context.Components.Remove(component);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ComponentExists(int id)
        {
            return _context.Components.Any(e => e.Id == id);
        }
    }
}
