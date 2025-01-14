using Game2048.DataAccess.Entities;
using SQLite;

namespace Game2048.DataAccess.Repositories
{
    public class ScoreRepository
    {
        SQLiteAsyncConnection Database;

        public ScoreRepository()
        {
        }

        private async Task Init()
        {
            if (this.Database is not null)
                return;

            this.Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await this.Database.CreateTableAsync<Score>();
        }

        public async Task<List<Score>> GetScoresAsync()
        {
            await Init();
            return await Database.Table<Score>()
                .OrderByDescending(s => s.Points)
                .ThenBy(s => s.Moves)
                .ThenByDescending(s => s.CreatedOn)
                .ToListAsync();
        }

        public async Task<Score> GetScoreAsync(Guid id)
        {
            await Init();
            return await Database.Table<Score>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveScoreAsync(Score item)
        {
            await this.Init();
            return await this.Database.InsertAsync(item);
        }
    }
}