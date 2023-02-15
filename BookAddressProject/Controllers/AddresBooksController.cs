
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AddressBookProject.Data;
using AddressBookProject.Models;
using BookAddressProject.ViewModels;
using AutoMapper;

namespace BookAddressProject.Controllers
{

    public class AddresBooksController : BaseApiController
    {
        private readonly DBContext? _context;
        private readonly IMapper? _mapper;

        public AddresBooksController(DBContext? Context, IMapper? mapper)
        {
            _context = Context;
            _mapper = mapper;
        }

        // GET: api/AddresBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressBookViewModel>>> GetAddressBooks()
        {
            var data = await _context!.AddressBooks!.ToListAsync();
            var viewModel = _mapper!.Map<IEnumerable<AddresBook>, IEnumerable<AddressBookViewModel>>(data);
            return Ok(viewModel);
        }

        // GET: api/AddresBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressBookViewModel>> GetAddresBook(int? id)
        {
            var addresBook = await _context!.AddressBooks!.FirstOrDefaultAsync(x => x.Id == id);
            if (addresBook == null)
            {
                return NotFound();
            }
            var viewModel = _mapper!.Map<AddresBook, AddressBookViewModel>(addresBook);
            return viewModel;
        }

        // PUT: api/AddresBooks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddresBook(int? id, AddresBook addresBook)
        {
            var data = await _context!.AddressBooks!.FindAsync(id);
            if (data != null)
            {
                _context!.AddressBooks!.Update(data);
                return Ok();
            }
            return NotFound();
        }

        // POST: api/AddresBooks
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<AddressBookViewModel>> PostAddresBook(AddressBookViewModel addresBook)
        {
            var Departement = await _context!.Departments!.FindAsync(addresBook.Id);
            var Job = await _context!.Jobs!.FindAsync(addresBook.Id);
            string fileName = UploadImage(addresBook.Image!)== null? string.Empty : UploadImage(addresBook.Image!);
            var model = new AddresBook
            {
              department = Departement,
              job = Job,
              Id = addresBook.Id,
              Age = addresBook.Age,
              Address = addresBook.Address,
              FullName = addresBook.FullName,
              BirthOfDate = addresBook.BirthOfDate,
              PhoneNumber = addresBook.PhoneNumber,
              ImageUrl = fileName,
            };
            _context!.AddressBooks!.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAddresBook", new { id = addresBook.Id }, addresBook);
        }

        // DELETE: api/AddresBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddresBook(int? id)
        {
            var data = await _context!.AddressBooks!.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            _context!.AddressBooks!.Remove(data);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //UploadImage :
         string UploadImage(IFormFile image)
        { 
            if(image != null)
            { 
             var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", image.FileName);
             using (var stream = new FileStream(path, FileMode.Create))    
             image.CopyToAsync(stream);
             return image.FileName;
            }
            return null;
        }

        //search between 2 dates
        public async Task<IActionResult> SearchByDate(DateTime? startDate, DateTime? endDate)
        {
            var data = _context!.AddressBooks!.Where(x => x.BirthOfDate >= startDate && x.BirthOfDate <= endDate).ToListAsync();
            return Ok(data);
        }
    }
}
