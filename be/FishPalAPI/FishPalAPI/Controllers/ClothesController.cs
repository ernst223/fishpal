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
        /// gets the order items
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        [HttpGet("getOrderItems")]
        public async Task<IActionResult> getOrderItems()
        {
            try
            {  
                return Ok(clothesService.getOrderItems());
            }
            catch (Exception e)
            {
                return BadRequest();
            }
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
        /// <summary>
        /// update a item
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        [HttpPut("updateOrderItem")]
        public async Task<IActionResult> updateOrderItem([FromBody] OrderItems T)
        {
            try
            {
                bool result = clothesService.updateOrderItem(T);
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
        /// <summary>
        /// delete a item
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        [HttpDelete("deleteOrderItem/{orderItemId}")]
        public async Task<IActionResult> deleteOrderItem(int orderItemId)
        {
            try
            {
                bool result = clothesService.deleteOrderItem(orderItemId);
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
