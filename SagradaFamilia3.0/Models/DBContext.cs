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
           "([DNI],[Nombre],[Apellido],[Matricula],[CUIL],[Monto],[Mail],[Telefono],[FechaNacimiento],[Domicilio],[Habilitado])" + 
           "VALUES ("+ medico.DNI + ",'" + medico.Nombre + "','" + medico.Apellido + "'," + medico.Matricula + "," + medico.CUIL + "," + medico.Monto +
           ",'" + medico.Mail + "'," + medico.Telefono + ",'" + medico.FechaNacimiento.Year + "-" + medico.FechaNacimiento.Month + "-" + medico.FechaNacimiento.Day 
           + "','" + medico.Domicilio + "', 1)");

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
           "',[Domicilio] = '" + medico.Domicilio + "',[Habilitado] = " + (medico.Habilitado ? "1" : "0") +
           " WHERE [IdMedico] = " + medico.IdMedico);

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
                    Telefono = int.Parse(reader["Telefono"].ToString()),
                    Habilitado = bool.Parse(reader["Habilitado"].ToString())
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
                    Nombre = reader["Nombre"].ToString(),
                    Habilitada = bool.Parse(reader["Habilitada"].ToString())
                };

                obrasSociales.Add(obraSocial);
            }

            connection.Close();

            return obrasSociales;
        }

        public StatusMessage GuardarObraSocial(ObraSocial obraSocial)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "Obra social creada" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[ObraSocial]" +
            "([Nombre], [Habilitada])" +
            "VALUES ('" + obraSocial.Nombre + "', 1)");

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

        public StatusMessage EditarObraSocial(ConsultorioSagradaFamilia.Models.ObraSocial obraSocial)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "Obra social editada" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[ObraSocial] SET " +
            "[Nombre] = '" + obraSocial.Nombre + "',[Habilitada] = " + (obraSocial.Habilitada ? "1" : "0") +
            " WHERE [IdObraSocial] = " + obraSocial.IdObraSocial);

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
                    Nombre = reader["Nombre"].ToString(),
                    Habilitada = bool.Parse(reader["Habilitada"].ToString())
                };

                especialidades.Add(especialidad);
            }

            connection.Close();

            return especialidades;
        }

        public StatusMessage GuardarEspecialidad(Especialidad especialidad)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "Especialidad creada" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Especialidad]" +
            "([Nombre], [Habilitada])" +
            "VALUES ('" + especialidad.Nombre + "', 1)");

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

        public StatusMessage EditarEspecialidad(ConsultorioSagradaFamilia.Models.Especialidad especialidad)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "Especialidad editada" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Especialidad] SET " +
            "[Nombre] = '" + especialidad.Nombre  + "', [Habilitada] = " + (especialidad.Habilitada ? "1" : "0") +
            " WHERE [IdEspecialidad] = " + especialidad.IdEspecialidad);

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

        public StatusMessage GuardarBanco(Banco banco)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "Banco creado" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Banco]" +
            "([Nombre])" +
            "VALUES ('" + banco.Nombre + "')");

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

        public StatusMessage EditarBanco(ConsultorioSagradaFamilia.Models.Banco banco)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "Banco editado" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Banco] SET " +
            "[Nombre] = '" + banco.Nombre +
            "' WHERE [IdBanco] = " + banco.IdBanco);

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

        public List<Tarjeta> GetTarjetas()
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Tarjeta");
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<Tarjeta> tarjetas = new List<Tarjeta>();

            while (reader.Read())
            {
                Tarjeta tarjeta = new Tarjeta
                {
                    IdTarjeta = int.Parse(reader["IdTarjeta"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Numero = reader["Numero"].ToString(),
                    IdBanco = int.Parse(reader["IdBanco"].ToString())
                };

                tarjetas.Add(tarjeta);
            }

            connection.Close();

            return tarjetas;
        }


        //public StatusMessage GuardarBanco(Banco banco)
        //{
        //    StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "Banco creado" };
        //    connection.Open();

        //    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Banco]" +
        //    "([Nombre])" +
        //    "VALUES ('" + banco.Nombre + "')");

        //    try
        //    {
        //        cmd.Connection = connection;
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    {
        //        connection.Close();

        //        statusMessage.Status = 1;
        //        statusMessage.Mensaje = e.Message;
        //    }

        //    connection.Close();

        //    return statusMessage;
        //}

        public StatusMessage GuardarMedicoEspecialidad(MedicoEspecialidad medicoEspecialidad)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[MedicoEspecialidad]" +
            "([IdMedico], [IdEspecialidad])" +
            "VALUES (" + medicoEspecialidad.IdMedico+ ", " + medicoEspecialidad.IdEspecialidad +")");

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

        public StatusMessage BorrarMedicoEspecialidad(MedicoEspecialidad medicoEspecialidad)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[MedicoEspecialidad] " +
                "WHERE [IdMedico]=" + medicoEspecialidad.IdMedico + " and " + "[IdEspecialidad]=" + medicoEspecialidad.IdEspecialidad);

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

        public StatusMessage GuardarObraSocialMedico(ObraSocialMedico obraSocialMedico)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[ObraSocialMedico]" +
            "([IdMedico], [IdObraSocial], [Habilitado])" +
            "VALUES (" + obraSocialMedico.IdMedico + ", " + obraSocialMedico.IdObraSocial + ",1)");

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

        public StatusMessage BorrarObraSocialMedico(ObraSocialMedico obraSocialMedico)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[ObraSocialMedico] " +
                "WHERE [IdMedico]=" + obraSocialMedico.IdMedico + " and " + "[IdObraSocial]=" + obraSocialMedico.IdObraSocial);

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

        public StatusMessage GuardarHorarioAtencion(HorarioAtencion horarioAtencion)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[HorarioAtencion]" +
            "([IdMedico], [IdDia], [HorarioInicio], [HorarioFinal], [Habilitado])" +
            "VALUES (" + horarioAtencion.IdMedico + ", " + horarioAtencion.IdDia + ", '" + horarioAtencion.HorarioInicio.Hours +":"+ 
            horarioAtencion.HorarioInicio.Minutes + ":00', '"+ horarioAtencion.HorarioFinal.Hours + ":" +
            horarioAtencion.HorarioFinal.Minutes + ":00', 1)");

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

        public StatusMessage EditarHorarioAtencion(HorarioAtencion horarioAtencion)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[HorarioAtencion] " +
            "set [IdMedico] = " + horarioAtencion.IdMedico +
            ",   [IdDia] = " + horarioAtencion.IdDia +
            ",   [HorarioInicio] = '" + horarioAtencion.HorarioInicio.Hours + ":" + horarioAtencion.HorarioInicio.Minutes + ":00'" +
            ",   [HorarioFinal] = '" + horarioAtencion.HorarioFinal.Hours + ":" + horarioAtencion.HorarioFinal.Minutes + ":00'" +
            ",   [Habilitado] = " + (horarioAtencion.Habilitado ? "1" : "0") +
            " WHERE [IdHorarioAtencion] = " + horarioAtencion.IdHorarioAtencion);

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


        public ConsultorioSagradaFamilia.Models.Medico GetLastMedico()
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "" };
            connection.Open();

            SqlCommand cmd = new SqlCommand(" SELECT top 1 * FROM Medico order by IdMedico desc");

            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            ConsultorioSagradaFamilia.Models.Medico medico = new ConsultorioSagradaFamilia.Models.Medico();

            while (reader.Read())
            {
                medico = new ConsultorioSagradaFamilia.Models.Medico
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
                    Telefono = int.Parse(reader["Telefono"].ToString()),
                    Habilitado = bool.Parse(reader["Habilitado"].ToString())
                };
            }

            connection.Close();

            return medico;
        }

        public List<Especialidad> GetEspecialidadesPorMedico(int idMedico)
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("select * from Especialidad as e inner join MedicoEspecialidad as me on e.IdEspecialidad = me.IdEspecialidad " +
                                            "where me.IdMedico = " + idMedico);

            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<Especialidad> especialidades = new List<Especialidad>();

            while (reader.Read())
            {
                Especialidad especialidad = new Especialidad
                {
                    IdEspecialidad = int.Parse(reader["IdEspecialidad"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Habilitada = bool.Parse(reader["Habilitada"].ToString())
                };

                especialidades.Add(especialidad);
            }

            connection.Close();

            return especialidades;
        }

        public List<ObraSocial> GetObraSocialesPorMedico(int idMedico)
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("select * from ObraSocial as e inner join ObraSocialMedico as me on e.IdObraSocial = me.IdObraSocial " +
                                            "where me.IdMedico = " + idMedico);

            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<ObraSocial> obraSociales = new List<ObraSocial>();

            while (reader.Read())
            {
                ObraSocial obraSocial = new ObraSocial
                {
                    IdObraSocial = int.Parse(reader["IdObraSocial"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Habilitada = bool.Parse(reader["Habilitada"].ToString())
                };

                obraSociales.Add(obraSocial);
            }

            connection.Close();

            return obraSociales;
        }

        public List<ObraSocial> GetObraSocialesPorPaciente(int idPaciente)
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("select * from ObraSocial as e inner join ObraSocialPaciente as me on e.IdObraSocial = me.IdObraSocial " +
                                            "where me.IdPaciente = " + idPaciente);

            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<ObraSocial> obraSociales = new List<ObraSocial>();

            while (reader.Read())
            {
                ObraSocial obraSocial = new ObraSocial
                {
                    IdObraSocial = int.Parse(reader["IdObraSocial"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Habilitada = bool.Parse(reader["Habilitada"].ToString())
                };

                obraSociales.Add(obraSocial);
            }

            connection.Close();

            return obraSociales;
        }

        public StatusMessage BorrarObraSocialPaciente(ObraSocialPaciente obraSocialPaciente)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[ObraSocialPaciente] " +
                "WHERE [IdPaciente]=" + obraSocialPaciente.IdPaciente + " and " + "[IdObraSocial]=" + obraSocialPaciente.IdObraSocial);

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

        public StatusMessage GuardarObraSocialPaciente(ObraSocialPaciente obraSocialPaciente)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[ObraSocialPaciente]" +
            "([IdPaciente], [IdObraSocial], [Habilitado])" +
            "VALUES (" + obraSocialPaciente.IdPaciente + ", " + obraSocialPaciente.IdObraSocial + ",1)");

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

        public List<HorarioAtencion> GetHorariosAtencionPorMedico(int idMedico)
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("select * from HorarioAtencion as e where e.IdMedico = " + idMedico + " order by e.Habilitado desc");

            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            List<HorarioAtencion> horariosAtencion = new List<HorarioAtencion>();

            while (reader.Read())
            {
                HorarioAtencion horarioAtencion = new HorarioAtencion
                {
                    Habilitado = bool.Parse(reader["Habilitado"].ToString()),
                    HorarioFinal = TimeSpan.Parse(reader["HorarioFinal"].ToString()),
                    HorarioInicio = TimeSpan.Parse(reader["HorarioInicio"].ToString()),
                    IdDia = int.Parse(reader["IdDia"].ToString()),
                    IdMedico = int.Parse(reader["IdMedico"].ToString()),
                    IdHorarioAtencion = int.Parse(reader["IdHorarioAtencion"].ToString())
                };

                horariosAtencion.Add(horarioAtencion);
            }

            connection.Close();

            return horariosAtencion;
        }

        public ConsultorioSagradaFamilia.Models.Paciente GetLastPaciente()
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "" };
            connection.Open();

            SqlCommand cmd = new SqlCommand(" SELECT top 1 * FROM Paciente order by IdPaciente desc");

            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            ConsultorioSagradaFamilia.Models.Paciente paciente = new ConsultorioSagradaFamilia.Models.Paciente();

            while (reader.Read())
            {
                paciente = new ConsultorioSagradaFamilia.Models.Paciente
                {
                    IdPaciente = int.Parse(reader["IdPaciente"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Apellido = reader["Apellido"].ToString(),
                    DNI = int.Parse(reader["DNI"].ToString()),
                    Direccion = reader["Direccion"].ToString(),
                    FechaNacimiento = DateTime.Parse(reader["FechaNacimiento"].ToString())
                };
            }

            connection.Close();

            return paciente;
        }

        public void MarcarTurnoAtendido(int idTurno)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Turno set Atendido=1 where IdTurno=" + idTurno);

            cmd.Connection = connection;
            cmd.ExecuteNonQuery();            

            connection.Close();            
        }

        public ConsultorioSagradaFamilia.Models.Tarjeta GetLastTarjeta()
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("SELECT top 1 * FROM Tarjeta order by IdTarjeta desc");

            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();
            ConsultorioSagradaFamilia.Models.Tarjeta tarjeta = new ConsultorioSagradaFamilia.Models.Tarjeta();

            while (reader.Read())
            {
                tarjeta = new ConsultorioSagradaFamilia.Models.Tarjeta
                {
                    IdTarjeta = int.Parse(reader["IdTarjeta"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Numero = reader["Numero"].ToString(),
                    IdBanco = int.Parse(reader["IdBanco"].ToString())
                };
            }

            connection.Close();

            return tarjeta;
        }

        public StatusMessage GuardarTarjeta(Tarjeta tarjeta)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "Tarjeta creada" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Tarjeta]" +
            "([Nombre], [Numero], [IdBanco])" +
            "VALUES ('" + tarjeta.Nombre + "'," + tarjeta.Numero + "," + tarjeta.IdBanco +")");

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

        public StatusMessage GuardarPacienteTarjeta(PacienteTarjeta pacienteTarjeta)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[PacienteTarjeta]" +
            "([IdPaciente], [IdTarjeta])" +
            "VALUES (" + pacienteTarjeta.IdPaciente + ", " + pacienteTarjeta.IdTarjeta + ")");

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

        public void GuardarPacienteMedico(PacienteMedico pacienteMedico)
        {
            StatusMessage statusMessage = new StatusMessage { Status = 0, Mensaje = "" };
            connection.Open();

            SqlCommand cmd = new SqlCommand("SELECT * from PacienteMedico WHERE IdPaciente=" + pacienteMedico.IdPaciente +
                                            "and IdMedico=" + pacienteMedico.IdMedico);

            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            var reader = cmd.ExecuteReader();

            bool existe = reader.HasRows;

            if (!existe)
            {
                reader.Close();

                SqlCommand cmd2 = new SqlCommand("INSERT INTO [dbo].[PacienteMedico]" +
                "([IdPaciente], [IdMedico], [Habilitado])" +
                "VALUES (" + pacienteMedico.IdPaciente + ", " + pacienteMedico.IdMedico + ",1)");

                try
                {
                    cmd2.Connection = connection;
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    connection.Close();

                    statusMessage.Status = 1;
                    statusMessage.Mensaje = e.Message;
                }
            }

            connection.Close();
        }
    }
}
