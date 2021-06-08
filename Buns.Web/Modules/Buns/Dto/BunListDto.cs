using System;

namespace Buns.Web.Modules.Buns.Dto
{
    public class BunListDto
    {
        public long Id { get; set; }
        public string TypeName { get; set; }
        public double StartPrice { get; set; }
        public double CurrentPrice { get; set; }
        public double NextPrice { get; set; }
        public DateTimeOffset NextPriceChangeTime { get; set; }
    }
}