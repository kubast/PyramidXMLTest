using System;
using System.Collections.Generic;
using System.Text;
using XMLLoad.Files;
using System.Linq;
using Microsoft.Extensions.Configuration;
using XMLLoad.Processing;

namespace XMLLoad
{
    /// <summary>
    /// Solution based on strongly typed objet deserialized from XML
    /// Recursion algorithm
    /// </summary>
    public class RecursiveSolution
    {

        private readonly IXmlService _xmlService;
        private readonly IConfiguration _configs;
        private readonly IPyramidService _pyramidService;
        public RecursiveSolution(IXmlService service, IPyramidService pyramidService, IConfiguration configs)
        {
            _xmlService = service;
            _configs = configs;
            _pyramidService = pyramidService;
        }
        public void Execute()
        {
            //get input from files..can be chanhed in appsettings.json to test different options;
            var basepPath = _configs["XmlFiles:BasePath"];
            var pyramidFile = basepPath + _configs["XmlFiles:PyramidFile"];
            var paymentsFile = basepPath +_configs["XmlFiles:PaymentsFile"];

            var pyramidMembers = _xmlService.ParseXml<Piramida>(pyramidFile);
            var payments = _xmlService.ParseXml<Payments>(paymentsFile);

            if(pyramidMembers == null || pyramidMembers.Member == null || payments == null)
            {
                return;
            }

            _pyramidService.AnalyseMembers(pyramidMembers.Member);   
            
             Console.WriteLine("\n Piramid report  without deposits");
            _pyramidService.DispalyReport(MembersComaprer.ComparerType.Id);
            //optionaly can be sorted by level in pyramiod structure
            _pyramidService.DispalyReport(MembersComaprer.ComparerType.Level);
           
            _pyramidService.CalculateProvision(payments);

             Console.WriteLine("\n Piramid report after payments ");
            _pyramidService.DispalyReport(MembersComaprer.ComparerType.Id);
        }
    }
}