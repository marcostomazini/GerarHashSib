using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace GerarHashSib
{
    public class TratarArquivo
    {
        private static XElement root;

        public static void Executar(string file)
        {
            string subPath = "out";
            Directory.CreateDirectory(subPath);

            XDocument xmlDoc = XDocument.Load(file);

            Console.WriteLine(string.Empty);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("------------------------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("Parsing file: " + file);
            Console.WriteLine("Generating hashing from file: " + file);
            var hash = GerarHash.CalcularHash(xmlDoc.ToString());
            System.Console.WriteLine("Generated hash: " + hash);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(hash);
            Console.ResetColor();

            bool existsTagHash = xmlDoc.Descendants("epilogo").Any() && xmlDoc.Descendants("hash").Any();

            if (existsTagHash)
            {
                var tagHash = xmlDoc.Descendants("hash").LastOrDefault();
                tagHash.SetValue(hash);
            }
            else
            {
                bool existsTagEpilogo = xmlDoc.Descendants("epilogo").Any() && !xmlDoc.Descendants("hash").Any();

                if (existsTagEpilogo)
                {
                    root = new XElement("hash", hash);
                    xmlDoc.Root.Element("epilogo").Add(root);
                }
                else
                {
                    root = new XElement("epilogo");
                    root.Add(new XElement("hash", hash));
                    xmlDoc.Element("mensagemSIB").Add(root);
                }
            }

            Console.WriteLine("Save file output: " + file);
            xmlDoc.Save(@"out\" + file);

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("------------------------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine(string.Empty);
        }
    }
}
