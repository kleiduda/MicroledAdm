using Cargill.DUE.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargill.DUE.Web.Services.Interfaces
{
    public interface IConsultaSerpro
    {
        void validarChaveDeAcesso(string key);
        ConsultaAverbacoesNFE GetConsultaChaveNfe(string chaveNfe);
    }
}
