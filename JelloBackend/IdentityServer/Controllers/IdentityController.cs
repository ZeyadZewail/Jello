using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JelloBackend.Controllers;

[Route("identity")]
[Authorize]
public class IdentityController : Controller
{
    [HttpPost("GetToken")]
    public async Task<ActionResult<AuthenticationResponseDto>> CreateBearerToken(AuthenticationRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest("Bad request");

        var user = await _userService.CheckCredentialsAsync(request.Username, request.Password);

        var token = await _jwtService.IssueTokenAsync(user);
        var response = new AuthenticationResponseDto
        {
            Expiration = token.Expiration,
            Token = token.Token,
            RefreshToken = token.RefreshToken
        };

        return Ok(response);
    }
}