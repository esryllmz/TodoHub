using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoHub.Core.Responses
{
     public class ReturnModel<TData>
    {
        public TData Data { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public int Status { get; set; }


    }
}
