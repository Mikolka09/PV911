using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUserAjax.Entities.Slider;

namespace WebUserAjax.Entities
{
    public class SliderItem
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public int SliderGroupId { get; set; }
        public SliderGroup SliderGroup { get; set; }
    }
}
