namespace NFSeToXLSXConverterMacOs.Domain;

public class CamposUtilizadosDTO
{
    public bool IndicaAppConfigurado { get; set; } = false;// Indica se o app foi configurado ou está utilizando valor default
    public bool NumeroNfse { get; set; }
    public bool Situacao { get; set; }
    public bool CodigoVerificacao { get; set; }
    public bool DtEmissao { get; set; }
    public bool CnpjPrestador { get; set; }
    public bool InscricaoMunicipalPrestador { get; set; }
    public bool RazaoSocialPrestador { get; set; }
    public bool CpfCnpjTomador { get; set; }
    public bool InscricaoMunicipalTomador { get; set; }
    public bool RazaoSocialTomador { get; set; }
    public bool EnderecoTomador { get; set; }
    public bool NumeroEnderecoTomador { get; set; }
    public bool ComplementoEnderecoTomador { get; set; }
    public bool BairroTomador { get; set; }
    public bool CodigoMunicipioTomador { get; set; }
    public bool UfTomador { get; set; }
    public bool CodigoPaisTomador { get; set; }
    public bool CepTomador { get; set; }
    public bool TelefoneTomador { get; set; }
    public bool EmailTomador { get; set; }
    public bool RPS { get; set; }
    public bool NumeroRps { get; set; }
    public bool Competencia { get; set; }
    public bool ValorServicos { get; set; }
    public bool ValorBaseCalculo { get; set; }
    public bool Aliquota { get; set; }
    public bool ValorIss { get; set; }
    public bool ValorLiquidoNfse { get; set; }
    public bool ValorDeducoes { get; set; }
    public bool ValorPis { get; set; }
    public bool ValorCofins { get; set; }
    public bool ValorInss { get; set; }
    public bool ValorIr { get; set; }
    public bool ValorCsll { get; set; }
    public bool OutrasRetencoes { get; set; }
    public bool DescontoCondicionado { get; set; }
    public bool DescontoIncondicionado { get; set; }
    public bool IssRetido { get; set; }
    public bool OutrasInformacoes { get; set; }
    public bool Atividade { get; set; }
    public bool CNAE { get; set; }
    public bool CodigoTributacaoMunicipio { get; set; }
    public bool Discriminacao { get; set; }
    public bool MunicipioPrestacao { get; set; }
    public bool ExigibilidadeIss { get; set; }
    public bool MunicipioIncidencia { get; set; }
}