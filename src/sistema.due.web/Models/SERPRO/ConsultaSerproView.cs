using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargill.DUE.Web.Models.SERPRO
{
    public class ConsultaSerproView
    {

        public string ChaveNfe { get; set; }
        public string UnidadeTributavel { get; set; }
        public float QtdeTributavel { get; set; }
        public int? ItemNfe { get; set; }
        public DateTime? DataDoEmbarque { get; set; }
        public DateTime? DataDaAverbacao { get; set; }
        public float? QtdeAverbada { get; set; }
        public string Due { get; set; }
        public int? ItemDue { get; set; }
        public string Descricao { get; set; }
    }
}
