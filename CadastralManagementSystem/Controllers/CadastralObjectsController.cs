using Microsoft.AspNetCore.Mvc;
using CadastralManagement.Data;
using CadastralManagement.Models;
using Microsoft.AspNetCore.Authorization;

/*
 *  Контроллер для работы с кадастровыми объектами из базы данных
 */

namespace CadastralManagement.Controllers
{
    [Authorize]
    public class CadastralObjectsController(CadastralManagementContext database) : Controller
    {
        // GET: CadastralObjects
        public IActionResult Index(string filter = "")
        {
            var cadastralObjects = database.CadastralObjects.ToList().Where(cadastralObject => cadastralObject.Address.Contains(filter, StringComparison.CurrentCultureIgnoreCase));

            return View(cadastralObjects);
        }

        // GET: CadastralObjects/Details/5
        public IActionResult Details(int id)
        {
            var cadastralObject = database.CadastralObjects.Find(id);

            if (cadastralObject == null)
            {
                return NotFound();
            }

            return View(cadastralObject);
        }

        // GET: CadastralObjects/Create
        [Authorize(Roles = UsersInitializer.ADMIN_ROLE)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CadastralObjects/Create
        [Authorize(Roles = UsersInitializer.ADMIN_ROLE)]
        [HttpPost]
        public IActionResult Create(CadastralObject cadastralObject)
        {
            if (ModelState.IsValid)
            {
                database.Add(cadastralObject);
                database.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(cadastralObject);
        }

        // GET: CadastralObjects/Edit/5
        [Authorize(Roles = UsersInitializer.ADMIN_ROLE)]
        public IActionResult Edit(int id)
        {
            var cadastralObject = database.CadastralObjects.Find(id);

            if (cadastralObject == null)
            {
                return NotFound();
            }

            return View(cadastralObject);
        }

        // POST: CadastralObjects/Edit/5
        [Authorize(Roles = UsersInitializer.ADMIN_ROLE)]
        [HttpPost]
        public IActionResult Edit(CadastralObject cadastralObject)
        {
            if (ModelState.IsValid)
            {
                database.Update(cadastralObject);
                database.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(cadastralObject);
        }

        // GET: CadastralObjects/Delete/5
        [Authorize(Roles = UsersInitializer.ADMIN_ROLE)]
        public IActionResult Delete(int id)
        {
            var cadastralObject = database.CadastralObjects.Find(id);

            if (cadastralObject == null)
            {
                return NotFound();
            }

            return View(cadastralObject);
        }

        // POST: CadastralObjects/Delete/5
        [Authorize(Roles = UsersInitializer.ADMIN_ROLE)]
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var cadastralObject = database.CadastralObjects.Find(id);

            database.CadastralObjects.Remove(cadastralObject!);
            database.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}