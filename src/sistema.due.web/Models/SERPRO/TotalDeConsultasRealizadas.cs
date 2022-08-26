using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cargill.DUE.Web.Models
{

    public class TotalDeConsultasRealizadas
    {
        public string IdUsuario { get; set; }
        public DateTime DataConsulta { get; set; }
        public int QtdeDiaria { get; set; }
        public int ConsultasRealizadas { get; set; }
    }

}
