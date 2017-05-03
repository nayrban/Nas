using System.Data.Entity;

namespace NasData.Configuration
{
    public class AuthModelInitializer : DropCreateDatabaseIfModelChanges<AuthContext>
    {
        protected override void Seed(AuthContext context)
        {
            base.Seed(context);
        }
    }
}
