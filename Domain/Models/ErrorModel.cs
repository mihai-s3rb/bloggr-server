using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bloggr.Domain.Models
{
    public class ErrorModel
    {
        public ErrorModel()
        {
            Errors = new List<ErrorModel>();
        }
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public IList<ErrorModel> Errors { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
