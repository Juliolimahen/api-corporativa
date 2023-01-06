using System;
using System.Collections.Generic;
using System.Text;

namespace CT.Core.Shared.ModelsViews
{
    public class NovoEndereco
    {
        ///<example>17900000</example>
        public int Cep { get; set; }

        public string Estado { get; set; }

        ///<example>Dracena</example>
        public string Cidade { get; set; }

        ///<example>Rua Adamantina</example>
        public string Logradouro { get; set; }

        ///<example>13</example>
        public string Numero { get; set; }

        ///<example>Ao lado do posto</example>
        public string Complemento { get; set; }
    }
}
