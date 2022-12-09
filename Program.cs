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

            try
            {
                // LISTA TODOS OS ARQUIVOS DO DIRETÓRIO
                List<string>? allFiles = new(Directory.GetFiles(sourcePath));

                // GERA UM ARRAY IGUAL AO NÚMERO DE PASTAS
                IEnumerable<string>[] folderFiles = new IEnumerable<string>[foldersCategory.Length];

                // FILTRA OS ARQUIVOS POR CATEGORIA
                folderFiles[0] = allFiles.Where(a => a.EndsWith(".txt") || a.EndsWith(".pdf") || a.EndsWith(".docx") || a.EndsWith(".ppt") || a.EndsWith(".xls"));
                folderFiles[1] = allFiles.Where(a => a.EndsWith(".jpeg") || a.EndsWith(".png") || a.EndsWith(".bmp") || a.EndsWith(".gif") || a.EndsWith(".svg"));
                folderFiles[2] = allFiles.Where(a => a.EndsWith(".mp3") || a.EndsWith(".wav") || a.EndsWith(".acc") || a.EndsWith(".mqa") || a.EndsWith(".ogg"));
                folderFiles[3] = allFiles.Where(a => a.EndsWith(".mp4") || a.EndsWith(".avi") || a.EndsWith(".mpeg") || a.EndsWith(".mkv") || a.EndsWith(".webm"));

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
            catch (System.ArgumentException e)
            {
                Console.WriteLine();
                Console.WriteLine($"ERRO: O caminho do arquivo não pode ser vazio.");
            }
        }
    }
}