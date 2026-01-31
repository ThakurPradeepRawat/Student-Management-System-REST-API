using Common.Model.Entities;
using System.Collections.Generic;

namespace Common.DAL.Interface
{
    public interface IStudentRepository
    {
        int Add(StudentModel student);
        void Update(StudentModel student);

        IEnumerable<StudentModel> GetById(int StudentID);

        IEnumerable<StudentModel> GetAll();

        void Delete(int StudentID);
        HashSet<string> FetchEmail();

    }
}
