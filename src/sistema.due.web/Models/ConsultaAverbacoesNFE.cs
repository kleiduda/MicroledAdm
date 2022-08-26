using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cargill.DUE.Web.Models
{

    public class ConsultaAverbacoesNFE
    {
        public Nfeproc nfeProc { get; set; }
        public Proceventonfe[] procEventoNFe { get; set; }
        public string Error { get; set; }
    }

    public class Nfeproc
    {
        //public Protnfe protNFe { get; set; }
        public Nfe NFe { get; set; }
        //public int versao { get; set; }
    }

    public class Protnfe
    {
        //public Infprot infProt { get; set; }
    }

    public class Infprot
    {
        //public long nProt { get; set; }
        //public string digVal { get; set; }
        //public DateTime dhRecbto { get; set; }
        //public string Id { get; set; }
        //public string chNFe { get; set; }
        //public string xMotivo { get; set; }
        //public int cStat { get; set; }
    }

    public class Nfe
    {
        public Infnfe infNFe { get; set; }
    }

    public class Infnfe
    {
        //public Infadic infAdic { get; set; }
        //public Infresptec infRespTec { get; set; }
        public Det[] det { get; set; }
        //public Total total { get; set; }
        //public Cobr cobr { get; set; }
        //public Pag pag { get; set; }
        //public Ide ide { get; set; }
        //public string Id { get; set; }
        //public Dest dest { get; set; }
        //public Emit emit { get; set; }
        //public Transp transp { get; set; }
        //public Retirada retirada { get; set; }
    }

    public class Infadic
    {
        //public Obscont[] obsCont { get; set; }
        //public string infCpl { get; set; }
        //public string infAdFisco { get; set; }
    }

    public class Obscont
    {
        //public string xCampo { get; set; }
        //public string xTexto { get; set; }
    }

    public class Infresptec
    {
        //public long fone { get; set; }
        //public string CNPJ { get; set; }
        //public string xContato { get; set; }
        //public string email { get; set; }
    }

    public class Total
    {
        //public Icmstot ICMSTot { get; set; }
    }

    public class Icmstot
    {
        //public float vICMSUFDest { get; set; }
        //public float vICMSUFRemet { get; set; }
        //public float vCOFINS { get; set; }
        //public float vBCST { get; set; }
        //public decimal vICMSDeson { get; set; }
        //public float vFCPUFDest { get; set; }
        //public float vProd { get; set; }
        //public float vSeg { get; set; }
        //public float vFCP { get; set; }
        //public float vFCPST { get; set; }
        //public float vNF { get; set; }
        //public float vPIS { get; set; }
        //public float vIPIDevol { get; set; }
        //public float vBC { get; set; }
        //public float vST { get; set; }
        //public float vICMS { get; set; }
        //public float vII { get; set; }
        //public float vFCPSTRet { get; set; }
        //public float vDesc { get; set; }
        //public float vOutro { get; set; }
        //public float vIPI { get; set; }
        //public float vFrete { get; set; }
    }

    public class Cobr
    {
        //public Fat fat { get; set; }
        //public Dup[] dup { get; set; }
    }

    public class Fat
    {
        //public float vOrig { get; set; }
        //public int nFat { get; set; }
        //public int vDesc { get; set; }
        //public float vLiq { get; set; }
    }

    public class Dup
    {
        //public string dVenc { get; set; }
        //public string nDup { get; set; }
        //public float vDup { get; set; }
    }

    public class Pag
    {
        //public int vTroco { get; set; }
        //public Detpag[] detPag { get; set; }
    }

    public class Detpag
    {
        //public float vPag { get; set; }
        //public int tPag { get; set; }
        //public int indPag { get; set; }
    }

    public class Ide
    {
        //public int tpNF { get; set; }
        //public int mod { get; set; }
        //public int indPres { get; set; }
        //public int tpImp { get; set; }
        //public int nNF { get; set; }
        //public int cMunFG { get; set; }
        //public int procEmi { get; set; }
        //public int finNFe { get; set; }
        //public DateTime dhEmi { get; set; }
        //public int tpAmb { get; set; }
        //public int indFinal { get; set; }
        //public DateTime dhSaiEnt { get; set; }
        //public int idDest { get; set; }
        //public int tpEmis { get; set; }
        //public int cDV { get; set; }
        //public int cUF { get; set; }
        //public int serie { get; set; }
        //public string natOp { get; set; }
        //public string cNF { get; set; }
        //public string verProc { get; set; }
    }

    public class Dest
    {
        //public string xNome { get; set; }
        //public long CNPJ { get; set; }
        //public Enderdest enderDest { get; set; }
        //public long IE { get; set; }
        //public int indIEDest { get; set; }
        //public string email { get; set; }
    }

    public class Enderdest
    {
        //public string xCpl { get; set; }
        //public string fone { get; set; }
        //public string UF { get; set; }
        //public string xPais { get; set; }
        //public int cPais { get; set; }
        //public string xLgr { get; set; }
        //public string xMun { get; set; }
        //public string nro { get; set; }
        //public int cMun { get; set; }
        //public string xBairro { get; set; }
        //public string CEP { get; set; }
    }

    public class Emit
    {
        //public int CNAE { get; set; }
        //public string xNome { get; set; }
        //public string IM { get; set; }
        //public int CRT { get; set; }
        //public string xFant { get; set; }
        //public string CNPJ { get; set; }
        //public Enderemit enderEmit { get; set; }
        //public long IE { get; set; }
    }

    public class Enderemit
    {
        //public string xCpl { get; set; }
        //public long fone { get; set; }
        //public string UF { get; set; }
        //public string xPais { get; set; }
        //public int cPais { get; set; }
        //public string xLgr { get; set; }
        //public string xMun { get; set; }
        //public string nro { get; set; }
        //public int cMun { get; set; }
        //public string xBairro { get; set; }
        //public int CEP { get; set; }
    }

    public class Transp
    {
        //public int modFrete { get; set; }
        //public Vol[] vol { get; set; }
        //public Transporta transporta { get; set; }
    }

    public class Transporta
    {
        //public string xNome { get; set; }
        //public string UF { get; set; }
        //public string xEnder { get; set; }
        //public string xMun { get; set; }
        //public string CNPJ { get; set; }
        //public long IE { get; set; }
    }

    public class Vol
    {
        //public float pesoL { get; set; }
        //public string esp { get; set; }
        //public int qVol { get; set; }
        //public float pesoB { get; set; }
    }

    public class Retirada
    {
        //public int cPais { get; set; }
        //public string xLgr { get; set; }
        //public string nro { get; set; }
        //public int cMun { get; set; }
        //public string xBairro { get; set; }
        //public int CEP { get; set; }
        //public long fone { get; set; }
        //public string xNome { get; set; }
        //public string UF { get; set; }
        //public string xPais { get; set; }
        //public string CPF { get; set; }
        //public string xMun { get; set; }
        //public string IE { get; set; }
        //public string email { get; set; }
    }

    public class Det
    {
        //public int nItem { get; set; } // campo ItemNFE
        public Prod prod { get; set; }
        //public Imposto imposto { get; set; }
    }

    public class Prod
    {
        //public string cEAN { get; set; }
        //public long cProd { get; set; }
        //public int qCom { get; set; }
        //public string cEANTrib { get; set; }
        //public float vUnTrib { get; set; }
        public float qTrib { get; set; }
        //public float vProd { get; set; }
        //public string xProd { get; set; }
        //public float vUnCom { get; set; }
        //public int indTot { get; set; }
        public string uTrib { get; set; } //campo UnidadeTributavel
        //public int NCM { get; set; }
        //public string uCom { get; set; }
        //public int CFOP { get; set; }
    }

    public class Imposto
    {
        //public ICMS ICMS { get; set; }
        //public IPI IPI { get; set; }
        //public COFINS COFINS { get; set; }
        //public PIS PIS { get; set; }
    }

    public class ICMS
    {
        //public ICMS40 ICMS40 { get; set; }
    }

    public class ICMS40
    {
        //public int orig { get; set; }
        //public int CST { get; set; }
    }

    public class IPI
    {
        //public IPINT IPINT { get; set; }
        //public int cEnq { get; set; }
    }

    public class IPINT
    {
        //public int CST { get; set; }
    }

    public class COFINS
    {
        //public COFINSNT COFINSNT { get; set; }
    }

    public class COFINSNT
    {
        //public string CST { get; set; }
    }

    public class PIS
    {
        //public PISNT PISNT { get; set; }
    }

    public class PISNT
    {
        //public string CST { get; set; }
    }

    public class Proceventonfe
    {
        public Evento evento { get; set; }
        //public Retevento retEvento { get; set; }
    }

    public class Evento
    {
        public Infevento infEvento { get; set; }
    }

    public class Infevento
    {
        //public int cOrgao { get; set; }
        //public DateTime dhEvento { get; set; }
        public Detevento detEvento { get; set; }
        //public int tpEvento { get; set; }
        //public int nSeqEvento { get; set; }
        //public long CNPJ { get; set; }
        public string Id { get; set; }
        //public string chNFe { get; set; } // campo chaveNfe
        //public int tpAmb { get; set; }
        //public int verEvento { get; set; }
    }

    public class Detevento
    {
        public string descEvento { get; set; }
        //public int versao { get; set; }
        public Itensaverbados itensAverbados { get; set; }
        //public int verAplic { get; set; }
        //public int tpAutor { get; set; }
    }

    public class Itensaverbados
    {
        public int nItem { get; set; }
        public string nDue { get; set; } // campo DUE
        public DateTime dhEmbarque { get; set; }
        //public int motAlteracao { get; set; }
        public DateTime dhAverbacao { get; set; }
        public int nItemDue { get; set; } // campo ItemDue
        public float qItem { get; set; }
    }

    public class Retevento
    {
        //public Infevento1 infEvento { get; set; }
    }

    public class Infevento1
    {
        //public int cOrgao { get; set; }
        //public long nProt { get; set; }
        //public DateTime dhRegEvento { get; set; }
        //public int tpEvento { get; set; }
        //public int nSeqEvento { get; set; }
        //public string verAplic { get; set; }
        //public string xMotivo { get; set; }
        //public string chNFe { get; set; }
        //public int tpAmb { get; set; }
        //public long CNPJDest { get; set; }
        //public int cStat { get; set; }
        //public string xEvento { get; set; }
        //public string Id { get; set; }
    }

}
