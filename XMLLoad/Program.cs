using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Xml;
using XMLLoad.Processing;

namespace XMLLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pyramid Test - Dawid Stoga");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration configuration = builder.Build();
            //in case another xml processor will be tested
           
            
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IXmlService, XmlService>()
                .AddSingleton<IPyramidService, PyramidService>()
                .AddSingleton<IConfiguration>(configuration)
                .BuildServiceProvider();

            var xmlService =  serviceProvider.GetService<IXmlService>();
            var pyramidService = serviceProvider.GetService<IPyramidService>();

            new RecursiveSolution(xmlService, pyramidService, configuration).Execute();
        }
    }
}
