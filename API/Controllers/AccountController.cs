using API.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController(UserManager<AppUser> userManager,
        IMapper mapper,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService)
    : BaseApiController
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDTO>> GetCurrentUser()
    {
        var user = await userManager.FindByEmailFromClaimsPrincipalAsync(User);
        return new UserDTO
        {
            Email = user.Email,
            Token = tokenService.CreateToken(user),
            DisplayName = user.DisplayName
        };
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
        return await userManager.FindByEmailAsync(email) != null;
    }

    [Authorize]
    [HttpGet("address")]
    public async Task<ActionResult<AddressDTO>> GetUserAddress()
    {
        var user = await userManager.FindUserByClaimsPrincipleWithAddressAsync(User);
        return mapper.Map<Address, AddressDTO>(user.Address);
    }

    [Authorize]
    [HttpPut("address")]
    public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO address)
    {
        var user = await userManager.FindUserByClaimsPrincipleWithAddressAsync(User);
        user.Address = mapper.Map<AddressDTO, Address>(address);
        var result = await userManager.UpdateAsync(user);
        if (result.Succeeded) return Ok(mapper.Map<Address, AddressDTO>(user.Address));
        return BadRequest("Problem updating the user");
    }


    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
        return new UserDTO
        {
            Email = user.Email,
            Token = tokenService.CreateToken(user),
            DisplayName = user.DisplayName
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
    {
        if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            return new BadRequestObjectResult(new ApiValidationErrorResponse
            {
                Errors = new[] { "Email address is in use" }
            });

        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Email
        };
        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) return BadRequest(new ApiResponse(400));
        return new UserDTO
        {
            DisplayName = user.DisplayName,
            Token = tokenService.CreateToken(user),
            Email = user.Email
        };
    }
}