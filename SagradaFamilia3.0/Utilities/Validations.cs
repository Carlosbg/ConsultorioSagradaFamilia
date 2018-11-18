using SagradaFamilia3._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SagradaFamilia3._0.Utilities
{
    static class Validations
    {
        public static string ValidarSoloTexto(string texto)
        {
            if (texto.Count() != 0)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(texto, "^[a-zA-Z ]*$"))
                {
                    MessageBox.Show("Este campo solo puede tener letras");
                    if (texto.Count() > 1)
                    {
                        for (int i = 0; i < texto.Length; i++)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(texto[i].ToString(), "^[a-zA-Z ]*$"))
                            {
                                char car = texto[i];
                                texto = texto.Replace(car.ToString(), "");
                            }
                        }
                    }
                    else
                    {
                        texto = "";
                    }
                }
            }

            return texto;
        }
    }
}
