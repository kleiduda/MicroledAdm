﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargill.DUE.Web.Models.SERPRO
{
    public class ConsultaSerproView
    {

        public string ChaveNfe { get; set; }
        public string ArquivoXml { get; set; }
        public string UnidadeTributavel { get; set; }
        public string QtdeTributavel { get; set; }
        public string ItemNfe { get; set; }
        public string DataDoEmbarque { get; set; }
        public string DataAverbacao { get; set; }
        public string QtdeAverbada { get; set; }
        public string Due { get; set; }
        public string ItemDue { get; set; }
        public string Descricao { get; set; }
        public string DataDaConsulta { get; set; }
    }
}
