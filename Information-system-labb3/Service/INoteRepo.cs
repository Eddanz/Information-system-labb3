using Information_system_labb3.Models;

namespace Information_system_labb3.Service
{
    public interface INoteRepo
    {
        // Method to get all notes
        Task<IEnumerable<Note>> GetAllNotesAsync();

        // Method to get note by id
        Task<Note> GetNoteByIdAsync(int id);

        // Method to add note
        Task AddNoteAsync(Note note);

        // Method to update note
        Task UpdateNoteAsync(Note note);

        // Method to delete note
        Task DeleteNoteAsync(Note note);
    }
}
