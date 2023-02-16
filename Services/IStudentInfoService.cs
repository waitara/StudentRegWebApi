using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApi.Models;

namespace StudentRegWebApi.Services
{
    public interface IStudentInfoService
    {
        int Add(StudentInfo studentInfo);
        int AddRange(IEnumerable<StudentInfo> students);
        IEnumerable<StudentInfo> GetAll();
        StudentInfo Find(int id);
        int Remove(int id);
        int Update(StudentInfo studentInfo);
    }
}
