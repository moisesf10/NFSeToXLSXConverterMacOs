using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSeToXLSXConverterMacOs.Domain
{
    public class ImportXml : IFileImport
    {
        private string content = string.Empty;
        public async Task<(bool, string)> loadFile(Stream? stream)
        {
            
            try
            {
                if (stream != null) {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        content = streamReader.ReadToEnd();
                    };
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

            return (true, "");
        }

        public IDictionary<string, string> readFile(int index = 0)
        {
            throw new NotImplementedException();
        }

        public async Task<IDictionary<string, string?>> readFile(Stream? stream = null)
        {
            if (stream != null)
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    content = await streamReader.ReadToEndAsync();
                };
            }

            if (content == null)
            {
                throw new Exception("Nenhum arquivo XML foi carregado");
            }

            Dictionary<string, string?> dictData = ParseXml.parse(content);

            return dictData;
        }

        public IEnumerable<IDictionary<string, string>> readFiles()
        {
            throw new NotImplementedException();
        }
    }
}
