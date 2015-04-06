public interface INotesRepository
{
IEnumerable<Note> GetAll();
Note Get(int id);
Note Add(Note item);
bool Update(Note item);
bool Delete(int id);
}
