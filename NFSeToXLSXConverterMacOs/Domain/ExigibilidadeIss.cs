using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSeToXLSXConverterMacOs.Domain
{
    internal static class ExigibilidadeIss
    {
        private static readonly Dictionary<int, string> _status = new Dictionary<int, string>
        {
            { 1, "Exigível" },
            { 2, "Não incidência" },
            { 3, "Isenção" },
            { 4, "Exportação" },
            { 5, "Imunidade" },
            { 6, "Exigibilidade Suspensa por Decisão Judicial" },
            { 7, "Exigibilidade Suspensa por Processo Administrativo" }
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
