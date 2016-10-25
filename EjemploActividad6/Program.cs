using EjemploActividad6.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EjemploActividad6
{
    class Program
    {
        static void Main(string[] args)
        {
            //http://www.flickr.com/services/feeds/photos_public.gne?tags=soccer&format=json&nojsoncallback=1

            //Task.Run es necesario porque las aplicaciones de consola no admiten async. Ej: async void Main(string[] args) no es posible. en las aplicaciones 
            // para Windows Phone o Escritorio, no es necesario usar Task.Run
            Task.Run(async () =>
            {
                //Inicializamos el objeto
                using (HttpClient client = new HttpClient())
                {

                    //Realizamos una llama asincrona. En iOS tendriamos que usar callbacks con NSURLSession, y en Android AsyncTask con HttpUrlConnection. En este caso, async hace esta tarea mas sencilla.
                    //content tendra el resultado como cadena (String). Despues hay que "desaplanar" esta cadena, esto es, pasarlo a una estructura de datos que podamos manipular
                    String content = await client.GetStringAsync("http://www.flickr.com/services/feeds/photos_public.gne?tags=soccer&format=json&nojsoncallback=1");

                    //La libreria Newtonsoft, realiza el "desaplanado" de los datos. Tenemos que pasarle como parametros, el tipo de datos que esperamos, en este caso Feed y la cadena 
                    //que queremos desaplanar
                    Feed feed = JsonConvert.DeserializeObject<Feed>(content);
                    //En este punto feed debera contener los datos del json
                    //Para entender las clases que se crearon en la carpeta Models, tienen que abrir la liga del json en un navegador como Firefox o Chrome; y analisar su estructura.
                    
                    //Imprimimos el titulo del feed
                    Console.WriteLine("Titulo: {0}",feed.Title);

                    //Las imagenes estan en Feed -> Items [] -> Media -> M
                    foreach (var f in feed.Items) {
                        Console.WriteLine("Imagen: {0}",f.Media.M);
                    }
                    
                    //Se termina la aplicacion con pulsar una tecla
                    Console.ReadLine();
                }

            }).Wait();


        }
    }
}
