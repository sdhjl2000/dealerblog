using Auto.Lib3.DataAccess;
using Autohome.CSH.Framework.Data;

namespace DealerBlog.DAL
{
    public partial class CategoryDAL
    {
        private static DatabaseProperty CONN = DBSettings.GetDatabaseProperty("Local");
    }
}