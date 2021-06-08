using System;

namespace Buns.Domain.Entities.Buns
{
    public class PretzelBun : Bun
    {
        public const string BunTypeName = "Крендель";

        public PretzelBun(BunType bunType, double startPrice, int hoursToSell, int hoursToControlSaleTime)
            : base(bunType, startPrice, hoursToSell, hoursToControlSaleTime)
        {
            PriceDecreasePercent *= 2;
        }

        public PretzelBun(BunType bunType, double startPrice, int hoursToSell, int hoursToControlSaleTime,
            DateTimeOffset addHours)
            : base(bunType, startPrice, hoursToSell, hoursToControlSaleTime, addHours)
        {
        }

        private PretzelBun()
        {
            PriceDecreasePercent *= 2;
        }
    }
}