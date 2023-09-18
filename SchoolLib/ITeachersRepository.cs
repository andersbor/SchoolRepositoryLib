namespace SchoolLib
{
    public interface ITeachersRepository
    {
        Teacher Add(Teacher teacher);
        IEnumerable<Teacher> Get(string? orderBy = null, string? namePart = null, int? salaryBelow = null);
        Teacher? GetById(int id);
        Teacher? Remove(int id);
        Teacher? Update(int id, Teacher data);
    }
}