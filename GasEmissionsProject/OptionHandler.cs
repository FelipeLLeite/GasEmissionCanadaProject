/**
 * Authors:         Felipe Lopes Leite & Renato Medeiros
 * Class name:      OptionHandler
 * Purpose:         Represent the handler of the option. All the menu options are deal by this class.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GasEmissionsProject
{
    public class OptionHandler
    {
        /// <summary>
        /// Obtain the user input for the menu option.
        /// </summary>
        /// <returns>A string represent the user selecion option.</returns>
        public string GetMainOption()
        {
            string option = "";
            bool isValid = false;
            while (!isValid)
            {
                Console.Write("Your selection: ");
                option = Console.ReadLine();
                if (!Regex.Match("[Y]|[y]|[R]|[r]|[S]|[s]|[X]|[x]", option).Success || String.IsNullOrEmpty(option))
                    Console.WriteLine($"Invalid input. Please select one of the options above.{Environment.NewLine}");
                else
                    isValid = true;
            } // end while
            return option;
        } // end GetMainOption method

        /// <summary>
        /// Ajust the start and end year of the program.
        /// </summary>
        /// <param name="startYear">Integer representation of start year.</param>
        /// <param name="endYear">Integer representation of end year.</param>
        public void AdjustRangeYear(ref int startYear, ref int endYear)
        {
            string input;
            bool isValid = false;
            while (!isValid)
            {
                Console.Write($"{Environment.NewLine}Starting year (1990 to 2019): ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out startYear))
                    Console.WriteLine($"ERROR: Ending year must be an integer between 1990 to 2019{Environment.NewLine}");
                else
                {
                    if (!(startYear >= 1990 && startYear <= 2019))
                        Console.WriteLine($"ERROR: Ending year must be an integer between 1990 to 2019{Environment.NewLine}");
                    else
                        isValid = true;
                } // end if-else block
            } // end while block


            int limitYear = startYear + 4 <= 2019 ? startYear + 4 : 2019;
            isValid = false;
            while (!isValid)
            {
                Console.Write($"{Environment.NewLine}Ending year ({startYear} to {limitYear}): ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out endYear))
                    Console.WriteLine($"ERROR: Ending year must be an integer between {startYear} to {limitYear}{Environment.NewLine}");
                else
                {
                    if (!(endYear >= 1990 && endYear <= 2019))
                        Console.WriteLine($"ERROR: Ending year must be an integer between {startYear} to {limitYear}{Environment.NewLine}");
                    else
                    {
                        if (!(endYear >= startYear && endYear <= limitYear))
                            Console.WriteLine($"ERROR: Ending year must be an integer between {startYear} to {limitYear}{Environment.NewLine}");
                        else
                            isValid = true;
                    } // end if-else block
                } // end if-else block
            } // end while block
        } // end AdjustRangeYear method


        /// <summary>
        /// Obtain the user input that represent the region name.
        /// </summary>
        /// <param name="regions"></param>
        /// <returns>String that represent the region name.</returns>
        public string GetRegionName(HashSet<string> regions)
        {
            int idx = 0;
            bool doneRegionOption = false;
            while (!doneRegionOption)
            {
                Console.Write($"{Environment.NewLine}Enter a region #: ");
                doneRegionOption = int.TryParse(Console.ReadLine(), out idx);
                if (!doneRegionOption)
                    Console.WriteLine($"Invalid input. Please select a number shown in the list above...");
                else if (idx <= 0 || idx > regions.Count)
                {
                    Console.WriteLine($"Invalid input. Please select a number shown in the list above...");
                    doneRegionOption = false;
                } // end if-else block
            } // end while

            return regions.ElementAt<string>(idx - 1);
        } // end GetRegionName method


        /// <summary>
        /// Obtain the user input that represent the source name.
        /// </summary>
        /// <param name="sources"></param>
        /// <returns>String that represent the source name.</returns>
        public string GetSourceName(HashSet<string> sources)
        {
            int idx = 0;
            bool doneRegionOption = false;
            while (!doneRegionOption)
            {
                Console.Write($"{Environment.NewLine}Enter a source #: ");
                doneRegionOption = int.TryParse(Console.ReadLine(), out idx);
                if (!doneRegionOption)
                    Console.WriteLine($"Invalid input. Please select a number shown in the list above...");
                else if (idx <= 0 || idx > sources.Count)
                {
                    Console.WriteLine($"Invalid input. Please select a number shown in the list above...");
                    doneRegionOption = false;
                } // end if-else block
            } // end while

            return sources.ElementAt<string>(idx - 1);
        } // end GetSourceName method

    } // end OptionHandler class
} // end namespace
