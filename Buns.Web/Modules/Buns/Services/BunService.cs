using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Buns.Data;
using Buns.Web.Modules.Buns.Dto;
using Microsoft.EntityFrameworkCore;

namespace Buns.Web.Modules.Buns.Services
{
    public class BunService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BunService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<BunListDto> GetList()
        {
            return _applicationDbContext.Buns
                .Include(x => x.BunType)
                //fixme: не транслируется в sqlite          
                //.Where(x => x.IsAbleToSell)
                .ToList()
                .Where(x => x.IsAbleToSell)
                .Select(x => new BunListDto
                {
                    Id = x.Id,
                    TypeName = x.BunType.Name,
                    StartPrice = x.StartPrice,
                    CurrentPrice = x.GetCurrentPrice(),
                    NextPrice = x.GetNextPrice(),
                    NextPriceChangeTime = x.GetNextPriceChangeTime()
                })
                .ToList();
        }
    }
}