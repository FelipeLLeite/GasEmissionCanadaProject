/**
 * Authors:         Felipe Lopes Leite & Renato Medeiros
 * Class name:      DataHendler
 * Purpose:         Handle the data analysis and cleaning the data to be display at the screen.
 */

using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;

namespace GasEmissionsProject
{
    public class DataHandler
    {
        /// <summary>
        /// Populate the hash sets containers to store the regions and sources strings that exists in the original documents
        /// </summary>
        /// <param name="doc">XmlDocument object reference.</param>
        /// <param name="regions">HashSet to represent the regions strings.</param>
        /// <param name="sources">HashSet to represent the sources strings.</param>
        public void PopulateLists(XmlDocument doc, ref HashSet<string> regions, ref HashSet<string> sources)
        {
            // Populate region list 
            XmlNodeList regionsNodeList = doc.GetElementsByTagName("region");
            foreach (XmlNode node in regionsNodeList)
            {
                XmlAttributeCollection regionAttr = node.Attributes;
                XmlNode nameNode = regionAttr.GetNamedItem("name");
                if (nameNode == null)
                    Console.WriteLine("Attribute 'name' not found.");
                else
                    regions.Add(nameNode.InnerText);
            } // end foreach

            // Populate source list
            XmlNodeList sourceNodeList = doc.GetElementsByTagName("source");
            foreach (XmlNode node in sourceNodeList)
            {
                XmlAttributeCollection sourceAttr = node.Attributes;
                XmlNode nameNode = sourceAttr.GetNamedItem("description");
                if (nameNode == null)
                    Console.WriteLine("Attribute 'description' not found.");
                else
                    sources.Add(nameNode.InnerText);
            } // end foreach
        } // end PopulateLists method

        /// <summary>
        /// Analyse and filter the data based on the choice mande by the user input.
        /// Store the filtered data it into a dictionary to be display later.
        /// </summary>
        /// <param name="doc">XmlDocument object reference.</param>
        /// <param name="regions">HashSet to represent the regions strings.</param>
        /// <param name="sources">HashSet to represent the sources strings.</param>
        /// <param name="startYear">Integer number to represent start year of the analysis.</param>
        /// <param name="endYear">Integer number to represent end year of the analysis.</param>
        /// <param name="choice">String to represetn the region or the source that the user wants.</param>
        /// <param name="reportOption">Represent the type of the report that user wants.</param>
        public void DataRport(XmlDocument doc, HashSet<string> regions, HashSet<string> sources,
            int startYear, int endYear, string choice, ReportType.ReportOption reportOption)
        {
            double dataValue = -1;
            // dictionary to store the combination of region/source and its data (values)
            Dictionary<string, List<double>> dataMap = new Dictionary<string, List<double>>();
            // strings placeholdedr for the heading text
            string miniText = reportOption == ReportType.ReportOption.Regions ? "in" : "from";
            string heading = $"Emissions {miniText} {choice} (Megatonnes)";

            Console.WriteLine($"{Environment.NewLine}{heading}");
            Console.WriteLine($"{new string('-', heading.Length)}{Environment.NewLine}");
            if (reportOption == ReportType.ReportOption.Regions)
                Console.Write($"{"Source",55}");
            else
                Console.Write($"{"Region",55}");
            // Display the years based on the limit years (startYear and endYear)
            for (int i = startYear; i <= endYear; i++)
                Console.Write($"{i,9}");
            Console.WriteLine(Environment.NewLine);

            string query;
            // Create the navegator obj and its iterator 
            XPathNavigator navigator = doc.CreateNavigator();
            XPathNodeIterator nodeIterator;
            // check if the report type is for regions, otehrwise will be for source.
            if (reportOption == ReportType.ReportOption.Regions)
            {
                // Loop through all the source in the source set container.
                foreach (string src in sources)
                {
                    // Add to the dictionary to contain our source and its datas.
                    dataMap.Add(src, new List<double>());
                    // loop through the years and query the original data.
                    for (int year = startYear; year <= endYear; year++)
                    {
                        // create the query for base on region
                        query = $"//region[@name='{choice}']/source[@description='{src}']/emissions[@year={year}]";
                        try
                        {
                            // Get data interator from xpath query
                            nodeIterator = navigator.Select(query);
                            if (nodeIterator.MoveNext())
                            {
                                // try to parse the data from the document
                                if (Double.TryParse(nodeIterator.Current.Value, out dataValue))
                                    // Add the data to the container.
                                    dataMap[src].Add(dataValue);
                                else
                                    // If parse is unsuccessiful we store the negative value
                                    dataMap[src].Add(-1);
                            }
                            else
                                // If the data does not exist it will add -1 to the list.
                                // The negative value does not exist in real life. Emission cannot be negative.
                                // Wee use this idea to track the missing data.
                                dataMap[src].Add(-1);
                        }
                        catch (XPathException ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.StackTrace);
                        } // end try-catch block
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.StackTrace);
                        } // end try-catch block
                    } // end for-loop block
                } // end foreach block
            }
            else
            {
                // Loop through all the region in the region set container.
                foreach (string reg in regions)
                {
                    // Add to the dictionary to contain our region and its datas.
                    dataMap.Add(reg, new List<double>());
                    // loop through the years and query the original data.
                    for (int year = startYear; year <= endYear; year++)
                    {
                        // create the query for base on choice
                        query = $"//region[@name='{reg}']/source[@description='{choice}']/emissions[@year={year}]";
                        try
                        {
                            // Get data interator from xpath query
                            nodeIterator = navigator.Select(query);
                            if (nodeIterator.MoveNext())
                            {
                                // try to parse the data from the document
                                if (Double.TryParse(nodeIterator.Current.Value, out dataValue))
                                    // Add the data to the container.
                                    dataMap[reg].Add(dataValue);
                                else
                                    // If parse is unsuccessiful we store the negative value
                                    dataMap[reg].Add(-1);
                            }
                            else
                                // If the data does not exist it will add -1 to the list.
                                // The negative value does not exist in real life. Emission cannot be negative.
                                // Wee use this idea to track the missing data.
                                dataMap[reg].Add(-1);
                        }
                        catch (XPathException ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.StackTrace);
                        } // end try-catch block
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.StackTrace);
                        } // end try-catch block
                    } // end for-loop block
                } // end foreach block
            } // end if-else

            // Display data based on the report type
            if (reportOption == ReportType.ReportOption.Regions)
            {
                foreach (string src in sources)
                {
                    Console.Write($"{src,55}");
                    foreach (double val in dataMap[src])
                    {
                        if (val != -1)
                            Console.Write($"{val,9:0.000}");
                        else
                            Console.Write($"{"-",9}");
                    } // end inner foreach block
                    Console.WriteLine();
                } // end outter foreach block
            }
            else
            {
                foreach (string reg in regions)
                {
                    Console.Write($"{reg,55}");
                    foreach (double val in dataMap[reg])
                    {
                        if (val != -1)
                            Console.Write($"{val,9:0.000}");
                        else
                            Console.Write($"{"-",9}");
                    } // end inner foreach block
                    Console.WriteLine();
                } // end outter foreach block
            } // end if-else
        } // end DataRport method
    } // end ResultHandler class
} // end namespace
