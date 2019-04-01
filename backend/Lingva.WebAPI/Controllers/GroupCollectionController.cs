using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Lingva.DataAccessLayer.Entities;
using Lingva.BusinessLayer.Contracts;
using Lingva.WebAPI.Dto;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupCollectionController : ControllerBase
    {
        private readonly IGroupService _groupsService;
        private readonly IFilmService _movieService;
        private readonly IMapper _mapper;

        public GroupCollectionController(IGroupService groupsCollectionService,
               IFilmService movieService, IMapper mapper)
        {
            _groupsService = groupsCollectionService;

            _mapper = mapper;
        }

        // GET: api/groupcollection
        [HttpGet]
        public async Task<IActionResult> GetGroupsList()
        {
            var groups = _groupsService.GetGroupsList();

            return Ok(_mapper.Map<IEnumerable<GroupViewDTO>>(groups));
        }

        // GET: api/groupcollection/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Group group = _groupsService.GetGroup(id);

            if (group == null)
            {
                return NotFound();
            }

            GroupViewDTO response = _mapper.Map<GroupViewDTO>(group);
            //response.StatusCode = 200;
            //response.Message = "ok";

            return Ok(response);
        }

        // GET: api/groupcollection/title
        [HttpGet("{title}")]
        public async Task<IActionResult> GetGroupByTitle([FromRoute] string title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Group group = _groupsService.GetGroupByTitle(title);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GroupViewDTO>(group));
        }

        // POST: api/groupcollection
        [HttpPost]
        public async Task<IActionResult> PostGroup([FromBody] GroupCreatingDTO groupCreatingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Group group;

            try
            {
                group = _mapper.Map<Group>(groupCreatingDTO);
                Film movie = new Film();

                await Task.Run(() =>
                {
                    _groupsService.AddGroup(group);
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            Group newGroup = _groupsService.GetGroup(group.Id);

            return Ok(_mapper.Map<GroupViewDTO>(newGroup));
        }

        // PUT: api/groupcollection/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup([FromRoute] int id, [FromBody] GroupCreatingDTO groupCreatingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Group group = _mapper.Map<Group>(groupCreatingDTO);
                await Task.Run(() => _groupsService.UpdateGroup(id, group));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE: api/groupcollection/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await Task.Run(() => _groupsService.DeleteGroup(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}