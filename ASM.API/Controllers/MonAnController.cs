using ASM.Share.Interfaces;
using ASM.Share.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.API.Controllers
{
    [ApiController]
    public class MonAnController : ControllerBase
    {
        private readonly IMonAnService _monAnService;

        public MonAnController(IMonAnService monAnService)
        {
            _monAnService = monAnService;
        }
        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonAn>>> GetMonAns()
        {
            return await _monAnService.GetMonAnAllAsync();
        }

        [Route("api/[controller]/1")]
        [HttpGet]
        public async Task<ActionResult<List<MonAn>>> One()
        {
            List<MonAn> list = await _monAnService.GetMonAnAllAsync();
            var one = list.Where(x => x.Type == PhanLoai.MonAn || x.Type == PhanLoai.Nuoc).ToList();
            return one;
        }

        [Route("api/[controller]/2")]
        [HttpGet]
        public async Task<ActionResult<List<MonAn>>> Combo()
        {
            List<MonAn> list = await _monAnService.GetMonAnAllAsync();
            var combo = list.Where(x => x.Type == PhanLoai.Combo).ToList();
            return combo;
        }

        [Route("api/[controller]/search")]
        [HttpGet]
        public async Task<ActionResult<List<MonAn>>> Search([FromQuery] string name)
        {
            return await _monAnService.GetMonAnByNameAsync(name);
        }

    }
}
