using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Test_Iqvia.Enumerados;

namespace Test_Iqvia.Models
{
    public class FIleSender
    {
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public string Duracao { get; set; }
        public int Ano { get; set; }
        public TipoMidia TipodeMidia { get; set; }
        public string Direcao { get; set; }
        public string Elenco { get; set; }
        //public FileUploadAPI Arquivo { get; set; }
        public IFormFile files { get; set; }
    }
}
