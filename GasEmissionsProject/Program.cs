using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace GasEmissionsProject
{
    public class Program
    {
        // String for the 
        private static readonly string XML_FILE = @"ghg-canada.xml";

        private static MenuHandler objMenu = new MenuHandler();
        private static OptionHandler objOption = new OptionHandler();
        private static DataHandler objData = new DataHandler();

        private static HashSet<string> regions = new HashSet<string>();
        private static HashSet<string> sources = new HashSet<string>();

        private static int startYear = 2015;
        private static int endYear = 2019;

        private static string regionName = "";
        private static string sourceName = "";
        private static string option = "";


        static void Main(string[] args)
        {
            bool done = false;
            try
            {
                // Create a Xml document object and load the file with the data
                XmlDocument doc = new XmlDocument();
                doc.Load(XML_FILE);
                // Populate list for region and section
                objData.PopulateLists(doc, ref regions, ref sources);
                while (!done)
                {
                    // Display Title and main menu
                    objMenu.MainTitle();
                    objMenu.MainMenu();
                    // Display the option and handle to 
                    option = objOption.GetMainOption();
                    switch (option)
                    {
                        case "y":
                        case "Y":
                            objOption.AdjustRangeYear(ref startYear, ref endYear);
                            Console.Clear();
                            break;
                        case "r":
                        case "R":
                            objMenu.MenuRegion(regions);
                            regionName = objOption.GetRegionName(regions);
                            objData.DataRport(doc, regions, sources, startYear, endYear, regionName, ReportType.ReportOption.Regions);
                            Console.Write("Press any key to continue.");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "s":
                        case "S":
                            objMenu.MenuSource(sources);
                            sourceName = objOption.GetSourceName(sources);
                            objData.DataRport(doc, regions, sources, startYear, endYear, sourceName, ReportType.ReportOption.Sources);
                            Console.Write("Press any key to continue.");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        default:
                            Console.WriteLine($"{Environment.NewLine}Press any key to continue.");
                            Console.ReadKey();
                            Console.WriteLine($"{Environment.NewLine}All done!");
                            Environment.Exit(0);
                            break;

                    } // end switch
                } // end while block
            } // end try
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            } // end catch
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            } // end try-catch block
        } // end main
    } // end class
} // end namespace