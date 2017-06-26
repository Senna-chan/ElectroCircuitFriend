
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ElectroCircuitFriendRemake.Data;
using ElectroCircuitFriendRemake.Models;
using ElectroCircuitFriendRemake.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                var componentSaveName = Regex.Replace(model.Name, @"\s+", "");
                var component = new Component
                {
                    Name = model.Name,
                    Description = model.Description,
                    ExtraDescription = model.ExtraDescription,
                    InStock = model.InStock,
                    Used = model.Used,
                    NormalizedString = componentSaveName
                };
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                
                if (model.DataSheet != string.Empty)
                {
                    var path = Path.Combine(uploads, componentSaveName + "-datasheet.pdf");
                    //component.DataSheet = true;
                    if (model.DataSheet.StartsWith("http"))
                    {
                        using (var httpClient = new HttpClient())
                        {
                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                var downloadStream = await httpClient.GetStreamAsync(new Uri(model.DataSheet));
                                downloadStream.CopyTo(fileStream);
                            }
                        }
                    }
                    else if (model.DatasheetFile != null)
                    {
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await model.DatasheetFile.CopyToAsync(fileStream);
                        }
                    }
                }

                if (model.ComponentImage != null)
                {
                    string path;
                    //component.ComponentImage = true;
                    if (model.ComponentImage.StartsWith("http"))
                    {
                        path = Path.Combine(uploads, componentSaveName + Path.GetExtension(await GetFileNameFromUrl(model.ComponentImage)));
                        using (var httpClient = new HttpClient())
                        {
                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                var downloadStream = await httpClient.GetStreamAsync(new Uri(model.ComponentImage));
                                downloadStream.CopyTo(fileStream);
                            }
                        }
                    }
                    else if (model.ComponentImageFile != null)
                    {
                        path = Path.Combine(uploads, componentSaveName + Path.GetExtension(model.ComponentImage));
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await model.ComponentImageFile.CopyToAsync(fileStream);
                        }
                    }
                }

                if (model.ComponentPinoutImage != string.Empty)
                {
                    string path;
                    //component.ComponentPinoutImage = true;
                    if (model.ComponentPinoutImage.StartsWith("http"))
                    {
                        path = Path.Combine(uploads, componentSaveName + Path.GetExtension(await GetFileNameFromUrl(model.ComponentPinoutImage)));
                        using (var httpClient = new HttpClient())
                        {
                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                var downloadStream = await httpClient.GetStreamAsync(new Uri(model.ComponentPinoutImage));
                                downloadStream.CopyTo(fileStream);
                            }
                        }
                    }
                    else if (model.ComponentPinoutImageFile != null)
                    {
                        path = Path.Combine(uploads, componentSaveName + Path.GetExtension(model.ComponentPinoutImage));
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await model.ComponentPinoutImageFile.CopyToAsync(fileStream);
                        }
                    }
                }
                


                _context.Add(component);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        private async Task<string> GetFileNameFromUrl(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            string fileName = "";
            try
            {
                HttpWebResponse res = (HttpWebResponse)await request.GetResponseAsync();
                using (Stream rstream = res.GetResponseStream())
                {
                    fileName = res.Headers["Content-Disposition"] != null ?
                        res.Headers["Content-Disposition"].Replace("attachment; filename=", "").Replace("\"", "") :
                        res.Headers["Location"] != null ? Path.GetFileName(res.Headers["Location"]) :
                            Path.GetFileName(url).Contains('?') || Path.GetFileName(url).Contains('=') ?
                                Path.GetFileName(res.ResponseUri.ToString()) : "";
                }
                res.Dispose();
            }
            catch { }
            return fileName;
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
