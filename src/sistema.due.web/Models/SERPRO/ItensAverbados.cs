using System;

namespace Cargill.DUE.Web.Models.SERPRO
{

    public class ItensAverbados
    {
        public string UnidadeTributavel { get; set; }
        public float? QtdeTributavel { get; set; }
        public string DataDoEmbarque { get; set; }
        public string DataDaAverbacao { get; set; }
        public int? ItemNfe { get; set; }
        public string QtdeAverbada { get; set; }
        public string Due { get; set; }
        public string ItemDue { get; set; }
    }

}
