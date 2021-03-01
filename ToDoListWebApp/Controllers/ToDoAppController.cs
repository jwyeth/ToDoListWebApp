using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using ToDoListWebApp.Models;

namespace ToDoListWebApp.Controllers
{
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

            List<Tasks> taskList = _toDoAppDB.Tasks.ToList();
            List<Tasks> userTaskList = taskList.Where(x => x.OwnerId == loggedInUserId).ToList();

            return View(userTaskList);
        }
    }
}
