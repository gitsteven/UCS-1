using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace UCS.Database
{
    internal class ucsdbEntities : DbContext
    {
        #region Public Constructors

        public ucsdbEntities(string connectionString)
            : base("name=" + connectionString)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public virtual DbSet<clan> clan { get; set; }

        public virtual DbSet<player> player { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        #endregion Protected Methods
    }
}