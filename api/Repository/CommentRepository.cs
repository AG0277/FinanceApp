using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext dbContext;
        public CommentRepository(ApplicationDbContext DBContext)
        {
            dbContext = DBContext;
        }
        public async Task<List<Comment>> GetAllAsync()
        {
            return await dbContext.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}