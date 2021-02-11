using System;
using System.Collections.Generic;

namespace Chemistree
{
    static class Chemistree
    {
        public static double CalcMol(this string input, Dictionary<string, double> data = null)
        {
            Dictionary<string, char> ToDealWithCustomData = new Dictionary<string, char>();
            ToDealWithCustomData.Add("He", 'E'); ToDealWithCustomData.Add("Li", 'L'); ToDealWithCustomData.Add("Be", '^');
            ToDealWithCustomData.Add("Ne", '+'); ToDealWithCustomData.Add("Na", '['); ToDealWithCustomData.Add("Mg", ']');
            ToDealWithCustomData.Add("Al", '.'); ToDealWithCustomData.Add("Si", ','); ToDealWithCustomData.Add("Cl", '!');
            ToDealWithCustomData.Add("Ar", 'A'); ToDealWithCustomData.Add("Ca", 'G'); ToDealWithCustomData.Add("Cr", '=');
            ToDealWithCustomData.Add("Mn", '~'); ToDealWithCustomData.Add("Fe", '`'); ToDealWithCustomData.Add("Cu", '_');
            ToDealWithCustomData.Add("Zn", 'Z'); ToDealWithCustomData.Add("Br", '{'); ToDealWithCustomData.Add("Ag", '}');
            ToDealWithCustomData.Add("Ba", '|'); ToDealWithCustomData.Add("Hg", '*'); ToDealWithCustomData.Add("Pb", '$');

            input = input.Replace("He", "E").Replace("Li", "L").Replace("Be", "^").Replace("Ne", "+");
            input = input.Replace("Na", "[").Replace("Mg", "]").Replace("Al", ".").Replace("Si", ",");
            input = input.Replace("Cl", "!").Replace("Ar", "A").Replace("Ca", "G").Replace("Cr", "=");
            input = input.Replace("Mn", "~").Replace("Fe", "`").Replace("Cu", "_").Replace("Zn", "Z");
            input = input.Replace("Br", "{").Replace("Ag", "}").Replace("Ba", "|").Replace("Hg", "*");
            input = input.Replace("Pb", "$");

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

            if (data != null) 
            {
                foreach (var item in data)
                {
                    var Key = item.Key;
                    var Val = item.Value;

                    hold[ToDealWithCustomData[Key]] = Val;
                }
            }
            

            return CalculateHelper(input, hold);
        }

        public static double CalculateHelper(string s, Dictionary<char, double> hold)
        {
            double res0 = 0;
            List<char> list = new List<char>(s);
            for (int i = 0; i < list.Count; i++)
            {
                if (!Char.IsDigit(list[i]) && list[i] != '(') // if it's an element
                {
                    string times = "";
                    char element = list[i];
                    times = "";
                    for (int ei = i+1; ei < list.Count; ei++)
                    {
                        if (Char.IsDigit(list[ei])) { times += list[ei]; }
                        else { break; }
                    }

                    res0 += hold[element] * int.Parse(times.Length != 0 ? times : "1");
                    i += times.Length;
                }

                else if (list[i] == '(')
                {
                    int l = i;
                    int r = i;
                    int help = 1;

                    while (true)
                    {
                        r++;
                        if (list[r] == '(') { help++; }
                        else if (list[r] == ')')
                        {
                            if (help == 1) { break; }
                            else { help--; }
                        }
                    }
                    string times = "";
                    for (int ei = r+1; ei < list.Count; ei++)
                    {
                        if (Char.IsDigit(list[ei])) { times += list[ei]; }
                        else { break; }
                    }

                    res0 += CalculateHelper(string.Concat(list.GetRange(l + 1, r - l - 1)), hold) * (times.Length != 0 ? int.Parse(times) : 1);
                    i = r + times.Length;
                }
            }

            return res0;
        }
    }
}
