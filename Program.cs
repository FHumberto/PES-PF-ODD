namespace ODD
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Informe o caminho da pasta que deseja organizar:");
            Console.WriteLine();
            Console.Write("Exemplo: D:\\Downloads : ");
            string? sourcePath = Console.ReadLine();

            string[] foldersCategory = { "\\Documentos\\", "\\Imagens\\", "\\Música\\", "\\Vídeos\\" };

            // TIPOS DE ARQUIVO MAIS COMUNS
            List<string> docExtensions = new()
            { ".doc", ".docx", ".pdf", ".ppt", ".pptx", ".xls", ".txt" };
            List<string> imgExtensions = new()
            { ".bmp", ".gif", ".jpg", ".png", ".psd", ".raw", ".svg", ".tiff", ".webp" };
            List<string> mscExtensions = new()
            { ".acc", ".ac3", ".flac", ".m4a", ".mp3", ".ogg", ".wav", ".wma" };
            List<string> vidExtensions = new()
            { ".3gp", ".avi", ".flv", ".h264", ".m4v", ".mkv", ".mov", ".mp4", ".mpeg", ".mpg", ".mp4", ".mpeg", ".mpg", ".ogv", ".webm", ".wmv" };

            try
            {
                if (string.IsNullOrEmpty(sourcePath))
                {
                    throw new ArgumentException("não pode ser vazio");
                }
                else
                {
                    // LISTA TODOS OS ARQUIVOS DO DIRETÓRIO
                    List<string>? allFiles = new(Directory.GetFiles(sourcePath));

                    // GERA UM ARRAY IGUAL AO NÚMERO DE PASTAS
                    IEnumerable<string>[] folderFiles = new IEnumerable<string>[foldersCategory.Length];

                    // FILTRA OS ARQUIVOS POR CATEGORIA
                    folderFiles[0] = allFiles.Where(a => docExtensions.Any(x => a.EndsWith(x)));
                    folderFiles[1] = allFiles.Where(a => imgExtensions.Any(x => a.EndsWith(x)));
                    folderFiles[2] = allFiles.Where(a => mscExtensions.Any(x => a.EndsWith(x)));
                    folderFiles[3] = allFiles.Where(a => vidExtensions.Any(x => a.EndsWith(x)));

                    // CRIA AS PASTAS
                    for (int i = 0; i < foldersCategory.Length; i++)
                    {
                        if (!Directory.Exists(sourcePath + foldersCategory[i]))
                        {
                            Directory.CreateDirectory(sourcePath + foldersCategory[i]);
                        }

                        // VASCULHA CADA CATEGORIA
                        foreach (var olderPath in folderFiles[i])
                        {
                            var newPath = sourcePath + foldersCategory[i] + Path.GetFileName(olderPath);

                            if (!File.Exists(olderPath))
                            {
                                // CRIA UM NOVO CAMINHO
                                using FileStream fs = File.Create(olderPath);
                            }

                            if (File.Exists(newPath))
                            {
                                Console.WriteLine($"O arquivo {olderPath} já existe {newPath} [não movido]");
                            }
                            else
                            {
                                // MOVE OS ARQUIVOS.
                                File.Move(olderPath, newPath, false);
                                Console.WriteLine($"{olderPath} [movido para] {newPath}.");
                            }
                        }
                    }
                }
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                Console.WriteLine();
                Console.Write($"[ERRO]: Diretório não encontrado");
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine();
                Console.WriteLine($"[ERRO]: O caminho do arquivo não pode ser vazio.");
            }
        }
    }
}