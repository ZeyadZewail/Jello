using System.Diagnostics;
using JelloBackend.Data;
using JelloBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace JelloBackend.Hubs;

[SignalRHub]
public class BoardControlHub : Hub
{
    
    public async Task RenameBoard(string boardId,string newName)
    {
        Debug.Print("xdd");
        DatabaseContext context = new DatabaseContext();
        Board? board = context.Boards.FirstOrDefault(b => b.id == int.Parse(boardId));
        if (board != null)
        {

            board.name = newName;
            context.Update(board);
            
            SignalCommand command = new SignalCommand();
            command.commandName = "rename";
            command.payload = newName;
        
            await Clients.All.SendAsync(boardId,command);
            
        }
        

    }
}