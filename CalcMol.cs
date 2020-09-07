using System;
using System.Collections.Generic;

namespace CalcMol
{
    static class MyBoy
    {
        public static double CalcMol(string input)
        {
            input = Translate(input);
            double result = 0;
            Dictionary<char, int> hold = new Dictionary<char, int>();
            hold.Add('C', 12);
            hold.Add('H', 1);
            hold.Add('O', 16);

            foreach (char c in input)
            {
                result += hold[c];
            }

            return result;
        }

        private static string Translate (string s)
        {
            string[] parts = RemoveStartTrash(s.Split('('));

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].EndsWith(')')) { parts[i] += '1'; }
            }


            string result = "";

            foreach (var part in parts)
            {
                result += Translate2(part);
            }

            result = Translate3(result);

            return result;

        }

        private static string Translate2 (string s)
        {
            if (!s.Contains(')')) { return s; }

            int multiply = int.Parse(s.Substring(s.IndexOf(')')+1));

            string thePart = s.Substring(0, s.IndexOf(')'));
            string result = "";

            for (int i = 0; i < multiply; i++)
            {
                result += thePart;
            }

            return result;
        }

        private static string Translate3(string s)
        {
            int numberNum = 0;

            string result = "";

            string[] numbers = RemoveStartTrash(s.Split(new char[] { 'C', 'H', 'O' }));

            for (int i = 0; i < s.Length; i++)
            {
                if (!Char.IsDigit(s[i])) { result += s[i]; }

                else
                {
                    string number = numbers[numberNum];

                    for (int j = 0; j < int.Parse(number)-1; j++)
                    {
                        result += s[i - 1];
                    }

                    s = RemoveFirst(s, number);
                    i--;

                    numberNum++;
                }
            }

            return result;
        }

        private static string[] RemoveStartTrash(string[] s)
        {
            List<string> list = new List<string>();
            list.AddRange(s);

            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals("")) { list.Remove(""); i = -1; }
            }

            return list.ToArray();
        }

        private static string RemoveFirst(string input, string kill)
        {
            int index = input.IndexOf(kill);

            return input.Remove(index, kill.Length);
        }
    }
}
