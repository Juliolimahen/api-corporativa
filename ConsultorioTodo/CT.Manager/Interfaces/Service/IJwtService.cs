using CT.Core.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Manager.Interfaces.Managers;

public interface IJwtService
{
    string GerarToken(Usuario usuario);
}
