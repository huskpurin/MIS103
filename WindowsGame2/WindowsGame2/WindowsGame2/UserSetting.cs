using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame2
{
    static class UserSetting
    {
        // 玩家設定
        public enum UserSex
        {
            Male = 1,
            Female = 2
        }

        public enum UserHander
        {
            Left_Hander = 1,
            Right_Hander = 2
        }

        public static UserSex userSex;
        public static UserHander userHander;
        
    }
}
