using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AddressBookProject.Data;
using AddressBookProject.Models;
using AddressBook.ViewModels;
using BookAddressProject.ViewModels;
using AutoMapper;
using System.Data.Entity;

namespace BookAddressProject.Controllers
{
    public class JobsController : BaseApiController
    {
        private readonly DBContext _context;
        private readonly IMapper? _mapper;

        public JobsController(DBContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobViewModel>>> GetJobs()
        {
            var data = await _context.Jobs.ToListAsync();
            var viewModel = _mapper!.Map<IEnumerable<Job>, IEnumerable<JobViewModel>>(data);
            return Ok(viewModel);
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobViewModel>> GetJob(int? id)
        {
            var data = await _context!.Jobs!.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            var viewModel = _mapper!.Map<Job, JobViewModel>(data);
            return viewModel;
        }

        // PUT: api/Jobs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int? id, Job job)
        {
            var data = await _context!.Jobs!.FindAsync(id);
            if (data != null)
            {
                _context.Jobs.Update(job);
                return Ok();
            }
            return NotFound();
        }

        // POST: api/Jobs
        [HttpPost]
        public async Task<ActionResult<JobViewModel>> PostJob(JobViewModel Model)
        {
             var job = new Job { Id = Model.Id , JobTitle = Model.JobTitle};
            _context!.Jobs!.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { id = job.Id }, job);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int? id)
        {
            var job = await _context!.Jobs!.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
