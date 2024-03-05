using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1Dotnet.Models;

namespace WebApplication1Dotnet.Controllers
{
    public class StudentsController : Controller
    {
        private AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {e.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong: invalid model");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var exist = await _context.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
            return View(exist);
        }

        [HttpPost]
        public async Task <IActionResult> Edit(Student model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = await _context.Students.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if(exist != null)
                    {
                        exist.Name = model.Name;
                        exist.Email = model.Email;
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {e.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong: invalid model");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _context.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
            return View(exist);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = await _context.Students.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if(exist != null)
                    {
                        _context.Remove(exist);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {e.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong: invalid model");
            return View(model);
        }
    }
}