using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServerApp_Week3.Data;
using WebServerApp_Week3.Models;
using WebServerApp_Week3.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace WebServerApp_Week3.Controllers
{
    public class DepartmentContriller : ControllerBase
    {
        private readonly BusinessContext context;

        public DepartmentContriller(BusinessContext Context, IValidator Validator)
        {
            context = Context;
            validator = Validator;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResultStatusCodeAttribute<IEnumerable<Department>>> GetDepartment()
        {
            if (context.Departments == null)
            {
                return NotFound();
            }

            if (!validator.IsValid("test"))
            {
                return BadRequest();
            }
            return await context.Departments.ToListAsync();
        }
    }
}
