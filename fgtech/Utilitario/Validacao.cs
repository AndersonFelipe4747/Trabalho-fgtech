using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace fgtech.Utilitario
{
    public static class Validacao
    {
        public static bool ValidarCNPJ(string cnpj)
        {
            cnpj = Regex.Replace(cnpj ?? "", @"[^\d]", ""); //para o cnpj seguir o formato original
                 
            if (cnpj.Length != 14 || new string(cnpj[0], 14) == cnpj)
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;
            tempCnpj += resto;

            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;
            tempCnpj += resto;

            return cnpj.EndsWith(tempCnpj.Substring(12));
        }
    }
}
