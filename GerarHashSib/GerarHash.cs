using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace GerarHashSib
{
    // http://ans.gov.br/a-ans/sala-de-noticias-ans/operadoras-e-servicos-de-saude/610-ans-divulga-exemplo-de-arquivo-sib-xml
    public class GerarHash
    {                
        public static byte[] StringToIsoByteArray(string s)
        {
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            return iso.GetBytes(s);
        }

        public static string CalcularHash(string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                //iniciar do nó mensagemSIB
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "mensagemSIB"))
                        break;
                }

                //percorrer todo o xml, criando string com os valores dos elementos
                //até chegar no epílogo, que não deve ser considerado.
                do
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "epilogo"))
                    {
                        break;
                    }

                    if ((reader.AttributeCount > 2) && (reader.Name != "registroRejeitado"))
                    {
                        for (var i = 2; i < reader.AttributeCount; i++)
                        {
                            if (reader[i].Trim() != "")
                                sb.Append(reader[i]);
                        }
                    }
                    else if (reader.AttributeCount > 0)
                    {
                        for (var i = 0; i < reader.AttributeCount; i++)
                        {
                            if (reader[i].Trim() != "")
                                sb.Append(reader[i]);
                        }
                    }
                    else
                    {
                        if (reader.NodeType == XmlNodeType.Text)
                        {
                            if (reader.Value.Trim() != "")
                                sb.Append(reader.Value);
                        }
                    }
                } while (reader.Read());
            }

            byte[] byteArray = StringToIsoByteArray(sb.ToString());
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(byteArray);

            // converter o array de bytes em uma string hexadecimal
            sb = new StringBuilder();
            foreach (byte i in hash)
                sb.Append(i.ToString("x2"));
            return sb.ToString().ToUpper();
        }        
    }
}
