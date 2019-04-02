﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Lingva.DataAccessLayer.Entities;
using Lingva.BusinessLayer.Contracts;
using Lingva.WebAPI.Dto;
using Lingva.BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;

namespace Lingva.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]    
    [ApiController]
    public class GroupCollectionController : ControllerBase
    {
        private readonly IGroupService _groupsService;
        private readonly IMapper _mapper;

        public GroupCollectionController(IGroupService groupsCollectionService, IMapper mapper)
        {
            _groupsService = groupsCollectionService;
            _mapper = mapper;
        }

        // GET: api/groupcollection
        [HttpGet]
        public async Task<IActionResult> GetGroupsList()
        {
            var groups = await Task.Run(() => _groupsService.GetGroupsList());

            return Ok(_mapper.Map<IEnumerable<GroupViewDTO>>(groups));
        }

        // GET: api/groupcollection/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGroup([FromRoute] int id)
        {
            Group group = await Task.Run(() => _groupsService.GetGroup(id));

            if (group == null)
            {
                return NotFound(BaseStatusDto.CreateErrorDto());
            }

            GroupViewDTO response = _mapper.Map<GroupViewDTO>(group);
            response.CreateSuccess();
            return Ok(response);
        }

        // GET: api/groupcollection/title
        [HttpGet("{title}")]
        public async Task<IActionResult> GetGroupByTitle([FromRoute] string title)
        {

            Group group = await Task.Run(() => _groupsService.GetGroupByTitle(title));

            if (group == null)
            {
                return NotFound(BaseStatusDto.CreateErrorDto());
            }

            var groupToReturn = _mapper.Map<GroupViewDTO>(group);
            groupToReturn.CreateSuccess();

            return Ok(groupToReturn);
        }

        // POST: api/groupcollection
        [HttpPost]
        public async Task<IActionResult> PostGroup([FromBody] GroupCreatingDTO groupCreatingDTO)
        {

            Group group;

            try
            {
                group = await Task.Run(() => _mapper.Map<Group>(groupCreatingDTO));
                Film movie = new Film();

                await Task.Run(() =>
                {
                    _groupsService.AddGroup(group, UserService.GetLoggedInUserId(this));
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
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

            try
            {
                Group group = _mapper.Map<Group>(groupCreatingDTO);
                await Task.Run(() => _groupsService.UpdateGroup(id, group));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        // DELETE: api/groupcollection/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup([FromRoute] int id)
        {
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