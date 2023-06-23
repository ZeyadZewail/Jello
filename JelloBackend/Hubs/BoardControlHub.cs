using JelloBackend.Data;
using JelloBackend.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace JelloBackend.Hubs;

[SignalRHub]
public class BoardControlHub : Hub
{
    private readonly DatabaseContext _context;

    public BoardControlHub(DatabaseContext dbContext)
    {
        _context = dbContext;
    }

    public async Task RenameBoard(string boardId, string newName)
    {
        var board = _context.Boards.FirstOrDefault(b => b.id == int.Parse(boardId));
        if (board != null)
        {
            board.name = newName;
            await _context.SaveChangesAsync();

            var command = new SignalCommand();
            command.commandName = "renameBoard";
            command.payload = board.name;

            await Clients.All.SendAsync(boardId, command);
        }
    }
}