using System;
using System.Threading.Tasks;
using ContosoMaintenance.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ContosoMaintenance.WebAPI.Controllers
{
    public class BaseController<T> : Controller where T : Models.BaseModel
    {
        public DocumentDBRepositoryBase<T> DBRepository = new DocumentDBRepositoryBase<T>();

        public BaseController()
        {
            DBRepository.Initialize();
        }

        void CreateDummyDataIfNeeded()
        {

        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var items = await DBRepository.GetItemsAsync(x => x.Id != null && x.IsDeleted != true);
            return new ObjectResult(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id) == true)
                return BadRequest();

            var items = await DBRepository.GetItemsAsync(x => x.Id == id);
            return new ObjectResult(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] T item)
        {
            if (item == null)
                return BadRequest();

            if (string.IsNullOrEmpty(item.Id))
                item.Id = Guid.NewGuid().ToString();

            await DBRepository.CreateItemAsync(item);
            return new ObjectResult(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] T item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var headline = await DBRepository.GetItemAsync(item.Id);
            if (headline == null)
            {
                return NotFound();
            }

            await DBRepository.UpdateItemAsync(item.Id, item);
            return new ObjectResult(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            T item = await DBRepository.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            // Soft Delete
            item.IsDeleted = true;
            await DBRepository.UpdateItemAsync(item.Id, item);
            //await DBRepository.DeleteItemAsync(id);

            return new ObjectResult(item);
        }
    }
}
