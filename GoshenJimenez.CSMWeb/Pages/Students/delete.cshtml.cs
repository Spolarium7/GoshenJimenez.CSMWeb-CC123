using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoshenJimenez.CSMWeb.Infrastructure.Models;
using GoshenJimenez.CSMWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GoshenJimenez.CSMWeb.Pages.Students;

public class Delete : PageModel
{
    private SchoolDbContext _dbContext;
    
    [BindProperty]
    public int Id { get; set; }

    [BindProperty]
    public string Fullname { get; set; }

    [BindProperty]
    public int Age { get; set; }
    public Delete(SchoolDbContext dbContext){
        _dbContext = dbContext;
    }
    public async Task OnGet(int? id = null)
    {
        if (id.HasValue)
        {
            var student = await _dbContext.Students.FindAsync(id.Value);
            if (student != null)
            {
                Id = student.Id;
                Fullname = student.FullName;
                Age = student.Age;
            }
        }
    }
    public async Task OnPost()
    {
        if (Id > 0)
        {
            var student = await _dbContext.Students.FindAsync(Id);
            if (student != null)
            {
                student.FullName = Fullname;
                student.Age = Age;
            }
     
            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();
        }
        Response.Redirect("/students");
        
    }

}
