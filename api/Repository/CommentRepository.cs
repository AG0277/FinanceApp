using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await dbContext.Comments.AddAsync(commentModel);
            await dbContext.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            { return null; }
            dbContext.Comments.Remove(comment);
            await dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await dbContext.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var comment = await dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return null;
            }
            comment.Title = commentModel.Title;
            comment.Content = commentModel.Content;
            await dbContext.SaveChangesAsync();
            return comment;
        }
    }
}