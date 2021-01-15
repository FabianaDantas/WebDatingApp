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

namespace WebDatingApp.API.Controllers
{
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
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();

            var usersReturn = _mapper.Map<IEnumerable<UserForListDTO>>(users);

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