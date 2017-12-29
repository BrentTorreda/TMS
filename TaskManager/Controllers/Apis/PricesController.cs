using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using System.Web.Http;
using TaskManager.Dtos;
using TaskManager.Models;

namespace TaskManager.Controllers.Apis
{
    public class PricesController : ApiController
    {
        private ApplicationDbContext _context;

        public PricesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/prices
        public IHttpActionResult GetPrices()
        {
            var priceDtos = _context.Prices
                .ToList()
                .Select(Mapper.Map<Prices, PriceDto>);

            return Ok(priceDtos);
        }

        // DELETE /api/price/id
        [HttpDelete]
        public IHttpActionResult DeletePrice(int id)
        {
            var priceInDb = _context.Prices.SingleOrDefault(p => p.PriceId == id);

            if (priceInDb == null)
                return NotFound();

            _context.Prices.Remove(priceInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
