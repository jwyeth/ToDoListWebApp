using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using ToDoListWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ToDoListWebApp.Controllers
{
    [Authorize]
    public class ToDoAppController : Controller
    {
        private readonly ToDoAppContext _toDoAppDB;

        public ToDoAppController(ToDoAppContext toDoAppContext)
        {
            _toDoAppDB = toDoAppContext;

        }
        public IActionResult Index()
        {
            string loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Tasks> userTaskList = _toDoAppDB.Tasks.Where(x => x.OwnerId == loggedInUserId).ToList();

            return View(userTaskList);
        }

        public IActionResult AddTask()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTask(Tasks task)
        {
            string loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            task.OwnerId = loggedInUserId;
            _toDoAppDB.Tasks.Add(task);
            _toDoAppDB.SaveChanges();
            return RedirectToAction("TaskAdded", task);
        }

        public IActionResult DeleteTask(int id)
        {
            Tasks task = _toDoAppDB.Tasks.Find(id);
            _toDoAppDB.Tasks.Remove(task);
            _toDoAppDB.SaveChanges();
            return RedirectToAction("DeleteTask", task);
        }

        public IActionResult TaskAdded(Tasks task)
        {
            return View(task);
        }

        public IActionResult ViewTasks()
        {
            string loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Tasks> userTaskList = _toDoAppDB.Tasks.Where(x => x.OwnerId == loggedInUserId).ToList();

            return View(userTaskList);
        }

        public IActionResult ViewCompleted()
        {
            string loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Tasks> completedTaskList = _toDoAppDB.Tasks.Where(x => x.Completed == true && x.OwnerId == loggedInUserId).ToList();
            return View(completedTaskList);
        }

        public IActionResult ViewIncomplete()
        {
            string loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Tasks> incompleteTaskList = _toDoAppDB.Tasks.Where(x => x.Completed == false && x.OwnerId == loggedInUserId).ToList();
            return View(incompleteTaskList);
        }

        public IActionResult MarkComplete(int id)
        {
            Tasks task = _toDoAppDB.Tasks.Find(id);
            task.Completed = true;
            _toDoAppDB.Tasks.Update(task);
            _toDoAppDB.SaveChanges();
            return RedirectToAction("Success", task);
        }

        public IActionResult MarkIncomplete(int id)
        {
            Tasks task = _toDoAppDB.Tasks.Find(id);
            task.Completed = false;
            _toDoAppDB.Tasks.Update(task);
            _toDoAppDB.SaveChanges();
            return RedirectToAction("Success", task);
        }

        public IActionResult Success(Tasks task)
        {
            return View(task);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
