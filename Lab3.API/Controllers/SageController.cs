using AutoMapper;
using Lab3.API.Models;
using Lab3.Domain.Models.Entities;
using Lab3.Domain.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3.API.Controllers
{
    [Route("api/[controller]")]
    public class SageController : Controller
    {
        private readonly ISageService _sageService;
        private readonly IMapper _mapper;

        public SageController(ISageService sageService, IMapper mapper)
        {
            _sageService = sageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SageViewModel>> Get()
        {
            var sages = await _sageService.GetAllSage();
            var sagesView = _mapper.Map<IEnumerable<SageViewModel>>(sages);

            return sagesView;
        }

        [HttpGet("{id}")]
        public async Task<SageViewModel> Get(int id)
        {
            var sage = await _sageService.GetSageById(id);
            var sageView = _mapper.Map<SageViewModel>(sage);

            return sageView;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody]SageViewModel sageView)
        {
            if (ModelState.IsValid)
            {
                var sage = _mapper.Map<SageModel>(sageView);
                await _sageService.AddSage(sage);

                return Ok(sage);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody]SageViewModel sageView)
        {
            if (ModelState.IsValid)
            {
                var sage = _mapper.Map<SageModel>(sageView);
                await _sageService.EditSage(sage);

                return Ok(sage);
            }
            return BadRequest(ModelState);
        }

        [Route("DeleteSage/{id:int}")]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sageService.DeleteSage(id);

            return Ok();
        }
    }
}
