using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSystem.Service
{
    public class Common
    {

        public class ResultViewModel
        {
            public string ConsoleMessage { get; set; }
            public string Message { get; set; }
            public MessageTypeEnum Status { get; set; }

        }

        public enum MessageTypeEnum
        {
            Success = 1,
            Error = 2,
            Warning = 3,
            Exception = -2,

        }
    }
}
