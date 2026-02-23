using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoshenJimenez.CSMWeb.Infrastructure.Models;
using GoshenJimenez.CSMWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GoshenJimenez.CSMWeb.Pages.Students;

public class Create : PageModel
{
    private SchoolDbContext _dbContext;
    
    [BindProperty]
    public string Fullname { get; set; }

    [BindProperty]
    public int Age { get; set; }
    public Create(SchoolDbContext dbContext){
        _dbContext = dbContext;
    }

    public void OnPost()
    {
        var student = new Student
        {
            FullName = Fullname,
            Age = Age
        };

        _dbContext.Students.Add(student);
        _dbContext.SaveChanges();

        Response.Redirect("/students");
    }

}
