/**
 * Authors:         Felipe Lopes Leite & Renato Medeiros
 * Class name:      MenuHandler
 * Purpose:         Represent the handler of the menu. All the menu options are deal by this class.
 */

using System;
using System.Collections.Generic;

namespace GasEmissionsProject
{
    public class MenuHandler
    {
        /// <summary>
        /// Display the main title.
        /// </summary>
        public void MainTitle()
        {
            Console.WriteLine("Greenhouse Gas Emissions in Canada");
            Console.WriteLine($"{new string('=', 34)}\n");
        } // end MainTitle method

        /// <summary>
        /// Display the main menu.
        /// </summary>
        public void MainMenu()
        {
            Console.WriteLine("'Y' to adjust the range of years");
            Console.WriteLine("'R' to select a region");
            Console.WriteLine("'S' to select a specific GHG source");
            Console.WriteLine("'X' to exit the program");
        } // end MainMenu method

        /// <summary>
        /// Display the menu region.
        /// </summary>
        /// <param name="regions">HashSet of the regions from the original data document</param>
        public void MenuRegion(HashSet<string> regions)
        {
            int idx = 1;
            Console.WriteLine(Environment.NewLine + "Select a region by number as shown below...");
            foreach (string reg in regions)
                Console.WriteLine($"{idx++,3}. {reg}");
            // end foreach block statement
        } // end MenuRegion method

        /// <summary>
        /// Display the menu source.
        /// </summary>
        /// <param name="sources">HashSet of the sources from the original data document</param>
        public void MenuSource(HashSet<string> sources)
        {
            int idx = 1;
            Console.WriteLine(Environment.NewLine + "Select a region by number as shown below...");
            foreach (string src in sources)
                Console.WriteLine($"{idx++,3}. {src}");
            // end foreach block statement
        } // end MenuSource method
    }// end Menu class
} // end namespace
