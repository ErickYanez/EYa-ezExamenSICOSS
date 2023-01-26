using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Login(string userName)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezSicossContext context = new DL.EyañezSicossContext())
                {
                    var item = context.Usuarios.FromSqlRaw($"UsuarioLogin '{userName}'").AsEnumerable().FirstOrDefault();

                    if (item != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = item.IdUsuario;
                        usuario.UserName = item.UserName;
                        usuario.Password = item.Password;                       

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Message = "Usuario no encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error: "+ result.Ex;
            }
            return result;
        }

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezSicossContext context = new DL.EyañezSicossContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.UserName}','{usuario.Password}'");

                    if (query > 0)
                    {
                        result.Message = "Se inserto el usuario correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al insertar el usuario" + result.Ex;
            }
            return result;
        }
    }
}