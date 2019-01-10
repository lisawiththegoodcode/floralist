using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using Shared.Web.MvcExtensions;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Controllers
{
    [Authorize]
    public class ImagesController : Controller
    {
        private readonly IRepository _repository;

        public ImagesController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult AddTags(int imageId)
        {
            var vm = new TagSearch
            {
                ImageId = imageId,
                ImageTags = _repository.GetImageById(imageId).ImageTags,
                Tags = _repository.Tags.ToList(),
                Types = _repository.Tags.Select(x => x.Type).Distinct().ToList()
            };
            // pass the view model to the view
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTags(int imageId, int tagId)
        {
            await _repository.CreateImageTagsAsync(imageId, tagId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveTag(int imageId, int tagId)
        {
            await _repository.DeleteImageTagAsync(imageId, tagId);
            return RedirectToAction(nameof(Index));
        }
        // GET: Images
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetImagesForDesignerAsync(User.GetUserId()));
        }

        public IActionResult AddNewTag(int imageid)
        {
            var vm = new AddNewTag
            {
                ImageId = imageid,
                Tag = new Tag()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewTag(int imageId, Tag tag)
        {
            if (_repository.Tags.Any(x => x.Type == tag.Type && x.Name == tag.Name))
            {
                var vm = new AddNewTag
                {
                    ImageId = imageId,
                    Tag = new Tag()
                };

                ModelState.AddModelError("", "This tag already exists");
                return View(vm);
            }
            await _repository.AddTagAsync(tag);
            await _repository.CreateImageTagsAsync(imageId, tag.Id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind()]Image image, IFormFile files)
        {
            if (files == null || files.Length == 0)
                return Content("file not selected");

            if (ModelState.IsValid)
            {
                using (var stream = new MemoryStream())
                {
                    await files.CopyToAsync(stream);
                    image.FileImage = stream.ToArray();
                }

                image.FileName = Path.GetFileName(files.FileName);
                image.DesignerId = _repository.GetDesignerIdForUserId(User.GetUserId());

                await _repository.AddImageAsync(image);
                return RedirectToAction(nameof(Index));
            }
            return View(image);
        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _repository.Images
                .Where(i=>i.DesignerId == _repository.GetDesignerIdForUserId(User.GetUserId()))
                .FirstOrDefaultAsync(m => m.Id == id);

            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteImageAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
            return _repository.Images.Any(e => e.Id == id);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Raw(int id)
        {
            var image = _repository.Images.FirstOrDefault(f => f.Id == id);
            var extension = new FileInfo(image.FileName).Extension.Replace(".", "");
            return File(image?.FileImage, $"image/{extension}");
        }
    }
}
