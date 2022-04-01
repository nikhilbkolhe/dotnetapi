using System;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.DTOs;




namespace Play.Catalog.Service.Controllers
{
    [ApiController]  //enables some features that make web api developmet easyu like automatically giving out 404 errors in case of ModelValidationerrors
    [Route("catalogItems")]
    public class CatalogItemsController : ControllerBase
    {
        public static readonly List<CatalogItemDto> CatalogItemsList = new List<CatalogItemDto>()
        {
            new CatalogItemDto(Guid.NewGuid(), "Potion", "Increases health by 2", (decimal)23.0, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow),
            new CatalogItemDto(Guid.NewGuid(), "Armor", "Prevents damage by 10 during attack", (decimal)45.9, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow),
            new CatalogItemDto(Guid.NewGuid(), "Steel Sword", "Increases damage caused on opponent by 10%", (decimal)45.9, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow)
        };

        //GET "/catalogItems/"
        [HttpGet]
        public IEnumerable<CatalogItemDto> GetAllCatalogItems()
        {
                return CatalogItemsList;
        }

        //GET "/catalogItems/{id}"
        [HttpGet("{id}")]
        public ActionResult<CatalogItemDto> GetCatalogItem(Guid id)
        {            
            CatalogItemDto Catalogitem = CatalogItemsList.Where(x => x.Id == id).FirstOrDefault();
            if (Catalogitem == null)
            {
                return NotFound();
            }
            return Catalogitem;
        }

        //POST "/catalogItems/"
        [HttpPost]
        public ActionResult<CatalogItemDto> CreateCatalogItem(CreateCatalogItemDTO input)
        {
            if (input == null)
            {
                return BadRequest();                
            }
            Guid newItemGuid = Guid.NewGuid();
            DateTimeOffset newItemCreateDate = DateTimeOffset.UtcNow;
            CatalogItemDto newCatalogItem = new CatalogItemDto
            (
                newItemGuid,
                input.Name,
                input.Description,
                input.Price,
                newItemCreateDate,
                newItemCreateDate
            );

            CatalogItemsList.Add(newCatalogItem);
            
            return CreatedAtAction(nameof(GetCatalogItem),new{id = newItemGuid}, newCatalogItem);            
        }

        //PUT "/catalogItems/{id}"
        [HttpPut("{id}")]
        public IActionResult UpdateCatalogItem (Guid id, UpdateCatalogItemsDTO input)
        {                
                var oldItem = CatalogItemsList.Where(x => x.Id == id).FirstOrDefault();        
                if (oldItem == null)
                {
                    return NotFound();
                }
                CatalogItemDto newItem = oldItem with 
                {
                    Name = input.Name,
                    Description = input.Description,
                    Price = input.Price,
                    LastUpdateDate = DateTimeOffset.UtcNow
                    
                };

                var oldIndex = CatalogItemsList.FindIndex(x => x.Id == id);
                if (oldIndex == -1)
                {
                    return NotFound();
                }
                CatalogItemsList[oldIndex] = newItem;
                return NoContent();
        }


        //DELETE "/catalogItems/{id}"
        [HttpDelete("{id}")]
        public IActionResult DeleteCatalogItem(Guid id)
        {
            var oldItem = CatalogItemsList.Where(x => x.Id == id).FirstOrDefault();
            if (oldItem == null)
            {
                return NotFound();
            }
            
            var oldIndex = CatalogItemsList.FindIndex(x => x.Id == id);
            if (oldIndex == -1)
            {
                return NotFound();
            }

            CatalogItemsList.RemoveAt(oldIndex);
            return NoContent();

        }





    }
}