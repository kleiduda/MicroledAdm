
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
using System.Linq;
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
public partial class Envelope
{

    private EnvelopeBody bodyField;

    /// <remarks/>
    public EnvelopeBody Body
    {
        get
        {
            return this.bodyField;
        }
        set
        {
            this.bodyField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
public partial class EnvelopeBody
{

    private nfeConsultaNFeLogResult nfeConsultaNFeLogResultField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
    public nfeConsultaNFeLogResult nfeConsultaNFeLogResult
    {
        get
        {
            return this.nfeConsultaNFeLogResultField;
        }
        set
        {
            this.nfeConsultaNFeLogResultField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class nfeConsultaNFeLogResult
{

    private nfeConsultaNFeLogResultRetConsNFeLog retConsNFeLogField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLog retConsNFeLog
    {
        get
        {
            return this.retConsNFeLogField;
        }
        set
        {
            this.retConsNFeLogField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLog
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLog nFeLogField;

    private string versaoField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLog NFeLog
    {
        get
        {
            return this.nFeLogField;
        }
        set
        {
            this.nFeLogField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string versao
    {
        get
        {
            return this.versaoField;
        }
        set
        {
            this.versaoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLog
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProc nfeProcField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFe[] procEventoNFeField;

    private string versaoField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProc nfeProc
    {
        get
        {
            return this.nfeProcField;
        }
        set
        {
            this.nfeProcField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("procEventoNFe")]
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFe[] procEventoNFe
    {
        get
        {
            return this.procEventoNFeField;
        }
        set
        {
            this.procEventoNFeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string versao
    {
        get
        {
            return this.versaoField;
        }
        set
        {
            this.versaoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProc
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcProtNFe protNFeField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFe nFeField;

    private string versaoField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcProtNFe protNFe
    {
        get
        {
            return this.protNFeField;
        }
        set
        {
            this.protNFeField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFe NFe
    {
        get
        {
            return this.nFeField;
        }
        set
        {
            this.nFeField = value;
        }
    }

    /// <remarks/>
    public string versao
    {
        get
        {
            return this.versaoField;
        }
        set
        {
            this.versaoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcProtNFe
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcProtNFeInfProt infProtField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcProtNFeInfProt infProt
    {
        get
        {
            return this.infProtField;
        }
        set
        {
            this.infProtField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcProtNFeInfProt
{

    private string nProtField;

    private string digValField;

    private System.DateTime dhRecbtoField;

    private string chNFeField;

    private string xMotivoField;

    private string cStatField;

    /// <remarks/>
    public string nProt
    {
        get
        {
            return this.nProtField;
        }
        set
        {
            this.nProtField = value;
        }
    }

    /// <remarks/>
    public string digVal
    {
        get
        {
            return this.digValField;
        }
        set
        {
            this.digValField = value;
        }
    }

    /// <remarks/>
    public System.DateTime dhRecbto
    {
        get
        {
            return this.dhRecbtoField;
        }
        set
        {
            this.dhRecbtoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
    public string chNFe
    {
        get
        {
            return this.chNFeField;
        }
        set
        {
            this.chNFeField = value;
        }
    }

    /// <remarks/>
    public string xMotivo
    {
        get
        {
            return this.xMotivoField;
        }
        set
        {
            this.xMotivoField = value;
        }
    }

    /// <remarks/>
    public string cStat
    {
        get
        {
            return this.cStatField;
        }
        set
        {
            this.cStatField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFe
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFe infNFeField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFe infNFe
    {
        get
        {
            return this.infNFeField;
        }
        set
        {
            this.infNFeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFe
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeInfAdic infAdicField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeInfRespTec infRespTecField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDet[] detField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeTotal totalField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeCobr cobrField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFePag pagField;

    private string idField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeIde ideField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeEmit emitField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDest destField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeTransp transpField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeInfAdic infAdic
    {
        get
        {
            return this.infAdicField;
        }
        set
        {
            this.infAdicField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeInfRespTec infRespTec
    {
        get
        {
            return this.infRespTecField;
        }
        set
        {
            this.infRespTecField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("det")]
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDet[] det
    {
        get
        {
            return this.detField;
        }
        set
        {
            this.detField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeTotal total
    {
        get
        {
            return this.totalField;
        }
        set
        {
            this.totalField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeCobr cobr
    {
        get
        {
            return this.cobrField;
        }
        set
        {
            this.cobrField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFePag pag
    {
        get
        {
            return this.pagField;
        }
        set
        {
            this.pagField = value;
        }
    }

    /// <remarks/>
    public string Id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeIde ide
    {
        get
        {
            return this.ideField;
        }
        set
        {
            this.ideField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeEmit emit
    {
        get
        {
            return this.emitField;
        }
        set
        {
            this.emitField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDest dest
    {
        get
        {
            return this.destField;
        }
        set
        {
            this.destField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeTransp transp
    {
        get
        {
            return this.transpField;
        }
        set
        {
            this.transpField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeInfAdic
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeInfAdicObsCont[] obsContField;

    private string infCplField;

    private string infAdFiscoField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("obsCont")]
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeInfAdicObsCont[] obsCont
    {
        get
        {
            return this.obsContField;
        }
        set
        {
            this.obsContField = value;
        }
    }

    /// <remarks/>
    public string infCpl
    {
        get
        {
            return this.infCplField;
        }
        set
        {
            this.infCplField = value;
        }
    }

    /// <remarks/>
    public string infAdFisco
    {
        get
        {
            return this.infAdFiscoField;
        }
        set
        {
            this.infAdFiscoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeInfAdicObsCont
{

    private string xCampoField;

    private string xTextoField;

    /// <remarks/>
    public string xCampo
    {
        get
        {
            return this.xCampoField;
        }
        set
        {
            this.xCampoField = value;
        }
    }

    /// <remarks/>
    public string xTexto
    {
        get
        {
            return this.xTextoField;
        }
        set
        {
            this.xTextoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeInfRespTec
{

    private string foneField;

    private string cNPJField;

    private string xContatoField;

    private string emailField;

    /// <remarks/>
    public string fone
    {
        get
        {
            return this.foneField;
        }
        set
        {
            this.foneField = value;
        }
    }

    /// <remarks/>
    public string CNPJ
    {
        get
        {
            return this.cNPJField;
        }
        set
        {
            this.cNPJField = value;
        }
    }

    /// <remarks/>
    public string xContato
    {
        get
        {
            return this.xContatoField;
        }
        set
        {
            this.xContatoField = value;
        }
    }

    /// <remarks/>
    public string email
    {
        get
        {
            return this.emailField;
        }
        set
        {
            this.emailField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDet
{

    private string nItemField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetProd prodField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImposto impostoField;

    /// <remarks/>
    public string nItem
    {
        get
        {
            return this.nItemField;
        }
        set
        {
            this.nItemField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetProd prod
    {
        get
        {
            return this.prodField;
        }
        set
        {
            this.prodField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImposto imposto
    {
        get
        {
            return this.impostoField;
        }
        set
        {
            this.impostoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetProd
{

    private string xPedField;

    private string cEANField;

    private string cProdField;

    private string indEscalaField;

    private string qComField;

    private string cEANTribField;

    private string vUnTribField;

    private string qTribField;

    private string vProdField;

    private string xProdField;

    private string vUnComField;

    private string nItemPedField;

    private string indTotField;

    private string uTribField;

    private string nCMField;

    private string uComField;

    private string cFOPField;

    private string cESTField;

    /// <remarks/>
    public string xPed
    {
        get
        {
            return this.xPedField;
        }
        set
        {
            this.xPedField = value;
        }
    }

    /// <remarks/>
    public string cEAN
    {
        get
        {
            return this.cEANField;
        }
        set
        {
            this.cEANField = value;
        }
    }

    /// <remarks/>
    public string cProd
    {
        get
        {
            return this.cProdField;
        }
        set
        {
            this.cProdField = value;
        }
    }

    /// <remarks/>
    public string indEscala
    {
        get
        {
            return this.indEscalaField;
        }
        set
        {
            this.indEscalaField = value;
        }
    }

    /// <remarks/>
    public string qCom
    {
        get
        {
            return this.qComField;
        }
        set
        {
            this.qComField = value;
        }
    }

    /// <remarks/>
    public string cEANTrib
    {
        get
        {
            return this.cEANTribField;
        }
        set
        {
            this.cEANTribField = value;
        }
    }

    /// <remarks/>
    public string vUnTrib
    {
        get
        {
            return this.vUnTribField;
        }
        set
        {
            this.vUnTribField = value;
        }
    }

    /// <remarks/>
    public string qTrib
    {
        get
        {
            return this.qTribField;
        }
        set
        {
            this.qTribField = value;
        }
    }

    /// <remarks/>
    public string vProd
    {
        get
        {
            return this.vProdField;
        }
        set
        {
            this.vProdField = value;
        }
    }

    /// <remarks/>
    public string xProd
    {
        get
        {
            return this.xProdField;
        }
        set
        {
            this.xProdField = value;
        }
    }

    /// <remarks/>
    public string vUnCom
    {
        get
        {
            return this.vUnComField;
        }
        set
        {
            this.vUnComField = value;
        }
    }

    /// <remarks/>
    public string nItemPed
    {
        get
        {
            return this.nItemPedField;
        }
        set
        {
            this.nItemPedField = value;
        }
    }

    /// <remarks/>
    public string indTot
    {
        get
        {
            return this.indTotField;
        }
        set
        {
            this.indTotField = value;
        }
    }

    /// <remarks/>
    public string uTrib
    {
        get
        {
            return this.uTribField;
        }
        set
        {
            this.uTribField = value;
        }
    }

    /// <remarks/>
    public string NCM
    {
        get
        {
            return this.nCMField;
        }
        set
        {
            this.nCMField = value;
        }
    }

    /// <remarks/>
    public string uCom
    {
        get
        {
            return this.uComField;
        }
        set
        {
            this.uComField = value;
        }
    }

    /// <remarks/>
    public string CFOP
    {
        get
        {
            return this.cFOPField;
        }
        set
        {
            this.cFOPField = value;
        }
    }

    /// <remarks/>
    public string CEST
    {
        get
        {
            return this.cESTField;
        }
        set
        {
            this.cESTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImposto
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoICMS iCMSField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoIPI iPIField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoCOFINS cOFINSField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoPIS pISField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoICMS ICMS
    {
        get
        {
            return this.iCMSField;
        }
        set
        {
            this.iCMSField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoIPI IPI
    {
        get
        {
            return this.iPIField;
        }
        set
        {
            this.iPIField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoCOFINS COFINS
    {
        get
        {
            return this.cOFINSField;
        }
        set
        {
            this.cOFINSField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoPIS PIS
    {
        get
        {
            return this.pISField;
        }
        set
        {
            this.pISField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoICMS
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoICMSICMS40 iCMS40Field;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoICMSICMS40 ICMS40
    {
        get
        {
            return this.iCMS40Field;
        }
        set
        {
            this.iCMS40Field = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoICMSICMS40
{

    private string motDesICMSField;

    private string origField;

    private string cSTField;

    private string vICMSDesonField;

    /// <remarks/>
    public string motDesICMS
    {
        get
        {
            return this.motDesICMSField;
        }
        set
        {
            this.motDesICMSField = value;
        }
    }

    /// <remarks/>
    public string orig
    {
        get
        {
            return this.origField;
        }
        set
        {
            this.origField = value;
        }
    }

    /// <remarks/>
    public string CST
    {
        get
        {
            return this.cSTField;
        }
        set
        {
            this.cSTField = value;
        }
    }

    /// <remarks/>
    public string vICMSDeson
    {
        get
        {
            return this.vICMSDesonField;
        }
        set
        {
            this.vICMSDesonField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoIPI
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoIPIIPINT iPINTField;

    private string cEnqField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoIPIIPINT IPINT
    {
        get
        {
            return this.iPINTField;
        }
        set
        {
            this.iPINTField = value;
        }
    }

    /// <remarks/>
    public string cEnq
    {
        get
        {
            return this.cEnqField;
        }
        set
        {
            this.cEnqField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoIPIIPINT
{

    private string cSTField;

    /// <remarks/>
    public string CST
    {
        get
        {
            return this.cSTField;
        }
        set
        {
            this.cSTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoCOFINS
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoCOFINSCOFINSNT cOFINSNTField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoCOFINSCOFINSNT COFINSNT
    {
        get
        {
            return this.cOFINSNTField;
        }
        set
        {
            this.cOFINSNTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoCOFINSCOFINSNT
{

    private string cSTField;

    /// <remarks/>
    public string CST
    {
        get
        {
            return this.cSTField;
        }
        set
        {
            this.cSTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoPIS
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoPISPISNT pISNTField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoPISPISNT PISNT
    {
        get
        {
            return this.pISNTField;
        }
        set
        {
            this.pISNTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDetImpostoPISPISNT
{

    private string cSTField;

    /// <remarks/>
    public string CST
    {
        get
        {
            return this.cSTField;
        }
        set
        {
            this.cSTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeTotal
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeTotalICMSTot iCMSTotField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeTotalICMSTot ICMSTot
    {
        get
        {
            return this.iCMSTotField;
        }
        set
        {
            this.iCMSTotField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeTotalICMSTot
{

    private string vICMSUFDestField;

    private string vICMSUFRemetField;

    private string vCOFINSField;

    private string vBCSTField;

    private string vICMSDesonField;

    private string vFCPUFDestField;

    private string vProdField;

    private string vSegField;

    private string vFCPField;

    private string vFCPSTField;

    private string vNFField;

    private string vPISField;

    private string vIPIDevolField;

    private string vBCField;

    private string vSTField;

    private string vICMSField;

    private string vIIField;

    private string vFCPSTRetField;

    private string vDescField;

    private string vOutroField;

    private string vIPIField;

    private string vFreteField;

    /// <remarks/>
    public string vICMSUFDest
    {
        get
        {
            return this.vICMSUFDestField;
        }
        set
        {
            this.vICMSUFDestField = value;
        }
    }

    /// <remarks/>
    public string vICMSUFRemet
    {
        get
        {
            return this.vICMSUFRemetField;
        }
        set
        {
            this.vICMSUFRemetField = value;
        }
    }

    /// <remarks/>
    public string vCOFINS
    {
        get
        {
            return this.vCOFINSField;
        }
        set
        {
            this.vCOFINSField = value;
        }
    }

    /// <remarks/>
    public string vBCST
    {
        get
        {
            return this.vBCSTField;
        }
        set
        {
            this.vBCSTField = value;
        }
    }

    /// <remarks/>
    public string vICMSDeson
    {
        get
        {
            return this.vICMSDesonField;
        }
        set
        {
            this.vICMSDesonField = value;
        }
    }

    /// <remarks/>
    public string vFCPUFDest
    {
        get
        {
            return this.vFCPUFDestField;
        }
        set
        {
            this.vFCPUFDestField = value;
        }
    }

    /// <remarks/>
    public string vProd
    {
        get
        {
            return this.vProdField;
        }
        set
        {
            this.vProdField = value;
        }
    }

    /// <remarks/>
    public string vSeg
    {
        get
        {
            return this.vSegField;
        }
        set
        {
            this.vSegField = value;
        }
    }

    /// <remarks/>
    public string vFCP
    {
        get
        {
            return this.vFCPField;
        }
        set
        {
            this.vFCPField = value;
        }
    }

    /// <remarks/>
    public string vFCPST
    {
        get
        {
            return this.vFCPSTField;
        }
        set
        {
            this.vFCPSTField = value;
        }
    }

    /// <remarks/>
    public string vNF
    {
        get
        {
            return this.vNFField;
        }
        set
        {
            this.vNFField = value;
        }
    }

    /// <remarks/>
    public string vPIS
    {
        get
        {
            return this.vPISField;
        }
        set
        {
            this.vPISField = value;
        }
    }

    /// <remarks/>
    public string vIPIDevol
    {
        get
        {
            return this.vIPIDevolField;
        }
        set
        {
            this.vIPIDevolField = value;
        }
    }

    /// <remarks/>
    public string vBC
    {
        get
        {
            return this.vBCField;
        }
        set
        {
            this.vBCField = value;
        }
    }

    /// <remarks/>
    public string vST
    {
        get
        {
            return this.vSTField;
        }
        set
        {
            this.vSTField = value;
        }
    }

    /// <remarks/>
    public string vICMS
    {
        get
        {
            return this.vICMSField;
        }
        set
        {
            this.vICMSField = value;
        }
    }

    /// <remarks/>
    public string vII
    {
        get
        {
            return this.vIIField;
        }
        set
        {
            this.vIIField = value;
        }
    }

    /// <remarks/>
    public string vFCPSTRet
    {
        get
        {
            return this.vFCPSTRetField;
        }
        set
        {
            this.vFCPSTRetField = value;
        }
    }

    /// <remarks/>
    public string vDesc
    {
        get
        {
            return this.vDescField;
        }
        set
        {
            this.vDescField = value;
        }
    }

    /// <remarks/>
    public string vOutro
    {
        get
        {
            return this.vOutroField;
        }
        set
        {
            this.vOutroField = value;
        }
    }

    /// <remarks/>
    public string vIPI
    {
        get
        {
            return this.vIPIField;
        }
        set
        {
            this.vIPIField = value;
        }
    }

    /// <remarks/>
    public string vFrete
    {
        get
        {
            return this.vFreteField;
        }
        set
        {
            this.vFreteField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeCobr
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeCobrFat fatField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeCobrFat fat
    {
        get
        {
            return this.fatField;
        }
        set
        {
            this.fatField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeCobrFat
{

    private string vOrigField;

    private string nFatField;

    private string vDescField;

    private string vLiqField;

    /// <remarks/>
    public string vOrig
    {
        get
        {
            return this.vOrigField;
        }
        set
        {
            this.vOrigField = value;
        }
    }

    /// <remarks/>
    public string nFat
    {
        get
        {
            return this.nFatField;
        }
        set
        {
            this.nFatField = value;
        }
    }

    /// <remarks/>
    public string vDesc
    {
        get
        {
            return this.vDescField;
        }
        set
        {
            this.vDescField = value;
        }
    }

    /// <remarks/>
    public string vLiq
    {
        get
        {
            return this.vLiqField;
        }
        set
        {
            this.vLiqField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFePag
{

    private string vTrocoField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFePagDetPag detPagField;

    /// <remarks/>
    public string vTroco
    {
        get
        {
            return this.vTrocoField;
        }
        set
        {
            this.vTrocoField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFePagDetPag detPag
    {
        get
        {
            return this.detPagField;
        }
        set
        {
            this.detPagField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFePagDetPag
{

    private string vPagField;

    private string tPagField;

    private string indPagField;

    /// <remarks/>
    public string vPag
    {
        get
        {
            return this.vPagField;
        }
        set
        {
            this.vPagField = value;
        }
    }

    /// <remarks/>
    public string tPag
    {
        get
        {
            return this.tPagField;
        }
        set
        {
            this.tPagField = value;
        }
    }

    /// <remarks/>
    public string indPag
    {
        get
        {
            return this.indPagField;
        }
        set
        {
            this.indPagField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeIde
{

    private string tpNFField;

    private string modField;

    private string indPresField;

    private string tpImpField;

    private string nNFField;

    private string cMunFGField;

    private string procEmiField;

    private string finNFeField;

    private System.DateTime dhEmiField;

    private string tpAmbField;

    private string indFinalField;

    private System.DateTime dhSaiEntField;

    private string idDestField;

    private string tpEmisField;

    private string cDVField;

    private string cUFField;

    private string serieField;

    private string natOpField;

    private string cNFField;

    private string verProcField;

    /// <remarks/>
    public string tpNF
    {
        get
        {
            return this.tpNFField;
        }
        set
        {
            this.tpNFField = value;
        }
    }

    /// <remarks/>
    public string mod
    {
        get
        {
            return this.modField;
        }
        set
        {
            this.modField = value;
        }
    }

    /// <remarks/>
    public string indPres
    {
        get
        {
            return this.indPresField;
        }
        set
        {
            this.indPresField = value;
        }
    }

    /// <remarks/>
    public string tpImp
    {
        get
        {
            return this.tpImpField;
        }
        set
        {
            this.tpImpField = value;
        }
    }

    /// <remarks/>
    public string nNF
    {
        get
        {
            return this.nNFField;
        }
        set
        {
            this.nNFField = value;
        }
    }

    /// <remarks/>
    public string cMunFG
    {
        get
        {
            return this.cMunFGField;
        }
        set
        {
            this.cMunFGField = value;
        }
    }

    /// <remarks/>
    public string procEmi
    {
        get
        {
            return this.procEmiField;
        }
        set
        {
            this.procEmiField = value;
        }
    }

    /// <remarks/>
    public string finNFe
    {
        get
        {
            return this.finNFeField;
        }
        set
        {
            this.finNFeField = value;
        }
    }

    /// <remarks/>
    public System.DateTime dhEmi
    {
        get
        {
            return this.dhEmiField;
        }
        set
        {
            this.dhEmiField = value;
        }
    }

    /// <remarks/>
    public string tpAmb
    {
        get
        {
            return this.tpAmbField;
        }
        set
        {
            this.tpAmbField = value;
        }
    }

    /// <remarks/>
    public string indFinal
    {
        get
        {
            return this.indFinalField;
        }
        set
        {
            this.indFinalField = value;
        }
    }

    /// <remarks/>
    public System.DateTime dhSaiEnt
    {
        get
        {
            return this.dhSaiEntField;
        }
        set
        {
            this.dhSaiEntField = value;
        }
    }

    /// <remarks/>
    public string idDest
    {
        get
        {
            return this.idDestField;
        }
        set
        {
            this.idDestField = value;
        }
    }

    /// <remarks/>
    public string tpEmis
    {
        get
        {
            return this.tpEmisField;
        }
        set
        {
            this.tpEmisField = value;
        }
    }

    /// <remarks/>
    public string cDV
    {
        get
        {
            return this.cDVField;
        }
        set
        {
            this.cDVField = value;
        }
    }

    /// <remarks/>
    public string cUF
    {
        get
        {
            return this.cUFField;
        }
        set
        {
            this.cUFField = value;
        }
    }

    /// <remarks/>
    public string serie
    {
        get
        {
            return this.serieField;
        }
        set
        {
            this.serieField = value;
        }
    }

    /// <remarks/>
    public string natOp
    {
        get
        {
            return this.natOpField;
        }
        set
        {
            this.natOpField = value;
        }
    }

    /// <remarks/>
    public string cNF
    {
        get
        {
            return this.cNFField;
        }
        set
        {
            this.cNFField = value;
        }
    }

    /// <remarks/>
    public string verProc
    {
        get
        {
            return this.verProcField;
        }
        set
        {
            this.verProcField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeEmit
{

    private string cNAEField;

    private string xNomeField;

    private string imField;

    private string cRTField;

    private string xFantField;

    private string cNPJField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeEmitEnderEmit enderEmitField;

    private string ieField;

    /// <remarks/>
    public string CNAE
    {
        get
        {
            return this.cNAEField;
        }
        set
        {
            this.cNAEField = value;
        }
    }

    /// <remarks/>
    public string xNome
    {
        get
        {
            return this.xNomeField;
        }
        set
        {
            this.xNomeField = value;
        }
    }

    /// <remarks/>
    public string IM
    {
        get
        {
            return this.imField;
        }
        set
        {
            this.imField = value;
        }
    }

    /// <remarks/>
    public string CRT
    {
        get
        {
            return this.cRTField;
        }
        set
        {
            this.cRTField = value;
        }
    }

    /// <remarks/>
    public string xFant
    {
        get
        {
            return this.xFantField;
        }
        set
        {
            this.xFantField = value;
        }
    }

    /// <remarks/>
    public string CNPJ
    {
        get
        {
            return this.cNPJField;
        }
        set
        {
            this.cNPJField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeEmitEnderEmit enderEmit
    {
        get
        {
            return this.enderEmitField;
        }
        set
        {
            this.enderEmitField = value;
        }
    }

    /// <remarks/>
    public string IE
    {
        get
        {
            return this.ieField;
        }
        set
        {
            this.ieField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeEmitEnderEmit
{

    private string xCplField;

    private string foneField;

    private string ufField;

    private string xPaisField;

    private string cPaisField;

    private string xLgrField;

    private string xMunField;

    private string nroField;

    private string cMunField;

    private string xBairroField;

    private string cEPField;

    /// <remarks/>
    public string xCpl
    {
        get
        {
            return this.xCplField;
        }
        set
        {
            this.xCplField = value;
        }
    }

    /// <remarks/>
    public string fone
    {
        get
        {
            return this.foneField;
        }
        set
        {
            this.foneField = value;
        }
    }

    /// <remarks/>
    public string UF
    {
        get
        {
            return this.ufField;
        }
        set
        {
            this.ufField = value;
        }
    }

    /// <remarks/>
    public string xPais
    {
        get
        {
            return this.xPaisField;
        }
        set
        {
            this.xPaisField = value;
        }
    }

    /// <remarks/>
    public string cPais
    {
        get
        {
            return this.cPaisField;
        }
        set
        {
            this.cPaisField = value;
        }
    }

    /// <remarks/>
    public string xLgr
    {
        get
        {
            return this.xLgrField;
        }
        set
        {
            this.xLgrField = value;
        }
    }

    /// <remarks/>
    public string xMun
    {
        get
        {
            return this.xMunField;
        }
        set
        {
            this.xMunField = value;
        }
    }

    /// <remarks/>
    public string nro
    {
        get
        {
            return this.nroField;
        }
        set
        {
            this.nroField = value;
        }
    }

    /// <remarks/>
    public string cMun
    {
        get
        {
            return this.cMunField;
        }
        set
        {
            this.cMunField = value;
        }
    }

    /// <remarks/>
    public string xBairro
    {
        get
        {
            return this.xBairroField;
        }
        set
        {
            this.xBairroField = value;
        }
    }

    /// <remarks/>
    public string CEP
    {
        get
        {
            return this.cEPField;
        }
        set
        {
            this.cEPField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDest
{

    private string xNomeField;

    private string iSUFField;

    private string cNPJField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDestEnderDest enderDestField;

    private string ieField;

    private string indIEDestField;

    private string emailField;

    /// <remarks/>
    public string xNome
    {
        get
        {
            return this.xNomeField;
        }
        set
        {
            this.xNomeField = value;
        }
    }

    /// <remarks/>
    public string ISUF
    {
        get
        {
            return this.iSUFField;
        }
        set
        {
            this.iSUFField = value;
        }
    }

    /// <remarks/>
    public string CNPJ
    {
        get
        {
            return this.cNPJField;
        }
        set
        {
            this.cNPJField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDestEnderDest enderDest
    {
        get
        {
            return this.enderDestField;
        }
        set
        {
            this.enderDestField = value;
        }
    }

    /// <remarks/>
    public string IE
    {
        get
        {
            return this.ieField;
        }
        set
        {
            this.ieField = value;
        }
    }

    /// <remarks/>
    public string indIEDest
    {
        get
        {
            return this.indIEDestField;
        }
        set
        {
            this.indIEDestField = value;
        }
    }

    /// <remarks/>
    public string email
    {
        get
        {
            return this.emailField;
        }
        set
        {
            this.emailField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeDestEnderDest
{

    private string foneField;

    private string ufField;

    private string xPaisField;

    private string cPaisField;

    private string xLgrField;

    private string xMunField;

    private string nroField;

    private string cMunField;

    private string xBairroField;

    private string cEPField;

    /// <remarks/>
    public string fone
    {
        get
        {
            return this.foneField;
        }
        set
        {
            this.foneField = value;
        }
    }

    /// <remarks/>
    public string UF
    {
        get
        {
            return this.ufField;
        }
        set
        {
            this.ufField = value;
        }
    }

    /// <remarks/>
    public string xPais
    {
        get
        {
            return this.xPaisField;
        }
        set
        {
            this.xPaisField = value;
        }
    }

    /// <remarks/>
    public string cPais
    {
        get
        {
            return this.cPaisField;
        }
        set
        {
            this.cPaisField = value;
        }
    }

    /// <remarks/>
    public string xLgr
    {
        get
        {
            return this.xLgrField;
        }
        set
        {
            this.xLgrField = value;
        }
    }

    /// <remarks/>
    public string xMun
    {
        get
        {
            return this.xMunField;
        }
        set
        {
            this.xMunField = value;
        }
    }

    /// <remarks/>
    public string nro
    {
        get
        {
            return this.nroField;
        }
        set
        {
            this.nroField = value;
        }
    }

    /// <remarks/>
    public string cMun
    {
        get
        {
            return this.cMunField;
        }
        set
        {
            this.cMunField = value;
        }
    }

    /// <remarks/>
    public string xBairro
    {
        get
        {
            return this.xBairroField;
        }
        set
        {
            this.xBairroField = value;
        }
    }

    /// <remarks/>
    public string CEP
    {
        get
        {
            return this.cEPField;
        }
        set
        {
            this.cEPField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeTransp
{

    private string modFreteField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeTranspVol volField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeTranspTransporta transportaField;

    /// <remarks/>
    public string modFrete
    {
        get
        {
            return this.modFreteField;
        }
        set
        {
            this.modFreteField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeTranspVol vol
    {
        get
        {
            return this.volField;
        }
        set
        {
            this.volField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeTranspTransporta transporta
    {
        get
        {
            return this.transportaField;
        }
        set
        {
            this.transportaField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeTranspVol
{

    private string pesoLField;

    private string espField;

    private string qVolField;

    private string pesoBField;

    /// <remarks/>
    public string pesoL
    {
        get
        {
            return this.pesoLField;
        }
        set
        {
            this.pesoLField = value;
        }
    }

    /// <remarks/>
    public string esp
    {
        get
        {
            return this.espField;
        }
        set
        {
            this.espField = value;
        }
    }

    /// <remarks/>
    public string qVol
    {
        get
        {
            return this.qVolField;
        }
        set
        {
            this.qVolField = value;
        }
    }

    /// <remarks/>
    public string pesoB
    {
        get
        {
            return this.pesoBField;
        }
        set
        {
            this.pesoBField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogNfeProcNFeInfNFeTranspTransporta
{

    private string xNomeField;

    private string ufField;

    private string xEnderField;

    private string xMunField;

    private string cNPJField;

    private string ieField;

    /// <remarks/>
    public string xNome
    {
        get
        {
            return this.xNomeField;
        }
        set
        {
            this.xNomeField = value;
        }
    }

    /// <remarks/>
    public string UF
    {
        get
        {
            return this.ufField;
        }
        set
        {
            this.ufField = value;
        }
    }

    /// <remarks/>
    public string xEnder
    {
        get
        {
            return this.xEnderField;
        }
        set
        {
            this.xEnderField = value;
        }
    }

    /// <remarks/>
    public string xMun
    {
        get
        {
            return this.xMunField;
        }
        set
        {
            this.xMunField = value;
        }
    }

    /// <remarks/>
    public string CNPJ
    {
        get
        {
            return this.cNPJField;
        }
        set
        {
            this.cNPJField = value;
        }
    }

    /// <remarks/>
    public string IE
    {
        get
        {
            return this.ieField;
        }
        set
        {
            this.ieField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFe
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeEvento eventoField;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeRetEvento retEventoField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeEvento evento
    {
        get
        {
            return this.eventoField;
        }
        set
        {
            this.eventoField = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeRetEvento retEvento
    {
        get
        {
            return this.retEventoField;
        }
        set
        {
            this.retEventoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeEvento
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeEventoInfEvento infEventoField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeEventoInfEvento infEvento
    {
        get
        {
            return this.infEventoField;
        }
        set
        {
            this.infEventoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeEventoInfEvento
{

    private string cOrgaoField;

    private System.DateTime dhEventoField;

    private string nSeqEventoField;

    private bool nSeqEventoFieldSpecified;

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeEventoInfEventoDetEvento detEventoField;

    private string cNPJField;

    private bool cNPJFieldSpecified;

    private string idField;

    private string chNFeField;

    private string tpAmbField;

    private bool tpAmbFieldSpecified;

    private string verEventoField;

    private bool verEventoFieldSpecified;

    private string tpEventoField;

    /// <remarks/>
    public string cOrgao
    {
        get
        {
            return this.cOrgaoField;
        }
        set
        {
            this.cOrgaoField = value;
        }
    }

    /// <remarks/>
    public System.DateTime dhEvento
    {
        get
        {
            return this.dhEventoField;
        }
        set
        {
            this.dhEventoField = value;
        }
    }

    /// <remarks/>
    public string nSeqEvento
    {
        get
        {
            return this.nSeqEventoField;
        }
        set
        {
            this.nSeqEventoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool nSeqEventoSpecified
    {
        get
        {
            return this.nSeqEventoFieldSpecified;
        }
        set
        {
            this.nSeqEventoFieldSpecified = value;
        }
    }

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeEventoInfEventoDetEvento detEvento
    {
        get
        {
            return this.detEventoField;
        }
        set
        {
            this.detEventoField = value;
        }
    }

    /// <remarks/>
    public string CNPJ
    {
        get
        {
            return this.cNPJField;
        }
        set
        {
            this.cNPJField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool CNPJSpecified
    {
        get
        {
            return this.cNPJFieldSpecified;
        }
        set
        {
            this.cNPJFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string Id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
    public string chNFe
    {
        get
        {
            return this.chNFeField;
        }
        set
        {
            this.chNFeField = value;
        }
    }

    /// <remarks/>
    public string tpAmb
    {
        get
        {
            return this.tpAmbField;
        }
        set
        {
            this.tpAmbField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool tpAmbSpecified
    {
        get
        {
            return this.tpAmbFieldSpecified;
        }
        set
        {
            this.tpAmbFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string verEvento
    {
        get
        {
            return this.verEventoField;
        }
        set
        {
            this.verEventoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool verEventoSpecified
    {
        get
        {
            return this.verEventoFieldSpecified;
        }
        set
        {
            this.verEventoFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string tpEvento
    {
        get
        {
            return this.tpEventoField;
        }
        set
        {
            this.tpEventoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeEventoInfEventoDetEvento
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeEventoInfEventoDetEventoItensAverbados[] itensAverbadosField;

    private string verAplicField;

    private bool verAplicFieldSpecified;

    private string descEventoField;

    private string tpAutorField;

    private bool tpAutorFieldSpecified;

    private string versaoField;

    private bool versaoFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("itensAverbados")]
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeEventoInfEventoDetEventoItensAverbados[] itensAverbados
    {
        get
        {
            return this.itensAverbadosField;
        }
        set
        {
            this.itensAverbadosField = value;
        }
    }

    /// <remarks/>
    public string verAplic
    {
        get
        {
            return this.verAplicField;
        }
        set
        {
            this.verAplicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool verAplicSpecified
    {
        get
        {
            return this.verAplicFieldSpecified;
        }
        set
        {
            this.verAplicFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string descEvento
    {
        get
        {
            return this.descEventoField;
        }
        set
        {
            this.descEventoField = value;
        }
    }

    /// <remarks/>
    public string tpAutor
    {
        get
        {
            return this.tpAutorField;
        }
        set
        {
            this.tpAutorField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool tpAutorSpecified
    {
        get
        {
            return this.tpAutorFieldSpecified;
        }
        set
        {
            this.tpAutorFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string versao
    {
        get
        {
            return this.versaoField;
        }
        set
        {
            this.versaoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool versaoSpecified
    {
        get
        {
            return this.versaoFieldSpecified;
        }
        set
        {
            this.versaoFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeEventoInfEventoDetEventoItensAverbados
{

    private string nItemField;

    private string nDueField;

    private System.String dhEmbarqueField;

    private string motAlteracaoField;

    private System.String dhAverbacaoField;

    private string nItemDueField;

    private string qItemField;

    /// <remarks/>
    public string nItem
    {
        get
        {
            return this.nItemField;
        }
        set
        {
            this.nItemField = value;
        }
    }

    /// <remarks/>
    public string nDue
    {
        get
        {
            return this.nDueField;
        }
        set
        {
            this.nDueField = value;
        }
    }

    /// <remarks/>
    public System.String dhEmbarque
    {
        get
        {
            return this.dhEmbarqueField;
        }
        set
        {
            this.dhEmbarqueField = value;
        }
    }

    /// <remarks/>
    public string motAlteracao
    {
        get
        {
            return this.motAlteracaoField;
        }
        set
        {
            this.motAlteracaoField = value;
        }
    }

    /// <remarks/>
    public System.String dhAverbacao
    {
        get
        {
            return this.dhAverbacaoField;
        }
        set
        {
            this.dhAverbacaoField = value;
        }
    }

    /// <remarks/>
    public string nItemDue
    {
        get
        {
            return this.nItemDueField;
        }
        set
        {
            this.nItemDueField = value;
        }
    }

    /// <remarks/>
    public string qItem
    {
        get
        {
            return this.qItemField;
        }
        set
        {
            this.qItemField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeRetEvento
{

    private nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeRetEventoInfEvento infEventoField;

    /// <remarks/>
    public nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeRetEventoInfEvento infEvento
    {
        get
        {
            return this.infEventoField;
        }
        set
        {
            this.infEventoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nfeConsultaNFeLogResultRetConsNFeLogNFeLogProcEventoNFeRetEventoInfEvento
{

    private string cOrgaoField;

    private string nProtField;

    private string nSeqEventoField;

    private bool nSeqEventoFieldSpecified;

    private string verAplicField;

    private System.DateTime dhRegEventoField;

    private string idField;

    private string xMotivoField;

    private string chNFeField;

    private string tpAmbField;

    private bool tpAmbFieldSpecified;

    private string cNPJDestField;

    private bool cNPJDestFieldSpecified;

    private string cStatField;

    private bool cStatFieldSpecified;

    private string tpEventoField;

    /// <remarks/>
    public string cOrgao
    {
        get
        {
            return this.cOrgaoField;
        }
        set
        {
            this.cOrgaoField = value;
        }
    }

    /// <remarks/>
    public string nProt
    {
        get
        {
            return this.nProtField;
        }
        set
        {
            this.nProtField = value;
        }
    }

    /// <remarks/>
    public string nSeqEvento
    {
        get
        {
            return this.nSeqEventoField;
        }
        set
        {
            this.nSeqEventoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool nSeqEventoSpecified
    {
        get
        {
            return this.nSeqEventoFieldSpecified;
        }
        set
        {
            this.nSeqEventoFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string verAplic
    {
        get
        {
            return this.verAplicField;
        }
        set
        {
            this.verAplicField = value;
        }
    }

    /// <remarks/>
    public System.DateTime dhRegEvento
    {
        get
        {
            return this.dhRegEventoField;
        }
        set
        {
            this.dhRegEventoField = value;
        }
    }

    /// <remarks/>
    public string Id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public string xMotivo
    {
        get
        {
            return this.xMotivoField;
        }
        set
        {
            this.xMotivoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
    public string chNFe
    {
        get
        {
            return this.chNFeField;
        }
        set
        {
            this.chNFeField = value;
        }
    }

    /// <remarks/>
    public string tpAmb
    {
        get
        {
            return this.tpAmbField;
        }
        set
        {
            this.tpAmbField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool tpAmbSpecified
    {
        get
        {
            return this.tpAmbFieldSpecified;
        }
        set
        {
            this.tpAmbFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string CNPJDest
    {
        get
        {
            return this.cNPJDestField;
        }
        set
        {
            this.cNPJDestField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool CNPJDestSpecified
    {
        get
        {
            return this.cNPJDestFieldSpecified;
        }
        set
        {
            this.cNPJDestFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string cStat
    {
        get
        {
            return this.cStatField;
        }
        set
        {
            this.cStatField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool cStatSpecified
    {
        get
        {
            return this.cStatFieldSpecified;
        }
        set
        {
            this.cStatFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string tpEvento
    {
        get
        {
            return this.tpEventoField;
        }
        set
        {
            this.tpEventoField = value;
        }
    }
}

