using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreeApp.ApiService.Infrastructure;

namespace TreeApp.ApiService.Controllers;

[ApiController]
[Route("Test")]
public class TestController : Controller
{
    private TreeAppContext _context;
    public TestController(TreeAppContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("Test")]
    public async Task<string> Test()
    {
        var result = await _context.TreeNodes.FirstOrDefaultAsync();
        return result.ToString();
    }
}