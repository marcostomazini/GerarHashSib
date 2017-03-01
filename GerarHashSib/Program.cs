using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace GerarHashSib
{
    class Program
    {
        private static bool success = false;

        static void Main(string[] args)
        {
            try
            {                
                Console.WriteLine(string.Empty);
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Porto Seguro - Geracao de Hash no Epilogo");
                Console.ResetColor();
                Console.WriteLine(string.Empty);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Starting....");
                Console.ResetColor();

                if (args.Length > 0)
                {
                    string file = args[0];
                    TratarArquivo.Executar(file);
                    success = true;
                }
                else
                {
                    var currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    string[] files = Directory.GetFiles(currentPath, "*.sbx");

                    foreach (var file in files)
                    {
                        DirectoryInfo directory = new DirectoryInfo(file);
                        TratarArquivo.Executar(directory.Name);
                    }

                    success = files.Length > 0;
                }                

                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(success ? "Success!" : "Nenhum arquivo encontrado para geracao.");
                Console.ResetColor();
                Console.WriteLine(string.Empty);
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Porto Seguro - Geracao de Hash no Epilogo");
                Console.ResetColor();
                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.ResetColor();                
                Console.WriteLine(string.Empty);
                Console.WriteLine("Erro ao gerar hash");
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.WriteLine(string.Empty);
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Porto Seguro - Geracao de Hash no Epilogo");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }
}
