using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebDatingApp.API.Data;
using WebDatingApp.API.Domain.DTOs;
using System.Security.Claims;
using WebDatingApp.API.Helpers;

namespace WebDatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UsersParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _repo.GetUser(currentUserId);

            userParams.UserId = currentUserId;

            if(string.IsNullOrEmpty(userParams.Gender)) {
                userParams.Gender = userFromRepo.Gender == "feminino" ? "masculino" : "feminino";
            }

            var users = await _repo.GetUsers(userParams);

            var usersReturn = _mapper.Map<IEnumerable<UserForListDTO>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(usersReturn);
        }
        
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userReturn = _mapper.Map<UserForDetailedDTO>(user);

            return Ok(userReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDTO userForUpdate)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var userRepo = await _repo.GetUser(id);

            var userReturn = _mapper.Map(userForUpdate, userRepo);

            if(await _repo.SaveAll()) {
                return NoContent();
            }

            throw new Exception($"Alterando usuário id {id} falhou ao salvar");
        }
    }
}