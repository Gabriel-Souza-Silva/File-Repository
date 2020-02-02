using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Iqvia.Enumerados;

namespace Test_Iqvia.Models
{
    public class FileUpload
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public string Duracao { get; set; }
        public int Ano { get; set; }
        public TipoMidia TipodeMidia { get; set; }
        public string Direcao { get; set; }
        public string Elenco { get; set; }
        public string NomeArquivo { get; set; }
    }
}
