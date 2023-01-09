using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bloggr.Domain.Models
{
    public class ErrorModel
    {
        public ErrorModel()
        {
            Errors = new List<ErrorModel>();
        }

        public string Message { get; set; }

        public List<ErrorModel>? Errors { get; set; }

        public override string ToString()
        {
            //return JsonSerializer.Serialize(this);
            return JsonConvert.SerializeObject(
                this,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
    }
}
