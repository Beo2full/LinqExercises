﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LinQExercises
{
    internal class ExcerciesW3
    {
        List<string> food_choices;
        Func<string, char, string> _diff;
        public ExcerciesW3(Func<string, char, string> diff)
        {
            food_choices = new List<string>();
            this._diff = diff;
        }

        public void thirty()
        {

            //Console.WriteLine("--------------- Alphabetically Ordered Food Choices ---------------");
            this._diff("Alphabetically Ordered Food Choices", '-');
            food_choices.Add("Honey");
            food_choices.Add("Butter");
            food_choices.Add("Biscuit");
            food_choices.Add("Brade");
            food_choices.Add("Honey");
            food_choices.Add("Butter");
            food_choices.Add("Biscuit");
            food_choices.Add("Brade");
            food_choices.Add("Honey");
            food_choices.Add("Butter");
            food_choices.Add("Biscuit");
            food_choices.Add("Brade");
            var distinct_food_choices = food_choices.Distinct().ToList();

            distinct_food_choices.Sort();
            foreach (string element in distinct_food_choices)
            {
                System.Console.WriteLine(element);
            }
            System.Console.ReadKey();
        }

        public void tewntyNine()
        {
            Console.WriteLine("--------------- Cities Grouped by Index  ---------------");
            string[] cities =
            {
            "ROME","LONDON","NAIROBI","CALIFORNIA",
            "ZURICH","NEW DELHI","AMSTERDAM",
            "ABU DHABI", "PARIS","NEW YORK"
            };


            var citySplit = from i in Enumerable.Range(0, cities.Length)
                            group cities[i] by i / 3;

            foreach (var city in citySplit)
            {
                string cityMetro = string.Join(";  ", city.ToArray());
                Console.WriteLine(cityMetro);
                Console.WriteLine("-- here is a group of cities --\n");
            }
            Console.ReadKey();
        }
    }
}