using System;
using System.Collections.Generic;
using System.Text;

namespace CT.Core.Shared.ModelsViews.Telefone
{
    public class TelefoneView : ICloneable
    {
        public int Id { get; set; }
        public string Numero { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
