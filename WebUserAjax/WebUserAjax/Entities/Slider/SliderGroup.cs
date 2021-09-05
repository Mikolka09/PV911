using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUserAjax.Entities.Slider
{
    public class SliderGroup
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public List<SliderItem> SliderItems { get; set; }
    }
}
