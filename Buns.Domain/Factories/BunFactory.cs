using System;
using Buns.Domain.Entities.Buns;

namespace Buns.Domain.Factories
{
    public class BunFactory
    {
        //придется захардкодить тип в дочерних классах
        public Bun CreateBun(BunType bunType, double startPrice, int hoursToSell, int hoursToControlSaleTime) =>
            CreateBun(bunType, startPrice, hoursToSell, hoursToControlSaleTime, DateTimeOffset.Now);

        public Bun CreateBun(BunType bunType, double startPrice, int hoursToSell, int hoursToControlSaleTime, DateTimeOffset addHours) =>
            bunType.Name switch
            {
                PretzelBun.BunTypeName => new PretzelBun(bunType, startPrice, hoursToSell, hoursToControlSaleTime, addHours),
                SourCreamBun.BunTypeName => new SourCreamBun(bunType, startPrice, hoursToSell, hoursToControlSaleTime, addHours),
                _ => new Bun(bunType, startPrice, hoursToSell, hoursToControlSaleTime, addHours)
            };
    }
}