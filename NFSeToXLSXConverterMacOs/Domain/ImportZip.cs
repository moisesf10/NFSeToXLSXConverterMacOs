using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSeToXLSXConverterMacOs.Domain
{
    internal class ImportZip : IFileImport
    {
        private MemoryStream? _memoryStream = null;
        private ZipArchive _archive;
        private List<ZipArchiveEntry> _entries;

        public async Task<(bool, string)> loadFile(Stream stream)
        {
            
            try
            {
                _memoryStream = new MemoryStream();
                await stream.CopyToAsync(_memoryStream);
                _memoryStream.Position = 0; // Resetar a posição do stream para o início
                _archive = new ZipArchive(_memoryStream, ZipArchiveMode.Read);
                _entries = new List<ZipArchiveEntry>();
                foreach (var entry in _archive.Entries)
                {
                    _entries.Add(entry);
                }
            }
            catch (Exception ex)
            {
                return (false, "Erro ao importar arquivo: " + ex.Message);
            }

            return (true, "");
        }

        public IDictionary<string, string?>? readFile(int index = 0)
        {
            if (index >= 0 && index < _entries.Count)
            {
                ZipArchiveEntry entry = _entries[index];
                using (var entryStream = entry.Open())
                {
                    using (var reader = new StreamReader(entryStream))
                    {

                        string content = reader.ReadToEnd();
                        Dictionary<string, string?> dictData = ParseXml.parse(content);
                        return dictData;
                    }
                }
            }
            return null; // Retorna null se o índice for inválido
        }

        public IEnumerable<IDictionary<string, string?>> readFiles()
        {
            foreach (ZipArchiveEntry entry in _archive.Entries)
            {

                // Ler o conteúdo do arquivo individual
                using (Stream entryStream = entry.Open())
                {
                    using (StreamReader reader = new StreamReader(entryStream))
                    {
                        string content = reader.ReadToEnd();
                        yield return ParseXml.parse(content);
                    }
                }
            }
        }

        public void dispose()
        {
            _archive.Dispose();
            _memoryStream.Dispose();

        }
    }
}
