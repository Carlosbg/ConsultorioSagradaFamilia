using ConsultorioSagradaFamilia.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagradaFamilia3._0.Models
{
    public class StatusMessage
    {
        //0 = exito, 1 = error
        public int Status { get; set; }
        public string Mensaje { get; set; }
    }
    public class DBContext
    {
        public string ConnectionString = "Server=tcp:consultoriosagradafamilia.database.windows.net,1433;Initial Catalog=ConsultorioSagradaFamilia;Persist Security Info=False;User ID=sagradaFamilia;Password=Carlos.1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public SqlConnection connection;
        
        public DBContext()
        {
            connection = new SqlConnection
            {
                ConnectionString = ConnectionString
            };
        }

        public int GetMedicoIdByMail(string mail)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("select IdMedico from Medico where Mail = '" + mail + "'");

            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            int medicoId = 0;

            while (reader.Read())
            {
                medicoId = int.Parse(reader["IdMedico"].ToString());
            }

            connection.Close();

            return medicoId;
        }

        public int GetRol(string usuarioMail)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("select distinct AspNetRoles.Id from AspNetUsers " +

            "inner join AspNetUserRoles " +
            "on AspNetUsers.Id = AspNetUSerRoles.UserId " +
            "inner join AspNetRoles " +
            "on AspNetUserRoles.RoleId = AspNetRoles.Id " +

            "where Email = '"+ usuarioMail +"'");

            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            int rolId = 0;

            while (reader.Read())
            {
                rolId = int.Parse(reader["Id"].ToString());
            }

            connection.Close();

            return rolId;
        }

        public List<Rol> GetRoles()
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("Select * from AspNetRoles");
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<Rol> roles = new List<Rol>();

            while (reader.Read())
            {
                Rol rol = new Rol
                {
                    Nombre = reader["Name"].ToString(),
                };

                roles.Add(rol);
            }

            connection.Close();

            return roles;
        }

        public StatusMessage GuardarMedico(ConsultorioSagradaFamilia.Models.Medico medico)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "Médico creado"  };
            connection.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Medico]" +
           "([DNI],[Nombre],[Apellido],[Matricula],[CUIL],[Monto],[Mail],[Telefono],[FechaNacimiento],[Domicilio])" + 
           "VALUES ("+ medico.DNI + ",'" + medico.Nombre + "','" + medico.Apellido + "'," + medico.Matricula + "," + medico.CUIL + "," + medico.Monto +
           ",'" + medico.Mail + "'," + medico.Telefono + ",'" + medico.FechaNacimiento.Year + "-" + medico.FechaNacimiento.Month + "-" + medico.FechaNacimiento.Day 
           + "','" + medico.Domicilio + "')");

            try
            {
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                connection.Close();

                statusMessage.Status = 1;
                statusMessage.Mensaje = e.Message;
            }

            connection.Close();

            return statusMessage;
        }

        public StatusMessage EditarMedico(ConsultorioSagradaFamilia.Models.Medico medico)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "Médico editado" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Medico] SET " +
           "[DNI] = " + medico.DNI + ",[Nombre] = '" + medico.Nombre + "',[Apellido] = '" + medico.Apellido + "',[Matricula] = " + medico.Matricula + 
           ",[CUIL] = " + medico.CUIL + ",[Monto] = " + medico.Monto.ToString("n2").Replace(",",".") + ",[Mail] = '" + medico.Mail + "',[Telefono] = " + medico.Telefono + 
           ",[FechaNacimiento] = '" + medico.FechaNacimiento.Year + "-" + medico.FechaNacimiento.Month + "-" + medico.FechaNacimiento.Day + 
           "',[Domicilio] = '" + medico.Domicilio +
           "' WHERE [IdMedico] = " + medico.IdMedico);

            try
            {
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                connection.Close();

                statusMessage.Status = 1;
                statusMessage.Mensaje = e.Message;
            }

            connection.Close();

            return statusMessage;
        }

        public List<ConsultorioSagradaFamilia.Models.Medico> GetMedicos()
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("Select * from Medico");
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<ConsultorioSagradaFamilia.Models.Medico> medicos = new List<ConsultorioSagradaFamilia.Models.Medico>();

            while (reader.Read())
            {
                ConsultorioSagradaFamilia.Models.Medico medico = new ConsultorioSagradaFamilia.Models.Medico
                {
                    IdMedico = int.Parse(reader["IdMedico"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Apellido = reader["Apellido"].ToString(),
                    CUIL = reader["CUIL"].ToString(),
                    DNI = int.Parse(reader["DNI"].ToString()),
                    Matricula = int.Parse(reader["Matricula"].ToString()),
                    Monto = decimal.Parse(reader["Monto"].ToString()),
                    Domicilio = reader["Domicilio"].ToString(),
                    FechaNacimiento = (DateTime)reader["FechaNacimiento"],
                    Mail = reader["Mail"].ToString(),
                    Telefono = int.Parse(reader["Telefono"].ToString())
                };

                medicos.Add(medico);
            }

            connection.Close();

            return medicos;
        }

        public StatusMessage GuardarPaciente(Paciente paciente)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "Paciente creado" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Paciente]" +
            "([DNI],[Nombre],[Apellido],[FechaNacimiento],[Direccion])" +
            "VALUES (" + paciente.DNI + ",'" + paciente.Nombre + "','" + paciente.Apellido + "','" + paciente.FechaNacimiento.Year + "-" + paciente.FechaNacimiento.Month + "-" + 
            paciente.FechaNacimiento.Day + "','" + paciente.Direccion + "')");

            try
            {
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                connection.Close();

                statusMessage.Status = 1;
                statusMessage.Mensaje = e.Message;
            }

            connection.Close();

            return statusMessage;
        }

        public StatusMessage EditarPaciente(ConsultorioSagradaFamilia.Models.Paciente paciente)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "Paciente editado" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Paciente] SET " +
            "[DNI] = " + paciente.DNI + ",[Nombre] = '" + paciente.Nombre + "',[Apellido] = '" + paciente.Apellido + "',[FechaNacimiento] = '" + 
            paciente.FechaNacimiento.Year + "-" + paciente.FechaNacimiento.Month + "-" + paciente.FechaNacimiento.Day +
            "' WHERE [IdPaciente] = " + paciente.IdPaciente);

            try
            {
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                connection.Close();

                statusMessage.Status = 1;
                statusMessage.Mensaje = e.Message;
            }

            connection.Close();

            return statusMessage;
        }

        public List<Paciente> GetPacientes(int idMedico = 0)
        {
            connection.Open();
            SqlCommand cmd;
            if (idMedico == 0)
            {
                cmd = new SqlCommand("Select * from Paciente");
            }
            else
            {
                cmd = new SqlCommand(
                    "select * from PacienteMedico " +
                    "inner join Paciente " +
                    "on PacienteMedico.IdPaciente = Paciente.IdPaciente " +
                    "where PacienteMedico.IdMedico = " + idMedico);
            }

            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<Paciente> pacientes = new List<Paciente> ();

            while (reader.Read())
            {
                Paciente paciente = new Paciente
                {
                    IdPaciente = int.Parse(reader["IdPaciente"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Apellido = reader["Apellido"].ToString(),
                    DNI = int.Parse(reader["DNI"].ToString()),
                    Direccion = reader["Direccion"].ToString(),
                    FechaNacimiento = DateTime.Parse(reader["FechaNacimiento"].ToString())
                };

                pacientes.Add(paciente);
            }

            connection.Close();

            return pacientes;
        }

        public List<TurnosPorPaciente> GetTurnos()
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("Select * from TurnosPorPaciente");
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<TurnosPorPaciente> turnos = new List<TurnosPorPaciente>();

            while (reader.Read())
            {
                TurnosPorPaciente turno = new TurnosPorPaciente
                {
                    IdTurno = int.Parse(reader["IdTurno"].ToString()),
                    Atendido = bool.Parse(reader["Atendido"].ToString()),
                    Fecha = DateTime.Parse(reader["Fecha"].ToString()),
                    IdMedico = int.Parse(reader["IdMedico"].ToString()),
                    IdPaciente = int.Parse(reader["IdPaciente"].ToString()),
                    CUILMedico = reader["CUILMedico"].ToString(),
                    DNIMedico= int.Parse(reader["DNIMedico"].ToString()),
                    FechaString = reader["FechaString"].ToString(),
                    HoraString = reader["HoraString"].ToString(),
                    MatriculaMedico = int.Parse(reader["MatriculaMedico"].ToString()),
                    NombreMedico = reader["NombreMedico"].ToString(),
                    NombrePaciente = reader["NombrePaciente"].ToString()
                };

                turnos.Add(turno);
            }

            connection.Close();

            return turnos;
        }

        public StatusMessage GuardarPago(Pago pago)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "Pago creado" };
            connection.Open();

            SqlCommand cmd;

            if(pago.IdObraSocial != null)
            {
                cmd = new SqlCommand("INSERT INTO [dbo].[Pago]" +
                    "([IdFormaPago],[IdTurno],[Monto],[IdObraSocial])" +
                    "VALUES (" + pago.IdFormaPago + "," + pago.IdTurno + "," + pago.Monto + "," + pago.IdObraSocial + ")");
            }
            else if(pago.IdTarjeta != null)
            {
                cmd = new SqlCommand("INSERT INTO [dbo].[Pago]" +
                    "([IdFormaPago],[IdTurno],[Monto],[IdTarjeta])" +
                    "VALUES (" + pago.IdFormaPago + "," + pago.IdTurno + "," + pago.Monto + "," + pago.IdTarjeta + ")");
            }
            else
            {
                cmd = new SqlCommand("INSERT INTO [dbo].[Pago]" +
                    "([IdFormaPago],[IdTurno],[Monto])" +
                    "VALUES (" + pago.IdFormaPago + "," + pago.IdTurno + "," + pago.Monto + ")");
            }

            try
            {
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                connection.Close();

                statusMessage.Status = 1;
                statusMessage.Mensaje = e.Message;
            }

            connection.Close();

            return statusMessage;
        }

        public List<PagosPorFormaPago> GetPagos()
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("Select * from PagosPorFormaPago");
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<PagosPorFormaPago> pagos = new List<PagosPorFormaPago>();

            while (reader.Read())
            {
                PagosPorFormaPago pago = new PagosPorFormaPago
                {
                    IdFormaPago = int.Parse(reader["IdFormaPago"].ToString()),
                    IdObraSocial = ToNullableInt(reader["IdObraSocial"].ToString()),
                    Monto = decimal.Parse(reader["Monto"].ToString()),
                    Fecha = DateTime.Parse(reader["Fecha"].ToString()),
                    FormaPago = reader["FormaPago"].ToString(),
                    IdMedico = int.Parse(reader["IdMedico"].ToString()),
                    IdPaciente = int.Parse(reader["IdPaciente"].ToString()),
                    NombreMedico = reader["NombreMedico"].ToString(),
                    NombreObraSocial = reader["NombreObraSocial"].ToString(),
                    NombrePaciente = reader["NombrePaciente"].ToString()
                };

                pagos.Add(pago);
            }

            connection.Close();

            return pagos;
        }

        public static int? ToNullableInt(string s)
        {
            if (int.TryParse(s, out int i)) return i;
            return null;
        }

        public List<ObraSocial> GetObrasSociales()
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("Select * from ObraSocial");
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<ObraSocial> obrasSociales = new List<ObraSocial>();

            while (reader.Read())
            {
                ObraSocial obraSocial = new ObraSocial
                {
                    IdObraSocial = int.Parse(reader["IdObraSocial"].ToString()),
                    Nombre = reader["Nombre"].ToString()
                };

                obrasSociales.Add(obraSocial);
            }

            connection.Close();

            return obrasSociales;
        }

        public List<Especialidad> GetEspecialidades()
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("Select * from Especialidad");
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<Especialidad> especialidades = new List<Especialidad>();

            while (reader.Read())
            {
                Especialidad especialidad = new Especialidad
                {
                    IdEspecialidad = int.Parse(reader["IdEspecialidad"].ToString()),
                    Nombre = reader["Nombre"].ToString()
                };

                especialidades.Add(especialidad);
            }

            connection.Close();

            return especialidades;
        }
        
        public List<FormaPago> GetFormasPago()
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("Select * from FormaPago");
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<FormaPago> formasPago = new List<FormaPago>();

            while (reader.Read())
            {
                FormaPago formaPago = new FormaPago
                {
                    IdFormaPago = int.Parse(reader["IdFormaPago"].ToString()),
                    Nombre = reader["Nombre"].ToString()                    
                };

                formasPago.Add(formaPago);
            }

            connection.Close();

            return formasPago;
        }

        public List<Banco> GetBancos()
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("Select * from Banco");
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<Banco> bancos = new List<Banco>();

            while (reader.Read())
            {
                Banco banco = new Banco
                {
                    IdBanco = int.Parse(reader["IdBanco"].ToString()),
                    Nombre = reader["Nombre"].ToString()
                };

                bancos.Add(banco);
            }

            connection.Close();

            return bancos;
        }

        public StatusMessage GuardarHistoriaClinica(ConsultorioSagradaFamilia.Models.HistoriaClinica historiaClinica)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "Historia clínica creada" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[HistoriaClinica]" +
            "([IdMedico],[IdPaciente],[Fecha],[Observaciones]) " +
            "VALUES (" + DatosUsuario.IdUsuario + "," + historiaClinica.IdPaciente + ",'" + historiaClinica.Fecha.Year + "-" + historiaClinica.Fecha.Month + "-" +
            historiaClinica.Fecha.Day + "','" + historiaClinica.Observaciones + "')");

            try
            {
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                connection.Close();

                statusMessage.Status = 1;
                statusMessage.Mensaje = e.Message;
            }

            connection.Close();

            return statusMessage;
        }

        public List<HistoriaClinica> GetHistoriasClinicas(int pacienteId)
        {
            connection.Open();            

            SqlCommand cmd = new SqlCommand("Select * from HistoriaClinica where IdPaciente=" + pacienteId);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<HistoriaClinica> historiaClinicas = new List<HistoriaClinica>();

            while (reader.Read())
            {
                HistoriaClinica historiaClinica = new HistoriaClinica
                {
                    IdPaciente = int.Parse(reader["IdPaciente"].ToString()),
                    IdHistoriaClinica = int.Parse(reader["IdHistoriaClinica"].ToString()),
                    Observaciones = reader["Observaciones"].ToString(),
                    IdMedico = int.Parse(reader["IdMedico"].ToString()),
                    Fecha = DateTime.Parse(reader["Fecha"].ToString())
                };

                historiaClinicas.Add(historiaClinica);
            }

            connection.Close();

            return historiaClinicas;
        }

        public List<Usuario> GetUsuarios()
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand(
                "select  AspNetUsers.Id, AspNetUsers.Email, AspNetRoles.Name from AspNetUsers " +
                "inner join AspNetUserRoles " +
                "on AspNetUsers.Id = AspNetUSerRoles.UserId " +
                "inner join AspNetRoles " +
                "on AspNetUserRoles.RoleId = AspNetRoles.Id");
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<Usuario> usuarios = new List<Usuario>();

            while (reader.Read())
            {
                Usuario usuario = new Usuario
                {
                    Id = reader["Id"].ToString(),
                    Email = reader["Email"].ToString(),
                    Rol = reader["Name"].ToString(),
                };

                usuarios.Add(usuario);
            }

            connection.Close();

            return usuarios;
        }

        public List<ObraSocial> GetObrasSocialesPorPaciente(int idPaciente)
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand(
                "select  p.IdPaciente, os.IdObraSocial, os.Nombre from ObraSocialPaciente as osp " +
                "inner join Paciente as p " +
                "on osp.IdPaciente = p.IdPaciente " +
                "inner join ObraSocial as os " +
                "on osp.IdObraSocial = os.IdObraSocial " +
                "where p.IdPaciente="+ idPaciente);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<ObraSocial> obrasSociales = new List<ObraSocial>();

            while (reader.Read())
            {
                ObraSocial obraSocial = new ObraSocial
                {
                    IdObraSocial = int.Parse(reader["IdObraSocial"].ToString()),
                    Nombre = reader["Nombre"].ToString()
                };

                obrasSociales.Add(obraSocial);
            }

            connection.Close();

            return obrasSociales;
        }

        public List<Tarjeta> GetTarjetasPorPaciente(int idPaciente)
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand(
                "select  p.IdPaciente, t.IdTarjeta, t.Nombre, t.Numero from PacienteTarjeta as pt " +
                "inner join Paciente as p " +
                "on pt.IdPaciente = p.IdPaciente " +
                "inner join Tarjeta as t " +
                "on pt.IdTarjeta = t.IdTarjeta " +
                "where p.IdPaciente=" + idPaciente);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<Tarjeta> tarjetas = new List<Tarjeta>();

            while (reader.Read())
            {
                Tarjeta tarjeta = new Tarjeta
                {
                    IdTarjeta  = int.Parse(reader["IdTarjeta"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Numero = reader["Numero"].ToString()
                };

                tarjetas.Add(tarjeta);
            }

            connection.Close();

            return tarjetas;
        }
    }
}
