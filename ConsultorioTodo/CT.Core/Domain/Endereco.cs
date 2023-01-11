using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CT.Core.Domain
{
    public class Endereco
    {
        public int ClienteId { get; set; }
        public int Cep { get; set; }
        public Estado Estado { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public Cliente Cliente { get; set; }
    }
}
