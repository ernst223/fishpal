using FishPalAPI.Data;
using FishPalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FishPalAPI.Controllers
{
    [Route("api/clothes")]
    public class ClothesController : Controller
    {
        private ClothesService clothesService;

        public ClothesController()
        {
            clothesService = new ClothesService();
        }

        /// <summary>
        /// Insert a new Item that can be ordered
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        [HttpPost("insertOrderItem")]
        public async Task<IActionResult> insertItem([FromBody] OrderItems T)
        {
            try
            {
                bool result = clothesService.insertOrderItems(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
