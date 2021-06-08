using System;

namespace Buns.Domain.Entities.Buns
{
    public class SourCreamBun : Bun
    {
        public const string BunTypeName = "Сметанник";

        public override double GetPrice(DateTimeOffset timeOffset)
        {
            if (!ControlSaleTimeExpired)
                return StartPrice;

            var price = StartPrice;
            for (var i = 0; i < GetHoursPassedAfterControlSaleTime(timeOffset); i++)
            {
                price /= 2;
            }

            return price;
        }

        public SourCreamBun(BunType bunType, double startPrice, int hoursToSell, int hoursToControlSaleTime)
            : base(bunType, startPrice, hoursToSell, hoursToControlSaleTime)
        {
        }

        public SourCreamBun(BunType bunType, double startPrice, int hoursToSell, int hoursToControlSaleTime,
            DateTimeOffset addHours)
            : base(bunType, startPrice, hoursToSell, hoursToControlSaleTime, addHours)
        {
        }

        protected SourCreamBun()
        {
        }
    }
}