
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ElectroCircuitFriendRemake.Data;
using ElectroCircuitFriendRemake.Helpers;
using ElectroCircuitFriendRemake.Models;
using ElectroCircuitFriendRemake.ViewModels;
using ImageSharp;
using ImageSharp.Processing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SixLabors.Primitives;

namespace ElectroCircuitFriendRemake.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private IHostingEnvironment _hostingEnvironment;
        public ComponentsController(ApplicationDbContext dbContext, IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
            _dbContext = dbContext;    
        }

        // GET: Components
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Components.ToListAsync());
        }

        // GET: Components/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _dbContext.Components
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

        [HttpPost]
        public async Task<string> ChangeItem([FromBody]ChangeItemAmountViewModel model)
        {
            var component = await _dbContext.Components.FirstOrDefaultAsync(c => c.Id == int.Parse(model.ItemId));
            if (model.Itemtype == "instock")
            {
                if(model.ChangeAction == "add")         component.InStock++;
                if (model.ChangeAction == "subtract")   component.InStock--;
                _dbContext.Update(component);
                await _dbContext.SaveChangesAsync();
                return component.InStock.ToString();
            }
            if (model.Itemtype == "used")
            {
                if (model.ChangeAction == "add") component.Used++;
                if (model.ChangeAction == "subtract") component.Used--;
                _dbContext.Update(component);
                await _dbContext.SaveChangesAsync();
                return component.Used.ToString();
            }
            return "";
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
                    component.DataSheet = componentSaveName + "-datasheet.pdf";
                    if (model.DataSheet.StartsWith("http"))
                    {
                        if (model.DataSheet.Contains("www.google."))
                        {
                            model.DataSheet = model.DataSheet.GetQueryParam("url");
                        }
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
                    string extension;
                    string path;
                    if (model.ComponentImage.StartsWith("http"))
                    {
                        extension = Path.GetExtension(await GetFileNameFromUrl(model.ComponentImage));
                        component.ComponentImage = componentSaveName + extension;
                        path = Path.Combine(uploads, component.ComponentImage);
                        using (var httpClient = new HttpClient())
                        {
                            if (model.ComponentPinoutImage.Contains("www.google."))
                            {
                                model.ComponentPinoutImage = model.ComponentPinoutImage.GetQueryParam("url");
                            }
                            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
                            {
                                var downloadStream = await httpClient.GetStreamAsync(new Uri(model.ComponentImage));
                                downloadStream.CopyTo(fileStream);
                                using (var thumbnailFileStream = new FileStream(Path.Combine(uploads,componentSaveName + "-254x254" + extension), FileMode.Create))
                                {
                                    Image<Rgba32> image = Image.Load<Rgba32>(fileStream);
                                    image.Resize(new ResizeOptions{Mode = ResizeMode.Max, Size = new Size(254, 254) }).Save(thumbnailFileStream);
                                    image.Dispose();
                                }
                            }
                        }
                    }
                    else if (model.ComponentImageFile != null)
                    {
                        extension = Path.GetExtension(model.ComponentImage);
                        component.ComponentImage = componentSaveName + extension;
                        path = Path.Combine(uploads, component.ComponentImage);
                        using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
                        {
                            await model.ComponentImageFile.CopyToAsync(fileStream);
                            using (var thumbnailFileStream = new FileStream(Path.Combine(uploads, componentSaveName + "-254x254" + extension), FileMode.Create))
                            {
                                Image<Rgba32> image = Image.Load<Rgba32>(fileStream);
                                image.Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size(254, 254) }).Save(thumbnailFileStream);
                                image.Dispose();
                            }
                        }
                    }
                }

                if (model.ComponentPinoutImage != string.Empty)
                {
                    string path;
                    string extension;
                    if (model.ComponentPinoutImage.StartsWith("http"))
                    {
                        if (model.ComponentPinoutImage.Contains("www.google."))
                        {
                            model.ComponentPinoutImage = model.ComponentPinoutImage.GetQueryParam("imgurl");
                        }
                        extension = Path.GetExtension(await GetFileNameFromUrl(model.ComponentPinoutImage));
                        component.ComponentPinoutImage = componentSaveName + extension;
                        path = Path.Combine(uploads, component.ComponentPinoutImage);
                        using (var httpClient = new HttpClient())
                        {
                            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
                            {
                                var downloadStream = await httpClient.GetStreamAsync(new Uri(model.ComponentPinoutImage));
                                await downloadStream.CopyToAsync(fileStream);
                                using (var thumbnailFileStream = new FileStream(Path.Combine(uploads, componentSaveName + "-254x254" + extension), FileMode.Create))
                                {
                                    Image<Rgba32> image = Image.Load<Rgba32>(fileStream);
                                    image.Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size(254, 254) }).Save(thumbnailFileStream);
                                    image.Dispose();
                                }
                            }
                        }
                    }
                    else if (model.ComponentPinoutImageFile != null)
                    {
                        extension = Path.GetExtension(model.ComponentPinoutImage);
                        component.ComponentPinoutImage = componentSaveName + extension;
                        path = Path.Combine(uploads, component.ComponentPinoutImage);
                        using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
                        {
                            await model.ComponentPinoutImageFile.CopyToAsync(fileStream);
                            using (var thumbnailFileStream = new FileStream(Path.Combine(uploads, componentSaveName + "-254x254" + extension), FileMode.Create))
                            {
                                Image<Rgba32> image = Image.Load<Rgba32>(fileStream);
                                image.Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size(254, 254) }).Save(thumbnailFileStream);
                                image.Dispose();
                            }
                        }
                    }
                }
                


                _dbContext.Add(component);
                await _dbContext.SaveChangesAsync();
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

            var component = await _dbContext.Components.SingleOrDefaultAsync(m => m.Id == id);
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
                    _dbContext.Update(component);
                    await _dbContext.SaveChangesAsync();
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

            var component = await _dbContext.Components
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
            var component = await _dbContext.Components.SingleOrDefaultAsync(m => m.Id == id);
            _dbContext.Components.Remove(component);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ComponentExists(int id)
        {
            return _dbContext.Components.Any(e => e.Id == id);
        }
    }
}
