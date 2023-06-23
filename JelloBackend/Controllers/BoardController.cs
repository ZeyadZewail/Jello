using JelloBackend.Data;
using JelloBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JelloBackend.Controllers;

[Route("/api")]
[ApiController]
public class BoardController : Controller
{
    private readonly DatabaseContext _context = new();

    [Route("/GetBoard")]
    [HttpGet]
    public ActionResult GetBoard(string id)
    {
        try
        {
            return Ok(_context.Boards
                .Include(b => b.columns)
                .ThenInclude(c => c.elements)
                .First(b => b.id == int.Parse(id)));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound("Board not found");
        }
    }

    [Route("/CreateBoard")]
    [HttpPost]
    public ActionResult CreateBoard(Board board)
    {
        try
        {
            return Ok(board.id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound("Something Unexpected Happened");
        }
    }
}