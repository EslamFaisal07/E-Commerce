using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.BasketModuleDTos
{
    public class BasketDto
    {

        public string Id { get; set; }

        public ICollection<BasketItemDTo> Items { get; set; }

    }
}
