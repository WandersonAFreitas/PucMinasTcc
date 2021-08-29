using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class ClientError
    {
        public ClientError()
        {
        }

        public ClientError(IEnumerable<string> userMessages = null, IEnumerable<string> devMessages = null, int? errorCode = null)
        {
            UserMessages = userMessages;
            DevMessages = devMessages;
            ErrorCode = errorCode;
        }

        public IEnumerable<string> UserMessages { get; set; }
        public IEnumerable<string> DevMessages { get; set; }
        public int? ErrorCode { get; set; }
    }
}