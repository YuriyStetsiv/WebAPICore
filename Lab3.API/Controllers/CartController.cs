using Microsoft.AspNetCore.Mvc;
using Lab3.Domain.Models.Entities;
using Lab3.Domain.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Lab3.API.Controllers
{
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly IUserCartService _userCartService;
        public CartController(IUserCartService userCartService)
        {
            _userCartService = userCartService;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IEnumerable<UserCartModel>> Get(string userId)
        {
            var carts = await _userCartService.GetAllUserCart(userId);

            return carts;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Post([FromBody]UserCartModel userCartModel)
        {
            if (ModelState.IsValid)
            {
                await _userCartService.AddUserCart(userCartModel);

                return Ok(userCartModel);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete([FromBody]UserCartModel id)
        {
            await _userCartService.DeleteUserCart(id);

            return Ok();
        }

    }
}
