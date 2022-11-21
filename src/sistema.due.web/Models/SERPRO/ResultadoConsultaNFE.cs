using Cargill.DUE.Web.Models.SERPRO;
using System.Collections.Generic;

namespace Cargill.DUE.Web.Models
{

    public class ResultadoConsultaNFE
    {
        public string ChaveNfe { get; set; }
        public List<EventosNfe> Eventos { get; set; }
        public List<ProdutosNfe> Produtos { get; set; }
        public string ArquivoXml { get; set; }

    }

}
