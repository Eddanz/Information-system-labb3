using Information_system_labb3.Data;
using Information_system_labb3.Models;
using Microsoft.EntityFrameworkCore;

namespace Information_system_labb3.Service
{
    public class NoteRepository : INoteRepo
    {
        private readonly AppDbContext _context;
        public NoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddNoteAsync(Note note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(Note note)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<Note> GetNoteByIdAsync(int id)
        {
            return await _context.Notes.FindAsync(id);
        }

        public async Task UpdateNoteAsync(Note note)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
        }
    }
}
