using System;
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
using Microsoft.AspNetCore.Http;

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
        /// <summary>
        /// Returns group list
        /// </summary>
        /// <remarks>
        /// 
        ///     GET: api/groupcollection
        ///      
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Returns OK if dictionary record updated</response>
        /// <response code="400">If the exception handled</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <summary>
        /// Return info about chosen group
        /// </summary>
        /// <remarks>
        /// 
        ///     GET: api/groupcollection/{title}
        /// 
        /// </remarks>
        /// <param name="title"></param>
        /// <returns>Group info</returns>
        /// <response code="200">Returns OK and group Dto</response>
        /// <response code="400">If model state is not valid</response> 
        /// <response code="404">If the group is null</response>
        [HttpGet("{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Creates new group
        /// </summary>
        /// <remarks>
        /// 
        ///     POST: groupcollection
        ///     
        /// Sample request
        ///         {
        ///             "Title" : "title"
        ///             "Desciption" : "descrioption"
        ///             "FilmId" : "filmId"
        ///             "Picture" : "picture"
        ///         }
        /// </remarks>
        /// <param name="groupCreatingDTO"></param>
        /// <returns>Status and group info if created</returns>
        /// <response code="200">Returns OK if created and group Dto</response>
        /// <response code="400">If exception is handled</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <summary>
        /// Updates gruop info by chosen id
        /// </summary>
        /// <remarks>
        ///     PUT: groupcollection/{id}
        ///     
        /// Sample request
        ///         {
        ///             "Title" : "title"
        ///             "Desciption" : "descrioption"
        ///             "FilmId" : "filmId"
        ///             "Picture" : "picture"
        ///         }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="groupCreatingDTO"></param>
        /// <returns>Status of operation</returns>
        /// <response code="200">Returns OK if group updated</response>
        /// <response code="400">If the exception handled</response>  
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <summary>
        /// Deletes chosen group 
        /// </summary>
        /// <remarks>
        ///     
        ///     DELETE: groupcollection/{id}
        ///         
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Status</returns>
        /// <response code="200">Returns OK if deleted</response>
        /// <response code="400">If exception is hendled</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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