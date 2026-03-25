using API.CLI.Enterprise;
using CLI.PoS.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.CLI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuItemController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return new MenuItemEC().Items;
        }

        [HttpDelete("{id}")]
        public Item? Delete(int id)
        {
            return new MenuItemEC().Delete(id);
        }

        [HttpPost]
        public Item? AddOrUpdate([FromBody] Item item)
        {
            return new MenuItemEC().AddOrUpdate(item);
        }
    }
}
