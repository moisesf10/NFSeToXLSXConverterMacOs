using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSeToXLSXConverterMacOs.Domain
{
    internal static class NaturezaOperacao
    {
        private static readonly Dictionary<int, string> _status = new Dictionary<int, string>
        {
            { 1, "Tributação no município" },
            { 2, "Tributação fora do município" },
            { 3, "Isenção" },
            { 4, "Imune" },
            { 5, "Exigibilidade Suspensa por Decisão Judicial" },
            { 6, "Exigibilidade Suspensa por Processo Administrativo" }
        };

        public static string GetValue(int codigo)
        {
            if (_status.TryGetValue(codigo, out string descricao))
            {
                return descricao;
            }
            else
            {
                return "Código inválido";
            }
        }
    }
}
