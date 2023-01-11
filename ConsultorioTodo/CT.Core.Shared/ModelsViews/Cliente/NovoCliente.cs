
using CT.Core.Shared.ModelsViews.Cliente;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.Core.Shared.ModelsViews
{
    /// <summary>
    /// Objeto utilizado para inserção de um novo cliente.
    /// </summary>
    public class NovoCliente
    {
        /// <summary>
        /// Nome do cliente
        /// </summary>
        /// <example>Irineu Silva</example>
        public string Nome { get; set; }

        /// <summary>
        /// Data do nascimento do cliente.
        /// </summary>
        /// <example>1990-03-01</example>
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// Sexo do cliente
        /// </summary>
        /// <example>M</example>
        public SexoView Sexo { get; set; }

        /// <summary>
        /// Documento do cliente: CNH, CPF, RG
        /// </summary>
        /// <example>1234123223</example>
        public string Documento { get; set; }

        public NovoEndereco Endereco { get; set; }

        public ICollection<NovoTelefone> Telefones { get; set; }
    }
}
