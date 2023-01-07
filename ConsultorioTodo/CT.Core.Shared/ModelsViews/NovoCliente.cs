using CT.Core.Domain;
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
        /// Nome o cliente.
        /// </summary>
        /// <example>Irineu Silva</example>
        public string Nome { get; set; }

        /// <summary>
        /// Data de nascimetodo cliente.
        /// </summary>
        /// <example>1995-01-01</example>
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// Sexo do cliente.
        /// </summary>
        /// <example>M</example>
        public char Sexo { get; set; }

        /// <summary>
        /// Telefone do cliente.
        /// </summary>
        /// <example>18997000000</example>
        public ICollection<NovoTelefone> Telefones { get; set; }

        /// <summary>
        /// Documento cliente: CNH, CPF e RG.
        /// </summary>
        /// <example>123456678</example>
        public string Documento { get; set; }

        public NovoEndereco Endereco { get; set; }
    }
}
