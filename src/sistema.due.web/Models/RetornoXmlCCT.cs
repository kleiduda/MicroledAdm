using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Cargill.DUE.Web.Models
{
    public class RetornoXmlCCT
    {

        [XmlElement]
        public string message { get; set; }
        [XmlElement]
        public string status { get; set; }
        [XmlElement]
        public string code { get; set; }

        public string Identificador { get; set; }

        public RetornoXmlCCT(string message, string status, string code)
        {
            this.message = message;
            this.status = status;
            this.code = code;
        }

        public RetornoXmlCCT(string identificador)
        {
            Identificador = identificador;
        }

        public RetornoXmlCCT()
        {

        }
    }
}