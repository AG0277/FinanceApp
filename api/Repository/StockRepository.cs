using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
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

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = dbContext.Stocks.Include(c => c.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            Stock? stockModel = await dbContext.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
            return stockModel;
        }

        public async Task<bool> StockExists(int id)
        {
            return await dbContext.Stocks.AnyAsync(s => s.Id == id);
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