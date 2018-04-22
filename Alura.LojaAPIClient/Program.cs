using System;
using System.IO;
using System.Net;
using System.Text;

namespace Alura.LojaAPIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TestaPost();
        }

        static void TestaPost()
        {
            string conteudo;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:58891/api/carrinho");
            request.Method = "POST";

            string json = "{'Produtos':[{'Id':6237,'Preco':4000.0,'Nome':'Xbox','Quantidade':3}],'Endereço':'Rua Vergueiro 3185, 8 andar, Sao Paulo','Id':2}";
            string xml = @"<Carrinho 
                            xmlns:i=""http://www.w3.org/2001/XMLSchema-instance""
                            xmlns = ""http://schemas.datacontract.org/2004/07/Alura.LojaAPI.Models"" >
                            <Endereco> Rua Vergueiro 3185, 8 andar, Sao Paulo</Endereco>
                            <Id>10</Id>
                            <Produtos>
                               <Produto >
                                   <Id>6237</Id>
                                   <Nome>Videogame 4</Nome>
                                      <Preco>4000</Preco>
                                      <Quantidade> 1 </Quantidade>
                                </Produto >  
                            </Produtos>
                           </Carrinho>";

            byte[] jsonBytes = Encoding.UTF8.GetBytes(xml);
            request.GetRequestStream().Write(jsonBytes, 0, jsonBytes.Length);

            request.ContentType = "application/json";

            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                conteudo = reader.ReadToEnd();
            }
            Console.Write(conteudo);
            Console.Read();
        }

        static void TestaGet()
        {
            string conteudo;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:58891/api/carrinho/1");
            request.Method = "GET";
            request.Accept = "application/json";

            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                conteudo = reader.ReadToEnd();
            }
            Console.Write(conteudo);
            Console.Read();
        }
    }
}
