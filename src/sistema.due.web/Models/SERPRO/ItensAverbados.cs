using Cargill.DUE.Web.Models.SERPRO;
using System;
using System.Collections.Generic;

namespace Cargill.DUE.Web.Models.SERPRO
{

    public class ItensAverbados
    {
        public string UnidadeTributavel { get; set; }
        public float? QtdeTributavel { get; set; }
        public DateTime? DataDoEmbarque { get; set; }
        public DateTime? DataDaAverbacao { get; set; }
        public int? ItemNfe { get; set; }
        public float? QtdeAverbada { get; set; }
        public string Due { get; set; }
        public int? ItemDue { get; set; }
    }

}
