using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ControleDeAutenticaçao.Models;
using ControleDeAutenticaçao.Services;

namespace ControleDeAutenticaçao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMembersService _membersService;

        public UsersController(IMembersService membersService)
        {
            _membersService = membersService;
        }

        [HttpGet]
        public IActionResult GetAllMembers()
        {
            IEnumerable<Member> members = _membersService.GetAllMembers();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public IActionResult GetMemberById(int id)
        {
            Member member = _membersService.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        [HttpPost]
        public IActionResult CreateMember([FromBody] Member member)
        {
            _membersService.CreateMember(member);
            return CreatedAtAction(nameof(GetMemberById), new { id = member.Id }, member);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMember(int id, [FromBody] Member member)
        {
            if (id != member.Id)
            {
                return BadRequest();
            }
            _membersService.UpdateMember(member);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            _membersService.DeleteMember(id);
            return NoContent();
        }
    }
}
