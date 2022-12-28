using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Domain.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {

        }

        public CustomException(string message, IEnumerable<string> errors) : base(message)
        {
            Errors = new List<string>();
            foreach(var error in errors)
            {
                Errors.Add(error);
            }
        }

        public List<string>? Errors { get; set; }


    }
}
