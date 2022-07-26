using System;
using System.Collections.Generic;

#nullable disable

namespace qqqq.Models
{
    public partial class Color
    {
        public Color()
        {
            MemberWishes = new HashSet<MemberWish>();
            PetDetails = new HashSet<PetDetail>();
        }

        public int ColorId { get; set; }
        public string ColorName { get; set; }

        public virtual ICollection<MemberWish> MemberWishes { get; set; }
        public virtual ICollection<PetDetail> PetDetails { get; set; }
    }
}
