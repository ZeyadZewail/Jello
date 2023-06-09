using JelloBackend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JelloBackend.Controllers;



[Route("/test")]
[ApiController]
public class TestController : Controller
{
    DatabaseContext _context = new DatabaseContext();

    [HttpGet]
    public ActionResult GetBoards()
    {
        return Ok(_context.Boards.Include(b =>b.Columns).ThenInclude(c=>c.Elements));
    }
}