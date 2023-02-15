using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AddressBookProject.Data;
using AddressBookProject.Models;
using AutoMapper;
using AddressBook.ViewModels;
using BookAddressProject.ViewModels;

namespace BookAddressProject.Controllers
{
    public class DepartmentsController : BaseApiController
    {
        private readonly DBContext _context;
        private readonly IMapper? _mapper;

        public DepartmentsController(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentViewModel>>> GetDepartments()
        {
            var data = await _context!.Departments!.ToListAsync();
            var viewModel = _mapper!.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(data);
            return Ok(viewModel);
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentViewModel>> GetDepartment(int? id)
        {
            var data = await _context!.Departments!.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            var viewModel = _mapper!.Map<Department, DepartmentViewModel>(data);
            return viewModel;
        }

        // PUT: api/Departments/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int? id, Department department)
        {
            var data = await _context!.Jobs!.FindAsync(id);
            if (data != null)
            {
                _context!.Departments!.Update(department);
                return Ok();
            }
            return NotFound();
        }

        // POST: api/Departments
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(DepartmentViewModel department)
        {
            var data = new Department
            {
                Id = department.Id,
                DepName = department.DepName,
            };
            _context!.Departments!.Add(data);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDepartment", new { id = department.Id }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int? id)
        {
            var data = await _context!.Departments!.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            _context!.Departments!.Remove(data);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
