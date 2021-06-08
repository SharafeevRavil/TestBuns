using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buns.Domain.Entities.Buns
{
    public class Bun
    {
        public long Id { get; set; }

        /// <summary>
        /// Тип булочки - круассан, крендель, багет, сметанник, батон
        /// </summary>
        public BunType BunType { get; set; }

        /// <summary>
        /// Определенное количество часов, в течение которых она должна быть продана
        /// </summary>
        public DateTimeOffset TimeToSell { get; set; }

        /// <summary>
        /// Цена, не превышает 100 руб.
        /// </summary>
        //fixme: double вместо decimal из-за провайдера sqlite ¯\_(ツ)_/¯
        public double StartPrice { get; set; }

        /// <summary>
        /// Контрольный срок продажи, после которого она становится несвежей и соответственно она хуже продается.
        /// </summary>
        public DateTimeOffset ControlSaleTime { get; set; }

        /// <summary>
        /// Время выпечки
        /// </summary>
        public DateTimeOffset BakeTime { get; set; }

        protected int GetHoursPassedAfterControlSaleTime(DateTimeOffset timeOffset) =>
            (int) (timeOffset - ControlSaleTime).TotalHours;

        public DateTimeOffset GetNextPriceChangeTime() =>
            DateTimeOffset.Now.AddMinutes(60 - (DateTimeOffset.Now - BakeTime).Minutes);


        protected double PriceDecreasePercent = 2;

        public virtual double GetPrice(DateTimeOffset timeOffset)
        {
            if (!ControlSaleTimeExpired)
                return StartPrice;

            var discount = GetHoursPassedAfterControlSaleTime(timeOffset) * PriceDecreasePercent / 100;
            if(discount > 1)
                return double.NaN;
            return StartPrice * (1 - discount);
        }

        public double GetCurrentPrice() => IsAbleToSell
            ? GetPrice(DateTimeOffset.Now)
            : throw new InvalidOperationException("Bun has no price when it is not able to sell");

        public double GetNextPrice() => IsAbleToSell
            ? GetPrice(GetNextPriceChangeTime())
            : throw new InvalidOperationException("Bun has no price when it is not able to sell");

        public bool ControlSaleTimeExpired => DateTimeOffset.Now > ControlSaleTime;
        public bool IsAbleToSell => DateTimeOffset.Now < TimeToSell;

        public Bun(BunType bunType, double startPrice, int hoursToSell, int hoursToControlSaleTime,
            DateTimeOffset bakeTime)
        {
            const int minPrice = 0;
            const int maxPrice = 100;
            if (startPrice < minPrice || startPrice > maxPrice)
                throw new ArgumentException($"Must be in [${minPrice}; ${maxPrice}] interval", nameof(startPrice));
            if (hoursToSell < 0)
                throw new ArgumentException("Must be non negative", nameof(hoursToSell));
            if (hoursToControlSaleTime < 0)
                throw new ArgumentException("Must be non negative", nameof(hoursToControlSaleTime));
            if (bakeTime > DateTimeOffset.Now)
                throw new ArgumentException("Must be less than current time", nameof(bakeTime));

            var controlSaleTime = bakeTime + TimeSpan.FromHours(hoursToControlSaleTime);
            
            BunType = bunType ?? throw new ArgumentException("Must be not null", nameof(bunType));
            StartPrice = startPrice;
            TimeToSell = bakeTime + TimeSpan.FromHours(hoursToSell);
            ControlSaleTime = controlSaleTime;
            BakeTime = bakeTime;
        }

        public Bun(BunType bunType, double startPrice, int hoursToSell, int hoursToControlSaleTime)
            : this(bunType, startPrice, hoursToSell, hoursToControlSaleTime, DateTimeOffset.Now)
        {
        }

        protected Bun()
        {
        }
    }
}