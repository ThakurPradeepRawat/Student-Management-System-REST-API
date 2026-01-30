using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model.Entities;
using Microsoft.Data.SqlClient;

namespace Common.DAL.Mapper
{

    public static class StudentMapper
    {
        public static StudentModel Map(SqlDataReader reader)
        {
            return new StudentModel
            {
                StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                MobileNumber = reader.GetString(reader.GetOrdinal("MobileNumber")),

                DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth"))
                    ? default
                    : DateOnly.FromDateTime(
                        reader.GetDateTime(reader.GetOrdinal("DateOfBirth"))
                      ),

                FathersName = reader.GetString(reader.GetOrdinal("FathersName")),
                MothersName = reader.GetString(reader.GetOrdinal("MothersName")),
                Address = reader.GetString(reader.GetOrdinal("Address")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),

                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),

                UpdatedAt = reader.IsDBNull(reader.GetOrdinal("UpdateAt"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("UpdateAt"))
            };
        }
    }

}
