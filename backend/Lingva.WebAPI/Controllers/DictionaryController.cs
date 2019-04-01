using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using AutoMapper;
using Lingva.DataAccessLayer.Entities;
using Lingva.BusinessLayer.Contracts;
using Lingva.WebAPI.Dto;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IDictionaryService _dictionaryService;
        private readonly IMapper _mapper;

        public DictionaryController(IDictionaryService dictionaryService, IMapper mapper)
        {
            _dictionaryService = dictionaryService;
            _mapper = mapper;
        }

        // GET: api/Dictionary

        /// <summary>
        /// Returns Dictionary record.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /Dictionary
        ///     { }
        ///
        /// </remarks>
        /// <response code="200">Returns the dictionary record</response>
        /// <response code="404">If the item is null</response> 

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDictionary()
        {
            var dictionaryRecords = _dictionaryService.GetDictionary();

            return Ok(_mapper.Map<IEnumerable<DictionaryRecordViewDTO>>(dictionaryRecords));
        }

        // GET: api/Dictionary/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDictionaryRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DictionaryRecord dictionaryRecord = _dictionaryService.GetDictionaryRecord(id);

            if (dictionaryRecord == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DictionaryRecordViewDTO>(dictionaryRecord));
        }

        // POST: api/Dictionary
        /// <summary>
        /// Deletes user from group.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /Dictionary
        ///     {
        ///        "Id" : 1
        ///        "UserId" : 12
        ///        "WordName" : "string"
        ///        "Translation" : "translation"
        ///        "LanguageName" : "languageName"
        ///        "Context" : "context"
        ///        "Picture" : "picture"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the dictionary record</response>
        /// <response code="404">If the item is null</response> 
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PostDictionaryRecord([FromBody] DictionaryRecordCreatingDTO dictionaryRecordCreatingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                DictionaryRecord dictionaryRecord = _mapper.Map<DictionaryRecord>(dictionaryRecordCreatingDTO);
                await Task.Run(() => _dictionaryService.AddDictionaryRecord(dictionaryRecord));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // PUT: api/Dictionary/5
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutDictionaryRecord([FromRoute] int id, [FromBody] DictionaryRecordCreatingDTO dictionaryRecordCreatingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                DictionaryRecord dictionaryRecord = _mapper.Map<DictionaryRecord>(dictionaryRecordCreatingDTO);
                await Task.Run(() => _dictionaryService.UpdateDictionaryRecord(id, dictionaryRecord));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE: api/Dictionary/5
        /// <summary>
        /// Deletes record from dictionary.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Dictionary
        ///     {
        ///        "id": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="404">If the item is null</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteDictionaryRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await Task.Run(() => _dictionaryService.DeleteDictionaryRecord(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}