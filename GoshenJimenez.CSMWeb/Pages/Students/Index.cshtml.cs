using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoshenJimenez.CSMWeb.Infrastructure.Models;
using GoshenJimenez.CSMWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GoshenJimenez.CSMWeb.Pages.Students;

public class Index : PageModel
{
    private SchoolDbContext _dbContext;
    public List<GoshenJimenez.CSMWeb.Infrastructure.Models.Student> Students { get; set; }
    public string? Keyword { get; set; }
    public Index(SchoolDbContext dbContext){
        _dbContext = dbContext;
    }

    public async Task OnGet(string? sortBy="fullname", string? sortOrder="asc", string? keyword="")
    {
        keyword = keyword?.ToLower() ?? "";

        IQueryable<Student> query = _dbContext.Students;

        if(!string.IsNullOrEmpty(keyword)){
            query = query.Where(a => a.FullName.ToLower().Contains(keyword ?? ""));
            Keyword = keyword;
        }

        sortBy = sortBy?.ToLower() ?? "fullname";
        sortOrder = sortOrder?.ToLower() ?? "asc";
        
        if(sortBy == "fullname" && sortOrder == "asc")
        {
            query = query.OrderBy(s => s.FullName);
        }
        else if(sortBy == "fullname" && sortOrder == "desc")
        {
            query = query.OrderByDescending(s => s.FullName);            
        }
        else if(sortBy == "age" && sortOrder == "asc")
        {
            query = query.OrderBy(s => s.Age);
        }
        else if(sortBy == "age" && sortOrder == "desc")
        {
            query = query.OrderByDescending(s => s.Age);            
        }

        Students = await query.ToListAsync();
        
    }
}

