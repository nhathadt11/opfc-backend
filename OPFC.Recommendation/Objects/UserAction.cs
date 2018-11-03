using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Recommendation.Objects
{
    public class UserAction
    {
        public long UserId { get; set; }

        public string Action { get; set; }

        public long MenuId { get; set; }

        public string MenuName { get; set; }

        public UserAction(long userId, string action, long menuId, string menuName)
        {
            UserId = userId;
            Action = action;
            MenuId = menuId;
            MenuName = menuName;
        }
    }
}
