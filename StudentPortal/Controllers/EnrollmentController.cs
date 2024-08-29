﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using System.Runtime.CompilerServices;

namespace StudentPortal.Controllers
{
	public class EnrollmentController : Controller
	{
		private readonly ApplicationDBContext DBContext;

		public EnrollmentController(ApplicationDBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        public IActionResult Entry()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Search(string idnumber)
		{
			if (string.IsNullOrWhiteSpace(idnumber))
			{
				return View();  // Optionally return a validation error
			}

			// Search for the student using the provided ID number
			var student = await DBContext.Student
				.FirstOrDefaultAsync(s => s.StudID.ToString() == idnumber);

			if (student == null)
			{
				// Optionally return an error or notification to the user
				ViewBag.Message = "Student not found.";
				return View();
			}

			// Pass the found student to the view
			return View(student);  // Adjust the view accordingly to display search results
		}
	}
}
