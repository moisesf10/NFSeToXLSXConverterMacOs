using System.Text.Json;

namespace NFSeToXLSXConverterMacOs.Domain;

public static class ConfiguracaoService
{
    private static string ObterCaminhoArquivoConfiguracao()
    {
        // Define um diretório apropriado no sistema operacional para armazenar configurações
        // string pastaConfiguracao = Path.Combine(
        //     Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        //     "NFSeToXlsxConverter" // Nome da sua aplicação (pasta personalizada)
        // );

        string pastaConfiguracao = FileSystem.AppDataDirectory;

        // Cria o diretório caso não exista
        if (! Directory.Exists(pastaConfiguracao))
        {
            Directory.CreateDirectory(pastaConfiguracao);
        }

        // Retorna o caminho completo do arquivo de configuração JSON
        return Path.Combine(pastaConfiguracao, "campos_utilizados.json");
    }
    
    public static async Task<bool> SalvarConfiguracoesAsync(CamposUtilizadosDTO campos)
    {
        campos.IndicaAppConfigurado = true;
        try
        {
            // Serializa o objeto para JSON
            string json = JsonSerializer.Serialize(campos, new JsonSerializerOptions { WriteIndented = true });

            // Salva no arquivo
            string caminhoArquivo = ObterCaminhoArquivoConfiguracao();
            await File.WriteAllTextAsync(caminhoArquivo, json);

        }
        catch (Exception ex)
        {
            return false;
        }
        
        return true;
    }
    
    public static  CamposUtilizadosDTO? CarregarConfiguracao()
    {
        try
        {
            string caminhoArquivo = ObterCaminhoArquivoConfiguracao();

            if (!File.Exists(caminhoArquivo))
            {
                Console.WriteLine("Arquivo de configuração não encontrado.");
                return null;
            }

            // Lê o conteúdo do arquivo JSON
            string json =  File.ReadAllText(caminhoArquivo);
            

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignora diferenciação de maiúsculas e minúsculas
            };
            // Desserializa para a classe NfseDto
            var desserializado = JsonSerializer.Deserialize<CamposUtilizadosDTO>(json, options);
            return desserializado;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao ler a configuração: {ex.Message}");
            return null;
        }
    }
    
}