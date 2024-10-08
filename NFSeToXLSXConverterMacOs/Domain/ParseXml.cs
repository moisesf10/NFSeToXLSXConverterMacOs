﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NFSeToXLSXConverterMacOs.Domain
{
    public static class ParseXml
    {
        public static Dictionary<string, string?> parse(string content)
        {


            Dictionary<string, string?> dict = new Dictionary<string, string?>();

            XmlDocument dom = new XmlDocument();
            dom.LoadXml(content);

            XmlElement nfse = dom?.GetElementsByTagName("Nfse")?.Count > 0 ? (XmlElement?)dom.GetElementsByTagName("Nfse").Item(0) : null;
            string versaoNota = "2.02";

            if (nfse != null && nfse.HasAttribute("versao") )
            {
                versaoNota =  nfse.GetAttribute("versao");
            }

            switch (versaoNota)
            {
                case "1.00":
                    //XmlElement? infNfse = nfse?.GetElementsByTagName("InfNfse")?.Count > 0 ? (XmlElement?)nfse.GetElementsByTagName("InfNfse").Item(0) : null;
                    parseV100(dict, dom);
                    break;
                case "2.02":
                   // infNfse = nfse?.GetElementsByTagName("InfNfse")?.Count > 0 ? (XmlElement?)nfse.GetElementsByTagName("InfNfse").Item(0) : null;
                    parseV202(dict, dom);
                    break;

            }
            
            
            

            return dict;
        }



        private static Dictionary<string, string?> parseV202(Dictionary<string, string?> dict, XmlDocument dom)
        {
            XmlElement? infNfse = dom?.GetElementsByTagName("InfNfse")?.Count > 0 ? (XmlElement?)dom.GetElementsByTagName("InfNfse").Item(0) : null;

            if (infNfse != null && infNfse.ChildNodes.Count > 0)
            {
                var statusNfse = dom?.GetElementsByTagName("NfseCancelamento")?.Count > 0
                        ? "Cancelada" : "Normal";
                dict.Add("Situacao", statusNfse);

                var numeroNfse = infNfse?.GetElementsByTagName("Numero")?.Count > 0
                        ? infNfse.GetElementsByTagName("Numero").Item(0).InnerText : null;
                dict.Add("NumeroNfse", numeroNfse);

                var codigoVerificacao = infNfse?.GetElementsByTagName("CodigoVerificacao")?.Count > 0
                    ? infNfse.GetElementsByTagName("CodigoVerificacao").Item(0).InnerText : null;
                dict.Add("CodigoVerificacao", codigoVerificacao);

                var dtEmissao = infNfse?.GetElementsByTagName("DataEmissao")?.Count > 0
                    ? infNfse.GetElementsByTagName("DataEmissao").Item(0).InnerText : null;
                dict.Add("DtEmissao", dtEmissao);

                if (!String.IsNullOrEmpty(dtEmissao))
                {
                    var dataEmissaoNfse = DateTime.Parse(dtEmissao, System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
                    if (!dict.ContainsKey("DtEmissao"))
                    {
                        dict.Add("DtEmissao", dataEmissaoNfse.ToString("dd/MM/yyyy HH:mm:ss"));
                    }
                    else
                    {
                        dict["DtEmissao"] = dataEmissaoNfse.ToString("dd/MM/yyyy HH:mm:ss");
                    }

                }

                



                

                XmlElement? prestadorServico = infNfse?.GetElementsByTagName("PrestadorServico")?.Count > 0
                    ? (XmlElement?)infNfse.GetElementsByTagName("PrestadorServico").Item(0) : null;

                if (prestadorServico != null)
                {
                    var cnpjPrestador = prestadorServico?.GetElementsByTagName("Cpf")?.Count > 0
                    ? prestadorServico.GetElementsByTagName("Cpf").Item(0).InnerText : null;
                    dict.Add("CnpjPrestador", cnpjPrestador);

                    if (prestadorServico?.GetElementsByTagName("Cnpj")?.Count > 0)
                    {
                        cnpjPrestador = prestadorServico?.GetElementsByTagName("Cnpj")?.Count > 0
                        ? prestadorServico.GetElementsByTagName("Cnpj").Item(0).InnerText : null;

                        dict["CnpjPrestador"] = cnpjPrestador;
                    }

                    var inscricaoMunicipalPrestador = prestadorServico?.GetElementsByTagName("InscricaoMunicipal")?.Count > 0
                        ? prestadorServico.GetElementsByTagName("InscricaoMunicipal").Item(0).InnerText : null;
                    dict.Add("InscricaoMunicipalPrestador", inscricaoMunicipalPrestador);

                    var razaoSocialPrestador = prestadorServico?.GetElementsByTagName("RazaoSocial")?.Count > 0
                        ? prestadorServico.GetElementsByTagName("RazaoSocial").Item(0).InnerText : null;
                    dict.Add("RazaoSocialPrestador", razaoSocialPrestador);

                    //var nomeFantasiaPrestador = prestadorServico.GetElementsByTagName("NomeFantasia").Count > 0
                    //	? prestadorServico.GetElementsByTagName("NomeFantasia").Item(0).InnerText : null;
                    //dict.Add("NomeFantasiaPrestador", nomeFantasiaPrestador);

                    //var enderecoPrestador = prestadorServico.GetElementsByTagName("Endereco").Count > 0
                    //	? prestadorServico.GetElementsByTagName("Endereco").Item(0).InnerText : null;
                    //dict.Add("EnderecoPrestador", enderecoPrestador);

                    //var numeroEnderecoPrestador = prestadorServico.GetElementsByTagName("Numero").Count > 0
                    //	? prestadorServico.GetElementsByTagName("Numero").Item(0).InnerText : null;
                    //dict.Add("NumeroEnderecoPrestador", numeroEnderecoPrestador);

                    //var bairroPrestador = prestadorServico.GetElementsByTagName("Bairro").Count > 0
                    //	? prestadorServico.GetElementsByTagName("Bairro").Item(0).InnerText : null;
                    //dict.Add("BairroPrestador", bairroPrestador);

                    //var codigoMunicipioPrestador = prestadorServico.GetElementsByTagName("CodigoMunicipio").Count > 0
                    //	? prestadorServico.GetElementsByTagName("CodigoMunicipio").Item(0).InnerText : null;
                    //dict.Add("CodigoMunicipioPrestador", codigoMunicipioPrestador);

                    //var ufPrestador = prestadorServico.GetElementsByTagName("Uf").Count > 0
                    //	? prestadorServico.GetElementsByTagName("Uf").Item(0).InnerText : null;
                    //dict.Add("UfPrestador", ufPrestador);

                    //var codigoPaisPrestador = prestadorServico?.GetElementsByTagName("CodigoPais")?.Count > 0
                    //    ? prestadorServico.GetElementsByTagName("CodigoPais").Item(0).InnerText : null;
                    //dict.Add("CodigoPaisPrestador", codigoPaisPrestador);

                    //var cepPrestador = prestadorServico?.GetElementsByTagName("Cep")?.Count > 0
                    //    ? prestadorServico.GetElementsByTagName("Cep").Item(0).InnerText : null;
                    //dict.Add("CepPrestador", cepPrestador);

                    //var telefonePrestador = prestadorServico?.GetElementsByTagName("Telefone")?.Count > 0
                    //    ? prestadorServico.GetElementsByTagName("Telefone").Item(0).InnerText : null;
                    //dict.Add("TelefonePrestador", telefonePrestador);

                    //var emailPrestador = prestadorServico?.GetElementsByTagName("Email")?.Count > 0
                    //    ? prestadorServico.GetElementsByTagName("Email").Item(0).InnerText : null;
                    //dict.Add("EmailPrestador", emailPrestador);

                }

                // TOMADOR DO SERVIÇO
                XmlElement tomador = infNfse?.GetElementsByTagName("Tomador")?.Count > 0
                        ? (XmlElement)infNfse.GetElementsByTagName("Tomador").Item(0) : null;


                var cpfCnpjTomador = tomador?.GetElementsByTagName("Cpf")?.Count > 0
                ? tomador.GetElementsByTagName("Cpf").Item(0).InnerText : null;
                dict.Add("CpfCnpjTomador", cpfCnpjTomador);

                if (tomador.GetElementsByTagName("Cnpj").Count > 0)
                {
                    cpfCnpjTomador = tomador?.GetElementsByTagName("Cnpj").Item(0).InnerText;
                    dict["CpfCnpjTomador"] = cpfCnpjTomador;
                }

                var inscricaoMunicipalTomador = tomador?.GetElementsByTagName("InscricaoMunicipal")?.Count > 0
                ? tomador.GetElementsByTagName("InscricaoMunicipal").Item(0).InnerText : null;
                dict.Add("InscricaoMunicipalTomador", inscricaoMunicipalTomador);

                var razaoSocialTomador = tomador?.GetElementsByTagName("RazaoSocial")?.Count > 0
                ? tomador.GetElementsByTagName("RazaoSocial").Item(0).InnerText : null;
                dict.Add("RazaoSocialTomador", razaoSocialTomador);

                var enderecoTomador = tomador?.GetElementsByTagName("Endereco")?.Count > 1
                ? tomador.GetElementsByTagName("Endereco").Item(1).InnerText : null;
                dict.Add("EnderecoTomador", enderecoTomador);

                var numeroEnderecoTomador = tomador?.GetElementsByTagName("Numero")?.Count > 0
                ? tomador.GetElementsByTagName("Numero").Item(0).InnerText : null;
                dict.Add("NumeroEnderecoTomador", numeroEnderecoTomador);

                var complementoEnderecoTomador = tomador?.GetElementsByTagName("Complemento")?.Count > 0
                ? tomador.GetElementsByTagName("Complemento").Item(0).InnerText : null;
                dict.Add("ComplementoEnderecoTomador", complementoEnderecoTomador);

                var bairroTomador = tomador?.GetElementsByTagName("Bairro")?.Count > 0
                ? tomador.GetElementsByTagName("Bairro").Item(0).InnerText : null;
                dict.Add("BairroTomador", bairroTomador);

                var codigoMunicipioTomador = tomador?.GetElementsByTagName("CodigoMunicipio")?.Count > 0
                ? tomador.GetElementsByTagName("CodigoMunicipio").Item(0).InnerText : null;
                dict.Add("CodigoMunicipioTomador", codigoMunicipioTomador);

                var ufTomador = tomador?.GetElementsByTagName("Uf")?.Count > 0
                ? tomador.GetElementsByTagName("Uf").Item(0).InnerText : null;
                dict.Add("UfTomador", ufTomador);

                var codigoPaisTomador = tomador?.GetElementsByTagName("CodigoPais")?.Count > 0
                ? tomador.GetElementsByTagName("CodigoPais").Item(0).InnerText : null;
                dict.Add("CodigoPaisTomador", codigoPaisTomador);

                var cepTomador = tomador?.GetElementsByTagName("Cep")?.Count > 0
                ? tomador.GetElementsByTagName("Cep").Item(0).InnerText : null;
                dict.Add("CepTomador", cepTomador);

                var telefoneTomador = tomador?.GetElementsByTagName("Telefone")?.Count > 0
                ? tomador.GetElementsByTagName("Telefone").Item(0).InnerText : null;
                dict.Add("TelefoneTomador", telefoneTomador);

                var emailTomador = tomador?.GetElementsByTagName("Email")?.Count > 0
                ? tomador.GetElementsByTagName("Email").Item(0).InnerText : null;
                dict.Add("EmailTomador", emailTomador);



                XmlElement declaracaoPrestacaoServico = infNfse?.GetElementsByTagName("DeclaracaoPrestacaoServico")?.Count > 0
                        ? (XmlElement)infNfse.GetElementsByTagName("DeclaracaoPrestacaoServico").Item(0) : null;

                if (declaracaoPrestacaoServico != null)
                {
                    XmlElement rps = declaracaoPrestacaoServico?.GetElementsByTagName("Rps")?.Count > 0
                    ? (XmlElement)declaracaoPrestacaoServico.GetElementsByTagName("Rps").Item(0) : null;

                    var numeroRps = rps?.GetElementsByTagName("Numero")?.Count > 0
                        ? rps.GetElementsByTagName("Numero").Item(0).InnerText : "0";
                    dict.Add("RPS", ((String.IsNullOrEmpty(numeroRps) || numeroRps == "0" ) ? "Não" : "Sim") );
                    dict.Add("NumeroRps", numeroRps);



                    if (declaracaoPrestacaoServico?.GetElementsByTagName("Competencia")?.Count > 0)
                    {
                        var competencia = DateTime.ParseExact(declaracaoPrestacaoServico.GetElementsByTagName("Competencia").Item(0).InnerText, "yyyy-MM-dd", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
                        dict.Add("Competencia", competencia.ToString("MM/yyyy"));
                    }


                    XmlElement? servico = declaracaoPrestacaoServico?.GetElementsByTagName("Servico")?.Count > 0
                    ? (XmlElement?)declaracaoPrestacaoServico.GetElementsByTagName("Servico").Item(0) : null;

                    if (servico != null)
                    {

                        XmlElement valoresNfse = infNfse?.GetElementsByTagName("ValoresNfse")?.Count > 0
                        ? (XmlElement)infNfse.GetElementsByTagName("ValoresNfse").Item(0) : null;

                        var baseCalculo = valoresNfse?.GetElementsByTagName("BaseCalculo").Count > 0
                        ? decimal.Parse(valoresNfse.GetElementsByTagName("BaseCalculo").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;

                        dict.Add("ValorBaseCalculo", Convert.ToString(baseCalculo));

                        var aliquota = valoresNfse?.GetElementsByTagName("Aliquota")?.Count > 0
                        ? decimal.Parse(valoresNfse.GetElementsByTagName("Aliquota").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;

                        dict.Add("Aliquota", Convert.ToString(aliquota));

                        var valorIss = valoresNfse?.GetElementsByTagName("ValorIss")?.Count > 0
                        ? decimal.Parse(valoresNfse.GetElementsByTagName("ValorIss").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                        dict.Add("ValorIss", Convert.ToString(valorIss));

                        var valorLiquidoNfse = valoresNfse?.GetElementsByTagName("ValorLiquidoNfse")?.Count > 0
                        ? decimal.Parse(valoresNfse.GetElementsByTagName("ValorLiquidoNfse").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                        dict.Add("ValorLiquidoNfse", Convert.ToString(valorLiquidoNfse));

                        decimal? valorCredito = null;

                        var valorServicos = servico?.GetElementsByTagName("ValorServicos")?.Count > 0
                        ? decimal.Parse(servico.GetElementsByTagName("ValorServicos").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                        dict.Add("ValorServicos", Convert.ToString(valorServicos));

                        var valorDeducoes = servico?.GetElementsByTagName("ValorDeducoes")?.Count > 0
                        ? decimal.Parse(servico.GetElementsByTagName("ValorDeducoes").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                        dict.Add("ValorDeducoes", Convert.ToString(valorDeducoes));

                        var valorPis = servico?.GetElementsByTagName("ValorPis")?.Count > 0
                        ? decimal.Parse(servico.GetElementsByTagName("ValorPis").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                        dict.Add("ValorPis", Convert.ToString(valorPis));

                        var valorCofins = servico?.GetElementsByTagName("ValorCofins")?.Count > 0
                        ? decimal.Parse(servico.GetElementsByTagName("ValorCofins").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                        dict.Add("ValorCofins", Convert.ToString(valorCofins));

                        var valorInss = servico?.GetElementsByTagName("ValorInss")?.Count > 0
                        ? decimal.Parse(servico.GetElementsByTagName("ValorInss").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                        dict.Add("ValorInss", Convert.ToString(valorInss));

                        var valorIr = servico?.GetElementsByTagName("ValorIr")?.Count > 0
                        ? decimal.Parse(servico.GetElementsByTagName("ValorIr").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                        dict.Add("ValorIr", Convert.ToString(valorIr));

                        var valorCsll = servico?.GetElementsByTagName("ValorCsll")?.Count > 0
                        ? decimal.Parse(servico.GetElementsByTagName("ValorCsll").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                        dict.Add("ValorCsll", Convert.ToString(valorCsll));

                        var outrasRetencoes = servico?.GetElementsByTagName("OutrasRetencoes")?.Count > 0
                        ? decimal.Parse(servico.GetElementsByTagName("OutrasRetencoes").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                        dict.Add("OutrasRetencoes", Convert.ToString(outrasRetencoes));

                        if (valorIss == null || valorIss == 0)
                        {
                            valorIss = servico?.GetElementsByTagName("ValorIss")?.Count > 0
                            ? decimal.Parse(servico.GetElementsByTagName("ValorIss").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                            dict.Add("ValorIss", Convert.ToString(valorIss));
                        }

                        if (aliquota == null || aliquota == 0)
                        {
                            aliquota = servico.GetElementsByTagName("Aliquota").Count > 0
                            ? decimal.Parse(servico.GetElementsByTagName("Aliquota").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                            dict.Add("Aliquota", Convert.ToString(aliquota));
                        }

                        var descontoCondicionado = servico?.GetElementsByTagName("DescontoCondicionado")?.Count > 0
                        ? decimal.Parse(servico.GetElementsByTagName("DescontoCondicionado").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                        dict.Add("DescontoCondicionado", Convert.ToString(descontoCondicionado));

                        var descontoIncondicionado = servico?.GetElementsByTagName("DescontoIncondicionado")?.Count > 0
                        ? decimal.Parse(servico.GetElementsByTagName("DescontoIncondicionado").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                        dict.Add("DescontoIncondicionado", Convert.ToString(descontoIncondicionado));


                        var issRetido = servico?.GetElementsByTagName("IssRetido")?.Count > 0
                        ? int.Parse(servico.GetElementsByTagName("IssRetido").Item(0).InnerText) : 0;
                        dict.Add("IssRetido", ((issRetido == 1) ? "Sim" : "Não"));

                        

                        var outrasInformacoes = infNfse?.GetElementsByTagName("OutrasInformacoes")?.Count > 0
                    ? infNfse.GetElementsByTagName("OutrasInformacoes").Item(0).InnerText : null;
                        dict.Add("OutrasInformacoes", outrasInformacoes);

                        var itemListaServico = servico?.GetElementsByTagName("ItemListaServico")?.Count > 0
                        ? servico.GetElementsByTagName("ItemListaServico").Item(0).InnerText : null;
                        dict.Add("Atividade", itemListaServico);

                        var codigoCnae = servico?.GetElementsByTagName("CodigoCnae")?.Count > 0
                        ? servico.GetElementsByTagName("CodigoCnae").Item(0).InnerText : null;
                        dict.Add("CNAE", codigoCnae);

                        var codigoTributacaoMunicipio = servico?.GetElementsByTagName("CodigoTributacaoMunicipio")?.Count > 0
                        ? servico.GetElementsByTagName("CodigoTributacaoMunicipio").Item(0).InnerText : null;
                        dict.Add("CodigoTributacaoMunicipio", codigoTributacaoMunicipio);

                        var discriminacao = servico?.GetElementsByTagName("Discriminacao")?.Count > 0
                        ? servico.GetElementsByTagName("Discriminacao").Item(0).InnerText : null;
                        dict.Add("Discriminacao", discriminacao);

                        var codigoMunicipioPrestacao = servico?.GetElementsByTagName("CodigoMunicipio")?.Count > 0
                        ? servico.GetElementsByTagName("CodigoMunicipio").Item(0).InnerText : null;
                        dict.Add("MunicipioPrestacao", codigoMunicipioPrestacao);

                        //var codigoPaisPrestacao = servico.GetElementsByTagName("CodigoPais").Count > 0
                        //? servico.GetElementsByTagName("CodigoPais").Item(0).InnerText : null;
                        //dict.Add("CodigoPais", codigoPaisPrestacao);


                        var exigibilidadeIss = servico?.GetElementsByTagName("ExigibilidadeISS")?.Count > 0
                        ? int.Parse(servico.GetElementsByTagName("ExigibilidadeISS").Item(0).InnerText) : 0;
                        dict.Add("ExigibilidadeIss", ExigibilidadeIss.GetValue(exigibilidadeIss));

                        var codigoMunicipioIncidencia = servico?.GetElementsByTagName("MunicipioIncidencia")?.Count > 0
                        ? servico.GetElementsByTagName("MunicipioIncidencia").Item(0).InnerText : null;
                        dict.Add("MunicipioIncidencia", codigoMunicipioIncidencia);

                        //var numeroProcesso = servico.GetElementsByTagName("NumeroProcesso").Count > 0
                        //  ? servico.GetElementsByTagName("NumeroProcesso").Item(0).InnerText : null;
                        //dict.Add("NumeroProcesso", numeroProcesso);


                    }


                }
            }

            return dict;

        }

        private static Dictionary<string, string?> parseV100(Dictionary<string, string?> dict, XmlDocument dom)
        {
            XmlElement? infNfse = dom?.GetElementsByTagName("InfNfse")?.Count > 0 ? (XmlElement?)dom.GetElementsByTagName("InfNfse").Item(0) : null;

            if (infNfse != null && infNfse.ChildNodes.Count > 0)
            {
                var statusNfse = dom?.GetElementsByTagName("NfseCancelamento")?.Count > 0
                        ? "Cancelada" : "Normal";

                dict.Add("Situacao", statusNfse);

                var numeroNfse = infNfse?.GetElementsByTagName("Numero")?.Count > 0
                        ? infNfse.GetElementsByTagName("Numero").Item(0).InnerText : null;
                dict.Add("NumeroNfse", numeroNfse);

                var codigoVerificacao = infNfse?.GetElementsByTagName("CodigoVerificacao")?.Count > 0
                    ? infNfse.GetElementsByTagName("CodigoVerificacao").Item(0).InnerText : null;
                dict.Add("CodigoVerificacao", codigoVerificacao);

                var dtEmissao = infNfse?.GetElementsByTagName("DataEmissao")?.Count > 0
                    ? infNfse.GetElementsByTagName("DataEmissao").Item(0).InnerText : null;
                dict.Add("DtEmissao", dtEmissao);

                if (!String.IsNullOrEmpty(dtEmissao))
                {
                    var dataEmissaoNfse = DateTime.Parse(dtEmissao, System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
                    if (!dict.ContainsKey("DtEmissao"))
                    {
                        dict.Add("DtEmissao", dataEmissaoNfse.ToString("dd/MM/yyyy HH:mm:ss"));
                    }
                    else
                    {
                        dict["DtEmissao"] = dataEmissaoNfse.ToString("dd/MM/yyyy HH:mm:ss");
                    }

                }

                XmlElement rps = infNfse?.GetElementsByTagName("IdentificacaoRps")?.Count > 0
                    ? (XmlElement)infNfse.GetElementsByTagName("IdentificacaoRps").Item(0) : null;

                var numeroRps = rps?.GetElementsByTagName("Numero")?.Count > 0
                    ? rps.GetElementsByTagName("Numero").Item(0).InnerText : "0";
                dict.Add("RPS", ((String.IsNullOrEmpty(numeroRps) || numeroRps == "0") ? "Não" : "Sim"));
                dict.Add("NumeroRps", numeroRps);

                var naturezaOperacao = infNfse?.GetElementsByTagName("NaturezaOperacao")?.Count > 0
                        ? int.Parse(infNfse.GetElementsByTagName("NaturezaOperacao").Item(0).InnerText) : 0;
                dict.Add("NaturezaOperacao", NaturezaOperacao.GetValue(naturezaOperacao));


                if (infNfse?.GetElementsByTagName("Competencia")?.Count > 0)
                {
                    //var comp = 
                    var competencia = DateTime.ParseExact(infNfse.GetElementsByTagName("Competencia").Item(0).InnerText, "yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
                    
                    
                    dict.Add("Competencia", competencia.ToString("MM/yyyy"));
                }


                

                // INFORMAÇÕES PRESTADORES E TOMADORES

                XmlElement? prestadorServico = infNfse?.GetElementsByTagName("PrestadorServico")?.Count > 0
                    ? (XmlElement?)infNfse.GetElementsByTagName("PrestadorServico").Item(0) : null;

                var cnpjPrestador = prestadorServico?.GetElementsByTagName("Cpf")?.Count > 0
                    ? prestadorServico.GetElementsByTagName("Cpf").Item(0).InnerText : null;
                dict.Add("CnpjPrestador", cnpjPrestador);

                if (prestadorServico?.GetElementsByTagName("Cnpj")?.Count > 0)
                {
                    cnpjPrestador = prestadorServico?.GetElementsByTagName("Cnpj")?.Count > 0
                    ? prestadorServico.GetElementsByTagName("Cnpj").Item(0).InnerText : null;

                    dict["CnpjPrestador"] = cnpjPrestador;
                }

                var inscricaoMunicipalPrestador = prestadorServico?.GetElementsByTagName("InscricaoMunicipal")?.Count > 0
                    ? prestadorServico.GetElementsByTagName("InscricaoMunicipal").Item(0).InnerText : null;
                dict.Add("InscricaoMunicipalPrestador", inscricaoMunicipalPrestador);

                var razaoSocialPrestador = prestadorServico?.GetElementsByTagName("RazaoSocial")?.Count > 0
                    ? prestadorServico.GetElementsByTagName("RazaoSocial").Item(0).InnerText : null;
                dict.Add("RazaoSocialPrestador", razaoSocialPrestador);



                var cepPrestador = prestadorServico?.GetElementsByTagName("Cep")?.Count > 0
                    ? prestadorServico.GetElementsByTagName("Cep").Item(0).InnerText : null;
                dict.Add("CepPrestador", cepPrestador);

                var telefonePrestador = prestadorServico?.GetElementsByTagName("Telefone")?.Count > 0
                    ? prestadorServico.GetElementsByTagName("Telefone").Item(0).InnerText : null;
                dict.Add("TelefonePrestador", telefonePrestador);

                var emailPrestador = prestadorServico?.GetElementsByTagName("Email")?.Count > 0
                    ? prestadorServico.GetElementsByTagName("Email").Item(0).InnerText : null;
                dict.Add("EmailPrestador", emailPrestador);



                XmlElement tomador = infNfse?.GetElementsByTagName("TomadorServico")?.Count > 0
                        ? (XmlElement)infNfse.GetElementsByTagName("TomadorServico").Item(0) : null;


                var cpfCnpjTomador = tomador?.GetElementsByTagName("Cpf")?.Count > 0
                ? tomador.GetElementsByTagName("Cpf").Item(0).InnerText : null;
                dict.Add("CpfCnpjTomador", cpfCnpjTomador);

                if (tomador?.GetElementsByTagName("Cnpj").Count > 0)
                {
                    cpfCnpjTomador = tomador?.GetElementsByTagName("Cnpj").Item(0).InnerText;
                    dict["CpfCnpjTomador"] = cpfCnpjTomador;
                }

                var inscricaoMunicipalTomador = tomador?.GetElementsByTagName("InscricaoMunicipal")?.Count > 0
                ? tomador.GetElementsByTagName("InscricaoMunicipal").Item(0).InnerText : null;
                dict.Add("InscricaoMunicipalTomador", inscricaoMunicipalTomador);

                var razaoSocialTomador = tomador?.GetElementsByTagName("RazaoSocial")?.Count > 0
                ? tomador.GetElementsByTagName("RazaoSocial").Item(0).InnerText : null;
                dict.Add("RazaoSocialTomador", razaoSocialTomador);

                XmlElement enderecoTomador = tomador?.GetElementsByTagName("Endereco")?.Count > 1
                ? (XmlElement) tomador.GetElementsByTagName("Endereco").Item(0) : null;
                

                var enderecoT = enderecoTomador?.GetElementsByTagName("Numero")?.Count > 0
                ? enderecoTomador.GetElementsByTagName("Numero").Item(0).InnerText : null;
                dict.Add("EnderecoTomador", enderecoT);
                
                var numeroEnderecoTomador = enderecoTomador?.GetElementsByTagName("Numero")?.Count > 0
                ? enderecoTomador.GetElementsByTagName("Numero").Item(0).InnerText : null;
                dict.Add("NumeroEnderecoTomador", numeroEnderecoTomador);

                var complementoEnderecoTomador = enderecoTomador?.GetElementsByTagName("Complemento")?.Count > 0
                ? enderecoTomador.GetElementsByTagName("Complemento").Item(0).InnerText : null;
                dict.Add("ComplementoEnderecoTomador", complementoEnderecoTomador);

                var bairroTomador = enderecoTomador?.GetElementsByTagName("Bairro")?.Count > 0
                ? enderecoTomador.GetElementsByTagName("Bairro").Item(0).InnerText : null;
                dict.Add("BairroTomador", bairroTomador);

                var codigoMunicipioTomador = enderecoTomador?.GetElementsByTagName("CodigoMunicipio")?.Count > 0
                ? enderecoTomador.GetElementsByTagName("CodigoMunicipio").Item(0).InnerText : null;
                dict.Add("CodigoMunicipioTomador", codigoMunicipioTomador);

                var ufTomador = enderecoTomador?.GetElementsByTagName("Uf")?.Count > 0
                ? enderecoTomador.GetElementsByTagName("Uf").Item(0).InnerText : null;
                dict.Add("UfTomador", ufTomador);


                var cepTomador = enderecoTomador?.GetElementsByTagName("Cep")?.Count > 0
                ? enderecoTomador.GetElementsByTagName("Cep").Item(0).InnerText : null;
                dict.Add("CepTomador", cepTomador);

                var telefoneTomador = tomador?.GetElementsByTagName("Telefone")?.Count > 0
                ? tomador.GetElementsByTagName("Telefone").Item(0).InnerText : null;
                dict.Add("TelefoneTomador", telefoneTomador);

                var emailTomador = tomador?.GetElementsByTagName("Email")?.Count > 0
                ? tomador.GetElementsByTagName("Email").Item(0).InnerText : null;

                dict.Add("EmailTomador", emailTomador);


                // VALORES DOS SERVIÇOS

                XmlElement? servico = infNfse?.GetElementsByTagName("Servico")?.Count > 0
                    ? (XmlElement?)infNfse.GetElementsByTagName("Servico").Item(0) : null;

                XmlElement? valoresServicos = servico?.GetElementsByTagName("Valores")?.Count > 0
                    ? (XmlElement?)servico.GetElementsByTagName("Valores").Item(0) : null;


                var valorServicos = valoresServicos?.GetElementsByTagName("ValorServicos")?.Count > 0
                ? decimal.Parse(valoresServicos.GetElementsByTagName("ValorServicos").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                dict.Add("ValorServicos", Convert.ToString(valorServicos));

                var valorDeducoes = valoresServicos?.GetElementsByTagName("ValorDeducoes")?.Count > 0
                ? decimal.Parse(valoresServicos.GetElementsByTagName("ValorDeducoes").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                dict.Add("ValorDeducoes", Convert.ToString(valorDeducoes));

                var valorPis = valoresServicos?.GetElementsByTagName("ValorPis")?.Count > 0
                ? decimal.Parse(valoresServicos.GetElementsByTagName("ValorPis").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                dict.Add("ValorPis", Convert.ToString(valorPis));

                var valorCofins = valoresServicos?.GetElementsByTagName("ValorCofins")?.Count > 0
                ? decimal.Parse(valoresServicos.GetElementsByTagName("ValorCofins").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                dict.Add("ValorCofins", Convert.ToString(valorCofins));

                var valorInss = valoresServicos?.GetElementsByTagName("ValorInss")?.Count > 0
                ? decimal.Parse(valoresServicos.GetElementsByTagName("ValorInss").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                dict.Add("ValorInss", Convert.ToString(valorInss));

                var valorIr = valoresServicos?.GetElementsByTagName("ValorIr")?.Count > 0
                ? decimal.Parse(valoresServicos.GetElementsByTagName("ValorIr").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                dict.Add("ValorIr", Convert.ToString(valorIr));

                var valorCsll = valoresServicos?.GetElementsByTagName("ValorCsll")?.Count > 0
                ? decimal.Parse(valoresServicos.GetElementsByTagName("ValorCsll").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                dict.Add("ValorCsll", Convert.ToString(valorCsll));

                var outrasRetencoes = valoresServicos?.GetElementsByTagName("OutrasRetencoes")?.Count > 0
                ? decimal.Parse(valoresServicos.GetElementsByTagName("OutrasRetencoes").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                dict.Add("OutrasRetencoes", Convert.ToString(outrasRetencoes));

                
                var valorIss = valoresServicos?.GetElementsByTagName("ValorIss")?.Count > 0
                ? decimal.Parse(valoresServicos.GetElementsByTagName("ValorIss").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                dict.Add("ValorIss", Convert.ToString(valorIss));
                
                var valorIssRetido = valoresServicos?.GetElementsByTagName("ValorIssRetido")?.Count > 0
                ? decimal.Parse(valoresServicos.GetElementsByTagName("ValorIssRetido").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                dict.Add("ValorIssRetido", Convert.ToString(valorIssRetido));


                
                var aliquota = valoresServicos.GetElementsByTagName("Aliquota").Count > 0
                ? decimal.Parse(valoresServicos.GetElementsByTagName("Aliquota").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                dict.Add("Aliquota", Convert.ToString(aliquota));

                var descontoCondicionado = valoresServicos?.GetElementsByTagName("DescontoCondicionado")?.Count > 0
                ? decimal.Parse(valoresServicos.GetElementsByTagName("DescontoCondicionado").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                dict.Add("DescontoCondicionado", Convert.ToString(descontoCondicionado));

                var descontoIncondicionado = valoresServicos?.GetElementsByTagName("DescontoIncondicionado")?.Count > 0
                ? decimal.Parse(valoresServicos.GetElementsByTagName("DescontoIncondicionado").Item(0).InnerText ?? "0", new CultureInfo("en-US")) : 0;
                dict.Add("DescontoIncondicionado", Convert.ToString(descontoIncondicionado));


                var issRetido = valoresServicos?.GetElementsByTagName("IssRetido")?.Count > 0
                ? int.Parse(valoresServicos.GetElementsByTagName("IssRetido").Item(0).InnerText) : 0;
                dict.Add("IssRetido", ((issRetido == 1) ? "Sim" : "Não"));


                var outrasInformacoes = infNfse?.GetElementsByTagName("OutrasInformacoes")?.Count > 0
                    ? infNfse.GetElementsByTagName("OutrasInformacoes").Item(0).InnerText : null;
                dict.Add("OutrasInformacoes", outrasInformacoes);

                var itemListaServico = servico?.GetElementsByTagName("ItemListaServico")?.Count > 0
                ? servico.GetElementsByTagName("ItemListaServico").Item(0).InnerText : null;
                dict.Add("ItemListaServico", itemListaServico);

                var codigoCnae = servico?.GetElementsByTagName("CodigoCnae")?.Count > 0
                ? servico.GetElementsByTagName("CodigoCnae").Item(0).InnerText : null;
                dict.Add("CodigoCnae", codigoCnae);

                var codigoTributacaoMunicipio = servico?.GetElementsByTagName("CodigoTributacaoMunicipio")?.Count > 0
                ? servico.GetElementsByTagName("CodigoTributacaoMunicipio").Item(0).InnerText : null;
                dict.Add("CodigoTributacaoMunicipio", codigoTributacaoMunicipio);

                var discriminacao = servico?.GetElementsByTagName("Discriminacao")?.Count > 0
                ? servico.GetElementsByTagName("Discriminacao").Item(0).InnerText : null;
                dict.Add("Discriminacao", discriminacao);



                var codigoMunicipioPrestacao = servico?.GetElementsByTagName("CodigoMunicipio")?.Count > 0
                ? servico.GetElementsByTagName("CodigoMunicipio").Item(0).InnerText : null;
                dict.Add("CodigoMunicipioPrestacao", codigoMunicipioPrestacao);

                                

                


            }

            return dict;

        }



    }
}
