using SQLite;

namespace Game2048.DataAccess.Entities
{
    public record Score
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public int Points { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Moves { get; set; }
    }
}
