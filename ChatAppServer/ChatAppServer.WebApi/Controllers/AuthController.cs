using ChatAppServer.WebApi.Context;
using ChatAppServer.WebApi.Dtos;
using ChatAppServer.WebApi.Models;
using GenericFileService.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatAppServer.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterDto request,CancellationToken cancellationToken)
        {
            bool isNameExists = await _context.Users.AnyAsync(p => p.Name == request.Name, cancellationToken);
            if (isNameExists) 
            {
                return BadRequest(new { Message = "Bu kullanıcı adı daha önce kullanılmıştır" });
            }
            string avatar = FileService.FileSaveToServer(request.File, "wwwroot/avatar/");
            User user = new()
            {
                Name = request.Name,
                Avatar = avatar
            };
            await _context.Users.AddAsync(user,cancellationToken);
            await _context.SaveChangesAsync();  
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> Login(string name, CancellationToken cancellationToken)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(x => x.Name == name,cancellationToken);
            if (user is null)
            {
                return BadRequest(new { Message = "Kullanıcı bulunamadı" });
;           }
            user.Status = "online";
            await _context.SaveChangesAsync(cancellationToken);
            return Ok(user);
        }
    }
}
