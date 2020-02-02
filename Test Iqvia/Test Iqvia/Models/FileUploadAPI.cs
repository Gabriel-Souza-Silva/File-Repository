using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Test_Iqvia.Models
{
    public class FileUploadAPI
    {
        public IFormFile files { get; set; }
    }
}
