using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Shared
{
    public static class Comparator
    {
        public static List<string> ObterDiferencas<T, Y>(T objeto1, Y objeto2)
        {
            var diferencas = new List<string>();

            if (objeto1 == null || objeto2 == null)
                throw new ArgumentException("Ambos os objetos devem ser não nulos.");

            foreach (PropertyInfo propriedade in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var valor1 = propriedade.GetValue(objeto1);
                var valor2 = propriedade.GetValue(objeto2);

                if (valor1 == null && valor2 == null)
                    continue;

                if ((valor1 == null && valor2 != null) || (valor1 != null && valor2 == null) || !valor1.Equals(valor2))
                {
                    diferencas.Add($"Propriedade: {propriedade.Name}, Original: {valor1}, Alterado para: {valor2}");
                }
            }

            return diferencas;
        }
    }
}
