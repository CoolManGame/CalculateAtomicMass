using System;
using System.Collections.Generic;

namespace Chemistree
{
    static class Chemistree
    {
        public static double CalcMol(string input)
        {
            input = input.Replace("He", "E").Replace("Li", "L").Replace("Be", "^").Replace("Ne", "+");
            input = input.Replace("Na", "[").Replace("Mg", "]").Replace("Al", ".").Replace("Si", ",");
            input = input.Replace("Cl", "!").Replace("Ar", "A").Replace("Ca", "G").Replace("Cr", "=");
            input = input.Replace("Mn", "~").Replace("Fe", "`").Replace("Cu", "_").Replace("Zn", "Z");
            input = input.Replace("Br", "{").Replace("Ag", "}").Replace("Ba", "|").Replace("Hg", "*");
            input = input.Replace("Pb", "$");

            input = Translate(input);
            double result = 0;
            Dictionary<char, double> hold = new Dictionary<char, double>();
            hold.Add('C', 12); // C for Carbon (C)
            hold.Add('H', 1);  // H for Hidro (H)
            hold.Add('O', 16);  // O for Oxygen (O)
            hold.Add('E', 4); // E for Helium (He)
            hold.Add('L', 7); // L for Liti (Li)
            hold.Add('^', 9); // ^ for Beri (Be)
            hold.Add('B', 11); // B for Bo (B)
            hold.Add('N', 14); // N for Nitrogen (N)
            hold.Add('F', 19); // F for Flo (F)
            hold.Add('+', 20); // + for Neon (Ne)
            hold.Add('[', 23); // [ for Natri (Na)
            hold.Add(']', 24); // ] for Magie (Mg)
            hold.Add('.', 27); // . for Aluminum (Al)
            hold.Add(',', 28); // , for Silicon (Si)
            hold.Add('P', 31);  // P for Photpho (P)
            hold.Add('S', 32);  // S for Sulfur (S)
            hold.Add('!', 35.5);  // L for Clo (Cl)
            hold.Add('A', 39.9); // A for Agon (Ar)
            hold.Add('K', 39);  // K for Kali (K)
            hold.Add('G', 40);  // G for Canxi (Ca)
            hold.Add('=', 52);  // = for Chrome (Cr)
            hold.Add('~', 55);  // ~ for Mangan (Mn)
            hold.Add('`', 56); // ` for Iron (Fe)
            hold.Add('_', 64); // _ for Bronze (Cu)
            hold.Add('Z', 65);  // Z for Zinc (Zn)
            hold.Add('{', 80);  // { for Brom (Br)
            hold.Add('}', 108);  // } for Silver (Ag)
            hold.Add('|', 137); // | for Bari (Ba)
            hold.Add('*', 201); // * for Mercury (Hg);
            hold.Add('$', 207); // $ for Lead (Pb);

            foreach (char c in input)
            {
                result += hold[c];
            }

            return result;
        }

        private static string Translate (string s)
        {
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] == ')' && Char.IsDigit(s[i + 1]))
                {
                    for (int j = i + 1; j < s.Length; j++)
                    {
                        if (!Char.IsDigit(s[j]))
                        {
                            s = s.Insert(j, "(");
                            break;
                        }
                    }
                }
            }

            string[] parts = RemoveStartTrash(s.Split('('));


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

            string[] numbers = RemoveStartTrash(s.Split(new char[] { 'C', 'H', 'O', 'E', 'L', '^', 'B', 'N', 
                'F', '+', '[', ']', '.', '.', 'P', 'S', '!', 'A', 'K', 'G', '=', '~', '`', '_', 'Z', '{', '}', '|', '*', '$'            }));

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
