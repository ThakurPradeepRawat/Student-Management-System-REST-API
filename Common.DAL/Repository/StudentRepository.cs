using Common.DAL.Base;
using Common.DAL.DataBase;
using Common.DAL.Exceptions;
using Common.DAL.Interface;
using Common.DAL.Mapper;
using Common.Model.DTO;
using Common.Model.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;

namespace Common.DAL.Repository
{
    public class StudentRepository : RepositoryBase, IStudentRepository 
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public StudentRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public int Add(StudentModel student)
        {
            return ExuteDb(() =>
        {
            SqlConnection con = _connectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_AddStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
            cmd.Parameters.AddWithValue("@LastName", student.LastName);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@MobileNumber", student.MobileNumber);
            cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
            cmd.Parameters.AddWithValue("@FathersName", student.FathersName);
            cmd.Parameters.AddWithValue("@MothersName", student.MothersName);
            cmd.Parameters.AddWithValue("@Address", student.Address);
            cmd.Parameters.AddWithValue("@IsActive", student.IsActive);
            cmd.Parameters.AddWithValue("@CreatedAt", student.CreatedAt);
            cmd.Parameters.AddWithValue("@UpdateAt", student.UpdatedAt);
            con.Open();
            var dr = cmd.ExecuteReader();
     
            while (dr.Read())
            {
                if ((int)dr["Success"] == 0)
                {
                    throw new EmailException(dr["Message"].ToString());
                }
              
            }
            con.Close();
            return 6;

        }, "Failed to add Student");
        }

        public void Update(StudentModel student)
        {
            ExuteDb(() =>
            {
                SqlConnection con = _connectionFactory.CreateConnection();
                SqlCommand cmd = new SqlCommand("sp_updateStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentID", student.StudentID);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@MobileNumber", student.MobileNumber);
                cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@FathersName", student.FathersName);
                cmd.Parameters.AddWithValue("@MothersName", student.MothersName);
                cmd.Parameters.AddWithValue("@Address", student.Address);
                cmd.Parameters.AddWithValue("@UpdateAt", student.UpdatedAt);
                con.Open();
                int rowUpdates = cmd.ExecuteNonQuery();
                con.Close();
            }, "Student Not Updated");
        
        }

        public void Delete(int StudentId)
        {
            ExuteDb(() =>
            {
                SqlConnection con = _connectionFactory.CreateConnection();
                SqlCommand cmd = new SqlCommand("sp_DeleteStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentID", StudentId);
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if ((int)dr["Success"] == 0)
                    {
                        throw new StudentIdException(dr["message"].ToString());
                    }
                }
                
                con.Close();
            },"Student Not Deleted"); 
        }

        public IEnumerable<StudentModel> GetById(int StudentId)
        {
           return ExuteDb(() =>
            {
                SqlConnection con = _connectionFactory.CreateConnection();
                SqlCommand cmd = new SqlCommand("sp_GetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentID", StudentId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                var student = new List<StudentModel>();
                while (dr.Read())
                {
                    student.Add(StudentMapper.Map(dr));
                }
                con.Close();
                return student;
            } ,"Student Data not Fetched");



        }

        public IEnumerable<StudentModel> GetAll()
        {
            return ExuteDb(() =>
            {
                var students = new List<StudentModel>();
                SqlConnection con = _connectionFactory.CreateConnection();
                SqlCommand cmd = new SqlCommand("sp_GetAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(StudentMapper.Map(reader));
                }
                con.Close();

                return students;
            }, "Studens Data not Fetched");


        }
        public async Task <string> BulkDataValidation(DataTable dt)
        {
            string json;
            SqlConnection con = _connectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_ValidateStudents", con);
            cmd.CommandType= CommandType.StoredProcedure;
            var batchParam = new SqlParameter("@BatchID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(batchParam);
            var param = cmd.Parameters.AddWithValue("@Students", dt);
            param.SqlDbType = SqlDbType.Structured;
            param.TypeName = "StudentTableType";
            await con.OpenAsync();
            json = (string) await cmd.ExecuteScalarAsync();
            return json;

        }
        public void AddBulk(int BatchID)
        {
            SqlConnection con = _connectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_SubmitBulkData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BatchID", BatchID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public string  GetPassWord(string Email)
        {
            SqlConnection con = _connectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_CheckCredentials", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", Email);
            con.Open();
            object result  = cmd.ExecuteScalar();
            if (result == null)
                throw new UnauthorizedAccessException("Invalid credentials");
            con.Close();
            string HashPassword= result.ToString();
            Console.Write(result);
            return HashPassword ;

        }

        public void InsertRefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            SqlConnection connection = _connectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_InsertRefreshToken", connection);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", refreshTokenDTO.Email);
            cmd.Parameters.AddWithValue("@RefreshToken", refreshTokenDTO.RefreshToken);
            cmd.Parameters.AddWithValue("ExpiredAt", refreshTokenDTO.ExpiredAt);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void RagisterUser(string Email, string password)
        {
            SqlConnection conn = _connectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_RagisterUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue ("@PasswordHash", password);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public RefreshTokenDTO GetRefreshTokenDetail(string RefreshToken)
        {

            SqlConnection con = _connectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_GetRefreshTokenDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RefreshToken", RefreshToken);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            var refreshTokenDTO = new RefreshTokenDTO
                {
                    Email = dr["Email"].ToString(),
                    RefreshToken = dr["RefreshToken"].ToString(),
                    IsRevoked = (bool)dr["IsRevoked"],
                    ExpiredAt = (DateTime)dr["ExpiresAt"]
                };
            con.Close();
            return refreshTokenDTO;

        }


        public void UpdateRefreshTokenDetail(string RefreshToken)
        {
            SqlConnection con = _connectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_UpdateRefreshToken", con);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RefreshToken", RefreshToken);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}
