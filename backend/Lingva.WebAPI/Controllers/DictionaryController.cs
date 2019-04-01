using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lingva.BusinessLayer.Contracts;
using Lingva.DataAccessLayer.Entities;
using Lingva.WebAPI.Dto;
using Microsoft.AspNetCore.Mvc;

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
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var dictionaryRecord = _dictionaryService.GetDictionaryRecord(id);

            if (dictionaryRecord == null) return NotFound();

            return Ok(_mapper.Map<DictionaryRecordViewDTO>(dictionaryRecord));
        }

        // POST: api/Dictionary
        [HttpPost]
        public async Task<IActionResult> PostDictionaryRecord(
            [FromBody] DictionaryRecordCreatingDTO dictionaryRecordCreatingDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var dictionaryRecord = _mapper.Map<DictionaryRecord>(dictionaryRecordCreatingDTO);
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
        public async Task<IActionResult> PutDictionaryRecord([FromRoute] int id,
            [FromBody] DictionaryRecordCreatingDTO dictionaryRecordCreatingDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var dictionaryRecord = _mapper.Map<DictionaryRecord>(dictionaryRecordCreatingDTO);
                await Task.Run(() => _dictionaryService.UpdateDictionaryRecord(id, dictionaryRecord));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE: api/Dictionary/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDictionaryRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

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