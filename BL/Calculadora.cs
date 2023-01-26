using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BL
{
    public class Calculadora
    {
        public static int SumarDigitos(string numero)
        {
            do
            {
                int sum = 0;
                for (int i = 0; i < numero.Length; i++)
                {
                    sum += Convert.ToInt32(numero.Substring(i, 1));
                }
                numero = sum.ToString();
            }
            while (int.Parse(numero) > 10);  
            
            return int.Parse(numero);
        }

        public static ML.Result GetAll(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezSicossContext context = new DL.EyañezSicossContext())
                {             
                    var query = context.Calculadoras.FromSqlRaw($"CalculadoraGetAll '{idUsuario}'").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Calculadora calculadora = new ML.Calculadora();

                            calculadora.IdCalculadora = item.IdCalculadora;
                            calculadora.Numero = item.Numero.ToString();
                            calculadora.Resultado = item.Resultado.Value;
                            calculadora.FechaHora = item.FechaHora.ToString();

                            calculadora.Usuario = new ML.Usuario();
                            calculadora.Usuario.IdUsuario = item.IdUsuario.Value;

                            result.Objects.Add(calculadora);
                        }
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar el historial" + result.Ex;
            }

            return result;
        }

        public static ML.Result ActualizarFecha(ML.Calculadora calculadora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezSicossContext context = new DL.EyañezSicossContext())
                {
                    var item = context.Calculadoras.FromSqlRaw($"ActualizarFecha '{calculadora.Numero}'").AsEnumerable().SingleOrDefault();

                    if (item != null)
                    {
                        ML.Calculadora calculadoraa = new ML.Calculadora();

                        calculadoraa.IdCalculadora = item.IdCalculadora;
                        calculadoraa.Numero = item.Numero.ToString();
                        calculadoraa.Resultado = item.Resultado.Value;
                        calculadoraa.FechaHora = item.FechaHora.ToString();

                        calculadoraa.Usuario = new ML.Usuario();
                        calculadoraa.Usuario.IdUsuario = item.IdUsuario.Value;

                        result.Object = calculadoraa;
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al actualizar la fecha" + result.Ex;
            }

            return result;
        }

        public static ML.Result VerificarNumero(string numero)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezSicossContext context = new DL.EyañezSicossContext())
                {
                    var item = context.Calculadoras.FromSqlRaw($"VerificarNumero '{numero}'").AsEnumerable().FirstOrDefault();

                    if (item.Existe > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al buscar el numero" + result.Ex;
            }

            return result;
        }

        public static ML.Result Add(ML.Calculadora calculadora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezSicossContext context = new DL.EyañezSicossContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"CalculadoraAdd '{calculadora.Usuario.IdUsuario}','{calculadora.Numero}','{calculadora.Resultado}'");

                    if (query > 0)
                    {
                        result.Message = "Se inserto el resultado correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al insertar el resultado" + result.Ex;
            }
            return result;
        }

        public static ML.Result Delete(int idUsuario, int idCalculadora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezSicossContext context = new DL.EyañezSicossContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"CalculadoraDelete '{idUsuario}','{idCalculadora}'");

                    if (query > 0)
                    {
                        result.Message = "Se elimino el dato correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al eliminar el dato" + result.Ex;
            }
            return result;
        }
    }
}
