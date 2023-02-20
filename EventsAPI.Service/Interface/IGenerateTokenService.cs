using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAPI.Service.Interface
{
    public interface IGenerateTokenService
    {
        string GenerateTokenCityEvent(string nome, string permissao);
    }
}
