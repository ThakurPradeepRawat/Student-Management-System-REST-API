using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model.DTO;
using Common.Model.Entities;

namespace Common.BAL.Interfaces
{
    public interface IStudentService
    {
        int AddStudent(CreateStudentRequestDTO dto);
        IEnumerable<StudentModel> GetAll();

        void DeleteStudent(int StudentID);

        void UpdateStudent(UpdateStudentRequestDTO dto);
        IEnumerable<StudentModel> GetByID(int StudentID);
    }
}
