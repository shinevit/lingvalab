using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using AutoMapper;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Lingva.BusinessLayer.Services;
using Lingva.DataAccessLayer.Dto;

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
        [HttpGet]
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
        [HttpPost]
        public async Task<IActionResult> PostDictionaryRecord([FromBody] CreateDictionaryRecordDTO record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await Task.Run(() => _dictionaryService.AddDictionaryRecord(record));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction("GetTranslation", record);
        }

        // PUT: api/Dictionary/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDictionaryRecord([FromRoute] int id, [FromBody] CreateDictionaryRecordDTO record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await Task.Run(() => _dictionaryService.UpdateDictionaryRecord(id, record));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction("GetTranslation", record);
        }

        // DELETE: api/Dictionary/5
        [HttpDelete("{id}")]
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