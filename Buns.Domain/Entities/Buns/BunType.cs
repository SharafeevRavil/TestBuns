using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buns.Domain.Entities.Buns
{
    public class BunType
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public BunType(string name)
        {
            Name = name;
        }

        private BunType()
        {
        }
    }
}