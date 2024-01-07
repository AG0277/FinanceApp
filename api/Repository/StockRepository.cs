using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext dbContext;
        public StockRepository(ApplicationDbContext DBContext)
        {
            dbContext = DBContext;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await dbContext.Stocks.AddAsync(stockModel);
            await dbContext.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            Stock? stockModel = await dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            dbContext.Stocks.Remove(stockModel);
            await dbContext.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await dbContext.Stocks.Include(c=>c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            Stock? stockModel = await dbContext.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(x => x.Id == id);
            return stockModel;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateDto)
        {
            Stock? existingStockModel = await dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStockModel == null)
            {
                return null;
            }

            existingStockModel.Symbol = updateDto.Symbol;
            existingStockModel.CompanyName = updateDto.CompanyName;
            existingStockModel.Purchase = updateDto.Purchase;
            existingStockModel.LastDiv = updateDto.LastDiv;
            existingStockModel.Industry = updateDto.Industry;
            existingStockModel.MarketCap = updateDto.MarketCap;

            await dbContext.SaveChangesAsync();
            return existingStockModel;
        }
    }
}